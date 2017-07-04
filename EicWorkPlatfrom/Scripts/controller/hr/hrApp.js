/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />
angular.module('bpm.hrApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'angular-popups'])

.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    
    //--------------人员管理--------------------------
    $stateProvider.state('workerInfoManage', {
        templateUrl: 'ProEmployee/WorkerInfoManage'

    }).state('proStationManage', {
        templateUrl: 'ProEmployee/ProStationManage'

    }).state('proClassManage', {
        templateUrl: 'ProEmployee/ProClassManage'

    }).state('workHoursManage', {
        templateUrl: 'ProEmployee/WorkHoursManage'
    })
    //--------------员工档案管理--------------------------
     .state('hrEmployeeDataInput', {
         templateUrl: 'HrArchivesManage/HrEmployeeDataInput'
     })
    .state('hrDepartmentChange', {
        templateUrl: 'HrArchivesManage/HrDepartmentChange'
    })
    .state('hrPostChange', {
        templateUrl: 'HrArchivesManage/HrPostChange'
    })
     .state('hrStudyManage', {
         templateUrl: 'HrArchivesManage/HrStudyManage'
     })
     .state('hrTelManage', {
         templateUrl: 'HrArchivesManage/HrTelManage'
     })
     .state('hrChangeWorkerId', {
         templateUrl: 'HrArchivesManage/HrChangeWorkerId'
     })
     .state('hrLeaveOffManage', {
         templateUrl: 'HrArchivesManage/HrLeaveOffManage'
     })
    //--------------档案业务管理--------------------------
     .state('hrPrintCard', {
         templateUrl: 'HrArchivesManage/HrPrintCard'
     })
    //--------------考勤业务管理--------------------------
     .state('hrClassTypeManage', {
         templateUrl: 'HrAttendanceManage/HrClassTypeManage'
     })
     .state('hrSumerizeAttendanceData', {
         templateUrl: 'HrAttendanceManage/HrSumerizeAttendanceData'
     })
     .state('hrAskLeaveManage', {
         templateUrl: 'HrAttendanceManage/HrAskLeaveManage'
     })
     .state('hrHandleException', {
         templateUrl: 'HrAttendanceManage/HrHandleException'
     })
    //--------------总务管理--------------------------
    //厂服管理
     .state('gaWorkerClothesManage', {
         templateUrl: 'HrGeneralAffairsManage/GaWorkerClothesManage'
     });
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
    $scope.promise = navDataService.getSubModuleNavs('人力资源管理', 'HrManage').then(function (datas) {
        moduleNavLayoutVm.menus = datas;
        moduleNavLayoutVm.navList = _.where(datas, { AtLevel: 2 });
    });
    var user = $scope.loginUser = Object.create(leeLoginUser);
    user.loadHeadPortrait();
});

