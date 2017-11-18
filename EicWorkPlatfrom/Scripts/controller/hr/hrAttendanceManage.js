/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />
var hrModule = angular.module('bpm.hrApp');
hrModule.factory('hrDataOpService', function (ajaxService) {
    var hr = {};

    var attendUrl = "/HrAttendanceManage/";

    var generalAffairsUrl = "/HrGeneralAffairsManage/";//控制器名称

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
            classType: classType
        });
    };
    //保存部门的班别的设置数据信息
    hr.saveClassTypeDatas = function (classTypes) {
        var url = attendUrl + "SaveClassTypeDatas";
        return ajaxService.postData(url, {
            classTypes: classTypes
        });
    };

    //获取当天的考勤数据信息
    hr.getAttendanceDatas = function (qryDate, dateFrom, dateTo, department, workerId, mode) {
        var url = attendUrl + 'GetAttendanceDatas';
        return ajaxService.getData(url, {
            qryDate: qryDate,
            dateFrom: dateFrom,
            dateTo: dateTo,
            department: department,
            workerId: workerId,
            mode: mode
        });
    };
    ///////////////////////////////////////////////////////////////////////////////////
    //获取某人的当月请假数据
    hr.getAskLeaveDataAbout = function (workerId, yearMonth) {
        var url = attendUrl + "GetAskLeaveDataAbout";
        return ajaxService.getData(url, {
            workerId: workerId,
            yearMonth: yearMonth
        });
    };

    //处理请假信息
    hr.handleAskForLeave = function (askForLeaves) {
        var url = attendUrl + "HandleAskForLeave";
        return ajaxService.postData(url, {
            askForLeaves: askForLeaves
        });
    };

    //自动检测考勤异常数据
    hr.autoCheckExceptionSlotData = function (yearMonth) {
        var url = attendUrl + "AutoCheckExceptionSlotData";
        return ajaxService.postData(url, {
            yearMonth: yearMonth
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
            model: model
        });
    };
    ///查询厂服记录
    hr.getWorkerClothesReceiveRecords = function (workerId, department, receiveMonth, mode) {
        var url = generalAffairsUrl + 'GetWorkerClothesReceiveRecords';
        return ajaxService.getData(url, {
            workerId: workerId,
            department: department,
            receiveMonth: receiveMonth,
            mode: mode
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
    
    //按日期查询加班数据
    hr.getWorkOverHoursData = function (workDate,departmentText,mode) {
        var url = attendUrl + 'GetWorkOverHoursData';
        return ajaxService.getData(url, {
            workDate: workDate,
            departmentText:departmentText,
            mode:mode
        });
    };
    //载入模板
    hr.getWorkOverHoursMode = function (departmentText,workDate) {
        var url = attendUrl + 'GetWorkOverHoursMode';
        return ajaxService.getData(url, {
            departmentText: departmentText,  
            workDate: workDate,
        });
    };

    //按加班时数汇总查询
    hr.getWorkOverHourSums = function (qrydate, departmentText, mode) {
        var url = attendUrl + 'GetWorkOverHoursSum';
        return ajaxService.getData(url, {
            qrydate: qrydate,
            departmentText: departmentText,
            mode: mode
        })
    };
    //按工号查询加班时数汇总
    hr.getWorkOverHourSumsByWorkIds = function (qrydate, departmentText, workId, mode) {
        var url = attendUrl + 'GetWorkOverHourSumsByWorkId';
        return ajaxService.getData(url, {
            qrydate: qrydate,
            departmentText: departmentText,
            workId: workId,
            mode: mode
        })
    };
    //查询员工明细
    hr.getWorkOverHoursWorkIdBydetails = function (qrydate, departmentText, workId, mode) {
        var url = attendUrl + 'GetWorkOverHoursWorkIdBydetail';
        return ajaxService.getData(url, {
            qrydate: qrydate,
            departmentText: departmentText,
            workId: workId,
            mode: mode
        })
    };

    //批量保存加班数据
    hr.storeHandlWorkOverHoursDt = function (workOverHours) {
        var url = attendUrl + 'HandlWorkOverHoursDt';
        return ajaxService.postData(url, {
            workOverHours: workOverHours

        });
    };
    //单条修改保存
    hr.storeWorkOverHoursDt = function (model)
    {
        var url = attendUrl + 'StoreWorkOverHoursRecordSingle';
        return ajaxService.postData(url, {
            model: model
        });
    },
    //加班时数导入Excel
    hr.importWorkOverHoursByDatas = function (file) {
        var url = attendUrl + 'ImportWorkOverHoursDatas';
        return ajaxService.uploadFile(url, file);
        };
    hr.getDepartments = function (datanodeName)
    {
        var url = attendUrl + 'GetDepartment';
        return ajaxService.getData(url, {
            datanodeName: datanodeName
            
        })
    }
    return hr;
});
//-----------考勤业务管理-----------------------
//班别设置管理
hrModule.controller('attendClassTypeSetCtrl', function ($scope, $modal, hrDataOpService, dataDicConfigTreeSet, connDataOpService) {
    var qryDto = {
        Department: null,
        DepartmentText: null,
        DateFrom: null,
        DateTo: null,
        ClassType: '白班'
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
        selectedCls: ''
    };
    var vmManager = {
        classTypes: [{ name: '白班', text: '白班' }, { name: '晚班', text: '晚班' }, { name: '', text: 'All' }],
        dataSets: [],
        dataSource: [],
        filterWorkerId: '',
        filterClassType: '',
        filterByWorkerId: function ($event) {
            if ($event.keyCode === 13) {
                if (qryDto.Department === null || vmManager.filterWorkerId === null) {
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
            content: '请先选择要转班人员的数据！',
            templateUrl: leeHelper.modalTplUrl.msgModalUrl,
            show: false
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
                });
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
                $scope.doPromise = hrDataOpService.saveClassTypeDatas(vmManager.selectedWorkers).then(function (opResult) {
                    leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                        vmManager.init();
                    });
                });
            }
        });
    };

    $scope.operate = operate;
    operate.vm = qryDto;

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");

    departmentTreeSet.bindNodeToVm = function () {
        qryDto.Department = departmentTreeSet.treeNode.vm.DataNodeName;
        qryDto.DepartmentText = departmentTreeSet.treeNode.vm.DataNodeText;
        vmManager.loadClassTypeDatas(qryDto.Department, null, null);
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
        AttendanceDate: new Date()
    };
    $scope.vm = qryDto;


    var vmManager = {
        dateFrom: new Date(),
        dateTo: new Date(),
        dataSets: [],
        dataSource: [],
        init: function () {
            vmManager.dataSets = [];
            vmManager.dataSource = [];
        },
        getAttendanceDatas: function ($event) {
            if ($event.keyCode === 13) {
                if (qryDto.WorkerId.length === 0) return;
                operate.loadData(2);
            }
        },
        //导出到Excel
        exportToExcel: function () {
            var url = "HrAttendanceManage/ExoportAttendanceDatasToExcel/?qryDate=" + qryDto.AttendanceDate;
            return url;
        },
        yearMonth: '',
        //导出到Excel
        exportYearMonthDatasToExcel: function () {
            var url = "HrAttendanceManage/ExoportAttendanceMonthDatasToExcel/?yearMonth=" + vmManager.yearMonth;
            return url;
        }
    };

    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);

    operate.loadData = function (mode) {
        vmManager.init();
        $scope.promise = hrDataOpService.getAttendanceDatas(qryDto.AttendanceDate, vmManager.dateFrom, vmManager.dateTo, qryDto.Department, qryDto.WorkerId, mode).then(function (datas) {
            vmManager.dataSource = datas;
            vmManager.dataSets = _.clone(vmManager.dataSource);
        });
    };
 
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
    var askLeaveVM = {
        Id: null,
        IsServer: false,//是否是从服务器端获取的数据标志
        AttendanceDate: null,
        Day: null,
        YearMonth: null,
        SlotCardTime: null,
        LeaveType: null,
        LeaveHours: null,
        LeaveTimeRegion: null,
        LeaveTimeRegionStart: null,
        LeaveTimeRegionEnd: null,
        LeaveMemo: null,
        WorkerId: null,
        WorkerName: null,
        Department: null,
        OpSign: null,
        Id_Key: 0
    };

    var uiVM = $scope.vm = _.clone(askLeaveVM);

    var editDialog = $scope.editDialog = leePopups.dialog();
    //查询字段视图
    var queryVM = $scope.qryvm = {
        year: null,
        month: null,
        yearMonth: null,
        dateFrom: new Date(),//请假其实日期
        dateTo: new Date(),//请假结束日期
        leaveType: '事假',
    };

    //视图管理器
    var vmManager = {
        activeTab: 'initTab',
        workerInfo: null,
        putData: function (datas, item, isAlert) {
            var data = _.findWhere(datas, { LeaveTimeRegion: item.LeaveTimeRegion, Day: item.Day });
            if (data !== undefined) {
                if (isAlert)
                    leePopups.alert("特别提醒", "请假时间段：" + item.LeaveTimeRegion + ",日期：" + item.Day + "已经添加过了！");
            }
            else {
                datas.push(item);
            }
        },
        setData: function (weekDay, askLeaveItem) {
            var dataItem = _.clone(askLeaveVM);
            leeHelper.copyVm(askLeaveItem, dataItem);

            var timeResgions = dataItem.LeaveTimeRegion.split('-');
            if (timeResgions.length == 2) {
                var timeStart = timeResgions[0];
                var timeEnd = timeResgions[1];
                dataItem.LeaveTimeRegionStart = new Date(dataItem.AttendanceDate + " " + timeStart);
                dataItem.LeaveTimeRegionEnd = new Date(dataItem.AttendanceDate + " " + timeEnd);
            }
            dataItem.Id = leeHelper.newGuid();
            dataItem.IsServer = true;
            vmManager.putData(vmManager.askLeaveDatas, dataItem);//插入到批量处理的数据集合中
            vmManager.putData(weekDay.askLeaveDatas, dataItem);
        },
        setAskLeaveDatas: function () {
            if (vmManager.workerInfo !== null) {
                vmManager.askLeaveDatas = [];
                hrDataOpService.getAskLeaveDataAbout(vmManager.workerInfo.WorkerId, queryVM.yearMonth).then(function (datas) {
                    if (angular.isArray(datas)) {
                        angular.forEach(vmManager.calendar.WeekCalendars, function (weekItem) {
                            angular.forEach(weekItem.WeekDays, function (weekDay) {
                                var askLeaveDatas = _.where(datas, { Day: parseInt(weekDay.Day) });
                                if (askLeaveDatas !== undefined && askLeaveDatas.length > 0) {
                                    weekDay.askLeaveDatas = [];
                                    //这一天是否有多条请假记录
                                    if (angular.isArray(askLeaveDatas) && askLeaveDatas.length > 1) {
                                        angular.forEach(askLeaveDatas, function (askLeaveItem) {
                                            askLeaveItem.OpSign = leeDataHandler.dataOpMode.none;
                                            vmManager.setData(weekDay, askLeaveItem);
                                        })
                                    }
                                    else {
                                        var askLeaveItem = askLeaveDatas[0];
                                        askLeaveItem.OpSign = leeDataHandler.dataOpMode.none;
                                        vmManager.setData(weekDay, askLeaveItem);
                                    }
                                }
                            });
                        });
                    }
                });
            }
        },
        leaveTypes: [],
        //请假数据
        askLeaveDatas: [],
        calendar: null,
        loadCalendarDatas: function () {
            $scope.promise = connDataOpService.getCalendarDatas(queryVM.year, queryVM.month).then(function (datas) {
                vmManager.calendar = datas;
            });
        },
        setAskLeaveTimeRegion: function (item, askDay, isFirst) {
            var day = parseInt(askDay), year = queryVM.year, month = queryVM.month - 1;
            item.Day = day;
            item.YearMonth = queryVM.yearMonth;
            if (isFirst) {
                item.LeaveTimeRegionStart = new Date(year, month, day, 7, 50, 0);
                item.LeaveTimeRegionEnd = new Date(year, month, day, 17, 10, 0);
            }
            item.LeaveTimeRegion = item.LeaveTimeRegionStart.pattern("HH:mm") + "-" + item.LeaveTimeRegionEnd.pattern("HH:mm");
        },
        //生成请假记录
        createAskLeaveRecord: function (item) {
            if (vmManager.workerInfo === null) return;
            var workerItem = _.clone(askLeaveVM);

            workerItem.Id = leeHelper.newGuid();
            workerItem.IsServer = false;//由客户端创建的数据
            workerItem.WorkerId = vmManager.workerInfo.WorkerId;
            workerItem.WorkerName = vmManager.workerInfo.Name;
            workerItem.Department = vmManager.workerInfo.Department;
            workerItem.LeaveHours = 8;
            workerItem.LeaveType = queryVM.leaveType;
            vmManager.setAskLeaveTimeRegion(workerItem, item.Day, true);
            workerItem.OpSign = leeDataHandler.dataOpMode.add;
            if (angular.isUndefined(item.askLeaveDatas) || !angular.isArray(item.askLeaveDatas)) {
                item.askLeaveDatas = [];
            }
            vmManager.putData(item.askLeaveDatas, workerItem, true);
            vmManager.putData(vmManager.askLeaveDatas, workerItem, true);
        },
        removeAskLeaveRecord: function (weekDay, item) {
            if (vmManager.workerInfo === null) return;
            leePopups.confirm("删除提示", "您确定要删除请假数据吗？", function () {
                $scope.$apply(function () {
                    if (item.IsServer) {
                        item.OpSign = leeDataHandler.dataOpMode.delete;
                    }
                    else {
                        leeHelper.delWithId(vmManager.askLeaveDatas, item);//从数据操作库中移除
                    }
                    leeHelper.delWithId(weekDay.askLeaveDatas, item);//移除临时数据
                });
            });
        },
        editAskLeaveRecord: function (item) {
            if (vmManager.workerInfo === null) return;
            item.OpSign = (item.IsServer) ? leeDataHandler.dataOpMode.edit : leeDataHandler.dataOpMode.add;
            $scope.vm = item;
            editDialog.show();
        },
        confirmEdit: function () {
            vmManager.setAskLeaveTimeRegion($scope.vm, $scope.vm.Day, false);
            editDialog.close();
        }
    };
    $scope.vmManager = vmManager;

    //业务逻辑操作对象
    var operate = $scope.operate = Object.create(leeDataHandler.operateStatus);

    operate.saveAll = function () {
        hrDataOpService.handleAskForLeave(vmManager.askLeaveDatas).then(function (opResult) {
            leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                vmManager.loadCalendarDatas();
                vmManager.askLeaveDatas = [];
            });
        });
    };

    $scope.promise = hrDataOpService.getLeaveTypesConfigs().then(function (datas) {
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
});
//加班管理
hrModule.controller('workOverHoursManageCtrl', function ($scope, $modal,$filter, hrDataOpService, dataDicConfigTreeSet, connDataOpService, hrArchivesDataOpService) {
    ///ui视图模型
    var uiVM = {
        WorkerId: null,//
        WorkerName: null,//    
        WorkoverType: null,//
        WorkClassType: null,//
        WorkDate: null,//
        WorkOverHours: null,//
        Remark: null,
        DepartmentText: null,       
        WorkStatus: '在职',
        QryDate: null,
        WorkReason: '产线加班',
        WorkDayTime: '空白',
        WorkNightTime: '空白',
        ParentDataNodeText: leeDataHandler.dataStorage.getLoginedUser().organization.B,
        //OpDate: null,
        OpPerson: leeLoginUser.userName,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null,
       
    };
    $scope.vm = uiVM;
    var dialog = $scope.dialog = leePopups.dialog();
    var qryDto = {
       
        departmentText: leeLoginUser.departmentText,
        workDate: new Date().toDateString(),
        workId:null,
    };
    $scope.query = qryDto;
    var originalVM = _.clone(uiVM);
    $scope.tempVm = tempVm = {
        tabCount: 0,
        department: leeLoginUser.departmentText,
        workOverCount: 0,
    };
    var vmManager = { 
        selectDepartment:null,
        qryWorkName:null,
        searchYear: new Date().getFullYear(),
        changeworkDate:null,
        workDayDate: new Date().toDateString(),
        workNightDate: new Date().toDateString(),
        workDayTimeStart: new Date(00, 00, 00),
        workDayTimeEnd: new Date(00, 00, 00),     
        workNightTimeStart: new Date(00, 00, 00),
        workNightTimeEnd: new Date(00, 00, 00),
        classTypes: [{ id: '白班', text: '白班' }, { id: '晚班', text: "晚班" }],
        overTypes: [{ id: '平时加班', text: '平时加班' }, { id: '假日加班', text: '假日加班' }, { id: '节假日加班', text: '节假日加班' }],
        workOverHourss: [{ id: 0.5, text: 0.5 }, { id: 1.0, text: 1.0 }, { id: 1.5, text: 1.5 }, { id: 2.0, text: 2.0 }, { id: 2.5, text: 2.5 }],
        workStatuss: [{ id: '在职', text: '在职' },{id:'离职',text:'离职'}],
        dataSets: [],
        dataSource: [],
        searchDatas: [],
        dataSourceSum: [],
        dataSourceDetail:[],
        selectedWorkers: [],
        edittingRowIndex: 0,//编辑中的行索引
        edittingRow: null,
        searchedWorkers: [],
        isSingle: true,//是否搜寻人员或部门
        dataTable: null,
        boardViewSize: '100%',
        editWindowShow: false,
        editWindowShow1: false,
        dataSet: [],
        selectedWorkers: [],
        dataSource: [],  
        DepartmentDatas:[],
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
        createRowItem: function () {
            var vm = _.clone(originalVM);
            uiVM = _.clone(vm);
            $scope.vm = uiVM;
            vm.WorkDate = qryDto.workDate;
            vm.WorkoverType = uiVM.WorkoverType;
            vm.rowindex = vmManager.edittingRowIndex;
            vm.editting = true;
            vmManager.edittingRow = vm;
            return vm;
        },
        createNewRow: function () {
            vmManager.addRow();
        },
        addRow: function () {
            vmManager.edittingRowIndex = vmManager.dataSource.length > 0 ? vmManager.dataSource.length + 1 : 1;
            var vm = vmManager.createRowItem();
            vmManager.dataSets.push(vm);
        },
        //删除行
        removeRow: function (item) {
            leeHelper.remove(vmManager.dataSets, item);
            var rowindex = item.rowindex;
            vmManager.edittingRowIndex = rowindex - 1;
            var index = 1;
            //重新更改行的索引
            angular.forEach(vmManager.dataSets, function (row) {
                row.rowindex = index;
                index += 1;
            }),
                tempVm.tabCount = vmManager.dataSets.length;
            //累计时数
            tempVm.workOverCount = 0;
            angular.forEach(vmManager.dataSets, function (row) {
                $scope.tempVm.workOverCount += parseFloat(row.WorkOverHours);
            })
        },
        //复制粘贴行
        copyAndPaste: function (item) {
            item.editting = false;
            var rowindex = item.rowindex;
            vmManager.edittingRowIndex = rowindex + 1;
            var vm = _.clone(item);
            vmManager.dataSets.push(vm);
            var index = 1;
            //重新更改行的索引
            angular.forEach(vmManager.dataSets, function (row) {
                row.rowindex = index;
                index += 1;
            }),
                //累计行数
                tempVm.tabCount = vmManager.dataSets.length;
            //累计时数
            tempVm.workOverCount = 0;
            angular.forEach(vmManager.dataSets, function (row) {
                $scope.tempVm.workOverCount += parseFloat(row.WorkOverHours);
            })
        },
        //取消编辑
        cancelEdit: function (item) {
            item.pheditting = false;
            item.isEdittingWorkerId = false;
            item.wkhing = false;
        },
        //获取行
        getCurrentRow: function (item) {
            vmManager.edittingRowIndex = item.rowindex;
            vmManager.edittingRow = item;
        },
        //结束编辑
        editOver: function (rowItem) {
            if (!angular.isUndefined(rowItem)) {
                leeHelper.copyVm(uiVM, rowItem);
                uiVM = _.clone(initVM);
                $scope.vm = uiVM;
                rowItem.editting = false;
            }
        },
        //编辑加班类型 
        editOverType: function (item) {
            item.isEdittingOverType = true;
            vmManager.getCurrentRow(item);
            $scope.vm.WorkoverType = item.WorkoverType;
        },
        //选择加班类型
        selectOverType: function () {
            vmManager.edittingRow.WorkoverType = $scope.vm.WorkoverType;
            vmManager.edittingRow.isEdittingOverType = false;
        },
        //编辑班别
        editClassType: function (item) {
            item.isEdittingClassType = true;
            vmManager.getCurrentRow(item);
            $scope.vm.WorkClassType = item.WorkClassType;
        },
        //选择班别
        selectClassType: function () {
            vmManager.edittingRow.WorkClassType = $scope.vm.WorkClassType;
            vmManager.edittingRow.isEdittingClassType = false;

        },
        editOverHours: function (item) {         
            item.wkhing = true;
            vmManager.getCurrentRow(item);
            var dataitem = _.clone(item);
            dataitem.OpSign = leeDataHandler.dataOpMode.edit;
            $scope.vm = item;        
            if (item !== undefined && item !== null) {
                angular.forEach(vmManager.dataSets, function (edititem) { edititem.wkhing = false });
                leeHelper.copyVm(item, uiVM);
                $scope.vm = uiVM;
                vmManager.edittingRowIndex = item.rowindex;
                vmManager.edittingRow = item;
                item.wkhing = true;
                focusSetter['workeroverFocus'] = true;
            }
        },
        //设置单元格编辑状态
        setEditCellStatus: function (item, cellField, status) {
            var editCellSign = 'editting' + cellField + 'Mode';
            item[editCellSign] = status;
        },
        editItem: null,
        getWorkerInfo: function () {
            uiVM = $scope.vm;
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
                uiVM.DepartmentText = worker.Department;
                uiVM.WorkClassType = worker.ClassType;
            }
            else {
                uiVM.Department = null;
            }
        },
        //插入某一行
        insertRow: function (item) {
            var rowindex = item.rowindex;
            vmManager.edittingRowIndex = rowindex + 1;
            var vm = vmManager.createRowItem();
            leeHelper.insert(vmManager.dataSets, rowindex, vm);
            var index = 1;
            //重新更改行的索引
            angular.forEach(vmManager.dataSets, function (row) {
                row.rowindex = index;
                index += 1;
            })
        },
        //查询
        getWorkOverHoursByDatas: function (mode) {
            $scope.searchPromise = hrDataOpService.getWorkOverHoursData(qryDto.workDate, vmManager.selectDepartment, mode).then(function (datas) {
                vmManager.dataSource = datas;
                vmManager.searchDatas = datas;

            })
        },

        //后台查询
        getWorkOverHoursDatas: function (mode)
        {    
            var datas = hrDataOpService.getWorkOverHoursData(qryDto.workDate, vmManager.selectDepartment, 1).then(function (datas) {
                vmManager.searchDatas = datas;
            })
        },
        //加班汇总
        getWorkOverHourSumss: function (mode)
        {               
            vmManager.dataSourceSum = [];
            if (vmManager.selectDepartment == null)
            {
                var datas = hrDataOpService.getWorkOverHourSums(vmManager.searchYear, uiVM.ParentDataNodeText, 1).then(function (datas) {
                    vmManager.dataSourceSum = datas;
                })

            }
            else
            {
                var datas = hrDataOpService.getWorkOverHourSums(vmManager.searchYear, vmManager.selectDepartment, 1).then(function (datas) {
                    vmManager.dataSourceSum = datas;
                })

            }
           
        },
        //按工号查询汇总
        getWorkOverHourSumsByWorkId: function (mode)
        {        
         
            vmManager.dataSourceSum = [];
            var datas = hrDataOpService.getWorkOverHourSumsByWorkIds(vmManager.searchYear, vmManager.selectDepartment,qryDto.workId,2).then(function (datas) {
                vmManager.dataSourceSum = datas;
            })
        },
        //查询员工明细
        getWorkOverHoursWorkIdBydetail: function (mode)
        {
             vmManager.dataSourceSum = [];
             var datas = hrDataOpService.getWorkOverHoursWorkIdBydetails(vmManager.searchYear, vmManager.selectDepartment, qryDto.workId,3).then(function (datas) {
              vmManager.dataSourceSum = datas;
            })

        },

        //获取正在编辑的行
        getEdittingRow: function () {
            var rowItem = _.find(vmManager.dataSets, { rowindex: vmManager.edittingRowIndex });
            return rowItem;
        },
        //快速查找员工
        getWorkName: function () {
                    
            var qryItem = _.find(vmManager.dataSets, { WorkerName: vmManager.qryWorkName });          
            if (qryItem != null)
            {              
                vmManager.editworkOverHours(qryItem);                
            }   
            vmManager.qryWorkName=null;
        },
        //加载部门信息
        getDepartment: function ()
        {         
          vmManager.DepartmentDatas = [];
          $scope.searchPromise = hrDataOpService.getDepartments(uiVM.ParentDataNodeText).then(function (datas) {
             vmManager.DepartmentDatas = datas;
               
          })       
        },
        //载入模板
        getWorkOverHoursModes: function () {           
            vmManager.dataSets = [];
            vmManager.dataSource = [];
            tempVm.workOverCount = 0;
            if (vmManager.selectDepartment == null)
            {
                $scope.searchPromise = hrDataOpService.getWorkOverHoursMode(uiVM.ParentDataNodeText, qryDto.workDate).then(function (datas) {
                    //构建索引号
                    var rindex = 1;
                    angular.forEach(datas, function (item) {
                        item.rowindex = rindex;
                        rindex += 1;
                    });
                    vmManager.dataSource = datas;
                    vmManager.dataSets = datas;
                    //统计行数
                    $scope.tempVm.workOverCount = 0;
                    tempVm.tabCount = vmManager.dataSets.length;
                    angular.forEach(vmManager.dataSets, function (row) {
                        $scope.tempVm.workOverCount += parseFloat(row.WorkOverHours);
                    })
                })
            }
            else
            {
                $scope.searchPromise = hrDataOpService.getWorkOverHoursMode(vmManager.selectDepartment, qryDto.workDate).then(function (datas) {
                    //构建索引号
                    var rindex = 1;
                    angular.forEach(datas, function (item) {
                        item.rowindex = rindex;
                        rindex += 1;
                    });
                    vmManager.dataSource = datas;
                    vmManager.dataSets = datas;
                    //统计行数
                    $scope.tempVm.workOverCount = 0;
                    tempVm.tabCount = vmManager.dataSets.length;
                    angular.forEach(vmManager.dataSets, function (row) {
                        $scope.tempVm.workOverCount += parseFloat(row.WorkOverHours);
                    })
                })


            }
               
                         
        },
        //编辑加班时数
        editworkOverHours: function (item) {          
            if (item !== undefined && item !== null) {
              
                angular.forEach(vmManager.dataSets, function (edititem) { edititem.wkhing = false });
                leeHelper.copyVm(item, uiVM);
                $scope.vm = uiVM;
               
                vmManager.edittingRowIndex = item.rowindex;
                vmManager.edittingRow = item;
                item.wkhing = true;
                focusSetter['workeroverFocus'] = true;
            }
        },
        //编辑下一行加班时数
        editNextworkOverHours: function ($event, item) {
            if ($event.keyCode === 13 || $event.keyCode === 9) {
                //累计时数            
                $scope.vm.WorkDate = item.WorkDate;            
                tempVm.workOverCount = 0;
                angular.forEach(vmManager.dataSets, function (row) {
                    $scope.tempVm.workOverCount += parseFloat(row.WorkOverHours);
                })
                leeHelper.copyVm($scope.vm, vmManager.edittingRow);
                if (item.rowindex < vmManager.dataSets.length) {
                    vmManager.edittingRowIndex = item.rowindex + 1;
                    var rowItem = vmManager.getEdittingRow();                 
                    vmManager.editworkOverHours(rowItem);
                }
                else {
                    vmManager.edittingRow.wkhing = false;
                }
                vmManager.qryWorkName=null;
            }
                  
        },
        //加班时数输入框
        inputWorkOverHours: function ($event, item) {
            var year = new Date().getFullYear();
            var mm = new Date().getMonth() + 1;
            var dd = new Date().getDate();
           var nowdate = year + "-" + mm + "-" + dd;         
            uiVM.WorkDate = nowdate;
            item.WorkOverHours = $scope.vm.WorkOverHours;
            focusSetter.doWhenKeyDown($event, function () {
                vmManager.editNextworkOverHours($event, item);
            });
        },
        //显示后台数据
        showWorkOverHoursDatas: function () {
            vmManager.editWindowShow = !vmManager.editWindowShow;
        },
        //加班汇总显示
        showWorkOverHoursSum: function () {
            vmManager.editWindowShow1 = !vmManager.editWindowShow1;
        },
        //返回操作界面
        returnWorkOverHoursDatas: function () {
           
            vmManager.editWindowShow = false;
        },
        //返回操作界面
        returnWorkOverHoursSum: function () {

            vmManager.editWindowShow1 = false;
        },      
        //后台删除
        delModalWindow: $modal({
            title: "删除提示", content: "您确定要删除此信息吗?",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl, show: false,
            controller: function ($scope) {
                $scope.confirmDelete = function () {
                    uiVM.OpSign = leeDataHandler.dataOpMode.delete;
                    hrDataOpService.storeWorkOverHoursDt(uiVM).then(function (opresult) {
                        leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                            if (opresult.Result) {
                                vmManager.getWorkOverHoursDatas();
                                vmManager.del();

                            }

                        })

                    })

                    vmManager.delModalWindow.$promise.then(vmManager.delModalWindow.hide);
                };
            },
        }),
    };
    //导入excel
    $scope.selectFile = function (el) {
        vmManager.dataSets = [];
        vmManager.dataSource = [];
        tempVm.workOverCount = 0;
        leeHelper.upoadFile(el, function (fd) {
            hrDataOpService.importWorkOverHoursByDatas(fd).then(function (datas) {
                vmManager.dataSets = datas;
                vmManager.dataSource = datas;
                //统计行数
                tempVm.tabCount = vmManager.dataSets.length;
                angular.forEach(vmManager.dataSets, function (row) {
                    $scope.tempVm.workOverCount += row.WorkOverHours;
                })
            });
        });
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //编辑行
    operate.handleItem = function (item) {     
        var dataitem = _.clone(item);
        dataitem.OpSign = leeDataHandler.dataOpMode.edit;     
        $scope.vm = item;  
        $scope.vm.WorkoverType = item.WorkoverType;
        $scope.vm.WorkDate = item.WorkDate;
         vmManager.changeworkDate = item.WorkDate;     
     
         $scope.vm.WorkClassType = item.WorkClassType;
         $scope.vm.WorkOverHours = item.WorkOverHours;
        dialog.show();
        if (item !== undefined && item !== null) {
            angular.forEach(vmManager.dataSets, function (edititem) { edititem.wkhing = false });
            leeHelper.copyVm(item, uiVM);
            $scope.vm = uiVM;
            vmManager.edittingRowIndex = item.rowindex;
            vmManager.edittingRow = item;
            item.wkhing = true;
            focusSetter['workeroverFocus'] = true;
        }
    };

    //后台编辑
    operate.editItem = function (item) {      
        item.OpSign = leeDataHandler.dataOpMode.edit;      
        $scope.vm = uiVM = item;      
        dialog.show();
    };
    //后台保存
    operate.editALL = function (isValid) {       
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            hrDataOpService.storeWorkOverHoursDt(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var mode = _.clone(uiVM)
                        mode.Id_Key === opresult.Id_Key;
                        if (mode.OpSign === leeDataHandler.dataOpMode.add) {
                            vmManager.datasource.push(mode);
                        }
                        vmManager.init();
                        dialog.close();
                        vmManager.getWorkOverHoursDatas(1);
                    }
                })
            })
        })
        dialog.close();
       
      
    };
    //后台删除
    operate.deleteItem = function (item) {
        vmManager.delItem = item;
        $scope.vm = uiVM = item;      
        operate.deleteDialog();
    }
    //关闭窗口
    operate.updateItem = function (item) {       
        tempVm.workOverCount = 0;
        if (vmManager.changeworkDate == null || uiVM.WorkDate == null) { leePopups.alert("亲！您未选择申请日期"); return; }           
        if (uiVM.WorkReason == null) { leePopups.alert("亲！您未填写加班原因"); return; }
        if (vmManager.workDayDate == null) { vmManager.workDayDate = vmManager.changeworkDate; }
        if (vmManager.workNightDate == null) { vmManager.workNightDate = vmManager.changeworkDate; }         
        var _workDayTime = "从 " + vmManager.changeworkDate + " " + vmManager.workDayTimeStart.pattern("HH:mm") + " 至 " + vmManager.workDayDate + " " + vmManager.workDayTimeEnd.pattern("HH:mm");
        var _workNightTime = "从 " + vmManager.changeworkDate + " " + vmManager.workNightTimeStart.pattern("HH:mm") + " 至 " + vmManager.workNightDate + " " + vmManager.workNightTimeEnd.pattern("HH:mm");
        var qryDateFormat = $filter('date')(uiVM.WorkDate, "yyyyMM");
           uiVM.WorkDayTime = _workDayTime;
           uiVM.WorkNightTime = _workNightTime;          
        angular.forEach(vmManager.dataSets, function (row) {
            $scope.tempVm.workOverCount += parseFloat(row.WorkOverHours);           
            row.WorkDate = uiVM.WorkDate;
            row.WorkReason = uiVM.WorkReason;
            row.WorkDayTime = _workDayTime;
            row.WorkNightTime = _workNightTime;
            row.WorkoverType = uiVM.WorkoverType;
            row.QryDate = qryDateFormat;
            row.WorkClassType = uiVM.WorkClassType;
            row.WorkOverHours = uiVM.WorkOverHours;
           
        });           
        dialog.close();
        focusSetter['workeroverFocus'] = true;             
    },
        //批量保存提示窗口
        operate.saveDialog = function () {
            leePopups.confirm("保存提示", "加班单编辑好了，您确定保存吗？", function () {
                operate.saveAll();
            });
        },
        operate.deleteDialog = function () {
        leePopups.confirm("删除提示", "是否确定删除吗？", function () {
            uiVM.OpSign = leeDataHandler.dataOpMode.delete;
            hrDataOpService.storeWorkOverHoursDt(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        vmManager.getWorkOverHoursDatas();
                        vmManager.del();
                    }
                })
            })
            vmManager.delModalWindow.$promise.then(vmManager.delModalWindow.hide);

            });
        }
        //批量保存
        operate.saveAll = function () {
            if (vmManager.dataSets.length == 0) {
                alert("没有任何记录！");
                return;
            }
            hrDataOpService.storeHandlWorkOverHoursDt(vmManager.dataSets).then(function (opResult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                    vmManager.dataSets = [];
                });
            });
        };
    //焦点设置
    var focusSetter = {
        workeroverFocus: false,

        //移动焦点到指定对象
        moveFocusTo: function ($event, elPreName, elNextName) {
            if ($event.keyCode === 13 || $event.keyCode === 39 || $event.keyCode === 9) {
                focusSetter[elNextName] = true;
            }
            else if ($event.keyCode === 37) {
                focusSetter[elPreName] = true;
            };
        },
        doWhenKeyDown: function ($event, fn) {
            if ($event.keyCode === 13 || $event.keyCode === 39 || $event.keyCode === 9) { fn(); }
        },
        changeEnter: function ($event, elPreName, elNextName) {
            focusSetter.moveFocusTo($event, elPreName, elNextName)
        }
    };
    $scope.focus = focusSetter;

    vmManager.getDepartment();
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
        SlotCardTime: null,
        LeaveHours: null,
        LeaveTimeRegion: null,
        SlotExceptionType: null,
        SlotExceptionMemo: null,
        Id_Key: 0
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
        LeaveTimeRegionEnd: null
    };
    var slotCardVM = {
        WorkerId: null,
        WorkerName: null,
        AttendanceDate: null,
        SlotCardTime1: null,
        SlotCardTime2: null,
        ForgetSlotReason: null
    };
    //视图管理器
    var vmManager = {
        activeTab: 'autoCheckExceptionTab',
        opSign: null,
        leaveTypes: [],
        //部门信息
        departments: [],
        //存储到数据库中的数据集
        dbDataSet: [],
        //异常数据集
        dataItems: [],
        //选定的项
        selectedItem: null,
        yearMonth: '',
        autoCheckExceptionData: function () {
            vmManager.dataItems = [];
            $scope.handlePromise = hrDataOpService.autoCheckExceptionSlotData(vmManager.yearMonth).then(function (datas) {
                angular.forEach(datas, function (item) {
                    var dataItem = _.clone(uiVM);
                    leeHelper.copyVm(item, dataItem);
                    dataItem.DepartmentText = leeHelper.getDepartmentText(vmManager.departments, item.Department);
                    vmManager.dataItems.push(dataItem);
                });
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
                });
            });
        }
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
                    var dataitem = _.find(vmManager.dbDataSet, {
                        Id_Key: vmManager.selectedItem.Id_Key
                    });
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
        show: false
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
                    var dataitem = _.find(vmManager.dbDataSet, {
                        Id_Key: vmManager.selectedItem.Id_Key
                    });
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
        show: false
    });

    var handleAttendExceptionData = function (exceptionMemo) {
        var dataitem = _.find(vmManager.dbDataSet, {
            Id_Key: vmManager.selectedItem.Id_Key
        });
        if (dataitem !== undefined) {
            dataitem.OpSign = vmManager.opSign;
            dataitem.SlotExceptionMemo = exceptionMemo;
            dataitem.HandleSlotExceptionStatus = 2;
        }
    };

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
        var departments = _.where(datas, {
            TreeModuleKey: "Organization"
        });
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
        Id_Key: null
    };
    $scope.vm = uiVM;
    var originalVM = _.clone(uiVM);

    //查询字段
    var queryFields = {
        workerId: null,
        department: null,
        receiveMonth: null
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
                id: "夏季厂服", text: "夏季厂服", specifyList: [{
                    id: "34", text: "34"
                }, {
                    id: "35", text: "35"
                }, {
                    id: "36", text: "36"
                }, {
                    id: "37", text: "37"
                }, {
                    id: "38", text: "38"
                }, {
                    id: "39", text: "39"
                }, {
                    id: "40", text: "40"
                }, {
                    id: "41", text: "41"
                }, {
                    id: "42", text: "42"
                }, {
                    id: "43", text: "43"
                }, {
                    id: "44", text: "44"
                }]
            },
            {
                id: "冬季厂服", text: "冬季厂服", specifyList: [{
                    id: "S", text: "S"
                }, {
                    id: "M", text: "M"
                }, {
                    id: "L", text: "L"
                }, {
                    id: "XL", text: "XL"
                }, {
                    id: "XXL", text: "XXL"
                }, {
                    id: "XXXL", text: "XXXL"
                }]
            }
        ],
        selectProductName: function () {
            var product = _.find(vmManager.productNames, {
                id: uiVM.ProductName
            });
            if (!angular.isUndefined(product)) {
                vmManager.closeSpecifies = product.specifyList;
            }
        },
        dealwithTypes: [
            {
                id: "领取新衣", text: "领取新衣"
            },
            {
                id: "以旧换新", text: "以旧换新"
            },
            {
                id: "以旧换旧", text: "以旧换旧"
            },
            { id: "购买新衣", text: "购买新衣" }
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
                show: false
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
                else if (mdl.OpSign === 'edit') {
                    var item = _.find(vmManager.storeDataset, {
                        Id_Key: uiVM.Id_Key
                    });
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
