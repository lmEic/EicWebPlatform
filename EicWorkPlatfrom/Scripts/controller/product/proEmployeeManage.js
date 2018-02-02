/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />
var proEmployeeModule = angular.module('bpm.productApp');
proEmployeeModule.factory('proEmployeeDataService', function (ajaxService) {
    var dataAccess = {};
    var urlPrefix = '/' + leeHelper.controllers.proEmployee+'/';
  
    //**************************人员管理**********************************

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
    //**************************请假管理**********************************
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
    dataAccess.getLeaveAskManagerData = function (workerId,leaveSate,department,leaveType,mode) {
        var url = urlPrefix + 'GetLeaveAskManagerDatas';
        return ajaxService.getData(url, {
            workerId: workerId,
            leaveSate:leaveSate,
            department: department,
            leaveType: leaveType,
            mode: mode
        });

    };
    //加载部门
    dataAccess.getDepartment = function (dataNodeName) {
        var url = urlPrefix + 'GetDepartment';
        return ajaxService.getData(url, {
            dataNodeName: dataNodeName
        })
    };
   //**************************************加班管理*********************************
    //按日期查询加班数据
    dataAccess.getWorkOverHoursData = function (workDate, departmentText, mode) {
        var url = urlPrefix + 'GetWorkOverHoursData';
        return ajaxService.getData(url, {
            workDate: workDate,
            departmentText: departmentText,
            mode: mode
        });
    };
    //载入模板
    dataAccess.getWorkOverHoursMode = function (departmentText,postNature, workDate,mode) {
        var url = urlPrefix + 'GetWorkOverHoursMode';
        return ajaxService.getData(url, {
            departmentText: departmentText,
            postNature:postNature,
            workDate: workDate,
            mode:mode
        });
    };

    //按加班时数汇总查询
    dataAccess.getWorkOverHourSums = function (qrydate, departmentText, mode) {
        var url = urlPrefix + 'GetWorkOverHoursSum';
        return ajaxService.getData(url, {
            qrydate: qrydate,
            departmentText: departmentText,
            mode: mode
        })
    };
    //按工号查询加班时数汇总
    dataAccess.getWorkOverHourSumsByWorkIds = function (qrydate, departmentText, workId, mode) {
        var url = urlPrefix + 'GetWorkOverHourSumsByWorkId';
        return ajaxService.getData(url, {
            qrydate: qrydate,
            departmentText: departmentText,
            workId: workId,
            mode: mode
        })
    };
    //查询员工明细
    dataAccess.getWorkOverHoursWorkIdBydetails = function (qrydate, departmentText, mode) {
        var url = urlPrefix + 'GetWorkOverHoursWorkIdBydetail';
        return ajaxService.getData(url, {
            qrydate: qrydate,
            departmentText: departmentText,
           
            mode: mode
        })
    };

    //批量保存加班数据
    dataAccess.storeHandlWorkOverHoursDt = function (workOverHours) {
        var url = urlPrefix + 'HandlWorkOverHoursDt';
        return ajaxService.postData(url, {
            workOverHours: workOverHours

        });
    };
    //单条修改保存
    dataAccess.storeWorkOverHoursDt = function (model) {
        var url = urlPrefix + 'StoreWorkOverHoursRecordSingle';
        return ajaxService.postData(url, {
            model: model
        });
    },
        //加班时数导入Excel
        dataAccess.importWorkOverHoursByDatas = function (file) {
            var url = urlPrefix + 'ImportWorkOverHoursDatas';
            return ajaxService.uploadFile(url, file);
        };
    dataAccess.getDepartments = function (datanodeName) {
        var url = urlPrefix + 'GetDepartment';
        return ajaxService.getData(url, {
            datanodeName: datanodeName

        })
    }
    //批量删除后台数据
    dataAccess.handlDeleteWorkOverHoursDt = function (departmentText, workDate) {
        var url = urlPrefix + 'HandlDeleteWorkOverHoursDt';
        return ajaxService.postData(url, {
            departmentText: departmentText,
            workDate: workDate
        })
    }  

    //计算时间
    dataAccess.calculateDate = function (date2, date1) {
        var url = urlPrefix + 'CalculateDate';
        return ajaxService.getData(url, {
            date2: date2,
            date1:date1
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
//请假管理
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

    $scope.$watch('vmManager.department', function () {
        if (vmManager.department !== null) {
            vmManager.selectDepartment();
        }
    });

    var dialog = $scope.dialog = leePopups.dialog();
    var queryFields = {
        workerId: null,
        leaveSate: null,
        leaveType:null
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
        leaveStates: [{ id: '己填写', text: '己填写' }, { id: '未填写', text: "未填写" }],
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
        DepartmentDatas: [],  
        selectLeaveType: [],
        //添加请假类别
        addLeaveType: function () {
            selectLeaveType=[],
            vmManager.selectLeaveType = vmManager.selectLeaveType + " " + uiVM.LeaveType;
            uiVM.LeaveType = vmManager.selectLeaveType;
        },
        //删除请假类别
        deleteLeaveType: function () {
            if (uiVM.LeaveType == undefined) {
                vmManager.selectLeaveType=[]
                uiVM.LeaveType = []

            }
            else {
                vmManager.selectLeaveType = uiVM.LeaveType

            }
            
        },
        //拼接时间
        SetDate: function ()
        {                              
            var workStart =uiVM.LeaveApplyDate+" "+vmManager.workTimeStart.pattern("HH:mm");
            var workEnd = uiVM.LeaveAskDate + " "+vmManager.workTimeEnd.pattern("HH:mm");
            uiVM.LeaveTimerStart = workStart;
            uiVM.LeaveTimerEnd = workEnd;         
        },
        //查询请假数据
        getLeaveAskManagerDatas: function (mode) {    
            if (vmManager.selectDepartment == null) { leePopups.alert("亲，您未选择部门"); return;}        
            queryFields.workerId = uiVM.WorkerId; 
            queryFields.leaveSate = uiVM.LeaveState;
            queryFields.leaveType = uiVM.LeaveType;
            vmManager.searchDatas = [];  
            vmManager.datasource = [];      
            var datas = proEmployeeDataService.getLeaveAskManagerData(queryFields.workerId,queryFields.leaveSate,vmManager.selectDepartment,queryFields.leaveType, mode).then(function (datas) {
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
        },    
         bindingDepartments: function () {
            var departments;
            var user = leeDataHandler.dataStorage.getLoginedUser();
            if (_.isObject(user)) {
                vmManager.organizationUnits = user.organizationUnits;
            }

        },
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
            item.OpSign = leeDataHandler.dataOpMode.delete;
          //  vmManager.delItem = item;
            $scope.vm = uiVM = item;
            operate.deleteDialog();
    }
    operate.deleteDialog = function () {
        leePopups.confirm("删除提示", "是否确定删除吗？", function () {
            uiVM.OpSign = leeDataHandler.dataOpMode.delete;
            proEmployeeDataService.storeLeaveAskManagerDatas(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {                   
                       vmManager.init();
                       vmManager.getLeaveAskManagerDatas(4);
                        deleteDialog.close();
                    }
                })
            })       
        });
    }  
    //保存
    operate.saveAll = function (isValid) {
        vmManager.SetDate(); 
        if (uiVM.LeaveApplyDate == null || uiVM.LeaveAskDate == null) { leePopups.alert("亲！您未选择请假日期"); return; }        
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
                        vmManager.selectLeaveType=[],
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

    //计算时数
    operate.calculateHours = function () { 
 
        uiVM.LeaveApplyDate = $scope.vm.LeaveApplyDate;
        uiVM.LeaveAskDate = $scope.vm.LeaveAskDate; 
       
        var dt01 = uiVM.LeaveApplyDate + " " + vmManager.workTimeStart.pattern("HH:mm");
        var dt02 = uiVM.LeaveAskDate + " " + vmManager.workTimeEnd.pattern("HH:mm");   
        proEmployeeDataService.calculateDate(dt02, dt01).then(function (data) {
            $scope.vm.LeaveHours = data;        
        });
    }

   // vmManager.getDepartments();
    vmManager.bindingDepartments();


});
//加班管理
proEmployeeModule.controller('workOverHoursManageCtrl', function ($scope, $modal, $filter, proEmployeeDataService, dataDicConfigTreeSet, connDataOpService) {
    ///ui视图模型
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        WorkoverType: null,
        WorkClassType: null,
        WorkDate: null,
        WorkOverHours: null,
        Remark: null,
        DepartmentText: null,
        WorkStatus: '在职',
        QryDate: null,
        WorkReason: '产线加班',
        WorkDayTime: null,
        WorkNightTime: null,
        WorkDayTime1: null,
        WorkNightTime1: null,
        PostNature: null, 
        WorkOverHoursCount: 0,
        WorkOverHoursNightCount:0,
        ParentDataNodeText: leeDataHandler.dataStorage.getLoginedUser().organization.B,
        BackgroundIndex: null,
        OpPerson: leeDataHandler.dataStorage.getLoginedUser().userName,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null,

    };
    $scope.vm = uiVM;
    var dialog = $scope.dialog = leePopups.dialog();
    var qryDto = {
        departmentText: leeLoginUser.departmentText,
        workDate: new Date().toDateString(),
        workId: null,
    };
    $scope.query = qryDto;
    var originalVM = _.clone(uiVM);
    $scope.tempVm = tempVm = {
        tabCount: 0,
        department: leeLoginUser.departmentText,
        workOverCount: 0,
    };
    var vmManager = {
        BackgroundIndexFirst: null,
        getworkoverhours: null,
        getworkdate: null,
        getcolorindex: null,
        selectDepartment: null,
        selectPostNature:null,
        searchYear: new Date().getFullYear(),
        changeworkDate: null,
        workDayDate: null,
        workNightDate: null,
        workDayDate1: null,
        workNightDate1: null,
        workDayTimeStart: new Date(00, 00, 00),
        workDayTimeEnd: new Date(00, 00, 00),
        workNightTimeStart: new Date(00, 00, 00),
        workNightTimeEnd: new Date(00, 00, 00),
        workDayTimeStart1: new Date(00, 00, 00),
        workDayTimeEnd1: new Date(00, 00, 00),
        workNightTimeStart1: new Date(00, 00, 00),
        workNightTimeEnd1: new Date(00, 00, 00),
        classTypes: [{ id: '白班', text: '白班' }, { id: '晚班', text: "晚班" }],      
        overTypes: [{ id: '平时加班', text: '平时加班' }, { id: '假日加班', text: '假日加班' }, { id: '节假日加班', text: '节假日加班' }],
        workOverHourss: [{ id: 0.5, text: 0.5 }, { id: 1.0, text: 1.0 }, { id: 1.5, text: 1.5 }, { id: 2.0, text: 2.0 }, { id: 2.5, text: 2.5 }],
        workStatuss: [{ id: '在职', text: '在职' }, { id: '离职', text: '离职' }],
        postNatures: [{ id: '间接', text: '间接' }, {id:'直接',text:'直接'}],
        dataSets: [],
        dataSource: [],
        searchDatas: [],
        dataSourceSum: [],
        dataSourceDetail: [],
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
        DepartmentDatas: [],
        signDepSum: 'true',
        signPerSum: 'false',  
        workhoursDayCount: null,
        workhoursNightCount:null,
        workhoursNightCountShow: 'false',
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
            var vm = _.clone(item);
            vmManager.dataSets.push(vm);
            item.editting = false;
            var rowindex = item.rowindex;
            vmManager.edittingRowIndex = rowindex + 1;

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
            item.isEdittingOverType = false;
            item.isEdittingClassType = false;
            item.isEdittingPostNature = false;
            item.wkhing = false;
            item.wkhing1 = false;
            item.wkhing2 = false;
            item.wkhing3 = false;
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

        //编辑直间类型
        editPostNature: function (item) {
            item.isEdittingPostNature = true;
            vmManager.getCurrentRow(item);
            $scope.vm.PostNature = item.PostNature;
        },
        //选择直间类型
        selectPost: function () {
     
            vmManager.edittingRow.PostNature = $scope.vm.PostNature;
            vmManager.edittingRow.isEdittingPostNature = false;
         
          
        },

        editOverHours: function (item) {
            item.pheditting = false;
            item.isEdittingWorkerId = false;
            item.isEdittingOverType = false;
            item.isEdittingClassType = false;
            item.isEdittingPostNature = false;
            item.wkhing2 = false;
            item.wkhing1 = false;
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
        //编辑部门
        editdepartmentText: function (item) {
            item.pheditting = false;
            item.isEdittingWorkerId = false;
            item.isEdittingOverType = false;
            item.isEdittingClassType = false;
            item.isEdittingPostNature = false;
            item.wkhing = false;//时数
            item.wkhing1 = false;//备注
            item.wkhing2 = true;//部门
            vmManager.getCurrentRow(item);
            var dataitem = _.clone(item);
            dataitem.OpSign = leeDataHandler.dataOpMode.edit;
            $scope.vm = item;
            if (item !== undefined && item !== null) {
                angular.forEach(vmManager.dataSets, function (edititem) { edititem.wkhing2 = false });
                leeHelper.copyVm(item, uiVM);
                $scope.vm = uiVM;
                vmManager.edittingRowIndex = item.rowindex;
                vmManager.edittingRow = item;
                item.wkhing2 = true;
                focusSetter['departmentTextFocus'] = true;
            }

        },
        editworkdepartmentText: function (item) {
            if (item !== undefined && item !== null) {

                angular.forEach(vmManager.dataSets, function (edititem) { edititem.wkhing2 = false });
                leeHelper.copyVm(item, uiVM);
                $scope.vm = uiVM;
                vmManager.edittingRowIndex = item.rowindex;
                vmManager.edittingRow = item;
                item.wkhing2 = true;
                focusSetter['departmentTextFocus'] = true;
            }
        },
        editNextdepartmentText: function ($event, item) {
            if ($event.keyCode === 13 || $event.keyCode === 9) {

                leeHelper.copyVm($scope.vm, vmManager.edittingRow);
                if (item.rowindex < vmManager.dataSets.length) {
                    vmManager.edittingRowIndex = item.rowindex + 1;
                    var rowItem = vmManager.getEdittingRow();
                    vmManager.editworkdepartmentText(rowItem);
                }
                else {
                    vmManager.edittingRow.wkhing2 = false;
                }
            }
        },
        inputdepartmentText: function ($event, item) {
            item.DepartmentText = $scope.vm.DepartmentText;
            focusSetter.doWhenKeyDown($event, function () {
                vmManager.editNextdepartmentText($event, item);
            });
        },
        //编辑注备
        editremark: function (item) {
            item.pheditting = false;
            item.isEdittingWorkerId = false;
            item.isEdittingOverType = false;
            item.isEdittingClassType = false;
            item.isEdittingPostNature = false;
            item.wkhing2 = false;
            item.wkhing = false;
            item.wkhing1 = true;
            vmManager.getCurrentRow(item);
            var dataitem = _.clone(item);
            dataitem.OpSign = leeDataHandler.dataOpMode.edit;
            $scope.vm = item;
            if (item !== undefined && item !== null) {
                angular.forEach(vmManager.dataSets, function (edititem) { edititem.wkhing1 = false });
                leeHelper.copyVm(item, uiVM);
                $scope.vm = uiVM;
                vmManager.edittingRowIndex = item.rowindex;
                vmManager.edittingRow = item;
                item.wkhing1 = true;
                focusSetter['remarkFocus'] = true;
            }
        },
        editworkremark: function (item) {
            if (item !== undefined && item !== null) {

                angular.forEach(vmManager.dataSets, function (edititem) { edititem.wkhing1 = false });
                leeHelper.copyVm(item, uiVM);
                $scope.vm = uiVM;
                vmManager.edittingRowIndex = item.rowindex;
                vmManager.edittingRow = item;
                item.wkhing1 = true;
                focusSetter['remarkFocus'] = true;
            }
        },
        editNextremark: function ($event, item) {
            if ($event.keyCode === 13 || $event.keyCode === 9) {
                //累计时数
                leeHelper.copyVm($scope.vm, vmManager.edittingRow);
                if (item.rowindex < vmManager.dataSets.length) {
                    vmManager.edittingRowIndex = item.rowindex + 1;
                    var rowItem = vmManager.getEdittingRow();
                    vmManager.editworkremark(rowItem);
                }
                else {
                    vmManager.edittingRow.wkhing1 = false;
                }
            }
        },
        inputremark: function ($event, item) {
            item.Remark = $scope.vm.Remark;
            focusSetter.doWhenKeyDown($event, function () {
                vmManager.editNextremark($event, item);
            });
        },
        //编辑工号   
        editId: function (item) {
            item.pheditting = false;
            item.isEdittingWorkerId = false;
            item.isEdittingOverType = false;
            item.isEdittingClassType = false;
            item.isEdittingPostNature = false;
            item.wkhing2 = false;
            item.wkhing = false;
            item.wkhing1 = false;
            item.wkhing3 = true;
            vmManager.getCurrentRow(item);
            var dataitem = _.clone(item);
            dataitem.OpSign = leeDataHandler.dataOpMode.edit;
            $scope.vm = item;
            if (item !== undefined && item !== null) {
                angular.forEach(vmManager.dataSets, function (edititem) { edititem.wkhing3 = false });
                leeHelper.copyVm(item, uiVM);
                $scope.vm = uiVM;
                vmManager.edittingRowIndex = item.rowindex;
                vmManager.edittingRow = item;
                item.wkhing3 = true;
                focusSetter['workerIdFocus'] = true;
            }

        },
        editworkerId: function (item) {
            if (item !== undefined && item !== null) {
                angular.forEach(vmManager.dataSets, function (edititem) { edititem.wkhing3 = false });
                leeHelper.copyVm(item, uiVM);
                $scope.vm = uiVM;
                vmManager.edittingRowIndex = item.rowindex;
                vmManager.edittingRow = item;
                item.wkhing3 = true;
                focusSetter['workerIdFocus'] = true;

            }
        },
        editNextId: function ($event, item) {
            if ($event.keyCode === 13 || $event.keyCode === 9) {
                //累计时数                                        
                leeHelper.copyVm($scope.vm, vmManager.edittingRow);
                if (item.rowindex < vmManager.dataSets.length) {
                    vmManager.edittingRowIndex = item.rowindex + 1;
                    var rowItem = vmManager.getEdittingRow();
                    vmManager.editworkerId(rowItem);
                }
                else {
                    vmManager.edittingRow.wkhing3 = false;
                }
            }
        },
        inputId: function ($event, item) {
            //vmmvmManager.getWorkerInfo();
            item.WorkerId = $scope.vm.WorkerId;
            focusSetter.doWhenKeyDown($event, function () {
                vmManager.editNextId($event, item);
            });
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
                // uiVM.DepartmentText = worker.Department;
                uiVM.PostNature = worker.PostNature;
                uiVM.WorkClassType = worker.WorkClassType;            
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
            if (vmManager.selectDepartment == null) { leePopups.alert("亲，您未选择部门"); return; }  
            $scope.searchPromise = proEmployeeDataService.getWorkOverHoursData(qryDto.workDate, vmManager.selectDepartment, mode).then(function (datas) {
                vmManager.dataSource = datas;
                vmManager.searchDatas = datas;

            })
        },

        //后台查询
        getWorkOverHoursDatas: function (mode) {
            if (vmManager.selectDepartment == null) { leePopups.alert("亲，您未选择部门"); return; }  
            var datas = proEmployeeDataService.getWorkOverHoursData(qryDto.workDate, vmManager.selectDepartment, 1).then(function (datas) {
                vmManager.searchDatas = datas;
            })
        },
        //加班汇总
        getWorkOverHourSumss: function (mode) {
            if (vmManager.selectDepartment == null) { leePopups.alert("亲，您未选择部门"); return; }  
            vmManager.signDepSum = true;
            vmManager.signPerSum = false;        
           vmManager.workhoursNightCountShow = true;  
            vmManager.dataSourceSum = [];
            if (vmManager.selectDepartment == null) {
                var datas = proEmployeeDataService.getWorkOverHourSums(vmManager.searchYear, uiVM.ParentDataNodeText, 1).then(function (datas) {
                    vmManager.dataSourceSum = datas;
                })
            }
            else
            {
                var datas = proEmployeeDataService.getWorkOverHourSums(vmManager.searchYear, vmManager.selectDepartment, 1).then(function (datas) {
                    vmManager.dataSourceSum = datas;
                })
            }

        },
        //按工号查询汇总
        getWorkOverHourSumsByWorkId: function (mode) {
            if (vmManager.selectDepartment == null) { leePopups.alert("亲，您未选择部门"); return; }  
            vmManager.dataSourceSum = [];
            vmManager.signPerSum = true;
            vmManager.signDepSum = false;
            var datas = proEmployeeDataService.getWorkOverHourSumsByWorkIds(vmManager.searchYear, vmManager.selectDepartment, qryDto.workId, 2).then(function (datas) {
                vmManager.dataSourceSum = datas;
            })
        },
        //查询员工明细
        getWorkOverHoursWorkIdBydetail: function (mode) {
            if (vmManager.selectDepartment == null) { leePopups.alert("亲，您未选择部门"); return; }  
            vmManager.dataSourceSum = [];
            vmManager.signPerSum = true;
            vmManager.signDepSum = false;
            var datas = proEmployeeDataService.getWorkOverHoursWorkIdBydetails(vmManager.searchYear, vmManager.selectDepartment, 3).then(function (datas) {
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
            if (qryItem != null) {
                vmManager.editworkOverHours(qryItem);
                vmManager.qryWorkName = null;
            }
        },
        //加载部门信息
        getDepartment: function () {
            vmManager.DepartmentDatas = [];
            $scope.searchPromise = proEmployeeDataService.getDepartments(uiVM.ParentDataNodeText).then(function (datas) {
                vmManager.DepartmentDatas = datas;
            })
        },
        bindingDepartments: function () {
            vmManager.workhoursNightCountShow = false;
            var departments;
            var user = leeDataHandler.dataStorage.getLoginedUser();
            if (_.isObject(user)) {
                vmManager.organizationUnits = user.organizationUnits;
            }
        },
        //载入模板
        getWorkOverHoursModes: function () {    
            vmManager.dataSets = [];
            vmManager.dataSource = [];
            tempVm.workOverCount = 0;
            if (vmManager.selectPostNature==null) {
                $scope.searchPromise = proEmployeeDataService.getWorkOverHoursMode(vmManager.selectDepartment,vmManager.selectPostNature, qryDto.workDate,1).then(function (datas) {
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
            else {
                $scope.searchPromise = proEmployeeDataService.getWorkOverHoursMode(vmManager.selectDepartment,vmManager.selectPostNature,qryDto.workDate,2).then(function (datas) {
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
                vmManager.qryWorkName = null;
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
                    proEmployeeDataService.storeWorkOverHoursDt(uiVM).then(function (opresult) {
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
            proEmployeeDataService.importWorkOverHoursByDatas(fd).then(function (datas) {
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
    //编辑行批量
    operate.handleItem = function (item) {
        var dataitem = _.clone(item);
        dataitem.OpSign = leeDataHandler.dataOpMode.edit;
        item.ParentDataNodeText = $scope.vm.ParentDataNodeText;
        item.OpPerson = $scope.vm.OpPerson;
        $scope.vm = item;
        vmManager.changeworkDate = item.WorkDate;
        vmManager.workDayDate = item.WorkDate;
        vmManager.workNightDate = item.WorkDate;
        vmManager.workDayDate1 = item.WorkDate;
        vmManager.workNightDate1 = item.WorkDate;
        dialog.show();
    };
    //后台编辑
    operate.editItem = function (item) {

        //构建索引号
        vmManager.getWorkOverHoursDatas(1);
        var rindex = 0;
        angular.forEach(vmManager.searchDatas, function (item) {
            item.rowindex1 = rindex;
            rindex += 1;

        });
        item.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVM = item;
        vmManager.workDayDate = item.WorkDate;
        vmManager.workNightDate = item.WorkDate;
        vmManager.workDayDate1 = item.WorkDate;
        vmManager.workNightDate1 = item.WorkDate;
        vmManager.BackgroundIndexFirst = item.rowindex1;
        dialog.show();
    };
    operate.addItem = function (item) {
        //构建索引号
        vmManager.getWorkOverHoursDatas(1);
        var rindex = 0;
        angular.forEach(vmManager.searchDatas, function (item) {
            item.rowindex1 = rindex;
            rindex += 1;

        });
        item.OpSign = leeDataHandler.dataOpMode.add;
        $scope.vm = uiVM = item;
        vmManager.workDayDate = item.WorkDate;
        vmManager.workNightDate = item.WorkDate;
        vmManager.workDayDate1 = item.WorkDate;
        vmManager.workNightDate1 = item.WorkDate;
        vmManager.BackgroundIndexFirst = item.rowindex1;
        dialog.show();
    }
    //后台保存
    operate.editALL = function (isValid) {
        uiVM.BackgroundIndex = vmManager.BackgroundIndexFirst;
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            proEmployeeDataService.storeWorkOverHoursDt(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var mode = _.clone(uiVM)
                        mode.Id_Key === opresult.Id_Key;
                        if (mode.OpSign === leeDataHandler.dataOpMode.add) {
                            vmManager.datasource.push(mode);
                        }
                        vmManager.init();
                        dialog.close();
                        vmManager.getWorkOverHoursDatas();
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
    //后台批量删除  
    operate.handleDel = function () {
        if (vmManager.searchDatas.length == 0 || vmManager.searchDatas == null) { leePopups.alert("亲，没有要删除的信息！"); return; }
        leePopups.confirm("删除提示", "是否确定删除吗？", function () {
            proEmployeeDataService.handlDeleteWorkOverHoursDt(vmManager.selectDepartment, qryDto.workDate).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        vmManager.getWorkOverHoursDatas();
                        // vmManager.del();
                    }
                })
            })
            vmManager.delModalWindow.$promise.then(vmManager.delModalWindow.hide);
        });

    }
    //关闭窗口
    operate.updateItem = function (item) {
        vmManager.workDayDate = uiVM.WorkDate;
        vmManager.workNightDate = uiVM.WorkDate;
        vmManager.workDayDate1 = uiVM.WorkDate;
        vmManager.workNightDate1 = uiVM.WorkDate;
        tempVm.workOverCount = 0;
        uiVM.WorkoverType = $scope.vm.WorkoverType;
        uiVM.DepartmentText = $scope.vm.DepartmentText;
        uiVM.WorkDate = $scope.vm.WorkDate;
        uiVM.WorkClassType = $scope.vm.WorkClassType;
        uiVM.PostNature = $scope.vm.PostNature;      
        uiVM.Remark = $scope.vm.Remark;
        uiVM.ParentDataNodeText = $scope.vm.ParentDataNodeText;
        uiVM.OpPerson = $scope.vm.OpPerson;
        if (vmManager.changeworkDate == null) { leePopups.alert("亲！您未选择申请日期"); return; }
        if (uiVM.WorkReason == null) { leePopups.alert("亲！您未填写加班原因"); return; }    
        var _workDayTime = "从 " + uiVM.WorkDate + " " + vmManager.workDayTimeStart.pattern("HH:mm") + " 至 " + vmManager.workDayDate + " " + vmManager.workDayTimeEnd.pattern("HH:mm");
        var _workNightTime = "从 " + uiVM.WorkDate + " " + vmManager.workNightTimeStart.pattern("HH:mm") + " 至 " + vmManager.workNightDate + " " + vmManager.workNightTimeEnd.pattern("HH:mm");
        var _workDayTime1 = "从 " + uiVM.WorkDate + " " + vmManager.workDayTimeStart1.pattern("HH:mm") + " 至 " + vmManager.workDayDate1 + " " + vmManager.workDayTimeEnd1.pattern("HH:mm");
        var _workNightTime1 = "从 " + uiVM.WorkDate + " " + vmManager.workNightTimeStart1.pattern("HH:mm") + " 至 " + vmManager.workNightDate1 + " " + vmManager.workNightTimeEnd1.pattern("HH:mm");
        var qryDateFormat = $filter('date')(uiVM.WorkDate, "yyyyMM");      
        uiVM.WorkOverHours = $scope.vm.WorkOverHours;
        angular.forEach(vmManager.dataSets, function (row) {
            $scope.tempVm.workOverCount += parseFloat(row.WorkOverHours);
            row.WorkDate = uiVM.WorkDate;
            row.WorkDayTime = _workDayTime;
            row.WorkNightTime = _workNightTime;
            row.WorkDayTime1 = _workDayTime1;
            row.WorkNightTime1 = _workNightTime1;         
            row.WorkoverType = uiVM.WorkoverType;
            row.QryDate = qryDateFormat;
          //  row.WorkClassType = uiVM.WorkClassType;
          //  row.WorkOverHours = uiVM.WorkOverHours;
            row.DepartmentText = uiVM.DepartmentText;
            row.ParentDataNodeText = uiVM.ParentDataNodeText;
            row.OpPerson = uiVM.OpPerson;
            row.WorkReason = uiVM.WorkReason;
            row.BackgroundIndex = null;
           $scope.tempVm.workOverCount += parseFloat(row.WorkOverHours);
        });
        dialog.close();
        //统计行数
        $scope.tempVm.workOverCount = 0;
        tempVm.tabCount = vmManager.dataSets.length;
        angular.forEach(vmManager.dataSets, function (row) {
            $scope.tempVm.workOverCount += parseFloat(row.WorkOverHours);
        })   
    },
        //白计算日期差值
        operate.calculateDayDates1 = function ()
        {
           uiVM.WorkDate = $scope.vm.WorkDate;
           vmManager.workDayDate = uiVM.WorkDate;   
           var dt01 = uiVM.WorkDate + " " + vmManager.workDayTimeStart.pattern("HH:mm");
           var dt02 = vmManager.workDayDate + " " + vmManager.workDayTimeEnd.pattern("HH:mm");     
           proEmployeeDataService.calculateDate(dt02, dt01).then(function (data)
           {                       
               $scope.vm.WorkOverHours = data;
               vmManager.workhoursDayCount = data;
           });                     
        }
        operate.calculateDayDates2 = function () {
           var workdayCount = vmManager.workhoursDayCount;
           uiVM.WorkDate = $scope.vm.WorkDate;
           vmManager.workDayDate1 = uiVM.WorkDate;
           var dt03 = uiVM.WorkDate + " " + vmManager.workDayTimeStart1.pattern("HH:mm");
           var dt04 = vmManager.workDayDate1 + " " + vmManager.workDayTimeEnd1.pattern("HH:mm");
           proEmployeeDataService.calculateDate(dt04, dt03).then(function (data) {
               $scope.vm.WorkOverHours = data + workdayCount ;
           });
         }
      //晚计算日期差值 
        operate.calculateNightDates1 = function () {      
            uiVM.WorkDate = $scope.vm.WorkDate;
            vmManager.workNightDate = uiVM.WorkDate;
            var nt01 = uiVM.WorkDate + " " + vmManager.workNightTimeStart.pattern("HH:mm");
            var nt02 = vmManager.workNightDate + " " + vmManager.workNightTimeEnd.pattern("HH:mm");       
            proEmployeeDataService.calculateDate(nt02, nt01).then(function (data) {
                $scope.vm.WorkOverHours = data;
                vmManager.workhoursNightCount = data;
               
            });

        }
        operate.calculateNightDates2 = function () {
           var worknightCount = vmManager.workhoursNightCount;
            uiVM.WorkDate = $scope.vm.WorkDate;
            vmManager.workNightDate1 = uiVM.WorkDate;
            var nt03 = uiVM.WorkDate + " " + vmManager.workNightTimeStart1.pattern("HH:mm");
            var nt04 = vmManager.workNightDate1+ " " + vmManager.workNightTimeEnd1.pattern("HH:mm");
          
            proEmployeeDataService.calculateDate(nt04, nt03).then(function (data) {
                $scope.vm.WorkOverHours = data + worknightCount;                         
            });                     
        }  
        //批量保存提示窗口
        operate.saveDialog = function () {
            if (vmManager.dataSets.length == 0) {
                leePopups.alert("亲，没有任何记录！");
                return;
            }
            leePopups.confirm("保存提示", "加班单编辑好了，您确定保存吗？", function () {

                operate.saveAll();
            });
        },
        operate.deleteDialog = function () {
            leePopups.confirm("删除提示", "是否确定删除吗？", function () {
                uiVM.OpSign = leeDataHandler.dataOpMode.delete;
                proEmployeeDataService.storeWorkOverHoursDt(uiVM).then(function (opresult) {
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
        proEmployeeDataService.storeHandlWorkOverHoursDt(vmManager.dataSets).then(function (opResult) {
            leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                vmManager.dataSets = [];
            });
        });
    };


    //焦点设置
    var focusSetter = {
        workeroverFocus: false,
        remarkFocus: false,
        departmentTextFocus: false,
        workerIdFocus: false,
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

   // vmManager.getDepartment();
    vmManager.bindingDepartments();



});

