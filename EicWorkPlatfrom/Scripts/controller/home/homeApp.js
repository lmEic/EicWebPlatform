/// <reference path="../../angular.min.js" />

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
.controller('layoutCtrl', function ($scope, $http, navDataService, homeDataopService) {
    var layoutVm = {
        navViewSwitch: true,//左侧视图导航开关
        switchView: function () {
            layoutVm.navViewSwitch = !layoutVm.navViewSwitch;
            if (layoutVm.navViewSwitch) {
                layoutVm.navLeftSize = '20%';
                layoutVm.navMainSize = '80%';
            }
            else {
                layoutVm.navLeftSize = '5%';
                layoutVm.navMainSize = '95%';
            }
        },
        navLeftSize: '20%',
        navMainSize: '80%',
    };
    $scope.navLayout = layoutVm;

    ///个人头像
    $scope.headPortrait = "../Content/login/profilepicture.jpg";
    ///载入个人头像
    $scope.loadHeadPortrait = function () {
        var loginUser = leeDataHandler.dataStorage.getLoginedUser();
        $scope.headPortrait = loginUser === null ? '../Content/login/profilepicture.jpg' : loginUser.headPortrait;
    };
    $scope.loadHeadPortrait();

    //013935创建日历视图模型
    var calendarVm = {
        nowYear: null,
        nowMonth: null,
        showEdit :false,
    }
    $scope.calendarVm = calendarVm;
    nowYear = new Date().getFullYear();
    nowMonth = new Date().getMonth() + 1;
    $scope.weeks = [5, 6, 7, 8, 9];
    $scope.calendarDatas = [
        { week: 5 },{ week: 5 },{ week: 5 },
        { dates: 1, day: 'wed', week: 5 ,color:'#ccc',tips:'1',calendarEdit:false},
        { dates: 2, day: 'thu', week: 5, color: '#ccc', tips: '2',calendarEdit:false },
        { dates: 3, day: 'fri', week: 5, color: '#ccc', tips: '3',calendarEdit:false },
        { dates: 4, day: 'sat', week: 5 },
        { dates: 5, day: 'sun', week: 6 },
        { dates: 6, day: 'mon', week: 6 },
        { dates: 7, day: 'tue', week: 6 },
        { dates: 8, dat: 'wed', week: 6 },
        { dates: 9, day: 'thu', week: 6 },
        { dates: 10, day: 'fri', week: 6 },
        { dates: 11, day: 'sat', week: 6 },
        { dates: 12, day: 'sun', week: 7 },
        { dates: 13, day: 'mon', week: 7 },
        { dates: 14, day: 'tue', week: 7 },
        { dates: 15, dat: 'wed', week: 7 },
        { dates: 16, day: 'thu', week: 7 },
        { dates: 17, day: 'fri', week: 7 },
        { dates: 18, day: 'sat', week: 7 },
        { dates: 19, day: 'sun', week: 8 },
        { dates: 20, day: 'mon', week: 8 },
        { dates: 21, day: 'tue', week: 8 },
        { dates: 22, dat: 'wed', week: 8 },
        { dates: 23, day: 'thu', week: 8 },
        { dates: 24, day: 'fri', week: 8 },
        { dates: 25, day: 'sat', week: 8 },
        { dates: 26, day: 'sun', week: 9 },
        { dates: 27, day: 'mon', week: 9 },
        { dates: 28, day: 'tue', week: 9, type:'lastDay'},
        { week: 9 }, { week: 9 }, { week: 9 }, { week: 9 }
    ]
    $scope.promise = homeDataopService.getCalendarDatas(nowYear,nowMonth).then(function (datas) {

    });
    $scope.calendarTipsEdit = function (item) {
        item.calendarEdit = true;
        console.log(item)
    }
})
.factory('homeDataopService', function (ajaxService) {
    var home = {};
    var calendarUrl = "/home/";
    home.getCalendarDatas = function (nowYear, nowMonth) {
        var url = calendarUrl + "GetCalendarDatas";
        return ajaxService.getData(url, {
            nowYear: nowYear,
            nowMonth: nowMonth
        })
    };
    return home;
})