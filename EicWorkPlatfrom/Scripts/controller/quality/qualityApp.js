/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />

angular.module('bpm.qualityApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', "pageslide-directive"])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //检验项目Url前缀
    var inspectionUrlPrefix = leeHelper.controllers.quaInspectionManage + "/";
    ////报表Url前缀
    //var reportUrlPrefix = leeHelper.controllers.dailyReport + "/";

    ////工单Url前缀
    //var mocUrlPrefix = leeHelper.controllers.mocManage + "/";

    //--------------检验项目管理-------------------------
    $stateProvider.state('iqcInspectionItemConfiguration', {
        //IQC检验项目配置
        templateUrl: inspectionUrlPrefix + 'IqcInspectionItemConfiguration',
    })
    .state('inspectionDataGatheringOfIQC', {
        //日报录入
        templateUrl: inspectionUrlPrefix + 'InspectionDataGatheringOfIQC',
    })
    ////--------------人员管理--------------------------
    //.state('registWorkerInfo', {
    //    templateUrl: 'ProEmployee/RegistWorkerInfo'
    //})
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
    //var urlPrefix = '/ProEmployee/'

    //dataAccess.getWorkers = function () {
    //    return ajaxService.getData(urlPrefix + 'GetWorkers', {});
    //};

    //dataAccess.GetWorkerBy = function (workerId) {
    //    return ajaxService.getData(urlPrefix + 'GetWorkerBy', {
    //        workerId: workerId
    //    });
    //};

    //dataAccess.registWorker = function (worker) {
    //    return ajaxService.postData(urlPrefix + 'RegistWorker', {
    //        worker: worker
    //    });
    //};

    return dataAccess;
})
.controller('moduleNavCtrl', function ($scope, navDataService, $state) {
    ///模块导航布局视图对象
    var moduleNavLayoutVm = {
        menus: [],
        navList: [],
        navItems: [],
        navTo: function (navMenu) {
            moduleNavLayoutVm.navItems = [];
            angular.forEach(navMenu.Childrens, function (childNav) {
                var navItem = _.findWhere(moduleNavLayoutVm.menus, { Name: childNav.ModuleName, AtLevel: 3 });
                if (!angular.isUndefined(navItem)) {
                    moduleNavLayoutVm.navItems.push(navItem);
                }
            })
        },
        stateTo: function (navItem) {
            $state.go(navItem.UiSerf);
        },
        navViewSwitch: true,//左侧视图导航开关
        switchView: function () {
            moduleNavLayoutVm.navViewSwitch = !moduleNavLayoutVm.navViewSwitch;
            if (moduleNavLayoutVm.navViewSwitch) {
                moduleNavLayoutVm.navLeftSize = '16%';
                moduleNavLayoutVm.navMainSize = '83%';
            }
            else {
                moduleNavLayoutVm.navLeftSize = '3%';
                moduleNavLayoutVm.navMainSize = '96%';
            }
        },
        navLeftSize: '16%',
        navMainSize: '83%',
    };
    $scope.navLayout = moduleNavLayoutVm;
    $scope.promise = navDataService.getSubModuleNavs('质量管理', 'QuantityManage').then(function (datas) {
        moduleNavLayoutVm.menus = datas;
        moduleNavLayoutVm.navList = _.where(datas, { AtLevel: 2 });
    });
})