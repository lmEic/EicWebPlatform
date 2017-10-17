﻿/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />

var productModule = angular.module('bpm.productApp');
productModule.factory('dReportDataOpService', function (ajaxService) {
    var urlPrefix = "/" + leeHelper.controllers.dailyReport + "/";
    var reportDataOp = {};
    //-------------------------标准工时设置-------------------------------------//
    //获取产品工艺流程列表
    reportDataOp.getProductionFlowList = function (department, productName, orderId, searchMode) {
        var url = urlPrefix + 'GetProductionFlowList';
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
    ///得到标准生产工艺流程总览（New）
    reportDataOp.getProductionFlowOverview = function (department, productName, searchMode) {
        var url = urlPrefix + 'GetProductionFlowOverview';
        return ajaxService.getData(url, {
            department: department,
            productName: productName,
            searchMode: searchMode,
        });
    };
    //  导入Excel
    reportDataOp.importProductFlowTemplateFile = function (file) {
        var url = urlPrefix + 'ImportProductFlowDatas';
        return ajaxService.uploadFile(url, file);
    };
    // 直接删除 产品工艺
    reportDataOp.immediatelyDeleteProcessesFlow = function (productName, processesName) {
        var url = urlPrefix + 'ImmediatelyDeleteProcessesFlow';
        return ajaxService.postData(url, {
            productName: productName,
            processesName: processesName,
        });
    };
    //--------------------------/载入ERP中   用于确认今天生产的订单--------------------------------//
    reportDataOp.getInProductionOrderDatas = function (department) {
        var url = urlPrefix + 'GetInProductionOrderDatas';
        return ajaxService.getData(url, {
            department: department,
        });
    }
    ///载入ERP中   用于确认今天生产的订单
    reportDataOp.getOrderDispatchInfoDatas = function (department, nowDate) {
        var url = urlPrefix + 'GetOrderDispatchInfoDatas';
        return ajaxService.getData(url, {
            department: department,
            nowDate: nowDate,
        });
    }
    /// 保存分配数据
    reportDataOp.saveOrderDispatch = function (entity) {
        var url = urlPrefix + 'StoreOrderDispatchDatas';
        return ajaxService.postData(url, {
            entity: entity,
        });
    }


    //------------------------日报录入 数据保存 获取产品工艺流程列表----------------------------------//
    reportDataOp.getProductionFlowCountDatas = function (department, productName, orderId) {
        var url = urlPrefix + 'GetProductionFlowCountDatas';
        return ajaxService.getData(url, {
            department: department,
            productName: productName,
            orderId: orderId,
        });
    };
    ////得到此工号最后一次录入信息
    reportDataOp.getWorkerDailyLastInfoDatas = function (workerId) {
        var url = urlPrefix + 'GetWorkerDailyInfoBy';
        return ajaxService.getData(url, {
            workerId: workerId,
        });
    };
    ///此工单,站别，日期下  所有职员录入的信息
    reportDataOp.getProcessesNameDailyInfoDatas = function (date, orderId, processesName) {
        var url = urlPrefix + 'getProcessesNameDailyDataBy';
        return ajaxService.getData(url, {
            processesName: processesName,
            orderId: orderId,
            date: date,
        });
    };

    reportDataOp.saveDailyReportData = function (entity) {
        var url = urlPrefix + 'SaveDailyReportData';
        return ajaxService.postData(url, {
            entity: entity,
        });
    }
    //----------------------------------------------------------//
    return reportDataOp;
});

//生产标准艺流程设定
productModule.controller("standardProductionFlowSetCtrl", function ($scope, dReportDataOpService, dataDicConfigTreeSet, connDataOpService, $modal) {
    ///工艺标准工时视图模型
    var uiVM = {
        Department: null,
        ProductId: null,
        ProductName: null,
        ProcessesIndex: 0,
        ProcessesSign: null,
        InputType: null,
        IsValid: null,
        IsVisualization: null,
        ProcessesName: null,
        ProductCoefficient: 1,
        ProcessesType: '人工',
        StandardProductionTimeType: 'UPS',
        StandardProductionTime: null,
        UPH: null,
        UPS: null,
        MachinePersonRatio: null,
        MouldId: null,
        MouldName: null,
        MouldHoleCount: 0,
        Remark: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        OpDate: null,
        OpTime: null,
        Id_Key: null,
        Id: null,
        IsServer: false,//是否是从服务器端获取的数据标志
    };
    $scope.vm = uiVM;
    //初始化视图
    var initVM = _.clone(uiVM);
    var vmManager = {
        init: function () {
            uiVM = _.clone(initVM);

            $scope.vm = uiVM;
        },
        saveOneinit: function () {
            uiVM.ProcessesName = null;
            uiVM.StandardProductionTime = null;
            uiVM.ProcessesIndex = uiVM.ProcessesIndex + 1;
            $scope.vm = uiVM;
        },
        isShowMachine: false,
        opSign: null,
        editWindowDisplay: false,
        editWindowWidth: '100%',
        copyWindowDisplay: false,
        department: leeLoginUser.department,
        productName: null,
        //编辑显示的数据集合
        editDataSet: [],
        editDatasSource: [],
        //编辑数据复制副本
        copyEditDataSet: [],
        copyEditDatasSource: [],
        productNameFrom: null,
        productNameTo: null,
        delItem: null,
        ///部门
        departments: [
           { value: "MS1", label: "制一课" },
           { value: "MS2", label: "制二课" },
            { value: "MS3", label: "制三课" },
           { value: "MS5", label: "制五课" },
           { value: "MS6", label: "制六课" },
           { value: "MS7", label: "制七课" },
           { value: "MS10", label: "制十课" },
           { value: "PT1", label: "成型课" }],
        ///部门
        inputTypes: [
           { value: "A", label: "制一课" },
           { value: "E", label: "制二课" },
            { value: "F", label: "制三课" },
        ],
        //工时类别
        standardProductionTimeTypes: [{ id: "UPH", text: "UPH" }, { id: "UPS", text: "UPS" }],
        //工艺类别
        processesTypes: [{ id: "人工", text: "人工" }, { id: "机台", text: "机台" }],
        editDatasSummyset: [],
        editDatasSummysetsource: [],
        //查看工艺流程明细 OK
        queryProductionFlowDetails: function (item) {
            vmManager.productName = item.ProductName;
            $scope.searchPromise = dReportDataOpService.getProductionFlowList(vmManager.department, vmManager.productName, "", 2).then(function (datas) {
                vmManager.handleEditDataSource(datas);
            });
        },
        ///处理数据
        handleEditDataSource: function (datas) {
            vmManager.editDataSet = [];
            vmManager.editDatasSource = [];
            angular.forEach(datas, function (e) {
                var dataItem = _.clone(e);
                leeHelper.copyVm(e, dataItem);
                dataItem.Id = leeHelper.newGuid();
                dataItem.IsServer = true;
                vmManager.editDatasSource.push(dataItem);
            });
            vmManager.editDataSet = vmManager.editDatasSource;
        },
        //确认复制
        copyConfirm: function () {
            vmManager.copyEditDataSet = [];
            vmManager.copyEditDatasSource = [];
            if (vmManager.editDatasSource.length <= 0) {
                leePopups.alert("复制数据不能为空");
                return;
            }
            angular.forEach(vmManager.editDatasSource, function (item) {
                var confirmData = _.clone(item);
                confirmData.Id = leeHelper.newGuid();
                confirmData.IsServer = false;//由客户端创建的数据
                confirmData.ProductName = vmManager.productNameTo;
                confirmData.opSign = leeDataHandler.dataOpMode.add;
                vmManager.copyEditDatasSource.push(confirmData);
            });
            vmManager.copyEditDataSet = vmManager.copyEditDatasSource;
        },
        ///选择批量复制
        selectBulkCopyOk: function () {
            vmManager.productNameFrom = vmManager.productName;
            vmManager.copyWindowDisplay = true;
            vmManager.editWindowDisplay = false;
        },

        //获取项目最大配置工序ID
        getInspectionIndex: function () {
            if (vmManager.editDataSet.length >= 0) {
                uiVM.ProcessesIndex = $scope.vm.ProcessesIndex = vmManager.editDataSet.length + 1;
            }
            else {
                $scope.vm.ProcessesIndex = 0;
            }
        },
        //项次添加 
        addProductionFlow: function (item) {
            console.log(item);
            vmManager.init();
            vmManager.editDataSet = [];
            vmManager.queryProductionFlowDetails(item);
            uiVM.ProcessesIndex = item.ProductFlowCount + 1;
            uiVM.ProductName = vmManager.productName;
            vmManager.editWindowDisplay = true;
            focusSetter.processesNameFocus = true;
        },
        // 模糊查找
        vagueQueryProductionSummyDatas: function () {
            $scope.searchPromise = dReportDataOpService.getProductionFlowOverview(vmManager.department, vmManager.productName, 1).then(function (datas) {
                vmManager.editDatasSummyset = [];
                vmManager.editDatasSummyset = datas;
                vmManager.editDatasSummysetsource = datas;
            });
        },
        ///是否显示输入机台信息
        changeIsShowMachine: function () {
            if ($scope.vm.ProcessesType === "机台") {
                vmManager.isShowMachine = true;
            }
            else { vmManager.isShowMachine = false; }
        },
        ///changeDepartment
        changeDepartment: function () {
            $scope.promise = dReportDataOpService.getProductionFlowOverview(vmManager.department, vmManager.productName, 0).then(function (datas) {
                vmManager.editDatasSummyset = [];
                vmManager.editDatasSummyset = datas;
                vmManager.editDatasSummysetsource = datas;
            });
        },
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

    ////
    operate.productNameFrom = function () {
        vmManager.productNameFrom = vmManager.productName;
        vmManager.copyWindowDisplay = true;
    };
    ///添加新一项
    operate.add = function () {
        vmManager.init();
        uiVM.ProductName = vmManager.productName;
        vmManager.getInspectionIndex();
        focusSetter.processesNameFocus = true;
        vmManager.editWindowDisplay = true;
    };
    ///编辑
    operate.editItem = function (item) {
        uiVM = item;
        uiVM.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVM;
        vmManager.editWindowDisplay = true;
        focusSetter.processesNameFocus = true;
    };
    ///删除 
    operate.deleteItem = function (item) {

        leePopups.confirm("删除提示", "您确定要删除该项数据吗？", function () {
            $scope.$apply(function () {
                if (item.IsServer) {
                    $scope.searchPromise = dReportDataOpService.immediatelyDeleteProcessesFlow(item.ProductName, item.ProcessesName).then(function (opresult) {
                        console.log(opresult);
                        if (opresult.Result) {
                            leeDataHandler.dataOperate.handleSuccessResult(operate, opresult);
                            leeHelper.delWithId(vmManager.editDataSet, item);//从表中移除
                            leeHelper.delWithId(vmManager.editDatasSource, item);
                        }
                    });
                }
                else {
                    leeHelper.delWithId(vmManager.editDataSet, item);
                    leeHelper.delWithId(vmManager.editDatasSource, item);
                }
                //移除临时数据
            });
        });
    },
    ////复制表中删除
    operate.copyDeleteItem = function (item) {
        console.log(item);
        leePopups.confirm("删除提示", "您确定要删除复制的数据吗？", function () {
            $scope.$apply(function () {
                if (item.IsServer) {
                    item.OpSign = leeDataHandler.dataOpMode.delete;
                }
                leeHelper.delWithId(vmManager.copyEditDataSet, item);//移除临时数据
            });
        });
        vmManager.copyWindowDisplay = true;
        vmManager.editWindowDisplay = false;
    },

    //确认数据(显示保存)
    operate.confirmSave = function (isValid) {
        leeHelper.setUserData(uiVM);
        //部门信息不用变化
        $scope.vm.Department = vmManager.department;
        ///查询数据是否有相同的工艺名称
        if (uiVM.OpSign === leeDataHandler.dataOpMode.add) {
            var issave = true;
            angular.forEach(vmManager.editDatasSource, function (i) {
                if (i.ProcessesName == uiVM.ProcessesName) {
                    leePopups.alert("已经添加过了！【" + i.ProcessesName + "】");
                    issave = false;
                    return;
                };
            });
            if (!issave) return;
            var confirmData = _.clone(uiVM);
            leeHelper.copyVm(uiVM, confirmData);
            confirmData.Id = leeHelper.newGuid();
            confirmData.IsServer = false;//由客户端创建的数据
            vmManager.editDatasSource.push(confirmData);
            // vmManager.editDataSet = vmManager.editDatasSource;
            vmManager.editWindowDisplay = true;
            vmManager.saveOneinit();
        }

    };

    //批量保存数据
    operate.saveAll = function () {

        console.log(vmManager.editDatasSource);
        $scope.searchPromise = dReportDataOpService.storeProductFlowDatas(vmManager.editDatasSource).then(function (opresult) {
            if (opresult.Result) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult);
                vmManager.handleEditDataSource(vmManager.editDatasSource);
                vmManager.init();
            }
        });
    };
    //批量复制保存数据
    operate.copySaveAll = function () {
        $scope.searchPromise = dReportDataOpService.storeProductFlowDatas(vmManager.copyEditDatasSource).then(function (opresult) {
            if (opresult.Result) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult);
                vmManager.copyEditDataSet = [];
                vmManager.copyEditDatasSource = [];
            }
        });
    };
    //复制取消
    operate.copyCancel = function () {
        vmManager.copyEditDatas = [];
        vmManager.copyWindowDisplay = false;
        vmManager.editWindowDisplay = true;
        vmManager.productNameTo = null;
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
            console.log(el);
            var file = files[0];
            var fd = new FormData();
            fd.append('file', file);
            dReportDataOpService.importProductFlowTemplateFile(fd).then(function (datas) {
                vmManager.editDatas = datas;
            });
        }
    };


    ////组织架构
    //$scope.promise = connDataOpService.getConfigDicData('Organization').then(function (tdatas) {
    //    departmentTreeSet.setTreeDataset(tdatas);
    //    var user = leeLoginUser;
    //});
    $scope.promise = dReportDataOpService.getProductionFlowOverview(vmManager.department, vmManager.productName, 0).then(function (datas) {
        vmManager.editDatasSummyset = [];
        vmManager.editDatasSummyset = datas;
        ///根据登录用户 载入信息 ，如果没有侧 选择载入
        if (datas.length > 0)
            vmManager.departments = [{ value: leeLoginUser.department, label: leeLoginUser.departmentText }];
    });
    //焦点设置器
    var focusSetter = {
        processesNameFocus: false,
        processesIndexFocus: false,
        standardProductionTimeFocus: false,
        remarkFocus: false,
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
        //回车事件
        changeEnter: function ($event, elPreName, elNextName) {
            focusSetter.moveFocusTo($event, elPreName, elNextName)
        }

    };
    $scope.focus = focusSetter;


    $(function () {
        $("[data-toggle='popover']").popover();
    });


    //$scope.ztree = departmentTreeSet;
});
//日报录入
productModule.controller("DailyProductionReportCtrl", function ($scope, dataDicConfigTreeSet, connDataOpService, dReportDataOpService, $modal) {
    ///日报录入视图模型
    var uiVM = {
        Department: null,
        ClassType: '白班',
        InPutDate: new Date(),
        OrderId: null,
        ProductId: null,
        ProductName: null,
        ProductSpec: null,
        OrderQuantity: null,
        StandardProductionTimeType: null,
        ProcessesIndex: 0,
        ProcessesName: null,
        ProcessesType: null,
        StandardProductionTime: null,
        MachinePersonRatio: null,
        MachineId: null,
        MouldId: null,
        MouldHoleCount: 0,
        MachineProductionTime: null,
        MachineUnproductiveTime: null,
        MachineUnproductiveReason: null,
        MasterWorkerId: null,
        MasterName: null,
        WorkerId: null,
        WorkerName: null,
        TodayProductionCount: null,
        TodayBadProductCount: null,
        WorkerProductionTime: null,
        WorkerNoProductionTime: null,
        WorkerNoProductionReason: null,
        Field1: null,
        Field2: null,
        Field3: null,
        Field4: null,
        Field5: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        OpDate: null,
        OpTime: null,
        Id_Key: null,
    }
    $scope.vm = uiVM;
    //初始化视图
    var initVM = _.clone(uiVM);
    var vmManager = {
        ///部门 
        department: leeLoginUser.department,
        queryActiveTab: 'qryFolwProcessTab',
        putInDisplay: false,
        classType: '白班',
        classTypes: [{ id: '白班', text: '白班' }, { id: '晚班', text: '晚班' }],
        putInDate: new Date(),
        productionFlowShow: true,
        putInDataProcessesName: null,

        ///初始化
        init: function () {
            uiVM = _.clone(initVM);
            $scope.vm = uiVM;
        },
        ///保存后继续
        continueSaveInit: function () {
            uiVM.TodayProductionCount = null;
            uiVM.TodayBadProductCount = null;
            uiVM.WorkerProductionTime = null;
            uiVM.WorkerNoProductionTime = null;
            uiVM.WorkerId = null;
            focusSetter.workerIdFocus = true;
            $scope.vm = uiVM;
        },
        ///选择部门
        departments: [
           { value: "MS1", label: "制一课" },
           { value: "MS2", label: "制二课" },
            { value: "MS3", label: "制三课" },
           { value: "MS5", label: "制五课" },
           { value: "MS6", label: "制六课" },
           { value: "MS7", label: "制七课" },
           { value: "MS10", label: "制十课" },
           { value: "PT1", label: "成型课" }],
        searchedWorkers: [],
        processesInfos: [],
        workerId: null,
        isSingle: true,//是否搜寻到的是单个人
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
                            focusSetter.processesIndexFocus = true;
                        }
                        else {
                            vmManager.isSingle = false;
                        }
                    }
                    else {
                        vmManager.selectWorker(null);
                    }
                });
                $scope.searchedWorkersPrommise = dReportDataOpService.getWorkerDailyLastInfoDatas(vmManager.WorkerId).then(function (dailyInfo) {

                });
            }
        },
        selectWorker: function (worker) {
            if (worker !== null) {
                uiVM.WorkerName = worker.Name;
                uiVM.WorkerId = worker.WorkerId;
                uiVM.Department = worker.Department;
                vmManager.WorkerId = worker.WorkerId;
            }
            else {
                uiVM.Department = null;
            }
        },
        havePutInData: [],
        isShowhavePutInData: false,
        erpOrderInfoDatasSet: [],//载入已分配的订单信息
        erpOrderInfoDatasSource: [],
        productionFlowDatasSet: [],//工序信息
        productionFlowDatasSouce: [],//工序信息
        //选择部门
        changeDepartment: function () {
            $scope.promise = dReportDataOpService.getInProductionOrderDatas(vmManager.department).then(function (erpDatas) {
                vmManager.erpOrderInfoDatasSet = [];
                vmManager.erpOrderInfoDatasSource = vmManager.erpOrderInfoDatasSet = erpDatas;
                ///根据登录用户 载入信息 ，如果没有侧 选择载入
                if (erpDatas.length > 0)
                    vmManager.departments = [{ value: leeLoginUser.department, label: leeLoginUser.departmentText }];
            });

        },
        //部门变化载入分配的订单信息
        ////选择产品名称得理该产品的
        putInDatas: function (item) {
            uiVM.OrderId = item.OrderId;
            uiVM.ProductId = item.ProductId;
            uiVM.ProductName = item.ProductName;
            uiVM.ProductSpec = item.ProductSpec;
            uiVM.OrderQuantity = item.ProduceNumber - item.PutInStoreNumber;
            vmManager.getProductionFlowDatas(uiVM.ProductName, uiVM.OrderId);
            vmManager.putInDisplay = false;
            vmManager.queryActiveTab = 'qryFolwProcessTab';
        },
        //选择录入的项次
        getProductionFlowDatas: function (productName, orderId) {
            $scope.searchPromise = dReportDataOpService.getProductionFlowCountDatas(vmManager.department, productName, orderId).then(function (datas) {
                vmManager.productionFlowDatasSet = datas;
                vmManager.productionFlowShow = true;
                vmManager.isShowhavePutInData = false;
                vmManager.putInDisplay = false;
                vmManager.productionFlowDatasSouce = vmManager.productionFlowDatasSet;
            });
        },
        // 得到工序信息
        findProcessesInfo: function ($event) {
            if ($event.keyCode === 13 || $event.keyCode === 40 || $event.keyCode === 9) {
                if (uiVM.ProcessesIndex === null || vmManager.productionFlowDatasSet.length == 0) return;
                var processesInfo = _.find(vmManager.productionFlowDatasSet, function (u) { return u.ProcessesIndex == uiVM.ProcessesIndex })
                if (!_.isUndefined(processesInfo)) {
                    //leePopups.alert("没有此工序号");
                    focusSetter.processesIndexFocus = true;
                    vmManager.selectProcesses(processesInfo);
                    focusSetter.todayProductionCountFocus = true;
                    vmManager.isProcessesNameShow = true;
                }
            };
        },
        //在数据查找相应的信息
        selectProcesses: function (info) {
            uiVM.ProcessesIndex = info.ProcessesIndex;
            uiVM.ProcessesName = info.ProcessesName;
            uiVM.ProcessesType = info.ProcessesType;
            uiVM.StandardProductionTime = info.StandardProductionTime;
            uiVM.StandardProductionTimeType = info.StandardProductionTimeType
            vmManager.isProcessesNameShow = true;
        },
        //选择工序
        showPutInForm: function (item) {
            if (item !== null) {
                uiVM.ProcessesIndex = item.ProcessesIndex;
                uiVM.ProcessesName = item.ProcessesName;
                uiVM.ProcessesType = item.ProcessesType;
                uiVM.StandardProductionTime = item.StandardProductionTime;
                uiVM.StandardProductionTimeType = item.StandardProductionTimeType;
                uiVM.InPutDate = vmManager.putInDate;
                vmManager.havePutInData = [];
                focusSetter.workerIdFocus = true;

            }
            else {
                uiVM.ProcessesIndex = 0;
            }
            if (!vmManager.putInDisplay)
            { vmManager.putInDisplay = true; }
        },
        // 单击录入
        putInshowPutInForm: function (item) {
            vmManager.putInDatas(item);
            vmManager.showputInDisplay();
        },
        //显示明细
        showPutInDetail: function (item) {
            console.log(item);
            vmManager.putInDataProcessesName = item.ProcessesName;
            vmManager.havePutInData = [];
            $scope.searchPromise = dReportDataOpService.getProcessesNameDailyInfoDatas(uiVM.InPutDate, item.OrderId, item.ProcessesName).then(function (dailyDatas) {
                _.forEach(dailyDatas, function (e) {
                    var dataItem = _.clone(e);
                    leeHelper.copyVm(e, dataItem);
                    dataItem.Id = leeHelper.newGuid();
                    dataItem.IsServer = true;
                    vmManager.havePutInData.push(dataItem);

                });
            });
            vmManager.queryActiveTab = 'qryUserInfoTab';
        },
        showputInDisplay: function () {
            if (!vmManager.putInDisplay)
            { vmManager.putInDisplay = true; }
            focusSetter.workerIdFocus = true;
        },
        //选择修改输入信息
        showUserInputInfo: function (item) {
            console.log(item);
            if (!vmManager.putInDisplay)
            { vmManager.putInDisplay = true; }
            focusSetter.workerIdFocus = true;
            uiVM = item;
            uiVM.OpSign = leeDataHandler.dataOpMode.edit;
            $scope.vm = uiVM;
        },
    };
    $scope.vmManager = vmManager;
    $scope.promise = vmManager.changeDepartment();
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    // 保存数据
    operate.saveData = function (isValid) {
        if (uiVM.WorkerProductionTime + uiVM.WorkerNoProductionTime >= 13) {
            leePopups.alert("生产时超出");
            return;
        }
        leeHelper.setUserData(uiVM);
        uiVM.Department = vmManager.department;
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            $scope.searchPromise = dReportDataOpService.saveDailyReportData(uiVM).then(function (opResult) {
                if (opResult.Result) {
                    leeDataHandler.dataOperate.handleSuccessResult(operate, opResult);
                    console.log(opResult.Entity.OpTime);
                    if (opResult.Entity.OpSign == leeDataHandler.dataOpMode.add) {
                        vmManager.havePutInData.push(opResult.Entity);
                        vmManager.continueSaveInit();
                        vmManager.getProductionFlowDatas(uiVM.ProductName, uiVM.OrderId);

                    }
                    vmManager.productionFlowShow = false;
                    vmManager.isShowhavePutInData = true;
                    vmManager.putInDisplay = true;
                }
            });
        });
    };
    //取消
    operate.refresh = function () {
        vmManager.putInDisplay = false;
    }
    //焦点设置器
    var focusSetter = {
        workerIdFocus: false,
        orderIdFocus: false,
        standardProductionTimeFocus: false,
        machineUnproductiveTimeFocus: false,
        processesIndexFocus: false,
        workerProductionTimeFocus: false,
        workerNoProductionTimeFocus: false,
        todayProductionCountFocus: false,
        saveAlldataFocus: false,
        remarkFocus: false,
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
        //回车事件
        changeEnter: function ($event, elPreName, elNextName) {
            focusSetter.moveFocusTo($event, elPreName, elNextName)
        }

    };
    $scope.focus = focusSetter;

});
/// 生产订单分派  
productModule.controller("DailyProductOrderDispatchCtrl", function ($scope, dataDicConfigTreeSet, connDataOpService, dReportDataOpService, $modal) {
    ///日报分派录入视图模型
    var uiVm = {
        OrderId: null,
        ProductionDepartment: null,
        ProductId: null,
        ProductName: null,
        ProductSpec: null,
        PutInStoreNumber: 0,
        ProduceNumber: 0,
        ProductStatus: null,
        ProductionDate: null,
        IsValid: true,
        ValidDate: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        OpDate: null,
        OpTime: null,
        Id_Key: null,
        Id: null,
    }
    $scope.vm = uiVm;
    var initVM = _.clone(uiVm);
    var dialog = $scope.dialog = leePopups.dialog();
    var vmManager = {
        ///部门 
        department: leeLoginUser.department,
        dispatchActiveTab: 'qryERPFormTab',
        departments: [
           { value: "MS1", label: "制一课" },
           { value: "MS2", label: "制二课" },
           { value: "MS3", label: "制三课" },
           { value: "MS5", label: "制五课" },
           { value: "MS6", label: "制六课" },
           { value: "MS7", label: "制七课" },
           { value: "MS10", label: "制十课" },
           { value: "PT1", label: "成型课" }],
        IsValids: [{ id: "true", text: "启用" }, { id: "false", text: "不启用" }],
        erpOrderInfoDatas: [],
        nowDate: new Date(),
        erpOrderInfoDatasSource: [],
        todayHaveDispatchDatas: [],
        haveHaveDispatchCount: 0,
        changeDepartment: function () {
            $scope.promise = dReportDataOpService.getOrderDispatchInfoDatas(vmManager.department, vmManager.nowDate).then(function (datas) {
                vmManager.erpOrderInfoDatas = [];
                vmManager.erpOrderInfoDatasSource = [];
                vmManager.erpOrderInfoDatasSource = vmManager.erpOrderInfoDatas = datas;
                vmManager.haveHaveDispatchCount = 0;
                angular.forEach(vmManager.erpOrderInfoDatas, function (item) {
                    if (item.IsValid == 'True') {
                        vmManager.haveHaveDispatchCount += 1;
                    }
                });
                ///根据登录用户 载入信息 ，如果没有侧 选择载入
                if (datas.erpInProductiondatas > 0)
                    vmManager.departments = [{ value: leeLoginUser.department, label: leeLoginUser.departmentText }];
            });
        },
        ///分配订单到数据表中(取消分配)
        dispatchOrder: function (item) {
            var findItem = _.findWhere(vmManager.todayHaveDispatchDatas, { OrderId: item.OrderId });
            if (_.isUndefined(findItem)) {
                console.log(item);
                leeHelper.copyVm(item, uiVm)
                uiVm.IsValid = true;
                uiVm.OpSign = leeDataHandler.dataOpMode.add;

            }
            else {
                leeHelper.copyVm(findItem, uiVm)
                uiVm.IsValid = false;
                uiVm.OpSign = leeDataHandler.dataOpMode.edit;
                console.log(findItem);
            }
            item.ProductStatus = uiVm.ProductStatus;
            dialog.show();
        },
        editHaveDispatchOrder: function (item) {
            leeHelper.copyVm(item, uiVm)
            uiVm.OpSign = leeDataHandler.dataOpMode.edit;
            dialog.show();
        },
        deleteHaveDispatchOrder: function (item) {
            leeHelper.copyVm(item, uiVm)
            uiVm.IsValid = false;
            uiVm.OpSign = leeDataHandler.dataOpMode.delete;
            dialog.show();
        },
    };
    $scope.vmManager = vmManager;
    $scope.promise = vmManager.changeDepartment();
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveDispatchData = function (isValid) {
        leeHelper.setUserData(uiVm);
        if (uiVm.IsValid == true) uiVm.ProductStatus = "已分配";
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            $scope.promise = dReportDataOpService.saveOrderDispatch(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var dataItem = _.clone(opresult.Entity);
                        var findItem = _.findWhere(vmManager.todayHaveDispatchDatas, { OrderId: dataItem.OrderId });
                        if (_.isUndefined(findItem)) {
                            vmManager.todayHaveDispatchDatas.push(dataItem);
                        }
                        else {
                            if (dataItem.opSign == leeDataHandler.dataOpMode.delete)
                                leeHelper.delWithId(vmManager.todayHaveDispatchDatas, uiVm)
                        };
                        //vmManager.dispatchActiveTab = "qryTodayHaveDispatchTab";
                        dialog.close();
                    }
                });
            });
        });
    }
    operate.Cancel = function () {
        dialog.close();
    }
});