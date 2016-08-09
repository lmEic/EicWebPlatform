/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.js" />
angular.module('eicPlatform.loginApp', ['ngMessages'])
.directive('focusSetter', function ($timeout) {
        return {
            restrict: 'A',
            scope: {
                trigger:'=focusIf',
            },
            link: function (scope, element, attrs) {
                var dom = element[0];
                scope.$watch('trigger', function(value) {
                    if (value===true) {
                        $timeout(function () {
                            dom.focus();
                            scope.trigger = false;
                        }, scope.$eval(attrs.focusDelay) || 0);
                    }
                });
            }
        };
 })
.controller('loginCtrl', function ($scope, $http) {
    var vm = {
        userId: null,
        password: null,
        start: false,//开始登录
    };
    $scope.vm = vm;

    ///个人头像
    $scope.headPortrait = "../Content/login/profilepicture.jpg";
    ///载入个人头像
    $scope.loadHeadPortrait = function () {
        var loginUser = leeDataHandler.dataStorage.getLoginedUser();
        $scope.headPortrait = loginUser === null ? '../Content/login/profilepicture.jpg' : loginUser.headPortrait;
    };

    var focus = {
        passwordFocus: false,
        loginFocus:false,
    };
    $scope.focus = focus;

    var loginResult = {
        isSuccess: true,
        message:null,
    };
    $scope.loginResult = loginResult;

    $scope.moveFocusToPassword = function ($event,name) {
        if ($event.keyCode === 13)
        {
            focus[name] = true;
        }
    };
    ///登录验证
    $scope.loginCheck = function (isValid) {
        if (isValid) {
            vm.start = true;
            $http.get('/Account/LoginCheck', {
                params: {
                    userId: vm.userId,
                    password: vm.password
                }
            }).success(function (data) {
                leeDataHandler.dataStorage.setLoginedUser(data);
                if (data.LoginStatus !== null) {
                    if (data.LoginStatus.StatusCode === 0) {
                        loginResult.isSuccess = true;
                        window.location = "/Home/Index";
                    }
                    else {
                        showErrorMsg(data);
                    }
                }
            }).error(function (data, status) {
                showErrorMsg(data);
            });
        }
    };

    var showErrorMsg = function (data) {
        vm.start = false;
        loginResult.isSuccess = false;
        loginResult.message = data.LoginStatus.StatusMessage;
        (function () {
            setTimeout(function () {
                loginResult.isSuccess = true;
            }, 100);
        })();
    };

    $scope.loadHeadPortrait();
})