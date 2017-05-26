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
            model: model
        });
    };


    return oAssistant;
});
///名片夹控制器
officeAssistantModule.controller('collaborateContactLibCtrl', function ($scope, oAssistantDataOpService, $modal) {
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
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null
    };
    var initVm = _.clone(uiVm);

    var deleteDialog = $scope.deleteDialog = Object.create(leeDialog);
    var dialog = $scope.dialog = Object.create(leeDialog);
    var qryVm = $scope.qryVm = {
        contactPerson: null,
        telephone: null
    };
    var vmManager = {
        activeTab: 'initTab',
        init: function () {
            uiVm = _.clone(initVM);
            leeHelper.setUserData(uiVm);
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVm;
        },
        editDatas: [],
        dataSource:[],
        loadDatas: function (department, searchMode, queryContent) {
            vmManager.editDatas = [];
            $scope.searchPromise = oAssistantDataOpService.getCollaborateContactDatas(department, searchMode, queryContent).then(function (datas) {
                if (angular.isArray(datas))
                    vmManager.editDatas = datas;
                vmManager.dataSource = datas;
            });
        },
        getDatasByName: function () {
            leeHelper.setUserData(uiVm);
            vmManager.loadDatas(uiVm.Department, 1, qryVm.contactPerson);
        },
        getDatasByTelPhone: function () {
            leeHelper.setUserData(uiVm);
            vmManager.loadDatas(uiVm.Department, 2, qryVm.telephone);
        }
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.createNew = function () {
        $scope.vm = uiVm = _.clone(initVm);
        dialog.show();
    };
    operate.editItem = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVm = item;
        dialog.show();
    },
    operate.deleteItem = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.delete;
        $scope.vm = uiVm = item;
        deleteDialog.show();
    },
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
                });
            });
        });
    };
    operate.refresh = function () { leeDataHandler.dataOperate.refresh(operate, function () { vmManager.init(); }); };
   
    vmManager.loadDatas(uiVm.Department, 0, null);
});
///工作任务管理控制器
officeAssistantModule.controller('workTaskManageCtrl', function ($scope, oAssistantDataOpService) {

});