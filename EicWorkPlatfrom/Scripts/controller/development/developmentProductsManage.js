
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
            SdId:null,
            SdPutInDate: null,
            SdCustomer: null,
            TriggerDevUnit: null,
            FillInPreparer: null,
            FillInCheckPreparer: null,
            AcceptSdPreparer: null,
            AcceptRdIdData: null,
            RdId: null,
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
            JsonSerializeModelDatas:null,
            OpPerson:null,
            OpDate:null,
            OpTime:null,
            OpSign:leeDataHandler.dataOpMode.add,
            Id_Key:null,
    }
    $scope.vm = uiVm;

    var vmManager = {
        activeTab: 'initTab',
        init: function () { },
        difficultyLevels: [{ id: 1, name: '难度低' }, { id: 2, name: '难度中' }, { id: 3, name: '难度高' }],
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
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
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    };

    //回车交点转下一个
    var $txtInput = $('input:text');
    $txtInput.first().focus();
    $txtInput.bind('keydown', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            var nxtIdx = $txtInput.index(this) + 1;              
            $txtInput.filter(":eq(" + nxtIdx + ")").focus();
        }
    });
});

developmentModule.controller("dponsorDocumentInputRecordCtrl", function ($scope, develpmentDataOpService, $modal) {
    ///录入开发部记录设计更变文档
    var uiVm = {
        SdId: null,
        SdPutInDate: null,
        SdCustomer: null,
        TriggerDevUnit: null,
        FillInPreparer: null,
        FillInCheckPreparer: null,
        AcceptSdPreparer: null,
        AcceptRdIdData: null,
        RdId: null,
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
        JsonSerializeModelDatas: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null,
    }
    $scope.vm = uiVm;

    var vmManager = {
        activeTab: 'initTab',
        init: function () { },
        difficultyLevels: [{ id: 1, name: '难度低' }, { id: 2, name: '难度中' }, { id: 3, name: '难度高' }],
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) {
        leeHelper.setUserData(uiVm);
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
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    };

    //回车交点转下一个
    var $txtInput = $('input:text');
    $txtInput.first().focus();
    $txtInput.bind('keydown', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            var nxtIdx = $txtInput.index(this) + 1;
            $txtInput.filter(":eq(" + nxtIdx + ")").focus();
        }
    });
});
///