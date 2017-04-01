/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />

var purchaseModule = angular.module('bpm.purchaseApp');
//获取供应商信息
purchaseModule.factory('supplierDataOpService', function (ajaxService) {
    var purDb = {};
    var purUrlPrefix = "/" + leeHelper.controllers.supplierManage + "/";
    //-------------------------供应商证书管理-------------------------------------
    //获取供应商信息
    purDb.getErpSuppplierInfoBy = function (supplierId) {
        var url = purUrlPrefix + 'GetErpSuppplierInfoBy';
        return ajaxService.getData(url, {
            supplierId: supplierId
        });
    };
    //根据年份获取合格供应商清单
    purDb.getPurQualifiedSupplierListBy = function (yearMonth) {
        var url = purUrlPrefix + 'GetPurQualifiedSupplierListBy';
        return ajaxService.getData(url, {
            yearMonth: yearMonth
        });
    };

    ////////////////////////////////////
    //        导出得到EXCEL            //
    //                                //
    ////////////////////////////////////
    purDb.CreateQualifiedSupplierInfoList = function (data) {
        var url = purUrlPrefix + 'CreateQualifiedSupplierInfoList';
        return ajaxService.getData(url, {
            data: data
        });
    };

    ///上传供应商证书文件
    purDb.uploadPurSupplierCertificateFile = function (file) {
        var url = purUrlPrefix + 'UploadPurSupplierCertificateFile';
        return ajaxService.uploadFile(url, file);
    };
    ///存储合格证书
    purDb.storePurSupplierCertificateInfo = function (certificateData) {
        var url = purUrlPrefix + 'StorePurSupplierCertificateInfo';
        return ajaxService.postData(url, {
            certificateData: certificateData
        });
    };
    ///获取合格供应商证书信息
    purDb.getSupplierQualifiedCertificateListBy = function (supplierId) {
        var url = purUrlPrefix + 'GetSupplierQualifiedCertificateListBy';
        return ajaxService.getData(url, {
            supplierId: supplierId
        });
    };
    ///删除供应商证书文件
    purDb.delPurSupplierCertificateFile = function (entity) {
        var url = purUrlPrefix + 'DelPurSupplierCertificateFile';
        return ajaxService.postData(url, {
            entity: entity
        });
    };
    //-------------------------供应商考核管理-------------------------------------
    //获取要考核的供应商列表
    purDb.getAuditSupplierList = function (season) {
        var url = purUrlPrefix + 'GetAuditSupplierList';
        return ajaxService.getData(url, {
            yearSeason: season
        });
    };
    //保存考核供应商信息
    purDb.saveAuditSupplierInfo = function (entity) {
        var url = purUrlPrefix + 'SaveAuditSupplierInfo';
        return ajaxService.postData(url, {
            entity: entity
        });
    };

    //-------------------------供应商辅导管理-------------------------------------
    //获取要辅导的供应商数据
    purDb.getWaittingTourSupplier = function (yearQuarter) {
        var url = purUrlPrefix + 'GetWaittingTourSupplier';
        return ajaxService.getData(url, {
            yearQuarter: yearQuarter
        });
    };
    ///保存供应商辅导信息
    purDb.savePurSupTourInfo = function (entity) {
        var url = purUrlPrefix + 'SavePurSupTourInfo';
        return ajaxService.postData(url, {
            entity: entity
        });
    };
    //-------------------------供应商评分管理-------------------------------------
    purDb.getPurSupGradeInfo = function (yearQuarter) {
        var url = purUrlPrefix + 'GetPurSupGradeInfo';
        return ajaxService.getData(url, {
            yearQuarter: yearQuarter
        });
    };
    ///保存供应商评分数据
    purDb.savePurSupGradeInfo = function (entity) {
        var url = purUrlPrefix + 'SavePurSupGradeData';
        return ajaxService.postData(url, {
            entity: entity
        });
    };
    ///获取供应商数据列表
    purDb.getPurSupplierDataList = function (supplierId, dataType) {
        var url = purUrlPrefix + 'GetPurSupplierDataList';
        return ajaxService.getData(url, {
            supplierId: supplierId,
            dataType: dataType
        });
    };
    return purDb;
});

//供应商证书管理
purchaseModule.controller('buildQualifiedSupplierInventoryCtrl', function ($scope, supplierDataOpService, $modal) {

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
    "2016-11-19"
    };

    $scope.vm = {
        PurchaseType: '',
        SupplierProperty: '',
        SupplierId: ''
    };

    var vmManager = $scope.vmManager = {
        searchYear: new Date().getFullYear(),
        datasets: [],
        datasource: [item],
        editWindowShow: false,
        goToEdit: function (item) {
            vmManager.editItem = item;
            if (!vmManager.editWindowShow) {
                leeHelper.copyVm(item, $scope.vm);
                editManager.getCertificateDatas();
            }
            vmManager.editWindowShow = !vmManager.editWindowShow;
        },
        getPurQualifiedSupplier: function () {
            $scope.searchPromise = supplierDataOpService.getPurQualifiedSupplierListBy(vmManager.searchYear).then(function (datas) {
                vmManager.datasource = datas;
            });
        },
        CreateQualifiedSupplierList: function () {
            $scope.searchPromise = supplierDataOpService.CreateQualifiedSupplierInfoList(vmManager.datasource).then(function () {

            });
        },
        editItem: null,
    };
    //上传文件项目
    var uploadFileVM = {
        id: 1, EligibleCertificate: '', adding: true, uploadSuccess: false,
        PurchaseType: '', SupplierProperty: '', SupplierId: '', FilePath: '',
        CertificateFileName: '', DateOfCertificate: null, OpSign: 'add',
    };
    var editManager = $scope.editManager = {
        fileList: [_.clone(uploadFileVM)],
        //新上传文件
        addFile: function (item) {
            item.adding = false;
            var newItem = _.clone(uploadFileVM);
            newItem.id = editManager.fileList.length + 1;
            editManager.fileList.push(newItem);
        },
        getFile: function (fileItem) {
            editManager.uploadFileItem = _.clone(fileItem);
        },
        uploadFileItem: null,
        //删除证书文件
        removeCertificateFile: function (item) {
            item.OpSign = leeDataHandler.dataOpMode.deleteFile;
            supplierDataOpService.delPurSupplierCertificateFile(item).then(function (opResult) {
                if (opResult.Result) {
                    leeHelper.remove(editManager.certificateDatas, item);
                }
                alert(opResult.Message);
            });
        },
        //证书数据
        certificateDatas: [],
        getCertificateDatas: function () {
            editManager.certificateDatas = [];
            $scope.searchCertificatePromise = supplierDataOpService.getSupplierQualifiedCertificateListBy(vmManager.editItem.SupplierId).then(function (datas) {
                editManager.certificateDatas = datas;
            });
        }
    }

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //保存供应商证书数据
    operate.savePurSupplierCertificateDatas = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            supplierDataOpService.storePurSupplierCertificateInfo(editManager.fileList).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        vmManager.editItem.PurchaseType = $scope.vm.PurchaseType;
                        vmManager.editItem.SupplierProperty = $scope.vm.SupplierProperty;
                        editManager.fileList = [];
                        vmManager.editWindowShow = false;
                    }
                })
            });
        });
    };

    ///选择文件并上传
    $scope.selectFile = function (el) {
        leeHelper.upoadFile(el, function (fd) {
            $scope.uploadPromie = supplierDataOpService.uploadPurSupplierCertificateFile(fd).then(function (result) {
                if (result === "OK") {
                    var fileItem = _.find(editManager.fileList, { id: editManager.uploadFileItem.id });
                    if (!_.isUndefined(fileItem)) {
                        //更新文件模型数据
                        var year = new Date().getFullYear();
                        fileItem.uploadSuccess = true;
                        fileItem.CertificateFileName = fd.name;
                        leeHelper.copyVm($scope.vm, fileItem);
                        fileItem.FilePath = "FileLibrary/PurSupplierCertificate/" + year + "/" + fd.name;
                        fileItem.OpSign = leeDataHandler.dataOpMode.uploadFile;
                        supplierDataOpService.storePurSupplierCertificateInfo(fileItem).then(function (opresult) {
                            if (opresult.Result) {
                                alert("上传成功");
                            }
                        });
                    }
                }
            });
        })
    };
});
//供应商考核管理
purchaseModule.controller('supplierEvaluationManageCtrl', function ($scope, supplierDataOpService, $modal) {
    var item = {
        SupplierId: 'D10069',
        SupplierShortName: '双溪橡胶',
        SupplierName: '双溪橡胶有限公司',
        QualityCheck: null,
        AuditPrice: null,
        DeliveryDate: null,
        ActionLiven: null,
        HSFGrade: null,
        TotalCheckScore: 0,
        CheckLevel: null,
        RewardsWay: null,
        MaterialGrade: null,
        ManagerRisk: null,
        SubstitutionSupplierId: null,
        SeasonNum: 0,
        Remark: null,
        OpPserson: null,
        OpDate: null,
        Optime: null,
        OpSign: null,
        Id_key: null,
        isEditting: false
    };

    ///供应商考核视图模型
    var uiVM = $scope.vm = {
        SupplierId: null,
        SupplierShortName: null,
        SupplierName: null,
        QualityCheck: null,
        AuditPrice: null,
        DeliveryDate: null,
        ActionLiven: null,
        HSFGrade: null,
        TotalCheckScore: null,
        CheckLevel: null,
        RewardsWay: null,
        MaterialGrade: null,
        ManagerRisk: null,
        SubstitutionSupplierId: null,
        SeasonNum: 0,
        Remark: null,
        OpPserson: null,
        OpDate: null,
        Optime: null,
        OpSign: 'add',
        Id_key: null
    };

    var initVm = _.clone(uiVM);

    //操作部分
    var operate = $scope.operate = Object.create(leeDataHandler.dataOperate);
    //数据操作
    var crud = leeDataHandler.dataOperate;

    $scope.operate = operate;
    operate.save = function (isValid) {
        crud.add(operate, isValid, function () {
            uiVM.TotalCheckScore = uiVM.QualityCheck * 0.3 + uiVM.AuditPrice * 0.2 + uiVM.DeliveryDate * 0.15 + uiVM.ActionLiven * 0.15 + uiVM.HSFGrade * 0.2;
            $scope.promise = supplierDataOpService.saveAuditSupplierInfo($scope.vm).then(function (opResult) {
                crud.handleSuccessResult(operate, opResult, function () {
                    leeHelper.copyVm($scope.vm, vmManager.editItem);
                    vmManager.init();
                });
            });
        });
    };
    operate.refresh = function () {
        crud.refresh(operate, function () {
            vmManager.init();
        });
    };


    //视图管理器
    var vmManager = $scope.vmManager = {
        init: function () {
            $scope.vm = uiVM = _.clone(initVm);
        },
        editDatas: [item],
        yearQuarter: '',
        //获取要考核的供应商数据列表
        getAuditSupplierDatas: function () {
            $scope.promise = supplierDataOpService.getAuditSupplierList(vmManager.yearQuarter).then(function (datas) {
                vmManager.editDatas = datas;
            });
        },
        editItem: null,
        displayEditForm: false,
        editSupplierAuditData: function (item) {
            vmManager.displayEditForm = true;
            vmManager.editItem = $scope.vm = uiVM = item;
        }
    };


});
//供应商辅导管理
purchaseModule.controller('supplierToturManageCtrl', function ($scope, supplierDataOpService, $modal) {
    var item = {
        SupplierId: 'D10069',
        SupplierShortName: '双溪橡胶',
        SupplierName: null,
        QualityCheck: null,
        AuditPrice: null,
        DeliveryDate: null,
        ActionLiven: null,
        HSFGrade: null,
        TotalCheckScore: null,
        CheckLevel: null,
        RewardsWay: null,
        MaterialGrade: null,
        ManagerRisk: null,
        SeasonNum: 0,
        PlanTutorDate: '2016-12-12',
        PlanTutorContent: 'hhassdf',
        ActionTutorDate: null,
        ActionTutorContent: null,
        TutorResult: null,
        QualityCheckProperty: null,
        Remark: null,
        YearMonth: null,
        OpPserson: null,
        OpDate: null,
        Optime: null,
        OpSign: null,
        Id_key: null,
        isEditting: false
    };

    ///供应商辅导视图模型
    var uiVM = $scope.vm = {
        SuppilerShortName: null,
        SupplierId: null,
        SupplierName: null,
        QualityCheck: null,
        AuditPrice: null,
        DeliveryDate: null,
        ActionLiven: null,
        HSFGrade: null,
        TotalCheckScore: null,
        CheckLevel: null,
        RewardsWay: null,
        MaterialGrade: null,
        ManagerRisk: null,
        SeasonNum: 0,
        PlanTutorDate: '2016-12-12',
        PlanTutorContent: 'hhassdf',
        ActionTutorDate: null,
        ActionTutorContent: null,
        TutorResult: null,
        QualityCheckProperty: null,
        Remark: null,
        YearMonth: null,
        OpPserson: null,
        OpDate: null,
        Optime: null,
        OpSign: null,
        Id_key: null
    };

    var initVm = _.clone(uiVM);


    //视图管理器
    var vmManager = $scope.vmManager = {
        supplierId: null,
        editDatas: [item],
        yearQuarter: '',
        //获取要考核的供应商数据列表
        getWaittingTourSupplier: function () {
            $scope.searchPromise = supplierDataOpService.getWaittingTourSupplier(vmManager.yearQuarter).then(function (datas) {
                vmManager.editDatas = datas;
            });
        },
        editItem: null,
        displayEditForm: false,
        editSupplierToturInfo: function (item) {
            vmManager.displayEditForm = true;
            vmManager.editItem = item;
            vmManager.supTourEditModal.$promise.then(vmManager.supTourEditModal.show);
        },
        ///根据供应商编号查询供应商辅导数据信息
        searchBySupplierId: function (dataType) {
            vmManager.editDatas = supplierDataOpService.getPurSupplierDataList(vmManager.supplierId, dataType).then(function (datas) {
                vmManager.editDatas = datas;
            });
        },
        supTourEditModal: $modal({
            title: '供应商辅导信息编辑', content: '',
            templateUrl: leeHelper.controllers.supplierManage + '/EditPurSupplierToturViewTpl/',
            controller: function ($scope) {
                $scope.edittingItem = _.clone(vmManager.editItem);
                var editItem = $scope.vm = vmManager.editItem;
                var crud = leeDataHandler.dataOperate;
                var operate = $scope.operate = Object.create(leeDataHandler.dataOperate);
                //保存供应商辅导信息
                operate.savePurSupTurDatas = function (isValid) {
                    crud.add(operate, isValid, function () {
                        supplierDataOpService.savePurSupTourInfo($scope.vm).then(function (opResult) {
                            if (opResult) {
                                vmManager.editItem = $scope.vm;
                                vmManager.supTourEditModal.$promise.then(vmManager.supTourEditModal.hide);
                            }
                        });
                    });
                };

            },
            show: false
        })
    };


    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };


});
//供应商稽核评分
purchaseModule.controller('supplierAuditToGradeCtrl', function ($scope, supplierDataOpService, $modal) {
    var item = {
        SupplierId: 'D10069',
        SupplierName: '双溪橡胶',
        SupplierProperty: null,
        PurchaseType: null,
        PurchaseMaterial: null,
        LastPurchaseDate: null,
        SupGradeType: null,
        FirstGradeScore: null,
        FirstGradeDate: null,
        SecondGradeScore: null,
        OpPerson: null,
        OpSign: null,
        OpDate: null,
        OpTime: null,
        Id_key: null,
        isEditting: false
    };

    ///供应商考核视图模型
    var uiVM = $scope.vm = {
        SupplierId: null,
        SupplierName: null,
        QualityCheck: null,
        AuditPrice: null,
        DeliveryDate: null,
        ActionLiven: null,
        HSFGrade: null,
        TotalCheckScore: null,
        CheckLevel: null,
        RewardsWay: null,
        MaterialGrade: null,
        ManagerRisk: null,
        SubstitutionSupplierId: null,
        SeasonNum: 0,
        Remark: null,
        OpPserson: null,
        OpDate: null,
        Optime: null,
        OpSign: null,
        Id_key: null
    };

    var initVm = _.clone(uiVM);

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };

    //视图管理器
    var vmManager = $scope.vmManager = {
        supplierId: '',
        editDatas: [item],
        yearQuarter: '',
        ///根据供应商编号查询供应商辅导数据信息
        searchBySupplierId: function (dataType) {
            vmManager.editDatas = supplierDataOpService.getPurSupplierDataList(vmManager.supplierId, dataType).then(function (datas) {
                vmManager.editDatas = datas;
            });
        },
        //获取要考核的供应商数据列表
        getSupGradeInfo: function () {
            $scope.promise = supplierDataOpService.getPurSupGradeInfo(vmManager.yearQuarter).then(function (datas) {
                vmManager.editDatas = datas;
            });
        },
        editItem: null,
        editSupGradeInfo: function (item) {
            vmManager.editItem = $scope.vm = item;
            vmManager.supGradeEditModal.$promise.then(vmManager.supGradeEditModal.show);
        },
        supGradeEditModal: $modal({
            title: '新增供应商评分信息', content: '',
            templateUrl: leeHelper.controllers.supplierManage + '/EditPurSupAuditToGradeTpl/',
            controller: function ($scope) {
                var editItem = $scope.vm = vmManager.editItem;
                $scope.gradeTypes = [{ id: '供应商系统稽核评估', text: '供应商系统稽核评估' },
                                    { id: '供应商产品无有害物质系统稽核评估', text: '供应商产品无有害物质系统稽核评估' },
                                    { id: '系统评估表-针对小供应商', text: '系统评估表-针对小供应商' }];

                var crud = leeDataHandler.dataOperate;
                var operate = $scope.operate = Object.create(leeDataHandler.dataOperate);
                //保存供应商辅导信息
                operate.savePurSupGradeDatas = function (isValid) {
                    crud.add(operate, isValid, function () {
                        supplierDataOpService.savePurSupGradeInfo($scope.vm).then(function (opResult) {
                            if (opResult) {
                                vmManager.editItem = $scope.vm;
                                vmManager.supGradeEditModal.$promise.then(vmManager.supGradeEditModal.hide);
                            }
                        });
                    });
                };
            },
            show: false
        })
    };
});

