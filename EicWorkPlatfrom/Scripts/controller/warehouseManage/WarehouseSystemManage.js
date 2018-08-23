/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var warehouseModule = angular.module('bpm.warehouseApp');
warehouseModule.factory("warehouseDataOpService", function (ajaxService) {
    var ware = {};
    var devUrlPrefix  = "/" + leeHelper.controllers.WarehouseManage+ "/";
    //保存档案记录 StoreExpressData
    ware.saverRecetionExpress = function (model) {
        var url = devUrlPrefix + 'StoreExpressData';
        return ajaxService.postData(url, {
            model: model
        });
    };
    return ware;
})
warehouseModule.controller("recetionExpressCtrl", function ($scope, warehouseDataOpService, connDataOpService, $modal) {
    ///录入开发部记录设计更变文档
    var uiVm = {
        ExpressId: '765465920545',
        ExpressCompany: null,
        Consignee: null,
        ReceptionDate: new Date(),
        SendGoodsCompanyAddress: null,
        GoodsNumber: 0,
        ReceiverWorkerId: null,
        ReceiverName: null,
        GetGoodsPerson: null,
        GetGoodsDate: new Date(),
        GoodssStatus: "已入库",
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null,
    }
    $scope.vm = uiVm;

    //查询字段
    var queryFields = {
        workerId: null,
        department: null,
        receiveMonth: null
    };

    $scope.query = queryFields;
    var vmManager = {
        init: function () {
            leeHelper.clearVM(uiVm);
            $scope.vm = uiVm;
        },
        expresssDatas: [],
        searchedWorkers: [],
        isLocal: true,
        isSingle: true,//是否搜寻到的是单个人
        isSingle2: true,
        isSingle3: true,
        canEdit:false,
        getWorkerInfo: function (workerIdorName,index) {
            if (workerIdorName === undefined) return;
            var strLen = leeHelper.checkIsChineseValue(workerIdorName) ? 2 : 6;
            if (workerIdorName.length >= strLen) {
                vmManager.searchedWorkers = [];
                $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(workerIdorName).then(function (datas) {
                    if (datas.length > 0) {
                        vmManager.searchedWorkers = datas;
                        if (vmManager.searchedWorkers.length === 1) {
                            switch (index) {
                                case 1:
                                    vmManager.isSingle = true;
                                    vmManager.selectWorker(vmManager.searchedWorkers[0]);
                                    break;
                                case 2:
                                    vmManager.isSingle2 = true;
                                    vmManager.selectWorker2(vmManager.searchedWorkers[0]);
                                    break;
                                case 3:
                                    vmManager.isSingle3 = true;
                                    vmManager.selectWorker3(vmManager.searchedWorkers[0]);
                                    break;
                                default:  break;
                            }; 
                        }
                        else {
                            switch (index) {
                                case 1:
                                    vmManager.isSingle = false;
                                    break;
                                case 2:
                                    vmManager.isSingle2 = false;
                                    break;
                                case 3:
                                    vmManager.isSingle3 = false;
                                    break;
                                default:  break;
                            }; 
                        }
                    }
                    else {
                        vmManager.selectWorker(null);
                        vmManager.selectWorker2(null);
                        vmManager.selectWorker3(null);
                    }
                });
            }
        },
        selectWorker: function (worker) {
            if (worker !== null) {
                uiVm.ReceiverName = worker.Name;
               // uiVM.SafekeepWorkerID = worker.WorkerId;
               // uiVM.SafekeepDepartment = worker.Department;
            }
            else {
                uiVm.ReceiverName = null;
                //uiVM.SafekeepWorkerID = null;
                //uiVM.SafekeepDepartment = null;
            }
        },
        selectWorker2: function (worker) {
            if (worker !== null) {
                uiVm.GetGoodsPerson = worker.Name;
                // uiVM.SafekeepWorkerID = worker.WorkerId;
                // uiVM.SafekeepDepartment = worker.Department;
            }
            else {
                uiVm.GetGoodsPerson = null;
                //uiVM.SafekeepWorkerID = null;
                //uiVM.SafekeepDepartment = null;
            }
        },
        selectWorker3: function (worker) {
            if (worker !== null) {
                uiVm.Consignee = worker.Name;
                // uiVM.SafekeepWorkerID = worker.WorkerId;
                // uiVM.SafekeepDepartment = worker.Department;
            }
            else {
                uiVm.Consignee = null;
                //uiVM.SafekeepWorkerID = null;
                //uiVM.SafekeepDepartment = null;
            }
        },
        selectExpress: function (item) {
            vmManager.canEdit = true;
            uiVm = _.clone(item);
            uiVm.OpSign = leeDataHandler.dataOpMode.edit;
            $scope.vm = uiVm;
        },
        searchBy: function () {
            $scope.searchPromise = hrDataOpService.getWorkerClothesReceiveRecords(queryFields.workerId, queryFields.department, queryFields.receiveMonth, 1).then(function (datas) {
                vmManager.storeDataset = datas;
            });
        },
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            warehouseDataOpService.saverRecetionExpress(uiVm).then(function (opresult) {
                console.log(opresult);
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    vmManager.init();
                });
                vmManager.expresss.push(opresult.entery);
            });
        });
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            console.log(99999);
            vmManager.init();
        });
    };

    //回车交点转下一个
    var $txtInput = $('input:text');
    $txtInput.first().focus();
    $txtInput.bind('keydown', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            var nxtIdx = $txtInput.index(this) + 1;
            $txtInput.filter(":eq(" + nxtIdx + ")").focus();
        }
    });
});


///