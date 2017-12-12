/// <reference path="../shippingApp.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.productApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', "pageslide-directive", 'angular-popups'])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //看板Url前缀
    var boardUrlPrefix = leeHelper.controllers.productBoard + "/";
    //工单Url前缀
    var mocUrlPrefix = leeHelper.controllers.mocManage + "/";
    //离职人员前缀
    var ProAskLeaveUrlPrefix = leeHelper.controllers.leaveAsk + "/";

    //报表Url前缀
    var reportUrlPrefix = leeHelper.controllers.dailyReport + "/";

    //重工制程前缀
    var redoUrlPrefix = leeHelper.controllers.redoProduct + "/";

    
 
    //--------------生产日报-------------------------
    //生产工艺设定
    $stateProvider.state('dReportFlowSet', {
       
        templateUrl: reportUrlPrefix + 'DReportFlowSet'
    });
    //标准工时设定
    $stateProvider.state('dReportHoursSet', {
     
        templateUrl: reportUrlPrefix + 'DReportHoursSet'
    });
    //非生产原因设定
    $stateProvider.state('dReportUnproductionSet', {
       
        templateUrl: reportUrlPrefix + 'DReportUnproductionSet'
    });
    //不良原因设定
    $stateProvider.state('dReportBadReasonSet', {
        templateUrl: reportUrlPrefix + 'DReportBadReasonSet'
    });
    //生产工单分派管理
    $stateProvider.state('dRProductOrderDispatching', {
        
        templateUrl: reportUrlPrefix + 'DRProductOrderDispatching'
    })
     //日报录入
    .state('dReportInput', {
       
        templateUrl: reportUrlPrefix + 'DReportInput'
    })
     //重工登记
    .state('dRRedoInput', {

        templateUrl: redoUrlPrefix + 'DRRedoInput'
    })


     //--------------人员管理--------------------------
    .state('registWorkerInfo', {
        templateUrl: 'ProEmployee/RegistWorkerInfo'
    })
    //请假管理
    .state('proAskLeaveManage', {
        templateUrl: 'ProEmployee/ProAskLeaveManage'
    })
    //加班管理
    .state('proWorkOverHoursManage', {
        templateUrl: 'ProEmployee/ProWorkOverHoursManage'
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
