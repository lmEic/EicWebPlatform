/// <reference path="../shippingApp.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.productApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', "pageslide-directive", 'angular-popups'])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //看板Url前缀
    var boardUrlPrefix = leeHelper.controllers.productBoard + "/";
    //报表Url前缀
    var reportUrlPrefix = leeHelper.controllers.dailyReport + "/";
    //重工制程前缀
    var redoUrlPrefix = leeHelper.controllers.redoProduct + "/";
    //工单Url前缀
    var mocUrlPrefix = leeHelper.controllers.mocManage + "/";
    //人员管理
    var proEmployeeUrlPrefix = leeHelper.controllers.proEmployee+ "/"
    //--------------生产日报-------------------------
    $stateProvider.state('dReportFlowSet', {
        //生产工艺设定
        templateUrl: reportUrlPrefix + 'DReportFlowSet'
    });
    $stateProvider.state('dReportHoursSet', {
        //标准工时设定
        templateUrl: reportUrlPrefix + 'DReportHoursSet'
    });
    $stateProvider.state('dReportUnproductionSet', {
        //非生产原因设定
        templateUrl: reportUrlPrefix + 'DReportUnproductionSet'
    });
    $stateProvider.state('dReportBadReasonSet', {
        //不良原因设定
        templateUrl: reportUrlPrefix + 'DReportBadReasonSet'
    });
    $stateProvider.state('dRProductOrderDispatching', {
        //生产工单分派管理
        templateUrl: reportUrlPrefix + 'DRProductOrderDispatching'
    })
    .state('dReportInput', {
        //日报录入
        templateUrl: reportUrlPrefix + 'DReportInput'
    })
    .state('dRRedoInput', {
        //重工登记
        templateUrl: redoUrlPrefix + 'DRRedoInput'
    })
    //--------------人员管理--------------------------
    .state('registWorkerInfo', {
        templateUrl: proEmployeeUrlPrefix+'RegistWorkerInfo'
    })
    //请假管理
    .state('proAskLeaveManage', {
        templateUrl: proEmployeeUrlPrefix +'ProAskLeaveManage'
    })
    //加班管理
    .state('proWorkOverHoursManage', {
        templateUrl: proEmployeeUrlPrefix +'ProWorkOverHoursManage'
    })
    //-------------看板管理-------------------
    .state('jumperWireBoard', {//线材看板管理
        templateUrl: boardUrlPrefix + 'JumperWireBoard'
    })
    //-------------工单管理-------------------
    .state('checkOrderBills', {//工单订单对比
        templateUrl: mocUrlPrefix + 'CheckOrderBills'
    });
})
