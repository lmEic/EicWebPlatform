/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />

angular.module('bpm.developmentApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'pageslide-directive', 'angular-popups'])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //检验项目Url前缀
    var inspectionUrlPrefix = leeHelper.controllers.developmentProductsManage + "/";
    
    //--------------设计变更录入-------------------------
    $stateProvider.state('documentInputRecord', {
        //IQC检验项目配置
        templateUrl: inspectionUrlPrefix + 'DocumentInputRecord'
    })

       //
    //.state('documentInputRecord', {
    //    templateUrl: inspectionUrlPrefix + 'DocumentInputRecord'
    //})
})
.factory('developmentDataService', function (ajaxService) {
    var dataAccess = {};

    return dataAccess;
})
