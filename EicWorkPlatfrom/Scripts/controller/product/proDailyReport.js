/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />

var productModule = angular.module('bpm.productApp');
productModule.factory('dReportDataOpService', function (ajaxService) {
    var urlPrefix = "/" + leeHelper.controllers.dailyReport + "/";

    var reportDataOp = {};
    //-------------------------标准工时设置-------------------------------------//
    //获取产品工艺流程列表
    reportDataOp.getProductionFlowDatas = function (queryDto) {
        var url = urlPrefix + 'GetProductionFlowDatas';
        return ajaxService.postData(url, {
            queryDto: queryDto,
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
    reportDataOp.getOrderDispatchInfoDatas = function (queryString, opType) {
        var url = urlPrefix + 'GetOrderDispatchInfoDatas';
        return ajaxService.getData(url, {
            queryString: queryString,
            opType: opType,
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
    /// 单人个保存
    reportDataOp.saveDailyReportData = function (entity) {
        var url = urlPrefix + 'SaveDailyReportData';
        return ajaxService.postData(url, {
            entity: entity,
        });
    };
    ///团队录入批量保存
    reportDataOp.saveGroupDailyReportData = function (entity, groupUserInfos) {
        var url = urlPrefix + 'SaveGroupDailyReportData';
        return ajaxService.postData(url, {
            entity: entity,
            groupUserInfos: groupUserInfos,
        });
    };
    ///处理机台输入分摊工时数据
    reportDataOp.handleMachineDailyReportDatas = function (entitys, workerLookMachineSumTime, workerNoProductionSumTime) {
        var url = urlPrefix + 'HandleMachineDailyReportDatas';
        return ajaxService.postData(url, {
            workerLookMachineSumTime: workerLookMachineSumTime,
            workerNoProductionSumTime:workerNoProductionSumTime,
            entitys: entitys,
        });
    };
    ///机台批量输入
    reportDataOp.saveMachineDailyReportDatas = function (entitys) {
        var url = urlPrefix + 'SaveMachineDailyReportDatas';
        return ajaxService.postData(url, {
            entitys: entitys,
        });
    };
    //----------------------------------------------------------//

    //-------------非生产原因配置--------------------- department, string aboutCategory
    reportDataOp.loadUnProductionConfigDicData = function (department, aboutCategory) {
        var url = urlPrefix + 'LoadUnProductionConfigDicData';
        return ajaxService.getData(url, {
            department: department,
            aboutCategory: aboutCategory,
        });
    };
    ///保存非生产配置原因
    reportDataOp.saveConfigDicData = function (vm, oldVm, opType) {
        var url = urlPrefix + "SaveUnProductionConfigDicData";
        return ajaxService.postData(url, {
            opType: opType,
            model: vm,
            oldModel: oldVm
        });
    };
   /////////--------------------------------生技课生产日报表-----------------------
    reportDataOp.loadPT1MachineInfo = function (department) {
        var url = urlPrefix + "LoadPt1MachineInfo";
        return ajaxService.getData(url, {
            department: department,
        });
    };
    ///保存数据信息
    reportDataOp.SaveMachinePutInDatas = function (entity) {
        var url = urlPrefix + "SaveMachinePutInDatas";
        return ajaxService.postData(url, {
            entity: entity,
        });
    };
    ///GetProductionFlowDatas
    ///--------------
    return reportDataOp;
});
//生产标准艺流程设定
productModule.controller("standardProductionFlowSetCtrl", function ($scope, dReportDataOpService, dataDicConfigTreeSet, connDataOpService, $modal) {
    ///工艺标准工时视图模型
    var uiVM = {
        Department: null,
        DepartmentText: null,
        ProductId: null,
        ProductName: null,
        ProcessesIndex: 0,
        ProcessesSign: '中继站',
        ProcessesName: null,
        ProcessesType: '人工',
        InputType: 'A',
        IsSum: 1,
        IsVisualization: 0,
        IsValid: 1,
        StandardProductionTimeType: 'UPS',
        StandardProductionTime: 0,
        ProductCoefficient: 1,
        UPH: null,
        UPS: null,
        ProductionTimeVersionID: 1,
        MachinePersonRatio: 1,
        MouldId: null,
        MouldName: null,
        MouldHoleCount: 0,
        ParameterKey: null,
        Remark: null,
        OpPerson: null,
        OpDate: null,
        OpSign: leeDataHandler.dataOpMode.add,
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
        editDatasList: [],
        //编辑数据复制副本
        copyEditDataSet: [],
        copyEditDatasSource: [],
        productNameFrom: null,
        productNameTo: null,
        delItem: null,
        isShowProductionTime: true,
        /// 是否有效数据
        IsOrNotDatas: [
            { id: "1", text: "是" },
           { id: "0", text: "否" }],
        ///部门
        departments:leeHelper.selectDepartment,
        ///输入类型
        inputTypes: [
           { id: "A", text: "有工时，计生产" },
                { id: "B", text: "无工时，计生产" },
           { id: "E", text: "无工时，不计生产" },
            { id: "F", text: "有工时，不计生产" },
         
        ],
        selectInputType: function (item) {
            uiVM.InputType = item.id;
        },
        //工时类别
        standardProductionTimeTypes: [
            { id: "UPH", text: "UPH" },
                { id: "UPS", text: "UPS" }],
        //工艺类别
        processesTypes: [
            { id: "人工", text: "人工工时" },
            { id: "机台", text: "机台运行" }],
     
        selectProcessesType: function (item) {
            uiVM.ProcessesType = item.id;
        },
        editDatasSummyset: [],
        editDatasSummysetsource: [],
        standardHoursCount: null,
        //查看工艺流程明细 OK
        queryProductionFlowDetails: function (item) {
            vmManager.productName = item.ProductName;
            uiVM.ProductId = item.ProductId;
            uiVM.ProductName = item.ProductName;
            vmManager.standardHoursCount = item.StandardHoursCount;
            var thisQueryDto = {
                Department: vmManager.department,
                InputDate:null,
                ProductName: vmManager.productName,
                ProcessesName: '',
                OrderId: null,
                SearchMode: 2
            };
           
            $scope.searchPromise = dReportDataOpService.getProductionFlowDatas(thisQueryDto).then(function (datas) {
                vmManager.handleEditDataSource(datas);
            });
        },
        ///处理数据
        handleEditDataSource: function (datas) {
            vmManager.editDataSet = [];
            vmManager.editDatasSource = [];
            vmManager.editDatasList = [];
            angular.forEach(datas, function (e) {
                var dataItem = _.clone(e);
                leeHelper.copyVm(e, dataItem);
                dataItem.OpSign = leeDataHandler.dataOpMode.edit;
                dataItem.Id = leeHelper.newGuid();
                dataItem.IsServer = true;
                vmManager.editDatasSource.push(dataItem);
                vmManager.editDatasList.push(dataItem);
            });
            vmManager.editDataSet = vmManager.editDatasSource;
        },
        //确认复制
        copyConfirm: function () {
            vmManager.copyEditDataSet = [];
            vmManager.copyEditDatasSource = [];
            vmManager.editDatasList = [];
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
                vmManager.copyEditDataSet.push(confirmData);
                vmManager.editDatasList.push(confirmData);
            });
        },
        ///选择批量复制
        selectBulkCopyOk: function () {
            vmManager.productNameFrom = vmManager.productName;
            vmManager.copyWindowDisplay = true;
            vmManager.editWindowDisplay = false;
        },

        //获取项目最大配置工序ID
        getInspectionIndex: function () {
            if (vmManager.editDatasSource.length >= 0) {
                uiVM.ProcessesIndex = $scope.vm.ProcessesIndex = vmManager.editDatasSource.length + 1;
            }
            else {
                $scope.vm.ProcessesIndex = 0;
            }
        },
        //项次添加
        addProductionFlow: function (item) {
            console.log(item);
            vmManager.init();
            vmManager.productName = item.ProductName;
            uiVM.ProductId = item.ProductId;
            uiVM.ProductName = item.ProductName;
            vmManager.standardHoursCount = item.StandardHoursCount;
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
                            leeHelper.delWithId(vmManager.editDatasList, item);

                        }
                    });
                }
                else {
                    leeHelper.delWithId(vmManager.editDataSet, item);
                    leeHelper.delWithId(vmManager.editDatasSource, item);
                    leeHelper.delWithId(vmManager.editDatasList, item);
                }
                vmManager.changeDepartment();
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
            angular.forEach(vmManager.editDatasList, function (i) {
                if (i.ProcessesName == uiVM.ProcessesName && i.MouldId == uiVM.MouldId) {
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
            vmManager.editDataSet.push(confirmData);
            vmManager.editDatasList.push(confirmData);
            // vmManager.editDataSet = vmManager.editDatasSource;
            vmManager.editWindowDisplay = true;
            vmManager.saveOneinit();
        }

    };

    //批量保存数据
    operate.saveAll = function () {
        console.log(vmManager.editDatasList);
        $scope.searchPromise = dReportDataOpService.storeProductFlowDatas(vmManager.editDatasList).then(function (opresult) {
            if (opresult.Result) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult);
                vmManager.handleEditDataSource(vmManager.editDataSet);
                vmManager.init();
                vmManager.changeDepartment();
            }
        });
    };
    //批量复制保存数据
    operate.copySaveAll = function () {
        operate.saveAll();
        //$scope.searchPromise = dReportDataOpService.storeProductFlowDatas(vmManager.copyEditDatasSource).then(function (opresult) {
        //    if (opresult.Result) {
        //        leeDataHandler.dataOperate.handleSuccessResult(operate, opresult);
        //        vmManager.copyEditDataSet = [];
        //        vmManager.copyEditDatasSource = [];
        //    }
        //});
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

        // if (datas.length > 0)
       //     vmManager.departments = [{ value: leeLoginUser.department, label: leeLoginUser.departmentText }];
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
    var nowdate =new Date();
    nowdate.setDate(nowdate.getDate() -1);
    var uiVM = {
        Department: null,
        ClassType: '白班',
        InPutDate: nowdate,
        OrderId: null,
        ProductId: null,
        ProductName: null,
        ProductSpec: null,
        OrderQuantity: null,
        ProcessesType: null,
        ProcessesIndex: 0,
        ProcessesName: null,
        StandardProductionTime: 0,
        WorkerId: null,
        WorkerName: null,
        TodayProductionCount: 0,
        TodayBadProductCount: 0,
        WorkerProductionTime: 0,
        GetProductionTime: 0,
        WorkerNoProductionTime: 0,
        WorkerNoProductionReason: null,
        MasterWorkerId: null,
        MasterName: null,
        MachineId: null,
        MouldId: null,
        MouldHoleCount: 0,
        MachinePersonRatio: 0,
        MachineProductionTime: 0,
        MachineUnproductiveTime: 0,
        MachineUnproductiveReason: null,
        MachineSetProductionTime: 0,
        MachineProductionCount: 0,
        MachineProductionBadCount: 0,
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
        inspectionDataGatherType: 'A',
        department: leeLoginUser.department,
        queryActiveTab: 'qryFolwProcessTab',
        putInDisplay: false,
        inPutMultiermUserInfoTable: false,
        classTypeChange:function(){
            if (uiVM.ClassType === "白班")
                uiVM.ClassType = "晚班";
            else uiVM.ClassType = "白班";
        },
        inPutDate: new Date(),
        productionFlowShow: true,
        putInDataProcessesName: null,
        //始化
        init: function () {
            uiVM = _.clone(initVM);
            $scope.vm = uiVM;
        },
        ///保存后继续
        continueSaveInit: function () {
            uiVM.WorkerId = null;
            uiVM.ProcessesIndex = 0;
            uiVM.ProcessesName = null;
            uiVM.TodayProductionCount = 0;
            uiVM.TodayBadProductCount = 0;
            uiVM.WorkerProductionTime = 0;
            uiVM.WorkerNoProductionTime = 0;
            uiVM.WorkerId = null;
            uiVM.OpSign = leeDataHandler.dataOpMode.add;
            focusSetter.workerIdFocus = true;
            $scope.vm = uiVM;
        },
        ///选择部门
        departments: leeHelper.selectDepartment,
        searchedWorkers: [],
        processesInfos: [],
        workerId: null,
        isSingle: true,//是否搜寻到的是单个人
        ///得到用户信息
        getWorkerInfo: function (workerid,opData) {
            if (workerid === undefined) return;
            var strLen = leeHelper.checkIsChineseValue(workerid) ? 2 : 6;
            if (workerid.length >= strLen) {
                if (opData==1)  vmManager.searchedWorkers = [];
                console.log(vmManager.departmentMasterDatas);
                $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(workerid).then(function (datas) {
                      vmManager.searchedWorkers = datas;
                      console.log(vmManager.searchedWorkers);
                    if (datas.length > 0) {
                            vmManager.searchedWorkers = datas;
                            console.log(vmManager.searchedWorkers);
                            var mm = _.findWhere(vmManager.departments, { value: vmManager.department });
                            var userInfo = _.findWhere(vmManager.searchedWorkers, { Department: mm.label });
                            if (_.isUndefined(userInfo)) {
                                if (vmManager.searchedWorkers.length == 1) {
                                    vmManager.isSingle = true;
                                    vmManager.selectWorker(vmManager.searchedWorkers[0], opData);
                                }
                                else vmManager.isSingle = false;
                            }
                            else {
                                vmManager.isSingle = true;
                                vmManager.selectWorker(userInfo, opData);
                            }
                        }
                        else {
                            vmManager.selectWorker(null);
                        }
                });
            }
        },
        ///录入作业员工号 selectWorker
        selectWorker: function (worker, opData) {
            if (worker !== null) {
                if (opData === 1) {
                    uiVM.WorkerName = worker.Name;
                    uiVM.WorkerId = worker.WorkerId;
                };
                if (opData == 2) {
                    uiVM.MasterName = worker.Name;
                    uiVM.MasterWorkerId = worker.WorkerId;
                };
                uiVM.Department = vmManager.department;
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
        departmentMasterDatas: [],
        //选择部门
        changeDepartment: function () {
            $scope.promise = dReportDataOpService.getInProductionOrderDatas(vmManager.department).then(function (datas) {
                vmManager.erpOrderInfoDatasSet = [];
                vmManager.productionFlowDatasSet = [];
                vmManager.erpOrderInfoDatasSource = vmManager.erpOrderInfoDatasSet = datas.haveDispatchOrderDatas;
                vmManager.departmentMasterDatas = datas.departmentMasterDatas;
              
                ///根据登录用户 载入信息 ，如果没有侧 选择载入
                // if (erpDatas.length > 0)
                ///   vmManager.departments = [{ value: leeLoginUser.department, label: leeLoginUser.departmentText }];
            });
        },
        ////选择产品名称得理该产品的
        putInDatas: function (item) {
            uiVM.OrderId = item.OrderId;
            uiVM.ProductId = item.ProductId;
            uiVM.ProductName = item.ProductName;
            uiVM.ProductSpec = item.ProductSpec;
            uiVM.OrderQuantity = item.ProduceNumber;
            vmManager.getProductionFlowDatas(uiVM.ProductName, uiVM.OrderId);
            vmManager.queryActiveTab = 'qryFolwProcessTab';
        },
        //选择品名得到所有的工艺统计
        getProductionFlowDatas: function (productName, orderId) {
            vmManager.productionFlowDatasSouce = vmManager.productionFlowDatasSet = [];
            $scope.searchPromise = dReportDataOpService.getProductionFlowCountDatas(vmManager.department, productName, orderId).then(function (datas) {
                //除重复的工艺
                _.forEach(datas, function (m) {
                    var mm = _.findWhere(vmManager.productionFlowDatasSet, { ProcessesName: m.ProcessesName })
                    if (_.isUndefined(mm))
                        vmManager.productionFlowDatasSet.push(m);
                });
                vmManager.isShowhavePutInData = false;
                vmManager.productionFlowDatasSouce = vmManager.productionFlowDatasSet;
                vmMMachineInPut.machineProductionFlowDatasSouce = _.where(vmManager.productionFlowDatasSouce, { ProcessesType: '机台' })
            });
        },
        // 得到工序信息
        findProcessesInfo: function ($event) {
            if ($event.keyCode === 13 || $event.keyCode === 39 || $event.keyCode === 9) {
                vmManager.findProcesses();
            };
        },
        ///选择录入
        findProcesses: function () {
            if (uiVM.ProcessesIndex === null || vmManager.productionFlowDatasSouce.length == 0) return;
            var processesInfo = _.find(vmManager.productionFlowDatasSouce, function (u) { return u.ProcessesIndex == uiVM.ProcessesIndex })
            if (!_.isUndefined(processesInfo)) {
                //leePopups.alert("没有此工序号");
                vmManager.selectProcesses(processesInfo);
            };
        },
        //在数据查找相应的信息
        selectProcesses: function (info) {
            ///先暂时这样设置  后面可以根据机台编号 得到他设置的时间
            if (info.ProcessesType == "机台") uiVM.MachineSetProductionTime = 12;
            uiVM.ProcessesIndex = info.ProcessesIndex;
            uiVM.ProcessesName = info.ProcessesName;
            uiVM.ProcessesType = info.ProcessesType;
            uiVM.StandardProductionTime = info.StandardProductionTime;
            vmManager.inspectionDataGatherType = info.InputType;
            uiVM.StandardProductionTimeType = info.StandardProductionTimeType;
            if (uiVM.TodayProductionCount != 0 && uiVM.TodayProductionCount != null && uiVM.TodayProductionCount != '' && vmManager.inputMultitermSelect)
            { focusSetter.saveAlldataFocus = true }
            else
            {
                if (info.InputType == 'E')
                    focusSetter.workerProductionTimeFocus = true;
                else focusSetter.todayProductionCountFocus = true;
            };
            vmManager.isProcessesNameShow = true;
        },
        //选择工序录入 显示输入界面
        showPutInForm: function (item) {
            if (item !== null) {
                uiVM.ProcessesIndex = item.ProcessesIndex;
                uiVM.Department = vmManager.department;
                uiVM.ProcessesType = item.ProcessesType;
                uiVM.ProcessesName = item.ProcessesName;
                uiVM.StandardProductionTime = item.StandardProductionTime;
                uiVM.StandardProductionTimeType = item.StandardProductionTimeType;
                if (item.StandardProductionTimeType == "UPH") {
                    uiVM.StandardProductionTime = item.UPS
                };
                uiVM.WorkerId = null;
                uiVM.WorkerName = null;
                uiVM.WorkerProductionTime = 0;
                uiVM.WorkerNoProductionTime = 0;
                uiVM.WorkerNoProductionReason = null;
                vmManager.findProcesses();
                vmManager.havePutInData = [];
                uiVM.OpSign = leeDataHandler.dataOpMode.add;
                focusSetter.workerIdFocus = true;
            }
            vmManagerPT.loadPtMachineDatas();
            vmManagerPT.getOrderIdDatas();
            //展示输入界面
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
            uiVM.Department = item.Department;
            uiVM.ProcessesType = item.ProcessesType;
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
            vmManager.showputInDisplay();
            uiVM = item;
            uiVM.OpSign = leeDataHandler.dataOpMode.edit;
            $scope.vm = uiVM;
            if (item.ProcessesType == '机台')
                vmMMachineInPut.showMachineDialog();
        },
        //删除录入个人日报表数据
        deleteUserInputInfo: function (item) {
            leePopups.confirm("删除提示", "您确定要删除该项数据吗？", function () {
                $scope.$apply(function () {
                    item.opSign = leeDataHandler.dataOpMode.delete;
                    $scope.searchPromise = dReportDataOpService.saveDailyReportData(item).then(function (opresult) {
                        if (opresult.Result) {
                            leeHelper.remove(vmManager.havePutInData, item);
                            var mm = _.findWhere(vmManager.productionFlowDatasSet, { ProcessesName: item.ProcessesName })
                            if (!_.isUndefined(mm)) {
                                mm.OrderHavePutInNumber -= item.TodayProductionCount;
                            };
                            vmManager.getProductionFlowDatas(item.ProductName, item.OrderId);
                        }
                    });
                    //移除临时数据
                });
            });
        },
        //是否团队合作输入
        isMultiermUserInPut: false,
        //快速查询确认
        confirmSearch: function ($event, item) {
            if ($event.keyCode === 13 || $event.keyCode === 40 || $event.keyCode === 9) {
                if (vmManager.erpOrderInfoDatasSet.length > 0) {
                    var data = _.find(vmManager.erpOrderInfoDatasSet, function (e) {
                        if (e.OrderId.indexOf(item) >= 0) {
                            return e;
                        }
                    });
                    if (!_.isUndefined(data)) {
                        vmManager.putInshowPutInForm(data);
                    }
                }
                console.log(item);
            }
        },
    };
    /////多项录入人员信息模板
    var uiVmUser = {
        WorkerId: null,
        WorkerName: null,
        WorkerProductionTime: 0,
        WorkerNoProductionTime: 0,
        WorkerNoProductionReason: 0,
        GetProductionTime:0,
    };
    var initVmUser = _.clone(uiVmUser);
    $scope.vmUser = uiVmUser;
    ///团队录入修改Dialog
    var dialog = $scope.dialog = leePopups.dialog();
    var vmManagerMultiermUser = {
        clearMultiermUsersInfo: function () {
            if (uiVmUser.WorkerProductionTime + uiVmUser.WorkerNoProductionTime >= 13) {
                leePopups.alert("生产时数超出");
                return;
            }
            if (uiVmUser.WorkerId != null) {
                uiVmUser.Id = leeHelper.newGuid();
                vmManagerMultiermUser.multiermUserInPutInfos.push(uiVmUser);
                vmManagerMultiermUser.init();
            };
        },
        searchedWorkers: [],
        isSingle: false,
        multiermGetWorkerInfo: function () {
            if (uiVmUser.WorkerId === undefined) return;
            var strLen = leeHelper.checkIsChineseValue(uiVmUser.WorkerId) ? 2 : 6;
            if (uiVmUser.WorkerId.length >= strLen) {
                vmManagerMultiermUser.searchedWorkers = [];
                $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(uiVmUser.WorkerId).then(function (workersdatas) {
                    if (workersdatas.length > 0) {
                        vmManagerMultiermUser.searchedWorkers = workersdatas;
                        if (vmManagerMultiermUser.searchedWorkers.length === 1) {
                            vmManagerMultiermUser.isSingle = true;
                            vmManagerMultiermUser.selectWorker(vmManagerMultiermUser.searchedWorkers[0]);
                        }
                        else vmManagerMultiermUser.isSingle = false;
                    }
                    else vmManagerMultiermUser.selectWorker(null);
                });
            };
        },
        selectWorker: function (worker) {
            if (worker !== null) {
                uiVmUser.WorkerId = worker.WorkerId;
                uiVmUser.WorkerName = worker.Name;
                uiVM.Department = worker.Department;
                if (uiVM.ProcessesIndex === 0 || uiVM.ProcessesIndex === null)
                    focusSetter.processesIndexFocus = true;
                else {
                    if (uiVmUser.WorkerName === null || uiVmUser.WorkerName === undefined)
                        focusSetter.workerIdsFocus = true;
                    else focusSetter.workerProductionTimeFocus = true;
                };
            }
            else uiVM.Department = null;
        },
        ///初始化
        init: function () {
            uiVmUser = _.clone(initVmUser);
            $scope.vmUser = uiVmUser;
        },
        //团长总产量
        groupTodayProductionCount: 0,
        //团长总产量
        groupTodayBadProductCount: 0,

        //选择多人方式
        selectMultiermUserInput: function () {
            if (!vmManager.isMultiermUserInPut) {
                vmManager.isMultiermUserInPut = true;
            }
        },
        //选择个人方式
        selectSingleUserInput: function () {
            if (vmManager.isMultiermUserInPut) {
                vmManager.isMultiermUserInPut = false;
            }
        },
        //是否人员显示列表
        multiermUserInPutInfoTable: false,
        //输入显示信息
        inPutShowlab: [],
        //输入存储信息
        multiermUserInPutInfos: [],
        //修改
        changeMultiermUserInfo: function (item) {
            $scope.vmUser = item;
            dialog.show();
        },
        userInfoChange: function () {
            vmManagerMultiermUser.multiermUserInPutInfoTable = true;
            dialog.close();
        },
        //删除
        deleteMultiermUserInfo: function (item) {
            leePopups.confirm("删除提示", "您确定要删除此数据吗？", function () {
                $scope.$apply(function () {
                    leeHelper.delWithId(vmManagerMultiermUser.multiermUserInPutInfos, item);;//移除临时数据
                });
            });
        },
        //显示列表
        showInPutInfoTable: function () {
            if (vmManagerMultiermUser.multiermUserInPutInfoTable) {
                vmManagerMultiermUser.multiermUserInPutInfoTable = false;
            }
            else vmManagerMultiermUser.multiermUserInPutInfoTable = true;
        },
        //多项输入最后一项
        changeInPutNoProductionTime: function ($event) {
            if ($event.keyCode === 13 || $event.keyCode === 40 || $event.keyCode === 9) {
                if (uiVmUser.WorkerNoProductionTime != 0) {
                    focusSetter.workerNoProductionReasonFocus = true;
                }
                else {
                    focusSetter.workerIdsFocus = true;
                    vmManagerMultiermUser.clearMultiermUsersInfo();
                }
            }
        },
        changeInPutNoProductionReason: function ($event) {
            if ($event.keyCode === 13 || $event.keyCode === 40 || $event.keyCode === 9) {
                if (uiVmUser.WorkerNoProductionReason !== null) {
                    vmManagerMultiermUser.clearMultiermUsersInfo();
                    focusSetter.workerIdsFocus = true;
                }
            }
        },

    };
    $scope.vmManagerMultiermUser = vmManagerMultiermUser;
    $scope.vmManager = vmManager;
    $scope.promise = vmManager.changeDepartment();
    var operate = Object.create(leeDataHandler.operateStatus);
    ///机台录入数据Dialog
    var machineDialog = $scope.machineDialog = leePopups.dialog();
    var vmMMachineInPut = {
        isConfirmMachineInputData: false,
        workerLookMachineProductionTime: null,
        workerNoLookMachineProductionTime: null,
        putInDatasSet: [],
        handleDatas: [],
        ///
        machineProductionFlowDatasSouce: [],
        //工序输入
        putInProcesses: function ($event) {
            if ($event.keyCode === 13 || $event.keyCode === 39 || $event.keyCode === 9) {
                if (uiVM.ProcessesIndex === null || vmMMachineInPut.machineProductionFlowDatasSouce.length == 0) return;
                var processesInfo = _.find(vmMMachineInPut.machineProductionFlowDatasSouce, function (u) { return u.ProcessesIndex == uiVM.ProcessesIndex })
                if (!_.isUndefined(processesInfo)) {
                    vmManager.selectProcesses(processesInfo);
                };
                focusSetter.machineProductionCountFocus = true;
            };
        },
        clearDatas: function () {
            uiVM.MachineId = null;
            uiVM.MachineProductionBadCount = null;
            uiVM.MachineProductionTime = null;
            uiVM.MachineUnproductiveTime = null;
            uiVM.MachineUnproductiveReason = "无订单";
            uiVM.MachineProductionCount = null;
            focusSetter.machineIdFocus = true;
        },
        ///罐入数据 半计算分摊数据
        pushMachinePutInData: function (isValid, $event) {
            if ($event.keyCode === 13 || $event.keyCode === 39 || $event.keyCode === 9) {
                var machineInfo = _.find(vmMMachineInPut.putInDatasSet, function (u) { return u.MachineId == uiVM.MachineId })
                if (_.isUndefined(machineInfo)) {
                    leeDataHandler.dataOperate.add(operate, isValid, function () {
                        leeHelper.setUserData(uiVM);
                        uiVM.Department = vmManager.department;
                        var inputMahcineVm = _.clone(uiVM);
                        if (vmMMachineInPut.inputInfoIsValid()) {
                            inputMahcineVm.id = leeHelper.newGuid();
                            vmMMachineInPut.putInDatasSet.push(inputMahcineVm);
                            vmMMachineInPut.handleHavePutIntData(vmMMachineInPut.putInDatasSet);
                            vmMMachineInPut.clearDatas();
                        };
                    });
                }
                else {
                    leePopups.alert("此机台已经输入");
                    return;
                };
            };
        },
       
        ///删除已经输入的机台信息
        deleteInputMachineVm: function (item) {
            leePopups.confirm("删除提示", "您确定要删除此数据吗？", function () {
                $scope.$apply(function () {
                    leeHelper.remove(vmMMachineInPut.handleDatas, item);
                    var machineInfo = _.find(vmMMachineInPut.putInDatasSet, function (u) { return u.MachineId == item.MachineId })
                    leeHelper.remove(vmMMachineInPut.putInDatasSet, machineInfo);
                    vmMMachineInPut.handleHavePutIntData(vmMMachineInPut.handleDatas);
                   
                });
            });
        },
        ///显示机台输入 machineDialog
        showMachineDialog: function () {
            $scope.promise = dReportDataOpService.loadUnProductionConfigDicData(leeLoginUser.organization.B, "UnProductionConfig").then(function (datas) {
                unProductionCodeTreeSet.setTreeDataset(datas);
            });
            vmMMachineInPut.workerLookMachineProductionTime = uiVM.WorkerProductionTime;
            vmMMachineInPut.workerNoLookMachineProductionTime = uiVM.WorkerNoProductionTime;
            if (vmMMachineInPut.inputInfoIsValid()) {
                machineDialog.show();
                focusSetter.machineIdFocus = true;
            };
        },
        //验证录入的信息是否有效
        inputInfoIsValid: function () {
            if (uiVM.OrderId == null || uiVM.OrderId.length == 0) {
                leePopups.alert("工单信息不能为空");
                return false;
            }
            if (uiVM.WorkerName == null || uiVM.WorkerName.length == 0 || uiVM.WorkerId == null || uiVM.WorkerId.length == 0) {
                leePopups.alert("作业人员信息不能为空");
                return false;
            };
            if (uiVM.MasterName == null || uiVM.MasterName.length == 0 || uiVM.MasterWorkerId == null || uiVM.MasterWorkerId.length == 0) {
                leePopups.alert("机台师傅信息不能为空");
                return false;
            };
            if (uiVM.WorkerProductionTime == null || uiVM.WorkerProductionTime.length == 0 || uiVM.WorkerProductionTime == 0) {
                leePopups.alert("出勤时数不能为空或零");
                return false;
            };
            if (uiVM.WorkerNoProductionTime != null && uiVM.WorkerNoProductionTime.length != 0 && uiVM.WorkerNoProductionTime != 0) {
                if (uiVM.WorkerNoProductionReason == null || uiVM.WorkerNoProductionReason.length == 0) {
                    leePopups.alert("非生产不为零，必填写非生产原因！");
                    return false;
                }; 
            };
            return true;
        },
        ///固定单头（人员录入信息）
        confirmWorkerInputInfoEnter: function ($event) {
            if ($event.keyCode === 13 || $event.keyCode === 39 || $event.keyCode === 9) {
                if (uiVM.WorkerProductionTime + uiVM.WorkerNoProductionTime >= 13) {
                    leePopups.alert("生产总工时不能超过13小时");
                    return;
                }
                if (uiVM.WorkerNoProductionTime > 0)
                    focusSetter.workerNoProductionReasonFocus = true;
                else focusSetter.showMachineDialogFocus = true
            };
        },
        ///不良代码编写---- 返回不良代码信息
        workerNoProductionReasonFoucschange: function ($event) {
            if ($event.keyCode === 13 || $event.keyCode === 39 || $event.keyCode === 9) {
                focusSetter.showMachineDialogFocus = true;
            };
        },
        ///清除返回输入数据
        hideInPutMachineInfoTable: function () {
            vmMMachineInPut.handleDatas = [];
            vmMMachineInPut.putInDatasSet = [];
            vmMMachineInPut.clearDatas();
            machineDialog.close();
        },
        /// 输入完成处理分摊数据
        handleHavePutIntData: function (datasSource) {
            vmMMachineInPut.handleDatas = [];
            dReportDataOpService.handleMachineDailyReportDatas(datasSource, vmMMachineInPut.workerLookMachineProductionTime, vmMMachineInPut.workerNoLookMachineProductionTime).then(function (datas) {
                vmMMachineInPut.handleDatas = datas
            });
        },
    };
    $scope.vmMMachineInPut = vmMMachineInPut;
    $scope.operate = operate;
    //保存(单项)数据
    operate.saveData = function (isValid) {
        if (vmManager.isMultiermUserInPut) {
            operate.saveMulitelData();
        }
        else {
            operate.saveSingleData(isValid);
        };
        vmManager.getProductionFlowDatas(uiVM.ProductName, uiVM.OrderId);
    };
    ///机台录入保存数据
    operate.saveMachineDatas = function (isValid) {
        _.forEach(vmMMachineInPut.handleDatas, function (e) {  e.InPutDate =leeHelper.formatServerDate(e.InPutDate); });
        $scope.searchPromise = dReportDataOpService.saveMachineDailyReportDatas(vmMMachineInPut.handleDatas).then(function (datasResult) {
            if (datasResult.opResult.Result) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, datasResult.opResult);
                angular.forEach(datasResult.entitys, function (m) {
                    if (m.OpSign == leeDataHandler.dataOpMode.add) {
                        m.InPutDate = leeHelper.formatServerDate(m.InPutDate);
                        vmManager.havePutInData.push(m);
                    }
                });
                vmMMachineInPut.handleDatas = [];
                vmMMachineInPut.putInDatasSet = [];
                vmMMachineInPut.clearDatas();
                machineDialog.close();
                vmManager.putInDisplay = false;
                vmManager.getProductionFlowDatas(uiVM.ProductName, uiVM.OrderId);
            };
        });
    };
    /// 团队合作录入信息
    operate.saveMulitelData = function (isValid) {
        if (vmManagerMultiermUser.multiermUserInPutInfos.length > 0) {
            leeHelper.setUserData(uiVM);
            uiVM.Department = vmManager.department;
            $scope.searchPromise = dReportDataOpService.saveGroupDailyReportData(uiVM, vmManagerMultiermUser.multiermUserInPutInfos).then(function (datasResult) {
                console.log(datasResult);
                if (datasResult.opResult.Result) {
                    console.log(datasResult.dataslist);
                    leeDataHandler.dataOperate.handleSuccessResult(operate, datasResult.opResult);
                    angular.forEach(datasResult.dataslist, function (m) {
                        if (m.OpSign == leeDataHandler.dataOpMode.add) {
                            m.InPutDate = leeHelper.formatServerDate(m.InPutDate);
                            vmManager.havePutInData.push(m);
                        }
                    });
                    // vmManager.getProductionFlowDatas(uiVM.ProductName, uiVM.OrderId);
                    vmManagerMultiermUser.multiermUserInPutInfos = [];
                };
            });
        };
    };
    /// 单个录入信息
    operate.saveSingleData = function (isValid) {
        if (uiVM.WorkerProductionTime + uiVM.WorkerNoProductionTime >= 13) {
            leePopups.alert("生产时超出");
            return;
        }
        if (uiVM.TodayProductionCount != 0 && uiVM.StandardProductionTime != 0 && uiVM.WorkerProductionTime != 0) {
            var getTime = (uiVM.TodayProductionCount * uiVM.StandardProductionTime / 3600).toFixed(2);
            uiVM.GetProductionTime = getTime;
            if (getTime >= (uiVM.WorkerProductionTime * 1.6)) {
                leePopups.confirm("错误提示", "您得到工时已经超出 【生产工时60%】",
                    function () {
                        $scope.$apply(function () {
                            save(isValid);
                        })
                    },
                    function () {
                        $scope.$apply(function () {
                            return;
                        })
                    });
            }
            else  save(isValid);
        }
        else save(isValid);
    };
    var save = function (isValid) {
        leeHelper.setUserData(uiVM);
        uiVM.Department = vmManager.department;
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            $scope.searchPromise = dReportDataOpService.saveDailyReportData(uiVM).then(function (opResult) {
                if (opResult.Result) {
                    leeDataHandler.dataOperate.handleSuccessResult(operate, opResult);
                    if (opResult.Entity.OpSign == leeDataHandler.dataOpMode.add) {
                        opResult.Entity.InPutDate = leeHelper.formatServerDate(opResult.Entity.InPutDate);
                        vmManager.havePutInData.push(opResult.Entity);
                        vmManager.continueSaveInit();
                        var mm = _.findWhere(vmManager.productionFlowDatasSet,{ ProcessesName: Result.Entity.ProcessesName })
                        if (!_.isUndefined(mm)) {
                            mm.OrderHavePutInNumber += Result.Entity.TodayProductionCount;
                        };
                    }
                    vmManager.showputInDisplay();
                }
            });
        });
    };
    //取消
    operate.refresh = function () {
        vmManager.putInDisplay = false;
        //  vmManager.init();
    };
    ///制6课 载入生产原因
    var unProductionCodeTreeSet = dataDicConfigTreeSet.getTreeSet('UnProductionSeason', "非生产原因");
       unProductionCodeTreeSet.bindNodeToVm = function () {
           unProductionCodeConfigDto = _.clone(unProductionCodeTreeSet.treeNode.vm);
           uiVM.MachineUnproductiveReason = unProductionCodeConfigDto.DataNodeText;
    };
       $scope.ztree = unProductionCodeTreeSet;

    ///生技课录入
       var vmManagerPT = {
           mentMasterDatas: [],
           machineDatas: [],
           mouldIdInfos:[],
           //载入初始机台的据数信息
           loadPtMachineDatas: function () {
               $scope.promise = dReportDataOpService.loadPT1MachineInfo(vmManager.department).then(function (datas) {
                   vmManagerPT.machineDatas = datas.machineData;
                   vmManagerPT.mentMasterDatas = datas.departmentMasterDatas;
               });
           },
           // 机台模具信息
           getOrderIdDatas: function () {
               var thisQueryDto = {
                   Department: 'PT1',
                   InputDate: null,
                   ProductName: uiVM.ProductName,
                   ProcessesName: '注塑',
                   OrderId: null,
                   SearchMode: 2
               };
               vmManagerPT.mouldIdInfos = [];
               $scope.promise = dReportDataOpService.getProductionFlowDatas(thisQueryDto).then(function (flowdatas) {
                   var flowdataList = _.where(flowdatas, { ProcessesType: '机台' })
                   _.forEach(flowdataList, function (m) {
                       vmManagerPT.mouldIdInfos.push(m);
                   });
               });
           },
           // 选择模具编号  .selectmouldId
           selecMachineId: function (i) {
               uiVM.MachineId = i.MachineId;
               uiVM.MachineSetProductionTime = i.MachineSetProductionTime;
           },
           selectmouldId: function (i) {
               uiVM.MouldHoleCount = i.MouldHoleCount;
               uiVM.MouldId = i.MouldId;
               var parameterKey = 'PT1' + '&' + uiVM.ProductName + '&' + '注塑' + '&' + uiVM.MouldId
               var dd = _.findWhere(vmManagerPT.mouldIdInfos, { ParameterKey: parameterKey });
               if (!_.isUndefined(dd)) {
                   uiVM.ProcessesIndex = dd.ProcessesIndex;
                   uiVM.ProcessesName = dd.ProcessesName;
                   uiVM.ProcessesType = dd.ProcessesType;
                   uiVM.StandardProductionTime = dd.StandardProductionTime
                   if (dd.StandardProductionTimeType == "UPH") {
                       uiVM.StandardProductionTime = dd.UPS
                   };
               }
           },
           //看机人员信息
           searchedWorkers: [],
           //师傅信息
           mentMasterDatas: [],

           isSingle: false,//是否显示人员
           isMasterSingle :false,
           ///得到用户信息
           getWorkerInfo: function (workerId,selectInt) {
               if (workerId === undefined) return;
               var strLen = leeHelper.checkIsChineseValue(workerId) ? 2 : 6;
               if (workerId.length >= strLen) {
                   $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(workerId).then(function (datas) {
                       console.log(datas);
                       if (datas.length > 0) {
                           vmManagerPT.searchedWorkers = datas;
                           if (vmManagerPT.searchedWorkers.length === 1) {
                               if (selectInt === 1)  vmManagerPT.selectWorker(vmManagerPT.searchedWorkers[0]);
                               if (selectInt === 2)
                               {
                                   vmManagerPT.selectMasterWorker(vmManagerPT.searchedWorkers[0]);
                                   var dd = _.findWhere(vmManagerPT.mentMasterDatas, { WorkerId: vmManagerPT.searchedWorkers[0].WorkerId });
                                   if (_.isUndefined(dd)) {
                                       vmManagerPT.mentMasterDatas.push(vmManagerPT.searchedWorkers[0]);
                                   };
                               }
                           }
                           else {
                               vmManagerPT.isSingle = false;
                               vmManagerPT.isMasterSingle = false;
                           }
                       }
                       else {
                           vmManagerPT.selectWorker(null);
                       }
                   });
               }
           },
           ///录入工号附值  selectWorker
           selectWorker: function (worker) {
               if (worker !== null) {
                   vmManagerPT.isSingle = true;
                       uiVM.WorkerName = worker.Name;
                       uiVM.WorkerId = worker.WorkerId;
                       focus.workerProductionTimeFocus=true
               }
           },
           ///selectMasterWorker
           selectMasterWorker:function(worker)
           {
               if (worker !== null) {
                   vmManagerPT.isMasterSingle = true;
                       uiVM.MasterName = worker.Name;
                       uiVM.MasterWorkerId = worker.WorkerId;
                       focus.workerIdFocus = true;
               }
           },


           //选择修改输入信息 changeHaveInputInfo
           changeHaveInputInfo: function (item) {
               vmManager.showputInDisplay();
               uiVM = item;
               uiVM.OpSign = leeDataHandler.dataOpMode.edit;
               $scope.vm = uiVM;
       
           },
          
           //删除录入个人日报表数据
           deleteHaveInputInfo: function (item) {
               leePopups.confirm("删除提示", "您确定要删除该项数据吗？", function () {
                   $scope.$apply(function () {
                       item.opSign = leeDataHandler.dataOpMode.delete;
                       $scope.searchPromise = dReportDataOpService.saveDailyReportData(item).then(function (opresult) {
                           if (opresult.Result) {
                               leeHelper.remove(vmManager.havePutInData, item);
                               var mm = _.findWhere(vmManager.productionFlowDatasSet, { ProcessesName: item.ProcessesName })
                               if (!_.isUndefined(mm)) {
                                   mm.OrderHavePutInNumber -= item.TodayProductionCount;
                               };
                               vmManager.getProductionFlowDatas(item.ProductName, item.OrderId);
                           }
                       });
                       //移除临时数据
                   });
               });
           },
       };
       $scope.vmManagerPT = vmManagerPT;

    //焦点设置器
    var focusSetter = {
        workerIdFocus: false,
        workerIdsFocus: false,
        orderIdFocus: false,
     
        //生产工时
        workerProductionTimeFocus: false,
        getProdutionTimeFocus: false,
        standardProductionTimeFocus: false,
        machineUnproductiveTimeFocus: false,
        processesIndexFocus: false,
        workerNoProductionReasonFocus: false,
        workerNoProductionTimeFocus: false,
        todayProductionCountFocus: false,
        saveAlldataFocus: false,
        inPutMachineworkerNoProductionTimeFocus: false,
        showMachineDialogFocus: false,
        getProductionTimeFocus:false,
        //机台输入焦点
        machineIdFocus: false,
        machineProductionCountFocus: false,
        machineProductionTimeFocus: false,
        machineUnproductiveReasonFocus: false,
        machineProductionBadCountFocus: false,
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
        },
        //移下个焦点
        moveFocusNext: function ($event, elNextName) {
            if ($event.keyCode === 13 || $event.keyCode === 39 || $event.keyCode === 9) {
                focusSetter[elNextName] = true;
            }
        },

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
        IsVirtualOrderId: 0,
        DicpatchStatus: null,
        IsValid: true,
        ValidDate: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        OpDate: null,
        OpTime: null,
        Id_Key: null
    }
    $scope.vm = uiVm;
    var initVM = _.clone(uiVm);
    var dialog = $scope.dialog = leePopups.dialog();
    var dialogVirtualOrder = $scope.dialogVirtualOrder = leePopups.dialog();
    var vmManager = {
        ///部门
        init: function () {
            uiVM = _.clone(initVM);
            $scope.vm = uiVM;
        },
        ////工单查询
        queryOrderId: function (orderid) {
            $scope.promise = dReportDataOpService.getOrderDispatchInfoDatas(orderid,2).then(function (datas) {
                vmManager.erpOrderInfoDatas = [];
                vmManager.erpOrderInfoDatasSource = [];
                vmManager.erpOrderInfoDatasSource = vmManager.erpOrderInfoDatas = datas;
            });
        },
        department: leeLoginUser.department,
        departments:leeHelper.selectDepartment,
        IsValids: [{ id: true, text: "启用" }, { id: false, text: "不启用" }],
        erpOrderInfoDatas: [],
        nowDate: new Date(),
        erpOrderInfoDatasSource: [],
        virtualOrderDatas: [],
        virtualOrderDatasSource: [],
        haveHaveDispatchCount: 0,
        changeDepartment: function () {
            $scope.promise = dReportDataOpService.getOrderDispatchInfoDatas(vmManager.department, 1).then(function (datas) {
                vmManager.erpOrderInfoDatas = [];
                vmManager.erpOrderInfoDatasSource = [];
                vmManager.virtualOrderDatas = [];
                vmManager.virtualOrderDatasSource = [];
                vmManager.erpOrderInfoDatasSource = vmManager.erpOrderInfoDatas = datas.erpOrderDatas;
                vmManager.virtualOrderDatasSource = vmManager.virtualOrderDatas = datas.virtualOrderDatas;
                vmManager.haveHaveDispatchCount = 0;
                angular.forEach(vmManager.erpOrderInfoDatas, function (item) {
                    if (item.IsValid == 'true') {
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
            leeHelper.copyVm(item, uiVm)
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            dialog.show();
        },
        editVirtualOrder: function (item) {
            leeHelper.copyVm(item, uiVm)
            uiVm.OpSign = leeDataHandler.dataOpMode.edit;
            dialogVirtualOrder.show();
        },
        deleteVirtualOrder: function (item) {
            leePopups.confirm("删除提示", "您确定要删除该项数据吗？", function () {
                $scope.$apply(function () {
                    leeHelper.copyVm(item, uiVm);
                    uiVm.IsValid = false;
                    uiVm.OpSign = leeDataHandler.dataOpMode.delete;
                    $scope.promise = dReportDataOpService.saveOrderDispatch(uiVm).then(function (opresult) {
                        leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                            if (opresult.Result) {
                                var orderid = opresult.Entity.OrderId;
                                var findItem = _.findWhere(vmManager.virtualOrderDatas, { OrderId: orderid });
                                vmManager.virtualOrderDatas.splice(0, findItem);
                            }
                        });
                    });

                })
            });



        },
        showVirtualOrderdialog: function () {
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            dialogVirtualOrder.show();
        },
    };
    $scope.vmManager = vmManager;
    $scope.promise = vmManager.changeDepartment();

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveDispatchData = function (isValid) {
        leeHelper.setUserData(uiVm);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            $scope.promise = dReportDataOpService.saveOrderDispatch(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var orderid = opresult.Entity.OrderId;
                        opresult.Entity.ValidDate = leeHelper.formatServerDate(opresult.Entity.ValidDate);
                        opresult.Entity.ProductionDate = leeHelper.formatServerDate(opresult.Entity.ProductionDate);
                        var findItem = _.findWhere(vmManager.erpOrderInfoDatas, { OrderId: orderid });
                        leeHelper.copyVm(opresult.Entity, findItem);
                        dialog.close();
                    }
                });
            });
        });
    }
    operate.savevirtualOrderData = function (isValid) {
        leeHelper.setUserData(uiVm);
        uiVm.IsVirtualOrderId = 1;
        uiVm.ProductionDepartment = vmManager.department;
        uiVm.ProductionDate = new Date();
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            $scope.promise = dReportDataOpService.saveOrderDispatch(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var orderid = opresult.Entity.OrderId;
                        opresult.Entity.ValidDate = leeHelper.formatServerDate(opresult.Entity.ValidDate);
                        opresult.Entity.ProductionDate = leeHelper.formatServerDate(opresult.Entity.ProductionDate);
                        var findItem = _.findWhere(vmManager.virtualOrderDatas, { OrderId: orderid });
                        if (_.isUndefined(findItem)) { vmManager.virtualOrderDatas.push(opresult.Entity) }
                        else {
                            leeHelper.copyVm(opresult.Entity, findItem);
                            findItem = opresult.Entity;
                        }
                        dialogVirtualOrder.close();
                    }
                });
            });
        });
    }
    operate.Cancel = function () {
        dialog.close();
        dialogVirtualOrder.close();
    }
});
/// 重工登记 DailyRedoProductOrderCtrl
productModule.controller("DailyRedoProductOrderCtrl", function ($scope, dataDicConfigTreeSet, connDataOpService, dReportDataOpService, $modal) {
    ///日报录入视图模型
    var uiVm = {
        Department:' 制一',
        OrderId: null,
        ProductName: null,
        ProductSpec: null,
        ProcessesName: null,
        ProductInPutNumber: 0,
        BadNumber: 0,
        BadDescription: null,
        BadReason: null,
        ScrapTreatmentNumber: null,
        ResponsibleAttributionClass: null,
        ResponsiblePerson: null,
        TreatmentMethod: null,
        TreatmentPerson: null,
        OpPerson: null,
        OpSign:leeDataHandler.dataOpMode.add,
        OpTime: null,
        Id_Key: null,
    }
    $scope.vm = uiVm;
    var initVM = _.clone(uiVm);



    var vmManager = {
    };
    $scope.vmManager = vmManager;

  
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

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
// 非生产原类配置 
productModule.controller("DailyReportUnProductionSetCtrl", function ($scope, dataDicConfigTreeSet, connDataOpService, dReportDataOpService, $modal) {
    ///
    var unProductionCodeConfigDto = {
        Department: departmentOrganization,
        DataNodeName: null,
        DataNodeText: null,
        ParentDataNodeText: null,
        IsHasChildren: 0,
        AboutCategory: 'smDepartmentSet',
        DisplayOrder: 0,
        Memo: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        OpDate: null,
        OpTime: null,
        Id_Key: null,
    };
    var oldUnProductionCodeConfigDto = _.clone(unProductionCodeConfigDto);
    $scope.vm = unProductionCodeConfigDto;
    var operate = Object.create(leeDataHandler.operateStatus);
    operate.vm = unProductionCodeConfigDto;
    $scope.operate = operate;
    //删除节点
    operate.delNode = function () {
        if (angular.isUndefined(departmentTreeSet.treeNode) || departmentTreeSet.treeNode === null) {
            alert("请先选择要删除的节点!")
        }
        else {
            operate.deleteModal.$promise.then(operate.deleteModal.show);
        }
    };
    ///添加子节点
    operate.addChildNode = function (isValid) {
        saveDataDicNode(isValid, leeDataHandler.dataOpMode.add, 'addChildNode');
    };
    ///添加同级节点
    operate.addNode = function (isValid) {
        saveDataDicNode(isValid, leeDataHandler.dataOpMode.add, 'addNode');
    };
    ///修改节点数据
    operate.updateNode = function (isValid) {
        saveDataDicNode(isValid, leeDataHandler.dataOpMode.edit, 'updateNode');
    };
   ///保存数据操作
    var saveDataDicNode = function (isValid, opType, opNodeType) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            dReportDataOpService.saveConfigDicData(unProductionCodeConfigDto, oldUnProductionCodeConfigDto, opType).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var vm = _.clone($scope.vm);
                        if (opType === leeDataHandler.dataOpMode.add) {
                            vm.Id_Key = opresult.Id_Key;
                            var newNode = {
                                name: vm.DataNodeText,
                                children: [],
                                vm: vm
                            };
                            if (opNodeType === "addChildNode")
                                leeTreeHelper.addChildrenNode(unProductionCodeTreeSet.treeId, unProductionCodeTreeSet.treeNode, newNode);
                            else if (opNodeType === "addNode")
                                leeTreeHelper.addNode(unProductionCodeTreeSet.treeId, unProductionCodeTreeSet.treeNode, newNode);
                        }
                            //修改节点
                        else if (opType === leeDataHandler.dataOpMode.edit) {
                            if (opNodeType === "updateNode") {
                                unProductionCodeTreeSet.treeNode.name = vm.DataNodeText;
                                unProductionCodeTreeSet.treeNode.vm = vm;
                                var childrens = unProductionCodeTreeSet.treeNode.children;
                                angular.forEach(childrens, function (childrenNode) {
                                    childrenNode.vm.ParentDataNodeText = vm.DataNodeText;
                                })
                                leeTreeHelper.updateNode(unProductionCodeTreeSet.treeId, unProductionCodeTreeSet.treeNode);
                            }
                        }
                        pHelper.clearVM();
                    }
                });
            });
        });
    };
    //刷新操作
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            pHelper.clearVM();
        });
    };
    ///删除对话框
    operate.deleteModal = $modal({
        title: "删除提示",
        content: "你确定要删除此节点数据吗?",
        templateUrl: leeHelper.modalTplUrl.deleteModalUrl,
        controller: function ($scope) {
            $scope.confirmDelete = function () {
                dReportDataOpService.saveConfigDicData(departmentDto, oldDepartmentDto, 'delete').then(function (opresult) {
                    if (opresult.Result) {
                        operate.deleteModal.$promise.then(operate.deleteModal.hide);
                        leeTreeHelper.removeNode(departmentTreeSet.treeId, departmentTreeSet.treeNode);
                        operate.refresh();
                    }
                })
            };
        },
        show: false
    });
    var pHelper = {
        clearVM: function () {
            leeHelper.clearVM(unProductionCodeConfigDto, ['AboutCategory', 'ParentDataNodeText']);
        }
    };
    // 部门组织
    var departmentOrganization = leeLoginUser.organization.C + "," + leeLoginUser.organization.B + "," + leeLoginUser.organization.K

    ///树的根级
    var unProductionCodeTreeSet = dataDicConfigTreeSet.getTreeSet('lightmaster', "非生原因架构");
    ///选择树 赋值对相应的模
    unProductionCodeTreeSet.bindNodeToVm = function () {
        unProductionCodeConfigDto = _.clone(unProductionCodeTreeSet.treeNode.vm);
        console.log(unProductionCodeConfigDto);
        unProductionCodeConfigDto.AboutCategory = "UnProductionConfig";
        oldUnProductionCodeConfigDto = _.clone(unProductionCodeConfigDto);
        $scope.vm = unProductionCodeConfigDto;
    };
    ///  对树赋值
    $scope.ztree = unProductionCodeTreeSet;

    $scope.promise = dReportDataOpService.loadUnProductionConfigDicData("MD1", "UnProductionConfig").then(function (datas) {
        console.log(departmentOrganization);
        unProductionCodeTreeSet.setTreeDataset(datas);
    });

});
//机台信息录入
productModule.controller("DailyReportMachineCtrl", function ($scope, dataDicConfigTreeSet, connDataOpService, dReportDataOpService, $modal) {
    ///日报录入视图模型
    var nowdate = new Date();
    nowdate.setDate(nowdate.getDate() - 1);
    var uiVM = {
        Department: '成型课',
        MachineId: '80T-4',
        MachineCode: null,
        MachineSetProductionTime: 12,
        MachineName: null,
        MachineSpec: null,
        MachineManufactureId: null,
        MachinePreserver: null,
        MachinePreserId:null,
        PurchaseDate: null,
        State: null,
        Remarks: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        OpDate: null,
        OpTime: null,
        Id_Key: null,
    }
    $scope.vm = uiVM;
    //初始化视图
    var initVM = _.clone(uiVM);
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    var vmManager = {
        searchedWorkers: [],
        init: function () {
            uiVM = _.clone(initVM);
            $scope.vm = uiVM;
        },
        isSingle: false,
        taxTypes:
        [
            { value: "自制件", label: "自制件" },
            { value: "改装件", label: "改装件" },
            { value: "采购件", label: "采购件" },
        ],
        ///得到用户信息
        getWorkerInfo: function (workerId) {
            if (workerId === undefined) return;
            var strLen = leeHelper.checkIsChineseValue(workerId ) ? 2 : 6;
            if (workerId.length >= strLen) {
                $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(workerId).then(function (datas) {
                    console.log(datas);
                    if (datas.length > 0) {
                        vmManager.searchedWorkers = datas;
                        if (datas.length=== 1) {
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
        ///选择用户信息
        selectWorker: function (worker) {
            if (worker !== null) {
                uiVM.MachinePreserver= worker.Name;
                uiVM .Department = worker.department;
            }
            else {
                uiVM.Department = null;
            }
        },

    };
    $scope.vmManager = vmManager;
    //保存数据
    operate.saveDatas = function () {
        _.forEach(vmManagerPT.datasets, function (e) {
            e.InPutDate = uiVM.InPutDate;
            e.Department = vmManagerPT.department;
            leeHelper.setUserData(e);
        });
        $scope.searchPromise = dReportDataOpService.saveMachineInPutDatas(vmManagerPT.datasets).then(function (datasResult) {
            if (datasResult.result) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, datasResult.result);
                
            };
        });
    };
    //刷新数据
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    };

    //var vmManagerPT = {
    //    ///部门
    //    inspectionDataGatherType: 'A',
    //    department: '成型课',
    //    queryActiveTab: 'qryFolwProcessTab',
    //    putInDisplay: false,
    //    inPutMultiermUserInfoTable: false,
    //    classType: '白班',
    //    classTypeChange: function (item) {
    //        if (item.ClassType === "白班")
    //            item.ClassType = "晚班";
    //        else item.ClassType = "白班";
    //    },
    //    inPutDate: nowdate,
    //    productionFlowShow: true,
    //    putInDataProcessesName: null,
    //    //始化
    //    init: function () {
    //        uiVM = _.clone(initVM);
    //        $scope.vm = uiVM;
    //    },
    //    datasource: [],
    //    datasets: [],
    //    ///选择部门
    //    departments: [
    //       { value: "成型课", label: "成型课" },
    //       { value: "注塑课", label: "注塑课" }],
    //    // 创建一列数据 目前没有用到
    //    createRowItem: function () {
    //        var vm = _.clone(initVM);
    //        uiVM = _.clone(vm);
    //        $scope.vm = uiVM;
    //        vm.DailyReportDate = vmManagerPT.InputDate;
    //        vm.rowindex = vmManagerPT.edittingRowIndex;
    //        vm.editting = true;
    //        vm.isMachineMode = false;
    //        $scope.vm.OrderId = orderIdPre[vmManagerPT.department];
    //        vmManagerPT.edittingRow = vm;
    //        return vm;
    //    },
    //    //插入某一行
    //    insertNewRow: function (tab, item) {
    //        var rowindex = item.rowindex;
    //        var vm = _.clone(item);
    //        vm.rowindex = rowindex + 1;
    //        leeHelper.insert(vmManagerPT.datasets, rowindex, vm);
    //        var index = 1;
    //        //重新更改行的索引
    //        angular.forEach(vmManagerPT.datasets, function (row) {
    //            row.rowindex = index;
    //            index += 1;
    //        })
    //    },
    //    //删除行
    //    removeRow: function (tab, item) {
    //        leeHelper.remove(vmManagerPT.datasets, item);
    //    },
    //    //载入初始机台的据数信息
    //    loadDaialyDatas: function (queryDate) {
    //        vmManagerPT.datasource = vmManagerPT.datasets = [];
    //        $scope.promise = dReportDataOpService.loadPT1MachineInfo("PT1").then(function (datas) {
    //            _.forEach(datas.Pt1ReportVmData, function (m) {
    //                m.rowindex++;
    //                m.ClassType = "白班";
    //                m.mouldIdInfos = [];
    //                vmManagerPT.datasets.push(m);
    //            });
    //            vmManagerPT.datasource = vmManagerPT.datasets;
    //            vmManagerPT.mentMasterDatas = datas.departmentMasterDatas;
    //        });
    //    },
    //    // 机台模具信息
    //    getOrderIdDatas: function (item) {
    //        $scope.promise = dReportDataOpService.getOrderDispatchInfoDatas(item.OrderId, 2).then(function (datas) {
    //            if (datas.length > 0) {
    //                var data = _.first(datas);
    //                item.ProductName = data.ProductName;
    //                item.ProductId = data.ProductId;
    //                item.ProductSpec = data.ProductSpec;
    //            };
    //            var thisQueryDto = {
    //                Department: 'PT1',
    //                InputDate: null,
    //                ProductName: item.ProductName,
    //                ProcessesName: '注塑',
    //                OrderId: null,
    //                SearchMode: 2
    //            };
    //            item.mouldIdInfos = [];
    //            $scope.promise = dReportDataOpService.getProductionFlowDatas(thisQueryDto).then(function (flowdatas) {
    //                var flowdataList = _.where(flowdatas, { ProcessesType: '机台' })
    //                _.forEach(flowdataList, function (m) {
    //                    item.mouldIdInfos.push(m);
    //                });
    //            });
    //        });
    //    },
    //    // 选择模具编号
    //    selectmouldId: function (i,item) {
    //        item.MouldHoleCount = i.MouldHoleCount;
    //        item.MouldId = i.MouldId;
    //        var parameterKey = 'PT1' + '&' + item.ProductName + '&' + '注塑' + '&' + item.MouldId
    //        var dd = _.findWhere(item.mouldIdInfos, { ParameterKey: parameterKey });
    //        if (!_.isUndefined(dd)) {
    //            item.ProcessesIndex = dd.ProcessesIndex;
    //            item.ProcessesName = dd.ProcessesName;
    //            item.ProcessesType = dd.ProcessesType;
    //            item.StandardProductionTime = dd.StandardProductionTime
    //            if (dd.StandardProductionTimeType == "UPH") {
    //                item.StandardProductionTime = dd.UPS
    //            };
    //        }
    //    },
    //    //看机人员信息
    //    searchedWorkers: [],
    //    //师傅信息
    //    mentMasterDatas:[],
    //    isSingle:false,//是否显示人员
    //    ///得到用户信息
    //    getWorkerInfo: function (item) {
    //        if (item.WorkerId === undefined) return;
    //        var strLen = leeHelper.checkIsChineseValue(item.WorkerId) ? 2 : 6;
    //        if (item.WorkerId.length >= strLen) {
    //            $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(item.WorkerId).then(function (datas) {
    //                console.log(datas);
    //                if (datas.length > 0) {
    //                    vmManagerPT.searchedWorkers = datas;
    //                    if (vmManagerPT.searchedWorkers.length === 1) {
    //                        vmManagerPT.isSingle = true;
    //                        vmManagerPT.selectWorker(vmManagerPT.searchedWorkers[0], item, 2);
    //                    }
    //                    else {
    //                        vmManagerPT.isSingle = false;
    //                    }
    //                }
    //                else {
    //                    vmManagerPT.selectWorker(null);
    //                }
    //            });
    //        }
    //    },
    //    ///录入工号附值  selectWorker
    //    selectWorker: function (worker, item,selectInt) {
    //        if (worker !== null) {
    //            if (selectInt == 2) {
    //                item.WorkerName = worker.Name;
    //                item.WorkerId = worker.WorkerId;
    //            }
    //            if (selectInt == 1) {
    //                vmManagerPT.isSingle = true;
    //                item.MasterName = worker.Name;
    //                item.MasterWorkerId = worker.WorkerId;
    //            }
    //            item.Department = worker.department;
    //        }
    //        else {
    //            uiVM.Department = null;
    //        }
    //    },
    //};
    //$scope.vmManagerPT = vmManagerPT;
    /////机台录入保存数据
    //operate.saveAllDatas = function () {
    //    _.forEach(vmManagerPT.datasets, function (e) {
    //        e.InPutDate = uiVM.InPutDate;
    //        e.Department = vmManagerPT.department;
    //        leeHelper.setUserData(e);
    //    });
    //    $scope.searchPromise = dReportDataOpService.saveMachineDailyReportDatas(vmManagerPT.datasets).then(function (datasResult) {
    //        if (datasResult.opResult.Result) {
    //            leeDataHandler.dataOperate.handleSuccessResult(operate, datasResult.opResult);
    //            angular.forEach(datasResult.entitys, function (m) {
    //                if (m.OpSign == leeDataHandler.dataOpMode.add) {
    //                  m.InPutDate = leeHelper.formatServerDate(m.InPutDate);
    //                }
    //            });
    //        };
    //    });
    //};
});