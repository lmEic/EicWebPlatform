/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />

var productModule = angular.module('bpm.productApp');
productModule.factory('boardDataOpService', function (ajaxService) {
    var urlPrefix = "/" + leeHelper.controllers.productBoard + "/";
    var boardDataOp = {};
    ///上传物料看板文件
    boardDataOp.uploadMaterialBoardFile = function (file) {
        var url = urlPrefix + 'UploadMaterialBoardFile';
        return ajaxService.uploadFile(url,file);
    };
    ///检测物料料号是否与产品料号相匹配
    boardDataOp.checkMaterialIdMatchProductId = function (materialId, productId) {
        var url = urlPrefix + 'CheckMaterialIdMatchProductId';
        return ajaxService.postData(url, {
            materialId: materialId,
            productId: productId,
        });
    };
    ///添加物料看板规格记录
    boardDataOp.addMaterialSpecBoardRecord = function (model) {
        var url = urlPrefix + 'AddMaterialSpecBoardRecord';
        return ajaxService.postData(url, {
            model: model,
        });
    };
    return boardDataOp;
});
///线材看板
productModule.controller('jumperWireBoardCtrl', function ($scope, boardDataOpService,$modal) {
    ///线材看板视图模型
    var uiVM = {
        ProductID: null,
        MaterialID: null,
        DocumentPath: null,
        Remarks: null,
        Department:null,
        OpPerson: null,
        OpSign:'add',
        Id_Key: null,
    }
    $scope.vm = uiVM;
    var vmManager = {
        activeTab: 'initTab',
        init: function () {
            leeHelper.clearVM(uiVM);
        },
        isMatchProductId: function ($event) {
            if ($event.keyCode === 13)
            {
                boardDataOpService.checkMaterialIdMatchProductId(uiVM.ProductID, uiVM.MaterialID).then(function (opResult) {
                    if (!opResult.Result)
                    {
                        var msgModal = $modal({
                            title: "错误提示", content:opResult.Message, templateUrl: leeHelper.modalTplUrl.msgModalUrl, show: false
                        });
                        msgModal.$promise.then(msgModal.show);
                        uiVM.MaterialID = null;
                    }
                });
            }
        },
        datasets:[],
    };
    $scope.vmManager = vmManager;


    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            leeHelper.setUserData(uiVM);
            boardDataOpService.addMaterialSpecBoardRecord(uiVM).then(function (opresult) {
                var storeEntity = _.clone(uiVM);
                storeEntity.Id_Key = opresult.Id_Key;
                if (storeEntity.OpSign === 'add') {
                    vmManager.datasets.push(storeEntity);
                }
                vmManager.init();
            });
        })
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.inti();
        });
    };
    ///选择文件并预览
    $scope.selectFile = function (el) {
        var files = el.files;
        if (files.length > 0) {
            var file = files[0];
            var fd = new FormData();
            fd.append('file', file);
            boardDataOpService.uploadMaterialBoardFile(fd).then(function (result) {
                if (result) {
                    leeHelper.readFile('previewFile', file);
                    uiVM.DocumentPath = "FileLibrary/TwoMaterialBoard/" + file.name;
                }
                else {

                }
            });
        }
    };

    $scope.makePdfFile = function () {
        var dd = {
            info: {
                title: '物料看板',
                author:'ylee'
            },
            pageSize: 'A4',
            content: [
		          'pdfmake (since it\'s based on pdfkit) supports JPEG and PNG format',
		          'If no width/height/fit is provided, image original size will be used',
                  {
                      image:'/FileLibrary/TwoMaterialBoard/LocalStore.PNG', 
                  },
            ],
        };
        pdfMake.createPdf(dd).getDataUrl(function (outDoc) {
            document.getElementById('pdfV').src = outDoc;

        });
    };
   

    $scope.makePdfFile();
});