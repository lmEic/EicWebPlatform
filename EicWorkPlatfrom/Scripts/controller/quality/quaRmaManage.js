
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');
//数据访问工厂
qualityModule.factory("rmaDataOpService", function (ajaxService) {
    var rma = {};
    var quaRmaManageUrl = "/quaRmaManage/";

    /////////// 生成创建的Rma表RmaId
    rma.createRmaId = function () {
        var url = quaRmaManageUrl + "CreateRmaId";
        return ajaxService.getData(url, {
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

    return rma;
})
////创建表单
qualityModule.controller('createRmaFormCtrl', function ($scope, rmaDataOpService) {
    ///视图模型
    var uiVm = $scope.vm = {
        RmaId: null,
        ProductName: null,
        CustomerShortName: null,
        RmaIdStatus: "未结案",
        RmaMonth: 04,
        RmaYear: 17,
        OpPerson: null,
        OpSign: null,
    };

    var vmManager = {
        activeTab: 'initTab',
        //自动生成RMA编号
        autoCreateRmaId: function () {
            rmaDataOpService.createRmaId().then(function (data) {
                uiVm.RmaId = data;
            });
        },
        //获取表单数据
        getRmaFormDatas: function () { },
        datasets: [],
        dataSource: [],
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);

    $scope.operate = operate;
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVM);
        var dataItem = _.clone(uiVM);
        uiVm.OpSign = 'add';
        vmManager.dataSource = $scope.vm;
        rmaDataOpService.storeRmaBuildRmaIdData(vmManager.dataSource).then(function (Reslut) {
            if (opresult.Result) {

                vmManager.datasets = [];
                vmManager.dataSource = [];
            }
        })
    };
    operate.refresh = function () { };


});
//// 描述登记
qualityModule.controller('rmaInputDescriptionCtrl', function ($scope) {
    ///视图模型
    var rmaVm = $scope.rmavm = {
        RmaId: null,
        CustomerId: null,
        CustomerShortName: null,
    };

    var vmManager = {
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

////检验处置
qualityModule.controller('rmaInspectionHandleCtrl', function ($scope) {
    ///视图模型
    var rmaVm = $scope.rmavm = {
        RmaId: null,
        CustomerId: null,
        CustomerShortName: null,
    };

    var vmManager = {
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