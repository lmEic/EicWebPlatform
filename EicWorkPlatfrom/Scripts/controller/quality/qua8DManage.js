/// <reference path="quaInspectionManage.js" />

/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');
//数据访问工厂
qualityModule.factory("BDataOpService", function (ajaxService) {
    var bugd = {};
    var quabugDManageUrl = "/qua8DManage/";


    return bugd;
});
////创建8D表单
qualityModule.controller('create8DFormCtrl', function ($scope, rmaDataOpService) {
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
            if (uiVm.CustomerShortName == customerShortName.name)
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