/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />

var purchaseModule = angular.module('bpm.purchaseApp');
//获取供应商信息
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
    ///上传供应商证书文件
    purDb.uploadPurSupplierCertificateFile = function (file) {
        var url = purUrlPrefix + 'UploadPurSupplierCertificateFile';
        return ajaxService.uploadFile(url, file);
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
purchaseModule.controller('buildQualifiedSupplierInventoryCtrl', function ($scope, supplierDataOpService,$modal) {

    var item = {
        BillAddress
:
"慈溪市周巷镇三江口村协同191号",
        EligibleCertificate
:
null,
        Id_key
    :
    0,
        LastPurchaseDate
    :
    "2016-11-22",
        OpDate
    :
    "0001-01-01",
        OpPerson
    :
    null,
        OpSign
    :
    null,
        OpTime
    :
    "0001-01-01",
        PurchaseType
    :
    null,
        PurchaseUser
    :
    "008409    ",
        Remark
    :
    null,
        SupplierAddress
    :
    "慈溪市周巷镇三江口村协同191号",
        SupplierEmail
    :
    "46158433@qq.com",
        SupplierFaxNo
    :
    "63498634",
        SupplierId
    :
    "D04004    ",
        SupplierName
    :
    "慈溪市周巷双溪橡胶制品厂",
        SupplierProperty
    :
    null,
        SupplierShortName
    :
    "双溪橡胶",
        SupplierTel
    :
    "63498634",
        SupplierUser
    :
    "袁晓春",
        UpperPurchaseDate
    :
    "2016-11-19",
    };

    var vmManager = {
        searchYear: new Date().getFullYear(),
        datasets: [],
        datasource:[item],
        getPurQualifiedSupplier: function () {
            $scope.searchPromise = supplierDataOpService.getPurQualifiedSupplierListBy(vmManager.searchYear).then(function (datas) {
                vmManager.datasource = datas;
            });
        },
        supplierCertificateEditModal: $modal({
            title: "供应商证书编辑", content: '',
            templateUrl: leeHelper.controllers.supplierManage + '/EditPurSupplierCertificateViewTpl/',
            controller: function ($scope) {
                //$scope.previewFileName = waittingAuditItem.DocumentPath;
                //$scope.auditMaterialBoard = function () {
                //    waittingAuditItem.OpSign = 'edit';
                //    boardDataOpService.auditMaterialBoardData(waittingAuditItem).then(function (opresult) {
                //        if (opresult.Result) {
                //            vmManager.editModal.$promise.then(vmManager.editModal.hide);
                //            leeHelper.remove(vmManager.editDatas, waittingAuditItem);
                //        }
                //    });
                //};
                var vmManager = {
                    fileList: [{id:1, fileName: '',certificateName:'', adding: true, uploadSuccess: false }, ],
                    addFile: function (item) {
                        item.adding = false;
                        var id = vmManager.fileList.length + 1;
                        vmManager.fileList.push({ id: id, fileName: '', certificateName: '', adding: true, uploadSuccess: false });
                       
                    },
                    getFile: function (fileItem) {
                        vmManager.uploadFileItem = _.clone(fileItem);
                    },
                    uploadFileItem:null,
                };
                $scope.vmManager = vmManager;
                ///选择文件并上传
                $scope.selectFile = function (el) {
                    var files = el.files;
                    if (files.length > 0) {
                        var file = files[0];
                        var fd = new FormData();
                        fd.append('file', file);
                        //上传证书文件
                        $scope.uploadPromie = supplierDataOpService.uploadPurSupplierCertificateFile(fd).then(function (result) {
                            if (result) {
                                var fileItem = _.find(vmManager.fileList, { id: vmManager.uploadFileItem.id });
                                if (!_.isUndefined(fileItem)) {
                                    fileItem.uploadSuccess = true;
                                    fileItem.fileName = file.name;
                                }
                            }
                        });
                    }
                };
            },
            show: false
        }),
        editSupplierCertificate: function (item) {

            vmManager.supplierCertificateEditModal.$promise.then(vmManager.supplierCertificateEditModal.show);
        },
    };

    $scope.vmManager = vmManager;
});

