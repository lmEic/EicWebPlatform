/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />

var purchaseModule = angular.module('bpm.purchaseApp');
purchaseModule.factory('supplierDataOpService', function (ajaxService) {
    var purDb = {};
    var urlPrefix = "/" + leeHelper.controllers.supplierManage + "/";
    var supplierDataOp = {};
    //-------------------------供应商管理-------------------------------------
    //待填写
    return purDb;
});

purchaseModule.controller('buildQualifiedSupplierInventoryCtrl', function ($scope, supplierDataOpService, $state) {

    var vmManager = {
        AssetNumber: 55555
    };

    $scope.vmManager = vmManager;
});

