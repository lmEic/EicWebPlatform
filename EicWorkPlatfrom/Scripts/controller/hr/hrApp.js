/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />
angular.module('bpm.hrApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', 'angular-popups'])

.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //--------------人员管理--------------------------
    $stateProvider.state('workerInfoManage', {
        templateUrl: 'ProEmployee/WorkerInfoManage'

    }).state('proStationManage', {
        templateUrl: 'ProEmployee/ProStationManage'

    }).state('proClassManage', {
        templateUrl: 'ProEmployee/ProClassManage'

    }).state('workHoursManage', {
        templateUrl: 'ProEmployee/WorkHoursManage'
    })
    //--------------员工档案管理--------------------------
     .state('hrEmployeeDataInput', {
         templateUrl: 'HrArchivesManage/HrEmployeeDataInput'
     })
    .state('hrDepartmentChange', {
        templateUrl: 'HrArchivesManage/HrDepartmentChange'
    })
    .state('hrPostChange', {
        templateUrl: 'HrArchivesManage/HrPostChange'
    })
     .state('hrStudyManage', {
         templateUrl: 'HrArchivesManage/HrStudyManage'
     })
     .state('hrTelManage', {
         templateUrl: 'HrArchivesManage/HrTelManage'
     })
     .state('hrChangeWorkerId', {
         templateUrl: 'HrArchivesManage/HrChangeWorkerId'
     })
     .state('hrLeaveOffManage', {
         templateUrl: 'HrArchivesManage/HrLeaveOffManage'
     })
    //--------------档案业务管理--------------------------
     .state('hrPrintCard', {
         templateUrl: 'HrArchivesManage/HrPrintCard'
     })
    //--------------考勤业务管理--------------------------
     .state('hrClassTypeManage', {
         templateUrl: 'HrAttendanceManage/HrClassTypeManage'
     })
     .state('hrSumerizeAttendanceData', {
         templateUrl: 'HrAttendanceManage/HrSumerizeAttendanceData'
     })
     .state('hrAskLeaveManage', {
         templateUrl: 'HrAttendanceManage/HrAskLeaveManage'
     })
     //加班管理
     .state('hrWorkOverHoursManage', {
         templateUrl: 'HrAttendanceManage/HrWorkOverHoursManage'
     })
     .state('hrHandleException', {
         templateUrl: 'HrAttendanceManage/HrHandleException'
     })
    //--------------总务管理--------------------------
    //厂服管理
     .state('gaWorkerClothesManage', {
         templateUrl: 'HrGeneralAffairsManage/GaWorkerClothesManage'
     });
})


