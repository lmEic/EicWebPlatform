/// <reference path="../angular.js" />
angular.module('eicomm.directive', ['ngSanitize', 'mgcrea.ngStrap'])
//月份控件
.directive('ylMonthButton', function () {
    return {
        restrict: 'EA',
        templateUrl: '/CommonTpl/MonthButtonTpl',
        replace: false,
        scope: {
            yearmonth: '=',//年月属性
            titleVisible: '='//是否显示标题
        },
        link: function (scope, element, attrs) {
            scope.months = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'];
            scope.titleVisible = true;
            var mydate = new Date();
            scope.currentYear = mydate.getFullYear();

            var cmonth = mydate.getMonth() + 1;
            scope.currentMonth = cmonth >= 10 ? cmonth.toString() : '0' + cmonth.toString();

            scope.yearmonth = scope.currentYear + scope.currentMonth;

            scope.selectMonth = function (m) {
                cmonth = m;
                scope.currentMonth = cmonth >= 10 ? cmonth.toString() : '0' + cmonth.toString();
                scope.yearmonth = scope.currentYear + scope.currentMonth;
            };

            scope.upYear = function () {
                scope.currentYear += 1;
                scope.yearmonth = scope.currentYear + scope.currentMonth;
            };
            scope.downYear = function () {
                scope.currentYear -= 1;
                scope.yearmonth = scope.currentYear + scope.currentMonth;
            };
        }
    };
})
//季度控件
.directive('ylQuarterButton', function () {
    return {
        restrict: 'EA',
        templateUrl: '/CommonTpl/QuarterButtonTpl',
        replace: false,
        scope: {
            yearquarter: '=',//年季度属性
            titleVisible: '='//是否显示标题
        },
        link: function (scope, element, attrs) {
            scope.titleVisible = true;
            scope.quarters = ['1', '2', '3', '4'];
            var mydate = new Date();
            scope.currentYear = mydate.getFullYear();
            //当前月份
            var cmonth = mydate.getMonth();

            var getCurrentQuarter = function () {
                return scope.currentYear.toString() + "0" + scope.currentQuarter.toString();
            };
            //当前季度
            scope.currentQuarter = parseInt(Math.floor(cmonth % 3 === 0) ? (cmonth / 3) : (cmonth / 3 + 1));

            scope.yearquarter = getCurrentQuarter();

            scope.selectQuarter = function (q) {
                scope.currentQuarter = q;
                scope.yearquarter = getCurrentQuarter();
            };

            scope.upYear = function () {
                scope.currentYear += 1;
                scope.yearquarter = getCurrentQuarter();
            };
            scope.downYear = function () {
                scope.currentYear -= 1;
                scope.yearquarter = getCurrentQuarter();
            };
        }
    };
})
.directive('formatDate', function ($filter) {
    return {
        require: 'ngModel',
        link: function (scope, elem, attr, ngModelCtrl) {
            ngModelCtrl.$formatters.push(function (modelValue) {
                if (modelValue) {
                    return new Date(modelValue);
                }
            });

            ngModelCtrl.$parsers.push(function (value) {
                if (value) {
                    return $filter('date')(value, 'yyyy-MM-dd');
                }
            });
        }
    };
})
.directive('formatTime', function ($filter) {
    return {
        require: 'ngModel',
        link: function (scope, elem, attr, ngModelCtrl) {
            ngModelCtrl.$formatters.push(function (modelValue) {
                if (modelValue) {
                    return new Date(modelValue);
                }
            });

            ngModelCtrl.$parsers.push(function (value) {
                if (value) {
                    return $filter('date')(value, 'HH:mm');
                }
            });
        }
    };
})
.directive('ylDateRange', function () {
    return {
        restrict: 'EA',
        templateUrl: '/CommonTpl/DateRangeTpl',
        replace: false,
        scope: {
            dateFrom: '=',//起始日期
            dateTo: '='//结束日期
        },
        link: function (scope, element, attrs) {
            scope.dateFrom = new Date;
            scope.dateTo = new Date();
        }
    };
})
.directive('ensureUserExist', function (connDataOpService) {
    return {
        require: '^ngModel',
        link: function (scope, element, attrs, ngModel) {
            var checkUserIsExist = function (bool) {
                ngModel.$setValidity('checkingUserExist', bool);
            };
            ngModel.$parsers.push(function (val) {
                if (val === undefined) return;
                var strLen = leeHelper.checkIsChineseValue(val) ? 2 : 6;
                if (val.length < strLen) {
                    return;
                }
                checkUserIsExist(false);
                connDataOpService.getWorkersBy(val).then(function (datas) {
                    if (datas.length > 0) {
                        checkUserIsExist(true);
                    }
                    else {
                        checkUserIsExist(false);
                    }
                }, function (errordata) {
                    checkUserIsExist(false);
                });
                return val;
            });
        }
    };
})
.directive('ylOperatemsgBoard', function () {
    return {
        restrict: 'EA',
        templateUrl: '/CommonTpl/OperateMsgBoardTpl',
        replace: false,
        scope: true,
        link: function (scope, element, attrs) {

        }
    };
})
.directive('ylSortbyField', function () {
    return {
        restrict: 'EA',
        template: '<span class="fa fa-arrow-circle-up text-info" ng-show="sortAscDirection" ng-click="sortBy(true)"></span>' +
                  '<span class="fa fa-arrow-circle-down text-info" ng-hide="sortAscDirection" ng-click="sortBy(false)"></span>',
        replace: false,
        scope: {
            dbset: '=',
            sortField: '@'
        },
        link: function (scope, element, attr) {
            //排序方向
            scope.sortAscDirection = true;
            //排序
            scope.sortBy = function (isAsc) {
                scope.dbset = leeHelper.sortArrOfObjectsByPtName(scope.dbset, scope.sortField, isAsc);
                scope.sortAscDirection = !isAsc;
            };
        }
    };
})
.directive('ylEditCommandbar', function ($modal) {
    return {
        restrict: 'EA',
        template: '<div class="btn-group btn-group-xs" ng-hide="rowItem.Id_Key === editItem.Id_Key">' +
                      '<span class="btn btn-primary" ng-click="editOperate.edit(rowItem)">修改</span>' +
                      '<span class="divider"></span>' +
                      '<span class="btn btn-danger" ng-click="editOperate.deleteop(rowItem)">删除</span>' +
                  '</div>' +
                  '<div class="btn-group btn-group-xs" ng-show="rowItem.Id_Key === editItem.Id_Key">' +
                      '<span class="btn btn-success" ng-click="editOperate.save()">保存</span>' +
                      '<span class="divider"></span>' +
                      '<span class="btn btn-warning" ng-click="editOperate.cancel()">取消</span>' +
                  '</div>',
        replace: false,
        scope: {
            rowItem: '=',
            editItem: '=',
            operateEdit: '&',
            operateCancel: '&',
            operateDel: '&',
            operateSave: '&'
        },
        link: function (scope, element, attrs) {
            var editOperate = {
                deleteModal: $modal({
                    title: "删除提示",
                    content: "你确定要删除数据吗?",
                    templateUrl: "/Account/DeleteConfirmModalTpl",
                    controller: function ($scope) {
                        $scope.confirmDelete = function () {
                            if (scope.operateDel(editOperate.deleteItem)) {
                                editOperate.deleteModal.$promise.then(editOperate.deleteModal.hide);
                            }
                        };
                    },
                    show: false
                }),
                deleteItem: null,
                edit: function (mdl) {
                    scope.editItem = mdl;
                    scope.operateEdit(mdl);
                },
                cancel: function () {
                    scope.editItem = null;
                    scope.operateCancel();
                },
                deleteop: function (mdl) {
                    editOperate.deleteItem = mdl;
                    scope.editItem = null;
                    editOperate.deleteModal.$promise.then(editOperate.deleteModal.show);
                },
                save: function () {
                    scope.operateSave();
                }
            };
            scope.editOperate = editOperate;
        }
    };
})
///图片预览按钮
.directive('ylImagePreview', function ($modal) {
    return {
        restrict: 'A',
        replace: false,
        scope: {
            fileSrc: '=src'
        },
        link: function (scope, element, attrs) {
            var imagePreviewModal = $modal({
                templateUrl: '/CommonTpl/ImageFilePreviewTpl',
                controller: function ($scope) {
                    $scope.fileSrc = scope.fileSrc;
                },
                show: false
            });

            element.bind('click', function () {
                imagePreviewModal.$promise.then(imagePreviewModal.show);
            });
        }
    };
})
.directive('ylOperatesignFilterbutton', function () {
    return {
        restrict: 'EA',
        replace: false,
        template: '<button type="button" class="btn btn-xs btn-primary" data-animation="am-flip-x" bs-dropdown aria-haspopup="true" aria-expanded="false">' +
                      '<i class="fa fa-flag"></i> ' +
                  '</button>' +
                  '<ul class="dropdown-menu" role="menu">' +
                      '<li><a ng-click="setFilterFieldAll()"><i class="fa fa-table"></i></a></li>' +
                      '<li><a ng-click="setFilterFieldInit()"><i class="fa fa-star"></i></a></li>' +
                      '<li><a ng-click="setFilterFieldAdd()"><i class="fa fa-plus-square"></i></a></li>' +
                      '<li><a ng-click="setFilterFieldEdit()"><i class="fa fa-edit"></i></a></li>' +
                  '</ul>',
        scope: {
            filterField: '='
        },
        link: function (scope, element, attrs) {
            scope.setFilterFieldAll = function () {
                scope.filterField = '';
            };
            scope.setFilterFieldInit = function () {
                scope.filterField = 'init';
            };
            scope.setFilterFieldAdd = function () {
                scope.filterField = 'add';
            };
            scope.setFilterFieldEdit = function () {
                scope.filterField = 'edit';
            };
        }
    };
})
.directive('ensureUserUnique', function (ajaxService) {
    return {
        require: '^ngModel',
        link: function (scope, element, attrs, ngModel) {
            var setAsChecking = function (bool) {
                ngModel.$setValidity('checkingAvailability', !bool);
            };
            var setAsUserName = function (bool) {
                ngModel.$setValidity('usernameAvailability', bool);
            };
            ngModel.$parsers.push(function (val) {
                if (!val || val.length <= 5) {
                    return;
                }
                setAsChecking(true);
                setAsUserName(false);

                ajaxService.getData('/Account/GetUserById', {
                    userId: val
                }).then(function (data) {
                    if (angular.isObject(data)) {
                        setAsChecking(false);
                        setAsUserName(false);
                    } else {
                        setAsChecking(false);
                        setAsUserName(true);
                    }
                }, function (errordata) {
                    setAsChecking(false);
                    setAsUserName(true);
                });
                return val;
            });
        }
    };
})
//-------------设置焦点移动-------------------------
.directive('focusSetter', function ($timeout) {
    return {
        restrict: 'A',
        scope: {
            trigger: '=focusIf'
        },
        link: function (scope, element, attrs) {
            var dom = element[0];
            scope.$watch('trigger', function (value) {
                if (value === true) {
                    $timeout(function () {
                        dom.focus();
                        scope.trigger = false;
                    }, scope.$eval(attrs.focusDelay) || 0);
                }
            });
        }
    };
})
//------------------pagination directive--------------------------
.directive('ylPagination', [function () {
    return {
        restrict: 'EA',
        template: '<div class="page-list">' +
            '<ul class="pagination" ng-show="config.totalItems > 0">' +
            '<li ng-class="{disabled: config.currentPage == 1}" ng-click="prevPage()"><span>&laquo;</span></li>' +
            '<li ng-repeat="item in config.pageList track by $index" ng-class="{active: item == config.currentPage, separate: item == \'...\'}" ' +
            'ng-click="config.changeCurrentPage(item)">' +
            '<span>{{ item }}</span>' +
            '</li>' +
            '<li ng-class="{disabled: config.currentPage == config.numberOfPages}" ng-click="nextPage()"><span>&raquo;</span></li>' +
            '</ul>' +
            '<div class="page-total" ng-show="config.totalItems > 0">' +
            //'第<input type="text" ng-model="jumpPageNum"  ng-keyup="jumpToPage($event)"/>页 ' +
            //'每页<select ng-model="config.itemsPerPage" ng-options="option for option in config.perPageOptions " ng-change="changeItemsPerPage()"></select>' +
            '<button class="btn btn-sm btn-info" type="button">总记录数：<span class="badge">{{ config.totalItems }}</span>' +
            '</button></div>' +
            '</div>',
        replace: true,
        scope: {
            config: '=',
            datasource: '=',
            dataset: '='
        },
        link: function (scope, element, attrs) {
            var defaultItemsPerPage = 8;
            var defaultPagesLength = 9;

            var config = {
                totalItems: 0,//总记录数
                currentPage: 1,//当前页数
                pagesList: [],//页数列表
                itemsPerPage: defaultItemsPerPage,//每页显示记录数
                numberOfPages: 0,//页的数量
                pagesLength: defaultPagesLength//默认页的数量
            };
            scope.config = config;
            // 变更当前页
            config.changeCurrentPage = function (page) {
                var d;
                if (page === '...') {
                    return;
                } else {
                    config.currentPage = page;
                }
                if (config.currentPage <= 1) {
                    d = _.clone(scope.datasource);
                    scope.dataset = d.splice(0, config.itemsPerPage);
                }
                else {
                    var startNumerPages = (config.currentPage - 1) * config.itemsPerPage;
                    d = _.clone(scope.datasource);
                    scope.dataset = d.splice(startNumerPages, config.itemsPerPage);
                }
            };
            // 定义分页的长度必须为奇数 (default:9)
            scope.config.pagesLength = parseInt(scope.config.pagesLength) ? parseInt(scope.config.pagesLength) : defaultPagesLength;
            if (scope.config.pagesLength % 2 === 0) {
                // 如果不是奇数的时候处理一下
                scope.config.pagesLength = scope.config.pagesLength - 1;
            }
            // config.erPageOptions
            if (!scope.config.perPageOptions) {
                scope.config.perPageOptions = [10, 15, 20, 30, 50];
            }
            // prevPage
            scope.prevPage = function () {
                if (config.currentPage > 1) {
                    config.currentPage -= 1;
                }
                config.changeCurrentPage(config.currentPage);
            };
            // nextPage
            scope.nextPage = function () {
                if (config.currentPage < config.numberOfPages) {
                    config.currentPage += 1;
                }
                config.changeCurrentPage(scope.config.currentPage);
            };

            // 跳转页
            scope.jumpToPage = function () {
                scope.jumpPageNum = scope.jumpPageNum.replace(/[^0-9]/g, '');
                if (scope.jumpPageNum !== '') {
                    scope.config.currentPage = scope.jumpPageNum;
                }
            };

            // 修改每页显示的条数
            scope.changeItemsPerPage = function () {
                // 清除本地存储的值方便重新设置
                if (scope.config.rememberPerPage) {
                    localStorage.removeItem(scope.config.rememberPerPage);
                }
            };

            //获取 pageList数组
            function getPagination() {
                // config.currentPage
                config.currentPage = parseInt(config.currentPage) ? parseInt(config.currentPage) : 1;
                // config.totalItems
                config.totalItems = parseInt(config.totalItems);

                // config.itemsPerPage (default:5)
                // 先判断一下本地存储中有没有这个值
                if (config.rememberPerPage) {
                    if (!parseInt(localStorage[config.rememberPerPage])) {
                        localStorage[config.rememberPerPage] = parseInt(config.itemsPerPage) ? parseInt(config.itemsPerPage) : defaultItemsPerPage;
                    }
                    config.itemsPerPage = parseInt(localStorage[config.rememberPerPage]);
                } else {
                    config.itemsPerPage = parseInt(config.itemsPerPage) ? parseInt(config.itemsPerPage) : defaultItemsPerPage;
                }
                // numberOfPages
                config.numberOfPages = Math.ceil(config.totalItems / config.itemsPerPage);

                // judge currentPage > scope.numberOfPages
                if (config.currentPage < 1) {
                    config.currentPage = 1;
                }
                if (config.currentPage > config.numberOfPages) {
                    config.currentPage = config.numberOfPages;
                }

                // jumpPageNum
                scope.jumpPageNum = config.currentPage;

                // 如果itemsPerPage在不在perPageOptions数组中，就把itemsPerPage加入这个数组中
                var perPageOptionsLength = scope.config.perPageOptions.length;
                // 定义状态
                var perPageOptionsStatus;
                for (var i = 0; i < perPageOptionsLength; i++) {
                    if (scope.config.perPageOptions[i] === scope.config.itemsPerPage) {
                        perPageOptionsStatus = true;
                    }
                }
                // 如果itemsPerPage在不在perPageOptions数组中，就把itemsPerPage加入这个数组中
                if (!perPageOptionsStatus) {
                    scope.config.perPageOptions.push(scope.config.itemsPerPage);
                }

                // 对选项进行sort
                scope.config.perPageOptions.sort(function (a, b) { return a - b; });
                config.pageList = [];

                if (config.numberOfPages <= config.pagesLength) {
                    // 判断总页数如果小于等于分页的长度，若小于则直接显示
                    for (i = 1; i <= config.numberOfPages; i++) {
                        config.pageList.push(i);
                    }
                } else {
                    // 总页数大于分页长度（此时分为三种情况：1.左边没有...2.右边没有...3.左右都有...）
                    // 计算中心偏移量
                    var offset = (config.pagesLength - 1) / 2;
                    if (config.currentPage <= offset) {
                        // 左边没有...
                        for (i = 1; i <= offset + 1; i++) {
                            config.pageList.push(i);
                        }
                        config.pageList.push('...');
                        config.pageList.push(config.numberOfPages);
                        //右边没有
                    } else if (config.currentPage > config.numberOfPages - offset) {
                        config.pageList.push(1);
                        config.pageList.push('...');
                        for (i = offset + 1; i >= 1; i--) {
                            config.pageList.push(config.numberOfPages - i);
                        }
                        config.pageList.push(config.numberOfPages);
                    } else {
                        // 最后一种情况，两边都有...
                        config.pageList.push(1);
                        config.pageList.push('...');
                        for (i = Math.ceil(offset / 2) ; i >= 1; i--) {
                            config.pageList.push(config.currentPage - i);
                        }
                        config.pageList.push(config.currentPage);
                        for (i = 1; i <= offset / 2; i++) {
                            config.pageList.push(config.currentPage + i);
                        }
                        config.pageList.push('...');
                        config.pageList.push(config.numberOfPages);
                    }
                }

                if (scope.config.onChange) {
                    scope.config.onChange();
                }
                scope.$parent.config = scope.config;
            }

            scope.$watch(function () {
                var newValue = scope.config.currentPage + ' ' + scope.config.totalItems + ' ';
                // 如果直接watch perPage变化的时候，因为记住功能的原因，所以一开始可能调用两次。
                //所以用了如下方式处理
                if (scope.config.rememberPerPage) {
                    // 由于记住的时候需要特别处理一下，不然可能造成反复请求
                    // 之所以不监控localStorage[scope.config.rememberPerPage]是因为在删除的时候会undefind
                    // 然后又一次请求
                    if (localStorage[scope.config.rememberPerPage]) {
                        newValue += localStorage[scope.config.rememberPerPage];
                    } else {
                        newValue += scope.config.itemsPerPage;
                    }
                } else {
                    newValue += scope.config.itemsPerPage;
                }
                return newValue;

            }, getPagination);

            scope.$watch('datasource', function () {
                if (!angular.isUndefined(scope.datasource)) {
                    if (scope.datasource.length > 0) {
                        config.totalItems = scope.datasource.length;
                        config.changeCurrentPage(1);
                    }
                }
            });
        }
    };
}])
//-------------------ztree directive------------------------------
.directive('ylTree', function () {
    return {
        restrict: 'A',
        require: '?ngModel',
        scope: {
            startLoad: '=',
            treeSetting: '=',
            treeDataset: '=',
            bindingNodevm: '&'
        },
        link: function (scope, element, attrs, ngModel) {
            scope.nodeCheck = false;

            var zTreeOnClick = function (event, treeId, treeNode) {
                scope.$apply(function () {
                    ngModel.$setViewValue(treeNode);
                    scope.bindingNodevm();
                });
            };

            var defaultSetting = {
                callback: {
                    onClick: zTreeOnClick
                },
                data: {
                    simpleData: {
                        enable: false
                    }
                }
            };

            var setting = null;
            if (angular.isUndefined(scope.treeSetting)) {
                setting = defaultSetting;
            }
            else {
                setting = scope.treeSetting;
            }

            scope.loadTree = function () {
                var id = '#' + element.context.id;
                var zNodes = scope.treeDataset;
                var zTreeObj = $.fn.zTree.init($(id), setting, zNodes);
            };

            scope.$watch('startLoad', function () {
                if (scope.startLoad) {
                    scope.loadTree();
                }
            });
        }
    };
})
.directive('ylCheckTree', function () {
    return {
        restrict: 'A',
        require: '?ngModel',
        scope: {
            startLoad: '=',
            treeSetting: '=',
            treeDataset: '=',
            bindingNodevm: '&',
            onCheck: '&'
        },
        link: function (scope, element, attrs, ngModel) {
            scope.nodeCheck = false;

            var zTreeOnClick = function (event, treeId, treeNode) {
                scope.$apply(function () {
                    ngModel.$setViewValue(treeNode);
                    scope.bindingNodevm();
                });
            };
            var zTreeOnCheck = function (event, treeId, treeNode) {
                scope.$apply(function () {
                    ngModel.$setViewValue(treeNode);
                    scope.bindingNodevm();
                    scope.onCheck();
                });
            };

            var defaultSetting = {
                callback: {
                    onClick: zTreeOnClick,
                    onCheck: zTreeOnCheck

                },
                data: {
                    simpleData: {
                        enable: false
                    },
                    key: {
                        checked: "isChecked"
                    }
                },
                check: {
                    enable: true,
                    chkboxType: { "Y": "", "N": "" }
                }
            };

            var setting = null;
            if (angular.isUndefined(scope.treeSetting)) {
                setting = defaultSetting;
            }
            else {
                setting = scope.treeSetting;
            }

            scope.loadTree = function () {
                var id = '#' + element.context.id;
                var zNodes = scope.treeDataset;
                var zTreeObj = $.fn.zTree.init($(id), setting, zNodes);
            };

            scope.$watch('startLoad', function () {
                if (scope.startLoad) {
                    scope.loadTree();
                }
            });
        }
    };
})
//--------------------------checkbox-------------------------------------
.directive('ylCheckbox', function () {
    return {
        restrict: "EA",
        replace: false,
        templateUrl: '/CommonTpl/CheckboxTpl',
        scope: {
            content: '@',
            onChecked: '&',
            isChecked: '='
        },
        link: function (scope, element, attr) {
            if (attr.size === undefined || attr.size.length < 1)
                attr.size = 'small';
            switch (attr.size) {
                case 'small':
                    scope.checkedClass = "fa fa-check-square-o";
                    scope.unCheckedClass = "fa  fa-square-o";
                    break;
                case 'middle':
                    scope.checkedClass = "fa fa-2x fa-check-square-o";
                    scope.unCheckedClass = "fa fa-2x fa-square-o";
                    scope.checkedSize = "2em";
                    break;
                case 'large':
                    scope.checkedClass = "fa fa-4x fa-check-square-o";
                    scope.unCheckedClass = "fa fa-4x fa-square-o";
                    scope.checkedSize = "3em";
                    break;
            }
            scope.showCheckbox = false;
            scope.content = attr.content;
            scope.selected = function () {
                scope.showCheckbox = false;
                scope.onChecked();
                scope.isChecked = false;
            };
            scope.unselected = function () {
                scope.showCheckbox = true;
                scope.onChecked();
                scope.isChecked = true;
            };
        }
    };
})
 //--------------------------uploadFile-------------------------------------
.directive('ylUploadFile', function () {
    return {
        restrict: "EA",
        replace: false,
        templateUrl: '/CommonTpl/UploadFileTpl',
        scope: true,
        link: function (scope, element, attr) {

        }
    };
})
 //--------------------------uploadFile-------------------------------------
.directive('ylSwitchButton', function () {
    return {
        restrict: "EA",
        replace: false,
        templateUrl: '/CommonTpl/SwitchButtonTpl',
        scope: {
            toggle: '&',
            switched: '='
        },
        link: function (scope, element, attr) {
            if (!scope.isSelectAll)
                scope.isSelectAll = false;
            scope.selectAll = function () {
                scope.toggle();
                scope.switched = !scope.switched;
                scope.isSelectAll = !scope.isSelectAll;
            };
        }
    };
})
//-----------------filter--------------------------------
.filter('unique', function () {
    return function (data, propertyName) {
        if (angular.isArray(data) && angular.isString(propertyName)) {
            var results = [];
            var keys = [];
            for (var i = 0; i < data.length; i++) {
                var val = data[i][propertyName];
                if (angular.isUndefined(keys[val])) {
                    keys[val] = true;
                    results.push(val);
                }
            }
            return results;
        }
        else { return data; }
    };
})
.filter('range', function ($filter) {
    return function (data, page, size) {
        if (angular.isArray(data) && angular.isNumber(page) && angular.isNumber(size)) {
            var start_index = (page - 1) * size;
            if (data.length < start_index) {
                return [];
            }
            else {
                return $filter("limitTo")(data.splice(start_index), size);
            }
        }
        else {
            return data;
        }
    };
})
.filter("pageCount", function () {
    return function (data, size) {
        if (angular.isArray(data)) {
            var result = [];
            var len = Math.ceil(data.length / size);
            for (var i = 0; i < len; i++) {
                result.push(i);
            }
            return result;
        }
        else {
            return data;
        }
    };
})
//-----------------factory------------------------------
.factory('ajaxService', function ($http, $q) {
    var myajax = {};
    myajax.getData = function (url, para) {
        var defer = $q.defer();
        $http.get(url, { params: para }).success(function (datas) {
            defer.resolve(datas);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    myajax.postData = function (url, para) {
        var defer = $q.defer();
        $http.post(url, para).success(function (data) {
            defer.resolve(data);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    //上传单个文件
    myajax.uploadFile = function (url, para) {
        var defer = $q.defer();
        $http.post(url, para, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined },
        }).success(function (data) {
            defer.resolve(data);
        }).error(function (errdata) {
            defer.reject(errdata);
        });
        return defer.promise;
    };
    return myajax;
})
.factory('navDataService', function (ajaxService) {
    var nav = {};
    nav.getModuleNavs = function () {
        return ajaxService.getData('/Home/GetModuleNavList', {});
    };
    nav.getSubModuleNavs = function (moduleText, cacheKey) {
        return ajaxService.getData('/Home/GetSubModuleNavList', {
            moduleText: moduleText,
            cacheKey: cacheKey
        });
    };
    return nav;
})
.factory('connDataOpService', function (ajaxService) {
    var conn = {};

    var urlPrefix = {
        hrArchivesManage: '/HrArchivesManage/',
        configManage: '/SysConfig/',
        commonTpl: '/CommonTpl/'
    };
    //获取部门数据
    conn.getDepartments = function () {
        var url = urlPrefix.hrArchivesManage + "GetDepartments";
        return ajaxService.getData(url, {});
    };
    //获取作业人员信息
    conn.getWorkersBy = function (workerIdOrName) {
        var url = urlPrefix.hrArchivesManage + 'GetWorkersBy';
        return ajaxService.getData(url, {
            workerIdOrName: workerIdOrName
        });
    };
    ///根据树模块键值获取配置数据
    conn.getConfigDicData = function (treeModuleKey) {
        var url = urlPrefix.configManage + "GetConfigDicData";
        return ajaxService.getData(url, {
            treeModuleKey: treeModuleKey
        });
    };
    ///根据模块名称与所属类别载入配置数据
    conn.loadConfigDicData = function (moduleName, aboutCategory) {
        var url = urlPrefix.configManage + "LoadConfigDicData";
        return ajaxService.getData(url, {
            moduleName: moduleName,
            aboutCategory: aboutCategory
        });
    };
    ///根据树的键值载入配置数据
    conn.saveConfigDicData = function (vm, oldVm, opType) {
        var url = urlPrefix.configManage + "SaveConfigDicData";
        return ajaxService.postData(url, {
            opType: opType,
            model: vm,
            oldModel: oldVm
        });
    };
    return conn;
});


