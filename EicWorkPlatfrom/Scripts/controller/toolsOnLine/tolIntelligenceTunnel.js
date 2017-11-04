/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var iTunnelModule = angular.module('bpm.toolsOnlineApp');
iTunnelModule.factory('iTunnelOpService', function (ajaxService) {
    var iTunnelManPowerDataOp = {};
    var iTunnelManPowerUrl = '/' + leeHelper.controllers.TolManPowerTunnel + '/';
    //获取人力分析数据
    iTunnelManPowerDataOp.getWorkerAnalogDatas = function () {
        var url = iTunnelManPowerUrl + 'GetWorkerAnalogDatas';
        return ajaxService.getData(url);
    };

    return iTunnelManPowerDataOp;
});

iTunnelModule.controller("tolManpowerDiskCtrl", function ($scope, iTunnelOpService, dataDicConfigTreeSet) {
    var vmManager = $scope.vmManager = {
        workerAnalogDatas: null,
        getWorkerAnalogDatas: function () {
            $scope.searchPromise = iTunnelOpService.getWorkerAnalogDatas().then(function (data) {
                vmManager.workerAnalogDatas = data.workerAnalogDatas;
                departmentTreeSet.setTreeDataset(data.departments);

                console.log(data);
            });
        },
    };

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        //departmentDto = _.clone(departmentTreeSet.treeNode.vm);
        //departmentDto.ModuleName = "HrBaseInfoManage";
        //departmentDto.AboutCategory = "HrDepartmentSet";
        //oldDepartmentDto = _.clone(departmentDto);
        //$scope.vm = departmentDto;
    };
    $scope.ztree = departmentTreeSet;

    vmManager.getWorkerAnalogDatas();
});