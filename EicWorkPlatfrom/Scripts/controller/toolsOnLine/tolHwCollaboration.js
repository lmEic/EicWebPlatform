/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var officeAssistantModule = angular.module('bpm.toolsOnlineApp');
officeAssistantModule.factory('hwDataOpService', function (ajaxService) {
    var hwDataOp = {};
    var hwUrlPrefix = '/' + leeHelper.controllers.TolCooperateWithHw + '/';
    //获取人力信息
    hwDataOp.getManPower = function () {
        var url = hwUrlPrefix + 'GetManPower';
        return ajaxService.getData(url);
    };
    return hwDataOp;
});

officeAssistantModule.controller('hwManPowerCtrl', function (hwDataOpService, $scope) {

    var vmManager = $scope.vmManager = {
        dataEntity: null,
        getManPower: function () {
            $scope.searchPromise = hwDataOpService.getManPower().then(function (dataobj) {
                vmManager.dataEntity = dataobj;

                console.log(dataobj);
            });
        },
    };

    vmManager.getManPower();
});