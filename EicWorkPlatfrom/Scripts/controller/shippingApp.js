/// <reference path="../angular.js" />
/// <reference path="../../Content/underscore/underscore.js" />
/// <reference path="../../Content/angular-busy/dist/angular-busy.min.js" />
angular.module('mesOptical.ShippingApp', ['ngAnimate', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap'])
 .config(function ($asideProvider) {
        angular.extend($asideProvider.defaults, {
            container: 'body',
            html: true
        });
 })
//格式化MVC端返回的Json格式的日期
.filter('jsonDate',function(){
    return function (x) {
        if (x == null) return null;
        else {
            return new Date(parseInt(x.substring(6)));
        }
    };
})
.controller('shippingCtrl', function ($scope, $http, $aside) {
    //出货排程对象
    var shippingBoard = {
        datasContext: [],//数据源上下文
        productCategories: [],//产品列表
        selectedProductCategory: null, //选中的产品列表项
        datasource: [],//过滤之后的数据源
        productTypeDatas: [],//产品型号列表
        selectedProductType: null,//选中的产品型号
        productTypeDetailDatas: [],//产品型号具体排程出货列表
        productTypeDetailDataCopy: [],//产品型号具体排程出货列表副份
        sumerizeDataByYear: [],//按年份汇总出货排程数据
        selectedYear: null,//选中的年份
        sumerizeDataByMonth: [],//按月份汇总出货排程数据
        selectedMonth: null,//选中的月份
        shippingBoardSwitch: 'on' // 出货排程面板日期切换开关
    };
    //创按日期进行显示的对象
    var shippingDateBoard = Object.create(shippingBoard);

    shippingDateBoard.selectedYearMonth= null,//选择的年月份
    shippingDateBoard.shippingDatasource= [],//出货排程数据源,分组汇总整理之后
    shippingDateBoard.shippingDataDetails= [],//出货排程数据源,分组汇总整理之后
    shippingDateBoard.shippingDataDetails= [],//按年月选择过滤出的出货排程数据源
    shippingDateBoard.ShippingDatasGroupByMonth= [],//对出货排程按年月进行分组汇总

    $scope.shippingBoard = shippingBoard;
    $scope.shippingDateBoard = shippingDateBoard;

    //按产品类别显示产品型号的详细列表数据
    shippingBoard.showDataByCategory = function (category) {
        shippingBoard.selectedProductCategory = category;
        shippingBoard.productTypeDatas = _.where(shippingBoard.datasource, { category: category });
        _.sortBy(shippingBoard.productTypeDatas, 'count');
        shippingBoard.productTypeDetailDatas = [];
    };
    ///显示每个产品型号的详细出货排程列表
    shippingBoard.showShippingDataBy = function (productType) {
        shippingBoard.selectedProductType = productType;
        shippingBoard.productTypeDetailDatas = _.where(shippingBoard.datasContext, { ProductType: productType });
        _.sortBy(shippingBoard.productTypeDetailDatas, 'ShippingDate');

        shippingBoard.productTypeDetailDataCopy = shippingBoard.productTypeDetailDatas;
        //按年份对此产品型号进行数据汇总
        shippingBoard.sumerizeDataByYear = [];
        shippingBoard.sumerizeDataByMonth = [];
        var years = [];
        angular.forEach(shippingBoard.productTypeDetailDatas, function (item, key) {
            if (!_.contains(years, item.AnalogYear)) {
                years.push(item.AnalogYear);
                shippingBoard.sumerizeDataByYear.push({ shippingYear: item.AnalogYear, count: item.ShippingCount });
            }
            else {
                var yeardata = _.findWhere(shippingBoard.sumerizeDataByYear, { shippingYear: item.AnalogYear });
                yeardata.count += item.ShippingCount;
            }
        });
    };
    ///按年份显示每个产品型号的详细出货排程列表
    shippingBoard.showShippingDataByYear = function (year) {
        shippingBoard.selectedYear = year;
        shippingBoard.productTypeDetailDatas = _.where(shippingBoard.productTypeDetailDataCopy, { AnalogYear: year });
        _.sortBy(shippingBoard.productTypeDetailDatas, 'ShippingDate');

        //按月份对此产品型号进行数据汇总
        shippingBoard.sumerizeDataByMonth = [];
        var months = [];
        angular.forEach(shippingBoard.productTypeDetailDatas, function (item, key) {
            var monthkey = item.AnalogYear + "-" + item.AnalogMonth;
            if (!_.contains(months, monthkey)) {
                months.push(monthkey);
                shippingBoard.sumerizeDataByMonth.push({ shippingMonth: monthkey, count: item.ShippingCount, year: item.AnalogYear, month: item.AnalogMonth });
            }
            else {
                var monthdata = _.findWhere(shippingBoard.sumerizeDataByMonth, { shippingMonth: monthkey });
                monthdata.count += item.ShippingCount;
            }
        })
    };
    ///按月份显示每个产品型号的详细出货排程列表
    shippingBoard.showShippingDataByMonth = function (year, month) {
        shippingBoard.selectedMonth = year + "-" + month;
        shippingBoard.productTypeDetailDatas = _.where(shippingBoard.productTypeDetailDataCopy, { AnalogYear: year, AnalogMonth: month });
        _.sortBy(shippingBoard.productTypeDetailDatas, 'ShippingDate');
    };

    //面板开关切换
    shippingBoard.changeShippingBoardSwitch = function () {
        if (shippingBoard.shippingBoardSwitch === "on") {
            InitShippingDateBoard();
            shippingBoard.shippingBoardSwitch = "off";
        }
        else {
            shippingBoard.shippingBoardSwitch = "on";
        }
    };

    ///初始化出货排程日期面板
    var InitShippingDateBoard = function () {
        shippingDateBoard.datasource = [];
        shippingDateBoard.productCategories = [];
        shippingDateBoard.productTypeDetailDatas = [];
        //选择的年月份
        shippingDateBoard.selectedYearMonth = null;
        //出货排程数据源,分组汇总整理之后
        shippingDateBoard.shippingDatasource = [];
        //按年月选择过滤出的出货排程数据源
        shippingDateBoard.shippingDataDetails = [];
        //对出货排程按年月进行分组汇总
        shippingDateBoard.ShippingDatasGroupByMonth = [];

        angular.forEach(shippingDateBoard.datasContext, function (item) {
            var key = item.AnalogYear + '-' + item.AnalogMonth;

            var ym = _.findWhere(shippingDateBoard.ShippingDatasGroupByMonth, { key: key });
            if (ym === undefined) {
                shippingDateBoard.ShippingDatasGroupByMonth.push({ key: key, count: item.ShippingCount });
            }
            else {
                ym.count += item.ShippingCount;
            }
            var dataItem = _.findWhere(shippingDateBoard.shippingDatasource, { shippingDate: item.ShippingDate });
            if (dataItem === undefined) {
                var productTypeList = [{ productType: item.ProductType, shippingCount: item.ShippingCount }];
                dataItem = { key: key, productTypeList: productTypeList, shippingDate:item.ShippingDate, totalCount: item.ShippingCount };
                shippingDateBoard.shippingDatasource.push(dataItem);
            }
            else {
                dataItem.productTypeList.push({ productType: item.ProductType, shippingCount: item.ShippingCount });
                dataItem.totalCount = dataItem.totalCount + item.ShippingCount;
            }
        })
    };
    ///按出货日期显示出货排程明细表
    shippingDateBoard.showShippingDataByDate = function (selecteddate) {
        shippingDateBoard.productTypeDetailDatas = [];
        shippingDateBoard.shippingDataDetails = [];

        var day = selecteddate.getDate().toString();
        var qryDate = selecteddate.getFullYear() + "-" + (selecteddate.getMonth() + 1) + "-" + (day.length > 1 ? day : "0" + day);
        shippingDateBoard.productTypeDetailDatas = _.where(shippingDateBoard.datasContext, { ShippingDate: qryDate });
        if (shippingDateBoard.productTypeDetailDatas != null && shippingDateBoard.productTypeDetailDatas.length > 0) {
            shippingDateBoard.productTypeDetailDataCopy = shippingDateBoard.productTypeDetailDatas;

            shippingDateBoard.productCategories = [];
            var categories = [];//产品类别列表
            angular.forEach(shippingDateBoard.productTypeDetailDatas, function (item, key) {
                //获取产品类别列表
                if (!_.contains(categories, item.ProductCatalog)) {
                    categories.push(item.ProductCatalog);
                    shippingDateBoard.productCategories.push({ category: item.ProductCatalog, count: item.ShippingCount });
                }
                else {
                    var category = _.findWhere(shippingDateBoard.productCategories, { category: item.ProductCatalog });
                    category.count += item.ShippingCount;
                }
            })
            _.sortBy(shippingDateBoard.productTypeDetailDatas, 'ShippingCount');
        }
    }
    ///选出当前日期，选中类别的出货排程明细列表
    shippingDateBoard.showShippingDetailsByCategory = function (category) {
        shippingDateBoard.selectedProductCategory = category;
        shippingDateBoard.productTypeDetailDatas = _.where(shippingDateBoard.productTypeDetailDataCopy, { ProductCatalog: category });
        _.sortBy(shippingDateBoard.productTypeDetailDatas, 'ShippingCount');
    };
    ///按年月选出当前日期的出货排除明细表
    shippingDateBoard.showShippingDetailsByYearMonth = function (yearMonth)
    {
        shippingDateBoard.productTypeDetailDatas = [];
        shippingDateBoard.selectedYearMonth = yearMonth;
        shippingDateBoard.shippingDataDetails = _.where(shippingDateBoard.shippingDatasource, {key:yearMonth});
        shippingDateBoard.shippingDataDetails = _.sortBy(shippingDateBoard.shippingDataDetails, 'shippingDate');
    }

    ///WIP明细数据面板
    var wipDataBoard = {
        //WIP边侧显示面板配置参数
        asideConfig: {
            title: 'WIP',
            content: 'content',
            show: false,
            placement: 'left',
            animation: 'am-slide-left',
            scope: $scope,
            templateUrl: '/Shipping/ShowWipDetailBoard'
        },
        asideBoard: null,//WIP边侧显示面板,
        normalStationSets:[],//WIP正常生产站别设置
        wipDatas:[],
        dataDetails: [],//WIP数据明细,
        selectedBigStationItem: null,//选中的大站站别
        perBigStationWipDatas: [],//每个大站站别的WIP数据明细
        perStationWipDatas: [],//每个小站站别的WIP数据明细
        selectedStation: null,//选中的小站站别
    };
    $scope.wipDataBoard = wipDataBoard;

    ///显示WIP详细信息面板
    wipDataBoard.showWipDetailsDataBy = function (productType, direction) {
        //载入数据
        $scope.loadWipData(productType);
        //显示面板
        var showDirection = direction || 'left';
        wipDataBoard.asideConfig.placement = showDirection;
        wipDataBoard.asideConfig.animation = 'am-slide-' + showDirection;
        wipDataBoard.asideBoard = $aside(wipDataBoard.asideConfig);
        wipDataBoard.asideBoard.$promise.then(function () {
            wipDataBoard.asideBoard.show();
        });
    }
    ///显示每个大站站别的数据
    wipDataBoard.showPerBigSationWipData = function (bigStationItem)
    {
        wipDataBoard.selectedBigStationItem = bigStationItem;
        wipDataBoard.perBigStationWipDatas = [];

        var datas = _.where(wipDataBoard.wipDatas, { ProductBigStation: bigStationItem.productBigStation });
        if (datas != undefined && datas.length > 0) {
            angular.forEach(datas, function (dataItem) {
                var wipdata = _.findWhere(wipDataBoard.perBigStationWipDatas, { productStation: dataItem.ProductStation });
                if (wipdata === undefined) {
                    wipDataBoard.perBigStationWipDatas.push({ productStation: dataItem.ProductStation, stationTotalCount: dataItem.GoodCount });
                }
                else {
                    wipdata.stationTotalCount += dataItem.GoodCount;
                }
            });
        }
    }
    ///显示每个具体站别的数据
    wipDataBoard.showPerStationWipData = function (station)
    {
        wipDataBoard.selectedStation = station;
        wipDataBoard.perStationWipDatas = [];
        wipDataBoard.perStationWipDatas = _.where(wipDataBoard.wipDatas, { ProductStation: station });
        wipDataBoard.perStationWipDatas = _.sortBy(wipDataBoard.perStationWipDatas, "ProductDate");
    }
    ///载入出货排程数据
    $scope.loadShippingDateData = function () {
        shippingBoard.datasource = [];
        shippingBoard.productCategories = [];

        $scope.shippingDataPromise = $http.get('/Shipping/LoadShippingScheduleDatas').success(function (data) {
            shippingBoard.datasContext = data;
            angular.forEach(data, function (item, key) {
                var category = _.findWhere(shippingBoard.productCategories, { category: item.ProductCatalog });
                //获取产品类别列表
                if (category===undefined) {
                    shippingBoard.productCategories.push({ category: item.ProductCatalog, count: item.ShippingCount });
                }
                else {
                    category.count += item.ShippingCount;
                }

                //获取产品型号数据源
                var protype = _.findWhere(shippingBoard.datasource, { productType: item.ProductType });
                if (protype===undefined) {
                    shippingBoard.datasource.push({ productType: item.ProductType, count: item.ShippingCount, category: item.ProductCatalog });
                }
                else {
                    protype.count += item.ShippingCount;
                }
            })
        }).error(function (data, status) {
            console.log(status.statusText);
        });
    };
    ///载入某型号的WIP数据
    $scope.loadWipData = function (productType) {
        wipDataBoard.normalStationSets = [];
        wipDataBoard.wipDatas = [];
        wipDataBoard.dataDetails = [];

        $scope.wipPromise = $http.get('/Shipping/LoadProductWipDatasBy', {
            params: {
                productType: productType
            }
        }).success(function (data) {
            wipDataBoard.normalStationSets=data.normalStationSets;
            wipDataBoard.wipDatas=data.wipDatas;
            angular.forEach(data.normalStationSets, function (stationSetItem) {
                //汇总大站站别数据
                var stationItem = _.findWhere(wipDataBoard.dataDetails, { productBigStation: stationSetItem.ProductStation });//大站站别
                if (stationItem === undefined)
                {
                    var wipDatas = _.where(data.wipDatas, { ProductBigStation: stationSetItem.ProductStation });
                    if (wipDatas !== undefined && wipDatas.length > 0)
                    {
                        var bigStationTotalCount = 0;//大站站别汇总数据
                        angular.forEach(wipDatas, function (stationWipData) {
                            bigStationTotalCount = bigStationTotalCount + stationWipData.GoodCount;
                        });
                        stationItem = {
                            id: stationSetItem.FlowID,
                            productType: stationSetItem.ProductType,
                            productBigStation: stationSetItem.ProductStation,
                            totalCount: bigStationTotalCount
                        };
                        wipDataBoard.dataDetails.push(stationItem);
                    }
                }
            })
        });
    }
    $scope.loadShippingDateData();
})