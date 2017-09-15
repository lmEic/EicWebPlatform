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
    //保存人力信息数据
    hwDataOp.saveManPower = function (entity) {
        var url = hwUrlPrefix + 'SaveManPower';
        return ajaxService.postData(url, {
            entity: entity,
        });
    };
    return hwDataOp;
});

officeAssistantModule.controller('hwManPowerCtrl', function (hwDataOpService, $scope) {
    ///数据实体模型
    var dataVM = {
        OpModule: null,
        OpContent: null,
        OpLog: null,
        OpDate: null,
        OpTime: null,
        OpPerson: null,
        OpSign: null,
        Id_Key: null,
    }
    $scope.vm = dataVM;

    var vmManager = $scope.vmManager = {
        dataEntity: null,
        getManPower: function () {
            $scope.searchPromise = hwDataOpService.getManPower().then(function (dataobj) {
                vmManager.dataEntity = JSON.parse(dataobj.OpContent);

                console.log(vmManager.dataEntity);
            });
        },
    };
    var operate = $scope.operate = Object.create(leeDataHandler.operateStatus);
    operate.save = function () {
        leeDataHandler.dataOperate.add(operate, true, function () {
            $scope.opPromise = hwDataOpService.saveManPower(vmManager.dataEntity).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    leePopups.alert(opresult, 1);
                })
            });
        })
    };
    vmManager.getManPower();
});