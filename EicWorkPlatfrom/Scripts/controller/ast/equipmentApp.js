/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.astApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap'])

.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);

    var urlPrefix = leeHelper.controllers.equipment + "/";

    $stateProvider.state('astArchiveInput', {
        templateUrl: urlPrefix + 'AstArchiveInput',

    }).state('proStationConfig', {
        templateUrl: 'DailyReport/ProStationConfig',

    })
    //--------------人员管理--------------------------
    .state('workerInfoManage', {
        templateUrl: 'ProEmployee/WorkerInfoManage'

    }).state('proStationManage', {
        templateUrl: 'ProEmployee/ProStationManage'

    }).state('proClassManage', {
        templateUrl: 'ProEmployee/ProClassManage'

    }).state('workHoursManage', {
        templateUrl: 'ProEmployee/WorkHoursManage'
    })
    ////--------------基本配置管理--------------------------
    //.state('hrDepartmentSet', {
    //    templateUrl: 'HrBaseInfoManage/HrDepartmentSet',

    //})
    //.state('hrCommonDataSet', {
    //    templateUrl: 'HrBaseInfoManage/HrCommonDataSet',

    //})
    ////--------------员工档案管理--------------------------
    // .state('hrEmployeeDataInput', {
    //     templateUrl: 'HrArchivesManage/HrEmployeeDataInput',
    // })
    //.state('hrDepartmentChange', {
    //    templateUrl: 'HrArchivesManage/HrDepartmentChange',
    //})
    //.state('hrPostChange', {
    //    templateUrl: 'HrArchivesManage/HrPostChange',
    //})
    // .state('hrStudyManage', {
    //     templateUrl: 'HrArchivesManage/HrStudyManage',
    // })
    // .state('hrTelManage', {
    //     templateUrl: 'HrArchivesManage/HrTelManage',
    // })
    ////--------------档案业务管理--------------------------
    // .state('hrPrintCard', {
    //     templateUrl: 'HrArchivesManage/HrPrintCard',
    // })
    ////--------------考勤业务管理--------------------------
    // .state('hrClassTypeManage', {
    //     templateUrl: 'HrAttendanceManage/HrClassTypeManage',
    // })
    // .state('hrAttendInToday', {
    //     templateUrl: 'HrAttendanceManage/HrAttendInToday',
    // })
    // .state('hrAskLeaveManage', {
    //     templateUrl: 'HrAttendanceManage/HrAskLeaveManage',
    // })
    // .state('hrHandleException', {
    //     templateUrl: 'HrAttendanceManage/HrHandleException',
    // })
})
.factory('astDataopService', function (ajaxService) {
    var ast = {};
    var astUrlPrefix = "/" + leeHelper.controllers.equipment + "/";
    ///获取设备录入配置数据信息
    ast.getAstInputConfigDatas = function () {
        var url = astUrlPrefix + "GetAstInputConfigDatas";
        return ajaxService.getData(url, {});
    };
    //获取设备编号
    ast.getEquipmentID = function (equipmentType, assetType, taxType)
    {
        var url = astUrlPrefix + 'GetEquipmentID';
        return ajaxService.getData(url, {
            equipmentType: equipmentType,
            assetType: assetType,
            taxType: taxType,
        });
    };
    //保存设备档案记录
    ast.saveEquipmentRecord = function (equipment)
    {
        var url = astUrlPrefix + 'SaveEquipmentRecord';
        return ajaxService.postData(url, {
            equipment: equipment,
        });
    };
    return ast;
})

.controller('moduleNavCtrl', function ($scope, navDataService, $state) {
    ///模块导航布局视图对象
    var moduleNavLayoutVm = {
        menus: [],
        navList: [],
        navItems: [],
        navTo: function (navMenu) {
            moduleNavLayoutVm.navItems = [];
            angular.forEach(navMenu.Childrens, function (childNav) {
                var navItem = _.findWhere(moduleNavLayoutVm.menus, { Name: childNav.ModuleName, AtLevel: 3 });
                if (!angular.isUndefined(navItem)) {
                    moduleNavLayoutVm.navItems.push(navItem);
                }
            })
        },
        stateTo: function (navItem) {
            $state.go(navItem.UiSerf);
        },
    };
    $scope.navLayout = moduleNavLayoutVm;
    $scope.promise = navDataService.getSubModuleNavs('设备管理', 'EquipmentManage').then(function (datas) {
        moduleNavLayoutVm.menus = datas;
        moduleNavLayoutVm.navList = _.where(datas, { AtLevel: 2 });
    });
})

.controller('astArchiveInputCtrl', function ($scope, dataDicConfigTreeSet,connDataOpService, astDataopService,$popover) {
    ///设备档案模型
    var uiVM = {
        AssetNumber: null,
        EquipmentName: null,
        EquipmentSpec: null,
        FunctionDescription: null,
        ServiceLife: 10,
        EquipmentPhoto: null,
        AssetType: null,
        EquipmentType: null,
        TaxType: null,
        Unit: '台',
        Manufacturer: null,
        ManufacturingNumber: null,
        ManufacturerWebsite: null,
        ManufacturerTel: null,
        AfterSalesTel: null,
        AddMode: "外购",
        DeliveryDate: null,
        DeliveryUser: null,
        DeliveryCheckUser: null,
        SafekeepWorkerID: null,
        SafekeepUser: null,
        SafekeepDepartment: null,
        Installationlocation: null,
        IsMaintenance: null,
        MaintenanceDate: null,
        MaintenanceInterval: 1,
        PlannedMaintenanceDate: null,
        MaintenanceState: null,
        State: null,
        IsCheck: null,
        CheckDate: null,
        CheckInterval: 6,
        PlannedCheckDate: null,
        ChechState: null,
        OpPerson: null,
        OpSign: 'add',
        Id_Key: null,
    }


    $scope.vm = uiVM;

    var vmManager = {
        inti: function () {
            leeHelper.clearVM(uiVM);
            uiVM.OpSign = 'add';
            vmManager.canEdit = false;
        },
        canEdit: false,
        equTypes: [{ id: 0, text: '量测设备' }, { id: 1, text: '生产设备' }],
        taxTypes: [{ id: 0, text: '保税' }, { id: 1, text: '非保税' }],
        assetTypes: [{ id: 0, text: '固定资产' }, { id: 1, text: '低质易耗品' }],
        equUnits: [{ id: 0, text: '台' }, { id: 1, text: '个' }],
        addModes: [{ id: 0, text: '外购' }, { id: 1, text: '自制' }],
        checkTypes: [{ id: 0, text: '内校' }, { id: 1, text: '外校' }],
        departments: [],
        equipments:[],
        workerId: '',
        searchedWorkers:[],
        getAstId: function () {
            astDataopService.getEquipmentID(uiVM.EquipmentType, uiVM.AssetType, uiVM.TaxType).then(function (data) {
                uiVM.AssetNumber = data;
            });
        },
        getWorkerInfo: function () {
            $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(vmManager.workerId).then(function (datas) {
                vmManager.searchedWorkers = leeHelper.getWorkersAboutChangedDepartment(datas, vmManager.departments);
            });
        },
        selectWorker: function (worker)
        {
            uiVM.SafekeepUser = worker.Name;
        },
        selectEquipment: function (item) {
            vmManager.canEdit = true;
            uiVM = _.clone(item);
            uiVM.OpSign = 'edit';
            $scope.vm = uiVM;
        },
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //存储
    operate.saveAll = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            astDataopService.saveEquipmentRecord(uiVM).then(function (opresult) {
                    leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                        var equipment = _.clone(uiVM);
                        equipment.Id_Key = opresult.Id_Key;
                        if (equipment.OpSign === 'add') {
                            vmManager.equipments.push(equipment)
                        }
                        else if (equipment.OpSign == 'edit') {
                            var current = _.find(vmManager.equipments, { AssetNumber: equipment.AssetNumber });
                            if (current !== undefined)
                                leeHelper.copyVm(equipment, current);
                        }
                        vmManager.inti();
                    });
            });
        })
    }
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.inti();
        });
    }

    
    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        uiVM.SafekeepDepartment = dto.DataNodeText;
    };
    $scope.ztree = departmentTreeSet;

    $scope.promise = astDataopService.getAstInputConfigDatas().then(function (data) {
        vmManager.departments = data.departments;
        departmentTreeSet.setTreeDataset(vmManager.departments);
    });

    $scope.exportToExcel = function () {

    }
})