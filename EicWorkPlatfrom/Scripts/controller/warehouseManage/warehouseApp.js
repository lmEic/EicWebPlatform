/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.warehouseApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'pageslide-directive', 'angular-popups'])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //检验项目Url前缀
    var inspectionUrlPrefix = leeHelper.controllers.WarehouseManage + "/";

    //--------------设计变更录入-------------------------
    //$stateProvider.state('documentInputRecord', {
    //    //设计变更录入
    //    templateUrl: inspectionUrlPrefix + 'DocumentInputRecord'
    //})
 
    //
    //.state('documentInputRecord', {
    //   templateUrl: inspectionUrlPrefix + 'DocumentInputRecord'
    //})
})
.factory('warehouseDataService', function (ajaxService) {
    var dataAccess = {};

    return dataAccess;
})
