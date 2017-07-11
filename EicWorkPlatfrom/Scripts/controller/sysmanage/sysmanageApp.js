/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.sysmanageApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'pageslide-directive', 'angular-popups'])

.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {
    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);

    var configUrlPrefix = leeHelper.controllers.configManage + "/";

    var itilUrlPrefix = leeHelper.controllers.itilManage + "/";

    //--------------配置管理--------------------------
    $stateProvider.state('hrDepartmentSet', {
        templateUrl: configUrlPrefix + 'HrDepartmentSet'
    })
    .state('hrCommonDataSet', {
        templateUrl: configUrlPrefix + 'HrCommonDataSet'
    })//业务常规配置
    .state('busiCommonDataSet', {
        templateUrl: configUrlPrefix + 'BusiCommonDataSet'
    })
    //--------------账户管理--------------------------
    .state('accRegistUser', {
        templateUrl: 'Account/RegistUser'
    })
    .state('accAssignRoleToUser', {
        templateUrl: 'Account/AssignRoleToUser'
    })
     .state('sysModuleEdit', {
         templateUrl: 'Account/SysModuleEdit'
     })
    .state('sysRoleManage', {
        templateUrl: 'Account/SysRoleManage'
    })
    .state('sysRoleAssignManage', {
        templateUrl: 'Account/SysRoleAssignManage'
    })
     .state('sysAssemblyEdit', {
         templateUrl: 'Account/SysAssemblyEdit'
     })
     .state('assignPowerToRole', {
         templateUrl: 'Account/AssignPowerToRole'
     })
     .state('assignModuleToRolers', {
         templateUrl: 'Account/AssignModuleToRolers'
     })
     .state('assignModuleToRole', {
         templateUrl: 'Account/AssignModuleToRole'
     })
    //--------------ITIL管理--------------------------
       //供应商管理
    .state('itilSupTelManage', {
        templateUrl: itilUrlPrefix + 'ItilSupTelManage'
    })
    //项目开发管理
    .state('itilProjectDevelopManage', {
        templateUrl: itilUrlPrefix + 'ItilProjectDevelopManage'
    })
    //消息通知模块管理
    .state('itilMessageNotifyManage', {
        templateUrl: itilUrlPrefix + 'ItilMessageNotifyManage'
    })
    //邮箱配置管理
    .state('itilEmailManage', {
        templateUrl: itilUrlPrefix + 'ItilEmailManage'
    })
    .state('workHoursManage', {
        templateUrl: 'ProEmployee/WorkHoursManage'
    });
    ////--------------基本配置管理--------------------------
    //.state('hrDepartmentSet', {
    //    templateUrl: 'HrBaseInfoManage/HrDepartmentSet',

    //})
})
