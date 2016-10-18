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
.controller('layoutCtrl', function ($scope) {
    var layoutVm = {
        navViewSwitch: true,//左侧视图导航开关
        switchView: function () {
            layoutVm.navViewSwitch = !layoutVm.navViewSwitch;
            if (layoutVm.navViewSwitch) {
                layoutVm.navLeftSize = '20%';
                layoutVm.navMainSize = '79%';
            }
            else {
                layoutVm.navLeftSize = '5%';
                layoutVm.navMainSize = '95%';
            }
        },
        navLeftSize: '20%',
        navMainSize: '79%',
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
})