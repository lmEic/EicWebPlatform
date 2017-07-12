/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.toolsOnlineApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'angular-popups'])

.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {
    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);

    var oAssistantUrlPrefix = leeHelper.controllers.TolOfficeAssistant + "/";

    var wfUrlPrefix = leeHelper.controllers.TolWorkFlow + "/";

    //--------------名片夹管理--------------------------
    $stateProvider.state('collaborateContactLib', {
        templateUrl: oAssistantUrlPrefix + 'CollaborateContactLib'
    })
    ///工作任务管理
    $stateProvider.state('workTaskManage', {
        templateUrl: oAssistantUrlPrefix + 'WorkTaskManage'
    });
    ///工作任务管理
    $stateProvider.state('reportImproveProblem', {
        templateUrl: oAssistantUrlPrefix + 'ReportImproveProblem'
    });
    //--------------电子签核--------------------------
    ///内部联络单
    $stateProvider.state('wFInternalContactForm', {
        templateUrl: wfUrlPrefix + 'WFInternalContactForm'
    });
})
