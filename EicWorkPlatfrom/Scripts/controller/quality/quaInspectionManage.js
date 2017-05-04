
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');
//数据访问工厂
qualityModule.factory("qualityInspectionDataOpService", function (ajaxService) {
    var quality = {};
    var quaInspectionManageUrl = "/quaInspectionManage/";
    ///////////////////////////////////////////////////iqc检验项目配置模块////////////////////////////////////////
    //iqc检验项目配置模块 物料查询
    quality.getIqcspectionItemConfigDatas = function (materialId) {
        var url = quaInspectionManageUrl + "GetIqcspectionItemConfigDatas";
        return ajaxService.getData(url, {
            materialId: materialId
        });
    };
    ///iqc检验项目配置模块 物料复制时是否存在
    quality.checkIqcspectionItemConfigMaterialId = function (materialId) {
        var url = quaInspectionManageUrl + "CheckIqcspectionItemConfigMaterialId";
        return ajaxService.getData(url, {
            materialId: materialId
        });
    };
    //iqc检验项目配置模块  导入Excel
    quality.importIqcInspectionItemConfigDatas = function (file) {
        var url = quaInspectionManageUrl + 'ImportIqcInspectionItemConfigDatas';
        return ajaxService.uploadFile(url, file);
    };
    //iqc检验项目配置模块  保存
    quality.saveIqcInspectionItemConfigDatas = function (iqcInspectionConfigItems) {
        var url = quaInspectionManageUrl + 'SaveIqcInspectionItemConfigDatas';
        return ajaxService.postData(url, {
            iqcInspectionConfigItems: iqcInspectionConfigItems
        });
    };
    // iqc检验项目配置模块 删除
    quality.deleteIqlInspectionConfigItem = function (configItem) {
        var url = quaInspectionManageUrl + 'DeleteIqlInspectionConfigItem';
        return ajaxService.postData(url, {
            configItem: configItem,
        });
    };

    ///////////////////////////////////////////////////检验项目转换配置模块////////////////////////////////////////
    //检验项目转换配置模块  获得数据
    quality.getModeSwitchDatas = function (inspectionModeType) {
        var url = quaInspectionManageUrl + "GetModeSwitchDatas";
        return ajaxService.getData(url, {
            inspectionModeType: inspectionModeType
        });
    };
    //检验项目转换配置模块  保存数据
    quality.saveModeSwitchDatas = function (inspectionModeType, switchModeList) {
        var url = quaInspectionManageUrl + "SaveModeSwitchDatas";
        return ajaxService.postData(url, {
            inspectionModeType: inspectionModeType,
            switchModeList: switchModeList
        });
    };

    ////////////////////////////////////////////////iqc检验方式配置模块////////////////////////////////////
    //检验方式配置模块      获取检验水平数据
    quality.getInspectionLevelValues = function (inspectionMode) {
        var url = quaInspectionManageUrl + "GetInspectionLevelValues";
        return ajaxService.postData(url, {
            inspectionMode: inspectionMode
        });
    };
    //检验方式配置模块      获取AQL数据
    quality.getInspectionAQLValues = function (inspectionMode, inspectionLevel) {
        var url = quaInspectionManageUrl + "GetInspectionAQLValues";
        return ajaxService.postData(url, {
            inspectionLevel: inspectionLevel,
            inspectionMode: inspectionMode
        });
    };
    //检验方式配置模块      处理'检验方式'配置数据
    quality.storeIqcInspectionModeData = function (inspectionModeConfigEntity) {
        var url = quaInspectionManageUrl + "StoreInspectionModeConfigData";
        return ajaxService.postData(url, {
            inspectionModeConfigEntity: inspectionModeConfigEntity
        });
    };
    //检验方式配置模块      获取“检验方式配置数据”
    quality.getIqcInspectionModeDatas = function (inspectionMode, inspectionLevel, inspectionAQL) {
        var url = quaInspectionManageUrl + "GetIqcInspectionModeDatas";
        return ajaxService.getData(url, {
            inspectionMode: inspectionMode,
            inspectionLevel: inspectionLevel,
            inspectionAQL: inspectionAQL
        });
    };

    ////////////////////////////////////////////fqc数据采集控制器////////////////////////////////////////
    //fqc数据采集控制器    获得检验项目数据
    quality.getFqcInspectionItemDataSummaryLabelList = function (orderId, materialId) {
        var url = quaInspectionManageUrl + "GetFqcInspectionItemDataSummaryLabelList";
        return ajaxService.getData(url, {
            orderId: orderId,
            materialId: materialId
        });
    };
    quality.getFqcOrderInfoDatas = function (orderId) {
        var url = quaInspectionManageUrl + "GetFqcOrderInfoDatas";
        return ajaxService.getData(url, {
            orderId: orderId
        })
    }
    quality.uploadFqcGatherDataAttachFile = function (file) {
        var url = quaInspectionManageUrl + 'UploadFqcGatherDataAttachFile';
        return ajaxService.uploadFile(url, file);
    };
    quality.storeFqcInspectionGatherDatas = function (gatherData) {
        var url = quaInspectionManageUrl + 'StoreFqcInspectionGatherDatas';
        return ajaxService.postData(url, {
            gatherData: gatherData,
        });
    };


    ////////////////////////////////////////////iqc进料检验数据采集模块//////////////////////////////////////////////
    quality.getInspectionDataGatherMaterialIdDatas = function (orderId) {
        var url = quaInspectionManageUrl + "GetIqcMaterialInfoDatas";
        return ajaxService.getData(url, {
            orderId: orderId
        })
    }
    //iqc进料检验数据采集模块     获得检验项目数据
    quality.getIqcInspectionItemDataSummaryLabelList = function (orderId, materialId) {
        var url = quaInspectionManageUrl + "GetIqcInspectionItemDataSummaryLabelList";
        return ajaxService.getData(url, {
            orderId: orderId,
            materialId: materialId
        })
    }
    //iqc进料检验数据采集模块     保存采集数据
    quality.storeIqcInspectionGatherDatas = function (gatherData) {
        var url = quaInspectionManageUrl + 'StoreIqcInspectionGatherDatas';
        return ajaxService.postData(url, {
            gatherData: gatherData,
        });
    };
    //iqc进料检验数据采集模块     上传iqc采集数据附件
    quality.uploadIqcGatherDataAttachFile = function (file) {
        var url = quaInspectionManageUrl + 'UploadIqcGatherDataAttachFile';
        return ajaxService.uploadFile(url, file);
    };



    ///////////////////////////////////////////iqc检验单管理模块///////////////////////////////////////////////
    //iqc检验单管理模块    获取表单数据
    quality.getInspectionFormManageOfIqcDatas = function (formStatus, dateFrom, dateTo) {
        var url = quaInspectionManageUrl + 'GetInspectionFormManageOfIqcDatas';
        return ajaxService.getData(url, {
            formStatus: formStatus,
            dateFrom: dateFrom,
            dateTo: dateTo
        })
    };
    //iqc检验单管理模块    获取详细数据
    quality.getInspectionFormDetailOfIqcDatas = function (orderId, materialId) {
        var url = quaInspectionManageUrl + "GetInspectionFormDetailOfIqcDatas";
        return ajaxService.getData(url, {
            orderId: orderId,
            materialId: materialId
        })
    }
    //iqc检验单管理模块    发送审核数据
    quality.postInspectionFormManageCheckedOfIqcData = function (model) {
        var url = quaInspectionManageUrl + "PostInspectionFormManageCheckedOfIqcData";
        return ajaxService.postData(url, {
            model: model
        })
    }

    ////////////////////////////////////////////fqc进料检验数据采集模块//////////////////////////////////////////////////
    //fqc进料检验数据采集模块   获取工单信息数据
    quality.getFqcOrderInfoDatas = function (orderId) {
        var url = quaInspectionManageUrl + 'GetFqcOrderInfoDatas';
        return ajaxService.getData(url, {
            orderId: orderId,
        });
    };
    //fqc进料检验数据采集模块   创建抽样表单项目
    quality.createFqcSampleFormItem = function (orderId, sampleCount) {
        var url = quaInspectionManageUrl + 'CreateFqcSampleFormItem';
        return ajaxService.getData(url, {
            orderId: orderId,
            sampleCount: sampleCount,
        });
    };
    //fqc进料检验数据采集模块   获取抽检表单项目
    quality.getFqcSampleFormItems = function (orderId, orderIdNumber) {
        var url = quaInspectionManageUrl + 'GetFqcSampleFormItems';
        return ajaxService.getData(url, {
            orderId: orderId,
            orderIdNumber: orderIdNumber,
        });
    };
    //fqc进料检验数据采集模块   保存采集数据
    quality.storeFqcInspectionGatherDatas = function (gatherData) {
        var url = quaInspectionManageUrl + 'StoreFqcInspectionGatherDatas';
        return ajaxService.postData(url, {
            gatherData: gatherData
        });
    };
    //fqc进料检验数据采集模块   上传采集数据附件
    quality.uploadFqcGatherDataAttachFile = function (file) {
        var url = quaInspectionManageUrl + 'UploadFqcGatherDataAttachFile';
        return ajaxService.uploadFile(url, file);
    };
    ////////////////////////////////////////////fqc检验项目配置模块////////////////////////////////////////////
    quality.getfqcInspectionItemConfigDatas = function (materialId) {
        var url = quaInspectionManageUrl + "GetFqcInspectionItemConfigDatas";
        return ajaxService.getData(url, {
            materialId: materialId
        })
    };
    ///fqc检验项目配置模块  物料复制时是否存在
    quality.checkFqcInspectionItemConfigMaterialId = function (materialId) {
        var url = quaInspectionManageUrl + "CheckFqcInspectionItemConfigMaterialId";
        return ajaxService.getData(url, {
            materialId: materialId
        })
    };
    //fqc检验项目配置模块   导入Excel
    quality.importfqcInspectionItemConfigDatas = function (file, para) {
        var url = quaInspectionManageUrl + 'ImportFqcInspectionItemConfigDatas';
        return ajaxService.uploadFile(url, { file: file, para: para });
    }

    //fqc检验项目配置模块  保存
    quality.saveFqcInspectionItemConfigDatas = function (fqcInspectionConfigItems) {
        var url = quaInspectionManageUrl + 'SaveFqcInspectionItemConfigDatas';
        return ajaxService.postData(url, {
            fqcInspectionConfigItems: fqcInspectionConfigItems
        })
    }
    // fqc检验项目配置模块  删除
    quality.deleteFqcInspectionConfigItem = function (configItem) {
        var url = quaInspectionManageUrl + 'DeleteFqcInspectionConfigItem';
        return ajaxService.postData(url, {
            configItem: configItem,
        });
    };

    //////////////////////////////////////////////fqc检验单管理模块/////////////////////////////////////////////
    //fqc检验单管理模块    获取表单数据
    quality.getInspectionFormManageOfFqcDatas = function (formStatus, dateFrom, dateTo) {
        var url = quaInspectionManageUrl + 'GetInspectionFormManageOfFqcDatas';
        return ajaxService.getData(url, {
            formStatus: formStatus,
            dateFrom: dateFrom,
            dateTo: dateTo
        })
    };
    //iqc检验单管理模块    获取详细数据
    quality.getInspectionFormDetailOfFqcDatas = function (orderId, orderIdNumber) {
        var url = quaInspectionManageUrl + "GetInspectionFormDetailOfFqcDatas";
        return ajaxService.getData(url, {
            orderId: orderId,
            orderIdNumber: orderIdNumber
        })
    }
    //iqc检验单管理模块    发送审核数据
    quality.postInspectionFormManageCheckedOfFqcData = function (model) {
        var url = quaInspectionManageUrl + "PostInspectionFormManageCheckedOfFqcData";
        return ajaxService.postData(url, {
            model: model
        })
    }
    return quality;
    ///////////////////////////////////////////////////////////////////////////////////////
})

//iqc检验项目配置模块
qualityModule.controller("iqcInspectionItemCtrl", function ($scope, qualityInspectionDataOpService, $modal) {
    leeHelper.setWebSiteTitle("质量管理", "IQC检验项目配置");
    var uiVM = {
        //表单变量
        MaterialId: null,
        InspectionItem: null,
        InspectionItemIndex: null,
        SizeUSL: null,
        SizeLSL: null,
        SizeMemo: null,
        EquipmentID: null,
        InspectionMethod: null,
        InspectionMode: null,
        InspectionLevel: null,
        InspectionAQL: null,
        InspectionDataGatherType: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: "add",
        Id_key: 0,
    }
    //表头变量
    var tableVM = {
        MaterialName: null,
        MaterialBelongDepartment: null,
        MaterialSpecify: null,
        MaterialrawID: null,
    }
    $scope.tableVm = tableVM;
    $scope.vm = uiVM;
    var initVM = _.clone(uiVM);
    var vmManager = {
        insert: false,
        inspectionMode: [{ id: "正常", text: "正常" }, { id: "加严", text: "加严" }, { id: "放宽", text: "放宽" }],
        InspectionDataGatherTypes: [{ id: "A", text: "A" }, { id: "B", text: "B" }, { id: "C", text: "C" }, { id: "D", text: "D" }, { id: "E", text: "E" }, { id: "F", text: "F" }],
        dataSource: [],
        dataSets: [],
        copyLotWindowDisplay: false,
        targetMaterialId: null,
        delItem: null,
        init: function () {
            if (uiVM.OpSign === 'add') {
                leeHelper.clearVM(uiVM, ["MaterialId"]);
            }
            else {
                uiVM = _.clone(initVM);
            }
            uiVM.OpSign = 'add';
            $scope.vm = uiVM;
        },
        ///查询得到配置数据
        getConfigDatas: function () {
            $scope.searchPromise = qualityInspectionDataOpService.getIqcspectionItemConfigDatas($scope.vm.MaterialId).then(function (datas) {
                if (datas !== null) {
                    $scope.tableVm = datas.ProductMaterailModel;
                    vmManager.dataSource = datas.InspectionItemConfigModelList;
                }
            });
        },
        //获取进料检验项目最大配置工序ID
        getInspectionIndex: function () {
            if (vmManager.dataSource.length > 0) {
                uiVM.InspectionItemIndex = $scope.vm.InspectionItemIndex = vmManager.dataSource.length + 1;
            }
            else {
                $scope.vm.InspectionItemIndex = 0;
            }
        },
        //显示批量复制操作窗口
        showCopyLotWindow: function () {
            vmManager.copyLotWindowDisplay = true;
        },
        //批量复制
        copyAll: function () {
            qualityInspectionDataOpService.checkIqcspectionItemConfigMaterialId(vmManager.targetMaterialId).then(function (opresult) {
                if (opresult.Result) {
                    alert(vmManager.targetMaterialId + "已经存在")
                } else {
                    angular.forEach(vmManager.dataSource, function (item) {
                        item.Id_key = null;
                        item.MaterialId = vmManager.targetMaterialId;
                    });
                }

            })
        },
        delModal: $modal({
            title: "删除提示",
            content: "你确定要删除此数据吗?",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
            controller: function ($scope) {
                $scope.confirmDelete = function () {
                    $scope.opPromise = qualityInspectionDataOpService.deleteIqlInspectionConfigItem(vmManager.delItem).then(function (opresult) {
                        leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                            if (opresult.Result || vmManager.delItem.Id_key == null) {
                                leeHelper.remove(vmManager.dataSets, vmManager.delItem);
                                var ds = _.clone(vmManager.dataSource);
                                leeHelper.remove(ds, vmManager.delItem);
                                vmManager.dataSource = ds;
                            }
                            vmManager.delModal.$promise.then(vmManager.delModal.hide);
                        });
                    });

                };

            },
            show: false,
        }),
        edittingRowIndex: 0,
    }
    //导入excel
    $scope.selectFile = function (el) {
        leeHelper.upoadFile(el, function (fd) {
            qualityInspectionDataOpService.importIqcInspectionItemConfigDatas(fd).then(function (datas) {
                vmManager.dataSource = datas;
            });
        })
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //确认
    operate.confirm = function (isValid) {
        leeHelper.setUserData(uiVM);
        var dataItem = _.clone(uiVM);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            if (uiVM.OpSign === "add") {
                vmManager.getInspectionIndex();
                var ds = _.clone(vmManager.dataSource);
                ds.push(dataItem);
                vmManager.dataSource = ds;
            }
            operate.refresh();
        })
    };

    //添加插入一项数据
    operate.copyItem = function (item) {
        var oldItem = _.clone(item);
        uiVM = oldItem;
        vmManager.getInspectionIndex();
        uiVM.OpSign = "copy";
        $scope.vm = uiVM;
        var dataItem = _.clone(uiVM);
        var ds = _.clone(vmManager.dataSource);
        ds.push(dataItem);
        vmManager.dataSource = ds;

    };
    //编辑
    operate.editItem = function (item) {
        uiVM = item;
        uiVM.OpSign = "edit";
        $scope.vm = uiVM;
    };
    //删除项
    operate.deleteItem = function (item) {
        if (item.OpSign === "copy") {
            vmManager.delItem = item;
            var ds = _.clone(vmManager.dataSource);
            leeHelper.remove(ds, vmManager.delItem);
            vmManager.dataSource = ds;
        }
        else {
            item.OpSign = "delete";
            vmManager.delItem = item;
            vmManager.delModal.$promise.then(vmManager.delModal.show);

        }

    }
    // 清空界面数据
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    }
    //批量保存所有数据
    operate.saveAll = function () {
        $scope.opPromise = qualityInspectionDataOpService.saveIqcInspectionItemConfigDatas(vmManager.dataSource).then(function (opresult) {
            if (opresult.Result) {
                vmManager.dataSource = [];
                vmManager.dataSets = [];
                vmManager.targetMaterialId = null;
                vmManager.copyLotWindowDisplay = false;
            }
        });
    }

})

//iqc检验方式配置模块
qualityModule.controller("iqcInspectionModeCtrl", function ($scope, qualityInspectionDataOpService, $modal) {
    leeHelper.setWebSiteTitle("质量管理", "IQC检验方式配置");
    $scope.states = ["Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Dakota", "North Carolina", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"];

    var uiVM = {
        InspectionMode: "正常",
        InspectionLevel: null,
        InspectionAQL: null,
        StartNumber: 0,
        EndNumber: 0,
        InspectionCount: 0,
        AcceptCount: 0,
        RefuseCount: 0,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: "add",
        Id_Key: null,
    }
    $scope.vm = uiVM;
    var initVM = _.clone(uiVM);
    var vmManager = {
        inspectionMode: "正常",
        inspectionLevel: null,
        inspectionAQL: null,
        dataSets: [],
        dataSource: [],
        deleteItem: null,
        inspectionLevelValues: [],
        inspectionAQLValues: [],
        init: function () {
            uiVM = _.clone(initVM);
            $scope.vm = uiVM;
        },
        inspectionModeArr: [{ id: "正常", text: "正常" }, { id: "加严", text: "加严" }, { id: "放宽", text: "放宽" }],
        onEnterDown: function ($event) {
            if ($event.keyCode === 13) {
                vmManager.getInspectionModeDatas();
            }
        },
        //获取检验水平数据
        getInspectionLevelValues: function () {
            qualityInspectionDataOpService.getInspectionLevelValues($scope.vm.InspectionMode).then(function (datas) {
                if (_.isArray(datas)) {
                    angular.forEach(datas, function (item) {
                        $scope.vmManager.inspectionLevelValues.push(item);
                    });
                }
            })
        },
        //获取AQL数据
        getInspectionAQLValues: function () {
            if ($scope.vm.InspectionLevel) {
                qualityInspectionDataOpService.getInspectionAQLValues($scope.vm.InspectionMode, $scope.vm.InspectionLevel).then(function (datas) {
                    angular.forEach(datas, function (item) {
                        $scope.vmManager.inspectionAQLValues.push(item);
                    })
                })
            } else {
                $scope.vmManager.inspectionAQLValues = [];
            }
        },
        getInspectionModeDatas: function () {
            $scope.searchPromise = qualityInspectionDataOpService.getIqcInspectionModeDatas($scope.vmManager.inspectionMode, $scope.vmManager.inspectionLevel, $scope.vmManager.inspectionAQL).then(function (datas) {
                vmManager.dataSource = datas;
                vmManager.dataSets = datas;
            })
        },
        deleteModalWindow: $modal({
            title: "删除提示",
            content: "确认删除此信息吗？",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
            show: false,
            controller: function ($scope) {
                $scope.confirmDelete = function () {
                    vmManager.deleteItem.OpSign = "delete";
                    qualityInspectionDataOpService.storeIqcInspectionModeData(vmManager.deleteItem).then(function (opresult) {
                        leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                            if (opresult.Result) {
                                leeHelper.remove(vmManager.dataSets, vmManager.deleteItem);
                                var ds = _.clone(vmManager.dataSource);
                                leeHelper.remove(ds, vmManager.deleteItem);
                                vmManager.dataSource = ds;
                                vmManager.deleteModalWindow.$promise.then(vmManager.deleteModalWindow.hide);
                            }
                        });
                    }
                )
                }
            }

        }),
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //保存iqc检验方式模块的数据
    operate.saveIqcInspectionModeData = function (isValid) {
        leeHelper.setUserData(uiVM);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            qualityInspectionDataOpService.storeIqcInspectionModeData($scope.vm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        if (uiVM.OpSign === "add") {
                            leeHelper.copyVm(opresult.Entity, uiVM);
                            var ds = _.clone(vmManager.dataSource);
                            ds.push(uiVM);
                            vmManager.dataSource = ds;
                        }
                        else {
                            var item = _.find(vmManager.dataSource, { Id_Key: uiVM.Id_Key });
                            leeHelper.copyVm(uiVM, item);
                        }
                        vmManager.init();
                    }
                })
            })
        });
    };
    //刷新iqc检验方式模块的数据
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    };
    //编辑iqc检验方式模块的数据
    operate.editItem = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVM = _.clone(item);
    }
    //删除iqc检验方式模块的数据
    operate.deleteItem = function (item) {
        vmManager.deleteItem = item;
        vmManager.deleteModalWindow.$promise.then(vmManager.deleteModalWindow.show)
    }
})

///iqc数据采集控制器
qualityModule.controller("iqcDataGatheringCtrl", function ($scope, qualityInspectionDataOpService) {
    leeHelper.setWebSiteTitle("质量管理", "IQC检验数据采集");
    var vmManager = {
        orderId: null,
        currentMaterialIdItem: null,
        currentInspectionItem: null,
        panelDataSource: [],
        //缓存数据
        cacheDatas: [],
        searchMaterialIdKeyDown: function ($event) {
            if ($event.keyCode === 13) {
                vmManager.getMaterialDatas();
            }
        },
        //按工单获取物料品号信息#modal
        getMaterialDatas: function () {
            if (vmManager.orderId) {
                vmManager.panelDataSource = [];
                qualityInspectionDataOpService.getInspectionDataGatherMaterialIdDatas(vmManager.orderId).then(function (materialIdDatas) {
                    angular.forEach(materialIdDatas, function (item) {
                        var dataItem = { productId: item.ProductID, productName: item.ProductName, materialIdItem: item, inspectionItemDatas: [], dataSets: [] };
                        vmManager.panelDataSource.push(dataItem);
                    })
                    vmManager.orderId = null;
                });
            }
        },
        //按物料品号获取检验项目信息
        selectMaterialIdItem: function (item) {
            vmManager.currentMaterialIdItem = item;
            var datas = _.find(vmManager.cacheDatas, { key: item.ProductID });
            if (datas === undefined) {
                $scope.searchPromise = qualityInspectionDataOpService.getIqcInspectionItemDataSummaryLabelList(item.OrderID, item.ProductID).then(function (inspectionItemDatas) {
                    datas = { key: item.ProductID, dataSource: inspectionItemDatas };
                    vmManager.cacheDatas.push(datas);
                    var dataItems = _.find(vmManager.panelDataSource, { productId: item.ProductID });
                    if (dataItems !== undefined) {
                        dataItems.inspectionItemDatas = inspectionItemDatas;
                        dataItems.dataSets = inspectionItemDatas;
                    }
                });
            }
            else {
                var dataItems = _.find(vmManager.panelDataSource, { productId: item.ProductID });
                if (dataItems !== undefined) {
                    dataItems.inspectionItemDatas = datas.dataSource;
                }
            }
        },
        //点击检验项目获取所有项目信息
        selectInspectionItem: function (item) {
            vmManager.currentInspectionItem = item;
            vmManager.dataList = [];
            //vmManager.currentInspectionItem.InspectionDataGatherType = "E";
            var dataGatherType = vmManager.currentInspectionItem.InspectionDataGatherType;
            if (dataGatherType === "E" || dataGatherType === "F") {
                if (item.InspectionCount === "0") return;
            }
            vmManager.createGataherDataUi(dataGatherType, item);
        },
        //生成
        createTypeEInput: function () {
            vmManager.dataList = [];
            var item = vmManager.currentInspectionItem;
            var dataGatherType = vmManager.currentInspectionItem.InspectionDataGatherType
            vmManager.createGataherDataUi(dataGatherType, item);
        },
        //根据采集方式创建数据采集窗口
        createGataherDataUi: function (dataGatherType, item) {
            var dataList = item.InspectionItemDatas === null || item.InspectionItemDatas === "" ? [] : item.InspectionItemDatas.split(',');
            item.NeedFinishDataNumber = parseInt(item.InspectionCount);
            if (dataGatherType === "A" || dataGatherType === "E") {
                vmManager.inputDatas = leeHelper.createDataInputs(item.NeedFinishDataNumber, 5, dataList, function (itemdata) {
                    itemdata.result = leeHelper.checkValue(vmManager.currentInspectionItem.SizeUSL, vmManager.currentInspectionItem.SizeLSL, itemdata.indata);
                    vmManager.dataList.push({ index: itemdata.index, data: itemdata.indata, result: itemdata.result });
                });
                if (dataGatherType === "E") {
                    vmManager.currentInspectionItem.AcceptCount = 0;
                    vmManager.currentInspectionItem.RefuseCount = 1;
                }
            }
            else if (dataGatherType === "C" || dataGatherType === "F") {
                if (dataList.length === 0) {
                    for (var i = 0; i < item.NeedFinishDataNumber; i++) {
                        dataList.push('OK');
                    }
                }
                vmManager.inputDatas = leeHelper.createDataInputs(item.NeedFinishDataNumber, 5, dataList, function (itemdata) {
                    vmManager.dataList.push({ index: itemdata.index, data: itemdata.indata, result: itemdata.indata === "OK" ? true : false });
                });

                if (dataGatherType === "F") {
                    vmManager.currentInspectionItem.AcceptCount = 0;
                    vmManager.currentInspectionItem.RefuseCount = 1;
                }
            }

        },
        setItemResult: function (item) {
            item.indata = item.indata === "OK" ? "NG" : "OK";
            item.result = item.indata === "OK" ? true : false;
            var dataInputItem = _.find(vmManager.dataList, { index: item.index });
            if (dataInputItem !== undefined) {
                dataInputItem.data = item.indata;
                dataInputItem.result = item.result;
            }
        },
        //数据集合
        dataList: [],
        inputDatas: [],
        //设置输入数据项
        setInputData(item) {
            //判定Item的值
            item.result = leeHelper.checkValue(vmManager.currentInspectionItem.SizeUSL, vmManager.currentInspectionItem.SizeLSL, item.indata);
            var dataInputItem = _.find(vmManager.dataList, { index: item.index });
            if (dataInputItem === undefined) {
                if (vmManager.dataList.length < vmManager.currentInspectionItem.NeedFinishDataNumber)
                    vmManager.dataList.push({ data: item.indata, result: item.result });
            }
            else {
                dataInputItem.data = item.indata;
                dataInputItem.result = item.result;
            }
        },
        dataInputKeyDown: function (item, $event) {
            if ($event.keyCode === 13) {
                item.focus = false;
                vmManager.setInputData(item);
                var row = _.find(vmManager.inputDatas, { rowId: item.rowId });
                if (row !== undefined) {
                    var col = _.find(row.cols, { colId: item.nextColId });
                    if (col !== undefined) {
                        col.focus = true;//设置下一个焦点
                    }
                }
                if (item.nextColId === "last") {
                    //保存数据
                    operate.saveIqcGatherDatas();
                }
            }
        },
        //更新检测项目列表
        updateInspectionItemList: function (editItem) {
            var materialItem = _.find(vmManager.cacheDatas, { key: vmManager.currentMaterialIdItem.ProductID });
            if (materialItem !== undefined) {
                var dataItem = _.find(materialItem.dataSource, { InspectionItem: vmManager.currentInspectionItem.InspectionItem });
                if (dataItem !== undefined) {
                    dataItem.HaveFinishDataNumber = editItem.HaveFinishDataNumber;//vmManager.dataList.length;
                    dataItem.InspectionItemResult = editItem.InspectionItemResult;
                    dataItem.InspectionItemDatas = editItem.InspectionItemDatas;
                    dataItem.InsptecitonItemIsFinished = editItem.InsptecitonItemIsFinished;
                }
            }
        },


    }
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //保存Iqc采集数据
    operate.saveIqcGatherDatas = function () {
        var dataList = [], result = true;
        var dataItem = vmManager.currentInspectionItem;
        if (dataItem.InspectionDataGatherType === "A" || dataItem.InspectionDataGatherType === "E" || dataItem.InspectionDataGatherType === "C" || dataItem.InspectionDataGatherType === "F") {
            //获取数据及判定结果
            angular.forEach(vmManager.dataList, function (item) {
                dataList.push(item.data);
                result = result && item.result;
            });
            //数据列表字符串
            dataItem.InspectionItemDatas = dataList.join(",");
            dataItem.InspectionItemResult = result ? "OK" : "NG";
            dataItem.HaveFinishDataNumber = vmManager.dataList.length;
            if (dataItem.InspectionDataGatherType === "E" || dataItem.InspectionDataGatherType === "F") {
                dataItem.HaveFinishDataNumber = dataItem.NeedFinishDataNumber;
            }
        }
        else if (dataItem.InspectionDataGatherType === "D") {
            dataItem.InspectionItemResult = dataItem.InspectionItemDatas;
            dataItem.HaveFinishDataNumber = dataItem.NeedFinishDataNumber;
        }
        dataItem.InsptecitonItemIsFinished = true;
        leeHelper.setUserData(dataItem);
        $scope.opPromise = qualityInspectionDataOpService.storeIqcInspectionGatherDatas(dataItem).then(function (opResult) {

            if (opResult.Result) {
                //更新界面检测项目列表
                vmManager.updateInspectionItemList(dataItem);
                vmManager.inputDatas = [];
                vmManager.dataList = [];
                //切换到下一项
            }
        });
    };
    //上传附件
    $scope.selectFile = function (el) {
        leeHelper.upoadFile(el, function (fd) {
            qualityInspectionDataOpService.uploadIqcGatherDataAttachFile(fd).then(function (result) {
                if (result === 'OK') {
                    var nowDate = new Date().getDate();
                    var nowHour = new Date().getHours();
                    if (nowDate < 10) { nowDate += '0' };
                    if (nowHour < 10) { nowHour += '0' };
                    vmManager.currentInspectionItem.FileName = $scope.uploadFileName = nowDate.toString() + nowHour.toString() + fd.name;
                    vmManager.currentInspectionItem.OpSign = leeDataHandler.dataOpMode.uploadFile;
                    qualityInspectionDataOpService.storeIqcInspectionGatherDatas(vmManager.currentInspectionItem).then(function (opResult) {
                        if (opResult.Result) {
                            if (opResult.Result) {
                                alert("上传文件成功");
                            }
                        }
                    })
                }
            })
        });
    }
})

///fqc数据采集控制器
qualityModule.controller("fqcDataGatheringCtrl", function ($scope, qualityInspectionDataOpService, connDataOpService) {
    leeHelper.setWebSiteTitle("质量管理", "FQC检验数据采集");
    $scope.opPersonInfo = { Department: '', ClassType: '' };

    var vmManager = {
        orderId: null,
        orderInfo: null,
        //抽样批次数量
        sampleCount: 0,
        currentOrderSubIdItem: null,
        currentInspectionItem: null,
        panelDataSource: [],
        //缓存数据
        cacheDatas: [],
        //生成抽样表单项
        createSampleFormItem: function () {
            qualityInspectionDataOpService.createFqcSampleFormItem(vmManager.orderInfo.OrderId, vmManager.sampleCount).then(function (inspectionItemDatas) {
                if (!vmManager.sampleCount) {
                    alert("抽样批次数量不能为空！")
                }
                if (angular.isArray(inspectionItemDatas) && inspectionItemDatas.length > 0) {
                    var item = inspectionItemDatas[0];
                    var dataItem = { orderId: item.OrderId, orderIdNumber: item.OrderIdNumber, inspectionStatus: item.InspectionStatus, inspectionItemDatas: inspectionItemDatas, dataSets: inspectionItemDatas };
                    vmManager.panelDataSource.push(dataItem);
                    vmManager.sampleCount = 0;
                }
            })
        },
        searchFqcOrderInfoKeyDown: function ($event) {
            if ($event.keyCode === 13) {
                vmManager.getFqcOrderInfo();
            }
        },
        //按工单获取物料品号信息 已抽数量 OrderNumber
        getFqcOrderInfo: function () {
            if (vmManager.orderId) {
                vmManager.panelDataSource = [];
                $scope.searchPromise = qualityInspectionDataOpService.getFqcOrderInfoDatas(vmManager.orderId).then(function (datas) {
                    vmManager.orderInfo = datas.orderInfo;
                    angular.forEach(datas.sampledDatas, function (item) {
                        var dataItem = { orderId: item.OrderId, orderIdNumber: item.OrderIdNumber, inspectionStatus: item.InspectionStatus, inspectionItemDatas: [], dataSets: [] };
                        vmManager.panelDataSource.push(dataItem);
                    })
                    vmManager.orderId = null;
                });
            }
        },
        //按物料品号获取检验项目信息
        selectOrderSubIdItem: function (item) {
            vmManager.currentOrderSubIdItem = item;
            if (item.inspectionItemDatas.length > 0) return;
            var key = item.orderId + item.orderIdNumber;
            var datas = _.find(vmManager.cacheDatas, { key: key });
            if (datas === undefined) {
                $scope.searchPromise = qualityInspectionDataOpService.getFqcSampleFormItems(item.orderId, item.orderIdNumber).then(function (inspectionItemDatas) {
                    datas = { key: key, dataSource: inspectionItemDatas };
                    vmManager.cacheDatas.push(datas);
                    var dataItems = _.find(vmManager.panelDataSource, { orderId: item.orderId, orderIdNumber: item.orderIdNumber });
                    if (dataItems !== undefined) {
                        dataItems.inspectionItemDatas = inspectionItemDatas;
                        dataItems.dataSets = inspectionItemDatas;
                    }
                });
            }
            else {
                var dataItems = _.find(vmManager.panelDataSource, { orderId: item.orderId, orderNum: item.orderNum });
                if (dataItems !== undefined) {
                    dataItems.inspectionItemDatas = datas.dataSource;
                }
            }
        },
        //点击检验项目获取所有项目信息
        selectInspectionItem: function (item) {
            vmManager.currentInspectionItem = item;
            vmManager.dataList = [];
            var dataGatherType = vmManager.currentInspectionItem.InspectionDataGatherType;
            vmManager.createGataherDataUi(dataGatherType, item);
        },
        ///根据采集方式创建数据采集窗口
        createGataherDataUi: function (dataGatherType, item) {
            var dataList = item.InspectionItemDatas === null || item.InspectionItemDatas === "" ? [] : item.InspectionItemDatas.split(',');
            if (dataGatherType === "A") {
                vmManager.inputDatas = leeHelper.createDataInputs(item.NeedFinishDataNumber, 5, dataList, function (itemdata) {
                    itemdata.result = leeHelper.checkValue(vmManager.currentInspectionItem.SizeUSL, vmManager.currentInspectionItem.SizeLSL, itemdata.indata);
                    vmManager.dataList.push({ index: itemdata.index, data: itemdata.indata, result: itemdata.result });
                });
            }
            else if (dataGatherType === "C") {
                if (dataList.length === 0) {
                    for (var i = 0; i < item.NeedFinishDataNumber; i++) {
                        dataList.push('OK');
                    }
                }
                vmManager.inputDatas = leeHelper.createDataInputs(item.NeedFinishDataNumber, 5, dataList, function (itemdata) {
                    vmManager.dataList.push({ index: itemdata.index, data: itemdata.indata, result: itemdata.indata === "OK" ? true : false });
                });
            }

        },
        setItemResult: function (item) {
            item.indata = item.indata === "OK" ? "NG" : "OK";
            item.result = item.indata === "OK" ? true : false;
            var dataInputItem = _.find(vmManager.dataList, { index: item.index });
            if (dataInputItem !== undefined) {
                dataInputItem.data = item.indata;
                dataInputItem.result = item.result;
            }
        },
        //数据集合
        dataList: [],
        inputDatas: [],
        //设置输入数据项
        setInputData(item) {
            //判定Item的值
            item.result = leeHelper.checkValue(vmManager.currentInspectionItem.SizeUSL, vmManager.currentInspectionItem.SizeLSL, item.indata);
            var dataInputItem = _.find(vmManager.dataList, { index: item.index });
            if (dataInputItem === undefined) {
                if (vmManager.dataList.length < vmManager.currentInspectionItem.NeedFinishDataNumber)
                    vmManager.dataList.push({ data: item.indata, result: item.result });
            }
            else {
                dataInputItem.data = item.indata;
                dataInputItem.result = item.result;
            }
        },
        dataInputKeyDown: function (item, $event) {
            if ($event.keyCode === 13) {
                item.focus = false;
                vmManager.setInputData(item);
                var row = _.find(vmManager.inputDatas, { rowId: item.rowId });
                if (row !== undefined) {
                    var col = _.find(row.cols, { colId: item.nextColId });
                    if (col !== undefined) {
                        col.focus = true;//设置下一个焦点
                    }
                }
                if (item.nextColId === "last") {
                    //保存数据
                    operate.saveGatherDatas();
                }
            }
        },
        //更新检测项目列表
        updateInspectionItemList: function (editItem) {
            var key = editItem.orderId + editItem.orderIdNumber;
            var materialItem = _.find(vmManager.cacheDatas, { key: key });
            if (materialItem !== undefined) {
                var dataItem = _.find(materialItem.dataSource, { InspectionItem: vmManager.currentInspectionItem.InspectionItem });
                if (dataItem !== undefined) {
                    dataItem.HaveFinishDataNumber = editItem.HaveFinishDataNumber;//vmManager.dataList.length;
                    dataItem.InspectionItemResult = editItem.InspectionItemResult;
                    dataItem.InspectionItemDatas = editItem.InspectionItemDatas;
                    dataItem.InsptecitonItemIsFinished = editItem.InsptecitonItemIsFinished;
                }
            }
        },
    }
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //保存Fqc采集数据
    operate.saveGatherDatas = function () {
        var dataList = [], result = true;
        var dataItem = vmManager.currentInspectionItem;
        if (dataItem.InspectionDataGatherType === "A" || dataItem.InspectionDataGatherType === "C") {
            //获取数据及判定结果
            angular.forEach(vmManager.dataList, function (item) {
                dataList.push(item.data);
                result = result && item.result;
            });
            //数据列表字符串
            dataItem.InspectionItemDatas = dataList.join(",");
            dataItem.InspectionItemResult = result ? "OK" : "NG";
            dataItem.HaveFinishDataNumber = vmManager.dataList.length;
        }
        else if (dataItem.InspectionDataGatherType === "D") {
            dataItem.InspectionItemResult = dataItem.InspectionItemDatas;
            dataItem.HaveFinishDataNumber = dataItem.NeedFinishDataNumber;
        }
        dataItem.InsptecitonItemIsFinished = true;
        leeHelper.setUserData(dataItem);
        leeHelper.copyVm($scope.opPersonInfo, dataItem);
        dataItem.OpSign = leeDataHandler.dataOpMode.add;

        $scope.opPromise = qualityInspectionDataOpService.storeFqcInspectionGatherDatas(dataItem).then(function (opResult) {
            if (opResult.Result) {
                console.log(dataItem.DocumentPath);
                //更新界面检测项目列表
                vmManager.updateInspectionItemList(dataItem);
                vmManager.inputDatas = [];
                vmManager.dataList = [];
                //切换到下一项
            }
        });
    };

    //上传附件
    $scope.selectFile = function (el) {
        leeHelper.upoadFile(el, function (fd) {
            qualityInspectionDataOpService.uploadFqcGatherDataAttachFile(fd).then(function (result) {
                if (result === 'OK') {
                    var nowDate = new Date().getDate();
                    var nowHour = new Date().getHours();
                    if (nowDate < 10) { nowDate += '0' };
                    if (nowHour < 10) { nowHour += '0' };
                    vmManager.currentInspectionItem.FileName = $scope.uploadFileName = nowDate.toString() + nowHour.toString() + fd.name;
                    vmManager.currentInspectionItem.OpSign = leeDataHandler.dataOpMode.uploadFile;
                    qualityInspectionDataOpService.storeFqcInspectionGatherDatas(vmManager.currentInspectionItem).then(function (opResult) {
                        if (opResult.Result) {
                            alert("上传文件成功");
                        }
                    })
                }
            })
        });
    }

    var loadWorkerInfo = (function () {
        var user = leeDataHandler.dataStorage.getLoginedUser();
        if (user) {
            connDataOpService.getWorkersBy(user.userId).then(function (users) {
                if (_.isArray(users) && users.length > 0) {
                    var userInfo = users[0];
                    leeHelper.copyVm(userInfo, $scope.opPersonInfo);
                }
            });
        }
    })();
})

///iqc检验单管理
qualityModule.controller("inspectionFormManageOfIqcCtrl", function ($scope, qualityInspectionDataOpService, $modal, $alert) {
    leeHelper.setWebSiteTitle("质量管理", "IQC检验单管理");
    var vmManager = $scope.vmManager = {
        dateFrom: null,
        dateTo: null,
        selectedFormStatus: "全部",
        formStatuses: [{ label: "全部", value: "全部" }, { label: "待检验", value: "待检验" }, { label: "未完成", value: "未完成" }, { label: "待审核", value: "待审核" }, { label: "已审核", value: "已审核" }],
        editWindowWidth: "100%",
        isShowDetailWindow: false,
        currentItem: null,
        detailDatas: [],
        InspectionItemDatasArr: [],
        dataSource: [],
        dataSets: [],
        isShowTips: false,
        //数据超过100条提示框
        showTips: $alert({ content: '亲~查询数量太多，只能显示100条信息哟', placement: 'top', type: 'info', show: false, duration: "3", container: '.tipBox' }),
        //模态框
        checkModal: $modal({
            title: "审核提示",
            content: "亲~您确定要审核吗",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
            controller: function ($scope) {
                $scope.confirmDelete = function () {
                    leeHelper.setUserData(vmManager.currentItem);
                    vmManager.currentItem.InspectionStatus = "已审核";
                    vmManager.currentItem.OpSign = "edit";
                    qualityInspectionDataOpService.postInspectionFormManageCheckedOfIqcData(vmManager.currentItem).then(function (opresult) {
                        if (opresult.Result) {
                            leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                                vmManager.checkModal.$promise.then(vmManager.checkModal.hide);
                            });
                        }
                    })
                }
            },
            show: false,
        }),
        //获取检验表单主数据
        getMasterDatas: function () {
            $scope.searchPromise = qualityInspectionDataOpService.getInspectionFormManageOfIqcDatas(vmManager.selectedFormStatus, $scope.vmManager.dateFrom, $scope.vmManager.dateTo).then(function (editDatas) {
                if (editDatas.length >= 100) {
                    vmManager.showTips.$promise.then(vmManager.showTips.show);
                }
                vmManager.dataSource = editDatas;
                vmManager.dataSets = editDatas;
            })
        },
        //审核
        showCheckModal: function (item) {
            if (item) vmManager.currentItem = item;
            vmManager.checkModal.$promise.then(vmManager.checkModal.show);
        },
        //获取详细数据
        getDetailDatas: function (item) {
            vmManager.currentItem = item;
            qualityInspectionDataOpService.getInspectionFormDetailOfIqcDatas(item.OrderId, item.MaterialId).then(function (datas) {
                vmManager.isShowDetailWindow = true;
                console.log(datas);
                angular.forEach(datas, function (item) {
                    var dataItems = item.InspectionItemDatas.split(",");
                    item.dataList = leeHelper.createDataInputs(dataItems.length, 4, dataItems);
                })
                vmManager.detailDatas = datas;
            })
        },
        //返回
        refresh: function () {
            vmManager.isShowDetailWindow = false;
        }
    };
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
})

//检验方式转换配置
qualityModule.controller("inspectionModeSwitchCtrl", function ($scope, qualityInspectionDataOpService) {
    leeHelper.setWebSiteTitle("质量管理", "检验方式配置");
    var vmManager = $scope.vmManager = {
        isEnable: false,
        switchModeList: [],
        inspectionModeTypes: [{ name: "IQC", text: "IQC" }, { name: "IQC", text: "FQC" }, { name: "IPQC", text: "IPQC" }],
        getModeSwitchDatas: function () {

            vmManager.switchModeList = [];
            $scope.searchPromise = qualityInspectionDataOpService.getModeSwitchDatas($scope.vmManager.inspectionModeType).then(function (datas) {
                angular.forEach(datas, function (item) {
                    vmManager.isEnable = Boolean(item.IsEnable.toLowerCase());
                    vmManager.switchModeList.push(item)
                })
            })
        }
    }
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) {
        leeHelper.setUserData();
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            for (var i = 0; i < vmManager.switchModeList.length; i++) {
                vmManager.switchModeList[i].SwitchVaule = $scope.vmManager.switchModeList[i].SwitchVaule;
            }
            qualityInspectionDataOpService.saveModeSwitchDatas(vmManager.isEnable, vmManager.switchModeList).then(function (opresult) {
                if (opresult.Result) {
                    leeDataHandler.dataOperate.handleSuccessResult(operate, opresult);

                    vmManager.switchModeList = [];

                }
            })
        })
    }
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.switchModeList = [];
        });
    }
})

//fqc检验项目配置
qualityModule.controller("fqcInspectionItemConfigCtrl", function ($scope, qualityInspectionDataOpService, $modal) {
    leeHelper.setWebSiteTitle("质量管理", "FQC检验项目配置");
    var uiVM = {
        MaterialId: null,
        ProductDepartment: null,
        InspectionItem: null,
        InspectionItemIndex: 0,
        SizeUSL: null,
        SizeLSL: null,
        SizeMemo: null,
        PrimarySecondaryProperty: null,
        EquipmentId: null,
        InspectionMethod: null,
        InspectionDataGatherType: null,
        SIPInspectionStandard: null,
        InspectionMode: null,
        InspectionLevel: null,
        InspectionAQL: null,
        Memo: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: "add",
        Id_Key: null,
    }
    $scope.vm = uiVM;
    var tableVM = {
        MaterialName: null,
        MaterialBelongDepartment: null,
        MaterialSpecify: null,
        MaterialrawID: null,
    }
    $scope.tableVm = tableVM;
    $scope.vm = uiVM;
    var initVM = _.clone(uiVM);
    var vmManager = {
        editWindowShow: false,
        inspectionMode: [{ id: "正常", text: "正常" }, { id: "加严", text: "加严" }, { id: "放宽", text: "放宽" }],
        InspectionDataGatherTypes: [{ id: "A", text: "A" }, { id: "B", text: "B" }, { id: "C", text: "C" }, { id: "D", text: "D" }, { id: "E", text: "E" }],
        dataSource: [],
        dataSets: [],
        copyLotWindowDisplay: false,
        targetMaterialId: null,
        delItem: null,
        init: function () {
            if (uiVM.OpSign === 'add') {
                leeHelper.clearVM(uiVM, ["MaterialId"]);
            }
            else {
                uiVM = _.clone(initVM);
            }
            uiVM.OpSign = 'add';
            $scope.vm = uiVM;
        },

        //013935根据品号查询
        getConfigDatas: function () {
            $scope.searchPromise = qualityInspectionDataOpService.getfqcInspectionItemConfigDatas($scope.vm.MaterialId).then(function (datas) {
                if (datas !== null) {
                    $scope.tableVm = datas.ProductMaterailModel;
                    vmManager.dataSource = datas.InspectionItemConfigModelList;
                }
            });
        },
        //013935获取进料检验项目最大配置工序ID
        getInspectionIndex: function () {
            if (vmManager.dataSource.length > 0) {
                var maxItem = _.max(vmManager.dataSource, function (item) { return item.InspectionItemIndex; });
                $scope.vm.InspectionItemIndex = maxItem.InspectionItemIndex + 1;
            }
            else {
                $scope.vm.InspectionItemIndex = 0;
            }
        },
        //显示批量复制操作窗口
        showCopyLotWindow: function () {
            vmManager.copyLotWindowDisplay = true;
        },
        //批量复制
        copyAll: function () {
            qualityInspectionDataOpService.checkFqcInspectionItemConfigMaterialId(vmManager.targetMaterialId).then(function (opresult) {

                if (opresult.Result) {
                    alert(vmManager.targetMaterialId + "已经存在")
                } else {
                    angular.forEach(vmManager.dataSource, function (item) {
                        item.Id_key = null;
                        item.MaterialId = vmManager.targetMaterialId;
                    });
                }

            })
        },
        delModal: $modal({
            title: "删除提示",
            content: "你确定要删除此数据吗?",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
            controller: function ($scope) {
                $scope.confirmDelete = function () {
                    $scope.opPromise = qualityInspectionDataOpService.deleteFqcInspectionConfigItem(vmManager.delItem).then(function (opresult) {
                        leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                            if (opresult.Result) {
                                leeHelper.remove(vmManager.dataSets, vmManager.delItem);
                                var ds = _.clone(vmManager.dataSource);
                                leeHelper.remove(ds, vmManager.delItem);
                                vmManager.dataSource = ds;
                                vmManager.delModal.$promise.then(vmManager.delModal.hide);
                            }
                        });
                    });
                };
            },
            show: false,
        }),
    }
    //013935导入excel
    $scope.selectFile = function (el) {
        var files = el.files;
        if (files.length > 0) {
            var file = files[0];
            var fd = new FormData();
            fd.append('file', file);
            qualityInspectionDataOpService.importfqcInspectionItemConfigDatas(fd).then(function (datas) {
                vmManager.dataSource = datas;
            });
        }
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //013935确认
    operate.confirm = function (isValid) {
        leeHelper.setUserData(uiVM);
        var dataItem = _.clone(uiVM);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            if (uiVM.OpSign === "add") {
                var ds = _.clone(vmManager.dataSource);
                ds.push(dataItem);
                vmManager.dataSource = ds;
            }
            operate.refresh();
        })
    };
    operate.editItem = function (item) {
        uiVM = item;
        uiVM.OpSign = "edit";
        $scope.vm = uiVM;
    };
    //删除项
    operate.deleteItem = function (item) {
        item.OpSign = "delete";
        vmManager.delItem = item;
        vmManager.delModal.$promise.then(vmManager.delModal.show);
    }

    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    }
    //批量保存所有数据
    operate.saveAll = function () {
        $scope.opPromise = qualityInspectionDataOpService.saveFqcInspectionItemConfigDatas(vmManager.dataSource).then(function (opresult) {
            leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                if (opresult.Result) {
                    vmManager.dataSource = [];
                    vmManager.dataSets = [];
                    vmManager.targetMaterialId = null;
                    vmManager.copyLotWindowDisplay = false;
                }
            });
        });
    }
})

//fqc检验单管理
qualityModule.controller("inspectionFormManageOfFqcCtrl", function ($scope, qualityInspectionDataOpService, $modal, $alert) {
    leeHelper.setWebSiteTitle("质量管理", "FQC检验单管理");
    var vmManager = $scope.vmManager = {
        dateFrom: null,
        dateTo: null,
        selectedFormStatus: "全部",
        formStatuses: [{ label: "全部", value: "全部" }, { label: "待检验", value: "待检验" }, { label: "未完成", value: "未完成" }, { label: "待审核", value: "待审核" }, { label: "已审核", value: "已审核" }],
        editWindowWidth: "100%",
        isShowDetailWindow: false,
        currentItem: null,
        detailDatas: [],
        InspectionItemDatasArr: [],
        dataSource: [],
        dataSets: [],
        isShowTips: false,
        //数据超过100条提示框
        showTips: $alert({ content: '亲~查询数量太多，只能显示100条信息哟', placement: 'top', type: 'info', show: false, duration: "3", container: '.tipBox' }),
        //模态框
        checkModal: $modal({
            title: "审核提示",
            content: "亲~您确定要审核吗",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
            controller: function ($scope) {
                $scope.confirmDelete = function () {
                    leeHelper.setUserData(vmManager.currentItem);
                    vmManager.currentItem.InspectionStatus = "已审核";
                    vmManager.currentItem.OpSign = "edit";
                    qualityInspectionDataOpService.postInspectionFormManageCheckedOfFqcData(vmManager.currentItem).then(function (opresult) {
                        if (opresult.Result) {
                            leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                                vmManager.checkModal.$promise.then(vmManager.checkModal.hide);
                            });
                        }
                    })
                }
            },
            show: false,
        }),
        //获取检验表单主数据
        getMasterDatas: function () {
            $scope.searchPromise = qualityInspectionDataOpService.getInspectionFormManageOfFqcDatas(vmManager.selectedFormStatus, $scope.vmManager.dateFrom, $scope.vmManager.dateTo).then(function (editDatas) {
                if (editDatas.length >= 100) {
                    vmManager.showTips.$promise.then(vmManager.showTips.show);
                }
                vmManager.dataSource = editDatas;
                vmManager.dataSets = editDatas;
                console.log(editDatas);
            })
        },
        //审核
        showCheckModal: function (item) {
            if (item) vmManager.currentItem = item;
            vmManager.checkModal.$promise.then(vmManager.checkModal.show);
        },
        //获取详细数据
        getDetailDatas: function (item) {
            vmManager.currentItem = item;
            qualityInspectionDataOpService.getInspectionFormDetailOfFqcDatas(item.OrderId, item.OrderIdNumber).then(function (datas) {
                vmManager.isShowDetailWindow = true;
                angular.forEach(datas, function (item) {
                    var dataItems = item.InspectionItemDatas.split(",");
                    item.dataList = leeHelper.createDataInputs(dataItems.length, 4, dataItems);
                })
                vmManager.detailDatas = datas;

                console.log(vmManager.detailDatas);
            })
        },
        //返回
        refresh: function () {
            vmManager.isShowDetailWindow = false;
        }
    };
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
})