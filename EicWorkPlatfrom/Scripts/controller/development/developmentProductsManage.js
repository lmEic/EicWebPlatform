
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var developmentModule = angular.module('bpm.developmentApp');
developmentModule.factory("develpmentDataOpService", function (ajaxService) {
    var dev = {};
    var devUrlPrefix = "/development/";

    //保存档案记录
    dev.saveDevepmentRecord = function (model) {
        var url = devUrlPrefix + 'StoreDisgnDveData';
        return ajaxService.postData(url, {
            model: model
        });
    };
    return dev;
})
developmentModule.controller("developmentInputRecordCtrl", function ($scope, develpmentDataOpService, $modal) {
    ///录入开发部记录设计更变文档
    var uiVm ={
        RdId: 'NRD18026',
        SDId: 'NSD18026',
        SDPreparer: null,
        ProductName: null,
        ProductNameDescriptionAttachmentPath: null,
        ProductSpecDescription: null,
        ProductSpecDescriptionAttachmentPath: null,
        ExpectedProductionNumber: null,
        SampleDemandQuantity: null,
        DemandDate: null,
        IsHaveTheClassProduct: null,
        DevelopmentDifficultyLevel: null,
        ProductSpecAndEquipmentIsAllReady: null,
        ProductSpecAndEquipmentAllReadyDescription: null,
        IsNeedTryProduction: null,
        NeedTryProductionNumber: null,
        CertifiedScrumProductOwner: null,
        DesignatedPerson: null,
        ScheduledCompletionDays: null,
        TheCostEstimate: null,
        OtherSupplementDescription: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign:leeDataHandler.dataOpMode.add,
        Id_key: null,
    }
    $scope.vm = uiVm;

    var vmManager = {
        activeTab: 'initTab',
        init: function () { },
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) {
        console.log(99999);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            develpmentDataOpService.saveDevepmentRecord(uiVm).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    var equipment = _.clone(uiVm);
                    vmManager.init();
                });
            });
        });
    };
    operate.refresh = function () {
        console.log(11111);
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    };
});
///