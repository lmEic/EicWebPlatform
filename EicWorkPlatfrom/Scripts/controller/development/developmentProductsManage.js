var developmentModule = angular.module('bpm.developmentApp');
developmentModule.factory("qualityInspectionDataOpService", function (ajaxService) {
    var development = {};
    var quaInspectionManageUrl = "/quaInspectionManage/";



    return development;
})
developmentModule.controller("developmentInputRecordCtrl", function ($scope, qualityInspectionDataOpService, $modal, $alert) {
    ///录入开发部记录设计更变文档
    var uiVm ={
        RdId: 'NRD18026',
        SDId: null,
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
        OpSign: null,
        Id_key: null,
    }

    $scope.vm = uiVm;
    var vmManager = {
    };

    $scope.vmManager = vmManager;
});
///