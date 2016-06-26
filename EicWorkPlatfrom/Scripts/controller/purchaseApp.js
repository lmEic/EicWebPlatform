/// <reference path="../../Content/underscore/underscore.js" />
/// <reference path="../common/angulee.js" />
/// <reference path="../jquery-2.1.4.js" />
/// <reference path="../angular.js" />
angular.module('Erp.purchaseApp', ['eicomm.directive', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap'])
.config(function ($stateProvider,$urlRouterProvider) {
    $stateProvider.state('purchaseQuery', {
        url: 'Purchase/PurQuery',
        templateUrl: 'Purchase/PurQuery'
    })
})
.factory('purchaseService', function ($http,$q) {
    var purDb = {};
    //--------------requisition---------------------
    purDb.GetReqHeaderDatasBy = function (reqvm) {
        var defer = $q.defer();
        $http.get('/Purchase/FindReqHeaderDatas', {
            params: {
                department: reqvm.department,
                dateFrom: reqvm.dateFrom,
                dateTo:reqvm.dateTo
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    purDb.GetReqBodyDatasBy = function (reqvm) {
        var defer = $q.defer();
        $http.get('/Purchase/FindReqBodyDatas', {
            params: {
                department: reqvm.department,
                dateFrom: reqvm.dateFrom,
                dateTo: reqvm.dateTo
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    purDb.GetReqBodyDatasByID = function (requsitionID) {
        var defer = $q.defer();
        $http.get('/Purchase/FindReqBodyDatasByID', {
            params: {
                requsitionID:requsitionID
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    //--------------purchase--------------------------
    purDb.GetPurHeaderDatasBy = function (purvm) {
        var defer = $q.defer();
        $http.get('/Purchase/FindPurHeaderDatas', {
            params: {
                department: purvm.department,
                dateFrom: purvm.dateFrom,
                dateTo: purvm.dateTo
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    purDb.GetPurBodyDatasBy = function (purvm) {
        var defer = $q.defer();
        $http.get('/Purchase/FindPurBodyDatas', {
            params: {
                department: purvm.department,
                dateFrom: purvm.dateFrom,
                dateTo: purvm.dateTo
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    purDb.GetPurBodyDatasByID = function (purchaseID) {
        var defer = $q.defer();
        $http.get('/Purchase/FindPurBodyDatasByID', {
            params: {
                purchaseID: purchaseID
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    //--------------stock-----------------------------
    purDb.GetStoHeaderDatasBy = function (stovm) {
        var defer = $q.defer();
        $http.get('/Purchase/FindStoHeaderDatas', {
            params: {
                department: stovm.department,
                dateFrom: stovm.dateFrom,
                dateTo: stovm.dateTo
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    purDb.GetStoBodyDatasBy = function (stovm) {
        var defer = $q.defer();
        $http.get('/Purchase/FindStoBodyDatas', {
            params: {
                department: stovm.department,
                dateFrom: stovm.dateFrom,
                dateTo: stovm.dateTo
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    purDb.GetStoBodyDatasByID = function (stockID) {
        var defer = $q.defer();
        $http.get('/Purchase/FindStoBodyDatasByID', {
            params: {
                stockID: stockID
            }
        }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    return purDb;
})
//----------------------purchase-------------------------
.controller('purchaseQueryCtrl', function ($scope) {
    $scope.navmenu = {
        getMenuItems: function () {
            var menuItems = [
               { templateId: 'RequisitionQueryTemplate', menuText: '请购单查询', },
               { templateId: 'PurchaseQueryTemplate', menuText: '采购单查询', },
               { templateId: 'StockQueryTemplate', menuText: '进货单查询', },
            ];
            return menuItems;
        },
        templateId: "RequisitionQueryTemplate",
    };
})
.controller("reqQueryCtrl", function ($scope,$aside,purchaseService) {
    var reqVm = {
        department: 'EIC',
        dateFrom: '2015-12-01',
        dateTo:'2016-02-20',
    };
    $scope.vm = reqVm;

    var requisition = {
        datasource:[],
        reqHeaders: [],
        reqBodys: [],
        reqBodyDetails:[]
    };
    $scope.req = requisition;

    var operate = {
        asideBoard: $aside({
            title: "请购单记录明细",
            content: "",
            scope: $scope,
            templateUrl: "/Purchase/PurchaseBodyTpl",
            placement: "right",
            animation: "am-slide-right",
            container: "body",
            show: false
        }),
        bodyDetailsTemplateId: 'requisitionDetailsTemplate',
        showReqBodyDatasBoard: function () {
            $scope.reqBodyPromise = purchaseService.GetReqBodyDatasBy(reqVm).then(function (datas) {
                requisition.reqBodyDetails = datas;
            });
            operate.asideBoard.$promise.then(function () {
                operate.asideBoard.show();
            })
        },
        selectedRow:null,
        getReqHeaderDatas: function () {
            $scope.promise = purchaseService.GetReqHeaderDatasBy(reqVm).then(function (datas) {
                requisition.datasource = _.clone(datas);
            });
        },
        viewBodyDatas: function (row) {
            operate.selectedRow = row;
            $scope.reqBodyPromise = purchaseService.GetReqBodyDatasByID(row.BuyingID).then(function (datas) {
                requisition.reqBodys = datas;
            })
        }
    };
   
    $scope.operate = operate;
})
.controller("purQueryCtrl", function ($scope, $aside, purchaseService) {
    var purVm = {
        department: 'EIC',
        dateFrom: '2015-12-01',
        dateTo: '2016-02-20',
    };
    $scope.vm = purVm;

    var purchase = {
        datasource: [],
        purHeaders: [],
        purBodys: [],
        purBodyDetails: []
    };
    $scope.pur = purchase;

    var operate = {
        asideBoard: $aside({
            title: "采购单记录明细",
            content: "",
            scope: $scope,
            templateUrl: "/Purchase/PurchaseBodyTpl",
            placement: "right",
            animation: "am-slide-right",
            container: "body",
            show: false
        }),
        bodyDetailsTemplateId: 'purchaseDetailsTemplate',
        showPurBodyDatasBoard: function () {
            $scope.purBodyPromise = purchaseService.GetPurBodyDatasBy(purVm).then(function (datas) {
                purchase.purBodyDetails = datas;
            });
            operate.asideBoard.$promise.then(function () {
                operate.asideBoard.show();
            })
        },
        selectedRow: null,
        getPurHeaderDatas: function () {
            $scope.promise = purchaseService.GetPurHeaderDatasBy(purVm).then(function (datas) {
                purchase.datasource = _.clone(datas);
            });
        },
        viewBodyDatas: function (row) {
            operate.selectedRow = row;
            $scope.purBodyPromise = purchaseService.GetPurBodyDatasByID(row.PurchaseID).then(function (datas) {
                purchase.purBodys = datas;
            })
        }
    };

    $scope.operate = operate;
})
.controller("stoQueryCtrl", function ($scope, $aside, purchaseService) {
    var stoVm = {
        department: 'EIC',
        dateFrom: '2015-12-01',
        dateTo: '2016-02-20',
    };
    $scope.vm = stoVm;

    var stock = {
        datasource: [],
        stoHeaders: [],
        stoBodys: [],
        stoBodyDetails: []
    };
    $scope.sto = stock;

    var operate = {
        asideBoard: $aside({
            title: "进货单记录明细",
            content: "",
            scope: $scope,
            templateUrl: "/Purchase/PurchaseBodyTpl",
            placement: "right",
            animation: "am-slide-right",
            container: "body",
            show: false
        }),
        bodyDetailsTemplateId: 'stockDetailsTemplate',
        showStoBodyDatasBoard: function () {
            $scope.stoBodyPromise = purchaseService.GetStoBodyDatasBy(stoVm).then(function (datas) {
                stock.stoBodyDetails = datas;
            });
            operate.asideBoard.$promise.then(function () {
                operate.asideBoard.show();
            })
        },
        selectedRow: null,
        getStoHeaderDatas: function () {
            $scope.promise = purchaseService.GetStoHeaderDatasBy(stoVm).then(function (datas) {
                stock.datasource = _.clone(datas);
            });
        },
        viewBodyDatas: function (row) {
            operate.selectedRow = row;
            $scope.stoBodyPromise = purchaseService.GetStoBodyDatasByID(row.StockID).then(function (datas) {
                stock.stoBodys = datas;
            })
        }
    };

    $scope.operate = operate;
})