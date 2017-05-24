/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var officeAssistantModule = angular.module('bpm.sysmanageApp');
officeAssistantModule.factory('oAssistantDataOpService', function (ajaxService) {
    var oAssistant = {};
    var urlPrefix = '/' + leeHelper.controllers.TolOfficeAssistant + '/';

    ///存储项目开发记录
    oAssistant.storeProjectDevelopRecord = function (entity) {
        var url = urlPrefix + 'StoreProjectDevelopRecord';
        return ajaxService.postData(url, {
            entity: entity
        });
    };



    return oAssistant;
});
///名片夹控制器
officeAssistantModule.controller('collaborateContactLibCtrl', function ($scope, oAssistantDataOpService) {
});
///工作任务管理控制器
officeAssistantModule.controller('workTaskManageCtrl', function ($scope, oAssistantDataOpService) {
    var workTaskIdentity = {
       SystemName:null,
       ModuleName:null,
       WorkItem:null,
       WorkDescription:null,
       DifficultyCoefficient:null,
       WorkPriority:null,
       StartDate:null,
       EndDate:null,
       ProgressStatus:null,
       ProgressDescription:null,
       OrderPerson:null,
       CheckPerson:null,
       Remark:null,    
       OpSign:'add',
       Id_Key:null      
    };
    $scope.vm = workTaskIdentity;
    var originalVM = _.clone(workTaskIdentity);
    var vmManager = {
        activeTab: 'inputTab',
        isLocal: true,
        opSign: 'add',
        closeModuleName:[],
        systemNames: [
            { id: "人力资源管理", text: "人力资源管理", ModuleNameList: [{ id: "员工档案管理", text: "员工档案管理" }, { id: "考勤管理", text: "考勤管理" }, {id:"总务管理",text:"总务管理"}]},
            { id: "生产管理", text: "生产管理", ModuleNameList: [{id:"生产管理",text:"生产管理"}] },
            { id: "质量管理", text: "质量管理", ModuleNameList: [{ id: "检验管理", text: "检验管理" }, { id: "RMA管理", text: "RMA管理" }, {id:"8D报告管理",text:"8D报告管理"}]},
            { id: "采购管理", text: "采购管理", ModuleNameList: [{ id: "供应商证书管理", text: "供应商证书管理" }, { id: "供应商考核管理", text: "供应商考核管理" }, { id: "供应商辅导管理", text: "供应商辅导管理" }, { id: "供应商稽核评分", text: "供应商稽核评分" }] },
            { id: "设备管理", text: "设备管理", ModuleNameList: [{ id: "设备总览", text: "设备总览" }, { id: "设备校验", text: "设备校验" }, { id: "设备保养", text: "设备保养" }, { id: "设备维修", text: "设备维修"}] },
          
        ],
        selectSystemName: function () {
            var sysName = _.find(vmManager.ystemNames, { id: workTaskIdentity.SystemNames });
            if (!angular.isUndefined(sysName)) {
                vmManager.closeModuleName=sysName.ModuleNameList
            }
        }


    }
});