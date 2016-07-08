/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var smModule = angular.module('bpm.sysmanageApp');
smModule.factory('sysitilService', function ($http, $q) {
    var itil = {};
    var urlPrefix = '/' + leeHelper.controllers.itilManage + '/';

    itil.getitilDicData = function (treeModuleKey) {
        var defer = $q.defer();
        var url = urlPrefix + "GetitilDicData";
        $http.get(url, {
            params: {
                treeModuleKey: treeModuleKey,
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    ///根据模块名称与所属类别载入配置数据
    itil.loaditilDicData = function (moduleName, aboutCategory) {
        var defer = $q.defer();
        var url = urlPrefix + "LoaditilDicData";
        $http.get(url, {
            params: {
                moduleName: moduleName,
                aboutCategory: aboutCategory,
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    ///根据树的键值载入配置数据
    itil.saveitilDicData = function (vm, oldVm, opType) {
        var defer = $q.defer();
        var url = urlPrefix + "SaveitilDicData";
        $http.post(url, {
            opType: opType,
            model: vm,
            oldModel: oldVm
        }).success(function (data) {
            defer.resolve(data);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };

    return itil;
});
smModule.controller('supTelManageCtrl', function ($scope, $modal,sysitilService) {
})