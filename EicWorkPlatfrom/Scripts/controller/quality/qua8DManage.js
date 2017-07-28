/// <reference path="quaInspectionManage.js" />

/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');
//数据访问工厂
qualityModule.factory("BDataOpService", function (ajaxService) {
    var bugd = {};
    var quabugDManageUrl = "/qua8DManage/";
    ///获取RMA表单单头
    bugd.getRua8DReportDatas = function (reportId) {
        var url = quabugDManageUrl + 'GetRmaReportDatas';
        return ajaxService.getData(url, {
            reportId: reportId
        });
    };
    bugd.getRua8DReportStepData = function (reportId, stepId) {
        var url = quabugDManageUrl + 'GetRua8DReportStepData';
        return ajaxService.getData(url, {
            reportId: reportId,
            stepId: stepId,
        });
    };

    bugd.getQueryDatas = function (searchModel, orderId) {
        var url = quabugDManageUrl + 'GetQueryDatas';
        return ajaxService.getData(url, {
            orderId: orderId,
            searchModel: searchModel,
        });
    };
    return bugd;
});
////创建8D表单
qualityModule.controller('create8DFormCtrl', function ($scope, BDataOpService, qualityInspectionDataOpService) {
    ///视图模型
    var uiVm = $scope.vm = {
        ReportId: 'M201705',
        DiscoverPosition: null,
        AccountabilityDepartment: null,
        OrderId: null,
        MaterialName: null,
        MaterialSpec: null,
        InPutHouseOrder: null,
        MaterialCount: null,
        InspectNumber: 0,
        FailQty: 0,
        FailClass: null,
        CreateReportDate: null,
        Status: null,
        OpPerson: null,
        OpDate: null,
        OpSign: null,
        OpTime: null,
        Id_Key: null,
    };
    ///视图处理
    $scope.vm = uiVm;
    //初始化原型
    var initVM = _.clone(uiVm);
    var dialog = $scope.dialog = leePopups.dialog();
    var vmManager = {
        orderInfo: [],
        dataSets: [],
        dataSource: [],
        iqcOrderId: '341-170327011',
        ///查询表单
        getQua8DCreateDatas: function () {
            $scope.searchPromise = BDataOpService.getQueryDatas("21", vmManager.iqcOrderId).then(function (datas) {
                vmManager.dataSets = datas;
                vmManager.dataSource = datas;
            });
        },
        ///创建8D表单
        create8DReportMaster: function (item) {
            console.log(item);
            dialog.show();
        },
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

});
////处理8D表单
qualityModule.controller('Handle8DFormCtrl', function ($scope, BDataOpService) {
    ///视图模型
    ///
    var uiVm = $scope.vm = {
        ReportId: null,
        StepId: 0,
        StepDescription: null,
        DescribeType: null,
        DescribeContent: null,
        FilePath: null,
        FileName: null,
        AboutDepartment: null,
        SignaturePeoples: null,
        ParameterKey: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: null,
        Id_Key: null,
    }
    var uiInitBaseInfoVm = $scope.baseInfoVm = {
        ReportId: null,
        DiscoverPosition: null,
        AccountabilityDepartment: null,
        OrderId: null,
        MaterialName: null,
        MaterialSpec: null,
        InPutHouseOrder: null,
        MaterialCount: null,
        InspectNumber: 0,
        FailQty: 0,
        FailClass: null,
    }
    var vmManager = {
        stepDisplay: true,
        // isCheck   selectStep  StepDescription
        steps: [],
        selecteStepdata: [],
        //对应界面显示的数据集
        viewDataset: [],
        selectStep: function (step) {
            console.log(step);
            var stepItem = _.findWhere(vmManager.viewDataset, { StepId: step.StepId });
            if (_.isUndefined(stepItem)) {
                console.log(stepItem);
                stepItem = {
                    StepId: step.StepId,
                    stepData: step.HandelQua8DStepDatas,
                };
                leeHelper.setObjectGuid(stepItem);
                vmManager.viewDataset.push(stepItem);
                console.log(stepItem);
            }
            else {
                stepItem.dataset = [];
                if (!stepItem.isCheck)
                    leeHelper.delWithId(vmManager.viewDataset, stepItem);
            }
            if (step.isCheck) step.isCheck = false;
            else step.isCheck = true;

            //$scope.doPromise = BDataOpService.getRua8DReportStepData("M1707004-2", 1).then(function (datas) {
            //    vmManager.selecteStepdata = datas;
            //    console.log(vmManager.selecteStepdata);
            //});

            //$scope.promise = accountService.findRoleMatchModulesBy(role.RoleId).then(function (datas) {
            //   angular.forEach(datas, function (item) {
            //     var mroleItem = _.clone(uiVm);
            //     leeHelper.copyVm(item, mroleItem);
            //     leeHelper.setObjectGuid(mroleItem);
            //     leeHelper.setObjectServerSign(mroleItem);
            //     mroleItem.OpSign = leeDataHandler.dataOpMode.none;
            //     mroleItem.RoleName = role.RoleName;
            //     leeHelper.insertWithId(roleItem.dataset, mroleItem);
            //       vmManager.addToDbDataset(mroleItem, role.isCheck);
            //   });
            //   vmManager.checkTreeNode(true, roleItem);
            //    vmManager.viewDataset.activePanel = vmManager.viewDataset.length - 1;
            //})
        },
        getQua8DCreateDatas: function () {
            $scope.doPromise = BDataOpService.getRua8DReportDatas(uiVm.ReportId).then(function (datas) {
                vmManager.steps = datas;
                console.log(datas);
            });
        },

    };
    $scope.vmManager = vmManager;
    vmManager.getQua8DCreateDatas();
});

////8D结案处理表单
qualityModule.controller('Colse8DFormCtrl', function ($scope, BDataOpService) {
    ///视图模型

});