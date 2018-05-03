angular.module('bpm.developmentApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'pageslide-directive', 'angular-popups'])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //检验项目Url前缀
    var inspectionUrlPrefix = leeHelper.controllers.developmentProductsManage + "/";

    //--------------IQC检验项目管理-------------------------
    $stateProvider.state('iqcInspectionItemConfiguration', {
        //IQC检验项目配置
        templateUrl: inspectionUrlPrefix + 'IqcInspectionItemConfiguration'
    })

       //IQC检验方式配置
    .state('iqcInspectionModeConfiguration', {
        templateUrl: inspectionUrlPrefix + 'IqcInspectionModeConfiguration'
    })
})
.factory('developmentDataService', function (ajaxService) {
    var dataAccess = {};

    return dataAccess;
})
