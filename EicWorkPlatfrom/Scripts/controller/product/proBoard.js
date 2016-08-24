/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var productModule = angular.module('bpm.productApp');
productModule.factory('boardDataOpService', function (ajaxService) {
    var urlPrefix = "/" + leeHelper.controllers.productBoard + "/";
    var boardDataOp = {};
    ///上传物料看板文件
    boardDataOp.uploadMaterialBoardFile = function (file) {
        var url = urlPrefix + 'UploadMaterialBoardFile';
        return ajaxService.uploadFile(url,file);
    };

    return boardDataOp;
});
///线材看板
productModule.controller('jumperWireBoardCtrl', function ($scope, boardDataOpService,$http) {
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
    };
    $scope.vmManager = vmManager;


    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) { };
    operate.refresh = function () { };
    ///选择文件并预览
    $scope.selectFile = function (el) {
        var files = el.files;
        if (files.length > 0) {
            var file = files[0];
            var fd = new FormData();
            fd.append('file', file);
            boardDataOpService.uploadMaterialBoardFile(fd).then(function () {
                leeHelper.readFile('previewFile', file);
            });
        }
    };
});