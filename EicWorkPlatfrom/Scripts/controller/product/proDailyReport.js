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
    //保存产品工艺流程数据
    reportDataOp.storeFlowData = function (entity) {
        var url = urlPrefix + 'StoreFlowData';
        return ajaxService.postData(url, {
            entity: entity,
        });
    };

    //导入模板数据
    reportDataOp.importProductFlowTemplateFile = function (file) {
        var url = urlPrefix + 'ImportProductFlowDatas';
        return ajaxService.uploadFile(url, file);
    };
    //获取产品工艺流程总览
    reportDataOp.getProductFlowOverview = function (department, productName, searchMode) {
        var url = urlPrefix + 'GetProductFlowOverview';
        return ajaxService.getData(url, {
            department: department,
            productName: productName,
            searchMode: searchMode,
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
    reportDataOp.getDailyReportTemplate = function (department, dailyReportDate) {
        var url = urlPrefix + 'GetDailyReportTemplate';
        return ajaxService.getData(url, {
            department: department,
            dailyReportDate: dailyReportDate
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
    reportDataOp.saveDailyReportDatas = function (datas, inputDate) {
        var url = urlPrefix + 'SaveDailyReportDatas';
        return ajaxService.postData(url, {
            datas: datas,
            inputDate: inputDate
        });
    };
    //013935保存出勤数据
    reportDataOp.saveReportsAttendenceDatas = function (entity) {
        var url = urlPrefix + 'SaveReportsAttendenceDatas';
        return ajaxService.postData(url, {
            entity: entity
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
    //013935获取日报考勤数据
    reportDataOp.getWorkerAttendanceData = function (department, attendenceStation, reportDate) {
        var url = urlPrefix + 'GetWorkerAttendanceData';
        return ajaxService.getData(url, {
            department: department,
            attendenceStation: attendenceStation,
            reportDate: reportDate
        })
    }
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
        ProcessesName: null,
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
    };
    $scope.vm = uiVM;
    //初始化视图
    var initVM = _.clone(uiVM);
    var vmManager = {
        init: function () {
            uiVM = _.clone(initVM);
            $scope.vm = uiVM;
        },
        isShowMachine: false,
        opSign: null,
        editWindowDisplay: false,
        editWindowWidth: '100%',
        copyWindowDisplay: false,
        department: 'MS7',
        productName: null,
        //编辑显示的数据集合
        editDatas: [],
        //编辑数据复制副本
        copyEditDatas: [],
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
        //工时类别
        standardProductionTimeTypes: [{ id: "UPH", text: "UPH" }, { id: "UPS", text: "UPS" }],
        //工艺类别
        processesTypes: [{ id: "人工", text: "人工" }, { id: "机台", text: "机台" }],
        editDatasSummyset: [],
        editDatasSummysetsource: [],
        //查看工艺流程明细 OK
        queryProductionFlowDetails: function (item) {
            vmManager.editDatas = [];
            vmManager.productName = item.ProductName;
            $scope.searchPromise = dReportDataOpService.getProductionFlowList(vmManager.department, vmManager.productName, "", 2).then(function (datas) {
                angular.forEach(datas, function (e) {
                    leeHelper.setObjectGuid(e);
                    vmManager.editDatas.push(e);
                });
                vmManager.editDatasSummy.push(vmManager.editDatas);
            });
        },
        //获取项目最大配置工序ID
        getInspectionIndex: function () {
            if (vmManager.editDatas.length >= 0) {
                uiVM.ProcessesIndex = $scope.vm.ProcessesIndex = vmManager.editDatas.length + 1;
            }
            else {
                $scope.vm.ProcessesIndex = 0;
            }
        },

        ///工艺名称不能有重复 ---待写
        verifyProcessesName: function () {
            console.log(78975464);
        },
        //项次添加 
        addProductionFlow: function (item) {
            console.log(item);
            vmManager.queryProductionFlowDetails(item);
            vmManager.init();
            uiVM.ProcessesIndex = $scope.vm.ProcessesIndex = item.ProductFlowCount + 1;
            uiVM.ProductName = vmManager.productName;
            leeHelper.setObjectGuid(uiVM);
            vmManager.editWindowDisplay = true;
        },
        // 模糊查找
        vagueQueryProductionSummyDatas: function () {
            $scope.searchPromise = dReportDataOpService.getProductionFlowOverview(vmManager.department, vmManager.productName, 1).then(function (datas) {
                vmManager.editDatasSummyset = [];
                vmManager.editDatasSummyset = datas;
                vmManager.editDatasSummysetsource = datas;
            });
        },
        //获取产品工艺流程列表
        getProductFlowList: function ($event) {
            if ($event.keyCode === 13) {
                $scope.searchPromise = dReportDataOpService.getProductFlowList(vmManager.department, vmManager.productName, "", 2).then(function (datas) {

                    vmManager.editDatas = [];
                    vmManager.editDatas = datas;
                });
            }
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

        delModalWindow: $modal({
            title: "删除提示", content: "您确定要删除此工序信息吗?",
            templateUrl: leeHelper.modalTplUrl.deleteModalUrl, show: false,
            controller: function ($scope) {
                $scope.confirmDelete = function () {
                    leeHelper.remove(vmManager.editDatas, vmManager.delItem);
                    if (vmManager.editDatas.length === 0) {
                        var flowItem = _.find(vmManager.flowOverviews, { ProductName: vmManager.productName });
                        if (flowItem !== undefined) {
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
    ///添加新一项
    operate.add = function () {
        vmManager.init();
        uiVM.ProductName = vmManager.productName;
        vmManager.getInspectionIndex();
        leeHelper.setObjectGuid(uiVM);
        vmManager.editWindowDisplay = true;
    };
    ////
    operate.productNameFrom = function () {
        vmManager.productNameFrom = vmManager.productName;
        vmManager.copyWindowDisplay = true;
    };
    //复制一项
    operate.copyConfirm = function () {
        vmManager.productName = vmManager.productNameTo;
        angular.forEach(vmManager.editDatas, function (item) {
            leeHelper.setObjectGuid(item);
            item.ProductName = vmManager.productNameTo;
            vmManager.copyEditDatas.push(item);
        });
    };
    ///批量复制
    operate.copyok = function () {
        vmManager.editDatas = vmManager.copyEditDatas;
        vmManager.copyWindowDisplay = false;
    };
    operate.copycancel = function () {
        vmManager.copyWindowDisplay = false;
    };
    ///编辑
    operate.editItem = function (item) {
        vmManager.opSign = leeDataHandler.dataOpMode.edit;
        uiVM = item;
        $scope.vm = uiVM;
        vmManager.editWindowDisplay = true;
    };
    ///删除 
    operate.deleteItem = function (item) {
        vmManager.delItem = item;
        vmManager.delModalWindow.$promise.then(vmManager.delModalWindow.show);
        leeHelper.delWithId(vmManager.viewDataset, stepItem);
    },
    //保存数据
    operate.save = function (isValid) {
        leeHelper.setUserData(uiVM);
        //部门信息不用变化
        $scope.vm.Department = vmManager.department;
        ///查询数据是否有相同的工艺名称
        angular.forEach(vmManager.editDatas, function (i) {
            if (i.ProcessesName == uiVM.ProcessesName) {

                return;
            }
        });
        console.log(uiVM);
        $scope.searchPromise = dReportDataOpService.storeFlowData(uiVM).then(function (opresult) {
            if (opresult.Result) {
                console.log(opresult);
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult);
                if (opresult.Entity.OpSign === leeDataHandler.dataOpMode.add) {
                    leeHelper.setObjectGuid($scope.vm);
                    vmManager.editDatas.push($scope.vm);
                    vmManager.editWindowDisplay = false;
                    vmManager.init();
                }

            }
        })
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

    // var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    //departmentTreeSet.bindNodeToVm = function () {
    //    var dto = _.clone(departmentTreeSet.treeNode.vm);
    //    vmManager.department = dto.DataNodeText;
    // };
    $scope.promise = dReportDataOpService.getProductionFlowOverview(vmManager.department, vmManager.productName, 0).then(function (datas) {
        // departmentTreeSet.setTreeDataset(data.departments);
        vmManager.editDatasSummyset = datas;
    });
    $(function () {
        $("[data-toggle='popover']").popover();
    });
    //$scope.ztree = departmentTreeSet;
});
//日报录入
productModule.controller("standardProductionFlowSetCtrl", function ($scope, dataDicConfigTreeSet, connDataOpService, dReportDataOpService, $modal) {
    ///日报录入视图模型
    var uiVM = {
    }
    $scope.vm = uiVM;

    var initVM = _.clone(uiVM);


    var vmManager = {

    };
    $scope.vmManager = vmManager;
    //焦点设置器
    var focusSetter = {
        orderIdFocus: false,
        workerIdFocus: false,
        masterWorkerIdFocus: false,
        productFlowFocus: false,
        qtyFocus: false,
        qtyBadFocus: false,
        setHoursFocus: false,
        inputHoursFocus: false,
        productHoursFocus: false,
        attendanceHoursFocus: false,
        nonProductHoursFocus: false,
        nonProductReasonCodeFocus: false,

        //013935新增考勤焦点
        shouldAttendenceUserCountFocus: false,
        askLeaveUserCountFoucs: false,
        haveLeaveUserCountFocus: false,
        supportOutUserCountFocus: false,
        realityWorkingUserCountFocus: false,
        inNewWorkerCountFocus: false,
        supportInShoutAbsentCount: false,
        supportInRealityWorkingCountFocus: false,
        supportInAskLeaveCountFocus: false,
        supportInHaveLeaveCountFocus: false,
        overWorkUserCountFocus: false,
        attendenceTotalCountFocus: false,
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
        //013935考勤回车事件
        changeEnter: function ($event, elPreName, elNextName) {
            focusSetter.moveFocusTo($event, elPreName, elNextName)
        }

    };
    $scope.focus = focusSetter;

});
