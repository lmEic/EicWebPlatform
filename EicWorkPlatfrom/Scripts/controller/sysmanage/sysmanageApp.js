/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.sysmanageApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'pageslide-directive', 'angular-popups'])

.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {
    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);

    var configUrlPrefix = leeHelper.controllers.configManage + "/";

    var itilUrlPrefix = leeHelper.controllers.itilManage + "/";

    //--------------配置管理--------------------------
    $stateProvider.state('hrDepartmentSet', {
        templateUrl: configUrlPrefix + 'HrDepartmentSet'
    })
    .state('hrCommonDataSet', {
        templateUrl: configUrlPrefix + 'HrCommonDataSet'
    })
    //--------------账户管理--------------------------
    .state('accRegistUser', {
        templateUrl: 'Account/RegistUser'
    })
    .state('accAssignRoleToUser', {
        templateUrl: 'Account/AssignRoleToUser'
    })
     .state('sysModuleEdit', {
         templateUrl: 'Account/SysModuleEdit'
     })
    .state('sysRoleManage', {
        templateUrl: 'Account/SysRoleManage'
    })
    .state('sysRoleAssignManage', {
        templateUrl: 'Account/SysRoleAssignManage'
    })
     .state('sysAssemblyEdit', {
         templateUrl: 'Account/SysAssemblyEdit'
     })
     .state('assignPowerToRole', {
         templateUrl: 'Account/AssignPowerToRole'
     })
     .state('assignModuleToRole', {
         templateUrl: 'Account/AssignModuleToRole'
     })
    //--------------ITIL管理--------------------------
       //供应商管理
    .state('itilSupTelManage', {
        templateUrl: itilUrlPrefix + 'ItilSupTelManage'
    })
    //项目开发管理
    .state('itilProjectDevelopManage', {
        templateUrl: itilUrlPrefix + 'ItilProjectDevelopManage'
    })
    //消息通知模块管理
    .state('itilMessageNotifyManage', {
        templateUrl: itilUrlPrefix + 'ItilMessageNotifyManage'
    })
    //邮箱配置管理
    .state('itilEmailManage', {
        templateUrl: itilUrlPrefix + 'ItilEmailManage'
    })
    .state('workHoursManage', {
        templateUrl: 'ProEmployee/WorkHoursManage'
    });
    ////--------------基本配置管理--------------------------
    //.state('hrDepartmentSet', {
    //    templateUrl: 'HrBaseInfoManage/HrDepartmentSet',

    //})
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
            });
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
        navMainSize: '83%'
    };
    $scope.navLayout = moduleNavLayoutVm;
    $scope.promise = navDataService.getSubModuleNavs('系统管理', 'EicSystemManage').then(function (datas) {
        moduleNavLayoutVm.menus = datas;
        moduleNavLayoutVm.navList = _.where(datas, { AtLevel: 2 });
    });
    var user = $scope.loginUser = Object.create(leeLoginUser);
    user.loadHeadPortrait();
});
