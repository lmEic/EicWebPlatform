﻿/// <reference path="../../common/eloam.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
angular.module('bpm.astApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap'])
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);

    var urlPrefix = leeHelper.controllers.equipment + "/";
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
        templateUrl: urlPrefix + 'AstScrapInput',
    })
    //--------------校验管理--------------------------
    .state('astBuildCheckList', {
        templateUrl: urlPrefix + 'AstBuildCheckList',
    })
    .state('astInputCheckRecord', {
        templateUrl: urlPrefix + 'AstInputCheckRecord',
        onExit: function () {
            //eloam.UnLoad();
        }
    })
    //--------------保养管理--------------------------
    .state('astBuildMaintenanceList', {
        templateUrl: urlPrefix + 'AstBuildMaintenanceList',
    })
    .state('astInputMaintenanceRecord', {
        templateUrl: urlPrefix + 'AstInputMaintenanceRecord',
        onExit: function () {
            eloam.UnLoad();
        }
    })
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
            searchMode: searchMode,
        });
    };
    //获取设备编号
    ast.getEquipmentID = function (equipmentType, assetType, taxType)
    {
        var url = astUrlPrefix + 'GetEquipmentID';
        return ajaxService.getData(url, {
            equipmentType: equipmentType,
            assetType: assetType,
            taxType: taxType,
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
            model: model,
        });
    };
    //保存设备档案记录
    ast.saveEquipmentRecord = function (equipment)
    {
        var url = astUrlPrefix + 'SaveEquipmentRecord';
        return ajaxService.postData(url, {
            equipment: equipment,
        });
    };
    ///获取设备校验清单
    ast.getAstCheckListByPlanDate = function (planDate) {
        var url = astUrlPrefix + 'GetAstCheckListByPlanDate';
        return ajaxService.getData(url, {
            planDate: planDate,
        });
    };
    ///保存设备校验记录
    ast.storeInputCheckRecord = function (model) {
        var url = astUrlPrefix + 'StoreInputCheckRecord';
        return ajaxService.postData(url, {
            model: model,
        });
    };
    //获取设备保养清单
    ast.getAstMaintenanceListByPlanMonth = function (planMonth) {
        var url = astUrlPrefix + 'GetAstMaintenanceListByPlanMonth';
        return ajaxService.getData(url, {
            planMonth: planMonth,
        });
    };
    ///保存设备保养记录
    ast.storeInputMaintenanceRecord = function (model) {
        var url = astUrlPrefix + 'StoreInputMaintenanceRecord';
        return ajaxService.postData(url, {
            model: model,
        });
    };
    ast.handleFile = function (moduleName, fileName) {
        var url = astUrlPrefix + 'HandleFile';
        return ajaxService.getData(url, {
            moduleName: moduleName,
            fileName: fileName,
        });
    };
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
            })
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
        navMainSize: '83%',
    };
    $scope.navLayout = moduleNavLayoutVm;
    $scope.promise = navDataService.getSubModuleNavs('设备管理', 'EquipmentManage').then(function (datas) {
        moduleNavLayoutVm.menus = datas;
        moduleNavLayoutVm.navList = _.where(datas, { AtLevel: 2 });
    });
})
///设备档案总览
.controller('astArchiveOverviewCtrl', function ($scope,astDataopService) {

    //视图管理器
    var vmManager = {
        activeTab: 'baseInfoTab',
        datasource:[],
        datasets: [],
        getAstArchiveOverview: function () {
            vmManager.datasets = [];
            vmManager.datasource=[],
            $scope.promise = astDataopService.getAstArchiveOverview().then(function (datas) {
                vmManager.datasource = datas;
            });
        },
    };
    $scope.vmManager = vmManager;

    vmManager.getAstArchiveOverview();
})
///设备档案登记
.controller('astArchiveInputCtrl', function ($scope, dataDicConfigTreeSet,connDataOpService, astDataopService,$modal) {
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
        Id_Key: null,
    }


    $scope.vm = uiVM;

    var vmManager = {
        activeTab: 'initTab',
        init: function () {
            if (uiVM.OpSign === 'add') {
                uiVM.ManufacturingNumber = null;
            }
            else {
                leeHelper.clearVM(uiVM, ['AssetType', 'EquipmentType', 'ServiceLife','AddMode',
                  'Unit', 'MaintenanceDate', 'CheckType', 'CheckDate', 'MaintenanceInterval', 'CheckInterval']);
            }
            uiVM.OpSign = 'add';
            $scope.vm = uiVM;
            vmManager.canEdit = false;
        },
        canEdit: false,
        equTypes: [{ id: 0, text: '量测设备' }, { id: 1, text: '生产设备' }, {id:2,text:"辅助设备"}],
        taxTypes: [{ id: 0, text: '保税' }, { id: 1, text: '非保税' }],
        assetTypes: [{ id: 0, text: '固定资产' }, { id: 1, text: '低质易耗品' }],
        equUnits: [{ id: 0, text: '台' }, { id: 1, text: '个' }],
        addModes: [{ id: 0, text: '外购' }, { id: 1, text: '自制' }],
        checkTypes: [{ id: 0, text: '内校' }, { id: 1, text: '外校' }],
        departments: [],
        equipments:[],
        workerId: '',
        searchedWorkers:[],
        getAstId: function () {
            astDataopService.getEquipmentID(uiVM.EquipmentType, uiVM.AssetType, uiVM.TaxType).then(function (data) {
                uiVM.AssetNumber = data;
            });
        },
        isSingle: true,//是否搜寻到的是单个人
        getWorkerInfo: function () {
            if (uiVM.SafekeepUser === undefined) return;
            var strLen = leeHelper.checkIsChineseValue(uiVM.SafekeepUser) ? 2 : 6;
            if (uiVM.SafekeepUser.length >= strLen)
            {
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
        selectWorker: function (worker)
        {
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
        assetNum:null,
        editDatas:[],
        getAstDatas: function (searchMode) {
            vmManager.editDatas = [];
            $scope.searchPromise = astDataopService.getEquipmentArchivesBy(vmManager.inputDate,vmManager.assetNum,searchMode).then(function (datas) {
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
        })
    }
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.inti();
        });
    }
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
                        if (angular.isDefined(item))
                        {
                            leeHelper.copyVm(uiVM, item);
                            vmManager.init();
                            operate.editModal.$promise.then(operate.editModal.hide);
                        }
                    })
                });
            };
        },
        show: false,
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
///报废统计 astScrapInputCtrl
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
        OpSign: 'add',
    }
    $scope.vm = uiVM;

    ///设备实体保养模型
    var equipmentVM = {
        AssetNumber: null,
        EquipmentName: null,
        EquipmentSpec: null,
        ManufacturingNumber: null,
    }
    $scope.equipmentvm = equipmentVM;

    var vmManager = {
        init: function () {
            leeHelper.clearVM(uiVM, ['DiscardDate']);
        },
        discardTypes:[{id:1,text:"自然报废"},{id:2,text:"人为报废"}],
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
            var fileName = previewImgPrefix + uiVM.AssetNumber + "_" + uiVM.DocumentId + "-" + random + ".jpg";
            eloam.SaveAsImg(serverFilePath + fileName);
            $scope.previewPromise = astDataopService.handleFile("EquScrapFiles", fileName).then(function (result) {
                if (result.exist) {
                    vedio.previewFileName = result.imgUrl;
                    uiVM.DocumentPath = "FileLibrary/EquScrapFiles/" + result.dateFile + "/" + uiVM.AssetNumber + ".jpg";
                }
            });
        },
    };
    $scope.vedio = vedio;
    $scope.$watch('$viewContentLoaded', function (event) {
        eloam.Load();
    })
    $scope.$on('$destroy', function () {
        eloam.UnLoad();
    })
})
///生成校验清单  
.controller('astBuildCheckListCtrl', function ($scope, dataDicConfigTreeSet, connDataOpService, astDataopService, $modal) {
    //视图管理器
    var vmManager = {
        activeTab: 'initTab',
        planDate: new Date(),
        datasource:[],
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
        OpSign: 'add',
    }
    $scope.vm = uiVM;

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
        CheckState: null,
    }
    $scope.checkvm = checkVM;

    var vmManager = {
        init: function () {
            leeHelper.clearVM(uiVM, ['CheckDate']);
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
            else { return true;}
        },
        getAstDatas: function () {
            if (uiVM.AssetNumber === undefined || uiVM.AssetNumber.length < 6) return;
            $scope.searchPromise = astDataopService.getEquipmentArchivesBy(uiVM.CheckDate,uiVM.AssetNumber, 1).then(function (datas) {
                if (angular.isArray(datas) && datas.length > 0)
                {
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
                    uiVM.DocumentPath = "FileLibrary/MaintenanceFiles/" + result.dateFile + "/" + uiVM.AssetNumber + ".jpg";
                }
            });
        },
    };

    $scope.vedio = vedio;

    $scope.$watch('$viewContentLoaded', function (event) {
        eloam.Load();
    })
    $scope.$on('$destroy', function () {
        eloam.UnLoad();
    })
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
        AssetNumber:null,
        EquipmentName:null,
        MaintenanceDate:new Date(),
        DocumentPath:null,
        OpPerson:null,
        OpSign:'add',
    }
    $scope.vm = uiVM;

    ///设备实体保养模型
    var equipmentVM = {
        AssetNumber: null,
        EquipmentName: null,
        EquipmentSpec: null,
        ManufacturingNumber: null,
        SafekeepDepartment: null,
        IsMaintenance:null,
        MaintenanceDate: null,
        MaintenanceInterval: 0,
        MaintenanceState: null,
    }
    $scope.equipmentVM = equipmentVM;

    var vmManager = {
        init: function () {
            leeHelper.clearVM(uiVM, ['MaintenanceDate']);
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
            var isServer=loginUser.serverName === "EICMAIN";
            var previewImgPath = isServer ? "PreviewFiles/" : "FileLibrary/PreviewFiles/";
            var serverHost = isServer ? "\\\\192.168.0.187\\" : loginUser.webSitePhysicalApplicationPath;
            var serverFilePath = (serverHost + previewImgPath).replace(/\//g, "\\");//服务器文件夹路径
            var fileName=previewImgPrefix + uiVM.AssetNumber + "-" + random + ".jpg";
            eloam.SaveAsImg(serverFilePath + fileName);
            $scope.previewPromise = astDataopService.handleFile("MaintenanceFiles",fileName).then(function (result) {
                if (result.exist) {
                    vedio.previewFileName = result.imgUrl;
                    uiVM.DocumentPath = "FileLibrary/MaintenanceFiles/" + result.dateFile + "/" + uiVM.AssetNumber + ".jpg";
                }
            });
        },
    };
    $scope.vedio = vedio;
    $scope.$watch('$viewContentLoaded', function (event) {
        eloam.Load();
    })
    $scope.$on('$destroy', function () {
        eloam.UnLoad();
    })
})

