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
    ///存储合格证书
    purDb.storePurSupplierCertificateInfo = function (certificateDatas) {
        var url = purUrlPrefix + 'StorePurSupplierCertificateInfo';
        return ajaxService.postData(url, {
            certificateDatas: certificateDatas,
        });
    };
    ///获取合格供应商证书信息
    purDb.getSupplierQualifiedCertificateListBy = function (supplierId)
    {
        var url = purUrlPrefix + 'GetSupplierQualifiedCertificateListBy';
        return ajaxService.getData(url, {
            supplierId: supplierId,
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
    "D10069",
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

    var vmManager = $scope.vmManager = {
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
                var editUiVM = {
                    PurchaseType: '',
                    SupplierProperty: '',
                    SupplierId: ''
                };
                leeHelper.copyVm(vmManager.editItem, editUiVM);

                $scope.vm = editUiVM;

                //保存供应商证书数据
                $scope.savePurSupplierCertificateDatas = function (isValid) {
                    if (isValid) {
                        //添加数据
                        supplierDataOpService.storePurSupplierCertificateInfo(editManager.fileList).then(function (opresult) {
                            if (opresult.Result) {
                                vmManager.supplierCertificateEditModal.$promise.then(vmManager.supplierCertificateEditModal.hide);
                                vmManager.editItem.PurchaseType = $scope.vm.PurchaseType;
                                vmManager.editItem.SupplierProperty = $scope.vm.SupplierProperty;
                                editManager.fileList = [];
                            }
                        });
                    }
                };
                //上传文件项目
                var uploadFileItem = {
                    id: 1,EligibleCertificate: '', adding: true, uploadSuccess: false,
                    PurchaseType: '', SupplierProperty: '', SupplierId: '', FilePath: '',
                    CertificateFileName: '', DateOfCertificate: null,
                };

                var editManager = {
                    fileList: [_.clone(uploadFileItem)],
                    //新上传文件
                    addFile: function (item) {
                        item.adding = false;
                        var newItem = _.clone(uploadFileItem);
                        newItem.id = editManager.fileList.length + 1;
                        editManager.fileList.push(newItem);
                    },
                    getFile: function (fileItem) {
                        editManager.uploadFileItem = _.clone(fileItem);
                    },
                    uploadFileItem: null,
                    //证书数据
                    certificateDatas:[],
                    getCertificateDatas: function () {
                        editManager.certificateDatas = [];
                        $scope.searchPromise = supplierDataOpService.getSupplierQualifiedCertificateListBy(vmManager.editItem.SupplierId).then(function (datas) {
                            editManager.certificateDatas = datas;
                        });
                    },
                };
                $scope.vmManager = editManager;
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
                                var fileItem = _.find(editManager.fileList, { id: editManager.uploadFileItem.id });
                                if (!_.isUndefined(fileItem)) {
                                    //更新文件模型数据
                                    var year = new Date().getFullYear();
                                    fileItem.uploadSuccess = true;
                                    fileItem.CertificateFileName = file.name;
                                    leeHelper.copyVm(editUiVM, fileItem);
                                    fileItem.FilePath = "FileLibrary/PurSupplierCertificate/" + year + "/" + file.name;
                                }
                            }
                        });
                    }
                };

                //提取数据
                editManager.getCertificateDatas();
            },
            show: false
        }),
        editItem:null,
        editSupplierCertificate: function (item) {
            vmManager.editItem = item;
            vmManager.supplierCertificateEditModal.$promise.then(vmManager.supplierCertificateEditModal.show);
        },
    };
});

