/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />

var productModule = angular.module('bpm.productApp');
productModule.factory('dReportDataOpService', function (ajaxService) {
    var urlPrefix = "/" + leeHelper.controllers.dailyReport + "/";
    var reportDataOp = {};
    //-------------------------标准工时设置-------------------------------------
    //获取产品工艺流程列表
    reportDataOp.getProductFlowList = function (department, productName, orderId, searchMode) {
        var url = urlPrefix + 'GetProductFlowList';
        return ajaxService.getData(url, {
            department: department,
            productName: productName,
            orderId: orderId,
            searchMode: searchMode,
        });
    };
    //保存产品工艺流程数据
    reportDataOp.storeProductFlowDatas = function (entities) {
        var url = urlPrefix + 'StoreProductFlowDatas';
        return ajaxService.postData(url, {
            entities: entities,
        });
    };
    //导入模板数据
    reportDataOp.importProductFlowTemplateFile = function (file) {
        var url = urlPrefix + 'ImportProductFlowDatas';
        return ajaxService.uploadFile(url,file);
    };
    //获取产品工艺流程总览
    reportDataOp.getProductFlowOverview = function (department) {
        var url = urlPrefix + 'GetProductFlowOverview';
        return ajaxService.getData(url, {
            department: department,
        });
    };
    //获取产品工艺流程配置数据
    reportDataOp.getProductFlowInitData = function (department) {
        var url = urlPrefix + 'GetProductFlowInitData';
        return ajaxService.getData(url, {
            department: department,
        });
    };
    //获取工单详细信息
    reportDataOp.getOrderDetails = function (department, orderId) {
        var url = urlPrefix + 'GetOrderDetails';
        return ajaxService.getData(url, {
            department: department,
            orderId: orderId,
        });
    };
    //-------------------------生产日报录入--------------------------------------
    //获取日报输入模板
    reportDataOp.getDailyReportTemplate = function (department,dailyReportDate) {
        var url = urlPrefix + 'GetDailyReportTemplate';
        return ajaxService.getData(url, {
            department: department,
            dailyReportDate:dailyReportDate
        });
    };
    //获取日报录入初始化数据
    reportDataOp.getDReportInitData = function (department) {
        var url = urlPrefix + 'GetDReportInitData';
        return ajaxService.getData(url, {
            department: department,
        });
    };
    //保存日报录入数据
    reportDataOp.saveDailyReportDatas = function (datas,inputDate) {
        var url = urlPrefix + 'SaveDailyReportDatas';
        return ajaxService.postData(url, {
            datas: datas,
            inputDate:inputDate
        });
    };
    //审核日报数据
    reportDataOp.auditDailyReport = function (department, dailyReportDate) {
        var url = urlPrefix + 'AuditDailyReport';
        return ajaxService.postData(url, {
            department: department,
            dailyReportDate: dailyReportDate
        });
    };
    return reportDataOp;
});
//标准工时设定
productModule.controller("dReportHoursSetCtrl", function ($scope, dReportDataOpService, dataDicConfigTreeSet, connDataOpService, $modal) {
    ///工艺标准工时视图模型
    var uiVM = {
        Department: null,
        ProductName: null,
        ProductFlowSign: 0,
        ProductFlowId: null,
        ProductFlowName: null,
        StandardHoursType: 0,
        StandardHours: null,
        RelaxCoefficient: 1,
        MinMachineCount: 0,
        MaxMachineCount: 0,
        DifficultyCoefficient: null,
        MouldId: null,
        MouldName: null,
        MouldCavityCount: 0,
        Remark: null,
        OpPerson: '章亚娅',
        OpSign: 'add',
        Id_Key: null,
    }
    $scope.vm = uiVM;
    //初始化视图
    var initVM = _.clone(uiVM);

    var vmManager = {
        init: function () {
            uiVM = _.clone(initVM);
            $scope.vm = uiVM;
        },
        opSign:null,
        editWindowDisplay: false,
        editWindowWidth: '100%',
        copyWindowDisplay: false,
        department: '成型课',
        productName: null,
        //编辑显示的数据集合
        editDatas: [],
        //编辑数据复制副本
        copyEditDatas: [],
        productNameFrom: null,
        productNameTo:null,
        delItem:null,
        flowOverviews:[],
        //查看工艺流程明细
        viewProductFlowDetails: function (item) {
            vmManager.productName = item.ProductName;
            $scope.searchPromise = dReportDataOpService.getProductFlowList(vmManager.department,vmManager.productName,"",2).then(function (datas) {
                vmManager.editDatas = datas;
            });
        },
        //获取产品工艺流程列表
        getProductFlowList: function ($event) {
            if ($event.keyCode === 13) {
               $scope.searchPromise=dReportDataOpService.getProductFlowList(vmManager.department, vmManager.productName,"",2).then(function (datas) {
                    vmManager.editDatas = datas;
                });
            }
        },
        delModalWindow: $modal({
            title: "删除提示", content: "您确定要删除此工序信息吗?",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl, show: false,
            controller: function ($scope) {
                $scope.confirmDelete = function () {
                    leeHelper.remove(vmManager.editDatas, vmManager.delItem);
                    if (vmManager.editDatas.length === 0)
                    {
                        var flowItem = _.find(vmManager.flowOverviews, { ProductName: vmManager.productName });
                        if (flowItem !== undefined)
                        {
                            leeHelper.remove(vmManager.flowOverviews, flowItem);
                        }
                    }
                    vmManager.delModalWindow.$promise.then(vmManager.delModalWindow.hide);
                };
            },
        }),
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.add = function () {
        vmManager.opSign = 'add';
        vmManager.init();
        uiVM.ProductName = vmManager.productName;
        vmManager.editWindowDisplay = true;
    };
    operate.copyAll = function () {
        vmManager.productNameFrom = vmManager.productName;
        vmManager.copyWindowDisplay = true;
    };
    operate.copyConfirm = function () {
        vmManager.productName = vmManager.productNameTo;
        angular.forEach(vmManager.editDatas, function (item) {
            item.ProductName = vmManager.productNameTo;
            vmManager.copyEditDatas.push(item);
        });
    };
    operate.copyok = function () {
        vmManager.editDatas = vmManager.copyEditDatas;
        vmManager.copyWindowDisplay = false;
    };
    operate.copycancel = function () {
        vmManager.copyWindowDisplay = false;
    };
    operate.editItem = function (item) {
        vmManager.opSign = 'edit';
        uiVM = item;
        $scope.vm = uiVM;
        vmManager.editWindowDisplay = true;
    };
    operate.deleteItem = function (item) {
        vmManager.delItem = item;
        vmManager.delModalWindow.$promise.then(vmManager.delModalWindow.show);
    },
    //保存数据
    operate.save = function (isValid) {
        //leeHelper.setUserData($scope.uiVM);
        $scope.vm.Department = vmManager.department;

        if (vmManager.opSign === 'add') {
            leeDataHandler.dataOperate.add(operate, isValid, function () {
                vmManager.editDatas.push($scope.vm);
            })
        }
        else {
            vmManager.editWindowDisplay = false;
        }
    };
    //批量保存数据
    operate.saveAll = function () {
        $scope.searchPromise = dReportDataOpService.storeProductFlowDatas(vmManager.editDatas).then(function (opresult) {
            if (opresult.Result) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult);
                vmManager.editDatas = [];
            }
        });
    };
    //取消编辑
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
            vmManager.editWindowDisplay = false;
        });
    };
   
    ///选择文件并导入数据
    $scope.selectFile = function (el) {
        var files = el.files;
        if (files.length > 0) {
            var file = files[0];
            var fd = new FormData();
            fd.append('file', file);
            dReportDataOpService.importProductFlowTemplateFile(fd).then(function (datas) {
                vmManager.editDatas = datas;
            });
        }
    };
    
    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        vmManager.department = dto.DataNodeText;
    };
    $scope.promise =dReportDataOpService.getProductFlowInitData(vmManager.department).then(function (data) {
        departmentTreeSet.setTreeDataset(data.departments);
        vmManager.flowOverviews = data.overviews;
    });

    $scope.ztree = departmentTreeSet;
});
//日报录入
productModule.controller("dReportInputCtrl", function ($scope, dataDicConfigTreeSet, connDataOpService, dReportDataOpService,$modal) {
    ///日报录入视图模型
    var uiVM = {
        Department: null,
        DailyReportDate: null,
        DailyReportMonth: null,
        InputTime: null,
        MachineId: null,
        EquipmentEifficiency: null,
        DifficultyCoefficient: null,
        MouldId: null,
        MouldName: null,
        MouldCavityCount: 0,
        OrderId: null,
        ProductName: null,
        ProductSpecification: null,
        ProductFlowSign: 0,
        ProductFlowID: null,
        ProductFlowName: null,
        StandardHoursType: 0,
        StandardHours: null,
        ClassType: null,
        UserWorkerId: null,
        UserName: null,
        MasterWorkerId: null,
        MasterName: null,
        InputStoreCount: null,
        Qty: null,
        QtyGood: null,
        QtyBad: null,
        FailureRate: null,
        SetHours: null,
        InputHours: null,
        ProductionHours: null,
        AttendanceHours: null,
        NonProductionHours: null,
        NonProductionReasonCode: null,
        NonProductionReason: null,
        ReceiveHours: null,
        ManHours: null,
        ProductionEfficiency: null,
        OperationEfficiency: null,
        Remarks:null,
        OpPerson: null,
        OpSign: null,
        OpDate: null,
        OpTime: null,
        Id_Key: null,
    }
    $scope.vm = uiVM;

    var initVM = _.clone(uiVM);

    var tablevm = {
        //跨列数字集合
        colSpans: [3, 2, 3, 3, 6],
        colVisible: true,//列的可见性
    };
    var tableSet = _.clone(tablevm);
    $scope.tbl = tablevm;



    var vmManager = {
        department: '成型课',
        classTypes: [{id:'B', text:'晚班'}, {id:'A',text:"白班"}],
        //该部门的机台列表
        machines: [],
        unproductReasons:[],
        InputDate:new Date(),
        dReportInputDisplay: false,
        dReportPreviewDisplay: false,
        proFlowBoardDisplay: false,
        personBoardDisplay: false,
        orderIdBoardDisplay: false,
        boardViewSize: '100%',
        inputViewSize: '70%',
        inputMode:'',
        inputModes: [{ id: 'simple', text: '普通编辑' }, { id: 'fast', text: '快速编辑' }],
        selectInputMode: function () {
            if (vmManager.inputMode === 'simple') {
                tablevm = _.clone(tableSet);
                $scope.tbl = tablevm;
            }
            else {
                tablevm.colSpans = [2, 1, 3, 2, 6];
                tablevm.colVisible = false;
            }
        },
        edittingRowIndex: 0,//编辑中的行索引
        edittingRow:null,
        addRow: function () {
            vmManager.edittingRowIndex = vmManager.editDatas.length > 0 ? vmManager.editDatas.length + 1 : 1;
            var vm = _.clone(initVM);
            vm.DailyReportDate = vmManager.InputDate;
            vm.rowindex = vmManager.edittingRowIndex;
            vm.editting = true;
            vm.isMachineMode = false;
            vmManager.edittingRow = vm;
            vmManager.editDatas.push(vm);
        },
        showDReportInputView: function () {
            //vmManager.dReportInputDisplay = true;


            vmManager.addRow();
        },
        showDReportPreviewView: function () { vmManager.dReportPreviewDisplay = true; },
        showProFlowView: function () { vmManager.proFlowBoardDisplay = true; },
        showPersonView: function () {
            vmManager.workerAttendanceSumerizeHours = [];
            vmManager.workerAttendanceHoursDetails = [];
            vmManager.sumerizeWorkerAttendanceHours(vmManager.editDatas);
            vmManager.personBoardDisplay = true;
        },
        showOrderIdView: function () { vmManager.orderIdBoardDisplay = true; },
        getReportInputDataTemplate: function () {
            vmManager.editDatas = [];
            $scope.promise = dReportDataOpService.getDailyReportTemplate(vmManager.department,vmManager.InputDate).then(function (datas) {
                if (angular.isArray(datas) && datas.length > 0) {
                    var rowindex = 1;
                    angular.forEach(datas, function (item) {
                        item.editting = false;
                        item.rowindex = rowindex;
                        //判断是否为机台
                        if (item.MachineId) {
                            item.isMachineMode = true;
                        } else {
                            item.isMachineMode = false;
                        }
                        item.pheditting = false;
                        vmManager.editDatas.push(item);
                        rowindex += 1;
                    });
                }
                else {
                    vmManager.editDatas = [];
                }
            });
            
        },
        //工单数据信息
        orderDatas: [],
        //工艺流程集合
        productFlows: [],
        //选择工艺流程
        selectProductFlow: function (flow) {
            uiVM.ProductFlowID = flow.ProductFlowId;
            vmManager.edittingRow.ProductFlowID = flow.ProductFlowId;
            vmManager.edittingRow.ProductFlowName = flow.ProductFlowName;
            vmManager.edittingRow.ProductFlowSign = flow.ProductFlowSign;
            vmManager.edittingRow.StandardHours = flow.StandardHours;
            vmManager.edittingRow.StandardHoursType = flow.StandardHoursType;
            $scope.vm = uiVM;
        },
        //绑定工单信息
        bindOrderInfo: function (orderDetails) {
            vmManager.edittingRow.OrderId = orderDetails.OrderId;
            vmManager.edittingRow.ProductName = orderDetails.ProductName;
            vmManager.edittingRow.ProductSpecification = orderDetails.ProductSpecify;
        },
        getProductFlows: function (orderId) {
            vmManager.productFlows = [];
            var item = _.find(vmManager.orderDatas, { orderId: $scope.vm.OrderId });
            if (!angular.isUndefined(item)) {
                vmManager.productFlows = item.data.productFlows;
            }
        },
        //获取工单信息
        getWorkOrderInfo: function ($event) {
            if ($event.keyCode === 13 || $event.keyCode === 40 || $event.keyCode===9)
            {
                if ($scope.vm.OrderId === undefined || $scope.vm.OrderId.length <= 10) return;

                var item = _.find(vmManager.orderDatas, { orderId: $scope.vm.OrderId });
                if (!angular.isUndefined(item)) {
                    vmManager.bindOrderInfo(item.data.orderDetails);
                    vmManager.productFlows = item.data.productFlows;
                }
                else {
                    $scope.searchPromise= dReportDataOpService.getOrderDetails(vmManager.department, $scope.vm.OrderId).then(function (data) {
                        if (angular.isObject(data)) {
                            vmManager.orderDatas.push({ orderId: $scope.vm.OrderId, data: data });
                            vmManager.bindOrderInfo(data.orderDetails);
                            vmManager.productFlows = data.productFlows;
                        }
                    });
                }
            }
            focusSetter.moveFocusTo($event, "orderIdFocus", 'productFlowFocus');
        },
        searchedWorkers: [],
        isSingle: true,//是否搜寻到的是单个人
        //获取作业人员信息
        getWorkerInfo: function ($event, workerType) {
            if ($event.keyCode === 37) {
                if (workerType === 'worker') {
                    focusSetter['productFlowFocus'] = true;
                    return;
                }
                else {
                    focusSetter['workerIdFocus'] = true;
                    return;
                }
            }
            if ($event.keyCode === 13 || $event.keyCode === 9) {
                var workerId = null;
                if (workerType === 'worker') {
                    workerId = uiVM.UserWorkerId;
                    if (uiVM.UserWorkerId === undefined) return;
                }
                else {
                    workerId = uiVM.MasterWorkerId;
                    if (uiVM.MasterWorkerId === undefined) return;
                }
                var strLen = leeHelper.checkIsChineseValue(workerId) ? 2 : 6;
                if (workerId.length >= strLen) {
                    vmManager.searchedWorkers = [];
                    $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(workerId).then(function (datas) {
                        if (datas.length > 0) {
                            vmManager.searchedWorkers = datas;
                            if (vmManager.searchedWorkers.length === 1) {
                                vmManager.isSingle = true;
                                vmManager.selectWorker(vmManager.searchedWorkers[0],workerType);
                            }
                            else {
                                vmManager.isSingle = false;
                            }
                        }
                        else {
                            vmManager.selectWorker(null,workerType);
                        }
                        if (workerType === 'worker') {
                            //处理焦点移动
                            if (vmManager.edittingRow.isMachineMode) {
                                focusSetter.moveFocusTo($event, 'productFlowFocus', 'masterWorkerIdFocus');
                            }
                            else {
                                vmManager.addNewRow($event);
                            }
                        }
                        else {
                            vmManager.addNewRow($event);
                        }
                        vmManager.searchedWorkers = [];
                    });
                }
            }
        },
        //添加备注信息
        addRemarks: function (item) {
            vmManager.getCurrentRow(item);
            vmManager.editRemarksModal.$promise.then(vmManager.editRemarksModal.show);
        },
        editRemarksModal: $modal({
            title: '添加备注信息',
            content: '',
            templateUrl:leeHelper.controllers.dailyReport+"/EditRemarkViewTpl/",
            controller: function ($scope) {
                $scope.vm = {
                    Remarks:null,
                };
                var op = Object.create(leeDataHandler.operateStatus);
                $scope.save = function (isValid) {
                    leeDataHandler.dataOperate.add(op, isValid, function () {
                        vmManager.edittingRow.Remarks = $scope.vm.Remarks;
                        uiVM.Remarks = vmManager.edittingRow.Remarks;
                        vmManager.editRemarksModal.$promise.then(vmManager.editRemarksModal.hide);
                    });
                };
            },
            show:false,
        }),
        //编辑班别
        editClassType: function (item) {
            item.isEdittingClassType = true;
            vmManager.getCurrentRow(item);
            $scope.vm.ClassType = item.ClassType;
        },
        //选择班别
        selectClassType: function () {
            vmManager.edittingRow.ClassType = $scope.vm.ClassType;
            vmManager.edittingRow.isEdittingClassType = false;
        },
        //获取正在编辑的行
        getEdittingRow: function () {
            var rowItem = _.find(vmManager.editDatas, { rowindex: vmManager.edittingRowIndex });
            return rowItem;
        },
        //添加新的行
        addNewRow: function ($event) {
            if ($event.keyCode === 13) {
                uiVM = _.clone(initVM);
                $scope.vm = uiVM;
                vmManager.edittingRow.editting = false;
                vmManager.addRow();
                focusSetter['orderIdFocus'] = true;
            };
        },
        selectWorker: function (worker,workerType) {
            if (worker !== null) {
                if (workerType === 'worker') {
                    vmManager.edittingRow.UserName = worker.Name;
                    vmManager.edittingRow.UserWorkerId = worker.WorkerId;
                    vmManager.edittingRow.ClassType = worker.ClassType;
                }
                else {
                    vmManager.edittingRow.MasterName = worker.Name;
                    vmManager.edittingRow.MasterWorkerId = worker.WorkerId;
                }
                vmManager.edittingRow.Department = vmManager.department;
            }
            else {
                vmManager.edittingRow.Department = null;
            }

            leeHelper.copyVm(vmManager.edittingRow, uiVM);
            $scope.vm = uiVM;
        },
        //是否是机台名称
        isInputMachineName: function () {
            var machineItem = _.find(vmManager.machines, { MachineId: $scope.vm.ProductFlowID });
            return machineItem !== undefined;
        },
        //获取工序信息
        getProductFlowInfo: function ($event) {
            if ($event.keyCode === 13) {
                if (vmManager.productFlows.length > 0) {
                    var flowItem = null;
                    if (vmManager.isInputMachineName()) {
                        vmManager.edittingRow.isMachineMode = true;
                        vmManager.edittingRow.MachineId = $scope.vm.ProductFlowID;
                        flowItem = vmManager.productFlows[0];
                    }
                    else {
                        flowItem = _.find(vmManager.productFlows, { ProductFlowId: $scope.vm.ProductFlowID });
                        vmManager.edittingRow.isMachineMode = false;
                    }
                    if (!angular.isUndefined(flowItem)) {
                        vmManager.selectProductFlow(flowItem);
                    }
                }
            }
            focusSetter.moveFocusTo($event, 'orderIdFocus', 'workerIdFocus');
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
        //删除行
        removeRow: function (item) {
            leeHelper.remove(vmManager.editDatas, item);
        },
        //复制并粘贴行
        copyAndPaste: function (item) {
            item.editting = false;
            var vm = _.clone(item);
            vm.rowindex = item.rowindex + 1;
            vmManager.edittingRow = vm;
            vmManager.editDatas.push(vm);
        },
        //取消编辑
        cancelEdit: function (item) {
            item.pheditting = false;
        },
        //编辑行
        editRow: function (item) {
            angular.forEach(vmManager.editDatas, function (edititem) { edititem.editting = false });
            leeHelper.copyVm(item, uiVM);
            $scope.vm = uiVM;
            vmManager.edittingRowIndex = item.rowindex;
            vmManager.edittingRow = item;
            vmManager.getProductFlows(item.OrderId);
            item.editting = true;
        },
        //设定行
        getCurrentRow: function (item) {
            vmManager.edittingRowIndex = item.rowindex;
            vmManager.edittingRow = item;
        },
        //待编辑的记录集合
        editDatas:[],
        //新增记录
        addRecord: function (isValid) {
            leeDataHandler.dataOperate.add(operate, isValid, function () {
                var vm = _.clone($scope.vm);
                vmManager.editDatas.push(vm);

                uiVM = _.clone(initVM);
                $scope.vm = uiVM;

                focusSetter['orderIdFocus'] = true;
            })
        },
        //编辑产量工时信息
        editProductHoursRow: function (item) {
            if (item !== undefined && item !== null) {
                angular.forEach(vmManager.editDatas, function (edititem) { edititem.pheditting = false });
                item.SetHours = 12;
                item.InputHours = 12;
                leeHelper.copyVm(item, uiVM);
                $scope.vm = uiVM;
                vmManager.edittingRowIndex = item.rowindex;
                vmManager.edittingRow = item;
                vmManager.getProductFlows(item.OrderId);
                item.pheditting = true;
                focusSetter['qtyFocus'] = true;
            }
        },
        //编辑下一行产量工时信息
        editNextProductHoursRow: function ($event,item) {
            if ($event.keyCode === 13 || $event.keyCode === 9) {
                leeHelper.copyVm($scope.vm, vmManager.edittingRow);
                if (item.rowindex < vmManager.editDatas.length) {
                    vmManager.edittingRowIndex = item.rowindex + 1;
                    var rowItem = vmManager.getEdittingRow();
                    vmManager.editProductHoursRow(rowItem);
                }
                else {
                    vmManager.edittingRow.pheditting = false;
                }
            }
        },
        inputQty: function ($event, item) {
            focusSetter.doWhenKeyDown($event, function () {
                item.Qty = $scope.vm.Qty;
            });
          
            focusSetter.moveFocusTo($event, 'qtyFocus', 'qtyBadFocus');
        },
        inputQtyBad: function ($event, item) {
            focusSetter.doWhenKeyDown($event, function () {
                item.QtyBad = $scope.vm.QtyBad;
                //良品数=总产量-不良品数
                item.QtyGood = item.Qty - item.QtyBad;
                $scope.vm.QtyGood = item.QtyGood;
            });
         

            focusSetter.moveFocusTo($event, 'qtyFocus', 'setHoursFocus');
        },
        inputSetHours: function ($event, item) {
            focusSetter.doWhenKeyDown($event, function () {
                item.SetHours = $scope.vm.SetHours;
            });

            focusSetter.moveFocusTo($event, 'qtyBadFocus', 'inputHoursFocus');
        },
        inputHours: function ($event,item) {
            focusSetter.doWhenKeyDown($event, function () {
                item.InputHours = $scope.vm.InputHours;
            });
           
            focusSetter.moveFocusTo($event, 'setHoursFocus', 'attendanceHoursFoucs');
        },
        inputAttendanceHours: function ($event,item) {
            focusSetter.doWhenKeyDown($event, function () {
                item.AttendanceHours = $scope.vm.AttendanceHours;
            });
            
            focusSetter.moveFocusTo($event, 'inputHoursFoucs', 'nonProductHoursFocus');
        },
        inputNonProductionHours: function ($event, item) {
            if ($event.keyCode === 37) {
                focusSetter['AttendanceHours'] = true;
                return;
            }
            focusSetter.doWhenKeyDown($event, function () {
                item.NonProductionHours = $scope.vm.NonProductionHours;
                item.ProductionHours = item.SetHours - item.NonProductionHours;
                $scope.vm.ProductionHours = item.ProductionHours;
                if ($scope.vm.NonProductionHours > 0) {
                    item.isHadNonProductionHours = true;
                    focusSetter['nonProductReasonCodeFocus'] = true;
                    vmManager.caculateEfficient(item);
                }
                else {
                    item.isHadNonProductionHours = false;
                    vmManager.caculateEfficient(item);
                    vmManager.editNextProductHoursRow($event, item);
                }
            });
        },
        inputNonProductionReasonCode: function ($event, item) {
            if ($event.keyCode === 37) {
                focusSetter['AttendanceHours'] = true;
                return;
            }
            if ($event.keyCode === 13 || $event.keyCode === 9) {
                item.NonProductionReasonCode = $scope.vm.NonProductionReasonCode;
                var itemcode = _.find(vmManager.unproductReasons, { NonProductionReasonCode: item.NonProductionReasonCode });
                if (itemcode !== undefined) {
                    item.NonProductionReason = itemcode.NonProductionReason;
                }
                else {
                    item.NonProductionReason = '待添加--';
                }
                $scope.vm.NonProductionReason = item.NonProductionReason;
            }
            vmManager.editNextProductHoursRow($event, item);
        },
        //计算效率
        caculateEfficient: function (item) {
            //稼动率=生产工时/设置工时*100%
            if (parseInt(item.SetHours) !== 0)
                item.EquipmentEifficiency = leeHelper.toPercent(parseFloat(item.ProductionHours) /parseFloat(item.SetHours), 0);
            //得到工时=生产数量/标准工时*100%
            if (parseInt(item.StandardHours) !== 0)
                var getProductHours =parseFloat(item.Qty) /parseFloat(item.StandardHours);
            //作业效率=得到工时/生产工时*100%
            if (parseInt(item.ProductionHours) !== 0)
                item.OperationEfficiency = leeHelper.toPercent(getProductHours /parseFloat(item.ProductionHours), 1);
            //生产效率=得到工时/投入时数*100%
            if (parseInt(item.InputHours) !== 0)
                item.ProductionEfficiency = leeHelper.toPercent(getProductHours /parseFloat(item.InputHours), 1);
            //不良率=不良品数量/总产量*100%
            if (parseInt(item.Qty) !== 0)
                item.FailureRate = leeHelper.toPercent(parseFloat(item.QtyBad) /parseFloat(item.Qty), 2);


            $scope.vm.EquipmentEifficiency = item.EquipmentEifficiency;
            $scope.vm.FailureRate = item.FailureRate;
            $scope.vm.OperationEfficiency = item.OperationEfficiency;
            $scope.vm.ProductionEfficiency = item.ProductionEfficiency;

        },
        //员工出勤汇总工时数据
        workerAttendanceSumerizeHours: [],
        //员工出勤汇总工时数据明细
        workerAttendanceHoursDetails:[],
        //汇总员工出勤工时
        sumerizeWorkerAttendanceHours: function (dReportDatas) {
            vmManager.workerAttendanceSumerizeHours = [];
            if (!angular.isArray(dReportDatas) || dReportDatas.length === 0) return;
            angular.forEach(dReportDatas, function (rowItem) {
                var wah = _.find(vmManager.workerAttendanceSumerizeHours, { UserWorkerId: rowItem.UserWorkerId });
                if (angular.isUndefined(wah)) {
                    vmManager.workerAttendanceSumerizeHours.push({
                        UserWorkerId: rowItem.UserWorkerId,
                        UserName: rowItem.UserName,
                        AttendanceHours: parseFloat(rowItem.AttendanceHours),
                        isAlert:parseFloat(rowItem.AttendanceHours)>11
                    });
                }
                else {
                    wah.AttendanceHours +=parseFloat(rowItem.AttendanceHours);
                }
            })
        },
        //查看作业员的工时记录明细
        viewAttendanceHoursDetails: function (item) {
            vmManager.edittingAttendanceHoursRow = item;
            vmManager.workerAttendanceHoursDetails = [];
            vmManager.workerAttendanceHoursDetails = _.where(vmManager.editDatas, { UserWorkerId: item.UserWorkerId });
        },
        //要编辑的出勤工时
        editAttendanceHours: 0,
        edittingAttendanceHoursRow:null,
        //进入编辑出勤工时模式
        enterEditAttendanceHoursMode: function (item) {
            vmManager.editAttendanceHours = item.AttendanceHours;
            item.edittingAttendanceHours = true;
        },
        //退出编辑出勤工时模式
        exitEditAttendanceHoursMode: function ($event, item) {
            focusSetter.doWhenKeyDown($event, function () {
                item.AttendanceHours = vmManager.editAttendanceHours;
                item.edittingAttendanceHours = false;
                //同步更新日报中的出勤工时数据
                var workerItem = _.find(vmManager.editDatas, { rowindex: item.rowindex });
                if (!angular.isUndefined(workerItem)) {
                    workerItem.AttendanceHours = vmManager.editAttendanceHours;
                }
                //同步更新左侧汇总表中的出勤工时数据
                var workerAttendanceHoursDetails = _.where(vmManager.editDatas, { UserWorkerId: item.UserWorkerId });
                var sumAttendanceHours = 0;
                if (workerAttendanceHoursDetails.length > 0) {
                    angular.forEach(workerAttendanceHoursDetails, function (data) {
                        sumAttendanceHours += parseFloat(data.AttendanceHours);
                    });
                    vmManager.edittingAttendanceHoursRow.AttendanceHours = sumAttendanceHours;
                    vmManager.edittingAttendanceHoursRow.isAlert = sumAttendanceHours > 11;
                }
            });
        },
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //保存日报录入数据
    operate.save = function () {
        if (vmManager.editDatas.length > 0)
        {
            $scope.promise = dReportDataOpService.saveDailyReportDatas(vmManager.editDatas,vmManager.InputDate).then(function (opresult) {
                if (opresult.Result) {
                    leeDataHandler.dataOperate.handleSuccessResult(operate, opresult);
                    vmManager.editDatas = []; 
                }
            });
        }
    };
    //审核确认日报录入数据
    operate.audit = function () {
        $scope.promise = dReportDataOpService.auditDailyReport(vmManager.department,vmManager.InputDate).then(function (opresult) {
            if (opresult.Result) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult);
                vmManager.editDatas = [];
            }
        });
    };

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        vmManager.department = dto.DataNodeText;
    };

    $scope.promise = dReportDataOpService.getDReportInitData(vmManager.department).then(function (datas) {
        departmentTreeSet.setTreeDataset(datas.departments);
        vmManager.machines = datas.machines;
        vmManager.unproductReasons = datas.unproductReasons;
    });

    $scope.ztree = departmentTreeSet;

    //焦点设置器
    var focusSetter = {
        orderIdFocus:false,
        workerIdFocus: false,
        masterWorkerIdFocus:false,
        productFlowFocus: false,
        qtyFocus: false,
        qtyBadFocus: false,
        setHoursFocus:false,
        inputHoursFocus: false,
        productHoursFoucs: false,
        attendanceHoursFoucs: false,
        nonProductHoursFocus: false,
        nonProductReasonCodeFocus: false,
        //移动焦点到指定对象
        moveFocusTo: function ($event, elPreName,elNextName) {
            if ($event.keyCode === 13 || $event.keyCode === 39 || $event.keyCode === 9) {
                focusSetter[elNextName] = true;
            }
            else if ($event.keyCode === 37) {
                focusSetter[elPreName] = true;
            };
        },
        doWhenKeyDown: function ($event, fn) {
            if ($event.keyCode === 13 || $event.keyCode === 39 || $event.keyCode === 9) { fn(); }
        }
    };
    $scope.focus = focusSetter;

});