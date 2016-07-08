/// <reference path="../angular.js" />
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

    var focus = {
        passwordFocus: false,
    };
    $scope.focus = focus;

    $scope.moveFocusToPassword = function ($event,name) {
        if ($event.keyCode === 13)
        {
            focus[name] = true;
        }
    };
    $scope.loginCheck = function (isValid) {
        if (isValid) {
            vm.start = true;
            $http.get('/Account/LoginCheck', {
                params: {
                    userId: vm.userId,
                    password: vm.password
                }
            }).success(function (data) {
                if (data.LoginStatus !== null) {
                    if (data.LoginStatus.StatusCode === 0) {
                        window.location = "/Home/Index";
                    }
                    else {
                        vm.start = false;
                    }
                }
            }).error(function (data,status) {
            });
        }
    };
})