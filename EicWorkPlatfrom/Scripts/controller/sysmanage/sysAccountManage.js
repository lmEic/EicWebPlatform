/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var smModule = angular.module('bpm.sysmanageApp');
smModule.factory('accountService', function ($http, $q) {
    var account = {};
    var urlPrefix = '/Account/';

    var getData = function (url, para) {
        var defer = $q.defer();
        $http.get(url, { params: para }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    var postData = function (url, para) {
        var defer = $q.defer();
        $http.post(url, para).success(function (data) {
            defer.resolve(data);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };

    account.getRoles = function () {
        return getData(urlPrefix + 'GetRoles', {});
    };
    account.addRole = function (vm) {
        return postData(urlPrefix + 'AddRole', {
            role: vm
        });
    };
    account.addUser = function (user) {
        return postData(urlPrefix + 'AddUser', {
            user: user
        });
    };
    account.findUserById = function (userId) {
        return getData(urlPrefix + 'GetUserById', {
            userId: userId
        });
    };
    account.addAssembly = function (assemblyvm) {
        return postData(urlPrefix + 'AddAssembly', {
            assembly: assemblyvm
        });
    };
    account.getAssemblies = function () {
        return getData(urlPrefix + 'GetAssemblies', {});
    };
    account.saveRoleMatchModule = function (roleMatchModulevm) {
        return postData(urlPrefix + 'SaveRoleMatchModuleData', {
            mdls: roleMatchModulevm
        });
    };
    account.getNavMenus = function () {
        return getData(urlPrefix + 'GetNavMenus', {});
    };
    account.getNavMenusAndRoles = function () {
        return getData(urlPrefix + 'GetNavsAndRoles', {});
    };
    account.saveModuleNavData = function (vm, oldVm, opType) {
        return postData(urlPrefix + 'SaveModuleNavData', {
            opType: opType,
            moduleNav: vm,
            oldModuleNav: oldVm,
        });
    };
    account.findRoleMatchModules = function (assemblyName, moduleName, ctrlName) {
        return getData(urlPrefix + 'FindRoleMatchModules', {
            assemblyName: assemblyName,
            moduleName: moduleName,
            ctrlName: ctrlName,
        });
    }
    account.findRoleMatchModulesBy = function (roleId) {
        return getData(urlPrefix + 'FindRoleMatchModulesBy', {
            roleId: roleId,
        });
    }

    account.getUserIdentityByUserId = function (userId) {
        return getData(urlPrefix + 'GetUserIdentityByUserId', {
            userId: userId
        });
    };
    account.findUserRolesByUserId = function (userId) {
        return getData(urlPrefix + 'FindUserMatchRolesByUserId', {
            userId: userId
        });
    };
    account.addMatchRoles = function (matchRoles) {
        return postData(urlPrefix + 'AddMatchRoles', {
            matchRoles: matchRoles
        });
    };

    return account;
})
smModule.factory('treeSetService', function () {
    var createTreeDataset = function (datas, root) {
        var treeNodes = [];
        var childrenNodes = _.where(datas, { ParentModuleName: root });
        if (childrenNodes !== undefined && childrenNodes.length > 0) {
            angular.forEach(childrenNodes, function (node) {
                var trnode = {
                    name: node.ModuleText,
                    children: createTreeDataset(datas, node.ModuleText),
                    vm: node
                };
                treeNodes.push(trnode);
            });
        }
        return treeNodes;
    };
    var zTreeSet = {
        treeId: 'navModuleTree',
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
smModule.factory('vmService', function () {
    var viewModel = {
        moduleNavVm: {
            AssemblyName: null,
            ModuleName: null,
            ModuleText: null,
            CtrlName: null,
            ModuleType: null,
            ParentModuleName: null,
            ActionName: null,
            ClientMode: null,
            UiSerf: null,
            TemplateID: null,
            AtLevel: 0,
            Icon: null,
            DisplayOrder: 0,
        },
    };
    return viewModel;
})

smModule.directive('ngBlur', function ($parse) {
    return function (scope, element, attr) {
        var fn = $parse(attr['ngBlur']);

        $(element).on('focusout', function (event) {
            fn(scope, { $event: event });
        });
    }
})
smModule.directive('ngFocus', function () {
    return {
        restrict: "AE",
        require: "ngModel",
        link: function (scope, element, attrs, ctrl) {
            var FOCUS_CLASS = "ng-focused";
            ctrl.$focused = false;
            element.bind('focus', function (evt) {
                element.addClass(FOCUS_CLASS);
                scope.$apply(function () {
                    ctrl.$focused = true;
                });
            }).bind('blur', function (evt) {
                element.removeClass(FOCUS_CLASS);
                scope.$apply(function () {
                    ctrl.$focused = false;
                });
            });
        },
    };
})
smModule.directive('ensureUserUnique', function (accountService) {
    return {
        require: '^ngModel',
        link: function (scope, element, attrs, ngModel) {
            var setAsChecking = function (bool) {
                ngModel.$setValidity('checkingAvailability', !bool);
            };
            var setAsUserName = function (bool) {
                ngModel.$setValidity('usernameAvailability', bool);
            };
            ngModel.$parsers.push(function (val) {
                if (!val || val.length <= 5) {
                    return;
                }
                setAsChecking(true);
                setAsUserName(false);
                accountService.findUserById(val).then(function (data) {
                    if (angular.isObject(data)) {
                        setAsChecking(false);
                        setAsUserName(false);
                    } else {
                        setAsChecking(false);
                        setAsUserName(true);
                    }
                }, function (errordata) {
                    setAsChecking(false);
                    setAsUserName(true);
                });
                return val;
            });
        }
    };
})

//-----------------用户管理---------------------
smModule.controller('registUserCtrl', function ($scope, accountService) {
    var registvm = {
        userId: null,
        password: null
    };
    $scope.vm = registvm;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

    operate.addUser = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            accountService.addUser(registvm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    pHelper.clearVM();
                });
            });
        })
    };
    operate.cancel = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            pHelper.clearVM();
        });
    };

    var pHelper = {
        clearVM: function () {
            leeHelper.clearVM(registvm);
        }
    };
})
smModule.controller('addUserMatchRoleCtrl', function ($scope, accountService) {
    var userVm = {
        UserId: null,
        UserName: null,
        RoleId: null,
        RoleName: null,
        RoleLevel: 0,
        OpSign: null,
        isSelect: false,
    };

    $scope.vm = userVm;

    var addRowToDataset = function (role) {
        var datarow = _.clone(userVm);
        leeHelper.copyVm(role, datarow, ['UserId', 'UserName']);
        vmManager.dataset.push(datarow);
    };

    var vmManager = {
        roles: [],
        isSelectAll: false,
        findUserByUserId: function ($event) {
            if ($event.keyCode === 13) {
                if (userVm.UserId === null) return;
                vmManager.matchRoles = [];
                vmManager.dataset = [];
                $scope.promise = accountService.getUserIdentityByUserId(userVm.UserId).then(function (data) {
                    leeHelper.copyVm(data.LoginedUser, userVm);
                    angular.forEach(data.MatchRoleList, function (item) {
                        var role = _.clone(userVm);
                        leeHelper.copyVm(item, role);
                        role.OpSign = 'init';
                        vmManager.matchRoles.push(role);
                        vmManager.dataset.push(role);
                        var r = _.find(vmManager.roles, { RoleId: role.RoleId });
                        if (r !== undefined) {
                            r.isSelect = true;
                        }
                    })
                });
            }
        },
        selectAll: function () {
            angular.forEach(vmManager.roles, function (role) {
                role.isSelect = !vmManager.isSelectAll;
                vmManager.selectRole(role);
            });
        },
        selectRole: function (role) {
            var item = _.find(vmManager.matchRoles, { RoleId: role.RoleId });
            var dataitem = _.find(vmManager.dataset, { RoleId: role.RoleId });

            if (role.isSelect) {
                if (item === undefined) {
                    vmManager.matchRoles.push(role);
                }
                if (dataitem === undefined) {
                    role.OpSign = 'add';
                    addRowToDataset(role);
                }
                else {
                    if (dataitem.OpSign === 'delete') {
                        dataitem.OpSign = 'add';
                    }
                }
            }
            else {
                if (item !== undefined) {
                    leeHelper.remove(vmManager.matchRoles, item);
                }
                //若不存在，则添加到数据集队列中，标志位delete
                if (dataitem === undefined) {
                    role.OpSign = 'delete';
                    addRowToDataset(role);
                    vmManager.dataset.push(role);
                }
                else {
                    dataitem.OpSign = 'delete';
                }
            }
        },
        removeRole: function (role) {
            role.isSelect = false;
            vmManager.selectRole(role);

            var roleitem = _.find(vmManager.roles, { RoleId: role.RoleId });
            if (roleitem !== undefined) {
                roleitem.isSelect = false;
            }
        },
        //数据集
        dataset: [],
        matchRoles: [],
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);

    operate.saveMatchRoles = function () {
        leeDataHandler.dataOperate.add(operate, true, function () {
            $scope.promise = accountService.addMatchRoles(vmManager.dataset).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    angular.forEach(vmManager.roles, function (role) {
                        role.isSelect = false;
                    });
                    vmManager.dataset = [];
                    vmManager.matchRoles = [];
                    userVm.UserId = null;
                });
            });
        });
    };
    $scope.uservm = userVm;
    $scope.operate = operate;
    $scope.promise = accountService.getRoles().then(function (datas) {
        vmManager.roles = [];
        angular.forEach(datas, function (item) {
            var role = _.clone(userVm);
            leeHelper.copyVm(item, role);
            role.OpSign = 'init';
            vmManager.roles.push(role);
        });
    });
})
//------------------模块管理---------------------
smModule.controller('moduleNavManageCtrl', function ($scope, $modal, vmService, accountService, treeSetService) {
    var moduleNavVm = vmService.moduleNavVm;
    var oldModuleNavVm = _.clone(moduleNavVm);
    $scope.vm = moduleNavVm;

    var operate = Object.create(leeDataHandler.operateStatus);
    operate.vm = moduleNavVm;
    operate.nodeTypes = [
            { name: 'module', text: '模块' },
            { name: 'action', text: '操作' },
    ];
    operate.UiTypes = [
           { name: 'BS', text: 'B/S端' },
           { name: 'CS', text: 'C/S端' },
    ];
    operate.delNode = function () {
        if (angular.isUndefined(zTreeSet.treeNode) || zTreeSet.treeNode === null) {
            alert("请先选择要删除的节点!")
        }
        else {
            operate.deleteModal.$promise.then(operate.deleteModal.show);
        }
    };
    operate.addChildNode = function (isValid) {
        saveModuleNav(isValid, 'add', 'addChildNode');
    };
    operate.addNode = function (isValid) {
        saveModuleNav(isValid, 'add', 'addNode');
    };
    operate.updateNode = function (isValid) {
        saveModuleNav(isValid, 'edit', 'updateNode');
    };

    var saveModuleNav = function (isValid, opType, opNodeType) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            accountService.saveModuleNavData(moduleNavVm, oldModuleNavVm, opType).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var vm = _.clone($scope.vm);
                        if (opType === 'add') {
                            vm.Id_Key = opresult.Id_Key;
                            var newNode = {
                                name: vm.ModuleText,
                                children: [],
                                vm: vm
                            };
                            if (opNodeType === "addChildNode")
                                leeTreeHelper.addChildrenNode(zTreeSet.treeId, zTreeSet.treeNode, newNode);
                            else if (opNodeType === "addNode")
                                leeTreeHelper.addNode(zTreeSet.treeId, zTreeSet.treeNode, newNode);
                        }
                            //修改节点
                        else if (opType === 'edit') {
                            if (opNodeType === "updateNode") {
                                zTreeSet.treeNode.name = vm.ModuleText;
                                zTreeSet.treeNode.vm = vm;
                                var childrens = zTreeSet.treeNode.children;
                                angular.forEach(childrens, function (childrenNode) {
                                    childrenNode.vm.ParentModuleName = vm.ModuleText;
                                })
                                leeTreeHelper.updateNode(zTreeSet.treeId, zTreeSet.treeNode);
                            }
                        }
                        leeHelper.clearVM(moduleNavVm);
                    }
                });
            }, function (errResult) {
                alert(errResult);
            });
        });
    };
    operate.cancel = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            leeHelper.clearVM(moduleNavVm);
        });
    };

    operate.deleteModal = $modal({
        title: "删除提示",
        content: "你确定要删除此节点数据吗?",
        templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
        controller: function ($scope) {
            $scope.confirmDelete = function () {
                accountService.saveModuleNavData(moduleNavVm, oldModuleNavVm, 'delete').then(function (opresult) {
                    leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                        if (opresult.Result) {
                            operate.deleteModal.$promise.then(operate.deleteModal.hide);
                            leeTreeHelper.removeNode(zTreeSet.treeId, zTreeSet.treeNode);
                            leeHelper.clearVM(moduleNavVm);
                        }
                    });
                })
            };
        },
        show: false,
    }),

    $scope.operate = operate;

    var zTreeSet = treeSetService;
    zTreeSet.bindNodeToVm = function () {
        moduleNavVm = _.clone(zTreeSet.treeNode.vm);
        oldModuleNavVm = _.clone(zTreeSet.treeNode.vm);
        $scope.vm = moduleNavVm;      
    };
    $scope.ztree = zTreeSet;

    $scope.promise = accountService.getNavMenus().then(function (datas) {
        zTreeSet.setTreeDataset(datas);
    });
})
//------------------角色管理---------------------
smModule.controller('addRoleCtrl', function ($scope, accountService) {
    ///
    var uiVM = {
        RoleId: null,
        RoleName: null,
        RoleLevel: 0,
        RoleGroupName: null,
        ParentName: null,
        Memo: null,
        OpSign: 'add',
        Id_Key: null,
    }

    vmManager = {
        inti: function () {
            leeHelper.clearVM(uiVM);
            uiVM.OpSign = 'add';
            vmManager.canEdit = false;
        },
        canEdit: false,
        roles: [],
        roleGroups: [],
        selectRoleGroup: function (item) {
            uiVM.RoleGroupName = item.RoleGroupName;
        },
        selectRole: function (item) {
            vmManager.canEdit = true;
            uiVM = _.clone(item);
            uiVM.OpSign = 'edit';
            $scope.vm = uiVM;
        },
    };
    $scope.vm = uiVM;
    $scope.vmManager = vmManager;

    $scope.promise = accountService.getRoles().then(function (datas) {
        vmManager.roles = datas;
        vmManager.roleGroups = leeHelper.getUniqDatas(vmManager.roles, 'RoleGroupName');
    });
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

    operate.addRole = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            accountService.addRole(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    var role = _.clone(uiVM);
                    role.Id_Key = opresult.Id_Key;
                    if (role.OpSign === 'add') {
                        vmManager.roles.push(role);
                        vmManager.roleGroups = leeHelper.getUniqDatas(vmManager.roles, 'RoleGroupName');
                    }
                    else if (role.OpSign === 'edit') {
                        var current = _.find(vmManager.roles, { RoleId: role.RoleId });
                        if (current !== undefined)
                            leeHelper.copyVm(role, current);
                    }
                    vmManager.inti();
                });
            });
        })
    }
    operate.cancel = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.inti();
        });
    };
})
smModule.controller('assignUserToRoleCtrl', function ($scope, accountService) {
    var userVm = {
        UserId: null,
        UserName: null,
        RoleId: null,
        RoleName: null,
        RoleLevel: 0,
        OpSign: 'add',
    };

    $scope.vm = userVm;

    var addRowToDataset = function (role) {
        var datarow = _.clone(userVm);
        leeHelper.copyVm(role, datarow, ['UserId', 'UserName']);
        vmManager.dataset.push(datarow);
    };

    var vmManager = {
        roles: [],
        matchRoles: [],
        findUserByUserId: function ($event) {
            if ($event.keyCode === 13) {
                if (userVm.UserId === null || userVm.RoleId === null) {
                    alert("请先选择角色!");
                    return;
                }
                $scope.promise = accountService.findUserById(userVm.UserId).then(function (data) {
                    if (_.isObject(data)) {
                        userVm.UserId = data.UserId;
                        userVm.UserName = data.UserName;
                        userVm.OpSign = 'add';
                        vmManager.matchRoles.push(_.clone(userVm));
                        userVm.UserId = null;
                    }
                });
            }
        },
        selectRole: function (role) {
            leeHelper.copyVm(role, userVm);
        },
        removeRole: function (role) {
            leeHelper.remove(vmManager.matchRoles, role);
        }
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);

    operate.saveMatchRoles = function () {
        leeDataHandler.dataOperate.add(operate, true, function () {
            $scope.promise = accountService.addMatchRoles(vmManager.matchRoles).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    vmManager.matchRoles = [];
                });
            });
        });
    };
    $scope.operate = operate;
    $scope.promise = accountService.getRoles().then(function (datas) {
        vmManager.roles = [];
        angular.forEach(datas, function (item) {
            var role = _.clone(userVm);
            leeHelper.copyVm(item, role);
            vmManager.roles.push(role);
        });
    });
})
//------------------权限分配---------------------
smModule.controller("assignRolersToModuleCtrl", function ($scope, vmService, accountService, treeSetService) {
    var uiVm = {
        RoleId: null,
        RoleName: null,
        AssemblyName: null,
        ModuleName: null,
        ModuleText: null,
        CtrlName: null,
        ActionName: null,
        PrimaryKey: null,
        ModuleNavPrimaryKey: null,
        OpSign: null,
    };
    var roleVm = {
        RoleId: null,
        RoleName: null,
        RoleLevel: 0,
    };
    //树的配置
    var zTreeSet = treeSetService;
    zTreeSet.bindNodeToVm = function () {
        uiVm = _.clone(zTreeSet.treeNode.vm);
        vmManager.loadRoleMatchModules();
    };
    $scope.ztree = zTreeSet;
    var vmManager = $scope.vmManager = {
        roles: [],
        initRolesData: function (roles) {
            angular.forEach(roles, function (role) {
                var roleItem = _.clone(roleVm);
                leeHelper.copyVm(role, roleItem);
                roleItem.isCheck = false;
                vmManager.roles.push(roleItem);
            });
        },
        resetRoles: function () {
            vmManager.roles.forEach(function (r) { r.isCheck = false; });
        },
        dbDataset: [],
        loadRoleMatchModules: function () {
            $scope.searchPromise = accountService.findRoleMatchModules(uiVm.AssemblyName, uiVm.ModuleName, uiVm.CtrlName).then(function (datas) {
                if (angular.isArray(datas)) {
                    vmManager.dbDataset = [];
                    vmManager.resetRoles();
                    angular.forEach(datas, function (rm) {
                        var role = _.find(vmManager.roles, { RoleId: rm.RoleId });
                        if (!_.isUndefined(role)) {
                            role.isCheck = true;
                        }
                        leeHelper.setObjectGuid(rm);
                        leeHelper.setObjectServerSign(rm);
                        rm.OpSign = leeDataHandler.dataOpMode.none;
                        vmManager.dbDataset.push(rm);
                    });
                }

            });
        },
        selectRole: function (role) {
            var isChecked = role.isCheck;
            var vm = _.clone(zTreeSet.treeNode.vm);
            var dataItem = _.clone(uiVm);
            leeHelper.copyVm(vm, dataItem);

            dataItem.ModuleNavPrimaryKey = vm.PrimaryKey;
            dataItem.RoleId = role.RoleId;
            dataItem.RoleName = role.RoleName;
            dataItem.PrimaryKey = dataItem.ModuleNavPrimaryKey + "&" + dataItem.RoleId;
            leeHelper.setObjectGuid(dataItem);
            leeHelper.setObjectClentSign(dataItem);

            //处理数据库对应数据
            var dbItem = _.find(vmManager.dbDataset, { PrimaryKey: dataItem.PrimaryKey });
            if (_.isUndefined(dbItem)) {
                if (isChecked) {
                    dataItem.OpSign = leeDataHandler.dataOpMode.add;
                    vmManager.dbDataset.push(dataItem);
                }
            }
            else {
                if (!isChecked) {
                    if (leeHelper.isServerObject(dbItem))
                        dbItem.OpSign = leeDataHandler.dataOpMode.delete;
                    else
                        leeHelper.remove(vmManager.dbDataset, dbItem);
                }
            }
            console.log(vmManager.dbDataset);
        }
    };

    var operate = $scope.operate = Object.create(leeDataHandler.operateStatus);
    operate.saveDatas = function () {
        $scope.savePromise = accountService.saveRoleMatchModule(vmManager.dbDataset).then(function (opresult) {
            leeDataHandler.dataOperate.displayMessage(operate, opresult);
            vmManager.resetRoles();
            vmManager.dbDataset = [];
        });
    };
    $scope.promise = accountService.getNavMenusAndRoles().then(function (datas) {
        zTreeSet.setTreeDataset(datas.modules);
        vmManager.initRolesData(datas.roles);
    });
})
smModule.controller("assignModuleToRolersCtrl", function ($scope, vmService, accountService, treeSetService) {
    var uiVm = {
        RoleId: null,
        RoleName: null,
        AssemblyName: null,
        ModuleName: null,
        ModuleText: null,
        CtrlName: null,
        ActionName: null,
        PrimaryKey: null,
        ModuleNavPrimaryKey: null,
        OpSign: null,
    };
    var roleVm = {
        RoleId: null,
        RoleName: null,
        RoleLevel: 0,
    };
    $scope.vmManager = vmManager = {
        roles: [],
        setRoleCheckStatus: function (roles) {
            angular.forEach(roles, function (role) {
                var roleItem = _.clone(roleVm);
                leeHelper.copyVm(role, roleItem);
                roleItem.isCheck = false;
                vmManager.roles.push(roleItem);
            });
        },
        roleDisplay: true,
        treeDisplay: true,
        selectedRole: null,
        //对应数据的数据集
        dbDataset: [],
        //对应界面显示的数据集
        viewDataset: [],
        checkTreeNode: function (checked, mrole) {
            leeTreeHelper.checkAllNodes(zTreeSet.treeId, false);

            var treeNodes = leeTreeHelper.transformToArray(zTreeSet.treeId, leeTreeHelper.getTreeNodes(zTreeSet.treeId));
            angular.forEach(treeNodes, function (treenode) {
                var trnode = _.find(mrole.dataset, { ModuleName: treenode.vm.ModuleName, AssemblyName: treenode.vm.AssemblyName });
                if (trnode !== undefined) {
                    leeTreeHelper.checkNode(zTreeSet.treeId, treenode, true, false, false);
                }
            });
        },
        addToDbDataset: function (dataItem, isCheck) {
            var item = _.findWhere(vmManager.dbDataset, { PrimaryKey: dataItem.PrimaryKey });

            if (dataItem.ModuleText == "系统集成平台") {
                console.log(dataItem);
            }
            if (_.isUndefined(item))
                vmManager.dbDataset.push(dataItem);
            else {
                if (!isCheck) {
                    leeHelper.delWithId(vmManager.dbDataset, item);
                }
            }
        },
        selectRole: function (role) {
            var roleItem = _.findWhere(vmManager.viewDataset, { roleId: role.RoleId });
            if (_.isUndefined(roleItem)) {
                roleItem = { roleId: role.RoleId, role: role, dataset: [] };
                leeHelper.setObjectGuid(roleItem);
                vmManager.viewDataset.push(roleItem);
            }
            else {
                roleItem.dataset = [];
                if (!role.isCheck)
                    leeHelper.delWithId(vmManager.viewDataset, roleItem);
            }
            $scope.promise = accountService.findRoleMatchModulesBy(role.RoleId).then(function (datas) {
                angular.forEach(datas, function (item) {
                    var mroleItem = _.clone(uiVm);
                    leeHelper.copyVm(item, mroleItem);
                    leeHelper.setObjectGuid(mroleItem);
                    leeHelper.setObjectServerSign(mroleItem);
                    mroleItem.OpSign = leeDataHandler.dataOpMode.none;
                    mroleItem.RoleName = role.RoleName;
                    leeHelper.insertWithId(roleItem.dataset, mroleItem);
                    vmManager.addToDbDataset(mroleItem, role.isCheck);
                });
                vmManager.checkTreeNode(true, roleItem);
                vmManager.viewDataset.activePanel = vmManager.viewDataset.length - 1;
            })


        },
    };
    //树的配置
    var zTreeSet = treeSetService;
    zTreeSet.bindNodeToVm = function () { };
    //选择树的节点
    zTreeSet.checkNode = function () {
        var isChecked = zTreeSet.treeNode.isChecked;
        var vm = _.clone(zTreeSet.treeNode.vm);
        vmManager.viewDataset.forEach(function (moduleRole, index) {
            var mr = moduleRole;
            var dataItem = _.clone(uiVm);
            leeHelper.copyVm(vm, dataItem);
            dataItem.ModuleNavPrimaryKey = vm.PrimaryKey;
            dataItem.RoleId = mr.roleId;
            dataItem.RoleName = mr.role.RoleName;
            dataItem.PrimaryKey = dataItem.ModuleNavPrimaryKey + "&" + dataItem.RoleId;
            leeHelper.setObjectGuid(dataItem);
            leeHelper.setObjectClentSign(dataItem);
            var findItem = _.find(mr.dataset, { PrimaryKey: dataItem.PrimaryKey });
            //处理ＵＩ数据
            if (_.isUndefined(findItem)) {
                //如果是选中节点，则进行添加操作
                if (isChecked) {
                    dataItem.OpSign = leeDataHandler.dataOpMode.add;
                    vmManager.viewDataset[index].dataset.push(dataItem);
                }
            }
            else {
                if (!isChecked) {
                    leeHelper.delWithId(mr.dataset, findItem);
                }
            }

            //处理数据库对应数据
            var dbItem = _.find(vmManager.dbDataset, { PrimaryKey: dataItem.PrimaryKey });
            if (_.isUndefined(dbItem)) {
                if (isChecked) {
                    dataItem.OpSign = leeDataHandler.dataOpMode.add;
                    vmManager.dbDataset.push(dataItem);
                }
            }
            else {
                if (!isChecked) {
                    if (leeHelper.isServerObject(dbItem))
                        dbItem.OpSign = leeDataHandler.dataOpMode.delete;
                    else
                        leeHelper.remove(vmManager.dbDataset, dbItem);
                }
            }
        });
    };
    $scope.ztree = zTreeSet;

    var operate = $scope.operate = Object.create(leeDataHandler.operateStatus);
    operate.saveDatas = function () {
        $scope.savePromise = accountService.saveRoleMatchModule(vmManager.dbDataset).then(function (opresult) {
            leeDataHandler.dataOperate.displayMessage(operate, opresult);
            leeTreeHelper.checkAllNodes(zTreeSet.treeId, false);
            vmManager.dbDataset = [];
            vmManager.viewDataset = [];
            vmManager.roles.forEach(function (r) { r.isCheck = false; });
        });
    };
    $scope.promise = accountService.getNavMenusAndRoles().then(function (datas) {
        zTreeSet.setTreeDataset(datas.modules);
        vmManager.roles = [];
        vmManager.setRoleCheckStatus(datas.roles);
    });
})

