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
qualityModule.controller('create8DFormCtrl', function ($scope, BDataOpService, qualityInspectionDataOpService) {
    ///视图模型
    var uiVm = $scope.vm = {
        ReportId: null,
        DiscoverPosition: null,
        AccountabilityDepartment: null,
        ProductName: null,
        ProductSpec: null,
        InPutHouseOrder: null,
        BatchNumber: 0,
        InspectNumber: 0,
        FailQty: 0,
        FailClass: null,
        CreateReportDate: null,
        Status: null,
        OpPerson: null,
        OpDate: null,
        OpSign: null,
        OpTime: null,
        Id_Key: null,
    };
    $scope.vm = uiVm;
    //初始化原型
    var initVM = _.clone(uiVm);
    var vmManager = {


    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

});
////处理8D表单
qualityModule.controller('Handle8DFormCtrl', function ($scope, BDataOpService) {
    ///视图模型

});
////8D结案处理表单
qualityModule.controller('Colse8DFormCtrl', function ($scope, BDataOpService) {
    ///视图模型

});