angular.module('bpm.developmentApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'pageslide-directive', 'angular-popups'])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //检验项目Url前缀
    var inspectionUrlPrefix = leeHelper.controllers.quaInspectionManage + "/";
    ////RMAUrl前缀
    var rmaUrlPrefix = leeHelper.controllers.quaRmaManage + "/";

    //8D报告Url前缀
    var BaDUrlPrefix = leeHelper.controllers.qua8DManage + "/";

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
    //------------------IPQC检验项目管理-----------------------------

    .state('ipqcInspectionItemConfiguration', {
        //IPQC检验项目配置
        templateUrl: inspectionUrlPrefix + 'IpqcInspectionItemConfiguration'
    })
    .state('inspectionDataGatheringOfIPQC', {
        //ipqc检验项目数据采集
        templateUrl: inspectionUrlPrefix + 'InspectionDataGatheringOfIPQC'
    })
    .state('inspectionFormManageOfIpqc', {
        //ipqc检验单管理
        templateUrl: inspectionUrlPrefix + 'InspectionFormManageOfIpqc'
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
    ////-------------8D报告管理-------------------
    .state('create8DForm', {//创建表单
        templateUrl: BaDUrlPrefix + 'Create8DForm'
    })
    .state('handle8DFolw', {//处理登记
        templateUrl: BaDUrlPrefix + 'Handle8DFolw'
    })
    .state('close8DForm', {//结案签核
        templateUrl: BaDUrlPrefix + 'Close8DForm'
    })
})
.factory('qualityDataService', function (ajaxService) {
    var dataAccess = {};

    return dataAccess;
})
