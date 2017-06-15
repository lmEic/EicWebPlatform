/// <reference path="quaInspectionManage.js" />

/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');
//数据访问工厂
qualityModule.factory("rmaDataOpService", function (ajaxService) {
    var rma = {};
    var quaRmaManageUrl = "/quaRmaManage/";
    //-------------RMA表单创建----------------------------
    //自动生成RMA表单号
    rma.autoCreateRmaId = function () {
        var url = quaRmaManageUrl + 'AutoBuildingRmaId';
        return ajaxService.getData(url, {
        });
    };
    ///获取RMA表单单头
    rma.getRmaReportMaster = function (rmaId) {
        var url = quaRmaManageUrl + 'GetRmaReportMaster';
        return ajaxService.getData(url, {
            rmaId: rmaId
        });
    };
    ////// 存储初始创建的Rma表
    rma.storeRmaBuildRmaIdData = function (initiateData) {
        var url = quaRmaManageUrl + 'StoreinitiateDataData';
        return ajaxService.postData(url, {
            initiateData: initiateData
        });
    };
    //----------------RMA业务处理--------------------------
    //////// 得到 Rma表描述
    rma.getRmaDescriptionDatas = function (rmaId) {
        var url = quaRmaManageUrl + "GetRmaDescriptionDatas";
        return ajaxService.getData(url, {
            rmaId: rmaId
        });
    };
    //获取退货单数据
    rma.getReturnOrderInfo = function (orderId) {
        var url = quaRmaManageUrl + 'GetReturnOrderInfo';
        return ajaxService.getData(url, {
            orderId: orderId
        });
    };
    ///保存RMA描述登记数据
    rma.storeRmaInputDescriptionData = function (model) {
        var url = quaRmaManageUrl + 'StoreRmaInputDescriptionData';
        return ajaxService.postData(url, {
            model: model
        });
    };
    //----------------RMA品保检测处理-----------------------
    rma.getRmaInspectionHandleDatas = function (rmaId) {
        var url = quaRmaManageUrl + 'GetRmaInspectionHandleDatas';
        return ajaxService.getData(url, {
            rmaId: rmaId
        });
    };
    rma.storeRmaInspectionHandleDatas = function (model) {
        var url = quaRmaManageUrl + 'StoreRmaInspectionHandleDatas';
        return ajaxService.postData(url, {
            model: model
        });
    };

    rma.queryRmaDatas = function (dateFrom, dateTo) {
        var url = quaRmaManageUrl + 'GetRmaDatas';
        return ajaxService.postData(url, {
            dateFrom: dateFrom,
            dateTo: dateTo
        });
    };
    return rma;
});
////创建RMA表单
qualityModule.controller('createRmaFormCtrl', function ($scope, rmaDataOpService) {
    leeHelper.setWebSiteTitle("质量管理", "创建RMA表单");
    ///视图模型
    var uiVm = $scope.vm = {
        RmaId: null,
        ProductName: null,
        CustomerShortName: null,
        RmaIdStatus: "空单号",
        RmaYear: null,
        RmaMonth: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: 0
    };
    $scope.vm = uiVm;
    //初始化原型
    var initVM = _.clone(uiVm);
    var vmManager = {
        //自动生成RMA编号
        autoCreateRmaId: function () {
            $scope.doPromise = rmaDataOpService.autoCreateRmaId().then(function (rmaId) {
                uiVm.RmaId = rmaId;
                uiVm.OpSign = leeDataHandler.dataOpMode.add;
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
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVm;
        }
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);

    $scope.operate = operate;
    operate.edit = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVm = item;
    };
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            rmaDataOpService.storeRmaBuildRmaIdData(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var dataItem = _.clone(opresult.Entity);
                        console.log(opresult.Entity);
                        dataItem.Id_Key = opresult.Id_Key;
                        if (dataItem.OpSign === leeDataHandler.dataOpMode.add) {
                            vmManager.dataSets.push(dataItem);
                        }
                        vmManager.init();
                    }
                });
            });
        });
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    };
});
//// 描述RMA登记
qualityModule.controller('rmaInputDescriptionCtrl', function ($scope, rmaDataOpService, $modal) {
    leeHelper.setWebSiteTitle("质量管理", "RMA表单描述登记");
    //需要存诸Model信息
    var uiVm = $scope.vm = {
        RmaId: null,
        RmaIdNumber: 0,
        ReturnHandleOrder: null,
        ProductId: null,
        ProductName: null,
        ProductSpec: null,
        ProductCount: null,
        RealityHandleProductCount: null,
        CustomerId: null,
        CustomerName: null,
        SalesOrder: null,
        ProductsShipDate: null,
        BadDescription: null,
        CustomerHandleSuggestion: null,
        FeePaymentWay: null,
        HandleStatus: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: 0
    };
    var initVM = _.clone(uiVm);
    ///视图模型
    var rmavm = $scope.rmavm = {
        RmaId: null,
        ProductName: null,
        CustomerShortName: null
    };
    ///导入ERP信息对话框
    var dialog = $scope.dialog = leePopups.dialog();
    ///删除对话框
    var deleteDialog = $scope.deleteDialog = leePopups.dialog();

    var vmManager = {
        //初始化
        init: function () {
            uiVm = _.clone(initVM);
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVm;
        },
        //是否为输入状态
        isdisabled: false,
        returnOrderDatas: [],
        //获取预处理数据
        getPreHandleData: function () {
            $scope.searchPromise = rmaDataOpService.getRmaDescriptionDatas(uiVm.RmaId).then(function (data) {
                if (angular.isObject(data)) {
                    vmManager.dataSets = [];
                    leeHelper.copyVm(data.rmaInitiateData, rmavm);
                    vmManager.dataSets = data.bussesDescriptionDatas;
                    vmManager.isdisabled = true;
                }
            });
        },
        //获取ERP退货单信息
        getReturnOrderData: function ($event) {
            if ($event.keyCode === 13)
                $scope.searchPromise = rmaDataOpService.getReturnOrderInfo(uiVm.ReturnHandleOrder).then(function (datas) {
                    vmManager.returnOrderDatas = datas;
                    dialog.show();
                });
        },
        //选ERP订单信息
        selectReturnOrderItem: function (item) {
            leeHelper.copyVm(item, uiVm);
            uiVm.RealityHandleProductCount = uiVm.ProductCount;
            $scope.vm = uiVm;
            dialog.close();
        },
        
        dataSets: [],
        ///删除数据
        deleteItemReturnHandleOrder: null,
        deleteItemProductName:null,
        deleteItemProductCount:null,
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    ///编辑
    operate.editItem = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVm = item;
    };
    //删除
    operate.deleteItem = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.delete;
        $scope.vm = uiVm = item;
        vmManager.deleteItemReturnHandleOrder = item.ReturnHandleOrder;
        vmManager.deleteItemProductName = item.ProductName;
        vmManager.deleteItemProductCount = item.ProductCount;
        deleteDialog.show();
    };
    //取消删除
    operate.cancelDeleteItem = function () {
        item.OpSign = leeDataHandler.dataOpMode.add;
        deleteDialog.close();
    };
    //复制
    operate.copyItem = function (item) {
        var oldItem = _.clone(item);
        uiVm = oldItem;
        uiVm.Id_Key = null;
        uiVm.ProductId = null;
        uiVm.ProductName = null;
        uiVm.ProductSpec = null;
        uiVm.ProductCount = null;
        uiVm.CustomerId = null;
        uiVm.CustomerName = null;
        uiVm.OpSign = leeDataHandler.dataOpMode.add;
        $scope.vm = uiVm;
        var dataItem = _.clone(uiVm);
        $scope.vm = uiVm = dataItem;

    };
    //保存
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            rmaDataOpService.storeRmaInputDescriptionData(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var dataItem = _.clone(uiVm);
                        dataItem.Id_Key = opresult.Id_Key;
                        if (dataItem.OpSign === leeDataHandler.dataOpMode.add) {
                            vmManager.dataSets.push(dataItem);
                        }
                        if (dataItem.OpSign === leeDataHandler.dataOpMode.delete) {
                            deleteDialog.close();
                        }
                        vmManager.init();
                    }
                });
            });
        });
    }
    //刷新
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () { vmManager.init(); });
    };
});
////检验处置
qualityModule.controller('rmaInspectionHandleCtrl', function ($scope, rmaDataOpService) {
    leeHelper.setWebSiteTitle("质量管理", "RMA检验处置");
    ///视图模型
    var rmavm = $scope.rmavm = {
        RmaId: null,
        ProductName: null,
        CustomerShortName: null
    };
    ///处理数据视图
    var uiVm = $scope.vm = {
        RmaId: null,
        RmaIdNumber: 0,
        RmaBussesesNumberStr:'',
        BadPhenomenon: null,
        BadDescription: null,
        ParameterKey: null,
        BadReadson: null,
        HandleWay: null,
        ResponsiblePerson: null,
        FinishDate: null,
        PayTime: null,
        LiabilityBelongTo: null,
        HandleStatus: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null
    };
    var initVM = _.clone(uiVm);
    var vmManager = $scope.vmManager = {
        init: function () {
            uiVm = _.clone(initVM);
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVm;
        },
        ///首次活动的版
        activeTab: 'businessTab',
        //获取表单数据
        getRmaInspectionHandleDatas: function (){
            if (uiVm.RmaId === null || uiVm.RmaId === "") return;
            $scope.searchPromise = rmaDataOpService.getRmaInspectionHandleDatas(uiVm.RmaId).then(function (data) {
                if (angular.isObject(data))
                {
                    ///把rmaInitiateData赋值到rmavm中
                    leeHelper.copyVm(data.rmaInitiateData, rmavm);
                    ///业务数据
                    vmManager.businessHandleDatas = data.bussesDescriptionDatas;
                    ///品保数据
                    vmManager.dataSets = data.inspectionHandleDatas;
                    /// 对已经处理
                    vmManager.isHanldlestatus();
                }
            });
        },
        businessHandleDatas: [],
        dataSets: [],
        businessHandleNumberDatas: [],
        selectedBusinessRmaNumberStr: [],
        isHanldlestatus: function () {
            if (vmManager.businessHandleDatas.length > 0 && vmManager.dataSets.length > 0) {
                angular.forEach(vmManager.businessHandleDatas, function (item) {
                    angular.forEach(vmManager.dataSets, function (Handleitem) {
                        var str = Handleitem.RmaBussesesNumberStr;
                        if (str == null || str.length==0) return;
                        var stringstr = str.split(',');
                        stringstr.forEach(function (i) {
                            if (i == item.RmaIdNumber) {
                                item.isHandle = true;
                            }
                        });
                    });
                });
            }
        },
    };
    var rmaNumberDatasDialog = $scope.rmaNumberDatasDialog = leePopups.dialog();
    var dialog = $scope.dialog = leePopups.dialog();
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    ///显示业务处理序号
    operate.getSelectedRmaIdData = function (item) {
        if (vmManager.businessHandleDatas.length > 0) {
            vmManager.selectedBusinessRmaNumberStr = [];
            vmManager.businessHandleNumberDatas = [];
            var dataitems = _.clone(vmManager.businessHandleDatas);
                angular.forEach(dataitems, function (item) {
                    var dataItem = { value: item.RmaIdNumber, label:'序号:'+item.RmaIdNumber+'工单:'+item.ReturnHandleOrder };
                    vmManager.businessHandleNumberDatas.push(dataItem);
                })
                dataitems = [];
        };
    };
    operate.handleItem = function (item) {
        var dataitem = _.clone(item);
        uiVm.ParameterKey = item.RmaId + "&" + item.ReturnHandleOrder + "&" + item.ProductId ;
        dataitem.OpSign = leeDataHandler.dataOpMode.add;
        leeHelper.copyVm(dataitem, uiVm);
        $scope.vm = uiVm;
        dialog.show();
   
    };
    operate.editItem = function (item) {
        var dataitem = item;
        dataitem.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVm = dataitem;
        dialog.show();
    };
    ///数组合并用逗号分开
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            if (vmManager.selectedBusinessRmaNumberStr.length>0){
                uiVm.RmaBussesesNumberStr = vmManager.selectedBusinessRmaNumberStr.join(',');
            }
            rmaDataOpService.storeRmaInspectionHandleDatas(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var dataItem = _.clone(uiVm);
                        dataItem.Id_Key = opresult.Id_Key;
                        if (dataItem.OpSign === 'add') {
                            vmManager.dataSets.push(dataItem);
                        };
                        vmManager.isHanldlestatus();
                        vmManager.init();
                        dialog.close();
                    }
                });
               
            });
        });
    };
    
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () { vmManager.init(); });
    };
});
////检验Rma查询管理
qualityModule.controller('rmaReportQueryCtrl', function ($scope, rmaDataOpService) {
    leeHelper.setWebSiteTitle("质量管理", "RMA查询检验管理");
    var vmManager = $scope.vmManager = {
        init: function () {
           vmManager. dataSets= [];
           vmManager. dateFrom= null;
           vmManager. dateTo= null;
        },
        //获取表单数据
        getRmaDatas: function () {
            if (vmManager.dateFrom === null || vmManager.dateTo === "") return;
            $scope.searchPromise = rmaDataOpService.queryRmaDatas(vmManager.searchFromYear, vmManager.searchToYear).then(function (data) {
                vmManager.dataSets = data;
            });
        },
        dataSets: [],
        searchFromYear: '',
        searchToYear: '',
    };
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    ///显示业务处理序号
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () { vmManager.init(); });
    };
});