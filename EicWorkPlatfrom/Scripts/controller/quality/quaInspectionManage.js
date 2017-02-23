var quarityModule = angular.module('bpm.qualityApp');
quarityModule.factory("quarityDataOpService", function (ajaxService) {
    var quarity = {};
    var quarityUrlPrefix = " ";
    quarity.get
    return quarity;
})
quarityModule.controller("iqcInspectionItemCtrl", function ($scope, quarityDataOpService) {
    var uiVM = {
        MaterialId: null,
        Inspectionterm: null,
        InspectiontermNumber: 0,
        SizeUSL: null,
        SizeLSL: null,
        SizeMemo: null,
        EquipmentID: null,
        InspectionMethod: null,
        InspectionMode: null,
        InspectionLevel: null,
        InspectionAQL: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: null,
        Id_key: null,
    }
    $scope.vm = uiVM;
    var vmManager = {
        init: function () {
            leeHelper.clearVM(uiVM);
        }
    }
})