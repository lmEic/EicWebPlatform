/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var smModule = angular.module('bpm.sysmanageApp');
smModule.factory('sysConfigService', function ($http, $q) {
    var config = {};
    var urlPrefix = '/' + leeHelper.controllers.configManage + '/';

    config.getConfigDicData = function (treeModuleKey) {
        var defer = $q.defer();
        var url = urlPrefix + "GetConfigDicData";
        $http.get(url, {
            params: {
                treeModuleKey: treeModuleKey,
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    ///根据模块名称与所属类别载入配置数据
    config.loadConfigDicData = function (moduleName, aboutCategory) {
        var defer = $q.defer();
        var url = urlPrefix + "LoadConfigDicData";
        $http.get(url, {
            params: {
                moduleName: moduleName,
                aboutCategory: aboutCategory,
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    ///根据树的键值载入配置数据
    config.saveConfigDicData = function (vm, oldVm, opType) {
        var defer = $q.defer();
        var url = urlPrefix + "SaveConfigDicData";
        $http.post(url, {
            opType: opType,
            model: vm,
            oldModel: oldVm
        }).success(function (data) {
            defer.resolve(data);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };

    return config;
});
smModule.controller('DepartmentSetCtrl', function ($scope, $modal, dataDicConfigTreeSet, sysConfigService) {
    var departmentDto = {
        TreeModuleKey: 'Organization',
        ModuleName: 'smconfigManage',
        DataNodeName: null,
        DataNodeText: null,
        ParentDataNodeText: null,
        IsHasChildren: 0,
        AtLevel: 0,
        AboutCategory: 'smDepartmentSet',
        Icon: null,
        DisplayOrder: 0,
        Memo: null,
    };
    var oldDepartmentDto = _.clone(departmentDto);

    $scope.vm = departmentDto;

    var operate = Object.create(leeDataHandler.operateStatus);
    operate.vm = departmentDto;
    $scope.operate = operate;

    operate.delNode = function () {
        if (angular.isUndefined(departmentTreeSet.treeNode) || departmentTreeSet.treeNode == null) {
            alert("请先选择要删除的节点!")
        }
        else {
            operate.deleteModal.$promise.then(operate.deleteModal.show);
        }
    };
    operate.addChildNode = function (isValid) {
        saveDataDicNode(isValid, 'add', 'addChildNode');
    };
    operate.addNode = function (isValid) {
        saveDataDicNode(isValid, 'add', 'addNode');
    };
    operate.updateNode = function (isValid) {
        saveDataDicNode(isValid, 'edit', 'updateNode');
    };

    var saveDataDicNode = function (isValid, opType, opNodeType) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            sysConfigService.saveConfigDicData(departmentDto, oldDepartmentDto, opType).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var vm = _.clone($scope.vm);
                        if (opType === 'add') {
                            vm.Id_Key = opresult.Id_Key;
                            var newNode = {
                                name: vm.DataNodeText,
                                children: [],
                                vm: vm
                            };
                            if (opNodeType === "addChildNode")
                                leeTreeHelper.addChildrenNode(departmentTreeSet.treeId, departmentTreeSet.treeNode, newNode);
                            else if (opNodeType === "addNode")
                                leeTreeHelper.addNode(departmentTreeSet.treeId, departmentTreeSet.treeNode, newNode);
                        }
                            //修改节点
                        else if (opType === 'edit') {
                            if (opNodeType === "updateNode") {
                                departmentTreeSet.treeNode.name = vm.DataNodeText;
                                departmentTreeSet.treeNode.vm = vm;
                                var childrens = departmentTreeSet.treeNode.children;
                                angular.forEach(childrens, function (childrenNode) {
                                    childrenNode.vm.ParentDataNodeText = vm.DataNodeText;
                                })
                                leeTreeHelper.updateNode(departmentTreeSet.treeId, departmentTreeSet.treeNode);
                            }
                        }
                        pHelper.clearVM();
                    }
                });
            });
        });
    };
    //刷新操作
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            pHelper.clearVM();
        });
    };

    operate.deleteModal = $modal({
        title: "删除提示",
        content: "你确定要删除此节点数据吗?",
        templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
        controller: function ($scope) {
            $scope.confirmDelete = function () {
                sysConfigService.saveConfigDicData(departmentDto, oldDepartmentDto, 'delete').then(function (opresult) {
                    if (opresult.Result) {
                        operate.deleteModal.$promise.then(operate.deleteModal.hide);
                        leeTreeHelper.removeNode(departmentTreeSet.treeId, departmentTreeSet.treeNode);
                        operate.refresh();
                    }
                })
            };
        },
        show: false,
    });
    var pHelper = {
        clearVM: function () {
            leeHelper.clearVM(departmentDto, ['ModuleName', 'AboutCategory', 'TreeModuleKey']);
        },
    };
    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        departmentDto = _.clone(departmentTreeSet.treeNode.vm);
        departmentDto.ModuleName = "smconfigManage";
        departmentDto.AboutCategory = "smDepartmentSet";
        oldDepartmentDto = _.clone(departmentDto);
        $scope.vm = departmentDto;
    };
    $scope.ztree = departmentTreeSet;

    $scope.promise = sysConfigService.getConfigDicData(departmentDto.TreeModuleKey).then(function (datas) {
        departmentTreeSet.setTreeDataset(datas);
    });
})
smModule.controller('CommonConfigSetCtrl', function ($scope, $modal, dataDicConfigTreeSet, sysConfigService) {
    var configDto = {
        TreeModuleKey: 'CommonConfigDataSet',
        ModuleName: null,
        DataNodeName: null,
        DataNodeText: null,
        ParentDataNodeText: null,
        IsHasChildren: 0,
        AtLevel: 0,
        AboutCategory: null,
        Icon: null,
        DisplayOrder: 0,
        Memo: null,
        HasChildren: false
    };
    var oldConfigDto = _.clone(configDto);

    $scope.vm = configDto;

    $scope.CheckIsHasChildren = function () {
        configDto.IsHasChildren = configDto.HasChildren === true ? 1 : 0;
    };

    var operate = Object.create(leeDataHandler.operateStatus);
    operate.vm = configDto;
    $scope.operate = operate;

    operate.delNode = function () {
        if (angular.isUndefined(commonConfigTreeSet.treeNode) || commonConfigTreeSet.treeNode == null) {
            alert("请先选择要删除的节点!")
        }
        else {
            operate.deleteModal.$promise.then(operate.deleteModal.show);
        }
    };
    operate.addChildNode = function (isValid) {
        saveDataDicNode(isValid, 'add', 'addChildNode');
    };
    operate.addNode = function (isValid) {
        saveDataDicNode(isValid, 'add', 'addNode');
    };
    operate.updateNode = function (isValid) {
        saveDataDicNode(isValid, 'edit', 'updateNode');
    };

    var saveDataDicNode = function (isValid, opType, opNodeType) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            sysConfigService.saveConfigDicData(configDto, oldConfigDto, opType).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var vm = _.clone($scope.vm);
                        if (opType === 'add') {
                            vm.Id_Key = opresult.Id_Key;
                            var newNode = {
                                name: vm.DataNodeText,
                                children: [],
                                vm: vm
                            };
                            if (opNodeType === "addChildNode")
                                leeTreeHelper.addChildrenNode(commonConfigTreeSet.treeId, commonConfigTreeSet.treeNode, newNode);
                            else if (opNodeType === "addNode")
                                leeTreeHelper.addNode(commonConfigTreeSet.treeId, commonConfigTreeSet.treeNode, newNode);
                        }
                            //修改节点
                        else if (opType === 'edit') {
                            if (opNodeType === "updateNode") {
                                commonConfigTreeSet.treeNode.name = vm.DataNodeText;
                                commonConfigTreeSet.treeNode.vm = vm;
                                var childrens = commonConfigTreeSet.treeNode.children;
                                angular.forEach(childrens, function (childrenNode) {
                                    childrenNode.vm.ParentDataNodeText = vm.DataNodeText;
                                })
                                leeTreeHelper.updateNode(commonConfigTreeSet.treeId, commonConfigTreeSet.treeNode);
                            }
                        }
                        pHelper.clearVM();
                    }
                });
            });
        });
    };

    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            pHelper.clearVM();
        });
    };


    var pHelper = {
        clearVM: function () {
            leeHelper.clearVM(configDto, ['TreeModuleKey']);
        },
    };

    operate.deleteModal = $modal({
        title: "删除提示",
        content: "你确定要删除此节点数据吗?",
        templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
        controller: function ($scope) {
            $scope.confirmDelete = function () {
                sysConfigService.saveConfigDicData(configDto, oldConfigDto, 'delete').then(function (opresult) {
                    if (opresult.Result) {
                        operate.deleteModal.$promise.then(operate.deleteModal.hide);
                        leeTreeHelper.removeNode(commonConfigTreeSet.treeId, commonConfigTreeSet.treeNode);
                        operate.refresh();
                    }
                })
            };
        },
        show: false,
    });

    var commonConfigTreeSet = dataDicConfigTreeSet.getTreeSet('commonConfigTree', '数据配置字典');
    commonConfigTreeSet.bindNodeToVm = function () {
        configDto = _.clone(commonConfigTreeSet.treeNode.vm);
        oldConfigDto = _.clone(configDto);
        $scope.vm = configDto;
    };
    $scope.ztree = commonConfigTreeSet;

    $scope.promise = sysConfigService.getConfigDicData(configDto.TreeModuleKey).then(function (datas) {
        commonConfigTreeSet.setTreeDataset(datas);
    });
})