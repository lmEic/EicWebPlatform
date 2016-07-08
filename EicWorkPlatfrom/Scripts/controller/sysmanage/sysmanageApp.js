/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.sysmanageApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap'])

.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {
    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);

    var configUrlPrefix = leeHelper.controllers.configManage + "/";

    var itilUrlPrefix = leeHelper.controllers.itilManage + "/";

    //--------------配置管理--------------------------
    $stateProvider.state('hrDepartmentSet', {
        templateUrl:configUrlPrefix + 'HrDepartmentSet',
    })
    .state('hrCommonDataSet', {
        templateUrl: configUrlPrefix + 'HrCommonDataSet',
    })
    //--------------账户管理--------------------------
    .state('accRegistUser', {
        templateUrl: 'Account/RegistUser'
    })
    .state('accAssignRoleToUser', {
        templateUrl: 'Account/AssignRoleToUser'
    })
     .state('sysModuleEdit', {
         templateUrl: 'Account/SysModuleEdit'
     })
    .state('sysRoleManage', {
        templateUrl: 'Account/SysRoleManage'
    })
     .state('sysAssemblyEdit', {
         templateUrl: 'Account/SysAssemblyEdit'
     })
     .state('assignPowerToRole', {
         templateUrl: 'Account/AssignPowerToRole'
     })
     .state('assignModuleToRole', {
         templateUrl: 'Account/AssignModuleToRole'
     })
    //--------------ITIL管理--------------------------
    .state('itilSupTelManage', {
        templateUrl: itilUrlPrefix + 'ItilSupTelManage',
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
.factory('hrDataOpService', function (ajaxService) {
    var hr = {};
    var archiveCtrl = "/HrArchivesManage/";
    var attendUrl = "/HrAttendanceManage/";
    ///获取身份证信息
    hr.getIdentityInfoBy = function (lastSixIdWord) {
        var url = archiveCtrl + "GetIdentityInfoBy";
        return ajaxService.getData(url, {
            lastSixIdWord: lastSixIdWord
        });
    };
    //获取作业工号
    hr.getWorkerIdBy = function (workerIdNumType) {
        var url = archiveCtrl + "GetWorkerIdBy";
        return ajaxService.getData(url, {
            workerIdNumType: workerIdNumType
        });
    };

    //获取作业人员信息
    hr.getWorkersInfo = function (vm, searchMode) {
        var url = archiveCtrl + "GetWorkersInfo";
        return ajaxService.getData(url, {
            workerId: vm.workerId,
            registedDateStart: vm.registedDateStart,
            registedDateEnd: vm.registedDateEnd,
            searchMode: searchMode
        });
    };

    ///获取档案信息配置数据
    hr.getArchiveConfigDatas = function () {
        var url = archiveCtrl + "GetArchiveConfigDatas";
        return ajaxService.getData(url, {});
    };

    ///获取请假配置信息
    hr.getLeaveTypesConfigs = function () {
        var url = attendUrl + "GetLeaveTypesConfigs";
        return ajaxService.getData(url, {});
    };

    //获取该工号列表的所有人员信息
    //mode:0为部门或岗位信息
    //1:为学习信息;2：为联系方式信息
    hr.getEmployeeByWorkerIds = function (workerIdList, mode) {
        var url = archiveCtrl + "FindEmployeeByWorkerIds";
        return ajaxService.getData(url, {
            workerIdList: workerIdList,
            mode: mode
        });
    };

    ///输入员工档案信息
    hr.inputWorkerArchive = function (employee, oldEmployeeIdentity, opSign) {
        var url = archiveCtrl + "InputWorkerArchive";
        return ajaxService.postData(url, {
            employee: employee,
            oldEmployeeIdentity: oldEmployeeIdentity,
            opSign: opSign
        });
    };

    ///修改部门信息
    hr.changeDepartment = function (changeDepartments) {
        var url = archiveCtrl + "ChangeDepartment";
        return ajaxService.postData(url, {
            changeDepartments: changeDepartments,
        });
    };

    ///修改岗位信息
    hr.changePost = function (changePosts) {
        var url = archiveCtrl + "ChangePost";
        return ajaxService.postData(url, {
            changePosts: changePosts,
        });
    };

    return hr;
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
    $scope.promise = navDataService.getSubModuleNavs('系统管理', 'EicSystemManage').then(function (datas) {
        moduleNavLayoutVm.menus = datas;
        moduleNavLayoutVm.navList = _.where(datas, { AtLevel: 2 });
    });
})
.controller('astArchiveInputCtrl', function ($scope) {
    ///设备档案模型
    var uiVM = {
        AssetNumber: null,
        EquipmentName: null,
        EquipmentSpec: null,
        FunctionDescription: null,
        ServiceLife: null,
        EquipmentPhoto: null,
        EquipmentType: null,
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
})