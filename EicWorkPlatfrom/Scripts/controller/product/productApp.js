/// <reference path="../shippingApp.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />

angular.module('bpm.productApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', "pageslide-directive"])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //看板Url前缀
    var boardUrlPrefix = leeHelper.controllers.productBoard + "/";
    //报表Url前缀
    var reportUrlPrefix = leeHelper.controllers.dailyReport + "/";

    //工单Url前缀
    var mocUrlPrefix = leeHelper.controllers.mocManage + "/";

    //--------------生产日报-------------------------
    $stateProvider.state('dReportHoursSet', {
        //标准工时设定
        templateUrl: reportUrlPrefix + 'DReportHoursSet'
    })
    $stateProvider.state('dRProductOrderDispatching', {
        //生产工单分派管理
        templateUrl: reportUrlPrefix + 'DRProductOrderDispatching'
    })
    .state('dReportInput', {
        //日报录入
        templateUrl: reportUrlPrefix + 'DReportInput'
    })
    //--------------人员管理--------------------------
    .state('registWorkerInfo', {
        templateUrl: 'ProEmployee/RegistWorkerInfo'
    })
    //-------------看板管理-------------------
    .state('jumperWireBoard', {//线材看板管理
        templateUrl: boardUrlPrefix + 'JumperWireBoard'
    })
    //-------------工单管理-------------------
    .state('checkOrderBills', {//工单订单对比
        templateUrl: mocUrlPrefix + 'CheckOrderBills'
    });
})
.factory('proEmployeeDataService', function (ajaxService) {
    var dataAccess = {};
    var urlPrefix = '/ProEmployee/';

    dataAccess.getWorkers = function () {
        return ajaxService.getData(urlPrefix + 'GetWorkers', {});
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
            if ($event.keyCode === 13) {
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
        }
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
        Id_Key: null
    };

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
        uiVM.Department = dto.DataNodeText;
    };
    $scope.ztree = departmentTreeSet;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

    operate.registUser = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            uiVM.IsPostKey = $scope.vmManager.isPostKey === true ? 1 : 0;
            proEmployeeDataService.registWorker(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    pHelper.clearVM();
                });
            });
        });
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
});