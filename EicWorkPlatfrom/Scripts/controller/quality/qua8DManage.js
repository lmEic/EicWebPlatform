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
        OrderId: null,
        MaterialName: null,
        MaterialSpec: null,
        InPutHouseOrder: null,
        MaterialCount: null,
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
    ///视图处理


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
    ///
    var uiVm = $scope.vm = {
        ReportId: null,
        StepId: 0,
        StepDescription: null,
        DescribeType: null,
        DescribeContent: null,
        FilePath: null,
        FileName: null,
        AboutDepartment: null,
        SignaturePeoples: null,
        ParameterKey: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: null,
        Id_Key: null,
    }
    var uiInitBaseInfoVm = $scope.baseInfoVm = {
        ReportId: null,
        DiscoverPosition: null,
        AccountabilityDepartment: null,
        OrderId: null,
        MaterialName: null,
        MaterialSpec: null,
        InPutHouseOrder: null,
        MaterialCount: null,
        InspectNumber: 0,
        FailQty: 0,
        FailClass: null,
    }
    var vmManager = {
        stepDisplay: false,
        // isCheck   selectStep  StepDescription
        steps: [step],
        selectStep: function (step) {

        },
    };
    $scope.vmManager = vmManager;
    var step = {
        isCheck: true,
        StepId: "M154545",
        StepDescription: "48798456468564",
        StepLevel: 7,
    };

});
////8D结案处理表单
qualityModule.controller('Colse8DFormCtrl', function ($scope, BDataOpService) {
    ///视图模型

});