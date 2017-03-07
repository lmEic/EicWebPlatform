/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');
//数据访问工厂
qualityModule.factory("qualityInspectionDataOpService", function (ajaxService) {
    var quality = {};
    var quaInspectionManageUrl = "/quaInspectionManage/";
    //013935获取IQC进料检验项目配置数据
    quality.GetIqcspectionItemConfigDatas = function (materialId) {
        var url = quaInspectionManageUrl + "GetIqcspectionItemConfigDatas";
        return ajaxService.getData(url,  {
            materialId: materialId
        })
    };
    ///数据库中是否存在此物料料号
   quality.checkIqcspectionItemConfigMaterialId = function (materialId) {
       var url = quaInspectionManageUrl + "CheckIqcspectionItemConfigMaterialId";
        return ajaxService.getData(url,  {
            materialId: materialId
        })
    };
    //013935从excel中IQC进料检验配置项数据
    quality.importIqcInspectionItemConfigDatas = function (file) {
        var url = quaInspectionManageUrl + 'ImportIqcInspectionItemConfigDatas';
        return ajaxService.uploadFile(url, file);
    }

    //013935保存IQC进料检验配置项数据
    quality.saveIqcInspectionItemConfigDatas = function (iqcInspectionConfigItems) {
        var url = quaInspectionManageUrl + 'SaveIqcInspectionItemConfigDatas';
        return ajaxService.postData(url, {
            iqcInspectionConfigItems: iqcInspectionConfigItems
        })
    }
    quality.deleteIqlInspectionConfigItem = function (configItem) {
        var url = quaInspectionManageUrl + 'DeleteIqlInspectionConfigItem';
        return ajaxService.postData(url, {
            configItem: configItem,
        });
    };

    //处理检验方式配置数据
    quality.storeInspectionModeConfigData = function (inspectionModeConfigEntity) {
        var url = quaInspectionManageUrl + "StoreInspectionModeConfigData";
        return ajaxService.postData(url, {
            inspectionModeConfigEntity: inspectionModeConfigEntity
        })
    }


    quality.getInspectionDataGatherMaterialIdDatas = function (orderId) {
        var url = quaInspectionManageUrl + "GetIqcMaterialInfoDatas";
        return ajaxService.getData(url, {
            orderId: orderId
        })
    }
    //iqc进料检验数据采集模块获得检验项目数据
    quality.getIqcInspectionItemDataSummaryLabelList = function (orderId, materialId) {
        var url = quaInspectionManageUrl + "GetIqcInspectionItemDataSummaryLabelList";
        return ajaxService.getData(url, {
            orderId:orderId,
            materialId: materialId
        })
    }
    //保存进料检验采集的数据  
    quality.storeIqcInspectionGatherDatas = function (gatherData) {
        var url = quaInspectionManageUrl + 'StoreIqcInspectionGatherDatas';
        return ajaxService.postData(url, {
           gatherData: gatherData,
        });
    };

    /////////////////////////////iqc检验单管理模块/////////////////////////
    //iqc检验单管理模块获取表单数据  
    quality.getInspectionFormManageOfIqcDatas = function (selectedFormStatus,dateFrom,dateTo) {
        var url = quaInspectionManageUrl + 'GetInspectionFormManageOfIqcDatas';
        return ajaxService.getData(url, {
            selectedFormStatus: selectedFormStatus,
            dateFrom:dateFrom,
            dateTo:dateTo
        })
    };
    //iqc检验单管理模块获取详细数据
    quality.getInspectionFormDetailDatas = function (orderId, materialId) {
        var url = quaInspectionManageUrl + "GetInspectionFormDetailDatas";
        return ajaxService.getData(url, {
            orderId: orderId,
            materialId: materialId
        })
    }
    return quality;
})

//iqc检验项目配置模块
qualityModule.controller("iqcInspectionItemCtrl", function ($scope, qualityInspectionDataOpService,$modal) {
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
        inspectionMode: [{ id: "正常", text: "正常" }, { id: "加严", text: "加严" }, { id: "放宽", text: "放宽" }],
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
            $scope.searchPromise = qualityInspectionDataOpService.GetIqcspectionItemConfigDatas($scope.vm.MaterialId).then(function (datas) {
                if (datas != null) {
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
            qualityInspectionDataOpService.checkIqcspectionItemConfigMaterialId(vmManager.targetMaterialId).then(function (opresult) {
                if (opresult.Result) {
                   alert(vmManager.targetMaterialId+"已经存在")
                } else {
                    angular.forEach(vmManager.dataSource, function (item) {
                        item.Id_key = null;
                        item.MaterialId = vmManager.targetMaterialId;
                    });
                }

            })
            
        },
        delItem:null,
        delModal:$modal({
            title: "删除提示",
            content: "你确定要删除此数据吗?",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
            controller: function ($scope) {
                $scope.confirmDelete = function () {
                    $scope.opPromise = qualityInspectionDataOpService.deleteIqlInspectionConfigItem(vmManager.delItem).then(function (opresult) {
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
            qualityInspectionDataOpService.importIqcInspectionItemConfigDatas(fd).then(function (datas) {
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
    operate.saveAll = function ( ){
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

//检验方式配置模块
qualityModule.controller("iqcInspectionModeCtrl", function ($scope, qualityInspectionDataOpService, $modal) {
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
        dataSets: [],
        dataSource:[],
        deleteItem: null,
        init: function () {
            uiVM = _.clone(initVM);
            $scope.vm = uiVM;
        },
        inspectionMode: [{ id: "正常", text: "正常" }, { id: "加严", text: "加严" }, { id: "放宽", text: "放宽" }],
        deleteModalWindow: $modal({
            title: "删除提示",
            content: "确认删除此信息吗？",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
            show: false,
            controller: function ($scope) {
                $scope.confirmDelete = function () {
                    vmManager.deleteItem.OpSign = "delete";
                    qualityInspectionDataOpService.storeInspectionModeConfigData(vmManager.deleteItem).then(function (opresult) {
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
                )}
            }

        })
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
                        if (uiVM.OpSign == "add") {
                            leeHelper.copyVm(opresult.Attach, uiVM);
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
            operate.refresh();
        });
    };
    //编辑iqc检验方式模块的数据
    operate.editItem = function (item) {
        item.OpSign = "edit";
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
    var vmManager = {
        orderId: null,
        currentMaterialIdItem: null,
        currentInspectionItem: null,
        panelDataSource:[],
        //缓存数据
        cacheDatas:[],
        searchMaterialIdKeyDown: function ($event) {
            if ($event.keyCode === 13) {
                vmManager.getMaterialDatas();
            }
        },
        //按工单获取物料品号信息
        getMaterialDatas: function () {
            if (vmManager.orderId) {
                vmManager.panelDataSource = [];
                qualityInspectionDataOpService.getInspectionDataGatherMaterialIdDatas(vmManager.orderId).then(function (materialIdDatas) {
                    angular.forEach(materialIdDatas, function (item) {
                        var dataItem = { productId: item.ProductID, materialIdItem: item, inspectionItemDatas: [] };
                        vmManager.panelDataSource.push(dataItem);
                    })
                    vmManager.orderId = null;
                });
            }
        },
        //按物料品号获取检验项目信息
        selectMaterialIdItem: function (item) {
            vmManager.currentMaterialIdItem = item;
            var datas = _.find(vmManager.cacheDatas, { key: item.ProductID});
            if (datas === undefined) {
               $scope.searchPromise=qualityInspectionDataOpService.getIqcInspectionItemDataSummaryLabelList(item.OrderID, item.ProductID).then(function (inspectionItemDatas) {
                    datas = { key: item.ProductID, dataSource: inspectionItemDatas };
                    vmManager.cacheDatas.push(datas);
                    var dataItems = _.find(vmManager.panelDataSource, { productId: item.ProductID });
                    if (dataItems !== undefined) {
                        dataItems.inspectionItemDatas = inspectionItemDatas;
                    }
                });
            }
            else {
                var dataItems = _.find(vmManager.panelDataSource,{ productId: item.ProductID });
                if (dataItems !== undefined) {
                    dataItems.inspectionItemDatas = datas.dataSource;
                }
            }
        },
        //点击检验项目获取所有项目信息
        selectInspectionItem: function (item) {
            vmManager.currentInspectionItem = item;
            var dataList = item.InspectionItemDatas === null ? null : item.InspectionItemDatas.split(',');
            vmManager.inputDatas = leeHelper.createDataInputs(item.NeedFinishDataNumber, 5, dataList, function (itemdata) {
                itemdata.result = leeHelper.checkValue(vmManager.currentInspectionItem.SizeUSL, vmManager.currentInspectionItem.SizeLSL, itemdata.indata);
                vmManager.dataList.push({index:itemdata.index, data: itemdata.indata, result: itemdata.result });
            });
        },
        //数据集合
        dataList: [],
        inputDatas: [],
        //设置输入数据项
        setInputData(item){
            //判定Item的值
            item.result = leeHelper.checkValue(vmManager.currentInspectionItem.SizeUSL, vmManager.currentInspectionItem.SizeLSL, item.indata);
            var dataInputItem = _.find(vmManager.dataList, { index: item.index });
            if (dataInputItem === undefined) {
                if (vmManager.dataList.length < vmManager.currentInspectionItem.NeedFinishDataNumber)
                    vmManager.dataList.push({ data: item.indata, result: item.result });
            }
            else {
                dataInputItem.indata=item.indata;
                dataInputItem.result=item.result;
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
            var materialItem = _.find(vmManager.cacheDatas, { key: vmManager.currentMaterialIdItem.ProductID});
            if (materialItem !== undefined) {
                var dataItem = _.find(materialItem.dataSource, { InspectionItem: vmManager.currentInspectionItem.InspectionItem });
                if (dataItem !== undefined) {
                    dataItem.HaveFinishDataNumber = vmManager.dataList.length;//vmManager.dataList.length;
                    dataItem.InspectionItemResult = editItem.InspectionItemResult;
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
        var dataItem = vmManager.currentInspectionItem;
        var dataList = [], result = true;
        //获取数据及判定结果
        angular.forEach(vmManager.dataList, function (item) {
            dataList.push(item.data);
            result = result && item.result;
        });
       //数据列表字符串
        dataItem.InspectionItemDatas = dataList.join(",");
        dataItem.InspectionItemResult = result ? "OK" : "NG";
        dataItem.InsptecitonItemIsFinished = true;
        console.log(dataItem);
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
})

///fqc数据采集控制器
qualityModule.controller("fqcDataGatheringCtrl", function ($scope,qualityInspectionDataOpService) {
    ///IQC检验采集数据视图模型
    var uiVM = {
        OrderId: null,
        MaterialId: null,
        MaterialCount: null,
        InprectionItem: null,
        InspectionCount: null,
        InspectionAcceptCount: null,
        InspectionRefuseCount: null,
        InspectionItemDatas: null,
        InsprectionItemSatus: null,
        InsprectionItemResult: null,
        InsprectionDate: null,
        Memo: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: 'add',
        Id_Key: null,
    }
    var initVM = _.clone(uiVM);
    $scope.vm = uiVM;
    
    var vmManager = $scope.vmManager = {
        //数据集合
        dataList: [],
        inputDatas:[],
        dataInputKeyDown: function (item, $event) {
            if ($event.keyCode === 13)
            {
                item.focus = false;
                if (item.nextColId === "last")
                {
                    vmManager.dataList.push(item.indata);
                    alert(vmManager.dataList.join(","));
                    return;
                }
                var row = _.find(vmManager.inputDatas, { rowId: item.rowId });
                if (row !== undefined)
                {
                    var col = _.find(row.cols, { colId: item.nextColId });
                    if (col !== undefined)
                    {
                        //判定Item的值
                        vmManager.dataList.push({ data: item.indata, result: item.result });
                        col.focus = true;
                    }
                }
            }
        },
        totalCount:null,
        createDatas: function () {
            vmManager.inputDatas = leeHelper.createDataInputs(vmManager.totalCount, 5);
        },
    };

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //保存Iqc采集数据
    operate.saveIqcGatherDatas = function () {
        var doneCount = vmManager.dataList.length;
        var leftCount = vmManager.totalCount - doneCount;
        var dataList = [], result = true;
        //获取数据及判定结果
        angular.forEach(vmManager.dataList, function (item) {
            dataList.push(item.data);
            result = result && item.result;
        });
        //数据列表字符串
        uiVM.InspectionItemDatas = dataList.join(",");
        uiVM.InsprectionItemResult = result ? "PASS" : "FAIL";


        $scope.opPromise = qualityInspectionDataOpService.saveIqcInspectionGetherDatas(uiVM).then(function (opResult) {
            if (opResult.Result)
            {
                uiVM = _.clone(initVM);
                //切换到下一项
            }
        });
    };
    operate.refresh = function () { };
})


///iqc检验单管理
qualityModule.controller("inspectionFormManageOfIqcCtrl", function ($scope, qualityInspectionDataOpService, $modal) {
    var vmManager = $scope.vmManager = {
        dateFrom: null,
        dateTo: null,
        editDatas: [],
        selectedFormStatus:null,
        formStatuses: [{ label: "未完成", value: "未完成" }, { label: "待审核", value: "待审核" }, { label: "已审核", value: "已审核" }],
        editWindowWidth: "100%",
        isShowEditWindow: false,
        currentItem:null,
        showItemWindow: function () {
            
        },
        checkModal: $modal({
            title: "审核提示",
            content: "亲~您确定要审核吗",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
            controller: function ($scope) {
                $scope.confirmDelete = function (item) {
                    
                }
            },
            show:false,
        }),
        //获取检验表单主数据
        getMasterDatas: function () {
            qualityInspectionDataOpService.getInspectionFormManageOfIqcDatas(vmManager.selectedFormStatus, $scope.vmManager.dateFrom, $scope.vmManager.dateTo).then(function (editDatas) {
                
                vmManager.editDatas = editDatas;
            })
        },
        //获取详细数据
        getDetailDatas:function(item){
            qualityInspectionDataOpService.getInspectionFormDetailDatas(item.OrderId, item.MaterialId).then(function (datas) {
                console.log(datas);
                vmManager.isShowEditWindow = !vmManager.isShowEditWindow;
                vmManager.detailDatas = datas;
            })
        },
        //返回
        refresh: function () {
            vmManager.isShowEditWindow = false;
        }
    };
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
   
})