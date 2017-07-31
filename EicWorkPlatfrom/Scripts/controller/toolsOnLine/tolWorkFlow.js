/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var officeAssistantModule = angular.module('bpm.toolsOnlineApp');
officeAssistantModule.factory('wfDataOpService', function (ajaxService) {
    var wfDataOp = {};
    var wfUrlPrefix = '/' + leeHelper.controllers.TolWorkFlow + '/';
    //获取人员邮箱信息
    wfDataOp.getWorkerMails = function (department) {
        var url = wfUrlPrefix + 'GetWorkerMails';
        return ajaxService.getData(url, {
            department: department,
        });
    };

    return wfDataOp;
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

        dataset: [],
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
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        var department = dto.DataNodeText;
        vmManager.dataset = [];
        $scope.searchPromise = wfDataOpService.getWorkerMails(department).then(function (datas) {
            vmManager.dataset = datas;
        });
    };
    $scope.ztree = departmentTreeSet;


    $scope.show = function () {
        alert(ue.getContent());
    };
});