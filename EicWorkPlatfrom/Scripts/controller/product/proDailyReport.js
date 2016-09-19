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
    //-------------------------生产日报录入--------------------------------------
    //获取日报输入模板
    reportDataOp.getDailyReportTemplate = function (department) {
        var url = urlPrefix + 'GetDailyReportTemplate';
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
        department: '生技部',
        productName: null,
        //编辑显示的数据集合
        editDatas: [],
        //编辑数据复制副本
        copyEditDatas: [],
        productNameFrom: null,
        productNameTo:null,
        delItem:null,
        flowOverviews:[],
        //获取产品工艺总览
        getProductFlowOverview: function () {
           
        },
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
        ReceiveHours: null,
        ManHours: null,
        ProductionEfficiency: null,
        OperationEfficiency: null,
        OpPerson: null,
        OpSign: null,
        OpDate: null,
        OpTime: null,
        Id_Key: null,
    }
    $scope.vm = uiVM;

    var initVM = _.clone(uiVM);

    var tablevm = {
        colVisible:true,//列的可见性
        orderIdColSpan: 3,
        proFlowColSpan:2,
        workerColSpan: 3,
        productColSpan: 4,
        workHoursColSpan:5
    };

    $scope.tbl = tablevm;

    var vmManager = {
        department: '生技部',
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
                tablevm.colVisible = true;
                tablevm.orderIdColSpan = 3;
                tablevm.proFlowColSpan = 2;
                tablevm.workerColSpan = 3;
                tablevm.productColSpan = 4;
                tablevm.workHoursColSpan = 5;
            }
            else {
                tablevm.colVisible = false;
                tablevm.orderIdColSpan = 1;
                tablevm.proFlowColSpan = 1;
                tablevm.workerColSpan = 2;
                tablevm.productColSpan = 2;
                tablevm.workHoursColSpan = 4;
            }
        },
        edittingRowIndex: 0,//编辑中的行索引
        addRow: function () {
            vmManager.edittingRowIndex = vmManager.editDatas.length > 0 ? vmManager.editDatas.length + 1 : 1;
            var vm = _.clone(initVM);
            vm.rowindex = vmManager.edittingRowIndex;
            vm.editting = true;
            vmManager.editDatas.push(vm);
        },
        showDReportInputView: function () {
            //vmManager.dReportInputDisplay = true;


            vmManager.addRow();
        },
        showDReportPreviewView: function () { vmManager.dReportPreviewDisplay = true; },
        showProFlowView: function () { vmManager.proFlowBoardDisplay = true; },
        showPersonView: function () { vmManager.personBoardDisplay = true; },
        showOrderIdView: function () { vmManager.orderIdBoardDisplay = true; },
        getReportInputDataTemplate: function () {

        },
        //工单数据信息
        orderDatas: [],
        //工艺流程集合
        productFlows:[],
        //绑定工单信息
        bindOrderInfo: function (orderDetails) {
            uiVM.OrderId = orderDetails.OrderId;
            uiVM.ProductName = orderDetails.ProductName;
            uiVM.ProductSpecification = orderDetails.ProductSpecify;
        },
        //获取工单信息
        getWorkOrderInfo: function ($event) {
            if ($event.keyCode === 13 || $event.keyCode === 40)
            {
                //var item = _.find(vmManager.orderDatas, { orderId: $scope.vm.OrderId });
                //if (!angular.isUndefined) {
                //    vmManager.bindOrderInfo(item.orderDetails);
                //    vmManager.productFlows = data.productFlows;
                //}
                //else {
                //    dReportDataOpService.getOrderDetails(vmManager.department, $scope.vm.OrderId).then(function (data) {
                //        if (angular.isObject(data)) {
                //            vmManager.orderDatas.push({ orderId: $scope.vm.OrderId, data: data });
                //            vmManager.bindOrderInfo(data.orderDetails);
                //            vmManager.productFlows = data.productFlows;
                //        }
                //    });
                //}
                
            }
            focusSetter.moveFocusTo($event, "orderIdFocus", 'workerIdFocus');
        },
        searchedWorkers: [],
        isSingle: true,//是否搜寻到的是单个人
        //获取作业人员信息
        getWorkerInfo: function ($event) {
            if ($event.keyCode === 13) {
                //if (uiVM.UserWorkerId === undefined) return;
                //var strLen = leeHelper.checkIsChineseValue(uiVM.UserWorkerId) ? 2 : 6;
                //if (uiVM.UserWorkerId.length >= strLen) {
                //    vmManager.searchedWorkers = [];
                //    $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(uiVM.UserWorkerId).then(function (datas) {
                //        if (datas.length > 0) {
                //            vmManager.searchedWorkers = datas;
                //            if (vmManager.searchedWorkers.length === 1) {
                //                vmManager.isSingle = true;
                //                vmManager.selectWorker(vmManager.searchedWorkers[0]);
                //            }
                //            else {
                //                vmManager.isSingle = false;
                //            }
                //        }
                //        else {
                //            vmManager.selectWorker(null);
                //        }
                //    });
                //}
            }
            focusSetter.moveFocusTo($event, 'orderIdFocus', 'productFlowFocus');
        },
        selectWorker: function (worker) {
            if (worker !== null) {
                uiVM.UserName = worker.Name;
                uiVM.UserWorkerId = worker.WorkerId;
                uiVM.Department = worker.Department;
            }
            else {
                uiVM.Department = null;
            }
        },
        //获取工序信息
        getProductFlowInfo: function ($event) {
            //focusSetter.moveFocusTo($event, 'saveCmdFocus');
            if ($event.keyCode === 37) { focusSetter['workerIdFocus'] = true; }
            if ($event.keyCode === 13) {
                var rowItem = _.find(vmManager.editDatas, { rowindex: vmManager.edittingRowIndex });
                if (!angular.isUndefined(rowItem)) {
                    leeHelper.copyVm(uiVM, rowItem);
                    uiVM = _.clone(initVM);
                    $scope.vm = uiVM;
                    rowItem.editting = false;
                    vmManager.addRow();
                    focusSetter['orderIdFocus'] = true;
                }
            };
        },
        editOver: function () {
            var rowItem = _.find(vmManager.editDatas, { rowindex: vmManager.edittingRowIndex });
            if (!angular.isUndefined(rowItem)) {
                leeHelper.copyVm(uiVM, rowItem);
                uiVM = _.clone(initVM);
                $scope.vm = uiVM;
                rowItem.editting = false;
            }
        },
        removeRow: function () {
            var rowItem = _.find(vmManager.editDatas, { rowindex: vmManager.edittingRowIndex });
            if (!angular.isUndefined(rowItem)) {
                leeHelper.remove(vmManager.editDatas, rowItem);
            }
        },
        editRow: function (item) {
            angular.forEach(vmManager.editDatas, function (edititem) { edititem.editting = false });
            leeHelper.copyVm(item, uiVM);
            $scope.vm = uiVM;
            vmManager.edittingRowIndex = item.rowindex;
            item.editting = true;
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
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        vmManager.department = dto.DataNodeText;
    };

    //$scope.promise = connDataOpService.getConfigDicData('Organization').then(function (data) {
    //    departmentTreeSet.setTreeDataset(data);
    //});

    $scope.ztree = departmentTreeSet;

    //焦点设置器
    var focusSetter = {
        orderIdFocus:false,
        workerIdFocus: false,
        productFlowFocus: false,
        saveCmdFocus:false,
        //移动焦点到指定对象
        moveFocusTo: function ($event, elPreName,elNextName) {
            if ($event.keyCode === 13 || $event.keyCode === 39) {
                focusSetter[elNextName] = true;
            }
            else if ($event.keyCode === 37) {
                focusSetter[elPreName] = true;
            };
        },
    };
    $scope.focus = focusSetter;

});