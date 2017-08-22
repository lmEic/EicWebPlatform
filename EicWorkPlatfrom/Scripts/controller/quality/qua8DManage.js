/// <reference path="quaInspectionManage.js" />

/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');

//数据访问工厂
qualityModule.factory("BDataOpService", function (ajaxService) {
    var bugd = {};

    var quabugDManageUrl = "/qua8DManage/";
    /////////////////////////////////////////////////////////////////
    ///展示8D详细数据
    bugd.showQua8DDetailDatas = function (reportId) {
        var url = quabugDManageUrl + 'ShowQua8DDetailDatas';
        return ajaxService.getData(url, {
            reportId: reportId
        });
    };
    /// 通过 单号和歩骤号得到信息
    bugd.getRua8DReportStepData = function (reportId, step) {
        var url = quabugDManageUrl + 'GetRua8DReportStepData';
        return ajaxService.postData(url, {
            reportId: reportId,
            step: step,
        });
    };
    /// 查找不良单号信息 
    bugd.getQueryDatas = function (searchModel, orderId) {
        var url = quabugDManageUrl + 'GetQueryDatas';
        return ajaxService.getData(url, {
            orderId: orderId,
            searchModel: searchModel,
        });
    };
    /// 存储8D初始表格
    bugd.storeCraet8DInitialData = function (initialData) {
        var url = quabugDManageUrl + 'StoreCraet8DInitialData';
        return ajaxService.postData(url, {
            initialData: initialData,
        });
    };
    ///自动生成8D单号 autoCreateReportId
    bugd.autoBuildingReportId = function (discoverPosition) {
        var url = quabugDManageUrl + 'AutoBuildingReportId';
        return ajaxService.getData(url, {
            discoverPosition: discoverPosition,
        });
    };
    ///上传文件
    bugd.upload8DHandleFile = function (file) {
        var url = quabugDManageUrl + 'Upload8DHandleFile';
        return ajaxService.uploadFile(url, file
        );
    };
    ///储存处理8D数据
    bugd.saveQua8DHandleDatas = function (handelData) {
        var url = quabugDManageUrl + 'SaveQua8DHandleDatas';
        return ajaxService.postData(url, {
            handelData: handelData,
        });

    }
    return bugd;
});

////创建8D表单
qualityModule.controller('create8DFormCtrl', function ($scope, BDataOpService, dataDicConfigTreeSet, qualityInspectionDataOpService) {
    ///视图模型
    var uiVm = $scope.vm = {
        ReportId: 'M201705',
        DiscoverPosition: null,
        AccountabilityDepartment: "品保部",
        OrderId: null,
        MaterialName: null,
        MaterialSpec: null,
        MaterialCount: 0,
        MaterialCountUnit: "个",
        InspectCount: 0,
        InspectCountUnit: "个",
        FailQty: 0,
        FailQtyUnit: "个",
        FailClass: null,
        FailRatio: 100,
        CreateReportDate: new Date(),
        Status: null,
        OpPerson: null,
        OpDate: null,
        OpSign: leeDataHandler.dataOpMode.add,
        OpTime: null,
        Id_Key: null,
    };

    ///处理初化视图模型
    var uiHandelVm = $scope.handelvm = {
        ReportId: null,
        StepId: 1,
        StepTitle: "异常说明",
        StepDescription: null,
        StepHandleContent: null,
        FilePath: null,
        FileName: null,
        HandleDepartment: "品保部",
        SignaturePeoples: "品保部",
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null,
    }

    //初始化原型
    var initVM = _.clone(uiVm);
    var initHandelVM = _.clone(uiHandelVm);
    var dialog = $scope.dialog = leePopups.dialog();
    var vmManager = {
        isdisabled: true,
        init: function () {
            uiVm = _.clone(initVM);
            uiHandelVm = _.clone(initHandelVM);
            $scope.handelvm = uiHandelVm;
            $scope.vm = uiVm;
        },
        IsShowMasterData: true,
        orderInfo: [],
        dataSets: [],
        dataSource: [],
        iqcOrderId: '341-17032701',
        fqcOrderId: '341-17032701F',
        fpqcOrderId: '341-17032701Q',
        unitDatas: [{ id: "Kg", text: "Kg" }, { id: "包", text: "包" }, { id: "个", text: "个" }],
        DiscoverPositions: [{ id: "客户抱怨", text: "客户抱怨" }, { id: "内部制造", text: "内部制造" }, { id: "供应商", text: "供应商" }, { id: "客诉", text: "客诉" }],
        failClassDatas: [{ id: "品质不合格", text: "品质不合格" }, { id: "HSF不合格", text: "HSF不合格" }],
        ///IQC查询表单
        getIqcQua8DCreateDatas: function () {
            query8DcreatData(1, vmManager.iqcOrderId)
        },
        ///FQC查询表单
        getFqcQua8DCreateDatas: function () {
            query8DcreatData(2, vmManager.fqcOrderId)
        },
        ///fPQC查询表单
        getfpqcQua8DCreateDatas: function () {
            query8DcreatData(3, vmManager.fpqcOrderId)
        },
        ///创建8D表单
        create8DReportMaster: function (item) {
            leeHelper.copyVm(item, uiVm);
            uiVm.DiscoverPosition = "内部制造";
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            uiVm.Id_Key = null;
            vmManager.autoCreateReportId();
            dialog.show();
        },
        ///显示隐藏头文件
        showMasterData: function () {
            if (vmManager.IsShowMasterData) vmManager.IsShowMasterData = false;
            else vmManager.IsShowMasterData = true;
        },
        ///自动生成编号
        autoCreateReportId: function () {
            $scope.searchPromise = BDataOpService.autoBuildingReportId(uiVm.DiscoverPosition).then(function (data) {
                console.log(data);
                uiVm.ReportId = data;
            });
        }
    };
    ///搜寻
    var query8DcreatData = function (secchModel, orderid) {
        $scope.searchPromise = BDataOpService.getQueryDatas(secchModel, orderid).then(function (datas) {
            vmManager.dataSets = vmManager.dataSource = [];
            vmManager.dataSource = vmManager.dataSets = datas;
        });
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //保存
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
        leeDataHandler.dataOperate.add(operate, true, function () {
            $scope.doPromise = BDataOpService.storeCraet8DInitialData(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        save8dHandelDatas(isValid);
                    }
                });
            });
        });
    }
    ///保存处理说明数据
    var save8dHandelDatas = function (isvlid) {
        leeHelper.setUserData(uiHandelVm);
        uiHandelVm.ReportId = uiVm.ReportId;
        console.log(uiHandelVm);
        $scope.doPromise = BDataOpService.saveQua8DHandleDatas(uiHandelVm).then(function (opResult) {
            leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                if (opresult.Result) {
                    vmManager.init();
                    dialog.close();
                }
            });
        });

    }
    //取消
    operate.Cancel = function () {
        vmManager.init();
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
    }
    ///上传表单附件模型
    var attachFileVM = {
        ModuleName: null,
        FormId: null,
        FileName: null,
        DocumentFilePath: null,
        OpSign: leeDataHandler.dataOpMode.uploadFile,
        OpPerson: null,
    }
    //上传文件
    $scope.selectFile = function (el) {
        console.log(el);
        leeHelper.upoadFile(el, function (fd) {
            var dto = leeWorkFlow.createFormFileAttachDto(attachFileVM, uiVm.ReportId, "Handle8DFormCtrl");
            fd.append("attachFileDto", JSON.stringify(dto));
            $scope.doPromise = BDataOpService.upload8DHandleFile(fd).then(function (uploadResult) {
                console.log(uploadResult);
                if (uploadResult.Result) {
                    $scope.uploadFileName = uiHandelVm.FileName = uploadResult.FileName;
                    uiHandelVm.FilePath = uploadResult.FilePath;
                }
            });
        });
    };

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

});

////处理8D表单
qualityModule.controller('Handle8DFormCtrl', function ($scope, dataDicConfigTreeSet, connDataOpService, BDataOpService) {

    // var ue = leeUeditor.getEditor('stepHandleContent');
    ///视图模型
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
    //初始化原型
    var initVM = _.clone(uiVm);
    //
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
    ///表单附件模型
    var attachFileVM = {
        ModuleName: null,
        FormId: null,
        FileName: null,
        DocumentFilePath: null,
        OpSign: leeDataHandler.dataOpMode.uploadFile,
        OpPerson: null,
    }

    var vmManager = {
        stepDisplay: true,
        isShowMasterData: true,
        reportMasterInfo: [],
        init: function () {
            uiVm = _.clone(initVM);
            $scope.vm = uiVm;
        },
        //对应界面显示的数据集
        viewDataset: [],
        selectStepItemData: [],
        query8DStepDatas: function () {
            $scope.doPromise = BDataOpService.showQua8DDetailDatas(uiVm.ReportId).then(function (datas) {
                vmManager.steps = datas.Stepdatas;
                vmManager.reportMasterInfo = datas.ShowQua8DMasterData;
                vmManager.viewDataset = [];
            });
        },
        //选择步骤
        selectStep: function (step) {
            vmManager.selectStepItemData = step;
            var stepItem = _.findWhere(vmManager.viewDataset, { stepId: step.StepId });
            if (_.isUndefined(stepItem)) {
                stepItem = {
                    stepId: step.StepId,
                    isdisabled: false,
                    stepName: step.StepName,
                    stepTitle: step.StepTitle,
                    stepLevel: step.StepLevel,
                    isSaveSucceed: false,
                    VmDatas: [],
                };
                BDataOpService.getRua8DReportStepData(uiVm.ReportId, step).then(function (data) {
                    stepItem.VmDatas = data;
                    isSaveSucceed = false;
                });
                leeHelper.setObjectGuid(stepItem);
                vmManager.viewDataset.push(stepItem);

            }
            else {
                if (!step.IsCheck)
                    leeHelper.delWithId(vmManager.viewDataset, stepItem);
            }
            if (step.IsCheck) step.IsCheck = false;
            else step.IsCheck = true;
            vmManager.init();
            leeHelper.copyVm(stepItem.VmDatas, uiVm);
            vmManager.viewDataset.activePanel = vmManager.viewDataset.length - 1;
        },
        ///显示主表信息数据 isShowMasterData  showMasterData
        showMasterData: function () {
            if (vmManager.isShowMasterData) vmManager.isShowMasterData = false;
            else vmManager.isShowMasterData = true;
        }
    };
    $scope.vmManager = vmManager;
    //上传文件
    $scope.selectFile = function (el) {
        leeHelper.upoadFile(el, function (fd) {
            var dto = leeWorkFlow.createFormFileAttachDto(attachFileVM, uiVm.ReportId, "Handle8DFormCtrl");
            fd.append("attachFileDto", JSON.stringify(dto));
            fd.append("step", vmManager.selectStepItemData.StepId);
            $scope.doPromise = BDataOpService.upload8DHandleFile(fd).then(function (uploadResult) {
                console.log(uploadResult);
                if (uploadResult.Result) {
                    angular.forEach(vmManager.viewDataset, function (e) {
                        if (e.stepId == vmManager.selectStepItemData.StepId) {
                            e.VmDatas.FileName = uploadResult.FileName;
                            e.isdisabled = true;
                        }
                    });
                }
            });
        });
    };
    //存储处理数据
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveDatas = function (step) {
        var stepItem = _.findWhere(vmManager.viewDataset, { stepId: step.stepId });
        console.log(stepItem);
        leeHelper.copyVm(stepItem.VmDatas, uiVm);
        leeHelper.setUserData(uiVm);
        console.log(uiVm);
        leeDataHandler.dataOperate.add(operate, true, function () {
            $scope.doPromise = BDataOpService.saveQua8DHandleDatas(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        vmManager.init();
                        angular.forEach(vmManager.viewDataset, function (e) {
                            if (e.stepId == step.stepId) {
                                e.isSaveSucceed = true;
                            }
                        });
                    }
                });
            });
        });
    }

    //vmManager.query8DStepDatas();
});

////8D结案处理表单
qualityModule.controller('Colse8DFormCtrl', function ($scope, BDataOpService) {
    ///视图模型

});