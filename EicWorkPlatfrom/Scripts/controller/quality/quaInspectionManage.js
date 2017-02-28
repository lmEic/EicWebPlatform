/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');
//数据访问工厂
qualityModule.factory("qualityDataOpService", function (ajaxService) {
    var quality = {};
    var quaInspectionManageUrl = "/quaInspectionManage/";
    //013935获取IQC进料检验项目配置数据
    quality.GetIqcspectionItemConfigDatas = function (materialId) {
        var url = quaInspectionManageUrl + "GetIqcspectionItemConfigDatas";
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
    quality.storeIqcInspectionModeData = function (iqcInspectionModeItem) {
        var url = quaInspectionManageUrl + "StoreIqcInspectionModeData";
        return ajaxService.postData(url, {
            iqcInspectionModeItem: iqcInspectionModeItem
        })
    }
    return quality;
})

//iqc检验项目配置模块
qualityModule.controller("iqcInspectionItem", function ($scope, qualityDataOpService,$modal) {
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
                leeHelper.clearVM(uiVM, ["MaterialId", "Id_key"]);
            }
            else {
                uiVM = _.clone(initVM);
            }
            uiVM.OpSign = 'add';
            $scope.vm = uiVM;
        },

        //013935根据品号查询
        getConfigDatas: function () {
            $scope.searchPromise = qualityDataOpService.GetIqcspectionItemConfigDatas($scope.vm.MaterialId).then(function (datas) {
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
            angular.forEach(vmManager.dataSource, function (item) {
                item.MaterialId = vmManager.targetMaterialId;
            });
        },
        delItem:null,
        delModal:$modal({
            title: "删除提示",
            content: "你确定要删除此数据吗?",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
            controller: function ($scope) {
                $scope.confirmDelete = function () {
                    $scope.opPromise = qualityDataOpService.deleteIqlInspectionConfigItem(vmManager.delItem).then(function (opresult) {
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
            qualityDataOpService.importIqcInspectionItemConfigDatas(fd).then(function (datas) {
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
        $scope.opPromise = qualityDataOpService.saveIqcInspectionItemConfigDatas(vmManager.dataSource).then(function (opresult) {
            if (opresult.Result) {
                vmManager.dataSource = [];
                vmManager.dataSets = [];
            }
        });
    }
})

//检验方式配置模块
qualityModule.controller("iqcInspectionMode", function ($scope, qualityDataOpService, $modal) {
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
                    qualityDataOpService.storeIqcInspectionModeData(vmManager.deleteItem).then(function (opresult) {
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
            qualityDataOpService.storeIqcInspectionModeData($scope.vm).then(function (opresult) {
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