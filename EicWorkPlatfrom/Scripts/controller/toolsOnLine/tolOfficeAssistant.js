/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var officeAssistantModule = angular.module('bpm.toolsOnlineApp');
officeAssistantModule.factory('oAssistantDataOpService', function (ajaxService) {
    var oAssistant = {};
    var oaUrlPrefix = '/' + leeHelper.controllers.TolOfficeAssistant + '/';

    ///获取联系人数据
    oAssistant.getCollaborateContactDatas = function (department, searchMode, contactPerson, telPhone) {
        var url = oaUrlPrefix + 'GetCollaborateContactDatas';
        return ajaxService.getData(url, {
            department: department,
            searchMode: searchMode,
            contactPerson: contactPerson,
            telPhone: telPhone
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
    oAssistant.getWorkTaskManageDatas = function (department, systemName, moduleName,mode) {
        var url = oaUrlPrefix + 'GetWorkTaskManageDatas';
        return ajaxService.getData(url, {
            department: department,
            systemName: systemName,
            moduleName: moduleName,
            mode:mode
        });
    };
    ///存储工作任务数据
    oAssistant.storeWorkTaskManageDatas = function (model) {
        var url = oaUrlPrefix + 'StoreWorkTaskManageDatas';
        return ajaxService.postData(url, {
            model: model
        });
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

    var deleteDialog = $scope.deleteDialog = leePopups.dialog("删除提示", "删除后数据将不存在，你确定要删除吗？");
    var dialog = $scope.dialog = leePopups.dialog();

    var qryVm = $scope.qryVm = {
        contactPerson: null,
        telephone: null
    };
    var vmManager = {
        activeTab: 'initTab',
        init: function () {
            uiVm = _.clone(initVm);
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVm;
        },
        editDatas: [],
        ///查询函数
        loadDatas: function (department, searchMode, contactPerson, telPhone) {
            vmManager.editDatas = [];
            $scope.searchPromise = oAssistantDataOpService.getCollaborateContactDatas(department, searchMode, contactPerson, telPhone).then(function (datas) {
                if (angular.isArray(datas))
                    vmManager.editDatas = datas;
            });
        },
        ///按联系人查询
        getDatasByName: function () {
            vmManager.loadDatas(uiVm.Department, 1, qryVm.contactPerson, null);
        },
        ///按电话查询
        getDatasByTelPhone: function () {
            vmManager.loadDatas(uiVm.Department, 2, null, qryVm.telephone);
        }
    };
    $scope.vmManager = vmManager;

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
        leeHelper.setUserData(uiVm);
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
        leeHelper.setUserData(uiVm);
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
    ///载入登录人信息
    leeHelper.setUserData(uiVm);
    ///初始载入本部门所有联系人
    vmManager.loadDatas(uiVm.Department, 0, null, null);
});
///工作任务管理控制器
officeAssistantModule.controller('workTaskManageCtrl', function ($scope, oAssistantDataOpService) {
    ///工作任务管理模型
    var uiVm = $scope.vm = {
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
        Id_Key: null
    };
    $scope.vm = uiVM;
    var initVm = _.clone(uiVm);
    var dialog = $scope.dialog = Object.create(leeDialog);
    ///定义查询字段
    var qryVm = {
        department: null,
        systemName: null,
        moduleName: null
    };
    $scope.query = qryVm;

    var vmManager = {
        activeTab:'initTab',
        init: function () {
            uiVm = _.clone(initVm);
            leeHelper.setUserData(uiVm)
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVm;
        },
        dataSource: [],
        editDatas: [],
        searchDataset: [],
        storeDataset: [],
        //载入信息
        //loadDatas: function (department, searchMode, queryContent) {
        //    vmManager.editDatas = [];
        //    //获取工作内容数据
        //    $scope.searchPromise = oAssistantDataOpService.getWorkTaskManageDatas(department, searchMode, queryContent).then(function (datas) {
        //        if (angular.isArray(datas))
        //            vmManager.editDatas = datas;
        //        vmManager.dataSource = datas;
        //    });
        //},
        searchBy: function () {
            $scope.searchPromise = oAssistantDataOpService.getWorkTaskManageDatas(qryVm.department, qryVm.systemName, qryVm.moduleName, 1).then(function (datas) {
                vmManager.storeDataset = datas;
                
            });
        },

        //按分类查询
        getDatasByName: function (mode) {
            oAssistantDataOpService.getWorkTaskManageDatas(qryVm.department, qryVm.systemName, qryVm.moduleName, mode).then(function (datas) {
                vmManager.searchDataset = datas;
            });
        },
    };
    //新增
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.createNew = function () {
        $scope.vm = uiVm = _.clone(initVm);
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

 
});