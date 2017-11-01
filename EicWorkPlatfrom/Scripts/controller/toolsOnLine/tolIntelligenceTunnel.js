/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var iTunnelModule = angular.module('bpm.toolsOnlineApp');
iTunnelModule.factory('iTunnelOpService', function (ajaxService) {
    var iTunnelDataOp = {};
    var iTunnelDataOp = '/' + leeHelper.controllers.TolIntelligenceTunnel + '/';

    return iTunnelDataOp;
});