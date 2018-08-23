/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.warehouseApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'pageslide-directive', 'angular-popups'])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //检验项目Url前缀
    var inspectionUrlPrefix = leeHelper.controllers.WarehouseManage + "/";
    console.log(inspectionUrlPrefix);
    $stateProvider.state('receptionExpress', {
        //
     
        templateUrl: inspectionUrlPrefix + 'ReceptionExpress'
    })
    $stateProvider.state('takeExpress', {
        templateUrl: inspectionUrlPrefix + 'TakeExpress'
    })
    $stateProvider.state('queriesExpress', {
        templateUrl: inspectionUrlPrefix + 'QueriesExpress'
    })
    //
    //.state('documentInputRecord', {
    //   templateUrl: inspectionUrlPrefix + 'DocumentInputRecord'
    //})
})
.factory('warehouseDataService', function (ajaxService) {
    var dataAccess = {};

    return dataAccess;
})
