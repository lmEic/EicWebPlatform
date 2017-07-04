/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />

angular.module('bpm.qualityApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'pageslide-directive', 'angular-popups'])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //检验项目Url前缀
    var inspectionUrlPrefix = leeHelper.controllers.quaInspectionManage + "/";
    ////RMAUrl前缀
    var rmaUrlPrefix = leeHelper.controllers.quaRmaManage + "/";

    ////工单Url前缀
    //var mocUrlPrefix = leeHelper.controllers.mocManage + "/";

    //--------------IQC检验项目管理-------------------------
    $stateProvider.state('iqcInspectionItemConfiguration', {
        //IQC检验项目配置
        templateUrl: inspectionUrlPrefix + 'IqcInspectionItemConfiguration'
    })
     //IQC检验方式配置
    .state('iqcInspectionModeConfiguration', {
        templateUrl: inspectionUrlPrefix + 'IqcInspectionModeConfiguration'
    })
     //IQC检验方式转换配置
    .state('inspectionModeSwitchConfiguration', {
        templateUrl: inspectionUrlPrefix + 'InspectionModeSwitchConfiguration'
    })
    .state('inspectionDataGatheringOfIQC', {
        //Iqc检验项目数据采集
        templateUrl: inspectionUrlPrefix + 'InspectionDataGatheringOfIQC'
    })
    .state('inspectionFormManageOfIqc', {
        //iqc检验单管理
        templateUrl: inspectionUrlPrefix + 'InspectionFormManageOfIqc'
    })
    //--------------FQC检验项目管理-------------------------
    .state('fqcInspectionItemConfiguration', {
        //FQC检验项目配置
        templateUrl: inspectionUrlPrefix + 'FqcInspectionItemConfiguration'
    })
    .state('inspectionDataGatheringOfFQC', {
        //Fqc检验项目数据采集
        templateUrl: inspectionUrlPrefix + 'InspectionDataGatheringOfFQC'
    })
    .state('inspectionFormManageOfFqc', {
        //Fqc检验单管理
        templateUrl: inspectionUrlPrefix + 'InspectionFormManageOfFqc'
    })
    ////--------------RMA管理--------------------------
    .state('createRmaForm', {
        templateUrl: rmaUrlPrefix + 'CreateRmaForm'
    })
    .state('rmaInputDescription', {
        templateUrl: rmaUrlPrefix + 'RmaInputDescription'
    })
    .state('rmaInspectionHandle', {
        templateUrl: rmaUrlPrefix + 'RmaInspectionHandle'
    })
    ////------------RMA查询-----------------------
    .state('rmaReportQuery', {
        templateUrl: rmaUrlPrefix + 'RmaReportQuery'
    })
    ////-------------看板管理-------------------
    //.state('jumperWireBoard', {//线材看板管理
    //    templateUrl: boardUrlPrefix + 'JumperWireBoard'
    //})
    ////-------------工单管理-------------------
    //.state('checkOrderBills', {//工单订单对比
    //    templateUrl: mocUrlPrefix + 'CheckOrderBills'
    //})
})
.factory('qualityDataService', function (ajaxService) {
    var dataAccess = {};

    return dataAccess;
})






