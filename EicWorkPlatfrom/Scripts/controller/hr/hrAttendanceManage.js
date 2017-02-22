/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />
var hrModule = angular.module('bpm.hrApp');
hrModule.factory('hrDataOpService', function (ajaxService) {
    var hr = {};

    var attendUrl = "/HrAttendanceManage/";

    var generalAffairsUrl = "/HrGeneralAffairsManage/";

    ///获取请假配置信息
    hr.getLeaveTypesConfigs = function () {
        var url = attendUrl + "GetLeaveTypesConfigs";
        return ajaxService.getData(url, {});
    };

    //获取部门的班别信息
    hr.getClassTypeDatas = function (department, workerId, classType) {
        var url = attendUrl + 'GetClassTypeDatas';
        return ajaxService.getData(url, {
            department: department,
            workerId: workerId,
            classType: classType,
        });
    };

    //保存部门的班别的设置数据信息
    hr.saveClassTypeDatas = function (classTypes) {
        var url = attendUrl + "SaveClassTypeDatas";
        return ajaxService.postData(url, {
            classTypes: classTypes,
        });
    };

    //获取当天的考勤数据信息
    hr.getAttendanceDatas = function (qryDate, department, workerId, mode) {
        var url = attendUrl + 'GetAttendanceDatas';
        return ajaxService.getData(url, {
            qryDate: qryDate,
            department: department,
            workerId: workerId,
            mode: mode,
        });
    };
    ///////////////////////////////////////////////////////////////////////////////////
    //获取某人的当月请假数据
    hr.getAskLeaveDataAbout = function (workerId, yearMonth) {
        var url = attendUrl + "GetAskLeaveDataAbout";
        return ajaxService.getData(url, {
            workerId: workerId,
            yearMonth: yearMonth,
        });
    };

    //处理请假信息
    hr.handleAskForLeave = function (askForLeaves) {
        var url = attendUrl + "HandleAskForLeave";
        return ajaxService.postData(url, {
            askForLeaves: askForLeaves,
        });
    };
    //修改请假信息
    hr.updateAskForLeave = function (askForLeaves) {
        var url = attendUrl + "UpdateAskForLeave";
        return ajaxService.postData(url, {
            askForLeaves: askForLeaves,
        });
    };

    //自动检测考勤异常数据
    hr.autoCheckExceptionSlotData = function (yearMonth) {
        var url = attendUrl + "AutoCheckExceptionSlotData";
        return ajaxService.postData(url, {
            yearMonth: yearMonth,
        });
    };

    //载入异常刷卡数据
    hr.getExceptionSlotData = function () {
        var url = attendUrl + "GetExceptionSlotData";
        return ajaxService.getData(url, {
        });
    };

    //保存处理的考勤异常数据
    hr.handleExceptionSlotData = function (exceptionDatas) {
        var url = attendUrl + "HandleExceptionSlotData";
        return ajaxService.postData(url, {
            exceptionDatas: exceptionDatas
        });
    };

    //保存领取厂服记录
    hr.storeWorkerClothesReceiveRecord = function (model) {
        var url = generalAffairsUrl + 'StoreWorkerClothesReceiveRecord';
        return ajaxService.postData(url, {
            model: model,
        });
    };
    ///查询厂服记录
    hr.getWorkerClothesReceiveRecords = function (workerId, department, receiveMonth, mode) {
        var url = generalAffairsUrl + 'GetWorkerClothesReceiveRecords';
        return ajaxService.getData(url, {
            workerId: workerId,
            department: department,
            receiveMonth: receiveMonth,
            mode: mode,
        });
    };
    //是否可以以旧换新
    hr.canChangeOldForNew = function (workerId, productName, dealwithType, department) {
        var url = generalAffairsUrl + 'CanChangeOldForNew';
        return ajaxService.getData(url, {
            workerId: workerId,
            productName: productName,
            dealwithType: dealwithType,
            department: department
        });
    };
    return hr;
});
//-----------考勤业务管理-----------------------
//班别设置管理
hrModule.controller('attendClassTypeSetCtrl', function ($scope, $modal, hrDataOpService, dataDicConfigTreeSet, connDataOpService) {
    var qryDto = {
        Department:null,
        DepartmentText:null,
        DateFrom: null,
        DateTo: null,
        ClassType:'白班'
    };
    $scope.vm = qryDto;

    ///ui视图模型
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        Department: null,
        ClassType: null,
        IsAlwaysDay: null,
        DateFrom: null,
        DateTo: null,
        OpDate: null,
        OpPerson: null,
        OpSign: null,
        Id_Key: null,
        isSelect: false,
        selectedCls:''
    }
    var vmManager = {
        classTypes: [{ name: '白班', text: '白班' }, { name: '晚班', text: '晚班' }, { name: '', text: 'All' }, ],
        dataSets: [],
        dataSource:[],
        filterWorkerId: '',
        filterClassType:'',
        filterByWorkerId: function ($event) {
            if ($event.keyCode === 13) {
                if (qryDto.Department === null || vmManager.filterWorkerId === null)
                {
                    alert("必须选择部门，作业工号不能为空！");
                    return;
                }
                vmManager.loadClassTypeDatas(qryDto.Department, vmManager.filterWorkerId, null);
            }
        },
        filterByClassType: function () {
            if (qryDto.Department === null || vmManager.filterClassType === null) {
                alert("必须选择部门，班别不能为空！");
                return;
            }
            vmManager.loadClassTypeDatas(qryDto.Department, null, vmManager.filterClassType);
        },
        detailsDisplay: false,
        selectedWorkers: [],
        init: function () {
            vmManager.dataSets = [];
            vmManager.selectedWorkers = [];
            vmManager.dataSource = [];
            vmManager.isSelectAll = false;
        },
        msgModal: $modal({
            title: '信息提示',
            content:'请先选择要转班人员的数据！',
            templateUrl: leeHelper.modalTplUrl.msgModalUrl,
            show:false
        }),
        isSelectAll: false,
        selectAll: function () {
            vmManager.selectedWorkers = [];
            angular.forEach(vmManager.dataSets, function (item) {
                item.isSelect = !vmManager.isSelectAll;
                operate.addWorkerToList(item);
            });
        },
        loadClassTypeDatas: function (department, workerId, classType) {
            vmManager.dataSource = [];
            $scope.classTypePromise = hrDataOpService.getClassTypeDatas(qryDto.Department, workerId, classType).then(function (datas) {
                angular.forEach(datas, function (item) {
                    var dataItem = _.clone(uiVM);
                    leeHelper.copyVm(item, dataItem);
                    vmManager.dataSource.push(dataItem);
                })
                vmManager.dataSets = _.clone(vmManager.dataSource);
            });
        }
    };
   
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    operate.loadData = function () {
        vmManager.init();
        vmManager.loadClassTypeDatas(qryDto.Department, null, null);
    };

    operate.preview = function () {
        vmManager.detailsDisplay = !vmManager.detailsDisplay;
    };

    operate.addWorkerToList = function (item) {
        var worker = _.find(vmManager.selectedWorkers, { WorkerId: item.WorkerId });
        if (worker === undefined) {
            if (item.isSelect) {
                item.selectedCls = "text-primary";
                vmManager.selectedWorkers.push(item);
            }
            else {
                item.selectedCls = "";
            }
        }
        else {
            if (!item.isSelect) {
                item.selectedCls = "";
                leeHelper.remove(vmManager.selectedWorkers, item);
            }

        }
    };
    operate.changeClass = function (isvalid) {
        leeDataHandler.dataOperate.add(operate, isvalid, function () {
            if (vmManager.selectedWorkers.length === 0) {
                vmManager.msgModal.$promise.then(vmManager.msgModal.show);
            }
            else {
                angular.forEach(vmManager.selectedWorkers, function (item) {
                    leeHelper.copyVm(qryDto, item);
                });
                $scope.doPromise=hrDataOpService.saveClassTypeDatas(vmManager.selectedWorkers).then(function (opResult) {
                    leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                        vmManager.init();
                    })
                })
            }
        });
    };

    $scope.operate = operate;
    operate.vm = qryDto;

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");

    departmentTreeSet.bindNodeToVm = function () {
        qryDto.Department = departmentTreeSet.treeNode.vm.DataNodeName;
        qryDto.DepartmentText = departmentTreeSet.treeNode.vm.DataNodeText;
    };
    $scope.ztree = departmentTreeSet;

    $scope.promise = connDataOpService.getDepartments().then(function (datas) {
        departmentTreeSet.setTreeDataset(datas);
    });
});
//汇总考勤数据
hrModule.controller('hrSumerizeAttendanceDataCtrl', function ($scope, $modal, hrDataOpService, dataDicConfigTreeSet, connDataOpService) {
    var qryDto = {
        Department: '部门',
        DepartmentText: '部门',
        WorkerId: '',
        AttendanceDate:new Date(),
    };
    $scope.vm = qryDto;


    var vmManager = {
        dataSets: [],
        dataSource: [],
        init: function () {
            vmManager.dataSets = [];
            vmManager.dataSource = [];
        },
        getAttendanceDatas: function ($event) {
            if ($event.keyCode === 13)
            {
                if (qryDto.WorkerId.length === 0) return;
                operate.loadData(2);
            }
        },
        //导出到Excel
        exportToExcel: function () {
            var url = "HrAttendanceManage/ExoportAttendanceDatasToExcel/?qryDate=" + qryDto.AttendanceDate;
            return url;
        },
        yearMonth:'',
        //导出到Excel
        exportYearMonthDatasToExcel: function () {
            var url = "HrAttendanceManage/ExoportAttendanceMonthDatasToExcel/?yearMonth=" + vmManager.yearMonth;
            return url;
        },
    };

    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);

    operate.loadData = function (mode) {
        vmManager.init();
        $scope.promise = hrDataOpService.getAttendanceDatas(qryDto.AttendanceDate,qryDto.Department,qryDto.WorkerId,mode).then(function (datas) {
            vmManager.dataSource = datas;
            vmManager.dataSets = _.clone(vmManager.dataSource);
        });
    };
    /////


    ////

    operate.preview = function () {
        vmManager.detailsDisplay = !vmManager.detailsDisplay;
    };

    $scope.operate = operate;
    operate.vm = qryDto;
    //部门树数据
    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        qryDto.Department = departmentTreeSet.treeNode.vm.DataNodeName;
        qryDto.DepartmentText = departmentTreeSet.treeNode.vm.DataNodeText;
    };
    $scope.ztree = departmentTreeSet;
    $scope.promise = connDataOpService.getDepartments().then(function (datas) {
        departmentTreeSet.setTreeDataset(datas);
    });
});
//请假设置管理
hrModule.controller('attendAskLeaveCtrl', function ($scope, $modal, hrDataOpService, dataDicConfigTreeSet, connDataOpService, hrArchivesDataOpService) {
  
    ///视图模型
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        Department: null,
        LeaveType: null,
        LeaveHours: null,
        LeaveTimeRegion: null,
        LeaveDescription: null,
        LeaveMark: 0,
        LeaveMemo:null,
        StartLeaveDate: null,
        EndLeaveDate: null,
        LeaveTimeRegionStart:null, 
        LeaveTimeRegionEnd:null,
        DepartmentText: null,
        OpSign: null,
        ClassType: null,
        OpCmdVisible: -1,
        ///请假数据集
        LeaveDataSet: [],
        id:0,
    }
    var askLeaveVM = {
        AttendanceDate:null,
        SlotCardTime: null,
        LeaveType: null,
        LeaveHours: null,
        LeaveTimeRegion: null,
        LeaveDescription: null,
        LeaveMemo: null,
        LeaveMark: 0,
        WorkerId: null,
        WorkerName: null,
        Department: null,
        DepartmentText: null,
        OpCmdVisible: false,
        OpSign:null,
    };
    //视图管理器
    var vmManager = {
        activeTab: 'initTab',
        workerId: null,
        yearMonth:null,
        opSign:null,
        //部门信息
        departments:[],
        leaveTypes: [],
        changeDatas: [],
        //存储到数据库中的数据集
        dbDataSet:[],
        workerIdList: [],
        addWorkerId: function ($event) {
            if ($event.keyCode===13) {
                var item = _.findWhere(vmManager.changeDatas, { WorkerId:vmManager.workerId });
                if (item === undefined) {
                    item = {
                        WorkerId: _.clone(vmManager.workerId),
                        WorkerName: null,
                        Department: null,
                        LeaveType: null,
                        LeaveHours: null,
                        LeaveTimeRegion: null,
                        LeaveDescription: null,
                        LeaveMark: 0,
                        StartLeaveDate: null,
                        EndLeaveDate: null,
                        LeaveTimeRegionStart: null,
                        LeaveTimeRegionEnd: null,
                        DepartmentText: null,
                        ClassType: null,
                        OpSign: null,
                        OpCmdVisible: -1,
                        LeaveDataSet: [],
                    };
                    vmManager.changeDatas.push(item);
                    vmManager.workerIdList.push(item.WorkerId);
                    vmManager.workerId = null;
                }
            }
        },
        searchLeaveData: function ($event) {
            if ($event.keyCode === 13)
            {
                vmManager.askLeaveDatas = [];
                $scope.askLeavePromise = hrDataOpService.getAskLeaveDataAbout(vmManager.workerId,vmManager.yearMonth).then(function (datas) {
                    angular.forEach(datas, function (item) {
                        var dataItem = _.clone(askLeaveVM);
                        leeHelper.copyVm(item, dataItem);
                        dataItem.OpCmdVisible = true;
                        dataItem.DepartmentText = leeHelper.getDepartmentText(vmManager.departments, item.Department);
                        vmManager.askLeaveDatas.push(dataItem);
                        vmManager.workerId = null;
                    })
                });
            }
        },
        //请假数据
        askLeaveDatas:[],
    };
    $scope.vmManager = vmManager;

    //业务逻辑操作对象
    var operate = Object.create(leeDataHandler.operateStatus);
    operate.vm = uiVM;
    $scope.operate = operate;
    
 
    operate.search = function () {
        $scope.workerPromise = hrArchivesDataOpService.getEmployeeByWorkerIds(vmManager.workerIdList, 0).then(function (data) {
            angular.forEach(data, function (item) {
                var queryItem = _.find(vmManager.changeDatas, { WorkerId: item.WorkerId });
                if (queryItem !== undefined) {
                    if (queryItem.OpCmdVisible !== 0) {
                        leeHelper.copyVm(item, queryItem);
                        queryItem.OpCmdVisible = 1;
                        queryItem.WorkerName = item.Name;
                        queryItem.OpSign = "init";
                        queryItem.DepartmentText = leeHelper.getDepartmentText(vmManager.departments, item.Department);
                    }
                }
            })
        });
    };

    operate.editModal = $modal({
        title: "操作窗口",
        templateUrl: 'HR/AskLeaveEditSelectTpl/',
        controller: function ($scope) {
            $scope.vm = uiVM;
            $scope.leaveTypes = vmManager.leaveTypes;

            var op = Object.create(leeDataHandler.operateStatus);
            op.vm = uiVM;
            $scope.operate = op;

            $scope.save = function (isvalidate) {
                leeDataHandler.dataOperate.add(op, isvalidate, function () {
                    var leaveItem = {
                        WorkerId: null,
                        WorkerName: null,
                        Department: null,
                        LeaveType: null,
                        LeaveHours: null,
                        LeaveTimeRegion: null,
                        LeaveDescription: null,
                        LeaveMark: 0,
                        LeaveMemo: null,
                        StartLeaveDate: null,
                        EndLeaveDate: null,
                        LeaveTimeRegionStart:null,
                        LeaveTimeRegionEnd:null,
                        DepartmentText: null,
                        ClassType: null,
                        id: 0
                    };
                    var item = _.clone(uiVM);
                    if (vmManager.opSign === "handle" || vmManager.opSign === "add") {
                        item.LeaveMark = 1;
                        item.OpCmdVisible = 0;
                        var rowItem = _.find(vmManager.changeDatas, { WorkerId: item.WorkerId });
                        leeHelper.copyVm(item, rowItem);
                        leeHelper.copyVm(item, leaveItem);
                        rowItem.LeaveDataSet.push(leaveItem);
                        leaveItem.id = rowItem.LeaveDataSet.length;

                        var litem = _.findWhere(vmManager.dbDataSet, { WorkerId: item.WorkerId, LeaveType: item.LeaveType, StartLeaveDate: item.StartLeaveDate, EndLeaveDate: item.EndLeaveDate });
                        if (litem === undefined)
                            litem = _.clone(leaveItem);
                        vmManager.dbDataSet.push(litem);
                    }
                    else if (vmManager.opSign === 'edit') {
                        var rowItem = _.find(vmManager.changeDatas, { WorkerId: item.WorkerId });
                        leaveItem = _.find(rowItem.LeaveDataSet, { id: item.id });
                        leeHelper.copyVm(item, leaveItem);
                    }
                    else if (vmManager.opSign === 'handleEdit') {
                        var rowItem = _.find(vmManager.askLeaveDatas, { WorkerId: item.WorkerId, AttendanceDate: item.StartLeaveDate });
                        if (rowItem !== undefined) {
                            leeHelper.copyVm(item, rowItem);
                            rowItem.LeaveTimeRegion = item.LeaveTimeRegionStart + '--' + item.LeaveTimeRegionEnd;
                        }
                    }
                    operate.editModal.$promise.then(operate.editModal.hide);
                });
            };
        },
        show: false,
    });
    operate.handleAskForLeave = function (item, opSign) {
        leeHelper.clearVM(uiVM);
        vmManager.opSign = opSign;
        if (opSign === 'handle' || opSign==='edit')
        {
            leeHelper.copyVm(item, uiVM);
        }
        else if (opSign === 'add')
        {
            leeHelper.copyVm(item, uiVM, ['LeaveType', 'LeaveHours', 'StartLeaveDate', 'EndLeaveDate']);
        }
        else if (opSign === 'del')
        {
            var rowItem = _.findWhere(vmManager.changeDatas, { WorkerId: item.WorkerId });
            if (rowItem !== undefined)
            {
                leeHelper.remove(rowItem.LeaveDataSet, item);
                leeHelper.remove(vmManager.dbDataSet, item);
               
            }
        }
        else if (opSign == 'handleEdit')
        {
            leeHelper.copyVm(item, uiVM);
            uiVM.StartLeaveDate = item.AttendanceDate;
            uiVM.EndLeaveDate = item.AttendanceDate;
            uiVM.OpSign = opSign;
        }

        if (opSign !== 'del')
            operate.editModal.$promise.then(operate.editModal.show);
    };

    operate.save = function () {
        if (vmManager.activeTab === 'initTab')
        {
            hrDataOpService.handleAskForLeave(vmManager.dbDataSet).then(function (opResult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                    vmManager.changeDatas = [];
                    vmManager.dbDataSet = [];
                });
            });
        }
        else if (vmManager.activeTab === 'manageTab')
        {
            hrDataOpService.updateAskForLeave(vmManager.askLeaveDatas).then(function (opResult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                    vmManager.askLeaveDatas = [];
                });
            });
        }
    };

    $scope.promise = hrDataOpService.getLeaveTypesConfigs().then(function (datas) {
        var leaveTypes = _.where(datas, { ModuleName: "AttendanceConfig", AboutCategory: "AskForLeaveType" });
        if (leaveTypes !== undefined)
        {
            angular.forEach(leaveTypes, function (item) {
                vmManager.leaveTypes.push({ name: item.DataNodeText, text: item.DataNodeText });
            })
        }
        var departments = _.where(datas, { TreeModuleKey: "Organization" });
        if (departments !== undefined) {
            vmManager.departments = departments;
        }
    });
});
//考勤异常数据处理
hrModule.controller('attendExceptionHandleCtrl', function ($scope, $modal, hrDataOpService, dataDicConfigTreeSet, connDataOpService) {
    ///视图模型
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        Department: null,
        DepartmentText: null,
        AttendanceDate: null,
        SlotCardTime:null,
        LeaveHours: null,
        LeaveTimeRegion: null,
        SlotExceptionType: null,
        SlotExceptionMemo: null,
        Id_Key:0
    };
    var askLeaveVM = {
        WorkerId: null,
        WorkerName: null,
        LeaveType: null,
        LeaveHours: null,
        LeaveMemo: null,
        StartLeaveDate: null,
        EndLeaveDate: null,
        LeaveTimeRegionStart: null,
        LeaveTimeRegionEnd: null,
    }
    var slotCardVM = {
        WorkerId: null,
        WorkerName: null,
        AttendanceDate:null,
        SlotCardTime1: null,
        SlotCardTime2: null,
        ForgetSlotReason:null,
    };
    //视图管理器
    var vmManager = {
        activeTab: 'autoCheckExceptionTab',
        opSign: null,
        leaveTypes:[],
        //部门信息
        departments: [],
        //存储到数据库中的数据集
        dbDataSet: [],
        //异常数据集
        dataItems: [],
        //选定的项
        selectedItem: null,
        yearMonth:'',
        autoCheckExceptionData: function () {
            vmManager.dataItems = [];
            $scope.handlePromise = hrDataOpService.autoCheckExceptionSlotData(vmManager.yearMonth).then(function (datas) {
                angular.forEach(datas, function (item) {
                    var dataItem = _.clone(uiVM);
                    leeHelper.copyVm(item, dataItem);
                    dataItem.DepartmentText = leeHelper.getDepartmentText(vmManager.departments, item.Department);
                    vmManager.dataItems.push(dataItem);
                })
            });
        },
        loadExceptionData: function () {
            vmManager.dbDataSet = [];
            $scope.loadPromise = hrDataOpService.getExceptionSlotData().then(function (datas) {
                angular.forEach(datas, function (item) {
                    var dataItem = _.clone(uiVM);
                    leeHelper.copyVm(item, dataItem);
                    dataItem.DepartmentText = leeHelper.getDepartmentText(vmManager.departments, item.Department);
                    vmManager.dbDataSet.push(dataItem);
                })
            });
        },
    };
    $scope.vmManager = vmManager;

    //业务逻辑操作对象
    var operate = Object.create(leeDataHandler.operateStatus);
    operate.vm = uiVM;
    $scope.operate = operate;

    operate.handleAskLeaveModal = $modal({
        title: "请假处理操作窗口",
        templateUrl: 'HR/AskLeaveEditSelectTpl/',
        controller: function ($scope) {
            $scope.vm = askLeaveVM;
            $scope.leaveTypes = vmManager.leaveTypes;
            var op = Object.create(leeDataHandler.operateStatus);
            op.vm = askLeaveVM;
            $scope.operate = op;
            $scope.save = function (isvalid) {
                leeDataHandler.dataOperate.add(op, isvalid, function () {
                    var dataitem = _.find(vmManager.dbDataSet, { Id_Key: vmManager.selectedItem.Id_Key });
                    if (dataitem !== undefined) {
                        leeHelper.copyVm($scope.vm, dataitem);
                        dataitem.OpSign = vmManager.opSign;
                        dataitem.HandleSlotExceptionStatus = 2;
                        dataitem.LeaveTimeRegion = askLeaveVM.LeaveTimeRegionStart + '--' + askLeaveVM.LeaveTimeRegionEnd;
                        dataitem.OpSign = vmManager.opSign;
                        dataitem.SlotExceptionMemo = '请假处理';
                    }
                    operate.handleAskLeaveModal.$promise.then(operate.handleAskLeaveModal.hide);
                });
            };
        },
        show: false,
    });
    operate.handleSlotCardModal = $modal({
        title: "增补刷卡时间窗口",
        templateUrl: 'HR/AddSlotCardTimeSelectTpl/',
        controller: function ($scope) {
            $scope.vm = slotCardVM;
            var op = Object.create(leeDataHandler.operateStatus);
            op.vm = slotCardVM;
            $scope.operate = op;
            $scope.save = function (isvalid) {
                leeDataHandler.dataOperate.add(op, isvalid, function () {
                    var dataitem = _.find(vmManager.dbDataSet, { Id_Key: vmManager.selectedItem.Id_Key });
                    if (dataitem !== undefined) {
                        leeHelper.copyVm($scope.vm, dataitem);
                        dataitem.OpSign = vmManager.opSign;
                        dataitem.HandleSlotExceptionStatus = 2;
                        dataitem.SlotCardTime1 = dataitem.AttendanceDate + " " + slotCardVM.SlotCardTime1;
                        dataitem.SlotCardTime2 = dataitem.AttendanceDate + " " + slotCardVM.SlotCardTime2;
                        dataitem.SlotCardTime = slotCardVM.SlotCardTime1 + ',' + slotCardVM.SlotCardTime2;
                        dataitem.SlotExceptionMemo = '补刷卡时间';
                    }
                    operate.handleSlotCardModal.$promise.then(operate.handleSlotCardModal.hide);
                });
            };
        },
        show: false,
    });

    var handleAttendExceptionData = function (exceptionMemo)
    {
        var dataitem = _.find(vmManager.dbDataSet, { Id_Key: vmManager.selectedItem.Id_Key });
        if (dataitem !== undefined) {
            dataitem.OpSign = vmManager.opSign;
            dataitem.SlotExceptionMemo = exceptionMemo;
            dataitem.HandleSlotExceptionStatus = 2;
        }
    }

    operate.requestAttendLateModal = $modal({
        title: '迟到处理确认窗口',
        content: '您确定为迟到吗？',
        templateUrl: '/HR/AbsentSelectTpl',
        controller: function ($scope) {
            $scope.save = function () {
                handleAttendExceptionData('迟到处理');
                operate.requestAttendLateModal.$promise.then(operate.requestAttendLateModal.hide);
            };
        },
        show: false
    });
    operate.requestAttendAbsentModal = $modal({
        title: '旷工处理确认窗口',
        content: '您确定为旷工吗？',
        templateUrl: '/HR/AbsentSelectTpl',
        controller: function ($scope) {
            $scope.save = function () {
                handleAttendExceptionData('旷工处理');
                operate.requestAttendAbsentModal.$promise.then(operate.requestAttendAbsentModal.hide);
            };
        },
        show: false
    });
    operate.handleExceptionSlotData = function (item, opSign) {
        vmManager.opSign = opSign;
        vmManager.selectedItem = _.clone(item);
        if (opSign === 'handleAskLeave') {
            leeHelper.copyVm(item, askLeaveVM);
            askLeaveVM.StartLeaveDate = item.AttendanceDate;
            askLeaveVM.EndLeaveDate = item.AttendanceDate;
            operate.handleAskLeaveModal.$promise.then(operate.handleAskLeaveModal.show);
        }
        else if (opSign === 'handleForgetSlot') {
            leeHelper.copyVm(item, slotCardVM);
            operate.handleSlotCardModal.$promise.then(operate.handleSlotCardModal.show);
        }
        else if (opSign === 'handleAbsent') {
            operate.requestAttendAbsentModal.$promise.then(operate.requestAttendAbsentModal.show);
        }
        else if (opSign === 'handleLate') {
            operate.requestAttendLateModal.$promise.then(operate.requestAttendLateModal.show);
        }
    };

    operate.save = function () {
        hrDataOpService.handleExceptionSlotData(vmManager.dbDataSet).then(function (opResult) {
            leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                vmManager.changeDatas = [];
                vmManager.dbDataSet = [];
            });
        });
    };

    $scope.promise = hrDataOpService.getLeaveTypesConfigs().then(function (datas) {
        var leaveTypes = _.where(datas, { ModuleName: "AttendanceConfig", AboutCategory: "AskForLeaveType" });
        if (leaveTypes !== undefined) {
            angular.forEach(leaveTypes, function (item) {
                vmManager.leaveTypes.push({ name: item.DataNodeText, text: item.DataNodeText });
            })
        }
        var departments = _.where(datas, { TreeModuleKey: "Organization" });
        if (departments !== undefined) {
            vmManager.departments = departments;
        }
    });
});

//厂服管理
hrModule.controller('workClothesManageCtrl', function ($scope, $modal, hrDataOpService, dataDicConfigTreeSet, connDataOpService) {
    ///厂服管理模型
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        Department: null,
        ProductName: null,
        ProductSpecify: null,
        ProductCategory: null,
        PerCount: 1,
        Unit: "件",
        InputDate: null,
        DealwithType: null,
        OpSign: 'add',
        Id_Key: null,
    }
    $scope.vm = uiVM;
    var originalVM = _.clone(uiVM);

    //查询字段
    var queryFields = {
        workerId: null,
        department: null,
        receiveMonth: null,
    };

    $scope.query = queryFields;

    var vmManager = {
        activeTab: 'initTab',
        isLocal: true,
        init: function () {
            if (uiVM.OpSign === 'add') {
                leeHelper.clearVM(uiVM, ['ProductName', 'PerCount', 'Unit']);
            }
            else {
                uiVM = _.clone(originalVM);
            }
            uiVM.OpSign = 'add';
            $scope.vm = uiVM;
            vmManager.canEdit = false;
        },
        searchedWorkers: [],
        isSingle: true,//是否搜寻到的是单个人
        closeSpecifies: [],
        productNames: [
            {
                id: "夏季厂服", text: "夏季厂服", specifyList: [{ id: "34", text: "34" }, { id: "35", text: "35" }, { id: "36", text: "36" }, { id: "37", text: "37" }, { id: "38", text: "38" }, { id: "39", text: "39" }, { id: "40", text: "40" }, { id: "41", text: "41" }, { id: "42", text: "42" }, { id: "43", text: "43" }, { id: "44", text: "44" }, ]
            },
            {
                id: "冬季厂服", text: "冬季厂服", specifyList: [{ id: "S", text: "S" }, { id: "M", text: "M" }, { id: "L", text: "L" }, { id: "XL", text: "XL" }, { id: "XXL", text: "XXL" }, { id: "XXXL", text: "XXXL" }, ]
            },
        ],
        selectProductName: function () {
            var product = _.find(vmManager.productNames, { id: uiVM.ProductName });
            if (!angular.isUndefined(product)) {
                vmManager.closeSpecifies = product.specifyList;
            }
        },
        dealwithTypes: [
            { id: "领取新衣", text: "领取新衣" },
            { id: "以旧换新", text: "以旧换新" },
            { id: "以旧换旧", text: "以旧换旧" },
            { id: "购买新衣", text: "购买新衣" },
        ],
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
        storeDataset: [],
        searchDataset: [],
        //选择领取衣服记录
        selectReceiveClothesRecord: function (item) {
            vmManager.canEdit = true;
            uiVM = _.clone(item);
            uiVM.OpSign = 'edit';
            $scope.vm = uiVM;
        },
        searchBy: function () {
            $scope.searchPromise = hrDataOpService.getWorkerClothesReceiveRecords(queryFields.workerId, queryFields.department, queryFields.receiveMonth, 1).then(function (datas) {
                vmManager.storeDataset = datas;
            });
        },
        getReceiveClothesRecords: function (mode) {
            hrDataOpService.getWorkerClothesReceiveRecords(queryFields.workerId, queryFields.department, queryFields.receiveMonth, mode).then(function (datas) {
                vmManager.searchDataset = datas;
            });
        },
        isCanChange: false,
        checkCanChange: function () {
            hrDataOpService.canChangeOldForNew(uiVM.WorkerId, uiVM.ProductName, uiVM.DealwithType, uiVM.Department).then(function (data) {
                vmManager.isCanChange = data;
                if (!vmManager.isCanChange) {
                    vmManager.showErrorMsg();
                }
            });
        },
        showErrorMsg: function () {
            var modalTip = $modal({
                title: "信息提示",
                content: "对不起，距离上次换领厂服时间，您还不能进行此操作！",
                templateUrl: leeHelper.modalTplUrl.msgModalUrl,
                show: false,
            });
            modalTip.$promise.then(modalTip.show);
        }
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) {
        if (!vmManager.isCanChange) {
            vmManager.showErrorMsg();
            return;
        }
        hrDataOpService.storeWorkerClothesReceiveRecord(uiVM).then(function (opresult) {
            leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                var mdl = _.clone(uiVM);
                mdl.Id_Key = opresult.Id_Key;
                if (mdl.OpSign === 'add') {
                    vmManager.storeDataset.push(mdl);
                }
                else (mdl.OpSign == 'edit')
                {
                    var item = _.find(vmManager.storeDataset, { Id_Key: uiVM.Id_Key });
                    leeHelper.copyVm(uiVM, item);
                }
                vmManager.init();
            });
        });
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.inti();
        });
    };


    $scope.promise = connDataOpService.getConfigDicData('Organization').then(function (datas) {
        departmentTreeSet.setTreeDataset(datas);
    });
    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        queryFields.department = dto.DataNodeText;
    };
    $scope.ztree = departmentTreeSet;

});
