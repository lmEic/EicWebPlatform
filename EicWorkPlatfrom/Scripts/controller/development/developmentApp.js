﻿/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.developmentApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'pageslide-directive', 'angular-popups'])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //检验项目Url前缀
    var inspectionUrlPrefix = leeHelper.controllers.developmentProductsManage + "/";
    
    //--------------设计变更录入-------------------------
    $stateProvider.state('documentInputRecord', {
        //设计变更录入
        templateUrl: inspectionUrlPrefix + 'DocumentInputRecord'
    })
    $stateProvider.state('sponsorDocumentInputRecord', {
        //设计发启录入
        templateUrl: inspectionUrlPrefix + 'SponsorDocumentInputRecord'
    })
       //
    //.state('documentInputRecord', {
    //<div data-ui-view="" class="ng-scope" data-ng-animate="1">
    //


    //   templateUrl: inspectionUrlPrefix + 'DocumentInputRecord'
    //})
})
.factory('developmentDataService', function (ajaxService) {
    var dataAccess = {};

    return dataAccess;
})
