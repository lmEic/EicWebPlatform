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
    purDb.uploadPurSupplierCertificateFile = function (files) {
        var url = purUrlPrefix + 'UploadPurSupplierCertificateFile';
        return ajaxService.uploadFile(url, files);
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
    purDb.getWaittingTourSupplier = function (yearQuarter, limitTotalCheckScore, limitQualityCheck) {
        var url = purUrlPrefix + 'GetWaittingTourSupplier';
        return ajaxService.getData(url, {
            yearQuarter: yearQuarter,
            limitTotalCheckScore: limitTotalCheckScore,
            limitQualityCheck: limitQualityCheck
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
    ///获取供应商稽核评份列表
    purDb.getPurSupGradeInfoList = function (supplierId, yearQuarter) {
        var url = purUrlPrefix + 'GetPurSupGradeInfoList';
        return ajaxService.getData(url, {
            supplierId: supplierId,
            yearQuarter: yearQuarter
        });
    };
    return purDb;
});

//供应商证书管理
purchaseModule.controller('buildQualifiedSupplierInventoryCtrl', function ($scope, supplierDataOpService) {

    $scope.vm = {
        PurchaseType: '',
        SupplierProperty: '',
        SupplierId: ''
    };

    var vmManager = $scope.vmManager = {
        searchYear: new Date().getFullYear(),
        filterSupplierId: null,
        certificateFileNames: [
            { id: 'ISO9001', text: 'ISO9001' },
            { id: 'ISO14001', text: 'ISO14001' },
            { id: '供应商评鉴表', text: '供应商评鉴表' },
            { id: '不使用童工申明', text: '不使用童工申明' },
            { id: '供应商环境调查表', text: '供应商环境调查表' },
            { id: '廉洁承诺书', text: '廉洁承诺书' },
            { id: 'PCN协议', text: 'PCN协议' },
            { id: '质量保证协议', text: '质量保证协议' },
            { id: 'HSF保证书', text: 'HSF保证书' },
            { id: 'REACH保证书', text: 'REACH保证书' },
            { id: 'SVHC调查表', text: 'SVHC调查表' },
            { id: '供应商基本资料表', text: '供应商基本资料表' }
        ],
        datasets: [],
        datasource: [],
        datasourceCopy: [],
        editWindowShow: false,
        goToEdit: function (item) {
            leeHelper.copyVm(item, $scope.vm);
            if (!vmManager.editWindowShow) {
                editManager.getCertificateDatas();
            }
            vmManager.editWindowShow = !vmManager.editWindowShow;
        },
        getPurQualifiedSupplier: function () {
            $scope.searchPromise = supplierDataOpService.getPurQualifiedSupplierListBy(vmManager.searchYear).then(function (datas) {
                vmManager.datasource = datas;
                vmManager.datasourceCopy = _.clone(datas);
            });
        },
        CreateQualifiedSupplierList: function () {
            $scope.searchPromise = supplierDataOpService.CreateQualifiedSupplierInfoList(vmManager.datasource).then(function () {

            });
        },
        editItem: null,
        editPurchaseTypeInfo: function (item) {
            item.purchaseTypeEditting = true;
            vmManager.editItem = item;
        },
        cancelEditPurchaseTypeInfo: function (item) {
            item.purchaseTypeEditting = false;
        },
        saveEditPurchaseTypeInfo: function (item) {
            item.OpSign = 'editPurchaseType';
            supplierDataOpService.storePurSupplierCertificateInfo(item).then(function (opresult) {
                if (opresult.Result) {
                    item.purchaseTypeEditting = false;
                }
            });
        },
        filterBySupplierId: function () {
            vmManager.datasource = _.clone(vmManager.datasourceCopy);
            if (vmManager.filterSupplierId !== null && vmManager.filterSupplierId.length > 0) {

                vmManager.datasource = _.clone(_.where(vmManager.datasource, { SupplierId: vmManager.filterSupplierId }));
            }
        }
    };
    //上传文件项目
    var uploadFileVM = $scope.fileItem = {
        EligibleCertificate: '',
        PurchaseType: '', SupplierProperty: '', SupplierId: null, FilePath: '',
        CertificateFileName: '', DateOfCertificate: null, OpSign: 'add', OpPerson: ''
    };
    var uploadFileVmCopy = _.clone(uploadFileVM);
    var editManager = $scope.editManager = {
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
            $scope.searchCertificatePromise = supplierDataOpService.getSupplierQualifiedCertificateListBy($scope.vm.SupplierId).then(function (datas) {
                editManager.certificateDatas = datas;
            });
        },
        ///下载文件
        loadFile: function (item) {
            var loadUrl = "/PurSupplierManage/LoadQualifiedCertificateFile?suppliserId=" + item.SupplierId + "&eligibleCertificate=" + item.EligibleCertificate;
            return loadUrl;
        },
        ///获取文件扩展名图标
        getFileExtentionIcon: function (item) {
            return leeHelper.getFileExtensionIcon(item.CertificateFileName);
        }
    };

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //保存供应商证书数据
    operate.savePurSupplierCertificateDatas = function (isValid) {
        vmManager.editItem.PurchaseType = $scope.vm.PurchaseType;
        vmManager.editItem.SupplierProperty = $scope.vm.SupplierProperty;
        operate.cancel();
    };
    operate.cancel = function () {
        editManager.certificateDatas = [];
        $scope.fileItem = uploadFileVM = _.clone(uploadFileVmCopy);
        vmManager.editWindowShow = false;
    };

    ///选择文件并上传
    $scope.selectFile = function (el) {
        if ($scope.fileItem.EligibleCertificate === null || $scope.fileItem.DateOfCertificate === null) {
            alert("证书类型或者证书日期不能为空!");
            return;
        }
        leeHelper.upoadFile(el, function (fd) {
            uploadFileVM.SupplierId = $scope.vm.SupplierId;
            var fileItem = uploadFileVM;
            var fileAttachData = { SupplierId: fileItem.SupplierId, EligibleCertificate: fileItem.EligibleCertificate };
            fd.append('fileAttachData', JSON.stringify(fileAttachData));
            $scope.uploadPromie = supplierDataOpService.uploadPurSupplierCertificateFile(fd).then(function (data) {
                if (data.Result === "OK") {
                    //更新文件模型数据
                    fileItem.CertificateFileName = data.FileName;
                    leeHelper.copyVm($scope.vm, fileItem);
                    fileItem.FilePath = "FileLibrary/PurSupplierCertificate/" + data.FileName;
                    fileItem.OpSign = leeDataHandler.dataOpMode.uploadFile;
                    leeHelper.setUserData(fileItem);
                    supplierDataOpService.storePurSupplierCertificateInfo(fileItem).then(function (opresult) {
                        if (opresult.Result) {
                            if (angular.isObject(opresult.Entity)) {
                                var item = _.findWhere(editManager.certificateDatas, { SupplierId: fileItem.SupplierId, EligibleCertificate: fileItem.EligibleCertificate });
                                if (item === undefined)
                                    editManager.certificateDatas.push(opresult.Entity);
                            }
                            console.log(opresult.Result);
                            $scope.fileItem = uploadFileVM = _.clone(uploadFileVmCopy);
                            alert("上传文件:" + fd.name + "成功！");
                        }
                    });
                }
            });
        });
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
        ParameterKey: null,
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
        leeHelper.setUserData(uiVM);
        crud.add(operate, isValid, function () {
            uiVM.TotalCheckScore = (uiVM.QualityCheck * 0.3 + uiVM.AuditPrice * 0.2 + uiVM.DeliveryDate * 0.15 + uiVM.ActionLiven * 0.15 + uiVM.HSFGrade * 0.2).toFixed(2);
            uiVM.ParameterKey = uiVM.SupplierId + "&&" + uiVM.SeasonDateNum;
            uiVM.OpSign = "add";
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
        limitTotalCheckScore: 80,
        limitQualityCheck: 90,
        //获取要考核的供应商数据列表
        getWaittingTourSupplier: function () {
            $scope.searchPromise = supplierDataOpService.getWaittingTourSupplier(vmManager.yearQuarter, vmManager.limitTotalCheckScore, vmManager.limitQualityCheck).then(function (datas) {
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
    var item = $scope.vm = {
        SupplierId: null,
        SupplierName: null,
        SupplierProperty: null,
        PurchaseType: null,
        PurchaseMaterial: null,
        ParameterKey: null,
        LastPurchaseDate: null,
        SupGradeType: null,
        FirstGradeScore: null,
        FirstGradeDate: null,
        SecondGradeScore: null,
        GradeYear: null,
        OpPerson: null,
        OpSign: null,
        OpDate: null,
        OpTime: null,
        Id_key: null,
        isEditting: false
    };
    var uiVm = $scope.vm;
    var operate = Object.create(leeDataHandler.operateStatus);
    var dialog = $scope.dialog = leePopups.dialog();
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };

    //视图管理器
    var vmManager = $scope.vmManager = {
        supplierId: '',
        editDatas: [item],
        supGradeInfoEditDatas: [],
        yearQuarter: '',
        dataSource: [],
        ///根据供应商编号查询供应商辅导数据信息
        ///详细列表
        editSupGradeInfoTable: function (item) {
            console.log(item);
            vmManager.editItem = $scope.vm = item;
            console.log(item.SupplierId);
            supplierDataOpService.getPurSupGradeInfoList(item.SupplierId, vmManager.yearQuarter).then(function (datas) {
                vmManager.supGradeInfoEditDatas = datas;
                dialog.show();
            });
        },

        /// 选择详细列表
        selectSupGradeInfoItem: function (item) {
            leeHelper.setUserData(uiVm);
            leeHelper.copyVm(item, uiVm);
            $scope.vm = uiVm;
            vmManager.supGradeEditModal.$promise.then(vmManager.supGradeEditModal.show);
        },
        //获取要考核的供应商数据列表
        getSupGradeInfo: function () {
            $scope.searchPromise = supplierDataOpService.getPurSupGradeInfo(vmManager.yearQuarter).then(function (datas) {
                vmManager.editDatas = datas;
                vmManager.dataSource = datas;
            });
        },

        editItem: null,
        deleteItem: null,
        editSupGradeInfo: function (item) {
            vmManager.editItem = $scope.vm = item;
            item.OpSign = leeDataHandler.dataOpMode.edit;
            dialog.close();
            vmManager.supGradeEditModal.$promise.then(vmManager.supGradeEditModal.show);
        },
        addSupGradeInfo: function (item) {
            vmManager.editItem = $scope.vm = item;
            leeHelper.setUserData(item);
            item.OpSign = leeDataHandler.dataOpMode.add;
            item.Id_Key = null;
            vmManager.supGradeEditModal.$promise.then(vmManager.supGradeEditModal.show);
        },
        deleteSupGradeInfo: function (item) {
            vmManager.deleteItem = $scope.vm = item;
            item.OpSign = leeDataHandler.dataOpMode.delete;
            vmManager.deleteModalWindow.$promise.then(vmManager.deleteModalWindow.show);
        },
        deleteModalWindow: $modal({
            title: "删除提示",
            content: "确认删除此信息吗？",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
            show: false,
            controller: function ($scope) {
                $scope.confirmDelete = function (item) {
                    supplierDataOpService.savePurSupGradeInfo(vmManager.deleteItem).then(function (opresult) {
                        leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                            if (opresult.Result) {
                                leeHelper.remove(vmManager.dataSource, vmManager.deleteItem);
                                var ds = _.clone(vmManager.dataSource);
                                leeHelper.remove(ds, vmManager.deleteItem);
                                vmManager.dataSource = ds;
                                vmManager.deleteModalWindow.$promise.then(vmManager.deleteModalWindow.hide);
                            }
                        });
                    }
                    );
                };
            }

        }),
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
                        $scope.vm.GradeYear = vmManager.yearQuarter.substring(0, 4);
                        $scope.vm.ParameterKey = vmManager.editItem.SupplierId + "&" + vmManager.editItem.GradeYear + "&" + vmManager.editItem.SupGradeType;
                        supplierDataOpService.savePurSupGradeInfo($scope.vm).then(function (opResult) {
                            if (opResult.Result) {
                                vmManager.editItem = $scope.vm;
                                vmManager.supGradeEditModal.$promise.then(vmManager.supGradeEditModal.hide);
                            }
                            else {
                                vmManager.supGradeEditModal.$promise.then(vmManager.supGradeEditModal.show);
                                leeDataHandler.dataOperate.displayMessage(operate, opresult);

                            }
                        });
                    });
                };
            },
            show: false
        })
    };
});

