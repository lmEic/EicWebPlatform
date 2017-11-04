/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var hwCollaborationModule = angular.module('bpm.toolsOnlineApp');
hwCollaborationModule.factory('hwDataOpService', function (ajaxService) {
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

    //获取物料配置数据
    hwDataOp.getMaterialBaseConfigDatas = function () {
        var url = hwUrlPrefix + 'GetMaterialBaseConfigDatas';
        return ajaxService.getData(url, {
        });
    };
    //保存物料配置数据
    hwDataOp.saveMaterialBaseConfigDatas = function (entity) {
        var url = hwUrlPrefix + 'SaveMaterialBaseConfigDatas';
        return ajaxService.postData(url, {
            entity: entity,
        });
    };
    hwDataOp.autoSynchironizeData = function () {
        var url = hwUrlPrefix + 'AutoSynchironizeData';
        return ajaxService.getData(url);
    };



    //获取物料信息信息
    hwDataOp.getMaterialDetails = function () {
        var url = hwUrlPrefix + 'GetMaterialDetails';
        return ajaxService.getData(url);
    };
    //保存物料明细信息数据
    hwDataOp.saveMaterialDetailData = function (entity) {
        var url = hwUrlPrefix + 'SaveMaterialDetailData';
        return ajaxService.postData(url, {
            entity: entity
        });
    };
    return hwDataOp;
});
hwCollaborationModule.factory('hwTreeSetService', function () {
    var createTreeDataset = function (datas, root) {
        var treeNodes = [];
        var childrenNodes = _.where(datas, { ParentMaterialId: root });
        if (childrenNodes !== undefined && childrenNodes.length > 0) {
            angular.forEach(childrenNodes, function (node) {
                var trnode = {
                    name: node.MaterialId,
                    children: createTreeDataset(datas, node.MaterialId),
                    vm: node
                };
                treeNodes.push(trnode);
            });
        }
        return treeNodes;
    };
    var zTreeSet = {
        treeId: 'materialBaseConfigTree',
        navMenus: [],
        startLoad: false,
        setTreeDataset: function (datas) {
            zTreeSet.navMenus = createTreeDataset(datas, 'Root');
            zTreeSet.startLoad = true;
        },
        treeNode: null,
    };
    return zTreeSet;
})
hwCollaborationModule.directive('ylRefreshFrequency', function () {
    return {
        restrict: 'EA',
        template: '<span class="text-danger">【更新频率：每{{time}}】</span>',
        replace: true,
        scope: {
            time: '@',
        },
        link: function (scope, element, attrs) {

        }
    };
})
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
    return {
        //数据实体
        crateDataEntity: function () {
            var dataEntity = new hwDataEntity();
            leeHelper.setUserData(dataEntity);
            return dataEntity;
        },
    };
})();
//物料基础信息配置(含BOM)控制器
hwCollaborationModule.controller('hwMaterialBaseConfigCtrl', function (hwDataOpService, hwTreeSetService, $scope) {
    ///物料基础信息配置视图模型
    var materialBaseConfigVm = $scope.vm = {
        MaterialId: null,
        MaterialName: null,
        ParentMaterialId: null,
        DisplayOrder: 1000,
        VendorProductModel: null,
        VendorItemDesc: null,
        ItemCategory: null,
        CustomerVendorCode: '157',
        CustomerItemCode: null,
        CustomerProductModel: null,
        UnitOfMeasure: null,
        InventoryType: null,
        GoodPercent: null,
        LeadTime: 7,
        LifeCycleStatus: null,
        Quantity: 1,
        SubstituteGroup: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null,
    };
    var initVm = _.clone(materialBaseConfigVm);
    //视图管理器
    var vmManager = $scope.vmManager = {
        oldVM: _.clone(initVm),
        lifeCycleStatuses: [{ id: 'NPI', text: '量产前' }, { id: 'MP', text: '量产' }, { id: 'EOL', text: '停产' }],
        inventoryTypes: [{ id: 'FG', text: '成品' }, { id: 'FGSEMI-FG', text: '半成品' }, { id: 'RM', text: '原材料' }],
        isSelectedTreeNode: function () {
            if (angular.isUndefined(zTreeSet.treeNode) || zTreeSet.treeNode === null) {
                leePopups.alert("请先选择节点!", 2);
                return false;
            }
            return true;
        },
        opHandler: {
            setOpStatus: function (editable, opSign, actionName) {
                vmManager.opHandler.editable = editable;
                materialBaseConfigVm.OpSign = opSign;
                vmManager.opHandler.actionName = actionName;
                if (actionName === 'add' || actionName === 'addChildren') {
                    vmManager.opHandler.parentNodeEditable = !editable;
                }
                else {
                    vmManager.opHandler.parentNodeEditable = editable;
                }
            },
            //是否可编辑
            editable: true,
            parentNodeEditable: true,
            actionName: 'add',
            init: function () {
                leeHelper.clearVM(materialBaseConfigVm);
                vmManager.opHandler.editable = true;
                vmManager.opHandler.parentNodeEditable = true;
            },
            add: function () {
                if (!vmManager.isSelectedTreeNode()) return;
                vmManager.opHandler.setOpStatus(false, leeDataHandler.dataOpMode.add, 'add');
                var brotherNode = _.clone(vmManager.oldVM);
                materialBaseConfigVm = $scope.vm = _.clone(initVm);
                materialBaseConfigVm.ParentMaterialId = brotherNode.ParentMaterialId;
            },
            addChildren: function () {
                if (!vmManager.isSelectedTreeNode()) return;
                var parentNode = _.clone(vmManager.oldVM);
                materialBaseConfigVm = $scope.vm = _.clone(initVm);
                materialBaseConfigVm.ParentMaterialId = parentNode.MaterialId;
                vmManager.opHandler.setOpStatus(false, leeDataHandler.dataOpMode.add, 'addChildren');
            },
            edit: function () {
                if (!vmManager.isSelectedTreeNode()) return;

                vmManager.opHandler.setOpStatus(false, leeDataHandler.dataOpMode.edit, 'edit');
            },
            del: function () {
                if (!vmManager.isSelectedTreeNode()) return;
                if (zTreeSet.treeNode.vm.ParentMaterialId == "Root") {
                    leePopups.alert("您选择的是根节点，系统禁止删除根节点！", 1);
                    return;
                }
                leePopups.inquire("删除提示", "您确认要删除数据吗?", function () {
                    $scope.$apply(function () {
                        vmManager.opHandler.setOpStatus(true, leeDataHandler.dataOpMode.delete);
                        hwDataOpService.saveMaterialBaseConfigDatas(materialBaseConfigVm).then(function (opresult) {
                            leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                                if (opresult.Result) {
                                    leeTreeHelper.removeNode(zTreeSet.treeId, zTreeSet.treeNode);
                                    vmManager.opHandler.init();
                                }
                            });
                        })
                    });
                });
            },
        },
    };


    //数据操作器
    var operate = $scope.operate = Object.create(leeDataHandler.operateStatus);
    operate.cancel = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            leeHelper.clearVM(materialBaseConfigVm);
            vmManager.opHandler.editable = true;
        });
    };
    operate.save = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            leeHelper.setUserData(materialBaseConfigVm);
            $scope.opPromise = hwDataOpService.saveMaterialBaseConfigDatas(materialBaseConfigVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var opNodeType = vmManager.opHandler.actionName;
                        var opType = materialBaseConfigVm.OpSign;
                        var vm = _.clone($scope.vm);
                        if (opType === 'add') {
                            //vm.Id_Key = opresult.Id_Key;
                            var newNode = {
                                name: vm.MaterialId,
                                children: [],
                                vm: vm
                            };

                            if (opNodeType === "addChildren") {
                                leeTreeHelper.addChildrenNode(zTreeSet.treeId, zTreeSet.treeNode, newNode);
                            }
                            else if (opNodeType === "add") {
                                leeTreeHelper.addNode(zTreeSet.treeId, zTreeSet.treeNode, newNode);
                            }
                        }
                        else if (opType === 'edit') {//修改节点
                            if (opNodeType === "edit") {
                                zTreeSet.treeNode.name = vm.MaterialId;
                                zTreeSet.treeNode.vm = vm;
                                var childrens = zTreeSet.treeNode.children;

                                angular.forEach(childrens, function (childrenNode) {
                                    childrenNode.vm.ParentMaterialId = vm.MaterialId;
                                })

                                leeTreeHelper.updateNode(zTreeSet.treeId, zTreeSet.treeNode);
                            }
                        }

                        vmManager.opHandler.init();
                    }
                });
            }, function (errResult) {
                leePopups.alert(errResult);
            });
        });
    };
    operate.autoSynchironizeData = function () {
        leeDataHandler.dataOperate.add(operate, true, function () {
            $scope.opPromise = hwDataOpService.autoSynchironizeData().then(function (opResult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function (opResult) { });
            });
        });
    };
    //树结构
    var zTreeSet = hwTreeSetService;
    zTreeSet.bindNodeToVm = function () {
        vmManager.oldVM = _.clone(zTreeSet.treeNode.vm);
        $scope.vm = materialBaseConfigVm = _.clone(zTreeSet.treeNode.vm);
    };
    $scope.ztree = zTreeSet;

    $scope.promise = hwDataOpService.getMaterialBaseConfigDatas().then(function (datas) {
        zTreeSet.setTreeDataset(datas);
    });
});
//人力信息控制器
hwCollaborationModule.controller('hwManPowerCtrl', function (hwDataOpService, dataDicConfigTreeSet, $scope) {
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
hwCollaborationModule.controller('hwMaterialManageCtrl', function (hwDataOpService, $scope) {
    ///数据实体模型
    var datavm = hwApiHelper.crateDataEntity();
    //数据传输对象
    var dto = {
        InvertoryEntity: _.clone(datavm),
        MakingEntity: _.clone(datavm),
        ShippmentEntity: _.clone(datavm),
        PurchaseEntity: _.clone(datavm)
    };

    var vmManager = $scope.vmManager = {
        activeTab: 'HwInventoryDetailTab',
        opresults: null,
        opResultsViewDisplay: false,
        inventoryDataEntity: null,
        makingDataEntity: null,
        shipmentDataEntity: null,
        purchaseOnwayDataEntity: null,
        getMaterialDatas: function () {
            $scope.searchPromise = hwDataOpService.getMaterialDetails().then(function (data) {
                vmManager.inventoryDataEntity = data.inventoryEntity;
                vmManager.makingDataEntity = data.makingEntity;
                vmManager.shipmentDataEntity = data.shippingEntity;
                vmManager.purchaseOnwayDataEntity = data.purchaseEntity;
            });
        },
        setMaterailDtoOpContent: function () {
            dto.InvertoryEntity.OpContent = JSON.stringify(vmManager.inventoryDataEntity);
            dto.MakingEntity.OpContent = JSON.stringify(vmManager.makingDataEntity);
            dto.PurchaseEntity.OpContent = JSON.stringify(vmManager.purchaseOnwayDataEntity);
            dto.ShippmentEntity.OpContent = JSON.stringify(vmManager.shipmentDataEntity);
        }
    };

    var operate = $scope.operate = Object.create(leeDataHandler.operateStatus);
    $scope.OpPermise = operate.saveInventoryData = function () {
        vmManager.setMaterailDtoOpContent();
        $scope.opPromise = hwDataOpService.saveMaterialDetailData(dto).then(function (opResults) {
            vmManager.opresults = opResults;
            vmManager.opResultsViewDisplay = true;
            console.log(opResults);
        });
    };
    vmManager.getMaterialDatas();
});