/// <reference path="quaInspectionManage.js" />

/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');

//数据访问工厂
qualityModule.factory("BDataOpService", function (ajaxService) {
    var bugd = {};
    var quabugDManageUrl = "/qua8DManage/";
    ///展示8D详细数据
    bugd.showQua8DDetailDatas = function (reportId) {
        var url = quabugDManageUrl + 'ShowQua8DDetailDatas';
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
    bugd.storeCraet8DInitialData = function (initialData) {
        var url = quabugDManageUrl + 'StoreCraet8DInitialData';
        return ajaxService.postData(url, {
            initialData: initialData,
        });
    };
    return bugd;
});

////创建8D表单
qualityModule.controller('create8DFormCtrl', function ($scope, BDataOpService, dataDicConfigTreeSet, qualityInspectionDataOpService) {
    ///视图模型
    var uiVm = $scope.vm = {
        ReportId: 'M201705',
        DiscoverPosition: null,
        AccountabilityDepartment: null,
        OrderId: null,
        MaterialName: null,
        MaterialSpec: null,
        MaterialCount: 0,
        MaterialCountUnit: null,
        InspectCount: 0,
        InspectCountUnit: null,
        FailQty: 0,
        FailQtyUnit: null,
        FailClass: null,
        FailRatio: 100,
        CreateReportDate: new Date(),
        Status: null,
        OpPerson: null,
        OpDate: null,
        OpSign: 'add',
        OpTime: null,
        Id_Key: null,
    };
    //初始化原型
    var initVM = _.clone(uiVm);

    var dialog = $scope.dialog = leePopups.dialog();
    var vmManager = {
        init: function () {
            uiVm = _.clone(initVM);
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVm;
        },
        orderInfo: [],
        dataSets: [],
        dataSource: [],
        iqcOrderId: '341-170327011',
        unitDatas: [{ id: "Kg", text: "Kg" }, { id: "包", text: "包" }, { id: "个", text: "个" }],
        DiscoverPositions: [{ id: "客户抱怨", text: "客户抱怨" }, { id: "内部制造", text: "内部制造" }, { id: "供应商", text: "供应商" }],
        failClassDatas: [{ id: "品质不合格", text: "品质不合格" }, { id: "HSF不合格", text: "HSF不合格" }],
        ///查询表单
        getQua8DCreateDatas: function () {
            $scope.searchPromise = BDataOpService.getQueryDatas("21", vmManager.iqcOrderId).then(function (datas) {
                vmManager.dataSets = datas;
                vmManager.dataSource = datas;
                console.log(datas);
            });
        },
        ///创建8D表单
        create8DReportMaster: function (item) {
            leeHelper.copyVm(item, uiVm);
            uiVm.DiscoverPosition = "内部制造";
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            uiVm.Id_Key = null;
            dialog.show();
        },
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //保存
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
        console.log(uiVm);
        leeDataHandler.dataOperate.add(operate, true, function () {
            $scope.doPromise = BDataOpService.storeCraet8DInitialData(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        vmManager.init();
                        dialog.close();
                    }
                });
            });
        });
    }
    //取消
    operate.Cancel = function () {
        dialog.close();
    }

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        console.log(dto);
        var getString = dto.DataNodeText;
        if (vmManager.AccountabilityDepartment != null || vmManager.AccountabilityDepartment != "")
        { vmManager.AccountabilityDepartment += "," + getString; }
        else { vmManager.AccountabilityDepartment = getString; }
    };
});

////处理8D表单
qualityModule.controller('Handle8DFormCtrl', function ($scope, BDataOpService) {
    ///视图模型
    ///
    var uiVm = $scope.vm = {
        ReportId: 'M1707001',
        StepId: 0,
        StepTitle: null,
        StepDescription: null,
        StepHandleContent: null,
        FilePath: null,
        FileName: null,
        HandleDepartment: null,
        SignaturePeoples: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: leeDataHandler.dataOpMode.add,
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
        IsShowMasterData: true,
        reportMasterInfo: [],
        // isCheck   selectStep  StepDescription
        steps: [],
        selecteStepdata: [],
        //对应界面显示的数据集
        viewDataset: [],
        selectStep: function (step) {
            var stepItem = _.findWhere(vmManager.viewDataset, { StepId: step.StepId });
            if (_.isUndefined(stepItem)) {
                console.log(stepItem);
                stepItem = {
                    StepId: step.StepId,
                    StepLevel: step.StepLevel,
                    VmDatas: step.HandelQua8DStepDatas,
                };
                leeHelper.setObjectGuid(stepItem);
                vmManager.viewDataset.push(stepItem);
            }
            else {
                if (!step.IsCheck)
                    leeHelper.delWithId(vmManager.viewDataset, stepItem);
            }

            if (step.IsCheck) step.IsCheck = false;
            else step.IsCheck = true;

            //$scope.doPromise = BDataOpService.getRua8DReportStepData(uiVm.ReportId, 1).then(function (datas) {
            //    stepItem.dataset = datas;
            //});
            vmManager.viewDataset.activePanel = vmManager.viewDataset.length - 1;
            leeHelper.copyVm(stepItem.VmDatas, uiVm);
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
        query8DStepDatas: function () {
            $scope.doPromise = BDataOpService.showQua8DDetailDatas(uiVm.ReportId).then(function (datas) {

                angular.forEach(datas, function (d) {
                    if (d != null)
                    { isContainCustomerShortName = true; }
                });

                vmManager.steps = datas.Stepdatas;
                vmManager.reportMasterInfo = datas.ShowQua8DMasterData;
                console.log(vmManager.reportMasterInfo);
            });
        },
        showMasterData: function () {
            if (vmManager.IsShowMasterData) vmManager.IsShowMasterData = false;
            else vmManager.IsShowMasterData = true;
        }
    };
    $scope.vmManager = vmManager;
    //vmManager.query8DStepDatas();
});

////8D结案处理表单
qualityModule.controller('Colse8DFormCtrl', function ($scope, BDataOpService) {
    ///视图模型

});