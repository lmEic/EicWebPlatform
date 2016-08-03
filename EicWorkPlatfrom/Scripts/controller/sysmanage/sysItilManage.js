/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var smModule = angular.module('bpm.sysmanageApp');
smModule.factory('sysitilService', function (ajaxService) {
    var itil = {};
    var urlPrefix = '/' + leeHelper.controllers.itilManage + '/';

    ///存储项目开发记录
    itil.storeProjectDevelopRecord = function (entity) {
        var url = urlPrefix + 'StoreProjectDevelopRecord';
        return ajaxService.postData(url, {
            entity: entity,
        });
    };
    ///根据开发进度状态查找开发模块
    itil.getProjectDevelopModuleBy = function (progressStatuses) {
        var url = urlPrefix + 'GetProjectDevelopModuleBy';
        return ajaxService.getData(url, {
            progressStatuses: progressStatuses,
        });
    };
    ///改变模块开发进度状态
    itil.changeDevelopModuleProgressStatus = function (entity) {
        var url = urlPrefix + 'ChangeDevelopModuleProgressStatus';
        return ajaxService.postData(url, {
            entity: entity,
        });
    };
    return itil;
});
smModule.controller('supTelManageCtrl', function ($scope, $modal, sysitilService) {
});
smModule.controller('itilProjectDevelopManageCtrl', function ($scope,$modal,sysitilService) {
    ///视图模型
    var uiVM = {
        MClassName: null,
        MFunctionName: null,
        DevID: null,
        ModuleName: null,
        FunctionDescription: null,
        DifficultyCoefficient: 1,
        DevPriority: 1,
        StartDate: new Date(),
        FinishDate:null,
        CurrentProgress: null,
        Executor: null,
        Memo: null,
        OpSign: 'add',
        Id_Key:0,
    }

    $scope.vm = uiVM;

    var vmManager = {
        activeTab: 'initTab',
        init: function () {
            if (uiVM.OpSign === 'add') {
                uiVM.MFunctionName = null;
                uiVM.FunctionDescription = null;
            }
            else {
                leeHelper.clearVM(uiVM, ['StartDate']);
            }
            uiVM.OpSign = 'add';
            $scope.vm = uiVM;
            vmManager.canEdit = false;
        },
        executors: [{ name: '万晓桥', text: '万晓桥' }, { name: '张文明', text: '张文明' }, { name: '杨垒', text: '杨垒' }],
        selectedProgressStatuses: [],
        progressStatuses: [
                { value: '待开发', label: '<i class="fa fa-calendar-o"></i>  待开发' },
                { value: '待审核', label: '<i class="fa fa-feed"></i>  待审核' },
                { value: '通过', label: '<i class="fa fa-check-square-o"></i>  通过' },
                { value: '不通过', label: '<i class="fa fa-bug"></i>  不通过' },
                { value: '待应用', label: '<i class="fa fa-cloud-upload"></i>  待应用' },
                { value: '结案', label: '<i class="fa fa-hand-peace-o"></i>  结案' }
        ],
        developModules: [],
        selectDevelopModule: function (item) {
            vmManager.canEdit = true;
            uiVM = item;
            uiVM.OpSign = 'edit';
            $scope.vm = uiVM;
        },
        datasource: [],
        datasets:[],
        getDevelopModules: function () {
            vmManager.datasets = [];
            vmManager.datasource = [];
            $scope.searchPromise = sysitilService.getProjectDevelopModuleBy(vmManager.selectedProgressStatuses).then(function (datas) {
                vmManager.datasource = datas;
            });
        },
        changeProgressStatus: function (item) {
            uiVM = _.clone(item);
            uiVM.OpSign = 'eidt';
            operate.editModal.$promise.then(operate.editModal.show);
        }
    };

    $scope.vmManager = vmManager;


    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.editModal = $modal({
        title: "状态变更窗口",
        templateUrl: 'SysITIL/ChangeDevelopModuleProgressStatusTpl/',
        controller: function ($scope) {
            $scope.vm = uiVM;
            var op = Object.create(leeDataHandler.operateStatus);
            $scope.operate = op;
            $scope.vmManager = vmManager;


            $scope.save = function (isvalidate) {
                leeDataHandler.dataOperate.add(op, isvalidate, function () {
                    sysitilService.changeDevelopModuleProgressStatus($scope.vm).then(function (opresult) {
                        var item = _.find(vmManager.datasource, { Id_Key: uiVM.Id_Key });
                        if (angular.isDefined(item)) {
                            leeHelper.copyVm(uiVM, item);
                            vmManager.init();
                            operate.editModal.$promise.then(operate.editModal.hide);
                        }
                    });
                });
            };
        },
        show: false,
    });
    operate.saveAll = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            sysitilService.storeProjectDevelopRecord(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    var mdl = _.clone(uiVM);
                    mdl.Id_Key = opresult.Id_Key;
                    if (mdl.OpSign === 'add') {
                        vmManager.developModules.push(mdl);
                    }
                    vmManager.init();
                });
            });
        })
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.inti();
        });
    };
})