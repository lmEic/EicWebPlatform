var qualityModule = angular.module('bpm.qualityApp');
qualityModule.factory("qualityDataOpService", function (ajaxService) {
    var quality = {};
    var qualityUrl = "/quaInspectionManage/";
    quality.getMaterialDatas = function (materialId) {
        var url = qualityUrl + "GetMaterialDatas";
        return ajaxService.getData(url,  {
            materialId: materialId
        })
    };
    quality.saveInspectionItemconfig = function (modelVM) {
        var url = qualityUrl + "SaveInspectionItemconfig";
        return ajaxService.postData(url, {
            modelVM: modelVM
        })
    }
    quality.deleteMaterialDatas = function (item) {
        var url = qualityUrl + "DeleteMaterialDatas";
        return ajaxService.postData(url, {
            item: item
        })
    }
    quality.getInspectionIndex = function (materialId) {
        var url = qualityUrl + "GetInspectionIndex";
        return ajaxService.postData(url, {
            materialId: materialId
        })
       
    }

    return quality;
})
qualityModule.controller("iqcInspectionItemCtrl", function ($scope, qualityDataOpService) {
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
        //dataSource: [],
        dataSets: [],
        delItem:null,
        init: function () {
            if (uiVM.OpSign === 'add') {
                leeHelper.clearVM(uiVM, ["MaterialId","Id_key"]);
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
                    vmManager.dataSets = datas.InspectionItemConfigModelList;
                }
            });
        },
        getInspectionIndex: function () {
            $scope.searchPromise = qualityDataOpService.getInspectionIndex($scope.vm.MaterialId).then(function (indexInt) {
                if (indexInt!= null) {
                    $scope.vm.InspectionItemIndex = indexInt;
                }
            });
        }
    } 
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    //013935保存
    operate.save = function (isValid) {
        var modelVM = _.clone(uiVM);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            leeHelper.setUserData(uiVM);
            if (uiVM.OpSign === "add") {
                qualityDataOpService.saveInspectionItemconfig(modelVM).then(function (datas) {
                    if (datas.Result) {
                        vmManager.dataSets.push(modelVM);
                    }
                })
            } else {
                qualityDataOpService.saveInspectionItemconfig(modelVM).then(function (datas) {

                })
            }
            vmManager.init();
        })
    };
        
    operate.editItem = function(item){
        uiVM = item;
        uiVM.OpSign = "edit";
        $scope.vm = uiVM;
    };


    operate.deleteItem = function (item) {
        uiVM = item;
        uiVM.OpSign = "delete";
        $scope.vm = uiVM;
        $scope.searchPromise = qualityDataOpService.saveInspectionItemconfig(item).then(function (datas) {
            if (datas.Result) {
                vmManager.delItem = item;
                leeHelper.remove(vmManager.dataSets, vmManager.delItem);
            }
        });
        vmManager.init();
    }


    operate.refresh = function () {
        vmManager.init();
    }
})