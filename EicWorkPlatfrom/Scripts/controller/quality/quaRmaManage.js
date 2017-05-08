
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');
//数据访问工厂
qualityModule.factory("rmaDataOpService", function (ajaxService) {
    var rma = {};
    var quaRmaManageUrl = "/quaRmaManage/";

    //自动生成RMA表单号
    rma.autoCreateRmaId = function () {
        var url = quaRmaManageUrl + 'AutoCreateRmaId';
        return ajaxService.getData(url, {
        });
    };
    ///获取RMA表单单头
    rma.getRmaReportMaster = function (rmaId) {
        var url = quaRmaManageUrl + 'GetRmaReportMaster';
        return ajaxService.getData(url, {
            rmaId: rmaId,
        });
    };

    ////// 存储初始创建的Rma表
    rma.storeRmaBuildRmaIdData = function (initiateData) {
        var url = quaRmaManageUrl + 'StoreinitiateDataData';
        return ajaxService.postData(url, {
            initiateData: initiateData,
        });
    };

    //////// 得到 Rma表描述
    rma.getRmaBussesDescriptionDatas = function (rmaId) {
        var url = quaRmaManageUrl + "GetRmaBussesDescriptionDatas";
        return ajaxService.getData(url, {
            rmaId: rmaId
        });
    };

    //获取退货单数据
    rma.getReturnOrderInfo = function (orderId) {
        var url = quaRmaManageUrl + 'GetReturnOrderInfo';
        return ajaxService.getData(url, {
            orderId: orderId,
        });
    };

    return rma;
})
////创建RMA表单
qualityModule.controller('createRmaFormCtrl', function ($scope, rmaDataOpService) {
    leeHelper.setWebSiteTitle("质量管理", "创建RMA表单");
    ///视图模型
    var uiVm = $scope.vm = {
        RmaId: null,
        ProductName: null,
        CustomerShortName: null,
        RmaIdStatus: "未结案",
        OpPerson: null,
        OpSign: null,
        Id_Key: 0
    };
    $scope.vm = uiVm;
    //初始化原型
    var initVM = _.clone(uiVm);
    var vmManager = {
        //自动生成RMA编号
        autoCreateRmaId: function () {
            $scope.doPromise = rmaDataOpService.autoCreateRmaId.then(function (rmaId) {
                uiVm.RmaId = rmaId;
            });
        },
        //获取表单数据
        getRmaFormDatas: function () {
            $scope.searchPromise = rmaDataOpService.getRmaReportMaster(uiVm.RmaId).then(function (data) {
                vmManager.dataSets = data;
            });
        },
        dataSets: [],
        init: function () {
            uiVm = _.clone(initVM);
            uiVm.OpSign = 'add';
            $scope.vm = uiVm;
        },
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);

    $scope.operate = operate;
    operate.edit = function (item) {
        item.OpSign = 'edit';
        $scope.vm = uiVm = item;
    };
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
        rmaDataOpService.storeRmaBuildRmaIdData(uiVm).then(function (opresult) {
            if (opresult.Result) {
                var dataItem = _.clone(uiVm);
                dataItem.Id_Key = opresult.Id_Key;
                if (dataItem.OpSign === 'add') {
                    vmManager.dataSets.push(dataItem);
                }
                vmManager.init();
            }
        })
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    };
});
//// 描述RMA登记
qualityModule.controller('rmaInputDescriptionCtrl', function ($scope) {
    leeHelper.setWebSiteTitle("质量管理", "RMA表单描述登记");
    var uiVm = $scope.vm = {
        RmaId: null,
        RmaIdNumber: 0,
        ReturnHandleOrder: null,
        ProdcutId: null,
        ProductName: null,
        ProductSpec: null,
        ProductCount: null,
        CustomerId: null,
        CustomerName: null,
        SalesOrder: null,
        ProductsShipDate: null,
        BadDescrption: null,
        CustomerHandleSuggestion: null,
        FeePaymentWay: null,
        HandleStatus: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null,
    }
    var initVM = _.clone(uiVm);
    ///视图模型
    var rma = $scope.rmavm = {
        RmaId: null,
        CustomerId: null,
        CustomerShortName: null,
    };
    var vmManager = {
        windowOpen: false,
        showWindow: function () {
            vmManager.windowOpen = !vmManager.windowOpen;
        },
        returnOrderDatas: [],
        //获取ERP退货单信息
        getReturnOrderInfo: function ($event) {
            if ($event.keyCode === 13)


                vmManager.showWindow();
        },
        //获取表单数据
        getRmaFormDatas: function () { },
        dataSets: [],
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };
})
////检验处置
qualityModule.controller('rmaInspectionHandleCtrl', function ($scope) {
    leeHelper.setWebSiteTitle("质量管理", "RMA检验处置");
    ///视图模型
    var rmaVm = $scope.rmavm = {
        RmaId: null,
        CustomerId: null,
        CustomerShortName: null,
    };

    var vmManager = {
        activeTab: 'businessTab',
        editWindowDisplay: false,
        showEditFormWindow: function () {
            vmManager.editWindowDisplay = !vmManager.editWindowDisplay;
        },
        //获取表单数据
        getRmaFormDatas: function () { },
        datasets: [],
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };
})