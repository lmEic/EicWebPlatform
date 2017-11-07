/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var iTunnelModule = angular.module('bpm.toolsOnlineApp');
iTunnelModule.factory('iTunnelOpService', function (ajaxService) {
    var iTunnelManPowerDataOp = {};
    var iTunnelManPowerUrl = '/' + leeHelper.controllers.TolManPowerTunnel + '/';
    //获取人力分析数据
    iTunnelManPowerDataOp.getWorkerAnalogDatas = function (department) {
        var url = iTunnelManPowerUrl + 'GetWorkerAnalogDatas';
        return ajaxService.getData(url, {
            department: department,
        });
    };
    //根据条件获取人员信息明细
    iTunnelManPowerDataOp.getWorkersDetailBy = function (department, post, searchMode) {
        var url = iTunnelManPowerUrl + 'GetWorkersDetailBy';
        return ajaxService.getData(url, {
            department: department,
            post: post,
            searchMode: searchMode,
        });
    };
    return iTunnelManPowerDataOp;
});

iTunnelModule.controller("tolManpowerDiskCtrl", function ($scope, iTunnelOpService, dataDicConfigTreeSet) {
    var vmManager = $scope.vmManager = {
        department: null,
        workerAnalogDatas: null,
        getWorkerAnalogDatas: function () {
            $scope.searchPromise = iTunnelOpService.getWorkerAnalogDatas(vmManager.department).then(function (data) {
                vmManager.workerAnalogDatas = data.workerAnalogDatas;
                departmentTreeSet.setTreeDataset(data.departments);
            });
        },
        workersDetail: [],
        workersDetailViewDisplay: false,
        getWorkersDetail: function (department, post, mode) {
            $scope.searchPromise = iTunnelOpService.getWorkersDetailBy(department, post, mode).then(function (datas) {
                vmManager.workersDetail = datas;
                vmManager.workersDetailViewDisplay = true;

                console.log(datas);
            });
        },
    };

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var vm = _.clone(departmentTreeSet.treeNode.vm);
        vmManager.department = vm.DataNodeName === "lightmaster" ? null : vm.DataNodeName;
        vmManager.getWorkerAnalogDatas();

        console.log(vmManager.department);
    };
    $scope.ztree = departmentTreeSet;

    vmManager.getWorkerAnalogDatas();
});