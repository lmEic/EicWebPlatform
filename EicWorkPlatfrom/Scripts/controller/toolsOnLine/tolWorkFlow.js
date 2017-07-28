/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var officeAssistantModule = angular.module('bpm.toolsOnlineApp');
officeAssistantModule.factory('wfDataOpService', function (ajaxService) {
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

    return oAssistant;
});
///内部联络单
officeAssistantModule.controller('wfInternalContactFormCtrl', function ($scope, dataDicConfigTreeSet, connDataOpService, wfDataOpService) {
    var ue = leeUeditor.getEditor('formContent');
      

    ///联络单视图模型
    var uiVM = {
        OrderId: null,
        FormSubject: null,
        FormContent: null,
        ApplyDate: null,
        ApplyDepartment: null,
        ApplyPerson: null,
        RelatedToPerson: null,
        RelatedToDepartment: null,
        Approver: null,
        Field1: null,
        Field2: null,
        Field3: null,
        OpDate: null,
        OpTime: null,
        OpPerson: null,
        OpSign: null,
        Id_Key: null,
    }
    $scope.vm = uiVM;

    var dialog = $scope.dialog = leePopups.dialog();

    var vmManager = $scope.vmManager = {
        activeTab: 'initTab',
        //选择相关人员及部门
        selectDepartmentAndPerson: function (mode) {
            dialog.show();
        },

        datasets: [],
    };



    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };


    $scope.promise = connDataOpService.getConfigDicData('Organization').then(function (datas) {
        departmentTreeSet.setTreeDataset(datas);
    });
    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        //var dto = _.clone(departmentTreeSet.treeNode.vm);
        //queryFields.department = dto.DataNodeText;
        //vmManager.getEmailRecords(4);
    };
    $scope.ztree = departmentTreeSet;


    $scope.show = function () {
        alert(ue.getContent());
    };
});