/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />

angular.module('bpm.productApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap'])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);

    var boardUrlPrefix = leeHelper.controllers.productBoard + "/";

    $stateProvider.state('standardHoursConfig', {
        templateUrl: 'DailyReport/StandardHoursConfig',
    }).state('proStationConfig', {
        templateUrl: 'DailyReport/ProStationConfig',
    })
    //--------------人员管理--------------------------
    .state('registWorkerInfo', {
        templateUrl: 'ProEmployee/RegistWorkerInfo'
    })
    .state('proStationManage', {
        templateUrl: 'ProEmployee/ProStationManage'
    })
    .state('proClassManage', {
        templateUrl: 'ProEmployee/ProClassManage'
    })
    .state('workHoursManage', {
        templateUrl: 'ProEmployee/WorkHoursManage'
    })
    //-------------看板管理-------------------
    .state('jumperWireBoard', {//线材看板管理
        templateUrl:boardUrlPrefix+ 'JumperWireBoard'
    })
})
.factory('proEmployeeDataService', function (ajaxService) {
    var dataAccess = {};
    var urlPrefix='/ProEmployee/'

    dataAccess.getWorkers = function () {
      return  ajaxService.getData(urlPrefix + 'GetWorkers', {});
    };

    dataAccess.GetWorkerBy = function (workerId) {
        return ajaxService.getData(urlPrefix + 'GetWorkerBy', {
            workerId: workerId
        });
    };

    dataAccess.registWorker = function (worker) {
        return ajaxService.postData(urlPrefix + 'RegistWorker', {
            worker: worker
        });
    };

    return dataAccess;
})
.controller('moduleNavCtrl', function ($scope,navDataService,$state) {
    ///模块导航布局视图对象
    var moduleNavLayoutVm={
        menus: [],
        navList: [],
        navItems: [],
        navTo: function (navMenu) {
            moduleNavLayoutVm.navItems = [];
            angular.forEach(navMenu.Childrens, function (childNav) {
                var navItem = _.findWhere(moduleNavLayoutVm.menus, { Name: childNav.ModuleName, AtLevel: 3 });
                if (!angular.isUndefined(navItem)) {
                    moduleNavLayoutVm.navItems.push(navItem);
                }
            })
        },
        stateTo: function (navItem) {
            $state.go(navItem.UiSerf);
        },
    };
    $scope.navLayout = moduleNavLayoutVm;
    $scope.promise = navDataService.getSubModuleNavs('生产管理','ProductManage').then(function (datas) {
        moduleNavLayoutVm.menus = datas;
        moduleNavLayoutVm.navList = _.where(datas, { AtLevel: 2 });
    });
})
.controller('proUserRegistCtrl', function ($scope, dataDicConfigTreeSet, proEmployeeDataService) {
    var vmManager = {
        activeTab: 'initTab',
        WorkerId: null,
        workers: [],
        departments: [],
        roles: [],
        user: {},
        isPostKey: false,
        postTypes: [{ id: 0, text: '直接' }, { id: 0, text: '间接' }],
        getWorkerName: function (type) {
            if (uiVM.WorkerId.length >= 6) {
                var worker = _.find(vmManager.workers, { WorkerId: type === 'WorkerName' ? uiVM.WorkerId : uiVM.LeadWorkerId });
                if (worker !== undefined) {
                    uiVM[type] = worker.Name;
                }
                else {
                    uiVM[type] = '';
                }
            }
        },
        searchWorker: function ($event) {
            if ($event.keyCode == 13) {
                $scope.promise = proEmployeeDataService.GetWorkerBy(vmManager.WorkerId).then(function (user) {
                    if (angular.isObject(user)) {
                        uiVM = user;
                        vmManager.isPostKey = uiVM.IsPostKey === 0 ? false : true;
                        vmManager.WorkerId = null;
                        $scope.vm = uiVM;
                    }
                    else {
                        leeDataHandler.dataOperate.displayMessage(operate, "没有找到该用户信息！");
                    }
                });
            }
        },
    };
    $scope.vmManager = vmManager;

    ///
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        Organizetion: null,
        Department: null,
        Post: null,
        IsPostKey: false,
        PostType: null,
        ClassType: null,
        LeadWorkerId: null,
        LeadWorkerName: null,
        OpPerson: null,
        OpSign: vmManager.activeTab === 'initTab' ? 'add' : 'edit',
        Id_Key: null,
    }

    $scope.vm = uiVM;

    $scope.promise = proEmployeeDataService.getWorkers().then(function (data) {
        vmManager.departments = data.departments;
        departmentTreeSet.setTreeDataset(vmManager.departments);
        vmManager.user = data.user;
        uiVM.Department = vmManager.user.Department;
        vmManager.workers = data.workers;

        vmManager.roles = data.roles;
    });

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        uiVM.Department =dto.DataNodeText;
    };
    $scope.ztree = departmentTreeSet;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

    operate.registUser = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            uiVM.IsPostKey =$scope.vmManager.isPostKey === true ? 1 : 0;
            proEmployeeDataService.registWorker(uiVM).then(function (opresult) {
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
            leeHelper.clearVM(uiVM, ['Department', 'LeadWorkerId', 'LeadWorkerName', 'OpSign']);
            vmManager.isPostKey = false;
            vmManager.postType = 0;
        }
    };
})