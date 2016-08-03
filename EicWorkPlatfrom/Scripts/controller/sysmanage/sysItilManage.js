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
    return itil;
});
smModule.controller('supTelManageCtrl', function ($scope, $modal, sysitilService) {
});
smModule.controller('itilProjectDevelopManageCtrl', function ($scope, sysitilService) {
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
        FinishDate: null,
        CurrentProgress: null,
        CodingPerson: null,
        CheckPerson: null,
        Memo: null,
        OpSign: 'add',
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
        codePersons: [{ name: '万晓桥', text: '万晓桥' }, { name: '张文明', text: '张文明' }, { name: '杨垒', text: '杨垒' }],
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
            uiVM = _.clone(item);
            uiVM.OpSign = 'edit';
            $scope.vm = uiVM;
        },
        editDatas: [],
        getDevelopModules: function () {
            $scope.searchPromise = sysitilService.getProjectDevelopModuleBy(vmManager.selectedProgressStatuses).then(function (datas) {
                vmManager.editDatas = datas;
            });
        },
    };

    $scope.vmManager = vmManager;


    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            sysitilService.storeProjectDevelopRecord(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    var mdl = _.clone(uiVM);
                    mdl.Id_Key = opresult.Id_Key;
                    if (mdl.OpSign === 'add') {
                        vmManager.developModules.push(mdl);
                    }
                    else if (mdl.OpSign == 'edit') {
                        var current = _.find(vmManager.developModules, { Id_Key: mdl.Id_Key });
                        if (current !== undefined)
                            leeHelper.copyVm(mdl, current);
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