/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var warehouseModule = angular.module('bpm.warehouseApp');
warehouseModule.factory("warehouseDataOpService", function (ajaxService) {
    var ware = {};
    var devUrlPrefix  = "/" + leeHelper.controllers.WarehouseManage+ "/";
    //保存档案记录 StoreExpressData
    ware.saverRecetionExpress = function (model) {
        var url = devUrlPrefix + 'StoreExpressData';
        return ajaxService.postData(url, {
            model: model
        });
    };
    return ware;
})
warehouseModule.controller("recetionExpressCtrl", function ($scope, warehouseDataOpService, $modal) {
    ///录入开发部记录设计更变文档
    var uiVm = {
        ExpressId: '765465920545',
        ExpressCompany: null,
        Consignee: null,
        ReceptionDate: null,
        SendGoodsCompanyAddress: null,
        GoodsNumber: 0,
        ReceiverWorkerId: null,
        ReceiverName: null,
        GetGoodsPerson: null,
        GetGoodsDate: null,
        GoodssStatus: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: null,
        Id_Key: null,
    }
    $scope.vm = uiVm;
    var vmManager = {
        init: function () { },
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            warehouseDataOpService.saverRecetionExpress(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    var equipment = _.clone(uiVm);
                    vmManager.init();
                });
            });
        });
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            console.log(99999);
            vmManager.init();
        });
    };

    //回车交点转下一个
    var $txtInput = $('input:text');
    $txtInput.first().focus();
    $txtInput.bind('keydown', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            var nxtIdx = $txtInput.index(this) + 1;
            $txtInput.filter(":eq(" + nxtIdx + ")").focus();
        }
    });
});


///