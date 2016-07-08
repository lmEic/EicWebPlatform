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

    //获取作业人员信息
    ast.getWorkersInfo = function (vm, searchMode) {
        var url = archiveCtrl + "GetWorkersInfo";
        return ajaxService.getData(url, {
            workerId: vm.workerId,
            registedDateStart: vm.registedDateStart,
            registedDateEnd: vm.registedDateEnd,
            searchMode: searchMode
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

.controller('astArchiveInputCtrl', function ($scope, dataDicConfigTreeSet,astDataopService) {
    ///设备档案模型
    var uiVM = {
        AssetNumber: null,
        EquipmentName: null,
        EquipmentSpec: null,
        FunctionDescription: null,
        ServiceLife: null,
        EquipmentPhoto: null,
        AssetType: null,
        EquipmentType: null,
        TaxType: null,
        Unit: null,
        Manufacturer: null,
        ManufacturingNumber: null,
        ManufacturerWebsite: null,
        ManufacturerTel: null,
        AfterSalesTel: null,
        AddMode: null,
        DeliveryDate: null,
        DeliveryUser: null,
        DeliveryCheckUser: null,
        SafekeepWorkerID: null,
        SafekeepUser: null,
        SafekeepDepartment: null,
        Installationlocation: null,
        IsMaintenance: null,
        MaintenanceDate: null,
        MaintenanceInterval: 0,
        PlannedMaintenanceDate: null,
        MaintenanceState: null,
        State: null,
        IsCheck: null,
        CheckDate: null,
        CheckInterval: 0,
        PlannedCheckDate: null,
        ChechState: null,
        OpPerson: null,
        OpSign: null,
        Id_Key: null,
    }

    $scope.vm = uiVM;

    var vmManager = {
        equTypes: [{ id: 0, text: '量测设备' }, { id: 1, text: '生产设备' }],
        taxTypes: [{ id: 0, text: '保税' }, { id: 1, text: '非保税' }],
        assetTypes: [{ id: 0, text: '固定资产' }, { id: 1, text: '低质易耗品' }],
        equUnits: [{ id: 0, text: '台' }, { id: 1, text: '个' }],
        departments: [],
        workerId: '',
        getEquipmentID: function () {
            uiVM.AssetNumber = uiVM.TaxType;
            //alert("1230");
        },
        getWorkerName: function () { },
    };
    $scope.vmManager = vmManager;

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
})