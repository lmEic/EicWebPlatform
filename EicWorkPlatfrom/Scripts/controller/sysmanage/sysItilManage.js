/// <reference path="../../common/eloam.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var smModule = angular.module('bpm.sysmanageApp');
smModule.factory('sysitilService', function (ajaxService) {
    var itil = {};
    var urlPrefix = '/' + leeHelper.controllers.itilManage + '/';

    //SystemManage/  SysITILController

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
    itil.storeItilNotifyAddress = function (entity) {
        var url = urlPrefix + 'StoreitilNotifyAddress';
        return ajaxService.getData(url, {
            entity: entity
        });
    };
    ///保存邮箱记录
    itil.storeEmailManageRecord = function (model) {
        var url = urlPrefix + 'StoreEmailManageRecord';
        return ajaxService.postData(url,{
                model: model
         });
    }
    //查询邮箱记录
    itil.getEmailManageRecord = function (workerId, email, receiveGrade,department, mode) {
        var url = urlPrefix + 'GetEmailManageRecord';
        return ajaxService.getData(url, {
            workerId: workerId,
            email: email,
            receiveGrade: receiveGrade,   
            department:department,
            mode: mode
        });
        
    }

    return itil;
});
///联系人
smModule.controller('supTelManageCtrl', function ($scope, $modal, sysitilService) {
});
/// 开发进程
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
smModule.controller('itilNotifyAddressManageCtrl', function ($scope,sysitilService) {
    ///视图模型
    ///通知邮件配置 
    var uiVm = $scope.vm = {
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
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: 0,
    };
    var originalVM = _.clone(uiVm);
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
            sysitilService.storeItilNotifyAddress(uiVm).then(function (opresult) {
                console.log(1);
            });
        });
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
        });
    };
});
///EmailManageController
smModule.controller('itilEmailManageCtrl', function ($scope, sysitilService, dataDicConfigTreeSet, connDataOpService,$modal) {
    leeHelper.setWebSiteTitle("系统管理", "邮箱配置管理")
    ///View
    var uiVM = {
        WorkerId: null,
        Name: null,
        Department: null,
        Email: null,
        NickName:null,
        ReceiveGrade: null,
        IsSender: 1,
        Password: null,
        IsValidity: 1,
        SmtpHost: 'smtp.exmail.qq.com',
        Pop3Host: 'pop.exmail.qq.com',
        SmtpPost: 25,
        Pop3Post: 110,
        RegDate: new Date(),
        OpDate: null,
        OpTime: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: 0
    };
    $scope.vm = uiVM;
    var originalVM = _.clone(uiVM);
    var dialog = $scope.dialog = leePopups.dialog();
    var queryFields = {
        workerId: null,
        email: null,
        receiveGrade: 0,
        department:null
    };
    $scope.query = queryFields;
    var vmManager = {
        activeTab: 'initTab',
        isLocal: true,
        init: function () {
            uiVM = _.clone(originalVM);
            uiVM.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVM;
        },
        del: function () {
            uiVM = _.clone(originalVM);
            uiVM.OpSign = leeDataHandler.dataOpMode.delete;
            $scope.vm = uiVM;

        },
        
        searchedWorkers: [],
        isSingle: true,//是否搜寻人员或部门
        departments: [
            { id: "管理部", text: "管理部" },
            { id: "生管部", text: "生管部" },
            { id: "品保部", text: "品保部" },
            { id: "开发部", text: "开发部" },
            { id: "生技部", text: "生技部" },
            { id: "采购室", text: "采购室" },
            { id: "业务室", text: "业务室" },
            { id: "环安课", text: "环安课" },
            { id: "制一部", text: "制一部" },
            { id: "制二部", text: "制二部" },
            { id: "制三部", text: "制三部" },
            { id: "制五部", text: "制五部" },
            { id: "企业讯息", text: "企业讯息" },
            { id: "自动化", text: "自动化" },
        ],
        receiveGrades: [{ name: "1", text: "1" }, { name: "2", text: "2" }, { name: "3", text: "3" }, { name: "4", text: "4" }, { name: "5", text: "5" },],
        storeModules: [],
        datasource: [],
        searchDataset: [],
        delItem: null,
        searchBy: function () {
            $scope.searchPromise = sysitilService.getEmailManageRecord(queryFields.workerId, queryFields.email,queryFields.receiveGrade,queryFields.department, 1).then(function (datas) {
                vmManager.searchDataset = datas;
            })
        },
        getEmailRecords: function (mode) {
            vmManager.searchDataset = [];
            vmManager.datasource = [];
            sysitilService.getEmailManageRecord(queryFields.workerId, queryFields.email,queryFields.receiveGrade,queryFields.department, mode).then(function (datas) {
                vmManager.datasource = datas;
            });
        },
        getWorkerInfo: function () {
            if (uiVM.WorkerId === undefined) return;
            var strLen = leeHelper.checkIsChineseValue(uiVM.WorkerId) ? 2 : 6;
            if (uiVM.WorkerId.length >= strLen) {
                vmManager.searchedWorkers = [];
                $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(uiVM.WorkerId).then(function (datas) {
                    if (datas.length > 0) {
                        vmManager.searchedWorkers = datas;
                        if (vmManager.searchedWorkers.length === 1) {
                            vmManager.isSingle = true;
                            vmManager.selectWorker(vmManager.searchedWorkers[0]);
                        }
                        else {
                            vmManager.isSingle = false;
                        }
                    }
                    else {
                        vmManager.selectWorker(null);
                    }
                });
            }
        },

        selectWorker: function (worker) {
            if (worker !== null) {
                uiVM.Name = worker.Name;
                uiVM.WorkerId = worker.WorkerId;
                uiVM.Department = worker.Department;
            }
            else {
                uiVM.Department = null;
            }
        },
        delModalWindow: $modal({
            title: "删除提示", content: "您确定要删除此信息吗?",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl, show: false,
            controller: function ($scope) {        
                $scope.confirmDelete = function () {             
                    uiVM.OpSign = leeDataHandler.dataOpMode.delete;
                    sysitilService.storeEmailManageRecord(uiVM).then(function (opresult) {
                        leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                            if (opresult.Result) {    
                                vmManager.searchBy();  
                                vmManager.del();
                               
                            }

                        })

                    })   
                   
                    vmManager.delModalWindow.$promise.then(vmManager.delModalWindow.hide);
                };
            },
        }),


       
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

    operate.editItem = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVM = item;
        dialog.show();
    };
    operate.deleteItem = function (item) {    
        vmManager.delItem = item;
       $scope.vm = uiVM = item;    
        vmManager.delModalWindow.$promise.then(vmManager.delModalWindow.show);
    }  
    operate.saveAll = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            sysitilService.storeEmailManageRecord(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var mode = _.clone(uiVM)
                        mode.Id_Key == opresult.Id_Key;
                        if (mode.OpSign === leeDataHandler.dataOpMode.add) {
                            vmManager.datasource.push(mode);
                            
                        }
                        vmManager.searchBy();  
                        vmManager.init();                    
                        dialog.close();
                       
                    }
                });
            });
        });
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    };
    $scope.promise = connDataOpService.getConfigDicData('Organization').then(function (datas) {
        departmentTreeSet.setTreeDataset(datas);
    });
    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        queryFields.department = dto.DataNodeText;
        vmManager.getEmailRecords(4);      
    };
    $scope.ztree = departmentTreeSet;

    

});
