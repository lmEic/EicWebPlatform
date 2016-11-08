/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />

var purchaseModule = angular.module('bpm.purchaseApp');
purchaseModule.factory('supplierDataOpService', function (ajaxService) {
    var purDb = {};
    var purUrlPrefix = "/" + leeHelper.controllers.supplierManage + "/";
    //-------------------------供应商管理-------------------------------------
    //获取供应商信息
    purDb.getErpSuppplierInfoBy = function (supplierId) {
        var url = purUrlPrefix + 'GetErpSuppplierInfoBy';
        return ajaxService.getData(url, {
            supplierId: supplierId
        });
    };
    //根据年份获取合格供应商清单
    purDb.getPurQualifiedSupplierListBy = function (yearStr)
    {
        var url = purUrlPrefix + 'GetPurQualifiedSupplierListBy';
        return ajaxService.getData(url, {
            yearStr: yearStr,
        });
    };

    return purDb;
});

//供应商信息录入
purchaseModule.controller('purSupplierInputCtrl', function ($scope, supplierDataOpService, $state) {

    var uiVM = {
        SupplierId:null,
        PurchaseType:null,
        SupplierProperty:null,
        SupplierShortName:null,
        SupplierName:null,
        PurchaseUser:null,
        SupplierTel:null,
        SupplierUser:null,
        SupplierFaxNo:null,
        SupplierEmail:null,
        SupplierAddress:null,
        BillAddress:null,
        PayCondition:null,
        Remark:null,
        OpPerson:null,
        OpSign:null,
        OpDate:null,
        OpTime:null,
        Id_key:null,
    }

    $scope.vm = uiVM;

    //视图管理器
    var vmManager = {
        SupplierId:null,

        //mIndex ==1 回车调用  否则直接调用
        getErpSuppplierInfoBy: function ($event, mthIndex) {
            if (mthIndex === 1 && $event.keyCode !== 13) return;
            if (vmManager.SupplierId === null || vmManager.SupplierId === undefined || vmManager.SupplierId.length < 6) return;
            $scope.searchPromise = supplierDataOpService.getErpSuppplierInfoBy(vmManager.SupplierId).then(function (data) {
                if (data !== null) {
                    leeHelper.copyVm(data, uiVM);
                } else {
                    leeHelper.clearVM(uiVM, null);
                }
            })
        }
    };
    $scope.vmManager = vmManager;
});


//生成合格供应商清单
purchaseModule.controller('buildQualifiedSupplierInventoryCtrl', function ($scope, supplierDataOpService, $state) {

    var vmManager = {
        searchYear: new Date().getFullYear(),
        datasets: [],
        datasource:[],
        getPurQualifiedSupplier: function () {
            $scope.searchPromise = supplierDataOpService.getPurQualifiedSupplierListBy(vmManager.searchYear).then(function (datas) {
                vmManager.datasource = datas;
            });
        },
    };

    $scope.vmManager = vmManager;
});

