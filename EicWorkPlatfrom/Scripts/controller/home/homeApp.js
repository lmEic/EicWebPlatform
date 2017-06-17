/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="D:\EICWebPlatForm\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore.js" />

angular.module('bpm.homeApp', ['eicomm.directive', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap'])
.factory('homeDataopService', function (ajaxService) {
    var home = {};
    var calendarUrl = "/home/";
    home.getCalendarDatas = function (nowYear, nowMonth) {
        var url = calendarUrl + "GetCalendarDatas";
        return ajaxService.getData(url, {
            nowYear: nowYear,
            nowMonth: nowMonth
        });
    };
    home.saveCalendarDatas = function (vm) {
        var url = calendarUrl + "SaveCalendarDatas";
        return ajaxService.postData(url, {
            vm: vm
        });
    };

    return home;
})
.controller('mainCtrl', function ($scope) {
    var user = $scope.loginUser = Object.create(leeLoginUser);
    user.loadHeadPortrait();

})
.controller('moduleNavCtrl', function ($scope, $http, navDataService) {
    var moduleNav = {
        navList: []
    };
    $scope.nav = moduleNav;

    $scope.promise = navDataService.getModuleNavs().then(function (datas) {
        moduleNav.navList = datas;
    });

    $scope.logout = function () {
        $http.get('/Account/Logout').success(function (datas) {
            if (datas === "ok") {
                window.location = "/Account/Login";
            }
        }).error(function (errdata) {
        });
    };
})
//布局控制器
.controller('layoutCtrl', function ($scope, $http, navDataService) {
    var layoutVm = {
        navViewSwitch: true,//左侧视图导航开关
        switchView: function () {
            layoutVm.navViewSwitch = !layoutVm.navViewSwitch;
            if (layoutVm.navViewSwitch) {
                layoutVm.navLeftSize = '25%';
                layoutVm.navMainSize = '75%';
            }
            else {
                layoutVm.navLeftSize = '5%';
                layoutVm.navMainSize = '95%';
            }
        },
        navLeftSize: '25%',
        navMainSize: '75%'
    };
    $scope.navLayout = layoutVm;
})
//日历控制器
.controller('calendarManageCtrl', function ($scope, connDataOpService) {
    //013935创建视图模型
    var uiVM = {
        CalendarMonth: 0,
        NowMonthWeekNumber: 0,
        ChineseCalendar: null,
        CalendarDate: null,
        CalendarYear: 0,
        CalendarDay: null,
        YearWeekNumber: 0,
        CalendarWeek: 0,
        DateProperty: null,
        DateColor: null,
        Title: null,
        OpSign: null,
        OpDate: null,
        OpTime: null,
        OpPerson: null,
        Id_Key: null
    };
    //013935创建视图管理器
    var vmManager = {
        nowYear: new Date().getFullYear(),
        nowMonth: new Date().getMonth() + 1,
        calendar: null,
        calendarViewSwitch: true,
        calendarView: function () {
            vmManager.calendarViewSwitch = !vmManager.calendarViewSwitch;
        },
        loadCalendarDatas: function () {
            var key = vmManager.nowYear + "-" + vmManager.nowMonth;
            var datasStr = null;
            if (window.localStorage) {
                datasStr = localStorage.getItem(key);
                if (datasStr === null) {
                    $scope.promise = connDataOpService.getCalendarDatas(vmManager.nowYear, vmManager.nowMonth).then(function (datas) {
                        datasStr = JSON.stringify(datas);
                        sessionStorage.setItem(key, datasStr);
                        vmManager.calendar = datas;
                    });
                }
                else {
                    vmManager.calendar = JSON.parse(datasStr);
                }
            }
        }
    };
    $scope.vmManager = vmManager;
    vmManager.loadCalendarDatas();
});