var quarityModule = angular.module('bpm.qualityApp');
quarityModule.factory("quarityDataOpService", function (ajaxService) {
    var quarity = {};
    var quarityUrl = "/quaInspectionManage/";
    quarity.getMaterialDatas = function (materialId) {
        var url = quarityUrl + "GetMaterialDatas";
        return ajaxService.getData(url,  {
            materialId: materialId
        })
    };
    quarity.saveInspectionItemconfig = function (dataSets) {
        var url = quarityUrl + "SaveInspectionItemconfig";
        return ajaxService.postData(url, {
            dataSets:dataSets
        })
    }
    return quarity;
})
quarityModule.controller("iqcInspectionItemCtrl", function ($scope, quarityDataOpService) {
    var uiVM = {
        //表单变量
        MaterialId: null,
        InspectionItem: null,
        InspectiontermNumber: 0,
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
            $scope.searchPromise = quarityDataOpService.getMaterialDatas($scope.vm.MaterialId).then(function (datas) {
                if (datas != null) {
                    console.log(datas)
                    $scope.tableVm = datas.ProductMaterailModel;
                    vmManager.dataSets = datas.InspectionItemConfigModelList;
                }
            });
        },

        //013935点击表格显示对应的表单
        selectQualityItem: function (item) {
            uiVM = item;
            uiVM.OpSign = "edit";
            $scope.vm = uiVM;
        },
        //013935删除表格
        deleteItem:function(item){
            vmManager.delItem = item;
            leeHelper.remove(vmManager.dataSets, vmManager.delItem);
        },
        //013935批量保存
        savsAll: function(){
            quarityDataOpService.saveInspectionItemconfig(vmManager.dataSets).then(function () {
                vmManager.dataSets = [];
                vmManager.init();
            })
        }
    } 
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;

    operate.save = function (isValid) {
        var modelVM = _.clone(uiVM);
        if (uiVM.OpSign == 'add') {
            leeDataHandler.dataOperate.add(operate, isValid, function () {
                vmManager.dataSets.push(modelVM);
            })
        } else {
            var item = _.find(vmManager.dataSets, { Id_key: uiVM.Id_key });
            if (angular.isDefined(item)) {
                leeDataHandler.dataOperate.add(operate, isValid, function () {
                    //quarityDataOpService.postQualityDatas($scope.vm).then(function () {
                        leeHelper.copyVm(uiVM, item);
                    //})
                })
               
            }
        }
        vmManager.init();
        
    };
    operate.refresh = function () {
        vmManager.init();
    }
})