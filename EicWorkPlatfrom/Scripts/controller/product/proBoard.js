/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var productModule = angular.module('bpm.productApp');

productModule.controller('jumperWireBoardCtrl', function ($scope) {
    ///线材看板视图模型
    var uiVM = {
        ProductID: null,
        MaterialID: null,
        Drawing: null,
        Remarks: null,
        OpPerson: null,
        OpSign:'add',
        Id_Key: null,
    }
    $scope.vm = uiVM;
    var vmManager = {
        activeTab: 'initTab',
        uploadFile: function () {
            //var files = $event.target.files;
            //if (files.length > 0)
            //{ }
            alert("123456");
        },
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };

});