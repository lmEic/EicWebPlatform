﻿
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var productModule = angular.module('bpm.productApp');
productModule.factory('mocDataOpService', function (ajaxService) {
    var mocUrlPrefix = "/" + leeHelper.controllers.mocManage + "/";
    var mocDataOp = {};
    //-------------------------工单管理-------------------------------------
    //待填写
    mocDataOp.getProductFlowList = function (department, productName, orderId, searchMode) {
        var url = urlPrefix + 'GetProductFlowList';
        return ajaxService.getData(url, {
            department: department,
            productName: productName,
            orderId: orderId,
            searchMode: searchMode
        });
    };

    ///获取校验清

    mocDataOp.getProductTypeMonitor = function (department) {
        var url = mocUrlPrefix + 'GetMS589ProductTypeMonitor';
        return ajaxService.getData(url, {
            department: department
        });
    };

    //CreateProductTypeMonitoList

    return mocDataOp;
});
//工单对比核对
productModule.controller('checkOrderBillsCtrl', function ($scope,mocDataOpService,$modal) {
    var vmManager = {
        department: '589',
        dataSource: [],

        getProductTypeMonitor: function () {
            $scope.searchPromise = mocDataOpService.getProductTypeMonitor(vmManager.department).then(function (datas) {
                vmManager.dataSource = datas;
            });
        }
    };

    $scope.vmManager = vmManager;  
});
//工单对比核对
productModule.controller('productionSnManagerCtrl', function ($scope, mocDataOpService, $modal) {

    var UiVm = {
        ProductType: '566272-B3215L',
        ProductTypeCommon: null,
        MaterialID: null,
        StartSN: null,
        EndSN: null,
        IsEngraved: 0,
        Memo: null,
        OpPerson: null,
        OpTime: null,
        IsCompute: 0,
        Id_Key: null,
    };
    var vmManager = {
        department: null,
        dataSource: [],

        getProductTypeMonitor: function () {
            $scope.searchPromise = mocDataOpService.getProductTypeMonitor(vmManager.department).then(function (datas) {
                vmManager.dataSource = datas;
            });
        }
    };

    $scope.vmManager = vmManager;
});