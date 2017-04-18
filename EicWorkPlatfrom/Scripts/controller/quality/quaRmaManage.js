
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/underscore/underscore-min.js" />
var qualityModule = angular.module('bpm.qualityApp');
//数据访问工厂
qualityModule.factory("rmaDataOpService", function (ajaxService) {
    var rma = {};
    var quaInspectionManageUrl = "/quaInspectionManage/";
    ///////////////////////////////////////////////////iqc检验项目配置模块////////////////////////////////////////
    //iqc检验项目配置模块 物料查询
    rma.getIqcspectionItemConfigDatas = function (materialId) {
        var url = quaInspectionManageUrl + "GetIqcspectionItemConfigDatas";
        return ajaxService.getData(url, {
            materialId: materialId
        });
    };
    return rma;
})
qualityModule.controller('createRmaFormCtrl', function ($scope) {

});