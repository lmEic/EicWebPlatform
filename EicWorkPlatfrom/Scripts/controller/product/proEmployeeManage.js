/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />
var proEmployeeModule = angular.module('bpm.productApp');
proEmployeeModule.factory('proEmployeeDataService', function (ajaxService) {
    var dataAccess = {};
    var urlPrefix = '/' + leeHelper.controllers.proEmployee+'/';
  


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
        var url = urlPrefix +'StoreLeaveAskManagerDatas';
        return ajaxService.postData(url, {
            model: model
        });
    };
    //查询请假数据
    dataAccess.getLeaveAskManagerData = function (workerId,department,mode) {
        var url = urlPrefix + 'GetLeaveAskManagerDatas';
        return ajaxService.getData(url, {
            workerId: workerId,
            department:department,
            mode: mode
        });

    };
    //加载部门
    dataAccess.getDepartment = function (dataNodeName) {
        var url = urlPrefix + 'GetDepartment';
        return ajaxService.getData(url, {
            dataNodeName:dataNodeName
        })
    }
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
proEmployeeModule.controller('proAskLeaveManagerCtrl', function ($scope, $filter,dataDicConfigTreeSet, connDataOpService, proEmployeeDataService) {
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        Department: null,
        LeaveType: null,
        LeaveHours: null,
        LeaveApplyDate: null,
        LeaveAskDate: null,
        LeaveMemo: null,
        LeaveTimerStart: null,
        LeaveTimerEnd: null,
        LeaveState: '未填写',
        ParentDataNodeText: leeDataHandler.dataStorage.getLoginedUser().organization.B,
        OpDate: null,
        OpTime: null,
        OpPerson:leeDataHandler.dataStorage.getLoginedUser().userName,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: 0
    };
    $scope.vm = uiVM;
    var originalVM = _.clone(uiVM);
    var dialog = $scope.dialog = leePopups.dialog();
    var queryFields = {
        workerId: null     
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
        leaveDepartment:[],
        datasource: [],
        searchDatas:[],
        workTimeStart: new Date(00, 00, 00),
        workTimeEnd: new Date(00, 00, 00),  
        leaveStats: [{ id: '未填写', text: '未填写' }, { id: '己填写', text: "己填写" }],
        selectDepartment: null,
        DepartmentDatas:[],
        //拼接时间
        SetDate: function ()
        {             
            if (uiVM.LeaveApplyDate == null && uiVM.LeaveAskDate == null) { leePopups.alert("亲！您未选择请假日期"); return; }        
            var workStart =uiVM.LeaveApplyDate+" "+vmManager.workTimeStart.pattern("HH:mm");
            var workEnd = uiVM.LeaveAskDate + " "+vmManager.workTimeEnd.pattern("HH:mm");
            uiVM.LeaveTimerStart = workStart;
            uiVM.LeaveTimerEnd = workEnd;         
        },
        //查询请假数据
        getLeaveAskManagerDatas: function (mode) {      
            vmManager.searchDatas = [];  
            vmManager.datasource = [];
            var datas = proEmployeeDataService.getLeaveAskManagerData(queryFields.workerId,vmManager.selectDepartment, mode).then(function (datas) {
                vmManager.searchDatas = datas;
                vmManager.datasource = datas;
            });
        },   
        //加载部门
        getDepartments: function () {          
            vmManager.DepartmentDatas = [];
            $scope.searchPromise= proEmployeeDataService.getDepartment(uiVM.ParentDataNodeText).then(function (datas) {
                vmManager.DepartmentDatas = datas;
            });
          
               
        }
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
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
    //编辑
    operate.editItem = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVM = item;
        dialog.show();
    },
    //删除      
    operate.deleteItem = function (item) {
            vmManager.delItem = item;
            $scope.vm = uiVM = item;
            operate.deleteDialog();
    }
    operate.deleteDialog = function () {
        leePopups.confirm("删除提示", "是否确定删除吗？", function () {
            uiVM.OpSign = leeDataHandler.dataOpMode.delete;
            proEmployeeDataService.storeLeaveAskManagerDatas(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) { 
                        vmManager.getLeaveAskManagerDatas(1);
                        vmManager.del();
                    }
                })
            })       
        });
    }  
    //保存
    operate.saveAll = function (isValid) {
        vmManager.SetDate(); 
        if (uiVM.LeaveHours < 0) { leePopups.alert("亲！您填写时数不能为负数"); return; }
        leeDataHandler.dataOperate.add(operate, isValid, function () {       
            proEmployeeDataService.storeLeaveAskManagerDatas(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var mode = _.clone(uiVM)
                        mode.Id_Key == opresult.Id_Key;
                        if (mode.OpSign === leeDataHandler.dataOpMode.add) {
                            vmManager.datasource.push(mode);
                        }                   
                        vmManager.init();
                        dialog.close();
                    }
                });
            });
        });
    };
    //更新
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    };
    vmManager.getDepartments();
});