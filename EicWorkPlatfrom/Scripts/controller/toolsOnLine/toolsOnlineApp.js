﻿/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.toolsOnlineApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'angular-popups'])

.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {
    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);

    var oAssistantUrlPrefix = leeHelper.controllers.TolOfficeAssistant + "/";

    var wfUrlPrefix = leeHelper.controllers.TolWorkFlow + "/";

    var hwUrlPrefix = leeHelper.controllers.TolCooperateWithHw + "/";

    //--------------名片夹管理--------------------------
    $stateProvider.state('collaborateContactLib', {
        templateUrl: oAssistantUrlPrefix + 'CollaborateContactLib'
    })
    ///工作任务管理
    $stateProvider.state('workTaskManage', {
        templateUrl: oAssistantUrlPrefix + 'WorkTaskManage'
    });
    ///上报改善问题
    $stateProvider.state('reportImproveProblem', {
        templateUrl: oAssistantUrlPrefix + 'ReportImproveProblem'
    });
    //--------------电子签核--------------------------
    ///内部联络单
    $stateProvider.state('wFInternalContactForm', {
        templateUrl: wfUrlPrefix + 'WFInternalContactForm'
    });
    //--------------华为协同--------------------
    $stateProvider.state('hwMaterialBaseInfo', {//物料基础信息
        templateUrl: hwUrlPrefix + 'HwMaterialBaseInfo'
    });
    $stateProvider.state('hwMaterialBomInfo', {//物料BOM信息
        templateUrl: hwUrlPrefix + 'HwMaterialBomInfo'
    });
    $stateProvider.state('hwManpowerInput', {//人力管理
        templateUrl: hwUrlPrefix + 'HwManpowerInput'
    });
    $stateProvider.state('hwLogisticDeliveryInput', {//物流信息登记
        templateUrl: hwUrlPrefix + 'HwLogisticDeliveryInput'
    });
    $stateProvider.state('hwInventoryDetail', {//库存明细
        templateUrl: hwUrlPrefix + 'HwInventoryDetail'
    });
})
