/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var officeAssistantModule = angular.module('bpm.toolsOnlineApp');
officeAssistantModule.factory('hwDataOpService', function (ajaxService) {
    var hwDataOp = {};
    var hwUrlPrefix = '/' + leeHelper.controllers.TolCooperateWithHw + '/';
    //获取人力信息
    hwDataOp.getManPower = function () {
        var url = hwUrlPrefix + 'GetManPower';
        return ajaxService.getData(url);
    };
    //保存人力信息数据
    hwDataOp.saveManPower = function (entity) {
        var url = hwUrlPrefix + 'SaveManPower';
        return ajaxService.postData(url, {
            entity: entity,
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
    };

    return {
        //数据实体
        crateDataEntity: function () {
            var dataEntity = new hwDataEntity();
            leeHelper.setUserData(dataEntity);
            return dataEntity;
        },
    };
})();

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
        keyDeptName: "部门1",
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