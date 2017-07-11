/// <reference path="quaInspectionManage.js" />

/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');
//数据访问工厂
qualityModule.factory("rmaDataOpService", function (ajaxService) {
    var rma = {};
    var quaRmaManageUrl = "/quaRmaManage/";
    //-------------RMA表单创建----------------------------
    //自动生成RMA表单号
    rma.autoCreateRmaId = function () {
        var url = quaRmaManageUrl + 'AutoBuildingRmaId';
        return ajaxService.getData(url, {
        });
    };
    ///获取RMA表单单头
    rma.getRmaReportMaster = function (rmaId) {
        var url = quaRmaManageUrl + 'GetRmaReportMaster';
        return ajaxService.getData(url, {
            rmaId: rmaId
        });
    };
    ////// 存储初始创建的Rma表
    rma.storeRmaBuildRmaIdData = function (initiateData) {
        var url = quaRmaManageUrl + 'StoreinitiateDataData';
        return ajaxService.postData(url, {
            initiateData: initiateData
        });
    };
    //----------------RMA业务处理--------------------------
    //////// 得到 Rma表描述
    rma.getRmaDescriptionDatas = function (rmaId) {
        var url = quaRmaManageUrl + "GetRmaDescriptionDatas";
        return ajaxService.getData(url, {
            rmaId: rmaId
        });
    };
    //获取退货单数据
    rma.getReturnOrderInfo = function (orderId) {
        var url = quaRmaManageUrl + 'GetReturnOrderInfo';
        return ajaxService.getData(url, {
            orderId: orderId
        });
    };
    ///保存RMA描述登记数据
    rma.storeRmaInputDescriptionData = function (model) {
        var url = quaRmaManageUrl + 'StoreRmaInputDescriptionData';
        return ajaxService.postData(url, {
            model: model
        });
    };
    //----------------RMA品保检测处理-----------------------
    ///通过RMA单得到业务处理 和品保处理相应信息
    rma.getRmaInspectionHandleDatas = function (rmaId) {
        var url = quaRmaManageUrl + 'GetRmaInspectionHandleDatas';
        return ajaxService.getData(url, {
            rmaId: rmaId
        });
    };
    ///存储业务处理RMA单
    rma.storeRmaInspectionHandleDatas = function (model) {
        var url = quaRmaManageUrl + 'StoreRmaInspectionHandleDatas';
        return ajaxService.postData(url, {
            model: model
        });
    };
    /// 上传文档
    rma.uploadRmaHandleFile = function (files) {
        var url = quaRmaManageUrl + 'UploadRmaHandleFile';
        return ajaxService.uploadFile(url, files);
    };
    ///
    ///打印文档
    ///
    rma.printDetailsDatas = function (rmaId) {
        var url = quaRmaManageUrl + 'PrintDetailsDatas';
        return ajaxService.getData(url, {
            rmaId: rmaId
        });
    };

    //========================== 查询=============================================
    //查询 从年月 到年月
    rma.queryRmaDatas = function (dateFrom, dateTo) {
        var url = quaRmaManageUrl + 'GetRmaDatas';
        return ajaxService.postData(url, {
            dateFrom: dateFrom,
            dateTo: dateTo
        });
    };
    rma.getCustomerShortNameDatas = function (archiveConfig, rmaCustomerShortName) {
        var url = quaRmaManageUrl + 'GetCustomerShortNameDatas';
        return ajaxService.getData(url, {
            archiveConfig: archiveConfig,
            rmaCustomerShortName: rmaCustomerShortName
        });
    };
    return rma;
});
////创建RMA表单
qualityModule.controller('createRmaFormCtrl', function ($scope, rmaDataOpService) {
    ///视图模型
    var uiVm = $scope.vm = {
        RmaId: null,
        ProductName: null,
        CustomerShortName: null,
        RmaIdStatus: "空单号",
        RmaYear: null,
        RmaMonth: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: 0
    };
    $scope.vm = uiVm;
    //初始化原型
    var initVM = _.clone(uiVm);
    var vmManager = {
        customerShortNames: [],
        //自动生成RMA编号
        autoCreateRmaId: function () {
            $scope.doPromise = rmaDataOpService.autoCreateRmaId().then(function (rmaId) {
                uiVm.RmaId = rmaId;
                uiVm.OpSign = leeDataHandler.dataOpMode.add;
            });
        },
        //获取表单数据
        getRmaFormDatas: function () {
            $scope.searchPromise = rmaDataOpService.getRmaReportMaster(uiVm.RmaId).then(function (data) {
                vmManager.dataSets = data;
            });
        },
        dataSets: [],
        init: function () {
            uiVm = _.clone(initVM);
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVm;
        }
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);

    $scope.operate = operate;
    operate.edit = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVm = item;
    };

    operate.saveAll = function (isValid) {
        var isContainCustomerShortName = false;
        leeHelper.setUserData(uiVm);
        angular.forEach(vmManager.customerShortNames, function (customerShortName) {
            if (uiVm.CustomerShortName == customerShortName.text)
            { isContainCustomerShortName = true; }
        });
        if (!isContainCustomerShortName) {
            alert("供应商不在列表中");
            return;
        };
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            rmaDataOpService.storeRmaBuildRmaIdData(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var dataItem = _.clone(opresult.Entity);
                        console.log(opresult.Entity);
                        dataItem.Id_Key = opresult.Id_Key;
                        if (dataItem.OpSign === leeDataHandler.dataOpMode.add) {
                            vmManager.dataSets.push(dataItem);
                        }
                        vmManager.init();
                    }
                });
            });
        });
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    };

    $scope.promise = rmaDataOpService.getCustomerShortNameDatas('ArchiveConfig', 'RmaCustomerShortName').then(function (datas) {
        vmManager.customerShortNames = [];
        console.log(datas);
        angular.forEach(datas, function (dataitem) {
            console.log(dataitem);
            vmManager.customerShortNames.push({ name: dataitem.DataNodeName, text: dataitem.DataNodeText, labelName: dataitem.labelName });
        });
        console.log(vmManager.customerShortNames);
    });
});
//// 描述RMA登记
qualityModule.controller('rmaInputDescriptionCtrl', function ($scope, rmaDataOpService, $modal) {
    //需要存诸Model信息
    var uiVm = item = $scope.vm = {
        Id: null,
        RmaId: null,
        RmaIdNumber: 0,
        ReturnHandleOrder: null,
        ProductId: null,
        ProductName: null,
        ProductSpec: null,
        ProductCount: null,
        RealityHandleProductCount: null,
        CustomerId: null,
        CustomerName: null,
        SalesOrders: null,
        ProductsShipDates: null,
        BadDescription: null,
        CustomerHandleSuggestion: null,
        FeePaymentWay: null,
        HandleStatus: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: 0
    };
    var initVM = _.clone(uiVm);
    ///视图模型
    var rmavm = $scope.rmavm = {
        RmaId: null,
        ProductName: null,
        CustomerShortName: null
    };
    var salesOrderItem = {
        SalesOrder: '',
        ProductsShipDate: ''
    };
    ///导入ERP信息对话框
    var dialog = $scope.dialog = leePopups.dialog();
    ///删除对话框
    var deleteDialog = $scope.deleteDialog = leePopups.dialog();

    var vmManager = {
        //初始化
        init: function () {
            uiVm = _.clone(initVM);
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVm;
        },
        //是否为输入状态
        isdisabled: false,
        returnOrderDatas: [],
        //获取预处理数据
        getPreHandleData: function () {
            $scope.searchPromise = rmaDataOpService.getRmaDescriptionDatas(uiVm.RmaId).then(function (data) {
                if (angular.isObject(data)) {
                    vmManager.dataSets = [];
                    leeHelper.copyVm(data.rmaInitiateData, rmavm);
                    vmManager.dataSets = data.bussesDescriptionDatas;
                    vmManager.isdisabled = true;
                }
                vmManager.init();
            });
        },
        //获取ERP退货单信息
        getReturnOrderData: function ($event) {
            if ($event.keyCode === 13)
                $scope.searchPromise = rmaDataOpService.getReturnOrderInfo(uiVm.ReturnHandleOrder).then(function (datas) {
                    vmManager.returnOrderDatas = datas;
                    dialog.show();
                });
        },
        //选ERP订单信息
        selectReturnOrderItem: function (item) {
            leeHelper.copyVm(item, uiVm);
            uiVm.Id = leeHelper.newGuid();
            uiVm.RealityHandleProductCount = uiVm.ProductCount;
            console.log(uiVm.ProductCount);
            if (uiVm.ProductCount < 0) {
                uiVm.SalesOrder = '/';
                uiVm.ProductsShipDate = new Date();
                uiVm.BadDescription = '/';
                uiVm.CustomerHandleSuggestion = '/';
            };
            $scope.vm = uiVm;
            dialog.close();
        },
        dataSets: [item],
        ///删除数据
        deleteItemReturnHandleOrder: null,
        deleteItemProductName: null,
        deleteItemProductCount: null,
        edittingRowIndex: 0,//编辑中的行索引
        salesOrders: [],
        productsShipDates: [],
        salersOrdersDatas: [salesOrderItem],
        createRowItem: function () {
            var vm = _.clone(salesOrderItem);
            return vm;
        },
        createNewRow: function () {
            vmManager.edittingRowIndex = vmManager.salersOrdersDatas.length > 0 ? vmManager.salersOrdersDatas.length + 1 : 1;
            var vm = vmManager.createRowItem();
            vmManager.salersOrdersDatas.push(vm);
        },
        //插入某一行
        insertRow: function (item) {
            var rowindex = item.rowindex;
            vmManager.edittingRowIndex = rowindex + 1;
            var vm = vmManager.createRowItem();
            leeHelper.insert(vmManager.salersOrdersDatas, rowindex, vm);
            var index = 1;
            //重新更改行的索引
            angular.forEach(vmManager.salersOrdersDatas, function (row) {
                row.rowindex = index;
                index += 1;
            })
        },
        //删除一行
        removeRow: function (item) {
            if (vmManager.salersOrdersDatas.length > 1) {
                leeHelper.remove(vmManager.salersOrdersDatas, item);
            }
        },
        handleSalsesOrdersAndShipDate: function () {
            uiVm.SalesOrders = [];
            vmManager.productsShipDates = [];
            angular.forEach(vmManager.salersOrdersDatas, function (item) {
                vmManager.salesOrders.push(item.SalesOrder);
                vmManager.productsShipDates.push(item.ProductsShipDate);
            });
            if (vmManager.salesOrders.length > 0) {
                uiVm.SalesOrders = vmManager.salesOrders.join(',');
            }
            if (vmManager.productsShipDates.length > 0) {
                uiVm.ProductsShipDates = vmManager.productsShipDates.join(',');
            };
        }
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    ///编辑
    operate.editItem = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.edit;
        var SalesOrders = item.SalesOrders.split(',');
        var ProductsShipDateStr = item.ProductsShipDates.split(',');
        console.log(SalesOrders);
        console.log(ProductsShipDateStr);
        vmManager.salersOrdersDatas = [];
        for (var i = 0; i < SalesOrders.length; i++) {
            salesOrderItem = [];
            salesOrderItem.SalesOrder = SalesOrders[i];
            salesOrderItem.ProductsShipDate = ProductsShipDateStr[i];
            console.log(salesOrderItem);
            vmManager.salersOrdersDatas.push(salesOrderItem);
        }
        $scope.vm = uiVm = item;
    };
    //删除
    operate.deleteItem = function (item) {
        item.OpSign = leeDataHandler.dataOpMode.delete;
        $scope.vm = uiVm = item;
        vmManager.deleteItemReturnHandleOrder = item.ReturnHandleOrder;
        vmManager.deleteItemProductName = item.ProductName;
        vmManager.deleteItemProductCount = item.ProductCount;
        deleteDialog.show();
    };
    //取消删除
    operate.cancelDeleteItem = function () {
        item.OpSign = leeDataHandler.dataOpMode.add;
        deleteDialog.close();
    };
    //复制
    operate.copyItem = function (item) {
        var oldItem = _.clone(item);
        uiVm.Id = leeHelper.newGuid();
        uiVm = oldItem;
        uiVm.Id_Key = null;
        uiVm.ProductId = null;
        uiVm.ProductName = null;
        uiVm.ProductSpec = null;
        uiVm.ProductCount = null;
        uiVm.CustomerId = null;
        uiVm.CustomerName = null;
        uiVm.OpSign = leeDataHandler.dataOpMode.add;
        $scope.vm = uiVm;
        var dataItem = _.clone(uiVm);
        $scope.vm = uiVm = dataItem;
    };
    //保存
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
        uiVm.RmaId = rmavm.RmaId;
        vmManager.handleSalsesOrdersAndShipDate();
        console.log(uiVm);
        if (uiVm.RmaId === '' || uiVm.RmaId === null) return;
        leeDataHandler.dataOperate.add(operate, true, function () {
            rmaDataOpService.storeRmaInputDescriptionData(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var dataItem = _.clone(uiVm);
                        dataItem.Id_Key = opresult.Id_Key;
                        if (dataItem.OpSign === leeDataHandler.dataOpMode.add) {
                            vmManager.dataSets.push(dataItem);
                        }
                        if (dataItem.OpSign === leeDataHandler.dataOpMode.delete) {
                            deleteDialog.close();
                            leeHelper.delWithId(vmManager.dataSets, dataItem)//移除界面上数据
                        }
                        vmManager.init();
                    }
                });
            });
        });
    }
    //刷新
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () { vmManager.init(); });
    };
});
////检验处置
qualityModule.controller('rmaInspectionHandleCtrl', function ($scope, rmaDataOpService) {
    ///视图模型
    var rmavm = $scope.rmavm = {
        RmaId: null,
        ProductName: null,
        CustomerShortName: null
    };
    ///处理数据视图
    var uiVm = $scope.vm = {
        RmaId: null,
        RmaIdNumber: 0,
        RmaBussesesNumberStr: '',
        BadPhenomenon: null,
        BadDescription: null,
        BadReadson: null,
        HandleWay: null,
        ResponsiblePerson: null,
        FinishDate: null,
        PayTime: null,
        LiabilityBelongTo: null,
        HandleStatus: null,
        FilePath: null,
        FileName: null,
        ParameterKey: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null
    };
    var initVM = _.clone(uiVm);
    var vmManager = $scope.vmManager = {
        init: function () {
            uiVm = _.clone(initVM);
            uiVm.OpSign = leeDataHandler.dataOpMode.add;
            $scope.vm = uiVm;
        },
        ///首次活动的版
        activeTab: 'businessTab',
        //获取表单数据
        getRmaInspectionHandleDatas: function () {
            if (uiVm.RmaId === null || uiVm.RmaId === "") return;
            $scope.searchPromise = rmaDataOpService.getRmaInspectionHandleDatas(uiVm.RmaId).then(function (data) {
                if (angular.isObject(data)) {
                    ///把rmaInitiateData赋值到rmavm中
                    leeHelper.copyVm(data.rmaInitiateData, rmavm);
                    ///业务数据
                    vmManager.businessHandleDatas = data.bussesDescriptionDatas;
                    ///品保数据
                    vmManager.dataSets = data.inspectionHandleDatas;
                    /// 对已经处理
                    vmManager.isHanldlestatus();
                }
            });
        },
        businessHandleDatas: [],
        dataSets: [],
        businessHandleNumberDatas: [],
        selectedBusinessRmaNumberStr: [],
        isHanldlestatus: function () {
            if (vmManager.businessHandleDatas.length > 0 && vmManager.dataSets.length > 0) {
                angular.forEach(vmManager.businessHandleDatas, function (item) {
                    angular.forEach(vmManager.dataSets, function (Handleitem) {
                        var str = Handleitem.RmaBussesesNumberStr;
                        if (str == null || str.length == 0) return;
                        var stringstr = str.split(',');
                        stringstr.forEach(function (i) {
                            if (i == item.RmaIdNumber) {
                                item.isHandle = true;
                            }
                        });
                    });
                });
            }
        },
        isdisabled: false,
        dataitems: [],
    };
    var rmaNumberDatasDialog = $scope.rmaNumberDatasDialog = leePopups.dialog();
    var dialog = $scope.dialog = leePopups.dialog();
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    ///显示业务处理序号
    operate.getSelectedRmaIdData = function (item) {
        if (vmManager.businessHandleDatas.length > 0) {
            vmManager.selectedBusinessRmaNumberStr = [];
            vmManager.businessHandleNumberDatas = [];
            var dataitems = _.clone(vmManager.businessHandleDatas);
            angular.forEach(dataitems, function (item) {
                var dataItem = { value: item.RmaIdNumber, label: '序号:' + item.RmaIdNumber + '工单:' + item.ReturnHandleOrder };
                vmManager.businessHandleNumberDatas.push(dataItem);
            })
            dataitems = [];
        };
    };
    operate.handleItem = function (item) {
        var dataitem = _.clone(item);
        uiVm.ParameterKey = item.RmaId + "&" + item.ReturnHandleOrder + "&" + item.ProductId;
        dataitem.OpSign = leeDataHandler.dataOpMode.add;
        leeHelper.copyVm(dataitem, uiVm);
        $scope.vm = uiVm;
        dialog.show();

    };
    operate.editItem = function (item) {
        var dataitem = item;
        dataitem.OpSign = leeDataHandler.dataOpMode.edit;
        $scope.vm = uiVm = dataitem;
        dialog.show();
    };
    ///数组合并用逗号分开
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            if (vmManager.selectedBusinessRmaNumberStr.length > 0) {
                uiVm.RmaBussesesNumberStr = vmManager.selectedBusinessRmaNumberStr.join(',');
            }
            rmaDataOpService.storeRmaInspectionHandleDatas(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    if (opresult.Result) {
                        var dataItem = _.clone(uiVm);
                        dataItem.Id_Key = opresult.Id_Key;
                        if (dataItem.OpSign === leeDataHandler.dataOpMode.add) {
                            vmManager.dataSets.push(dataItem);
                        };
                        vmManager.isHanldlestatus();
                        vmManager.init();
                        dialog.close();
                    }
                });

            });
        });
    };
    // //上传附件
    $scope.selectFile = function (el) {
        leeHelper.upoadFile(el, function (fd) {
            var fileItem = uiVm;
            var fileAttachData = { RmaId: fileItem.RmaId, RmaIdNumber: fileItem.RmaBussesesNumberStr };
            fd.append('fileAttachData', JSON.stringify(fileAttachData));
            console.log(fd);
            rmaDataOpService.uploadRmaHandleFile(fd).then(function (datas) {
                if (datas.Result === 'OK') {
                    uiVm.FileName = datas.FileName;
                    uiVm.FilePath = datas.FullFileName;
                    vmManager.isdisabled = true;
                    alert("上传文件成功");
                }
            })
        });
    }
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () { vmManager.init(); });
    };
});
////检验Rma查询管理
qualityModule.controller('rmaReportQueryCtrl', function ($scope, rmaDataOpService) {
    var vmManager = $scope.vmManager = {
        init: function () {
            vmManager.dataSets = [];
            vmManager.dateFrom = null;
            vmManager.dateTo = null;
        },
        dataSource: [],
        //获取表单数据
        getRmaDatas: function () {
            if (vmManager.dateFrom === null || vmManager.dateTo === "") return;
            $scope.searchPromise = rmaDataOpService.queryRmaDatas(vmManager.searchFromYear, vmManager.searchToYear).then(function (datas) {
                vmManager.dataSets = datas;
                vmManager.dataSource = datas;
            });
        },
        ///首次活动的版
        activeTab: 'qualityTab',
        businessHandleDatas: [],
        dataSets: [],
        searchFromYear: '',
        searchToYear: '',
        getDetailDatas: function (item) {
            console.log(item);
            $scope.searchPromise = rmaDataOpService.getRmaInspectionHandleDatas(item.RmaId).then(function (data) {
                if (angular.isObject(data)) {
                    ///业务数据
                    vmManager.businessHandleDatas = data.bussesDescriptionDatas;
                    ///品保数据
                    vmManager.dataSets = data.inspectionHandleDatas;
                    /// 对已经处理
                }
            });
            console.log(vmManager.businessHandleDatas);
            console.log(vmManager.dataSets);
            dialog.show();
        },
    };
    var dialog = $scope.dialog = leePopups.dialog();
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    ///显示业务处理序号
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () { vmManager.init(); });
    };
});