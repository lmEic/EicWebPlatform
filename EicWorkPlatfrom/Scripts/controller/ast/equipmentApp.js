/// <reference path="../../common/eloam.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />

angular.module('bpm.astApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap', "pageslide-directive"])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {
    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);

    var urlPrefix = leeHelper.controllers.equipment + "/";

    //设备信息展示
    $stateProvider.state('astEquipmentInfoView', {
        templateUrl: urlPrefix + 'AstEquipmentInfoView'
    })

    //设备档案登记
    $stateProvider.state('astArchiveInput', {
        templateUrl: urlPrefix + 'AstArchiveInput'
    })

    //设备档案总览
    .state('astArchiveOverview', {
        templateUrl: urlPrefix + 'AstArchiveOverview'
    })
         //设备报废登记
    .state('astScrapInput', {
        templateUrl: urlPrefix + 'AstScrapInput'
    })
    //--------------校验管理--------------------------
    .state('astBuildCheckList', {
        templateUrl: urlPrefix + 'AstBuildCheckList'
    })
    .state('astInputCheckRecord', {
        templateUrl: urlPrefix + 'AstInputCheckRecord',
        onExit: function () {
            //eloam.UnLoad();
        }
    })
    //--------------保养管理--------------------------
    .state('astBuildMaintenanceList', {
        templateUrl: urlPrefix + 'AstBuildMaintenanceList'
    })
    .state('astInputMaintenanceRecord', {
        templateUrl: urlPrefix + 'AstInputMaintenanceRecord',
        onExit: function () {
            eloam.UnLoad();
        }
    })

    //--------------维修管理--------------------------
     .state('astInputRepairRecord', {
         templateUrl: urlPrefix + 'AstInputRepairRecord'
     });
})
.factory('astDataopService', function (ajaxService) {
    var ast = {};
    var astUrlPrefix = "/" + leeHelper.controllers.equipment + "/";
    ///获取设备录入配置数据信息
    ast.getAstInputConfigDatas = function () {
        var url = astUrlPrefix + "GetAstInputConfigDatas";
        return ajaxService.getData(url, {});
    };
    ///根据录入日期查询设备档案资料
    ast.getEquipmentArchivesBy = function (inputDate, assetId, searchMode) {
        var url = astUrlPrefix + 'GetEquipmentArchivesBy';
        return ajaxService.getData(url, {
            inputDate: inputDate,
            assetId: assetId,
            searchMode: searchMode
        });
    };
    //获取设备编号
    ast.getEquipmentID = function (equipmentType, assetType, taxType) {
        var url = astUrlPrefix + 'GetEquipmentID';
        return ajaxService.getData(url, {
            equipmentType: equipmentType,
            assetType: assetType,
            taxType: taxType
        });
    };
    ///获取设备档案总览
    ast.getAstArchiveOverview = function () {
        var url =
            astUrlPrefix + 'GetAstArchiveOverview';
        return ajaxService.getData(url, {
        });
    };
    ///保存设备报废记录
    ast.storeAstScrapData = function (model) {
        var url = astUrlPrefix + 'StoreAstScrapData';
        return ajaxService.postData(url, {
            model: model
        });
    };
    //保存设备档案记录
    ast.saveEquipmentRecord = function (equipment) {
        var url = astUrlPrefix + 'SaveEquipmentRecord';
        return ajaxService.postData(url, {
            equipment: equipment
        });
    };
    ///获取设备校验清单
    ast.getAstCheckListByPlanDate = function (planDate) {
        var url = astUrlPrefix + 'GetAstCheckListByPlanDate';
        return ajaxService.getData(url, {
            planDate: planDate
        });
    };
    ///获取设备校验清单
    ast.getAstCheckListByAssetNumber = function (assetNumber) {
        var url = astUrlPrefix + 'GetAstCheckListByAssetNumber';
        return ajaxService.getData(url, {
            assetNumber: assetNumber
        });
    };
    ///保存设备校验记录
    ast.storeInputCheckRecord = function (model) {
        var url = astUrlPrefix + 'StoreInputCheckRecord';
        return ajaxService.postData(url, {
            model: model
        });
    };
    //获取设备保养清单
    ast.getAstMaintenanceListByPlanMonth = function (planMonth) {
        var url = astUrlPrefix + 'GetAstMaintenanceListByPlanMonth';
        return ajaxService.getData(url, {
            planMonth: planMonth
        });
    };

    //获取设备保养记录
    ast.getAstMaintenanceListByAssetNumber = function (assetNumber) {
        var url = astUrlPrefix + 'GetAstMaintenanceListByAssetNumber';
        return ajaxService.getData(url, {
            assetNumber: assetNumber
        });
    };


    ///保存设备保养记录
    ast.storeInputMaintenanceRecord = function (model) {
        var url = astUrlPrefix + 'StoreInputMaintenanceRecord';
        return ajaxService.postData(url, {
            model: model
        });
    };

    ast.handleFile = function (moduleName, fileName) {
        var url = astUrlPrefix + 'HandleFile';
        return ajaxService.getData(url, {
            moduleName: moduleName,
            fileName: fileName
        });
    };

    ///保存设备维修记录
    ast.storeAstRepairedData = function (model) {
        var url = astUrlPrefix + 'StoreAstRepairedData';
        return ajaxService.postData(url, {
            model: model
        });
    };

    //获取设备维修记录
    ast.getAstRepairListByAssetNumber = function (assetNumber) {
        var url = astUrlPrefix + 'GetAstRepairListByAssetNumber';
        return ajaxService.getData(url, {
            assetNumber: assetNumber
        });
    };

    //获取设备维修总览表
    ast.getEquipmentRepairedOverView = function () {
        var url = astUrlPrefix + 'GetEquipmentRepairedOverView';
        return ajaxService.getData(url, {
        });
    };
    //获取设备报废总览表
    ast.getEquipmentDiscardOverView = function () {
        var url = astUrlPrefix + 'GetEquipmentDiscardOverView';
        return ajaxService.getData(url, {
        });
    };

    //获取设备报废记录
    ast.getAstDiscardListByAssetNumber = function (assetNumber) {
        var url = astUrlPrefix + 'GetAstDiscardListByAssetNumber';
        return ajaxService.getData(url, {
            assetNumber: assetNumber
        });
    };

    //013935根据财产编号查询设备
    ast.getEquipmentRepairAssetNumberDatas = function (assetNumber) {
        var url = astUrlPrefix + 'GetEquipmentRepairAssetNumberDatas';
        return ajaxService.getData(url, {
            assetNumber:assstNumber
        })
    }
    //013935根据表单编号查询设备
    ast.getEquipmentRepairFormIdDatas = function (FormId) {
        var url = astUrlPrefix + 'getEquipmentRepairFormIdDatas';
        return ajaxService.getData(url, {
            FormId: FormId
        })
    }

    return ast;
})
.controller('moduleNavCtrl', function ($scope, navDataService, $state) {
    ///模块导航布局视图对象
    var moduleNavLayoutVm = {
        menus: [],
        navList: [],
        navItems: [],
        navTo: function (navMenu) {
            moduleNavLayoutVm.navItems = [];
            angular.forEach(navMenu.Childrens, function (childNav) {
                var navItem = _.findWhere(moduleNavLayoutVm.menus, { Name: childNav.ModuleName, AtLevel: 3 });
                if (!angular.isUndefined(navItem)) {
                    moduleNavLayoutVm.navItems.push(navItem);
                }
            });
        },
        stateTo: function (navItem) {
            $state.go(navItem.UiSerf);
        },
        navViewSwitch: true,//左侧视图导航开关
        switchView: function () {
            moduleNavLayoutVm.navViewSwitch = !moduleNavLayoutVm.navViewSwitch;
            if (moduleNavLayoutVm.navViewSwitch) {
                moduleNavLayoutVm.navLeftSize = '16%';
                moduleNavLayoutVm.navMainSize = '83%';
            }
            else {
                moduleNavLayoutVm.navLeftSize = '3%';
                moduleNavLayoutVm.navMainSize = '96%';
            }
        },
        navLeftSize: '16%',
        navMainSize: '83%'
    };
    $scope.navLayout = moduleNavLayoutVm;
    $scope.promise = navDataService.getSubModuleNavs('设备管理', 'EquipmentManage').then(function (datas) {
        moduleNavLayoutVm.menus = datas;
        moduleNavLayoutVm.navList = _.where(datas, { AtLevel: 2 });
    });
})
 //设备详细信息与各记录
.controller('astEquipmentInfoViewCtrl', function ($scope, dataDicConfigTreeSet, connDataOpService, astDataopService, $modal) {
        ///设备档案模型
        var uiVM = {
            AssetNumber: null,
            EquipmentName: null,
            EquipmentSpec: null,
            EquipmentType: null,
            AssetType: null,
            SafekeepDepartment: null,
            ManufacturingNumber: null,
            MaintenanceDate: null,
            CheckDate: null
        };

        $scope.vm = uiVM;

        var vmManager = {
            AssetNumber: null,
            PreviewFileName: null, //图片路径

            discardDataSource: [], //报废记录
            discardDataSets:[],

            checkRecordDataSource: [], //校验记录
            checkRecordDataSet: [],

            repairedDataSource: [], //维修记录
            repairedDataSet: [],

            maintenanceDataSource: [], //保养记录
            maintenanceDataSet: [],

            init: function () {
                leeHelper.clearVM(uiVM);
                $scope.vm = uiVM;
            },

            //mIndex ==1 回车调用  否则直接调用
            getAstDatas: function ($event, mthIndex) {
                if (mthIndex === 1 && $event.keyCode !== 13) return;
                if (vmManager.AssetNumber === null || vmManager.AssetNumber === undefined || vmManager.AssetNumber.length < 6) return;

                $scope.searchPromise = astDataopService.getEquipmentArchivesBy(new Date(), vmManager.AssetNumber, 1).then(function (datas) {
                    if (angular.isArray(datas) && datas.length > 0) {
                        leeHelper.copyVm(datas[0], uiVM);
                    } else {
                        leeHelper.clearVM(uiVM, null);
                    }
                });

                //校验记录
                vmManager.checkRecordDataSource = [];
                vmManager.checkRecordDataSet = [];
                $scope.searchPromise = astDataopService.getAstCheckListByAssetNumber(vmManager.AssetNumber).then(function (datas) {
                    vmManager.checkRecordDataSource = datas;
                });

                //保养记录
                vmManager.maintenanceDataSource = [];
                vmManager.maintenanceDataSet = [];
                $scope.searchPromise = astDataopService.getAstMaintenanceListByAssetNumber(vmManager.AssetNumber).then(function (datas) {
                    vmManager.maintenanceDataSource = datas;
                });

                //维修记录
                vmManager.repairedDataSource = [];
                vmManager.repairedDataSet = [];
                $scope.searchPromise = astDataopService.getAstRepairListByAssetNumber(vmManager.AssetNumber).then(function (datas) {
                    vmManager.repairedDataSource = datas;
                });

                //报废记录
                vmManager.discardDataSource = [];
                vmManager.discardDataSets = [];
                $scope.searchPromise = astDataopService.getAstDiscardListByAssetNumber(vmManager.AssetNumber).then(function (datas) {
                    vmManager.discardDataSource = datas;
                });
            },
        };

        $scope.vmManager = vmManager;


    })

///设备档案总览
.controller('astArchiveOverviewCtrl', function ($scope, astDataopService) {
    //视图管理器
    var vmManager = {
        activeTab: 'baseInfoTab',
        datasource: [],
        datasets: [],
        //设备校验
        checkDataSource: [],
        checkDataSets: [],
        //设备保养
        maintenanceDataSource: [],
        maintenanceDataSets: [],
        //维修
        repairDataSource: [],
        repairDataSets: [],
        //报废
        discardDataSource: [],
        discardDataSets:[],

        //设备信息总览表
        getAstArchiveOverview: function () {
            vmManager.datasets = [];
            vmManager.datasource = [],
            $scope.promise = astDataopService.getAstArchiveOverview().then(function (datas) {
                vmManager.datasource = datas;

                vmManager.checkDataSource = datas;

                vmManager.maintenanceDataSource = datas;
            });
        },

        //设备维修总览表
        getAstRepairOverView: function () {
            vmManager.repairDataSource = [];
            vmManager.repairDataSets = [];
            $scope.promise = astDataopService.getEquipmentRepairedOverView().then(function (datas) {
                vmManager.repairDataSource = datas;
            });
        },

        //获取报废总览表
        getAstDiscardOverView: function () {
            vmManager.discardDataSource = [];
            vmManager.discardDataSets = [];
            $scope.promise = astDataopService.getEquipmentDiscardOverView().then(function (datas) {
                vmManager.discardDataSource = datas;
            });
        },
        //校验查询
        isCheck: '',
        checkType: '',
        searchCheckDatas: function () {
            var db = _.clone(vmManager.datasource);
            if (vmManager.isCheck === '' && vmManager.checkType === '') {
                vmManager.checkDataSource = db;
            }
            else if (vmManager.isCheck !== '' && vmManager.checkType==='')
            {
                vmManager.checkDataSource = _.where(db, { IsCheck: vmManager.isCheck });
            }
            else if (vmManager.isCheck==='' && vmManager.checkType !== '') {
                vmManager.checkDataSource = _.where(db, { CheckType: vmManager.checkType });
            }
            else if(vmManager.isCheck!=='' && vmManager.checkType !=='')
            {
                vmManager.checkDataSource = _.where(db, { CheckType: vmManager.checkType, IsCheck: vmManager.isCheck });
            }
        },
        //保养查询
        isMaintenance: '',
        searchMaintenanceDatas: function () {
            var db = _.clone(vmManager.datasource);
            if (vmManager.isMaintenance === '') {
                vmManager.maintenanceDataSource = db;
            }
            else if (vmManager.isMaintenance !== '') {
                vmManager.maintenanceDataSource = _.where(db, { IsMaintenance: vmManager.isMaintenance });
            }
        },
        boardViewSize: '100%',
        //校验履历明细
        equCheckRecordDisplay: false,
        checkRecordDataSource: [],
        checkRecordDataSet: [],
        showEquCheckRecordWindow: function (item) {
            vmManager.equCheckRecordDisplay = true;
            vmManager.checkRecordDataSet = [];
            vmManager.checkRecordDataSource = [];
            $scope.searchPromise = astDataopService.getAstCheckListByAssetNumber(item.AssetNumber).then(function (datas) {
                vmManager.checkRecordDataSource = datas;
            });
        },
        //保养履历明细
        equMaintenanceRecordDisplay: false,
        maintenanceRecordDataSource: [],
        maintenanceRecordDataSet: [],
        showEquMaintenanceRecordWindow: function (item) {
            vmManager.maintenanceRecordDataSet = [];
            vmManager.maintenanceRecordDataSource = [];
            vmManager.equMaintenanceRecordDisplay = true;
            $scope.searchPromise = astDataopService.getAstMaintenanceListByAssetNumber(item.AssetNumber).then(function (datas) {
                vmManager.maintenanceRecordDataSource = datas;
            });
        },
    };
    $scope.vmManager = vmManager;


   


    vmManager.getAstArchiveOverview();
    vmManager.getAstRepairOverView();
    vmManager.getAstDiscardOverView();
})
///设备档案登记
.controller('astArchiveInputCtrl', function ($scope, dataDicConfigTreeSet, connDataOpService, astDataopService, $modal) {
    
    ///设备档案模型
    var uiVM = {
        AssetNumber: null,
        EquipmentName: null,
        EquipmentSpec: null,
        FunctionDescription: null,
        ServiceLife: 10,
        EquipmentPhoto: null,
        AssetType: '低质易耗品',
        EquipmentType: '量测设备',
        TaxType: null,
        FreeOrderNumber: null,
        DeclarationNumber: null,
        Unit: '台',
        Manufacturer: null,
        ManufacturingNumber: null,
        ManufacturerWebsite: null,
        ManufacturerTel: null,
        AfterSalesTel: null,
        AddMode: "外购",
        DeliveryDate: null,
        DeliveryUser: null,
        DeliveryCheckUser: null,
        SafekeepWorkerID: null,
        SafekeepUser: null,
        SafekeepDepartment: null,
        Installationlocation: null,
        IsMaintenance: null,
        MaintenanceDate: new Date(),
        MaintenanceInterval: 1,
        PlannedMaintenanceDate: null,
        MaintenanceState: null,
        State: null,
        IsCheck: null,
        CheckType: '内校',
        CheckDate: new Date(),
        CheckInterval: 6,
        PlannedCheckDate: null,
        ChechState: null,
        InputDate: null,
        OpDate: null,
        OpPerson: null,
        OpSign: 'add',
        Id_Key: null
    };

    $scope.vm = uiVM;

    var vmManager = {
        activeTab: 'initTab',
        init: function () {
            if (uiVM.OpSign === 'add') {
                uiVM.ManufacturingNumber = null;
            }
            else {
                leeHelper.clearVM(uiVM, ['AssetType', 'EquipmentType', 'ServiceLife', 'AddMode',
                  'Unit', 'MaintenanceDate', 'CheckType', 'CheckDate', 'MaintenanceInterval', 'CheckInterval']);
            }
            uiVM.OpSign = 'add';
            $scope.vm = uiVM;
            vmManager.canEdit = false;
        },
        canEdit: false,
        equTypes: [{ id: 0, text: '量测设备' }, { id: 1, text: '生产设备' }, { id: 2, text: "辅助设备" }],
        taxTypes: [{ id: 0, text: '保税' }, { id: 1, text: '非保税' }],
        assetTypes: [{ id: 0, text: '固定资产' }, { id: 1, text: '低质易耗品' }],
        equUnits: [{ id: 0, text: '台' }, { id: 1, text: '个' }],
        addModes: [{ id: 0, text: '外购' }, { id: 1, text: '自制' }],
        checkTypes: [{ id: 0, text: '内校' }, { id: 1, text: '外校' }],
        departments: [],
        equipments: [],
        workerId: '',
        searchedWorkers: [],
        getAstId: function () {
            astDataopService.getEquipmentID(uiVM.EquipmentType, uiVM.AssetType, uiVM.TaxType).then(function (data) {
                uiVM.AssetNumber = data;
            });
        },
        isSingle: true,//是否搜寻到的是单个人
        getWorkerInfo: function () {
            if (uiVM.SafekeepUser === undefined) return;
            var strLen = leeHelper.checkIsChineseValue(uiVM.SafekeepUser) ? 2 : 6;
            if (uiVM.SafekeepUser.length >= strLen) {
                vmManager.searchedWorkers = [];
                $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(uiVM.SafekeepUser).then(function (datas) {
                    if (datas.length > 0) {
                        vmManager.searchedWorkers = datas;
                        if (vmManager.searchedWorkers.length === 1) {
                            vmManager.isSingle = true;
                            vmManager.selectWorker(vmManager.searchedWorkers[0]);
                        }
                        else {
                            vmManager.isSingle = false;
                        }
                    }
                    else {
                        vmManager.selectWorker(null);
                    }
                });
            }
        },
        selectWorker: function (worker) {
            if (worker !== null) {
                uiVM.SafekeepUser = worker.Name;
                uiVM.SafekeepWorkerID = worker.WorkerId;
                uiVM.SafekeepDepartment = worker.Department;
            }
            else {
                uiVM.SafekeepWorkerID = null;
                uiVM.SafekeepDepartment = null;
            }
        },
        selectEquipment: function (item) {
            vmManager.canEdit = true;
            uiVM = _.clone(item);
            uiVM.OpSign = 'edit';
            $scope.vm = uiVM;
        },
        //-----------edit--------------
        inputDate: new Date(),//输入日期
        assetNum: null,
        editDatas: [],
        getAstDatas: function (searchMode) {
            vmManager.editDatas = [];
            $scope.searchPromise = astDataopService.getEquipmentArchivesBy(vmManager.inputDate, vmManager.assetNum, searchMode).then(function (datas) {
                vmManager.editDatas = datas;
            });
        }
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //存储
    operate.saveAll = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            astDataopService.saveEquipmentRecord(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    var equipment = _.clone(uiVM);
                    equipment.Id_Key = opresult.Id_Key;
                    if (equipment.OpSign === 'add') {
                        vmManager.equipments.push(equipment);
                        vmManager.getAstId();
                    }
                    else if (equipment.OpSign === 'edit') {
                        var current = _.find(vmManager.equipments, { AssetNumber: equipment.AssetNumber });
                        if (current !== undefined)
                            leeHelper.copyVm(equipment, current);
                    }
                    vmManager.init();
                });
            });
        });
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.inti();
        });
    };
    operate.editModal = $modal({
        title: "操作窗口",
        templateUrl: leeHelper.controllers.equipment + '/EditEquipmentTpl/',
        controller: function ($scope) {
            $scope.vm = uiVM;
            $scope.vmManager = vmManager;
            $scope.ztree = departmentTreeSet;
            var op = Object.create(leeDataHandler.operateStatus);
            $scope.operate = op;

            $scope.save = function (isValid) {
                uiVM.OpSign = 'edit';
                leeDataHandler.dataOperate.add(op, isValid, function () {
                    astDataopService.saveEquipmentRecord($scope.vm).then(function (opresult) {
                        var item = _.find(vmManager.editDatas, { Id_Key: uiVM.Id_Key });
                        if (angular.isDefined(item)) {
                            leeHelper.copyVm(uiVM, item);
                            vmManager.init();
                            operate.editModal.$promise.then(operate.editModal.hide);
                        }
                    });
                });
            };
        },
        show: false
    });

    operate.editItem = function (item) {
        uiVM = _.clone(item);
        operate.editModal.$promise.then(operate.editModal.show);
    };

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        uiVM.SafekeepDepartment = dto.DataNodeText;
    };
    $scope.ztree = departmentTreeSet;

    $scope.promise = astDataopService.getAstInputConfigDatas().then(function (data) {
        vmManager.departments = data.departments;
        departmentTreeSet.setTreeDataset(vmManager.departments);
    });
})
///报废登记 astScrapInputCtrl
.controller('astScrapInputCtrl', function ($scope, dataDicConfigTreeSet, connDataOpService, astDataopService, $modal) {
    ///设备保养记录模型
    var uiVM = {
        AssetNumber: null,
        EquipmentName: null,
        DiscardDate: new Date(),
        DiscardType: null,
        DiscardCause: null,
        DocumentId: null,
        DocumentPath: null,
        OpPerson: null,
        OpSign: 'add'
    };
    $scope.vm = uiVM;

    ///设备实体保养模型
    var equipmentVM = {
        AssetNumber: null,
        EquipmentName: null,
        EquipmentSpec: null,
        ManufacturingNumber: null
    };
    $scope.equipmentvm = equipmentVM;

    var vmManager = {
        init: function () {
            leeHelper.clearVM(uiVM, ['DiscardDate']);
        },
        discardTypes: [{ id: 1, text: "自然报废" }, { id: 2, text: "人为报废" }],
        datasets: [],
        //验证是否可以保存数据
        canSave: function () {
            ///验证内容
            if (angular.isUndefined(uiVM.AssetNumber) || uiVM.AssetNumber === null || angular.isUndefined(uiVM.DiscardDate) || uiVM.DiscardDate === null) {
                var msgModal = $modal({
                    title: "错误提示:报废日期或者财产编号不能为空！", content: "", templateUrl: leeHelper.modalTplUrl.msgModalUrl, show: false
                });
                msgModal.$promise.then(msgModal.show);
                return false;
            }
            else { return true; }
        },
        getAstDatas: function () {
            if (uiVM.AssetNumber === undefined || uiVM.AssetNumber.length < 6) return;
            $scope.searchPromise = astDataopService.getEquipmentArchivesBy(uiVM.DiscardDate, uiVM.AssetNumber, 1).then(function (datas) {
                if (angular.isArray(datas) && datas.length > 0) {
                    leeHelper.copyVm(datas[0], equipmentVM);
                }
            });
        }
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function () {
        if (!vmManager.canSave()) return;
        astDataopService.storeAstScrapData(uiVM).then(function (opresult) {
            leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                var storeEntity = opresult.Attach;
                if (Record !== null) {
                    if (storeEntity.OpSign === 'add') {
                        vmManager.datasets.push(storeEntity);
                    }
                }
                vmManager.init();
            });
        });
    };

    var previewImgPrefix = "Equscrap-";
    ///高拍仪控制对象
    var vedio = {
        previewFileName: '',
        open: function () { eloam.OpenVideo(); },
        close: function () { eloam.CloseVideo(); },
        rotateLeft: function () { eloam.RotateLeft(); },
        rotateRight: function () { eloam.RotateRight(); },
        saveAsImage: function () {
            if (!vmManager.canSave()) return;
            var loginUser = leeDataHandler.dataStorage.getLoginedUser();
            uiVM.OpPerson = loginUser.userName;
            var day = new Date();
            var random = day.getSeconds();
            var isServer = loginUser.serverName === "EICMAIN";
            var previewImgPath = isServer ? "PreviewFiles/" : "FileLibrary/PreviewFiles/";
            var serverHost = isServer ? "\\\\192.168.0.187\\" : loginUser.webSitePhysicalApplicationPath;
            var serverFilePath = (serverHost + previewImgPath).replace(/\//g, "\\");//服务器文件夹路径
            var fileName = previewImgPrefix + uiVM.AssetNumber + "-"+ random + ".jpg";
            eloam.SaveAsImg(serverFilePath + fileName);
            $scope.previewPromise = astDataopService.handleFile("EquScrapFiles", fileName).then(function (result) {
                if (result.exist) {
                    vedio.previewFileName = result.imgUrl;
                    uiVM.DocumentPath = "FileLibrary/EquScrapFiles/" + result.dateFile + "/" + uiVM.AssetNumber + ".jpg";
                }
            });
        }
    };
    $scope.vedio = vedio;
    $scope.$watch('$viewContentLoaded', function (event) {
        eloam.Load();
    });
    $scope.$on('$destroy', function () {
        eloam.UnLoad();
    });
})
///生成校验清单
.controller('astBuildCheckListCtrl', function ($scope, dataDicConfigTreeSet, connDataOpService, astDataopService, $modal) {
    //视图管理器
    var vmManager = {
        activeTab: 'initTab',
        planDate: new Date(),
        datasource: [],
        datasets: [],
        getAstCheckList: function () {
            vmManager.datasource = [];
            $scope.searchPromise = astDataopService.getAstCheckListByPlanDate(vmManager.planDate).then(function (datas) {
                vmManager.datasource = datas;
            });
        }
    };
    $scope.vmManager = vmManager;
})
///录入校验记录
.controller('astInputCheckRecordCtrl', function ($scope, connDataOpService, astDataopService, $modal) {
    ///登记校验记录
    var uiVM = {
        AssetNumber: null,
        EquipmentName: null,
        CheckDate: new Date(),
        DocumentPath: null,
        OpPerson: null,
        OpSign: 'add'
    };

    $scope.vm = uiVM;

    var initVm = _.clone(uiVM);

    ///设备实体校验模型
    var checkVM = {
        AssetNumber: null,
        EquipmentName: null,
        EquipmentSpec: null,
        ManufacturingNumber: null,
        SafekeepDepartment: null,
        IsCheck: null,
        CheckType: null,
        CheckDate: null,
        CheckInterval: 0,
        CheckState: null
    };
    $scope.checkvm = checkVM;

    var vmManager = {
        init: function () {
            uiVM = _.clone(initVm);
        },
        datasets: [],
        //验证是否可以保存数据
        canSave: function () {
            ///验证内容
            if (angular.isUndefined(uiVM.AssetNumber) || uiVM.AssetNumber === null || angular.isUndefined(uiVM.CheckDate) || uiVM.CheckDate === null) {
                var msgModal = $modal({
                    title: "错误提示:校验日期或者财产编号不能为空！", content: "", templateUrl: leeHelper.modalTplUrl.msgModalUrl, show: false
                });
                msgModal.$promise.then(msgModal.show);
                return false;
            }
            else { return true; }
        },
        getAstDatas: function () {
            if (uiVM.AssetNumber === undefined || uiVM.AssetNumber.length < 6) return;
            $scope.searchPromise = astDataopService.getEquipmentArchivesBy(uiVM.CheckDate, uiVM.AssetNumber, 1).then(function (datas) {
                if (angular.isArray(datas) && datas.length > 0) {
                    leeHelper.copyVm(datas[0], checkVM);
                }
            });
        }
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function () {
        if (!vmManager.canSave()) return;
        astDataopService.storeInputCheckRecord(uiVM).then(function (opresult) {
            leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                var checkRecord = opresult.Attach;
                if (checkRecord !== null) {
                    if (checkRecord.OpSign === 'add') {
                        vmManager.datasets.push(checkRecord);
                    }
                    vmManager.init();
                }
            });
        });
    };

    var previewImgPrefix = "Equcheck-";
    ///高拍仪控制对象
    var vedio = {
        previewFileName: '',
        open: function () { eloam.OpenVideo(); },
        close: function () { eloam.CloseVideo(); },
        rotateLeft: function () { eloam.RotateLeft(); },
        rotateRight: function () { eloam.RotateRight(); },
        saveAsImage: function () {
            if (!vmManager.canSave()) return;
            var loginUser = leeDataHandler.dataStorage.getLoginedUser();
            uiVM.OpPerson = loginUser.userName;
            var day = new Date();
            var random = day.getSeconds();
            var isServer = loginUser.serverName === "EICMAIN";
            var previewImgPath = isServer ? "PreviewFiles/" : "FileLibrary/PreviewFiles/";
            var serverHost = isServer ? "\\\\192.168.0.187\\" : loginUser.webSitePhysicalApplicationPath;
            var serverFilePath = (serverHost + previewImgPath).replace(/\//g, "\\");//服务器文件夹路径
            var fileName = previewImgPrefix + uiVM.AssetNumber + "-" + random + ".jpg";
            eloam.SaveAsImg(serverFilePath + fileName);
            $scope.previewPromise = astDataopService.handleFile("EqucheckFiles", fileName).then(function (result) {
                if (result.exist) {
                    vedio.previewFileName = result.imgUrl;
                    uiVM.DocumentPath = "FileLibrary/EqucheckFiles/" + result.dateFile + "/" + uiVM.AssetNumber + ".jpg";
                }
            });
        }
    };

    $scope.vedio = vedio;

    $scope.$watch('$viewContentLoaded', function (event) {
        eloam.Load();
    });
    $scope.$on('$destroy', function () {
        eloam.UnLoad();
    });
})
///生成保养清单
.controller('astBuildMaintenanceListCtrl', function ($scope, dataDicConfigTreeSet, connDataOpService, astDataopService, $modal) {
    //视图管理器
    var vmManager = {
        activeTab: 'initTab',
        planMonth: '',
        datasource: [],
        datasets: [],
        getAstMaintenanceList: function () {
            vmManager.datasource = [];
            $scope.searchPromise = astDataopService.getAstMaintenanceListByPlanMonth(vmManager.planMonth).then(function (datas) {
                vmManager.datasource = datas;
            });
        }
    };
    $scope.vmManager = vmManager;
})
///录入保养记录
.controller('astInputMaintenanceRecordCtrl', function ($scope, dataDicConfigTreeSet, connDataOpService, astDataopService, $modal) {
    ///设备保养记录模型
    var uiVM = {
        AssetNumber: null,
        EquipmentName: null,
        MaintenanceDate: new Date(),
        DocumentPath: null,
        OpPerson: null,
        OpSign: 'add'
    };
    $scope.vm = uiVM;

    var initVm = _.clone(uiVM);

    ///设备实体保养模型
    var equipmentVM = {
        AssetNumber: null,
        EquipmentName: null,
        EquipmentSpec: null,
        ManufacturingNumber: null,
        SafekeepDepartment: null,
        IsMaintenance: null,
        MaintenanceDate: null,
        MaintenanceInterval: 0,
        MaintenanceState: null
    };
    $scope.equipmentVM = equipmentVM;

    var vmManager = {
        init: function () {
            uiVM = _.clone(initVm);
        },
        datasets: [],
        //验证是否可以保存数据
        canSave: function () {
            ///验证内容
            if (angular.isUndefined(uiVM.AssetNumber) || uiVM.AssetNumber === null || angular.isUndefined(uiVM.MaintenanceDate) || uiVM.MaintenanceDate === null) {
                var msgModal = $modal({
                    title: "错误提示:设备保养日期或者财产编号不能为空！", content: "", templateUrl: leeHelper.modalTplUrl.msgModalUrl, show: false
                });
                msgModal.$promise.then(msgModal.show);
                return false;
            }
            else { return true; }
        },
        getAstDatas: function () {
            if (uiVM.AssetNumber === undefined || uiVM.AssetNumber.length < 6) return;
            $scope.searchPromise = astDataopService.getEquipmentArchivesBy(uiVM.MaintenanceDate, uiVM.AssetNumber, 1).then(function (datas) {
                if (angular.isArray(datas) && datas.length > 0) {
                    leeHelper.copyVm(datas[0], equipmentVM);
                }
            });
        }
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function () {
        if (!vmManager.canSave()) return;
        astDataopService.storeInputMaintenanceRecord(uiVM).then(function (opresult) {
            leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                var MaintenanceRecord = opresult.Attach;
                if (MaintenanceRecord !== null) {
                    if (MaintenanceRecord.OpSign === 'add') {
                        vmManager.datasets.push(MaintenanceRecord);
                    }
                }
                vmManager.init();
            });
        });
    };

    var previewImgPrefix = "Equmaintenance-";
    ///高拍仪控制对象
    var vedio = {
        previewFileName: '',
        open: function () { eloam.OpenVideo(); },
        close: function () { eloam.CloseVideo(); },
        rotateLeft: function () { eloam.RotateLeft(); },
        rotateRight: function () { eloam.RotateRight(); },
        saveAsImage: function () {
            if (!vmManager.canSave()) return;
            var loginUser = leeDataHandler.dataStorage.getLoginedUser();
            uiVM.OpPerson = loginUser.userName;
            var day = new Date();
            var random = day.getSeconds();
            var isServer = loginUser.serverName === "EICMAIN";
            var previewImgPath = isServer ? "PreviewFiles/" : "FileLibrary/PreviewFiles/";
            var serverHost = isServer ? "\\\\192.168.0.187\\" : loginUser.webSitePhysicalApplicationPath;
            var serverFilePath = (serverHost + previewImgPath).replace(/\//g, "\\");//服务器文件夹路径
            var fileName = previewImgPrefix + uiVM.AssetNumber + "-" + random + ".jpg";
            eloam.SaveAsImg(serverFilePath + fileName);
            $scope.previewPromise = astDataopService.handleFile("MaintenanceFiles", fileName).then(function (result) {
                if (result.exist) {
                    vedio.previewFileName = result.imgUrl;
                    uiVM.DocumentPath = "FileLibrary/MaintenanceFiles/" + result.dateFile + "/" + uiVM.AssetNumber + ".jpg";
                }
            });
        }
    };
    $scope.vedio = vedio;
    $scope.$watch('$viewContentLoaded', function (event) {
        eloam.Load();
    });
    $scope.$on('$destroy', function () {
        eloam.UnLoad();
    });
})

///录入设备维修单
.controller('astInputRepairedRecordCtrl', function ($scope, dataDicConfigTreeSet, connDataOpService, astDataopService, $modal) {
    $scope.test = function () {
        console.log(1);
    };
    ///设备档案模型
    var uiVM = {
        FormId: null,
        AssetNumber: null,
        EquipmentName: null,
        ManufacturingNumber: null,
        SafekeepDepartment: null,
        FilingDate: null,
        RepairedUser: null,
        FinishDate: null,
        RepairedResult: null,
        FaultDescription: null,
        Solution: null,
        OpDate: null,
        OpTime: null,
        OpPerson: null,
        OpSign: 'add',
        Id_Key: null
    };

    $scope.vm = uiVM;

    //视图管理器
    var vmManager = {

        init: function () {
            uiVM.OpSign = 'add';
            leeHelper.clearVM(uiVM);
            $scope.vm = uiVM;
        },

        searchedWorkers: [],
        isSingle: true,//是否搜寻到的是单个人

        checkTypes: [{ id: 0, text: '已修复' }, { id: 1, text: '未修复' }],

        getWorkerInfo: function () {
            if (uiVM.RepairedUser === undefined) return;
            var strLen = leeHelper.checkIsChineseValue(uiVM.RepairedUser) ? 2 : 6;
            if (uiVM.RepairedUser.length >= strLen) {
                vmManager.searchedWorkers = [];
                $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(uiVM.RepairedUser).then(function (datas) {
                    if (datas.length > 0) {
                        vmManager.searchedWorkers = datas;
                        if (vmManager.searchedWorkers.length === 1) {
                            vmManager.isSingle = true;
                            vmManager.selectWorker(vmManager.searchedWorkers[0]);
                        }
                        else {
                            vmManager.isSingle = false;
                        }
                    }
                    else {
                        vmManager.selectWorker(null);
                    }
                });
            }
        },
        selectWorker: function (worker) {
            if (worker !== null) {
                uiVM.RepairedUser = worker.Name;
            }
        },

        getAstDatas: function ($event) {
            if ($event.keyCode === 13) {//回车
                if (uiVM.AssetNumber === undefined || uiVM.AssetNumber.length < 6) return;
                $scope.searchPromise = astDataopService.getEquipmentArchivesBy(new Date(), uiVM.AssetNumber, 1).then(function (datas) {
                    if (angular.isArray(datas) && datas.length > 0) {
                        leeHelper.copyVm(datas[0], uiVM);
                    } else {
                        leeHelper.clearVM(uiVM, null);
                    }
                });
            }
        },

        //013935财产编号查询
        getEquipmentRepairAssetNumberDatas: function () {
            vmManager.editDatas = [];
            $scope.searchPromise = astDataopService.getEquipmentRepairAssetNumberDatas(vmManager.assetNumber).then(function (datas) {
                vmManager.editDatas = datas;
            });
        },
        //013935表单编号查询
        getEquipmentRepairFromIdDatas: function () {
            vmManager.editDatas = [];
            $scope.searchPromise = astDataopService.getEquipmentRepairFromIdDatas(vmManager.formId).then(function (datas) {
                vmManager.editDatas = datas;
            });
        },

    };

    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //存储
    operate.saveAll = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            astDataopService.storeAstRepairedData(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    vmManager.init();
                });
            });
        });
    };
    //013935创建设备维修编辑模态框
    operate.editModal = $modal({
        title: "操作窗口",
        templateUrl: leeHelper.controllers.equipment + '/EditEquipmentRepairTpl/',
        //controller: function ($scope) {
        //    $scope.vm = uiVM;
        //    $scope.vmManager = vmManager;
        //    $scope.ztree = departmentTreeSet;
        //    var op = Object.create(leeDataHandler.operateStatus);
        //    $scope.operate = op;

        //    $scope.save = function (isValid) {
        //        uiVM.OpSign = 'edit';
        //        leeDataHandler.dataOperate.add(op, isValid, function () {
        //            astDataopService.saveEquipmentRecord($scope.vm).then(function (opresult) {
        //                var item = _.find(vmManager.editDatas, { Id_Key: uiVM.Id_Key });
        //                if (angular.isDefined(item)) {
        //                    leeHelper.copyVm(uiVM, item);
        //                    vmManager.init();
        //                    operate.editModal.$promise.then(operate.editModal.hide);
        //                }
        //            });
        //        });
        //    };
        //},
        controller: function ($scope) {
            $scope.vmManager = vmManager;
            $scope.save = function (isVaild) {
                
            }
        },
        show: false
    });
    operate.refresh = function () {

    }
    operate.editItem = function (item) {
        uiVM = _.clone(item);
        operate.editModal.$promise.then(operate.editModal.show);
    };
    
});


