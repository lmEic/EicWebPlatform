/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />
var proEmployeeModule = angular.module('bpm.productApp');
proEmployeeModule.factory('proEmployeeDataService', function (ajaxService) {
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
    //获取请假类别
    dataAccess.getLeaveTypesConfigs = function () {
        var url = urlPrefix+'GetLeaveTypesConfigs';
        return ajaxService.getData(url, {});
    };
    //保存请假数据
    dataAccess.storeLeaveAskManagerDatas = function (model) {
        var url = urlPrefix+'StoreLeaveAskManagerDatas';
        return ajaxService.postData(url, {
            model: model
        })
    };

    return dataAccess;
})
//人员注册管理器
proEmployeeModule.controller('proUserRegistCtrl', function ($scope, dataDicConfigTreeSet, proEmployeeDataService) {
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
proEmployeeModule.controller('proAskLeaveManagerCtrl', function ($scope, $filter, dataDicConfigTreeSet, connDataOpService, proEmployeeDataService, $modal) {
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        Department: null,
        LeaveType: null,
        LeaveHours: 0,
        LeaveApplyDate: new Date().toDateString(),
        LeaveAskDate: new Date().toDateString(),
        LeaveMemo: null,
        LeaveTimerStart: new Date( 00, 00),
        LeaveTimerEnd: new Date( 00, 00),
        LeaveState: null,
        OpDate: null,
        OpTime: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: 0
    }
    $scope.vm = uiVM;
    var originalVM = _.clone(uiVM);
    var dialog = $scope.dialog = leePopups.dialog();
    var queryFields = {
        workerId: null,
        department: null
    }
    $scope.query = queryFields;
    var vmManager  = {
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
        isSingle: true,
        searchedWorkers: [],
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
                uiVM.WorkerName = worker.Name;
                uiVM.WorkerId = worker.WorkerId;
                uiVM.Department = worker.Department;
            }
            else {
                uiVM.Department = null;
            }
        },
        leaveTypes: [],
        datasource: [],




    };
    $scope.vmManager = vmManager;
    var operate = $scope.operate = Object.create(leeDataHandler.operateStatus);
    //请假类别
    $scope.promise = proEmployeeDataService.getLeaveTypesConfigs().then(function (datas) {   
        var leaveTypes = _.where(datas, {
            ModuleName: "AttendanceConfig", AboutCategory: "AskForLeaveType"
        });
        if (leaveTypes !== undefined) {
            angular.forEach(leaveTypes, function (item) {       
                vmManager.leaveTypes.push({
                    name: item.DataNodeText, text: item.DataNodeText
                });
            });
        }
    });

    operate.saveAll = function (isValid) {    
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            alert("hello");
            proEmployeeDataService.storeLeaveAskManagerDatas(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result)
                    {
                        var mode = _.clone(uiVM)
                        model.Id_Key == opresult.Id_Key;
                        if (mode.OpSign === leeDataHandler.dataOpMode.add)
                        {
                            vmManager.datasource.push(mode);
                        }
                        vmManager.init();
                        dialog.close();
                    }
                })
            })
        })

    };
    //更新
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        })
    };


});