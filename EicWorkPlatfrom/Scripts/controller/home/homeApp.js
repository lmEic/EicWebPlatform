﻿/// <reference path="../../angular.min.js" />

angular.module('bpm.homeApp', ['eicomm.directive', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap'])
.controller('moduleNavCtrl', function ($scope,$http,navDataService) {
    var moduleNav = {
        navList:[]
    };
    $scope.nav = moduleNav;

    $scope.promise = navDataService.getModuleNavs().then(function (datas) {
        moduleNav.navList = datas;
    });

    $scope.logout = function () {
        $http.get('/Account/Logout').success(function (datas) {
            if (datas === "ok")
            {
                window.location = "/Account/Login";
            }
        }).error(function (errdata) {
        });
    }
})
//布局控制器
.controller('layoutCtrl', function ($scope, $http, navDataService, homeDataopService, $modal) {

    var uiVM = {
        CalendarDay:null,
        CalendarMonth:null,
        CalendarWeek:null,
        CalendarYear:null,
        DateColor:null,
        DateProperty:null,
        Title:null,
        YearWeekNumber:null,
        NowMonthWeekNumber:null,
    }
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
        navMainSize: '75%',
        nowYear: new Date().getFullYear(),
        nowMonth: new Date().getMonth() + 1,
        calendarWeeks: null,
        calendarDatas: null
    };
    $scope.vm = uiVM;
    $scope.navLayout = layoutVm;
    $scope.layoutVm = layoutVm;
    ///个人头像
    $scope.headPortrait = "../Content/login/profilepicture.jpg";
    ///载入个人头像
    $scope.loadHeadPortrait = function () {
        var loginUser = leeDataHandler.dataStorage.getLoginedUser();
        $scope.headPortrait = loginUser === null ? '../Content/login/profilepicture.jpg' : loginUser.headPortrait;
    };
    $scope.loadHeadPortrait();

    //013935创建日历视图模型
    $scope.promise = homeDataopService.getCalendarDatas(layoutVm.nowYear, layoutVm.nowMonth).then(function (datas) {
        layoutVm.calendarDatas = datas;
        var week = [];
        for (var i = 0; i < datas.length; i++) {
            if (datas[i].YearWeekNumber != 0) {
                if (week.indexOf(datas[i].YearWeekNumber) == -1) {
                    week.push(datas[i].YearWeekNumber);
                }
            }
        }
        layoutVm.calendarWeeks = week;
    })

    //013935编辑日历模态框
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.editItem = function (item) {
        uiVM = _.clone(item);
        console.log(uiVM);
        operate.editModal.$promise.then(operate.editModal.show);
    }
    operate.editModal = $modal({
        title: '修改日历信息',
        content: '',
        templateUrl:"Home/EditHomeCalendarTpl/",
        controller: function ($scope) {
            //$scope.vm = {
            //    Remarks: null,
            //};
            //var op = Object.create(leeDataHandler.operateStatus);
            //$scope.save = function (isValid) {
            //    leeDataHandler.dataOperate.add(op, isValid, function () {
            //        vmManager.edittingRow.Remarks = $scope.vm.Remarks;
            //        uiVM.Remarks = vmManager.edittingRow.Remarks;
            //        vmManager.editRemarksModal.$promise.then(vmManager.editRemarksModal.hide);
            //    });
            //};  
        },
        show: false,
    });
})
.factory('homeDataopService', function (ajaxService) {
    var home = {};
    var calendarUrl = "/home/";
    home.getCalendarDatas = function (nowYear,nowMonth) {
        var url = calendarUrl + "GetCalendarDatas";
        return ajaxService.getData(url, {
            nowYear: nowYear,
            nowMonth: nowMonth
        })
    };
    return home;
})