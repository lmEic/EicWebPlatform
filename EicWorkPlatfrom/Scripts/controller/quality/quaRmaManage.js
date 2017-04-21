﻿
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');
//数据访问工厂
qualityModule.factory("rmaDataOpService", function (ajaxService) {
    var rma = {};
    var quaRmaManageUrl = "/quaRmaManage/";
    ////////
    rma.getRmaBussesDescriptionDatas = function (rmaId) {
        var url = quaRmaManageUrl + "GetRmaBussesDescriptionDatas";
        return ajaxService.getData(url, {
            rmaId: rmaId
        });
    };
    ///////////
    rma.getRmaBuildRmaId = function () {
        var url = quaRmaManageUrl + "getRmaBuildRmaId";
        return ajaxService.getData(url, {
        });
    };
    return rma;
})
////创建表单
qualityModule.controller('createRmaFormCtrl', function ($scope) {
    ///视图模型
    var uiVm = $scope.vm = {
        RmaId: null,
        CustomerId: null,
        CustomerShortName: null,
        OpPerson: null,
        OpSgin: null,
    };

    var vmManager = {
        activeTab: 'initTab',
        //自动生成RMA编号
        autoCreateRmaId: function () {

        },
        //获取表单数据
        getRmaFormDatas: function () { },
        datasets: [],
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };

});
//// 描述登记
qualityModule.controller('rmaInputDescriptionCtrl', function ($scope) {
    ///视图模型
    var rmaVm = $scope.rmavm = {
        RmaId: null,
        CustomerId: null,
        CustomerShortName: null,
    };

    var vmManager = {
        //获取表单数据
        getRmaFormDatas: function () { },
        datasets: [],
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };
})

////检验处置
qualityModule.controller('rmaInspectionHandleCtrl', function ($scope) {
    ///视图模型
    var rmaVm = $scope.rmavm = {
        RmaId: null,
        CustomerId: null,
        CustomerShortName: null,
    };

    var vmManager = {
        //获取表单数据
        getRmaFormDatas: function () { },
        datasets: [],
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };
})