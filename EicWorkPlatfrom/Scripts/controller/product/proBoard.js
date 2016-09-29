/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
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
    ///获取待审核列表
    boardDataOp.getWaittingAuditBoardList = function () {
        var url = urlPrefix + 'GetWaittingAuditBoardList';
        return ajaxService.getData(url, {
        });
    };
    ///审核物料信息看板数据
    boardDataOp.auditMaterialBoardData = function (model) {
        var url = urlPrefix + 'AuditMaterialBoardData';
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
        DocumentUrl: null,
        DocumentPath: null,
        Remarks: null,
        Department: null,
        State: null,
        OpPerson: null,
        OpSign:'add',
        Id_Key: null,
    }
    $scope.vm = uiVM;

    var waittingAuditItem;

    var vmManager = {
        activeTab: 'initTab',
        init: function () {
            leeHelper.clearVM(uiVM);
        },
        isMatchProductId: function ($event) {
            if ($event.keyCode === 13) {
                boardDataOpService.checkMaterialIdMatchProductId(uiVM.ProductID, uiVM.MaterialID).then(function (opResult) {
                    if (!opResult.Result) {
                        var msgModal = $modal({
                            title: "错误提示", content: opResult.Message, templateUrl: leeHelper.modalTplUrl.msgModalUrl, show: false
                        });
                        msgModal.$promise.then(msgModal.show);
                        uiVM.MaterialID = null;
                    }
                });
            }
        },
        datasets: [],
        editDatas: [],
        ///获取待审核信息
        getWaittingCheckInfo: function () {
            $scope.searchPromise = boardDataOpService.getWaittingAuditBoardList().then(function (datas) {
                vmManager.editDatas = datas;
            });
        },
        aduitItem: function (item) {
            waittingAuditItem = item;
            vmManager.editModal.$promise.then(vmManager.editModal.show);
        },
        editModal: $modal({
            title: "物料看板审核", content: '',
            templateUrl: leeHelper.controllers.productBoard + '/AuditMaterailBoardTpl/',
            controller: function ($scope) {
                $scope.previewFileName = waittingAuditItem.DocumentPath;
                $scope.auditMaterialBoard = function () {
                    waittingAuditItem.OpSign = 'edit';
                    boardDataOpService.auditMaterialBoardData(waittingAuditItem).then(function (opresult) {
                        if (opresult.Result) {
                            vmManager.editModal.$promise.then(vmManager.editModal.hide);
                            leeHelper.remove(vmManager.editDatas, waittingAuditItem);
                        }
                    });
                };
            },
            show:false
        })
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
    //生成图片
    operate.createImage = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            var queryPara = "orderId=" + printVM.orderId + "&shippingDate=" + printVM.shippingDate + "&shippingCount=" + printVM.shippingCount;
            $scope.previewFileName = "ProBoard/GetMaterialSpecBoardBy?" + queryPara;
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
              
            });
        }
    };


    function convertImgToBase64(url, callback, outputFormat) {
        var img = new Image();
        img.crossOrigin = 'Anonymous';
        img.onload = function () {
            var canvas = document.createElement('CANVAS');
            var ctx = canvas.getContext('2d');
            canvas.height = this.height;
            canvas.width = this.width;
            ctx.drawImage(this, 0, 0);
            var dataURL = canvas.toDataURL(outputFormat || 'image/png');
            callback(dataURL);
            canvas = null;
        };
        img.src = url;
    };

    ///打印视图模型
    var printVM = {
        orderId:'',
        shippingDate:new Date(),
        shippingCount: '0',
        print: function () {
            var img = document.getElementById('imagePreview');
            printJS(img.src, "image");
        },//打印图片
    };

    $scope.printvm = printVM;
});