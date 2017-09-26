/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var officeAssistantModule = angular.module('bpm.toolsOnlineApp');
officeAssistantModule.factory('oAssistantDataOpService', function (ajaxService) {
    var oAssistant = {};
    var oaUrlPrefix = '/' + leeHelper.controllers.TolOfficeAssistant + '/';

    ///获取联系人数据
    oAssistant.getCollaborateContactDatas = function (department, searchMode, queryContent) {
        var url = oaUrlPrefix + 'GetCollaborateContactDatas';
        return ajaxService.getData(url, {
            department: department,
            searchMode: searchMode,
            queryContent: queryContent
        });
    };
    ///存储联系人数据
    oAssistant.storeCollaborateContactDatas = function (model) {
        var url = oaUrlPrefix + 'StoreCollaborateContactDatas';
        return ajaxService.postData(url, {
            model: model,
        });
    };

    ///获取工作任务数据
    oAssistant.getWorkTaskManageDatas = function (systemName, moduleName, progressStatus, mode) {
        var url = oaUrlPrefix + 'GetWorkTaskManageDatas';
        return ajaxService.getData(url, {
            systemName: systemName,
            moduleName: moduleName,
            progressstatus: progressStatus,
            mode: mode
        });
    };
    ///存储工作任务数据
    oAssistant.storeWorkTaskManageDatas = function (model) {
        var url = oaUrlPrefix + 'StoreWorkTaskManageDatas';
        return ajaxService.postData(url, {
            model: model
        });
    };

    ///上报问题登记Save
    oAssistant.storeReportImproveProblemDatas = function (model) {
        var url = oaUrlPrefix + 'StoreReportImproveProblemDatas';
        return ajaxService.postData(url, {
            model: model
        });
    };
    ///自动生成上报问题编号
    oAssistant.autoCreateCaseId = function () {
        var url = oaUrlPrefix + 'AutoCreateCaseId';
        return ajaxService.getData(url, {

        })
    }
    ///查询上报问题处理状态
    oAssistant.getReportImproveProbleDatas = function (problemSove, department, mode) {
        var url = oaUrlPrefix + 'GetReportImproveProbleDatas';
        return ajaxService.getData(url, {
            problemSolve: problemSove,
            department: department,
            mode: mode
        })
    }
    //上传附件
    oAssistant.uploadReportProblemFile = function (file) {
        var url = oaUrlPrefix + 'UploadReportProblemFile';
        return ajaxService.uploadFile(url, file);
    };

    return oAssistant;
});
///名片夹控制器
officeAssistantModule.controller('collaborateContactLibCtrl', function ($scope, oAssistantDataOpService) {
    ///联系人模型
    var uiVm = $scope.vm = {
        Department: null,
        ContactPerson: null,
        Sex: null,
        CustomerCategory: null,
        ContactCompany: null,
        WorkerPosition: null,
        ContactMemo: null,
        Telephone: null,
        OfficeTelephone: null,
        Fax: null,
        Mail: null,
        QqOrSkype: null,
        ContactAdress: null,
        WebsiteAdress: null,
        IsDelete: 0,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null,
    };
    var initVm = _.clone(uiVm);
    //删除联系人对话框
    var deleteDialog = $scope.deleteDialog = leePopups.dialog("删除提示", "删除后数据将不存在，你确定要删除吗？");
    //编辑对话框
    var dialog = $scope.dialog = leePopups.dialog();
    // 查询条件
    var qryVm = $scope.qryVm = {
        contactPerson: null,
        telephone: null
    };
    ///界面管理
    var vmManager = {
        activeTab: 'initTab',
        //初始化
        init: function () {
            uiVm = _.clone(initVm);
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVm;
        },
        editDatas: [],
        ///查询函数
        loadDatas: function (searchMode, queryContent) {
            //var userOrganization = leeHelper.getUserOrganization();
            var department = leeHelper.getUserOrganization().B;
            vmManager.editDatas = [];
            console.log(uiVm.Department);
            console.log(searchMode);
            console.log(queryContent);
            $scope.searchPromise = oAssistantDataOpService.getCollaborateContactDatas(uiVm.Department, searchMode, queryContent).then(function (datas) {
                if (angular.isArray(datas))
                    vmManager.editDatas = datas;
            });
        },
        ///按联系人查询
        getDatasByName: function () {
            vmManager.loadDatas(1, qryVm.contactPerson);
        },
        ///按电话查询
        getDatasByTelPhone: function () {
            vmManager.loadDatas(2, qryVm.telephone);
        }
    };
    $scope.vmManager = vmManager;
    ///
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    ///创建新的联系人
    operate.createNew = function () {
        $scope.vm = uiVm = _.clone(initVm);
        dialog.show();
    };
    ///编辑联系人
    operate.editItem = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVm = item;
        dialog.show();
    };
    ///选择删除项
    operate.deleteItem = function (item) {
        $scope.vm = uiVm = item;
        deleteDialog.show();
    };
    ///取消删除
    operate.cancelDelete = function (isValid) {
        deleteDialog.close();
    };
    ///确认删除
    operate.confirmDelete = function (isValid) {
        var deleteItem = _.clone(uiVm);
        var ds = _.clone(vmManager.editDatas);
        //leeHelper.remove(ds, deleteItem);
        //vmManager.editDatas = ds;
        uiVm.OpSign = leeDataHandler.dataOpMode.delete;
        //leeHelper.setUserData(uiVm);
        //uiVm.Department = leeHelper.getUserOrganization().B;
        oAssistantDataOpService.storeCollaborateContactDatas(uiVm).then(function (opresult) {
            leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                if (opresult.Result) {
                    leeHelper.remove(ds, deleteItem);
                    vmManager.editDatas = ds;
                    deleteDialog.close();
                }
            })
        })
    };
    ///保存联系人
    operate.saveAll = function (isValid) {
        //leeHelper.setUserData(uiVm);
        //uiVm.Department = leeHelper.getUserOrganization().B;
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            oAssistantDataOpService.storeCollaborateContactDatas(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var dataItem = _.clone(uiVm);
                        dataItem.Id_Key = opresult.Id_Key;
                        if (dataItem.OpSign === leeDataHandler.dataOpMode.add) {
                            vmManager.editDatas.push(dataItem);
                        }
                        vmManager.init();
                        dialog.close();
                    }
                })
            })
        })
    };
    ///刷新数据
    operate.refresh = function () { leeDataHandler.dataOperate.refresh(operate, function () { vmManager.init(); }); };
    ///初始载入本部门所有联系人
    leeHelper.setUserData(uiVm);
    uiVm.Department = leeHelper.getUserOrganization().B;
    vmManager.loadDatas(0, null);
});
///工作任务管理控制器
officeAssistantModule.controller('workTaskManageCtrl', function ($scope, oAssistantDataOpService, dataDicConfigTreeSet, connDataOpService) {
    //view model
    var uiVM = {
        Department: null,
        SystemName: null,
        ModuleName: null,
        WorkItem: null,
        WorkDescription: null,
        DifficultyCoefficient: null,
        WorkPriority: null,
        StartDate: null,
        EndDate: null,
        ProgressStatus: null,
        ProgressDescription: null,
        OrderPerson: null,
        CheckPerson: null,
        Remark: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: 0
    };
    $scope.vm = uiVM;
    var orginalVM = _.clone(uiVM);
    var dialog = $scope.dialog = leePopups.dialog();

    var queryFields = {
        systemName: null,
        moduleName: null,
        progressStatus: null
    };
    $scope.query = queryFields;

    var vmManager = {
        activeTab: 'initTab',
        init: function () {
            uiVM = _.clone(orginalVM);
            uiVM.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVM;
        },
        datasource: [],
        searchDataset: [],
        showmoduleNameList: [],
        querymoduleNameList: [],
        searchBy: function () {
            $scope.searchPromise = oAssistantDataOpService.getWorkTaskManageDatas(qryVm.department, qryVm.systemName, qryVm.moduleName, qryVm.progressStatus, 1).then(function (datas) {
                vmManager.storeDataset = datas;

            });
        },
        systemNames: [
            {
                id: "人力资源管理", text: "人力资源管理", moduleNameList: [
                    { id: "员工档案管理", text: "员工档案管理" },
                    { id: "考勤管理", text: "考勤管理" },
                    { id: "总务管理", text: "总务管理" }
                ]
            },
            {
                id: "生产管理", text: "生产管理", moduleNameList: [
                    { id: "人员管理", text: "人员管理" },
                    { id: "日报管理", text: "日报管理" },
                    { id: "排程管理", text: "排程管理" },
                    { id: "出货管理", text: "出货管理" },
                    { id: "看板管理", text: "看板管理" }
                ]
            },
            {
                id: "质量管理", text: "质量管理", moduleNameList: [
                    { id: "检验管理", text: "检验管理" },
                    { id: "RMA管理", text: "RMA管理" },
                    { id: "8D报告管理", text: "8D报告管理" }

                ]
            },
            {
                id: "采购管理", text: "采购管理", moduleNameList: [
                    { id: "供应商管理", text: "供应商管理" }
                ]
            },
            {
                id: "设备管理", text: "设备管理", moduleNameList: [
                    { id: "设备总览", text: "设备总览" },
                    { id: "设备校验", text: "设备校验" },
                    { id: "设备保养", text: "设备保养" },
                    { id: "设备维修", text: "设备维修" }
                ]
            },
            {
                id: "系统管理", text: "系统管理", moduleNameList: [
                    { id: "帐户管理", text: "帐户管理" },
                    { id: "配置管理", text: "配置管理" },
                    { id: "ITIL", text: "ITIL" }
                ]
            },
            {
                id: "在线工具", text: "在线工具", moduleNameList: [
                    { id: "办公助手", text: "办公助手" }
                ]
            }
        ],
        selectSystemName: function () {
            var systemName = _.find(vmManager.systemNames, {
                id: uiVM.SystemName
            });
            if (!angular.isUndefined(systemName)) {
                vmManager.showmoduleNameList = systemName.moduleNameList;
            }
        },
        querySystemName: function () {
            var systemName = _.find(vmManager.systemNames, {
                id: queryFields.systemName
            });
            if (!angular.isUndefined(systemName)) {
                vmManager.querymoduleNameList = systemName.moduleNameList;
            }
        },


        getTaskRecords: function (mode) {
            vmManager.searchDataset = [];
            vmManager.datasource = [];
            oAssistantDataOpService.getWorkTaskManageDatas(queryFields.systemName, queryFields.moduleName, queryFields.progressStatus, mode).then(function (datas) {
                vmManager.datasource = datas;
            })
        },
        orderpersons: [{ name: '万晓桥', text: '万晓桥' }, { name: '林旺雷', text: '林旺雷' }, { name: '杨垒', text: '杨垒' }],
        progressstatus: [{ id: "未完成", text: "未完成" }, { id: "己完成", text: "己完成" }],


    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.createNew = function () {
        $scope.vm = uiVM = _.clone(orginalVM);
        dialog.show();
    };
    //编辑
    operate.editItem = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVm = item;
        dialog.show();
    },
    //删除
        operate.deleteItem = function (item) {
            item.OpSign = leeDataHandler.dataOpMode.delete;
            $scope.vm = uiVm = item;
            deleteDialog.show();

        };
    //保存
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            oAssistantDataOpService.storeWorkTaskManageDatas(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var dataItem = _.clone(uiVm);
                        dataItem.Id_Key = opresult.Id_Key;
                        if (dataItem.OpSign === leeDataHandler.dataOpMode.add) {
                            vmManager.editDatas.push(dataItem);
                        }
                        vmManager.init();
                        dialog.close();
                    }
                })
            })
        })
    };
    operate.refresh = function () { leeDataHandler.dataOperate.refresh(operate, function () { vmManager.init(); }); };

    $scope.promise = connDataOpService.getConfigDicData('Organization').then(function (datas) {
        departmentTreeSet.setTreeDataset(datas);
    });
    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        queryFields.department = dto.DataNodeText;
    };
    $scope.ztree = departmentTreeSet;
});
officeAssistantModule.controller('reportImproveProblemCtrl', function ($scope, oAssistantDataOpService, dataDicConfigTreeSet, connDataOpService) {
    ///View
    var uiVM = {
        WorkerId: leeLoginUser.userId,
        Name: leeLoginUser.userName,
        Department: leeLoginUser.departmentText,
        CaseId: null,
        FilePath: null,//
        FileName: null,//
        CaseIdYear: new Date().getFullYear(),
        SystemName: null,
        ModuleName: null,
        ProblemDate: new Date(),
        ProblemDesc: null,
        ProblemSolveMethod: null,
        ProblemProcess: null,
        ProblemAttach: null,
        ProblemDegree: null,
        ProblemSolve: '待解决',
        OpDate: null,
        OpTime: null,
        OpPerson: leeLoginUser.userName,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: 0
    }
    $scope.vm = uiVM;
    var originalVM = _.clone(uiVM);
    var dialog = $scope.dialog = leePopups.dialog();
    var queryFields = {
        problemSolve: null,
        department: null

    };
    $scope.query = queryFields;
    var vmManager = $scope.vmManager = {
        currentInspectionItem: null,
        activeTab: 'initTab',
        isLocal: true,
        init: function () {
            uiVM = _.clone(originalVM);
            uiVM.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVM;
        },
        searchedWorkers: [],
        isSingle: true,//是否搜寻人员或部门
        isdisabled: false,
        datasource: [],
        searchDataset: [],
        getWorkerInfo: function () {
            if (uiVM.WorkerId === undefined) return;
            var strLen = leeHelper.checkIsChineseValue(uiVM.WorkerId) ? 2 : 6;
            if (uiVM.WorkerId.length >= strLen) {
                vmManager.searchedWorkers = [];
                $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(uiVM.WorkerId).then(function (datas) {
                    if (datas.length > 0) {
                        vmManager.searchedWorkers = datas;
                        if (vmManager.searchedWorkers.length === 1) {
                            vmManager.isSingle = true;
                            vmManager.selectWorker(vmManager.searchedWorkers[0]);
                        }
                        else {
                            vmManager.isSingle = false;
                        }
                    }
                    else {
                        vmManager.selectWorker(null);
                    }
                });
            }
        },
        selectWorker: function (worker) {
            if (worker !== null) {
                uiVM.Name = worker.Name;
                uiVM.WorkerId = worker.WorkerId;
                uiVM.Department = worker.Department;
            }
            else {
                uiVM.Department = null;
            }
        },
        systemNames: [
            {
                id: "人力资源管理", text: "人力资源管理", moduleNameList:
                [
                    { id: "员工档案管理", text: "员工档案管理" },
                    { id: "考勤管理", text: "考勤管理" },
                    { id: "总务管理", text: "总务管理" }
                ]
            },
            {
                id: "生产管理", text: "生产管理", moduleNameList:
                [
                    { id: "人员管理", text: "人员管理" },
                    { id: "日报管理", text: "日报管理" },
                    { id: "排程管理", text: "排程管理" },
                    { id: "出货管理", text: "出货管理" },
                    { id: "看板管理", text: "看板管理" }
                ]
            },
            {
                id: "质量管理", text: "质量管理", moduleNameList:
                [
                    { id: "检验管理", text: "检验管理" },
                    { id: "RMA管理", text: "RMA管理" },
                    { id: "8D报告管理", text: "8D报告管理" }

                ]
            },
            {
                id: "采购管理", text: "采购管理", moduleNameList:
                [
                    { id: "供应商管理", text: "供应商管理" }
                ]
            },
            {
                id: "设备管理", text: "设备管理", moduleNameList:
                [
                    { id: "设备总览", text: "设备总览" },
                    { id: "设备校验", text: "设备校验" },
                    { id: "设备保养", text: "设备保养" },
                    { id: "设备维修", text: "设备维修" }
                ]
            },
            {
                id: "系统管理", text: "系统管理", moduleNameList:
                [
                    { id: "帐户管理", text: "帐户管理" },
                    { id: "配置管理", text: "配置管理" },
                    { id: "ITIL", text: "ITIL" }
                ]
            },
            {
                id: "在线工具", text: "在线工具", moduleNameList:
                [
                    { id: "办公助手", text: "办公助手" }
                ]
            }
        ],
       
        selectSystemName: function () {
            var systemName = _.find(vmManager.systemNames, {
                id: uiVM.SystemName
            });
            if (!angular.isUndefined(systemName)) {
                vmManager.showmoduleNameList = systemName.moduleNameList;
            }
        },
        querySystemName: function () {
            var systemName = _.find(vmManager.systemNames, {
                id: queryFields.systemName
            });
            if (!angular.isUndefined(systemName)) {
                vmManager.querymoduleNameList = systemName.moduleNameList;
            }
        },
        problemDegrees: [
            { id: '一般', text: '一般' },
            { id: '紧急不重要', text: '紧急不重要' },
            { id: '重要不紧急', text: '重要不紧急' },
            { id: '重要紧急', text: '重要紧急' }
        ],
        problemSolves: [
            { value: '己解决', label: '<i class="fa fa-check-square-o"></i>  己解决' },
            { value: '待解决', label: '<i class="fa fa-bug"></i>  待解决' }

        ],
        problemProcesss: [{ name: '万晓桥', text: '万晓桥' }, { name: '林旺雷', text: '林旺雷' }, { name: '杨垒', text: '杨垒' }],
        //s查询
        searchBy: function () {
            $scope.searchPromise = oAssistantDataOpService.getReportImproveProbleDatas(queryFields.problemSolve, queryFields.department, 1).then(function (datas) {
                vmManager.storeDataset = datas;

            })
        },
        getReportImproveProblemData: function (mode) {

            vmManager.searchDataset = [];
            vmManager.datasource = [];
            oAssistantDataOpService.getReportImproveProbleDatas(queryFields.problemSolve, queryFields.department, mode).then(function (datas) {
            vmManager.datasource = datas;
            })
        },

        //自动生成编号
        autoCreateCaseid: function () {
            $scope.doPromise = oAssistantDataOpService.autoCreateCaseId().then(function (caseId) {
                uiVM.CaseId = caseId;
                uiVM.OpSign = leeDataHandler.dataOpMode.add;
            })
        },
    };

    var dialog = $scope.dialog = leePopups.dialog();
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.handleItem = function (item) {
        var dataitem = _.clone(item);
        dataitem.OpSign = leeDataHandler.dataOpMode.edit;
        leeHelper.copyVm(dataitem, uiVM);
        $scope.vm = uiVM;
        dialog.show();

    };
    //修改
    operate.editItem = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVM = item;
    };
    //保存
    operate.saveAll = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            oAssistantDataOpService.storeReportImproveProblemDatas(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var mode = _.clone(uiVM)
                        mode.Id_Key === opresult.Id_Key;
                        if (mode.OpSign === leeDataHandler.dataOpMode.add) {
                            vmManager.datasource.push(mode);
                        }
                        vmManager.init();
                        dialog.close();
                        vmManager.getReportImproveProblemData(1);
                    }
                })
            })
        })
    };
    //更新
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        })
    };

    // 上传附件
    $scope.selectFile = function (el) {
        leeHelper.upoadFile(el, function (fd) {
            var fileItem = uiVM;
            var fileAttachData = { CaseId: fileItem.CaseId };
            fd.append('fileAttachData', JSON.stringify(fileAttachData));
            oAssistantDataOpService.uploadReportProblemFile(fd).then(function (datas) {
                if (datas.Result === 'OK') {
                    uiVM.FileName = datas.FileName;
                    uiVM.FilePath = datas.FullFileName;
                    vmManager.isdisabled = true;
                    //alert("上传"+fd.name+"文件成功!");
                }
            })
        })
    },
    //组织架构
    $scope.promise = connDataOpService.getConfigDicData('Organization').then(function (datas) {
        departmentTreeSet.setTreeDataset(datas);
    });
    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        queryFields.department = dto.DataNodeText;
        vmManager.getReportImproveProblemData(2);
    };
    $scope.ztree = departmentTreeSet;
});


