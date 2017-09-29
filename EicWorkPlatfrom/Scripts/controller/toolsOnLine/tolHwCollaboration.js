/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var officeAssistantModule = angular.module('bpm.toolsOnlineApp');
officeAssistantModule.factory('hwDataOpService', function (ajaxService) {
    var hwDataOp = {};
    var hwUrlPrefix = '/' + leeHelper.controllers.TolCooperateWithHw + '/';
    //----------人力信息----------------
    //获取人力信息
    hwDataOp.getManPower = function () {
        var url = hwUrlPrefix + 'GetManPower';
        return ajaxService.getData(url);
    };
    //保存人力信息数据
    hwDataOp.saveManPower = function (entity) {
        var url = hwUrlPrefix + 'SaveManPower';
        return ajaxService.postData(url, {
            entity: entity
        });
    };
    //----------物料信息模块：基础信息设置,库存、在制、发料--------------------
    //获取物料基础设置信息
    hwDataOp.getMaterialBaseInfo = function (materialId) {
        var url = hwUrlPrefix + 'GetMaterialBaseInfo';
        return ajaxService.getData(url, {
            materialId: materialId
        });
    };
    hwDataOp.saveMaterialBase = function (entities) {
        var url = hwUrlPrefix + 'SaveMaterialBase';
        return ajaxService.postData(url, {
            entities: entities
        });
    };

    hwDataOp.saveMaterialBom = function (entity) {
        var url = hwUrlPrefix + 'SaveMaterialBom';
        return ajaxService.postData(url, {
            entity: entity
        });
    };

    //获取物料信息信息
    hwDataOp.getMaterialDetails = function () {
        var url = hwUrlPrefix + 'GetMaterialDetails';
        return ajaxService.getData(url);
    };
    //保存库存信息数据
    hwDataOp.saveMaterialInventory = function (entity) {
        var url = hwUrlPrefix + 'SaveMaterialInventory';
        return ajaxService.postData(url, {
            entity: entity
        });
    };
    return hwDataOp;
});
///华为数据API数据操作助手
var hwApiHelper = (function () {
    ///华为数据实体
    function hwDataEntity() {
        //操作模块
        this.OpModule = null;
        //操作内容
        this.OpContent = null;
        //操作日志
        this.OpLog = null;
        //操作日期
        this.OpDate = null;
        //操作时间
        this.OpTime = null;
        //操作人
        this.OpPerson = null;
        //操作标志
        this.OpSign = leeDataHandler.dataOpMode.add;
    }
    ///华为数据配置实体
    function hwDataConfigEntity() {
        //物料编号
        this.MaterialId = null;
        //物料基础配置信息
        this.MaterialBaseDataContent = null;
        //物料BOM配置信息
        this.MaterialBomDataContent = null;
        //操作日志
        this.OpLog = null;
        //Item类别
        this.InventoryType = null;
        //数据状态
        this.DataStatus = 0;
        //操作日期
        this.OpDate = null;
        //操作时间
        this.OpTime = null;
        //操作人
        this.OpPerson = null;
        //操作标志
        this.OpSign = leeDataHandler.dataOpMode.add;
    }

    return {
        //数据实体
        crateDataEntity: function () {
            var dataEntity = new hwDataEntity();
            leeHelper.setUserData(dataEntity);
            return dataEntity;
        },
        //配置数据实体
        createConfigDataEntity: function () {
            var dataEntity = new hwDataConfigEntity();
            leeHelper.setUserData(dataEntity);
            return dataEntity;
        }
    };
})();
//物料基础信息控制器
officeAssistantModule.controller('hwMaterialBaseInfoCtrl', function (hwDataOpService, $scope) {
    ///数据实体模型
    var dataVM = hwApiHelper.createConfigDataEntity();
    $scope.materialVM = {
        itemCategory: "",
        vendorItemCode: "",
        customerVendorCode: "157",
        leadTime: 0,
        vendorItemDesc: "",
        unitOfMeasure: "PCS",
        customerItemCode: "NA",
        vendorProductModel: "",
        customerProductModel: "",
        inventoryType: "FG",
        goodPercent: 0,
        lifeCycleStatus: "MP"
    };
    var initMaterialVM = _.clone($scope.materialVM);

    var editDialog = $scope.editDialog = leePopups.dialog();

    var vmManager = $scope.vmManager = {
        dataset: [],
        lifeCycleStatuses: [{ id: 'NPI', text: '量产前' }, { id: 'MP', text: '量产' }, { id: 'EOL', text: '停产' }],
        inventoryTypes: [{ id: 'FG', text: '成品' }, { id: 'FGSEMI-FG', text: '半成品' }, { id: 'RM', text: '原材料' }],
        materialId: null,
        dataEntity: null,
        init: function () {
            vmManager.dataEntity = null;
            vmManager.dataset = [];
            vmManager.materialId = null;
        },
        oldSelectedItem: null,
        //创建Dto对象
        createDto: function (materialVM) {
            var data = {
                vendorItemList: [materialVM]
            };
            var dto = _.clone(dataVM);
            dto.MaterialId = materialVM.vendorItemCode;
            dto.InventoryType = materialVM.inventoryType;
            dto.MaterialBaseDataContent = JSON.stringify(data);
            leeHelper.setObjectClentSign(dto);
            return dto;
        },
        getMaterialBaseInfo: function ($event) {
            if ($event.keyCode === 13) {
                $scope.searchPromise = hwDataOpService.getMaterialBaseInfo(vmManager.materialId).then(function (data) {
                    if (data.MaterialBaseDataContent !== null) {
                        vmManager.dataEntity = JSON.parse(data.MaterialBaseDataContent);
                        //给每个实体添加键值
                        leeHelper.setObjectsGuid(vmManager.dataEntity.vendorItemList);
                        //设置服务器端的数据项
                        leeHelper.setObjectServerSign(data);
                        data.OpSign = 'init';
                        vmManager.dataset.push(data);
                    }
                });
            }
        },
        showEditWindow: function (item) {
            vmManager.oldSelectedItem = _.clone(item);
            $scope.materialVM = item;
            editDialog.show();
        },
        refreshDataInDataset: function (dataItem, opSign) {
            dataItem.OpSign = opSign;
            var item = _.find(vmManager.dataset, { MaterialId: dataItem.MaterialId });
            if (item !== undefined) {
                if (opSign === leeDataHandler.dataOpMode.delete) {
                    if (!leeHelper.isServerObject(item))
                        leeHelper.remove(vmManager.dataset, item);
                    else {
                        leeHelper.setObjectServerSign(dataItem);
                        leeHelper.copyVm(dataItem, item);
                    }
                }
                else {
                    leeHelper.copyVm(dataItem, item);
                }
            }
            else {
                if (opSign === leeDataHandler.dataOpMode.add)
                    vmManager.dataset.push(dataItem);
            }


        },
        confirmEditData: function () {
            var dataItem = vmManager.createDto($scope.materialVM);
            if ($scope.materialVM.isAdd) {
                leeHelper.setObjectGuid($scope.materialVM);
                vmManager.refreshDataInDataset(dataItem, leeDataHandler.dataOpMode.add);
                if (vmManager.dataEntity === null) {
                    vmManager.dataEntity = {
                        vendorItemList: [$scope.materialVM]
                    };
                }
                else {
                    var isExistData = _.find(vmManager.dataEntity.vendorItemList, { vendorItemCode: $scope.materialVM.vendorItemCode });
                    if (_.isUndefined(isExistData)) {
                        vmManager.dataEntity.vendorItemList.push($scope.materialVM);
                    }
                    else {
                        leePopups.alert($scope.materialVM.vendorItemCode + "已经添加过了！");
                    }
                }

                delete $scope.materialVM.isAdd;
            }
            else {
                vmManager.refreshDataInDataset(dataItem, leeDataHandler.dataOpMode.edit);
            }
            editDialog.close();
        },
        cancelEditData: function () {
            if (vmManager.dataEntity !== null)
                leeDataHandler.dataOperate.cancelEditItem(vmManager.oldSelectedItem, vmManager.dataEntity.vendorItemList);
            editDialog.close();
        },
        addMaterialInfo: function () {
            $scope.materialVM = _.clone(initMaterialVM);
            $scope.materialVM.isAdd = true;
            editDialog.show();
        },
        removeMaterial: function (item) {
            leePopups.inquire("删除提示", "您确认要删除数据吗?", function () {
                $scope.$apply(function () {
                    leeHelper.delWithId(vmManager.dataEntity.vendorItemList, item);
                    var dataItem = vmManager.createDto(item);
                    vmManager.refreshDataInDataset(dataItem, leeDataHandler.dataOpMode.delete);
                });
            });
        }
    };

    var operate = $scope.operate = Object.create(leeDataHandler.operateStatus);

    operate.save = function () {
        leeDataHandler.dataOperate.add(operate, true, function () {
            if (vmManager.dataset.length > 0) {
                $scope.opPromise = hwDataOpService.saveMaterialBase(vmManager.dataset).then(function (opresult) {
                    leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                        vmManager.init();
                    });
                });
            }
        });
    };
});
//关键物料BOM信息控制器
officeAssistantModule.controller('hwMaterialBomInfoCtrl', function (hwDataOpService, $scope) {
    ///数据实体模型
    var dataVM = hwApiHelper.createConfigDataEntity();

    $scope.materialVM = {
        vendorItemCode: null,
        subItemCode: null,
        substituteGroup: null,
        baseUsedQuantity: 1,
        standardQuantity: 1
    };
    var initMaterialVM = _.clone($scope.materialVM);

    var editDialog = $scope.editDialog = leePopups.dialog();

    var vmManager = $scope.vmManager = {
        materialId: null,
        dataEntity: null,
        init: function () {
            vmManager.dataEntity = null;
            vmManager.materialId = null;
            vmManager.masterParentMaterialId = null;
        },
        //主父阶料号编码
        masterParentMaterialId: null,
        oldSelectedItem: null,
        //检测物料编码
        checkMaterialId: function (vendorItemCode) {
            if (vmManager.masterParentMaterialId === null || vmManager.masterParentMaterialId.length < 5) {
                vmManager.masterParentMaterialId = vendorItemCode;
                return true;
            }
            else {
                if (vmManager.masterParentMaterialId !== vendorItemCode) {
                    leePopups.alert("新增加的父阶物料编码与已经增加的父阶物料编码：" + vmManager.masterParentMaterialId + "不相同！", 2);
                    return false;
                }
                else {
                    return true;
                }
            }
        },
        getMaterialBomInfo: function ($event) {
            if ($event.keyCode === 13) {
                $scope.searchPromise = hwDataOpService.getMaterialBaseInfo(vmManager.materialId).then(function (data) {
                    if (data.MaterialBomDataContent !== null && data.MaterialBomDataContent.length > 20) {
                        vmManager.dataEntity = JSON.parse(data.MaterialBomDataContent);
                        //给每个实体添加键值
                        leeHelper.setObjectsGuid(vmManager.dataEntity.keyMaterialList);
                    }
                });
            }
        },
        showEditWindow: function (item) {
            vmManager.oldSelectedItem = _.clone(item);
            $scope.materialVM = item;
            editDialog.show();
        },
        showCopyWindow: function (item) {
            vmManager.oldSelectedItem = _.clone(item);
            $scope.materialVM = _.clone(item);
            leeHelper.clearVM($scope.materialVM, ['vendorItemCode', 'baseUsedQuantity']);
            $scope.materialVM.isAdd = true;
            editDialog.show();
        },
        confirmEditData: function () {
            if ($scope.materialVM.isAdd) {
                if (!vmManager.checkMaterialId($scope.materialVM.vendorItemCode)) return;

                leeHelper.setObjectGuid($scope.materialVM);
                if (vmManager.dataEntity === null || vmManager.dataEntity.keyMaterialList === null) {
                    vmManager.dataEntity = {
                        keyMaterialList: [$scope.materialVM]
                    };
                }
                else {
                    vmManager.dataEntity.keyMaterialList.push($scope.materialVM);
                }
                delete $scope.materialVM.isAdd;
            }
            editDialog.close();
        },
        cancelEditData: function () {
            if (vmManager.dataEntity !== null)
                leeDataHandler.dataOperate.cancelEditItem(vmManager.oldSelectedItem, vmManager.dataEntity.vendorItemList);
            editDialog.close();
        },
        addMaterialBomInfo: function () {
            $scope.materialVM = _.clone(initMaterialVM);
            $scope.materialVM.isAdd = true;
            editDialog.show();
        },
        removeMaterial: function (item) {
            leePopups.inquire("删除提示", "您确认要删除数据吗?", function () {
                $scope.$apply(function () {
                    leeHelper.delWithId(vmManager.dataEntity.keyMaterialList, item);
                    vmManager.masterParentMaterialId = item.vendorItemCode;
                });
            });
        }
    };

    var operate = $scope.operate = Object.create(leeDataHandler.operateStatus);

    operate.save = function () {
        leeDataHandler.dataOperate.add(operate, true, function () {
            if (vmManager.dataEntity !== null && vmManager.dataEntity.keyMaterialList !== null && vmManager.dataEntity.keyMaterialList.length > 0) {
                dataVM.MaterialId = vmManager.dataEntity.keyMaterialList[0].vendorItemCode;
                dataVM.MaterialBomDataContent = JSON.stringify(vmManager.dataEntity);
            }
            else {
                dataVM.MaterialId = vmManager.masterParentMaterialId;
                dataVM.MaterialBomDataContent = "";
            }
            $scope.opPromise = hwDataOpService.saveMaterialBom(dataVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    vmManager.init();
                })
            });
        })
    };
});
//人力信息控制器
officeAssistantModule.controller('hwManPowerCtrl', function (hwDataOpService, dataDicConfigTreeSet, $scope) {
    ///数据实体模型
    var dataVM = hwApiHelper.crateDataEntity();
    $scope.manPowerVM = {
        vendorFactoryCode: "421072-001",
        manpowerAddQuantity: 0,
        manpowerGapQuantity: 0,
        hrLeavePercent: 0.0,
        manpowerTotalQuantity: 0,
    };
    $scope.depManPowerVM = {
        keyDeptName: "",
        hrAddQuantity: 0,
        hrGapQuantity: 0,
        hrLeavePercent: 0.0,
        hrRequestQuantity: 0,
        description: ""
    };
    var initDepManPowerVM = _.clone($scope.depManPowerVM);

    var manPowerEditDialog = $scope.manPowerEditDialog = leePopups.dialog();
    var manDetailEditDialog = $scope.manDetailEditDialog = leePopups.dialog();

    var vmManager = $scope.vmManager = {
        dataEntity: null,
        oldManPower: null,
        oldDepartmentManPower: null,
        getManPower: function () {
            $scope.searchPromise = hwDataOpService.getManPower().then(function (data) {
                vmManager.dataEntity = JSON.parse(data.entity.OpContent);
                departmentTreeSet.setTreeDataset(data.departments);
                //给每个实体添加键值
                leeHelper.setObjectsGuid(vmManager.dataEntity.manpowerMainList);
                angular.forEach(vmManager.dataEntity.manpowerMainList, function (item) {
                    leeHelper.setObjectsGuid(item.keyDeptDataList);
                })

                console.log(vmManager.dataEntity);
            });
        },
        //-------body-------------
        showMasterEditWindow: function (item) {
            vmManager.oldManPower = _.clone(item);
            $scope.manPowerVM = item;
            manPowerEditDialog.show();
        },
        confirmMasterEditData: function () {
            manPowerEditDialog.close();
        },
        cancelMasterEditData: function () {
            leeDataHandler.dataOperate.cancelEditItem(vmManager.oldManPower, vmManager.dataEntity.manpowerMainList);
            manPowerEditDialog.close();
        },
        //---------head-----------
        showDetailEditWindow: function (item) {
            vmManager.oldDepartmentManPower = _.clone(item);
            $scope.depManPowerVM = item;
            manDetailEditDialog.show();
        },
        confirmDetailEditData: function () {
            if ($scope.depManPowerVM.isAdd) {
                leeHelper.setObjectGuid($scope.depManPowerVM);
                var isExistData = _.find(vmManager.dataEntity.manpowerMainList[0].keyDeptDataList, { keyDeptName: $scope.depManPowerVM.keyDeptName });
                if (_.isUndefined(isExistData)) {
                    vmManager.dataEntity.manpowerMainList[0].keyDeptDataList.push($scope.depManPowerVM);
                }
                else {
                    leePopups.alert($scope.depManPowerVM.keyDeptName + "已经添加过了！");
                }
                delete $scope.depManPowerVM.isAdd;
            }
            manDetailEditDialog.close();
        },
        cancelDetailEditData: function () {
            leeDataHandler.dataOperate.cancelEditItem(vmManager.oldDepartmentManPower, vmManager.dataEntity.manpowerMainList[0].keyDeptDataList);
            manDetailEditDialog.close();
        },
        addDepartmentManPower: function () {
            $scope.depManPowerVM = _.clone(initDepManPowerVM);
            $scope.depManPowerVM.isAdd = true;
            manDetailEditDialog.show();
        },
        removeDepartmentManPower: function (item) {
            leePopups.inquire("删除提示", "您确认要删除数据吗?", function () {
                $scope.$apply(function () {
                    leeHelper.delWithId(vmManager.dataEntity.manpowerMainList[0].keyDeptDataList, item);
                });
            });
        },
    };

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        $scope.depManPowerVM.keyDeptName = dto.DataNodeName;
    };
    $scope.ztree = departmentTreeSet;

    var operate = $scope.operate = Object.create(leeDataHandler.operateStatus);
    operate.save = function () {
        leeDataHandler.dataOperate.add(operate, true, function () {
            dataVM.OpContent = JSON.stringify(vmManager.dataEntity);
            $scope.opPromise = hwDataOpService.saveManPower(dataVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () { })
            });
        })
    };
    vmManager.getManPower();
});
//物料信息控制器
officeAssistantModule.controller('hwMaterialManageCtrl', function (hwDataOpService, $scope) {
    ///数据实体模型
    var dataInventoryVM = hwApiHelper.crateDataEntity();

    var vmManager = $scope.vmManager = {
        activeTab: 'HwInventoryDetailTab',
        inventoryDataEntity: null,
        makingDataEntity: null,
        shipmentDataEntity: null,
        getMaterialDatas: function () {
            $scope.searchPromise = hwDataOpService.getMaterialDetails().then(function (data) {
                vmManager.inventoryDataEntity = data.inventoryEntity;
                vmManager.makingDataEntity = data.makingEntity;
                vmManager.shipmentDataEntity = data.shippingEntity;
            });
        },
    };

    var operate = $scope.operate = Object.create(leeDataHandler.operateStatus);
    operate.saveInventoryData = function () {
        leeDataHandler.dataOperate.add(operate, true, function () {
            dataInventoryVM.OpContent = JSON.stringify(vmManager.inventoryDataEntity);
            $scope.opPromise = hwDataOpService.saveMaterialInventory(dataInventoryVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () { })
            });
        })
    };
    vmManager.getMaterialDatas();
});