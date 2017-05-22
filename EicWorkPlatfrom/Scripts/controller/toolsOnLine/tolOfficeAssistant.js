/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var officeAssistantModule = angular.module('bpm.toolsOnlineApp');
officeAssistantModule.factory('oAssistantDataOpService', function (ajaxService) {
    var oAssistant = {};
    var oaUrlPrefix = '/' + leeHelper.controllers.TolOfficeAssistant + '/';

    ///联系人数据
    oAssistant.getCollaborateContactDatas = function () {
        var url = oaUrlPrefix + 'GetCollaborateContactDatas';
        return ajaxService.getData(url, {
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
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null,
    };
    var initVm = _.clone(uiVm);

    var dialog = $scope.dialog = Object.create(leeDialog);

    var qryManager = $scope.qryManager = {
        contactPerson: null,
        telephone: null,
    };


    var vmManager = {
        activeTab: 'initTab',
        editDatas: [],
        loadDatas: function () {
            vmManager.editDatas = [];
            $scope.searchPromise = oAssistantDataOpService.getCollaborateContactDatas().then(function (datas) {
                if (angular.isArray(datas))
                    vmManager.editDatas = datas;
            });
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
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };

    vmManager.loadDatas();
});
///工作任务管理控制器
officeAssistantModule.controller('workTaskManageCtrl', function ($scope, oAssistantDataOpService) {

});