var qualityModule = angular.module('bpm.qualityApp');
//工厂
qualityModule.factory("qualityDataOpService", function (ajaxService) {
    var quality = {};
    var qualityUrl = "/quaInspectionManage/";
    //013935获取数据
    quality.getMaterialDatas = function (materialId) {
        var url = qualityUrl + "GetMaterialDatas";
        return ajaxService.getData(url,  {
            materialId: materialId
        })
    };
    //013935获取最大序号
    quality.getInspectionIndex = function (materialId) {
        var url = qualityUrl + "GetInspectionIndex";
        return ajaxService.postData(url, {
            materialId: materialId
        })

    }

    //013935导入excel
    quality.importIqcInspectionItemConfigDatas = function (file) {
        var url = qualityUrl + 'ImportIqcInspectionItemConfigDatas';
        return ajaxService.uploadFile(url, file);
    }

    //013935保存所有项
    quality.saveAll = function (dataSource) {
        var url = qualityUrl + 'SaveAllMaterialDatas';
        return ajaxService.postData(url, {
            dataSource: dataSource
        })
    }

    //013935保存单项
    //quality.saveInspectionItemconfig = function (modelVM) {
    //    var url = qualityUrl + "SaveInspectionItemconfig";
    //    return ajaxService.postData(url, {
    //        modelVM: modelVM
    //    })
    //}

    //013935删除单项
    quality.deleteMaterialDatas = function (entity) {
        var url = qualityUrl + "DeleteMaterialDatas";
        return ajaxService.postData(url, {
            entity: entity
        })
    }
    //////////////////检验方式配置模块//////////////////
    ////////////////////////////////////////////////////


    //013935删除单项
    quality.deleteInspectionData = function () {
        var url = qualityUrl + "DeleteInspectionData";
        return ajaxService.getData(url, {
            
        })
    }
    //013935保存单项
    quality.saveInspectionModeData = function (Items) {
        var url = qualityUrl + "SaveInspectionModeData";
        return ajaxService.postData(url, {

        })
    }
    
    return quality;
})

//iqc检验项目配置模块
qualityModule.controller("iqcInspectionItem", function ($scope, qualityDataOpService) {
    var uiVM = {
        //表单变量
        MaterialId: null,
        InspectionItem: null,
        InspectionItemIndex: null,
        SizeUSL: null,
        SizeLSL: null,
        SizeMemo: null,
        EquipmentID: null,
        InspectionMethod: null,
        InspectionMode: null,
        InspectionLevel: null,
        InspectionAQL: null,
        OpPerson: null,
        OpDate: null,
        OpTime: null,
        OpSign: "add",
        Id_key: 0,
    }
    //表头变量
    var tableVM = {
        MaterialName: null,
        MaterialBelongDepartment: null,
        MaterialSpecify: null,
        MaterialrawID: null,
    }
    $scope.tableVm = tableVM;
    $scope.vm = uiVM;
    var initVM = _.clone(uiVM);
    var vmManager = {
        materialDatas: [],
        inspectionMode: [{ id: "正常", text: "正常" }, { id: "加严", text: "加严" }, { id: "放宽", text: "放宽" }],
        dataSource: [],
        dataSets: [],
        copyWindowDisplay: false,
        editWindowWidth: '100%',

        materialId: null,
        materialIdForm: null,
        materialIdTo: null,
        copyDataSets:[],

        delItem: null,
        init: function () {
            if (uiVM.OpSign === 'add') {
                leeHelper.clearVM(uiVM, ["MaterialId", "Id_key"]);
            }
            else {
                uiVM = _.clone(initVM);
            }
            uiVM.OpSign = 'add';
            $scope.vm = uiVM;

        },

        //013935根据品号查询
        getMaterialDatas: function () {
            $scope.searchPromise = qualityDataOpService.getMaterialDatas($scope.vm.MaterialId).then(function (datas) {
                if (datas != null) {
                    $scope.tableVm = datas.ProductMaterailModel;
                    vmManager.dataSource = datas.InspectionItemConfigModelList;
                }
            });
        },
        //013935获取最大序号
        getInspectionIndex: function () {
            $scope.searchPromise = qualityDataOpService.getInspectionIndex($scope.vm.MaterialId).then(function (indexInt) {
                if (indexInt != null) {
                    $scope.vm.InspectionItemIndex = indexInt;
                }
            });
        },
        
    }

    //013935导入excel
    $scope.selectFile = function (el) {
        var files = el.files;
        if (files.length > 0) {
            var file = files[0];
            var fd = new FormData();
            fd.append('file', file);
            qualityDataOpService.importIqcInspectionItemConfigDatas(fd).then(function (datas) {
                vmManager.dataSource = datas;
            });
        }
    };
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //013935保存
    operate.save = function (isValid) {
        var modelVM = _.clone(uiVM);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            leeHelper.setUserData(uiVM);
            if (uiVM.OpSign === "add") {
                vmManager.dataSource.push(modelVM);
                //qualityDataOpService.saveInspectionItemconfig(modelVM).then(function (datas) {
                //    if (datas.Result) {
                        
                //    }
                //})
            }
            //else
            //{
            //    qualityDataOpService.saveInspectionItemconfig(modelVM).then(function (datas) {

            //    })
            //}
            vmManager.init();
        })
    };

    operate.editItem = function (item) {
        uiVM = item;
        uiVM.OpSign = "edit";
        $scope.vm = uiVM;
    };

    operate.deleteItem = function (entity) {
        uiVM = entity;
        uiVM.OpSign = "delete";
        $scope.vm = uiVM;
        
        $scope.searchPromise = qualityDataOpService.deleteMaterialDatas(entity).then(function (datas) {
            leeHelper.remove(vmManager.dataSource, entity);
            //if (datas.Result) {
            //    vmManager.delItem = entity;
                
            //}
        });
        vmManager.init();
    }

    operate.refresh = function () {
        vmManager.init();
    }

    //013935保存所有数据
    operate.saveAll = function ( ){
        $scope.searchPromise = qualityDataOpService.saveAll(vmManager.dataSource).then(function (opresult) {
            if (opresult.Result) {
                vmManager.dataSource = [];
            }
        });
    }

    operate.copyAll = function () {
        vmManager.materialIdForm = $scope.vm.MaterialId;
        vmManager.copyWindowDisplay = true;
    }

    operate.copyConfirm = function () {
        angular.forEach(vmManager.dataSource, function (item) {
            item.MaterialId = vmManager.materialIdTo;
            vmManager.copyDataSets.push(item);
            vmManager.dataSource = vmManager.copyDataSets;
        });
        vmManager.copyDataSets = [];
        $scope.vm.MaterialId = vmManager.materialIdTo;
        vmManager.materialIdTo = null;

    }
})


//iqc检验方式配置模块
qualityModule.controller("iqcInspectionMode", function ($scope, qualityDataOpService) {
    var uiVM = {
        InspectionMode:null,
        InspectionLevel:null,
        InspectionAQL:null,
        StartNumber:0,
        EndNumber:0,
        InspectionCount:0,
        AcceptCount:0,
        RefuseCount:0,
        OpPerson:null,
        OpDate:null,
        OpTime:null,
        OpSign:"add",
        Id_Key:null,
    }
    $scope.vm = uiVM;
    var initVM = _.clone(uiVM);
    var vmManager = {
        editDatas:[],
        datasets: [],
        init: function () {
            uiVM = _.clone(initVM);
            $scope.vm = uiVM;
        },

    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //保存单项
    operate.save = function (isValid) {
        leeHelper.setUserData(uiVM);
        var dataItem = _.clone(uiVM);
        if(uiVM.OpSign==="add"){
            leeDataHandler.dataOperate.add(operate, isValid, function () {
                qualityDataOpService.saveInspectionModeData(data).then(function(opresult){
                    if(opresult.Result){
                        vmManager.editDatas.push(dataItem);
                    }
                })
            });
        } else {
            leeDataHandler.dataOperate.add(operate, isValid, function () {
                qualityDataOpService.saveInspectionModeData(data).then(function (opresult) {
                    if(opresult.Result){
                        var item = _.find(vmManager.editDatas, { Id_Key: uiVM.Id_Key });
                        item = uiVM = $scope.vm;
                    }
                })
            });
        }
    };
    //取消刷新
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    };
    //编辑单项
    operate.editItem = function (data) {
        data.OpSign = "edit";
        $scope.vm =uiVM = _.clone(data);

    }
    //删除单项
    operate.deleteItem = function (data) {
        data.OpSign = 'delete';
        qualityDataOpService.saveInspectionModeData(data).then(function (opresult) {
            if(opresult.Result){
                leeHelper.remove(vmManager.editDatas, data)
            }
        })
    }
})