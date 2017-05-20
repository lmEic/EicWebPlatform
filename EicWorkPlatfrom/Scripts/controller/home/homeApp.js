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
.controller('layoutCtrl', function ($scope, $http, navDataService, $modal) {
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
.controller('calendarManageCtrl', function ($scope, homeDataopService, $modal) {

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
        calendarWeeks: null,
        calendarDatas: null,

        calendarViewSwitch: true,
        calendarView: function () {
            vmManager.calendarViewSwitch = !vmManager.calendarViewSwitch;
        },
        //013935获取日历数据
        loadCalendarDatas: function () {

            if (window.localStorage) {
                if (localStorage.getItem("calendarDatas") === null || localStorage.getItem("nowMonth") !== vmManager.nowMonth || localStorage.getItem("nowYear") !== vmManager.nowYear) {
                    $scope.promise = homeDataopService.getCalendarDatas(vmManager.nowYear, vmManager.nowMonth).then(function (datas) {
                        var calendarStr = JSON.stringify(datas);
                        localStorage.setItem("nowYear", vmManager.nowYear);
                        localStorage.setItem("nowMonth", vmManager.nowMonth);
                        localStorage.setItem("calendarDatas", calendarStr);
                        vmManager.calendarDatas = datas;
                        var week = [];
                        for (var i = 0; i < datas.length; i++) {
                            if (datas[i].YearWeekNumber !== 0) {
                                if (week.indexOf(datas[i].YearWeekNumber) === -1) {
                                    week.push(datas[i].YearWeekNumber);
                                }
                            }
                        }
                        vmManager.calendarWeeks = week;
                    });
                } else {
                    var datas = JSON.parse(localStorage.getItem("calendarDatas"));
                    vmManager.calendarDatas = datas;
                    var week = [];
                    for (var i = 0; i < datas.length; i++) {
                        if (datas[i].YearWeekNumber !== 0) {
                            if (week.indexOf(datas[i].YearWeekNumber) === -1) {
                                week.push(datas[i].YearWeekNumber);
                            }
                        }
                    }
                    vmManager.calendarWeeks = week;
                }
            }
        },
        //013935双击显示模态框
        editItem: function (item) {
            if (item.CalendarDay !== "") {
                uiVM = _.clone(item);
                vmManager.editModal.$promise.then(vmManager.editModal.show);
            }
        },
        editModal: $modal({
            title: '修改日历信息',
            content: '',
            templateUrl: "/Home/EditHomeCalendarTpl/",
            controller: function ($scope, homeDataopService) {
                $scope.vm = uiVM;
                $scope.vmManager = vmManager;
                var op = Object.create(leeDataHandler.operateStatus);
                op.vm = uiVM;
                $scope.operate = op;

                var vmEditManager = {
                    calendarColors: [
                        { type: "正常", color: "white" },
                        { type: "法定假日", color: "#29B8CB" },
                        { type: "补班", color: "yellow" },
                        { type: "休假", color: "violet" },
                        { type: "星期六日", color: "red" }]
                };
                $scope.vmEditManager = vmEditManager;
                $scope.editSave = function () {
                    leeHelper.setUserData(uiVM);
                    uiVM.OpSign = 'edit';
                    $scope.promise = homeDataopService.saveCalendarDatas($scope.vm).then(function () {

                        var selItemColor = _.find(vmEditManager.calendarColors, { type: uiVM.DateProperty });
                        if (selItemColor !== undefined) {
                            var selectedItem = _.find(vmManager.calendarDatas, { CalendarDate: $scope.vm.CalendarDate });
                            if (selectedItem !== null) {
                                selectedItem.DateColor = $scope.vm.DateColor = selItemColor.color;
                                selectedItem.DateProperty = $scope.vm.DateProperty = selItemColor.type;
                                selectedItem.Title = $scope.vm.Title;
                            }
                        }
                        var calendarStr = JSON.stringify(vmManager.calendarDatas);
                        localStorage.setItem("calendarDatas", calendarStr);
                        vmManager.editModal.$promise.then(vmManager.editModal.hide);
                    });
                };
            },
            show: false
        })
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

    vmManager.loadCalendarDatas();
});