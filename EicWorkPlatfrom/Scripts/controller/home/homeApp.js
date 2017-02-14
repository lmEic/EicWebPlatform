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
.controller('layoutCtrl', function ($scope, $http, navDataService, homeDataopService, $modal) {
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
        nowYear: new Date().getFullYear(),
        nowMonth: new Date().getMonth() + 1,
        calendarDatas:[],
        //calendarDatas: [
        //{ week: 5 }, { week: 5 }, { week: 5 },
        //{ dates: 1, day: 'wed', week: 5, color: '#783', tips: '1', calendarEdit: false },
        //{ dates: 2, day: 'thu', week: 5, color: '#ccc', tips: '2', calendarEdit: false },
        //{ dates: 3, day: 'fri', week: 5, color: '#213', tips: '3', calendarEdit: false },
        //{ dates: 4, day: 'sat', week: 5, color: '#ccc', calendarEdit: false },
        //{ dates: 5, day: 'sun', week: 6, color: '#aaa', calendarEdit: false },
        //{ dates: 6, day: 'mon', week: 6, color: '#bbb', calendarEdit: false },
        //{ dates: 7, day: 'tue', week: 6, color: '#167', calendarEdit: false },
        //{ dates: 8, dat: 'wed', week: 6, color: '#ccc', calendarEdit: false },
        //{ dates: 9, day: 'thu', week: 6, color: '#ddd', calendarEdit: false },
        //{ dates: 10, day: 'fri', week: 6, color: '#ccc', calendarEdit: false },
        //{ dates: 11, day: 'sat', week: 6, color: '#381', calendarEdit: false },
        //{ dates: 12, day: 'sun', week: 7, color: '#ccc', calendarEdit: false },
        //{ dates: 13, day: 'mon', week: 7, color: '#ccc', calendarEdit: false },
        //{ dates: 14, day: 'tue', week: 7, color: '#412', calendarEdit: false },
        //{ dates: 15, dat: 'wed', week: 7, color: '#ccc', calendarEdit: false },
        //{ dates: 16, day: 'thu', week: 7, color: '#ccc', calendarEdit: false },
        //{ dates: 17, day: 'fri', week: 7, color: '#eee', calendarEdit: false },
        //{ dates: 18, day: 'sat', week: 7, color: '#ccc', calendarEdit: false },
        //{ dates: 19, day: 'sun', week: 8, color: '#ccc', calendarEdit: false },
        //{ dates: 20, day: 'mon', week: 8, color: '#ccc', calendarEdit: false },
        //{ dates: 21, day: 'tue', week: 8, color: '#ccc', calendarEdit: false },
        //{ dates: 22, dat: 'wed', week: 8, color: '#212', calendarEdit: false },
        //{ dates: 23, day: 'thu', week: 8, color: '#ccc', calendarEdit: false },
        //{ dates: 24, day: 'fri', week: 8, color: '#cad', calendarEdit: false },
        //{ dates: 25, day: 'sat', week: 8, color: '#fff', calendarEdit: false },
        //{ dates: 26, day: 'sun', week: 9, color: '#ccc', calendarEdit: false },
        //{ dates: 27, day: 'mon', week: 9, color: '#546', calendarEdit: false },
        //{ dates: 28, day: 'tue', week: 9, color: '#ccc', calendarEdit: false },
        //{ week: 9 }, { week: 9 }, { week: 9 }, { week: 9 }
        //],
        //weeks : [5, 6, 7, 8, 9],
    };
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
        console.log(datas)
    });

    //013935编辑日历模态框
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.editItem = function (item) {
        layoutVm = _.clone(item);
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
            $scope.layoutVm = layoutVm;
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