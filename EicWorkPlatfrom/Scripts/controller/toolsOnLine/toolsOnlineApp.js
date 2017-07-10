/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.toolsOnlineApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'angular-popups'])

.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {
    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);

    var oAssistantUrlPrefix = leeHelper.controllers.TolOfficeAssistant + "/";
    //--------------名片夹管理--------------------------
    $stateProvider.state('collaborateContactLib', {
        templateUrl: oAssistantUrlPrefix + 'CollaborateContactLib'
    })
    ///工作任务管理
    $stateProvider.state('workTaskManage', {
        templateUrl: oAssistantUrlPrefix + 'WorkTaskManage'
    });
    //-------------上报改善问题----------------------------
    $stateProvider.state('reportImproveProblem', {
        templateUrl: oAssistantUrlPrefix +'ReportImproveProblem'
    })
})

.controller('moduleNavCtrl', function ($scope, navDataService, $state) {
    ///模块导航布局视图对象
    var moduleNavLayoutVm = {
        menus: [],
        navList: [],
        navItems: [],
        navTo: function (navMenu) {
            sessionStorage.setItem("navMenuModuleText", navMenu.Item.ModuleText);
            moduleNavLayoutVm.navItems = [];
            angular.forEach(navMenu.Childrens, function (childNav) {
                var navItem = _.findWhere(moduleNavLayoutVm.menus, { Name: childNav.ModuleName, AtLevel: 3 });
                if (!angular.isUndefined(navItem)) {
                    moduleNavLayoutVm.navItems.push(navItem);
                }
            });
        },
        stateTo: function (navItem) {
            var navMenuModuleText = sessionStorage.getItem("navMenuModuleText");
            leeHelper.setWebSiteTitle(navMenuModuleText, navItem.ModuleText);
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
        navMainSize: '83%'
    };
    $scope.navLayout = moduleNavLayoutVm;
    $scope.promise = navDataService.getSubModuleNavs('在线工具', 'ToolsOnLine').then(function (datas) {
        moduleNavLayoutVm.menus = datas;
        moduleNavLayoutVm.navList = _.where(datas, { AtLevel: 2 });
    });
    var user = $scope.loginUser = Object.create(leeLoginUser);
    user.loadHeadPortrait();
});
