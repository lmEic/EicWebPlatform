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
            entity: entity
        });
    };
    ///根据开发进度状态查找开发模块
    itil.getProjectDevelopModuleBy = function (progressStatuses, functionName, mode) {
        var url = urlPrefix + 'GetProjectDevelopModuleBy';
        return ajaxService.getData(url, {
            progressStatuses: progressStatuses,
            functionName: functionName,
            mode: mode
        });
    };
    ///改变模块开发进度状态
    itil.changeDevelopModuleProgressStatus = function (entity) {
        var url = urlPrefix + 'ChangeDevelopModuleProgressStatus';
        return ajaxService.postData(url, {
            entity: entity
        });
    };

    itil.viewDevelopModuleDetails = function (entity) {
        var url = urlPrefix + 'ViewDevelopModuleDetails';
        return ajaxService.postData(url, {
            entity: entity
        });
    };
    ///发送邮件通知
    itil.sendMail = function () {
        var url = urlPrefix + 'SendMail';
        return ajaxService.getData(url, {
        });
    };
    ///保存  通知地址配置 
    itil.storeItilNotifyAddress = function () {
        var url = urlPrefix + 'StoreItilNotifyAddress';
        return ajaxService.getData(url, {
        });
    };
    return itil;
});
smModule.controller('supTelManageCtrl', function ($scope, $modal, sysitilService) {
});
smModule.controller('itilProjectDevelopManageCtrl', function ($scope, $modal, sysitilService) {
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
        Executor: null,
        Memo: null,
        OpSign: 'add',
        Id_Key: 0
    };

    $scope.vm = uiVM;

    var originalVM = _.clone(uiVM);

    var queryFields = {
        selectedProgressStatuses: [],
        functionName: null
    };
    $scope.query = queryFields;

    var vmManager = {
        activeTab: 'initTab',
        isLocal: true,
        init: function () {
            if (uiVM.OpSign === 'add') {
                uiVM.MFunctionName = null;
                uiVM.FunctionDescription = null;
            }
            else {
                uiVM = _.clone(originalVM);
            }
            uiVM.OpSign = 'add';
            $scope.vm = uiVM;
            vmManager.canEdit = false;
        },
        executors: [{ name: '万晓桥', text: '万晓桥' }, { name: '张文明', text: '张文明' }, { name: '杨垒', text: '杨垒' }],

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
            uiVM.OpSign = vmManager.isUpdate ? 'update' : 'edit';
            $scope.vm = uiVM;
        },
        datasource: [],
        datasets: [],
        isUpdate: false,
        searchBy: function () {
            vmManager.isUpdate = true;
            $scope.searchPromise = sysitilService.getProjectDevelopModuleBy(queryFields.selectedProgressStatuses, queryFields.functionName, 2).then(function (datas) {
                vmManager.developModules = datas;
            });
        },
        getDevelopModules: function () {
            vmManager.datasets = [];
            vmManager.datasource = [];
            $scope.searchPromise = sysitilService.getProjectDevelopModuleBy(queryFields.selectedProgressStatuses, queryFields.functionName, 1).then(function (datas) {
                vmManager.datasource = datas;
            });
        },
        showModalWindow: function (item, isShowDetailsBoard) {
            vmManager.showDetailsBoard = isShowDetailsBoard;
            editModalOption.title = isShowDetailsBoard ? "进度状态明细窗口" : "状态变更窗口";
            uiVM = _.clone(item);
            if (!isShowDetailsBoard) {
                uiVM.OpSign = 'eidt';
                uiVM.CurrentProgress = null;
                uiVM.Executor = null;
            }

            vmManager.editModal = $modal(editModalOption);
            vmManager.editModal.$promise.then(vmManager.editModal.show);
        },
        changeProgressStatus: function (item) {
            vmManager.showModalWindow(item, false);
        },
        developChangeDetails: [],
        showDetailsBoard: false,//显示明细面板
        editModal: null,
        functionName: null,
        sendMail: function () {
            $scope.searchPromise = sysitilService.sendMail().then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {

                });
            });
        }
    };

    $scope.vmManager = vmManager;


    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

    //模态窗口选项
    var editModalOption = {
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
                            vmManager.editModal.$promise.then(vmManager.editModal.hide);
                        }
                    });
                });
            };
        },
        show: false
    };
    operate.saveAll = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            sysitilService.storeProjectDevelopRecord(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    var mdl = _.clone(uiVM);
                    mdl.Id_Key = opresult.Id_Key;
                    if (mdl.OpSign === 'add') {
                        vmManager.developModules.push(mdl);
                        vmManager.isUpdate = false;
                    }
                    else if (mdl.OpSign === 'edit') {
                        var item = _.find(vmManager.developModules, { Id_Key: uiVM.Id_Key });
                        leeHelper.copyVm(uiVM, item);
                    }
                    vmManager.init();
                });
            });
        });
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.inti();
        });
    };
    operate.viewDetails = function (item) {
        vmManager.showModalWindow(item, true);
        $scope.searchPromise = sysitilService.viewDevelopModuleDetails(item).then(function (datas) {
            vmManager.developChangeDetails = datas;
        });
    };
});
//消息通知模块控制器
smModule.controller('itilNotifyAddressManageCtrl', function ($scope, sysitilService) {
    ///视图模型
    ///通知邮件配置 
    var uiVm = {
        ModuleName: "通知邮件配置 ",
        BusinessName: "通知邮件配置",
        TransactionName: "通知邮件配置 ",
        EmailList: "通知邮件配置 ",
        TelMessageList: "通知邮件配置 ",
        MicroMessageList: "通知邮件配置 ",
        NotifyMode: 1,
        OpStatus: "完成",
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: 'add',
        Id_Key: 0,
    }

    $scope.vm = uiVm;

    var originalVM = _.clone(uiVm);

    var queryFields = {
        selectedProgressStatuses: [],
        functionName: null
    };
    $scope.query = queryFields;

    var vmManager = {
        activeTab: 'initTab',
        isLocal: true,
        init: function () {
          
        },
        OpStatus: [{ name: '完成', text: '完成' }, { name: '进行中', text: '进行中' }, { name: '未完成', text: '未完成' }],
        datasource: [],
        datasets: [],
        searchBy: function () {
            vmManager.isUpdate = true;
            $scope.searchPromise = sysitilService.getProjectDevelopModuleBy(queryFields.selectedProgressStatuses, queryFields.functionName, 2).then(function (datas) {
                vmManager.developModules = datas;
            });
        },
        getDevelopModules: function () {
            vmManager.datasets = [];
            vmManager.datasource = [];
            $scope.searchPromise = sysitilService.getProjectDevelopModuleBy(queryFields.selectedProgressStatuses, queryFields.functionName, 1).then(function (datas) {
                vmManager.datasource = datas;
            });
        },
        showDetailsBoard: false,//显示明细面板
        editModal: null,
        functionName: null,
      
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            sysitilService.storeItilNotifyAddress().then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                   
                });
            });
        });
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
        });
    };
});