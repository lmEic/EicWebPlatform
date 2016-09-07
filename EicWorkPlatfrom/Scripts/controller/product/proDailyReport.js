/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />

var productModule = angular.module('bpm.productApp');
productModule.factory('DReportDataOpService', function (ajaxService) {
    var urlPrefix = "/" + leeHelper.controllers.dailyReport + "/";
    var reportDataOp = {};
    //获取产品工艺流程列表
    reportDataOp.getProductFlowList = function (department, productName) {
        var url = urlPrefix + 'GetProductFlowList';
        return ajaxService.getData(url, {
            department: department,
            productName: productName,
        });
    };
    //保存产品工艺流程数据
    reportDataOp.storeProductFlowDatas = function (entities) {
        var url = urlPrefix + 'StoreProductFlowDatas';
        return ajaxService.postData(url, {
            entities: entities,
        });
    };
});
//标准工时设定
productModule.controller("dReportHoursSetCtrl", function ($scope, dataDicConfigTreeSet, connDataOpService) {
    ///工艺标准工时视图模型
    var uiVM = {
        Department: null,
        ProductName: null,
        ProductFlowSign: 0,
        ProductFlowId: null,
        ProductFlowName: null,
        StandardHoursType: 0,
        StandardHours: null,
        RelaxCoefficient: null,
        MinMachineCount: 0,
        MaxMachineCount: 0,
        DifficultyCoefficient: null,
        MouldId: null,
        MouldName: null,
        MouldCavityCount: 0,
        Remark: null,
        OpPerson: null,
        OpSign: null,
        OpDate: null,
        OpTime: null,
        Id_Key: null,
    }
    $scope.vm = uiVM;
    var vmManager = {
        editWindowDisplay: false,
        editWindowWidth: '100%',
        copyWindowDisplay: false,
        viewProductFlowDetails: function (item) {

        },


    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.add = function () {
        vmManager.editWindowDisplay = true;
    };
    operate.copyAll = function () {
        vmManager.copyWindowDisplay = true;
    };
    operate.copyok = function () {
        vmManager.copyWindowDisplay = false;
    };
    operate.copycancel = function () {
        vmManager.copyWindowDisplay = false;
    };
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };


    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        uiVM.Department = dto.DataNodeText;
    };
    $scope.promise = connDataOpService.getConfigDicData('Organization').then(function (data) {
        departmentTreeSet.setTreeDataset(data);
    });

    $scope.ztree = departmentTreeSet;
});
//日报录入
productModule.controller("dReportInputCtrl", function ($scope, dataDicConfigTreeSet, connDataOpService) {
    ///日报录入视图模型
    var uiVM = {
        Department: null,
        DailyReportDate: null,
        DailyReportMonth: null,
        InputTime: null,
        MachineId: null,
        EquipmentEifficiency: null,
        DifficultyCoefficient: null,
        MouldId: null,
        MouldName: null,
        MouldCavityCount: 0,
        OrderId: null,
        ProductName: null,
        ProductSpecification: null,
        ProductFlowSign: 0,
        ProductFlowID: null,
        ProductFlowName: null,
        StandardHoursType: 0,
        StandardHours: null,
        ClassType: null,
        UserWorkerId: null,
        UserName: null,
        MasterWorkerId: null,
        MasterName: null,
        InputStoreCount: null,
        Qty: null,
        QtyGood: null,
        QtyBad: null,
        FailureRate: null,
        SetHours: null,
        InputHours: null,
        ProductionHours: null,
        AttendanceHours: null,
        NonProductionHours: null,
        ReceiveHours: null,
        ManHours: null,
        ProductionEfficiency: null,
        OperationEfficiency: null,
        OpPerson: null,
        OpSign: null,
        OpDate: null,
        OpTime: null,
        Id_Key: null,
    }
    $scope.vm = uiVM;
    var vmManager = {
        dReportInputDisplay: false,
        dReportPreviewDisplay: false,
        proFlowBoardDisplay: false,
        personBoardDisplay: false,
        orderIdBoardDisplay: false,
        boardViewSize: '100%',
        inputViewSize: '40%',
        showDReportInputView: function () { vmManager.dReportInputDisplay = true; },
        showDReportPreviewView: function () { vmManager.dReportPreviewDisplay = true; },
        showProFlowView: function () { vmManager.proFlowBoardDisplay = true; },
        showPersonView: function () { vmManager.personBoardDisplay = true; },
        showOrderIdView: function () { vmManager.orderIdBoardDisplay = true; },
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        uiVM.Department = dto.DataNodeText;
    };
    $scope.promise = connDataOpService.getConfigDicData('Organization').then(function (data) {
        departmentTreeSet.setTreeDataset(data);
    });

    $scope.ztree = departmentTreeSet;
});