/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />
angular.module('bpm.hrApp', ['eicomm.directive', 'mp.configApp', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap'])

.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    //--------------人员管理--------------------------
    $stateProvider.state('workerInfoManage', {
        templateUrl: 'ProEmployee/WorkerInfoManage'

    }).state('proStationManage', {
        templateUrl: 'ProEmployee/ProStationManage'

    }).state('proClassManage', {
        templateUrl: 'ProEmployee/ProClassManage'

    }).state('workHoursManage', {
        templateUrl: 'ProEmployee/WorkHoursManage'
    })
    //--------------员工档案管理--------------------------
     .state('hrEmployeeDataInput', {
         templateUrl: 'HrArchivesManage/HrEmployeeDataInput',
     })
    .state('hrDepartmentChange', {
        templateUrl: 'HrArchivesManage/HrDepartmentChange',
    })
    .state('hrPostChange', {
        templateUrl: 'HrArchivesManage/HrPostChange',
    })
     .state('hrStudyManage', {
         templateUrl: 'HrArchivesManage/HrStudyManage',
     })
     .state('hrTelManage', {
         templateUrl: 'HrArchivesManage/HrTelManage',
     })
    //--------------档案业务管理--------------------------
     .state('hrPrintCard', {
         templateUrl: 'HrArchivesManage/HrPrintCard',
     })
    //--------------考勤业务管理--------------------------
     .state('hrClassTypeManage', {
         templateUrl: 'HrAttendanceManage/HrClassTypeManage',
     })
     .state('hrAttendInToday', {
         templateUrl: 'HrAttendanceManage/HrAttendInToday',
     })
     .state('hrAskLeaveManage', {
         templateUrl: 'HrAttendanceManage/HrAskLeaveManage',
      })
     .state('hrHandleException', {
         templateUrl: 'HrAttendanceManage/HrHandleException',
     })
    //--------------总务管理--------------------------
    //厂服管理
     .state('gaWorkerClothesManage', {
         templateUrl: 'HrGeneralAffairsManage/GaWorkerClothesManage',
     })
})
.factory('hrDataOpService', function (ajaxService) {
    var hr={};
    var archiveCtrl = "/HrArchivesManage/";
    var attendUrl = "/HrAttendanceManage/";
    var generalAffairsUrl = "/HrGeneralAffairsManage/";

    hr.getWorkerCardImages = function ()
    {
        var url = archiveCtrl + 'GetWorkerCardImages';
        return ajaxService.getData(url, {
        });
    };

    ///获取身份证信息
    hr.getIdentityInfoBy = function (lastSixIdWord) {
        var url = archiveCtrl + "GetIdentityInfoBy";
        return ajaxService.getData(url, {
            lastSixIdWord: lastSixIdWord
        });
    };
    //获取作业工号
    hr.getWorkerIdBy = function (workerIdNumType) {
        var url = archiveCtrl + "GetWorkerIdBy";
        return ajaxService.getData(url, {
            workerIdNumType: workerIdNumType
        });
    };

    //获取作业人员信息
    hr.getWorkersInfo = function (vm, searchMode) {
        var url = archiveCtrl + "GetWorkersInfo";
        return ajaxService.getData(url, {
            workerId: vm.workerId,
            registedDateStart: vm.registedDateStart,
            registedDateEnd: vm.registedDateEnd,
            searchMode: searchMode
        });
    };

    ///获取档案信息配置数据
    hr.getArchiveConfigDatas = function () {
        var url = archiveCtrl + "GetArchiveConfigDatas";
        return ajaxService.getData(url, {});
    };

    
    ///获取请假配置信息
    hr.getLeaveTypesConfigs = function () {
        var url = attendUrl + "GetLeaveTypesConfigs";
        return ajaxService.getData(url, {});
    };

    //获取该工号列表的所有人员信息
    //mode:0为部门或岗位信息
    //1:为学习信息;2：为联系方式信息
    hr.getEmployeeByWorkerIds = function (workerIdList,mode) {
        var url = archiveCtrl + "FindEmployeeByWorkerIds";
        return ajaxService.getData(url, {
            workerIdList: workerIdList,
            mode: mode
        });
    };

    ///输入员工档案信息
    hr.inputWorkerArchive = function (employee, oldEmployeeIdentity, opSign) {
        var url = archiveCtrl + "InputWorkerArchive";
        return ajaxService.postData(url, {
            employee: employee,
            oldEmployeeIdentity: oldEmployeeIdentity,
            opSign: opSign
        });
    };

    ///修改部门信息
    hr.changeDepartment = function (changeDepartments) {
        var url = archiveCtrl + "ChangeDepartment";
        return ajaxService.postData(url, {
            changeDepartments: changeDepartments,
        });
    };

    ///修改岗位信息
    hr.changePost = function (changePosts) {
        var url = archiveCtrl + "ChangePost";
        return ajaxService.postData(url, {
            changePosts: changePosts,
        });
    };

    ///修改学习信息
    hr.changeStudy = function (studyInfos) {
        var url = archiveCtrl + "ChangeStudy";
        return ajaxService.postData(url, {
            studyInfos: studyInfos,
        });
    };

    ///修改联系方式信息
    hr.changeTel = function (tels) {
        var url = archiveCtrl + "ChangeTelInfo";
        return ajaxService.postData(url, {
            tels: tels,
        });
    };

    //获取部门的班别信息
    hr.getClassTypeDatas = function (department) {
        var url = attendUrl + "GetClassTypeDatas";
        return ajaxService.getData(url, {
            department: department,
        });
    };

    //保存部门的班别的设置数据信息
    hr.saveClassTypeDatas = function (classTypes) {
        var url = attendUrl + "SaveClassTypeDatas";
        return ajaxService.postData(url, {
            classTypes: classTypes,
        });
    };

    //获取部门的当天的考勤数据信息
    hr.getAttendanceDatasOfToday = function (department) {
        var url = attendUrl + "GetAttendanceDatasOfToday";
        return ajaxService.getData(url, {
            department: department,
        });
    };

    //获取某人的当月请假数据
    hr.getAskLeaveDataAbout = function (workerId,yearMonth) {
        var url = attendUrl + "GetAskLeaveDataAbout";
        return ajaxService.getData(url, {
            workerId: workerId,
            yearMonth: yearMonth,
        });
    };

    //处理请假信息
    hr.handleAskForLeave = function (askForLeaves) {
        var url = attendUrl + "HandleAskForLeave";
        return ajaxService.postData(url, {
            askForLeaves: askForLeaves,
        });
    };
    //修改请假信息
    hr.updateAskForLeave = function (askForLeaves) {
        var url = attendUrl + "UpdateAskForLeave";
        return ajaxService.postData(url, {
            askForLeaves: askForLeaves,
        });
    };

    //自动检测考勤异常数据
    hr.autoCheckExceptionSlotData = function () {
        var url = attendUrl + "AutoCheckExceptionSlotData";
        return ajaxService.postData(url, {
        });
    };

    //载入异常刷卡数据
    hr.getExceptionSlotData = function () {
        var url = attendUrl + "GetExceptionSlotData";
        return ajaxService.getData(url, {
        });
    };

    //保存处理的考勤异常数据
    hr.handleExceptionSlotData = function (exceptionDatas) {
        var url = attendUrl + "HandleExceptionSlotData";
        return ajaxService.postData(url, {
            exceptionDatas: exceptionDatas
        });
    };

    //保存领取厂服记录
    hr.storeWorkerClothesReceiveRecord = function (model) {
        var url = generalAffairsUrl + 'StoreWorkerClothesReceiveRecord';
        return ajaxService.postData(url, {
            model: model,
        });
    };
    ///查询厂服记录
    hr.getWorkerClothesReceiveRecords = function (workerId, department, receiveMonth, mode) {
        var url = generalAffairsUrl + 'GetWorkerClothesReceiveRecords';
        return ajaxService.getData(url, {
            workerId: workerId,
            department: department,
            receiveMonth: receiveMonth,
            mode: mode,
        });
    };
    return hr;
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
    };
    $scope.navLayout = moduleNavLayoutVm;
    $scope.promise = navDataService.getSubModuleNavs('人力资源管理', 'HrManage').then(function (datas) {
        moduleNavLayoutVm.menus = datas;
        moduleNavLayoutVm.navList = _.where(datas, { AtLevel: 2 });
    });
})

//-----------人员档案登记-----------------------
.controller('archiveInputCtrl', function ($scope,$modal,dataDicConfigTreeSet,hrDataOpService) {
    ///员工基础信息视图模型
    var employeeIdentity = {
        IdentityID: null,
        WorkerId: null,
        WorkerIdNumType: null,
        WorkerIdType: null,
        Name: null,
        CardID: null,
        Organizetion: null,
        Department: null,
        DepartmentChangeRecord: 0,
        Post: null,
        PostNature: null,
        PostChangeRecord: 0,
        Sex: null,
        Birthday: null,
        Address: null,
        Nation: null,
        SignGovernment: null,
        LimitedDate: null,
        NewAddress: null,
        PoliticalStatus: null,
        NativePlace: null,
        RegisteredPermanent: null,
        MarryStatus: null,
        BirthMonth: 0,
        IdentityExpirationDate: null,
        PersonalPicture: null,
        SchoolName: null,
        MajorName: null,
        Education: null,
        FamilyPhone: null,
        TelPhone: null,
        CertificateName: null,
        WorkingStatus: null,
        RegistedDate: null,
        RegistedSegment: null,
        ClassType: null,
        OpPerson: null,
        WorkerYears: 0,
        DepartmentText: null,
        PostType: null,
        Id_Key:0
    };

    var defualtCls = "btn btn-sm btn-primary";
    var activeCls = "btn btn-sm btn-info";
    var iconCls = "fa fa-pencil-square";

    var btnTypes = [
        { name: 'workerInfo', text: '工作信息', defaultCls: defualtCls, iconCls: iconCls, isEdit: false },
        { name: 'department', text: '部门信息', defaultCls: defualtCls, iconCls: iconCls, isEdit: false },
        { name: 'post', text: '岗位信息', defaultCls: defualtCls, iconCls: iconCls, isEdit: false },
        { name: 'school', text: '学历信息', defaultCls: defualtCls, iconCls: iconCls, isEdit: false },
        { name: 'tel', text: '联系方式', defaultCls: defualtCls, iconCls: iconCls, isEdit: false },
        //{ name: 'certificate', text: '证书信息', defaultCls: defualtCls, iconCls: iconCls, isEdit: false },
    ];


    var archiveInput = {
        opSign:'add',
        //输入成功的员工档案数据信息
        inputedEmployeeDatas:[],
        //配置数据
        configDatas:[],
        showIdentitySwitch:false,
        showIdetityIcon: 'fa fa-2x fa-toggle-down',
        showIdentityBoard: function () {
            archiveInput.showIdentitySwitch = !archiveInput.showIdentitySwitch;
            archiveInput.showIdetityIcon = archiveInput.showIdentitySwitch === true ? "fa fa-2x fa-toggle-up" : "fa fa-2x fa-toggle-down";
        },
        //身份证信息
        identityInfos: [],
        identityInfo: null,
        //姓名信息列表
        namesInfo:[],
        registedTimeSegments: [{ name: "am", text: "上午" }, { name: "pm", text: "下午" }],
        btnTypes: btnTypes,
        currentBtnType:btnTypes[0],
        selectBtnType: function (btnType) {
            archiveInput.currentBtnType = btnType;
            angular.forEach(archiveInput.btnTypes, function (bt) {
                if (!bt.isEdit)
                    bt.defaultCls = defualtCls;
            });
            if (!btnType.isEdit)
            {
                archiveInput.currentBtnType.defaultCls = activeCls;
            }
        },
        //根据身份证号后六位获取的数据是否唯一
        isSingle: true,
        setIdentityValue: function () {
            employeeIdentity.IdentityID = archiveInput.identityInfo.IdentityID;
            employeeIdentity.Name = archiveInput.identityInfo.Name;
            employeeIdentity.NativePlace = archiveInput.identityInfo.NativePlace;
        },
        //根据身份证号获取身份证数据
        getIdentityInfo: function ($event) {
            archiveInput.namesInfo = [];
            if (employeeIdentity.IdentityID === undefined || employeeIdentity.IdentityID === null) return;
            if (employeeIdentity.IdentityID.length === 6)
            {
                $scope.identityPromise = hrDataOpService.getIdentityInfoBy(employeeIdentity.IdentityID).then(function (datas) {
                    if (datas.length > 0) {
                        archiveInput.identityInfos = datas;
                        archiveInput.isSingle = datas.length === 1 ? true : false;
                        if (archiveInput.isSingle) {
                            var identityItem = datas[0];
                            archiveInput.identityInfo = identityItem.Identity;
                            archiveInput.showIdentityExpireInfoMsgModal(identityItem.IsExpire);
                        }
                        else {
                            angular.forEach(datas, function (item) {
                                archiveInput.namesInfo.push({ name: item.Name, text: item.Name });
                            });
                        }
                    }
                    else {
                        leeHelper.clearVM(archiveInput.identityInfo);
                    }
                    archiveInput.setIdentityValue();
                });
            }
        },
        identityInfoMsgModal : $modal({
            title: "信息提示",
            content: "此身份证已经超过有效期了！",
            templateUrl:leeHelper.modalTplUrl.msgModalUrl,
            show: false,
        }),
        showIdentityExpireInfoMsgModal: function (isExpire) {
            if (isExpire)
                archiveInput.identityInfoMsgModal.$promise.then(archiveInput.identityInfoMsgModal.show);
        },
        selectName: function () {
            var data=_.find(archiveInput.identityInfos, { Name: employeeIdentity.Name });
            archiveInput.identityInfo = data.Identity;
            archiveInput.showIdentityExpireInfoMsgModal(data.IsExpire);
            archiveInput.setIdentityValue();
        },
        //工号数字类别
        workerIdCategories: [],
        selectWorkerIdCategory: function () {
            var category = 'WorkerIdCategory';
            var datas = _.where(archiveInput.configDatas, { AboutCategory: category });
            archiveInput.workerIdTypes = createDataSource(datas, category, employeeIdentity.WorkerIdNumType);
            hrDataOpService.getWorkerIdBy(employeeIdentity.WorkerIdNumType).then(function (data) {
                employeeIdentity.WorkerId = data;
            });
        },
        //工号具体类型
        workerIdTypes: [],
        //政治面貌
        politicalStatus: [],
        //户口
        registeredPermanents: [],
        //婚姻状态
        marryStatuses: [],
        departments: [],
        selectDepartment: function () {
            $scope.ztree = departmentTreeSet;
            departmentTreeSet.setTreeDataset(archiveInput.departments);
        },
        posts: [],
        selectPost: function () {
            $scope.ztree = postTreeSet;
            postTreeSet.setTreeDataset(archiveInput.posts);
        },
        //岗位性质
        postNatures: [],
        //学历
        qulifacations: [],
        setCurrentBtnStatus: function () {
            archiveInput.currentBtnType.isEdit = true;
            archiveInput.currentBtnType.defaultCls = "btn btn-sm btn-success";
            archiveInput.currentBtnType.iconCls = "fa fa-check-square-o";
        },
        //界面跳转
        navigateTo:function(btnIndex){
            archiveInput.setCurrentBtnStatus();
            archiveInput.selectBtnType(archiveInput.btnTypes[btnIndex]);
        },
        //转向编辑部门信息
        goToEditDepartment: function () {
            archiveInput.navigateTo(1);
        },
        //转向编辑岗位信息
        goToEditPost: function () {
            archiveInput.navigateTo(2);
        },
        //转向编辑学校信息
        goToEditSchool: function () {
            archiveInput.navigateTo(3);
        },
        //转向编辑联系方式信息
        goToEditTel: function () {
            archiveInput.navigateTo(4);
        },
        //单独保存联系方式信息
        saveTelInfo: function () {
            archiveInput.setCurrentBtnStatus();
        },
        //转向编辑证书信息
        goToEditCertificate: function () {
            archiveInput.navigateTo(5);
        },
    }
    $scope.configPromise = hrDataOpService.getArchiveConfigDatas().then(function (datas) {
        
        archiveInput.configDatas = datas;
        archiveInput.workerIdCategories = createDataSource(datas, 'WorkerIdCategory', '工号类别');
        archiveInput.politicalStatus = createDataSource(datas, 'PoliticalStatus', "政治面貌");
        archiveInput.registeredPermanents = createDataSource(datas, 'PermanentResidence', "户籍");
        archiveInput.marryStatuses = createDataSource(datas, 'MarryStatus', "婚否");
        archiveInput.postNatures = createDataSource(datas, 'PostNature', "岗位性质");
        archiveInput.qulifacations = createDataSource(datas, 'QulificationType', "学历类型");

        archiveInput.departments = _.where(datas, { AboutCategory: "HrDepartmentSet" });
        archiveInput.posts = _.where(datas, { AboutCategory: "PostInfo" });
    });

    var createDataSource = function (datas,category,parentNodeText) {
        var datasetRtn = [];
        var ds = _.where(datas, { AboutCategory: category, ParentDataNodeText: parentNodeText });
        if (ds !== undefined && ds.length > 0) {
            angular.forEach(ds, function (categoryItem) {
                datasetRtn.push({name: categoryItem.DataNodeText, text: categoryItem.DataNodeText });
            });
        }
        return datasetRtn;
    };
    

    var getDepartmentOrganization = function (department) {
        var organizations = [];
        var depItem = _.find(archiveInput.departments, { DataNodeText: department });
        while (depItem !== undefined)
        {
            organizations.push(depItem.DataNodeName);
            if (depItem.ParentDataNodeText === "光圣科技") {
                depItem = undefined;
            }
            else {
                depItem = _.find(archiveInput.departments, { DataNodeText: depItem.ParentDataNodeText });
            }
           
        }
        return organizations.join(',');
    };

    $scope.archive = archiveInput;
    $scope.vm = employeeIdentity;

    var operate = Object.create(leeDataHandler.operateStatus);
    operate.vm = employeeIdentity;
    $scope.operate = operate;
    //保存全部数据信息
    operate.saveAll = function (isValid) {
        employeeIdentity.Organizetion = getDepartmentOrganization(employeeIdentity.DepartmentText);
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            hrDataOpService.inputWorkerArchive(employeeIdentity,pHelper.oldVm,archiveInput.opSign).then(function (opResult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opResult,function () {
                    if (opResult.Result) {
                        if (archiveInput.opSign === "add") {
                            employeeIdentity.Id_Key = opResult.Id_Key;
                            var emp = _.clone(employeeIdentity);
                            var identity = _.clone(archiveInput.identityInfo);
                            archiveInput.inputedEmployeeDatas.push({ employee: emp, identityInfo: identity, id: opResult.Id_Key });
                        }
                        else {
                            var emp = _.find(archiveInput.inputedEmployeeDatas, { id: opResult.Id_Key });
                            if (emp !== undefined) {
                                emp.employee = _.clone(employeeIdentity);
                                emp.identityInfo = _.clone(archiveInput.identityInfo);
                            }
                        }
                        pHelper.InitStatus();
                    }
                });
            });
        });
    };
    //取消操作
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            pHelper.InitStatus();
        });
    };
    operate.edit = function (empIdentity) {
        archiveInput.opSign = 'edit';
        pHelper.oldVm = _.clone(empIdentity.employee);
        employeeIdentity = _.clone(empIdentity.employee);
        $scope.vm = employeeIdentity;
        archiveInput.identityInfo =_.clone(empIdentity.identityInfo);
    };

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        employeeIdentity.Department = dto.DataNodeName;
        employeeIdentity.DepartmentText = dto.DataNodeText;
    };

    var postTreeSet = dataDicConfigTreeSet.getTreeSet('postTree', "岗位信息");
    postTreeSet.bindNodeToVm = function () {
        var dto = _.clone(postTreeSet.treeNode.vm);
        employeeIdentity.PostType = dto.ParentDataNodeText;
        employeeIdentity.Post = dto.DataNodeText;
    };

    var pHelper = {
        InitStatus: function () {
            archiveInput.opSign = 'add';
            angular.forEach(archiveInput.btnTypes, function (btnType) {
                btnType.isEdit = false;
                btnType.iconCls = iconCls;
            });
            archiveInput.selectBtnType(archiveInput.btnTypes[0]);
            leeHelper.clearVM(archiveInput.identityInfo);
            leeHelper.clearVM(employeeIdentity, ["RegistedDate", "RegistedSegment", "Post", "PostNature", "PostType", "DepartmentText", "Department"]);
        },
        oldVm:null,
    };
})
.controller('arDepartmentChangeCtrl', function ($scope, hrDataOpService, dataDicConfigTreeSet, connDataOpService) {
    //视图管理器
    var vmManager = {
        opSign:'edit',
        opDescription:'修改为：',
        configDatas:[],
        current:null,
        changeDatas: [],
        workerIdList:[],
        addWorkerId: function ($event) {
            if ($event.keyCode === 13)
            {
                var item = _.findWhere(vmManager.changeDatas, { WorkerId: $scope.WorkerId });
                if (item === undefined)
                {
                    item = {
                        WorkerId: _.clone($scope.WorkerId),
                        WorkerName: null,
                        OldDepartment: null,
                        OldDepartmentText: null,
                        NowDepartment: null,
                        NowDepartmentText:null,
                        opDescription: null,
                        OpSign: null,
                        OpCmdVisible:false,
                    };
                    vmManager.changeDatas.push(item);
                    vmManager.workerIdList.push($scope.WorkerId);
                    $scope.WorkerId = null;
                }
              
            }
        }
    };
    $scope.vmManager = vmManager;

    //业务逻辑操作对象
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.search = function () {
        $scope.workerPromise = hrDataOpService.getEmployeeByWorkerIds(vmManager.workerIdList,0).then(function (data) {
            angular.forEach(data, function (item) {
                var dep = _.find(vmManager.changeDatas, { WorkerId: item.WorkerId });
                if (dep !== undefined)
                {
                    dep.WorkerName = item.Name;
                    dep.OldDepartment = item.Department;
                    dep.OpCmdVisible = true;
                    var depConfig = _.find(vmManager.configDatas, { DataNodeName: item.Department });
                    if (depConfig !== undefined)
                    {
                        dep.OldDepartmentText = depConfig.DataNodeText;
                    }
                }
            })
        });
    };

    operate.edit = function (dep) {
        pHelper.setOpStatusInfo("修改为:", "edit", dep);
    };
    operate.change = function (dep) {
        pHelper.setOpStatusInfo("调动至:", "change", dep);
    }
    operate.save = function () {
        hrDataOpService.changeDepartment(vmManager.changeDatas).then(function (opResult) {
            if (opResult.Result) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                    vmManager.changeDatas = [];
                    vmManager.isLoad = false;
                });
            }
        });
    };

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentEditTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var treeNodeVm = _.clone(departmentTreeSet.treeNode.vm);
        vmManager.current.NowDepartmentText = treeNodeVm.DataNodeText;
        vmManager.current.NowDepartment = treeNodeVm.DataNodeName;
        vmManager.current.opDescription = _.clone(vmManager.opDescription);
        vmManager.current.OpSign = _.clone(vmManager.opSign);
       
    };

    $scope.ztree = departmentTreeSet;

    $scope.promise = connDataOpService.getDepartments().then(function (datas) {
        vmManager.configDatas = datas;
        departmentTreeSet.setTreeDataset(datas);
    });

    var pHelper = {
        setOpStatusInfo: function (opdescription, opsign,dep)
        {
            vmManager.opSign = opsign;
            vmManager.opDescription = opdescription;
            vmManager.current = dep;
        },
    };
})
.controller('arPostChangeCtrl', function ($scope,hrDataOpService, dataDicConfigTreeSet, connDataOpService) {

    //视图管理器
    var vmManager = {
        postNatures: [{ name: '直接', text: '直接' }, { name: '间接', text: '间接' }],
        postNature:null,
        opSign: 'edit',
        opDescription: '修改为：',
        configDatas: [],
        current: null,
        changeDatas: [],
        workerIdList: [],
        addWorkerId: function ($event) {
            if ($event.keyCode === 13) {
                var item = _.findWhere(vmManager.changeDatas, { WorkerId: $scope.WorkerId });
                if (item === undefined) {
                    item = {
                        WorkerId: _.clone($scope.WorkerId),
                        WorkerName: null,
                        PostNature: null,
                        PostType: null,
                        OldPost: null,
                        NowPost: null,
                        opDescription: null,
                        OpSign: null,
                        OpCmdVisible: false,
                    };
                    vmManager.changeDatas.push(item);
                    vmManager.workerIdList.push($scope.WorkerId);
                    $scope.WorkerId = null;
                }

            }
        }
    };
    $scope.vmManager = vmManager;

    //业务逻辑操作对象
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.search = function () {
        $scope.workerPromise = hrDataOpService.getEmployeeByWorkerIds(vmManager.workerIdList,0).then(function (data) {
            angular.forEach(data, function (item) {
                var dep = _.find(vmManager.changeDatas, { WorkerId: item.WorkerId });
                if (dep !== undefined) {
                    dep.WorkerName = item.Name;
                    dep.OldPost = item.Post;
                    dep.PostNature = item.PostNature;
                    dep.PostType = item.PostType;
                    dep.OpCmdVisible = true;
                    var depConfig = _.find(vmManager.configDatas, { DataNodeName: item.Department });
                    if (depConfig !== undefined) {
                        dep.OldDepartmentText = depConfig.DataNodeText;
                    }
                }
            })
        });
    };

    operate.edit = function (post) {
        pHelper.setOpStatusInfo("修改为:", "edit", post);
    };
    operate.change = function (post) {
        pHelper.setOpStatusInfo("变动为:", "change", post);
    }
    operate.save = function () {
        hrDataOpService.changePost(vmManager.changeDatas).then(function (opResult) {
            if (opResult.Result) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                    vmManager.changeDatas = [];
                });
            }
        });
    };

    var postTreeSet = dataDicConfigTreeSet.getTreeSet('postEditTree', "岗位信息");

    postTreeSet.bindNodeToVm = function () {
        var treeNodeVm = _.clone(postTreeSet.treeNode.vm);
        vmManager.current.NowPost = treeNodeVm.DataNodeText;
        vmManager.current.PostType = treeNodeVm.ParentDataNodeText;
        vmManager.current.PostNature = vmManager.postNature;
        vmManager.current.opDescription = _.clone(vmManager.opDescription);
        vmManager.current.OpSign = _.clone(vmManager.opSign);
    };

    $scope.ztree = postTreeSet;

    $scope.promise = connDataOpService.loadConfigDicData('ArchiiveConfig', 'PostInfo').then(function (datas) {
        vmManager.configDatas = datas;
        postTreeSet.setTreeDataset(datas);
    });

    var pHelper = {
        setOpStatusInfo: function (opdescription, opsign, post) {
            vmManager.opSign = opsign;
            vmManager.opDescription = opdescription;
            vmManager.current = post;
        },
    };
})
.controller('arStudyChangeCtrl', function ($scope,$modal,hrDataOpService, dataDicConfigTreeSet, connDataOpService) {
    ///视图模型
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        StudyDateFrom: null,
        StudyDateTo: null,
        SchoolName: null,
        MajorName: null,
        Qulification: null,
        OpSign: null,
        OpCmdVisible: false,
        Id_Key: null,
        id:0
    }
    //视图管理器
    var vmManager = {
        opSign: 'edit',
        qulifacations: [],
        current: null,
        //当前项的索引位置
        currentIndex:0,
        changeDatas: [],
        workerIdList: [],
        addWorkerId: function ($event) {
            if ($event.keyCode === 13) {
                var item = _.findWhere(vmManager.changeDatas, { WorkerId: $scope.WorkerId });
                if (item === undefined) {
                    item = {
                        WorkerId: _.clone($scope.WorkerId),
                        WorkerName: null,
                        StudyDateFrom: null,
                        StudyDateTo: null,
                        SchoolName: null,
                        MajorName: null,
                        Qulification: null,
                        OpSign: null,
                        OpCmdVisible: false,
                        id:0
                    };
                    vmManager.changeDatas.push(item);
                    vmManager.workerIdList.push($scope.WorkerId);
                    $scope.WorkerId = null;
                }

            }
        }
    };
    $scope.vmManager = vmManager;

    //业务逻辑操作对象
    var operate = Object.create(leeDataHandler.operateStatus);
    operate.vm = uiVM;

    $scope.operate = operate;

    var setItemValue = function (queryItem, item)
    {
        leeHelper.copyVm(item, queryItem);
        queryItem.OpCmdVisible = true;
        queryItem.OpSign = "init";
        queryItem.id += 1;
    }
    operate.search = function () {
        $scope.workerPromise = hrDataOpService.getEmployeeByWorkerIds(vmManager.workerIdList,1).then(function (data) {
            angular.forEach(data, function (item) {
                var queryItem = _.find(vmManager.changeDatas, { WorkerId: item.WorkerId });
                if (queryItem !== undefined) {
                    if (!queryItem.OpCmdVisible) {
                        setItemValue(queryItem, item);
                    }
                    else {
                        if (queryItem.SchoolName !== item.SchoolName) {
                            var addItem = _.clone(queryItem);
                            setItemValue(addItem, item);
                            leeHelper.insert(vmManager.changeDatas, addItem.id, addItem);
                        }
                    }
                }
            })
        });
    };

    operate.editModal = $modal({
        title: "操作窗口",
        templateUrl:'HR/StudyEditSelectTpl/',
        controller: function ($scope) {
            $scope.vm = uiVM;
            $scope.qulifacations = vmManager.qulifacations;

            var op = Object.create(leeDataHandler.operateStatus);
            op.vm = uiVM;
            $scope.operate = op;

            $scope.save = function (isvalidate) {
                leeDataHandler.dataOperate.add(op, isvalidate, function () {
                    var item = _.clone(uiVM);
                    item.OpSign = vmManager.opSign;
                    item.OpCmdVisible = true;
                    if (item.OpSign === "add") {
                        leeHelper.insert(vmManager.changeDatas, vmManager.currentIndex + 1, item);
                    }
                    else {
                        var qryItem = _.find(vmManager.changeDatas, { id: item.id });
                        leeHelper.copyVm(item, qryItem);
                    }
                    operate.editModal.$promise.then(operate.editModal.hide);
                });
            };
        },
        show: false,
    });
    operate.edit = function (item) {
        pHelper.setOpStatusInfo('edit', item);
    };
    operate.add = function (item) {
        pHelper.setOpStatusInfo('add', item);
        leeHelper.clearVM(uiVM, ['WorkerId', 'WorkerName']);
    }

    operate.save = function () {
        hrDataOpService.changeStudy(vmManager.changeDatas).then(function (opResult) {
            if (opResult.Result) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                    vmManager.changeDatas = [];
                });
            }
        });
    };

    $scope.promise = connDataOpService.loadConfigDicData('ArchiiveConfig', 'QulificationType').then(function (datas) {
        angular.forEach(datas, function (item) {
            vmManager.qulifacations.push({ name: item.DataNodeText, text: item.DataNodeText });
        })
    });

    var pHelper = {
        setOpStatusInfo: function (opsign, item) {
            vmManager.opSign = opsign;
            vmManager.current = item;
            vmManager.currentIndex = _.findIndex(vmManager.changeDatas, { id: item.id });
            uiVM = _.clone(item);
            vmManager.opSign = opsign;
            operate.editModal.$promise.then(operate.editModal.show);
        },
    };
})
.controller('arTelChangeCtrl', function ($scope, $modal, hrDataOpService, dataDicConfigTreeSet, connDataOpService) {
    ///视图模型
    var uiVM = {
        OpSign: null,
        WorkerId: null,
        WorkerName: null,
        FamilyPhone: null,
        PersonPhone: null,
        TelPhone: null,
        WorkingStatus: null,
        OpPerson: null,
        OpDate: null,
        Memo: null,
        Id_Key: null,
        OpCmdVisible: false,
        id:0
    }
    //视图管理器
    var vmManager = {
        opSign: 'edit',
        qulifacations: [],
        current: null,
        //当前项的索引位置
        currentIndex: 0,
        changeDatas: [],
        workerIdList: [],
        addWorkerId: function ($event) {
            if ($event.keyCode === 13) {
                var item = _.findWhere(vmManager.changeDatas, { WorkerId: $scope.WorkerId });
                if (item === undefined) {
                    item = {
                        WorkerId: _.clone($scope.WorkerId),
                        WorkerName: null,
                        PersonPhone: null,
                        TelPhone: null,
                        Memo: null,
                        OpSign: null,
                        OpCmdVisible: false,
                        id:0
                    };
                    vmManager.changeDatas.push(item);
                    vmManager.workerIdList.push($scope.WorkerId);
                    $scope.WorkerId = null;
                }

            }
        }
    };
    $scope.vmManager = vmManager;

    //业务逻辑操作对象
    var operate = Object.create(leeDataHandler.operateStatus);
    operate.vm = uiVM;

    $scope.operate = operate;

    var setItemValue = function (queryItem, item) {
        leeHelper.copyVm(item, queryItem);
        queryItem.OpCmdVisible = true;
        queryItem.OpSign = "init";
        queryItem.id += 1;
    }
    operate.search = function () {
        $scope.workerPromise = hrDataOpService.getEmployeeByWorkerIds(vmManager.workerIdList, 2).then(function (data) {
            angular.forEach(data, function (item) {
                var queryItem = _.find(vmManager.changeDatas, { WorkerId: item.WorkerId });
                if (queryItem !== undefined) {
                    if (!queryItem.OpCmdVisible) {
                        setItemValue(queryItem, item);
                    }
                    else {
                        if (queryItem.TelPhone !== item.TelPhone) {
                            var addItem = _.clone(queryItem);
                            setItemValue(addItem, item);
                            leeHelper.insert(vmManager.changeDatas, addItem.id, addItem);
                        }
                    }
                }
            })
        });
    };

    operate.editModal = $modal({
        title: "操作窗口",
        templateUrl: 'HR/TelEditSelectTpl/',
        controller: function ($scope) {
            $scope.vm = uiVM;
            var op = Object.create(leeDataHandler.operateStatus);
            op.vm = uiVM;
            $scope.operate = op;

            $scope.save = function (isvalidate) {
                leeDataHandler.dataOperate.add(op, isvalidate, function () {
                    var item = _.clone(uiVM);
                    item.OpSign = vmManager.opSign;
                    item.OpCmdVisible = true;
                    if (item.OpSign === "add") {
                        item.id = vmManager.changeDatas.length + 1;
                        leeHelper.insert(vmManager.changeDatas, vmManager.currentIndex + 1, item);
                    }
                    else {
                        var qryItem = _.find(vmManager.changeDatas, { id: item.id });
                        leeHelper.copyVm(item, qryItem);
                    }
                    operate.editModal.$promise.then(operate.editModal.hide);
                });
            };
        },
        show: false,
    });
    operate.edit = function (item) {
        pHelper.setOpStatusInfo('edit', item);
    };
    operate.add = function (item) {
        pHelper.setOpStatusInfo('add', item);
        leeHelper.clearVM(uiVM, ['WorkerId', 'WorkerName']);
    }

    operate.save = function () {
        hrDataOpService.changeTel(vmManager.changeDatas).then(function (opResult) {
            if (opResult.Result) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                    vmManager.changeDatas = [];
                });
            }
        });
    };

    var pHelper = {
        setOpStatusInfo: function (opsign, item) {
            vmManager.opSign = opsign;
            vmManager.current = item;
            vmManager.currentIndex = _.findIndex(vmManager.changeDatas, { id: item.id });
            uiVM = _.clone(item);
            vmManager.opSign = opsign;
            operate.editModal.$promise.then(operate.editModal.show);
        },
    };
})
.controller('printCardCtrl', function ($scope,hrDataOpService) {
    var uiVM = {
        workerId:null,
        registedDateStart: new Date(),
        registedDateEnd: new Date(),
    };
    $scope.vm = uiVM;

    
    var vmManager = {
        dataSet: [
        //    { WorkerId: '001359', Name: '万晓桥', Department: '企业讯息中心', PostNature: '直接' },
        //{ WorkerId: '001359', Name: '杨垒', Department: '企业讯息中心', PostNature: '直接' },
        //{ WorkerId: '001569', Name: '张文明', Department: '企业讯息中心', PostNature: '间接' },
        //{ WorkerId: '001569', Name: '张文明', Department: '企业讯息中心', PostNature: '直接' },
        //{ WorkerId: '001569', Name: '张文明', Department: '企业讯息中心', PostNature: '直接' },
        //{ WorkerId: '001569', Name: '张文明', Department: '企业讯息中心', PostNature: '间接' },
        //{ WorkerId: '001569', Name: '张文明', Department: '企业讯息中心', PostNature: '直接' },
        //{ WorkerId: '001569', Name: '张文明', Department: '企业讯息中心', PostNature: '直接' },
        //{ WorkerId: '001569', Name: '张文明', Department: '企业讯息中心', PostNature: '直接' },
        //{ WorkerId: '002295', Name: '孙晓华', Department: '企业讯息中心', PostNature: '直接' },
        //{ WorkerId: '001569', Name: '张文明', Department: '企业讯息中心', PostNature: '间接' },
        //{ WorkerId: '001569', Name: '张文明', Department: '企业讯息中心', PostNature: '直接' },
        //{ WorkerId: '001569', Name: '张文明', Department: '企业讯息中心', PostNature: '直接' },
        //{ WorkerId: '001569', Name: '张文明', Department: '企业讯息中心', PostNature: '直接' },
        //{ WorkerId: '002295', Name: '孙晓华', Department: '企业讯息中心', PostNature: '直接' }
        ],
        workerId:null,
        searchDatasBy: function (mode) {
            uiVM.workerId = vmManager.workerId;
            $scope.promise= hrDataOpService.getWorkersInfo($scope.vm, mode).then(function (datas) {
                angular.forEach(datas, function (item) {
                    vmManager.dataSet.push(item);
                })
            })
        },
        searchByWorkerId: function ($event) {
            if ($event.keyCode === 13) {
                vmManager.searchDatasBy(2);
                vmManager.workerId = null;
            }
        },
        searchByregistedDate: function () {
            vmManager.dataSet = [];
            vmManager.searchDatasBy(3);
        },
        removeItem: function (item) {
            leeHelper.remove(vmManager.dataSet, item);
        },
        getCardImgBg: function (item) {
            var url = (item.PostNature === "直接") ? "../../Content/image/DirectCardTemplate.jpg" : "../../Content/image/IndirectCardTemplate.jpg";
            return url;
        },
    };
    $scope.vmManager = vmManager;


    $scope.printCard = function () {
        hrDataOpService.getWorkerCardImages().then(function (data) {
            var dd = {
                content: [

                    	{
                            image:data,
                    	    width: 200
                    	},
                    'Another paragraph, this time a little bit longer to make sure, this line will be divided into at least two lines'
                ]
            }
            pdfMake.createPdf(dd).open();
        })
    }
})
//-----------考勤业务管理-----------------------
//班别设置管理
.controller('attendClassTypeSetCtrl', function ($scope, $modal,hrDataOpService,dataDicConfigTreeSet,connDataOpService) {
    var qryDto = {
        Department: '部门',
        DepartmentText: '部门',
        DateFrom: null,
        DateTo: null,
        ClassType:'白班'
    };
    $scope.vm = qryDto;

    ///ui视图模型
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        Department: null,
        ClassType: null,
        IsAlwaysDay: null,
        DateFrom: null,
        DateTo: null,
        OpDate: null,
        OpPerson: null,
        OpSign: null,
        Id_Key: null,
        isSelect: false,
        selectedCls:''
    }


    var vmManager = {
        classTypes: [{ name: '白班', text: '白班' }, { name: '晚班', text: '晚班' }, { name: '', text: 'All' }, ],
        dataSets: [],
        dataSource:[],
        filterWorkerId: '',
        filterClassType:'',
        filterByWorkerId: function () {
            var datas = _.clone(vmManager.dataSource);
            if (vmManager.filterWorkerId.length >= 6) {
                vmManager.dataSets = _.where(datas, { WorkerId: vmManager.filterWorkerId });
            }
            else {
                vmManager.dataSets = datas;
            }
        },
        filterByClassType: function () {
            var datas = _.clone(vmManager.dataSource);
            if (vmManager.filterClassType!=="") {
                vmManager.dataSets = _.where(datas, { ClassType: vmManager.filterClassType });
            }
            else {
                vmManager.dataSets = datas;
            }
        },
        detailsDisplay: false,
        selectedWorkers: [],
        init: function () {
            vmManager.dataSets = [];
            vmManager.selectedWorkers = [];
            vmManager.dataSource = [];
            vmManager.isSelectAll = false;
        },
        msgModal: $modal({
            title: '信息提示',
            content:'请先选择要转班人员的数据！',
            templateUrl: leeHelper.modalTplUrl.msgModalUrl,
            show:false
        }),
        isSelectAll: false,
        selectAll: function () {
            vmManager.selectedWorkers = [];
            angular.forEach(vmManager.dataSets, function (item) {
                item.isSelect = !vmManager.isSelectAll;
                operate.addWorkerToList(item);
            });
        },
    };
   
    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);
    operate.loadData = function () {
        vmManager.init();
        $scope.classTypePromise = hrDataOpService.getClassTypeDatas(qryDto.Department).then(function (datas) {
            angular.forEach(datas, function (item) {
                var dataItem = _.clone(uiVM);
                leeHelper.copyVm(item, dataItem);
                vmManager.dataSource.push(dataItem);
            })
            vmManager.dataSets = _.clone(vmManager.dataSource);
        });

    };

    operate.preview = function () {
        vmManager.detailsDisplay = !vmManager.detailsDisplay;
    };

    operate.addWorkerToList = function (item) {
        var worker = _.find(vmManager.selectedWorkers, { WorkerId: item.WorkerId });
        if (worker === undefined) {
            if (item.isSelect) {
                item.selectedCls = "text-primary";
                vmManager.selectedWorkers.push(item);
            }
            else {
                item.selectedCls = "";
            }
        }
        else {
            if (!item.isSelect) {
                item.selectedCls = "";
                leeHelper.remove(vmManager.selectedWorkers, item);
            }

        }
    };
    operate.changeClass = function (isvalid) {
        leeDataHandler.dataOperate.add(operate, isvalid, function () {
            if (vmManager.selectedWorkers.length === 0) {
                vmManager.msgModal.$promise.then(vmManager.msgModal.show);
            }
            else {
                angular.forEach(vmManager.selectedWorkers, function (item) {
                    leeHelper.copyVm(qryDto, item);
                });
                hrDataOpService.saveClassTypeDatas(vmManager.selectedWorkers).then(function (opResult) {
                    leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                        vmManager.init();
                    })
                })
            }
        });
    };

    $scope.operate = operate;
    operate.vm = qryDto;

    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");

    departmentTreeSet.bindNodeToVm = function () {
        qryDto.Department = departmentTreeSet.treeNode.vm.DataNodeName;
        qryDto.DepartmentText = departmentTreeSet.treeNode.vm.DataNodeText;
    };
    $scope.ztree = departmentTreeSet;

    $scope.promise = connDataOpService.getDepartments().then(function (datas) {
        departmentTreeSet.setTreeDataset(datas);
    });
})
//今日考勤
.controller('attendInTodayCtrl', function ($scope, $modal, hrDataOpService, dataDicConfigTreeSet, connDataOpService) {
    var qryDto = {
        Department: '部门',
        DepartmentText: '部门',
        ClassType: '白班'
    };
    $scope.vm = qryDto;


    var vmManager = {
        classTypes: [{ name: '白班', text: '白班' }, { name: '晚班', text: '晚班' }, { name: '', text: 'All' }, ],
        dataSets: [],
        dataSource: [],
        filterWorkerId: '',
        filterClassType: '',
        filterByWorkerId: function () {
            var datas = _.clone(vmManager.dataSource);
            if (vmManager.filterWorkerId.length >= 6) {
                vmManager.dataSets = _.where(datas, { WorkerId: vmManager.filterWorkerId });
            }
            else {
                vmManager.dataSets = datas;
            }
        },
        filterByClassType: function () {
            var datas = _.clone(vmManager.dataSource);
            if (vmManager.filterClassType !== "") {
                vmManager.dataSets = _.where(datas, { ClassType: vmManager.filterClassType });
            }
            else {
                vmManager.dataSets = datas;
            }
        },
        detailsDisplay: false,
        init: function () {
            vmManager.dataSets = [];
            vmManager.dataSource = [];
        },
        msgModal: $modal({
            title: '信息提示',
            content: '请先选择要转班人员的数据！',
            templateUrl: leeHelper.modalTplUrl.msgModalUrl,
            show: false
        }),
    };

    $scope.vmManager = vmManager;

    var operate = Object.create(leeDataHandler.operateStatus);

    operate.loadData = function () {
        vmManager.init();
        $scope.promise = hrDataOpService.getAttendanceDatasOfToday(qryDto.Department).then(function (datas) {
            vmManager.dataSource = datas;
            vmManager.dataSets = _.clone(vmManager.dataSource);
        });

    };

    operate.preview = function () {
        vmManager.detailsDisplay = !vmManager.detailsDisplay;
    };

    $scope.operate = operate;
    operate.vm = qryDto;
    //部门树数据
    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        qryDto.Department = departmentTreeSet.treeNode.vm.DataNodeName;
        qryDto.DepartmentText = departmentTreeSet.treeNode.vm.DataNodeText;
    };
    $scope.ztree = departmentTreeSet;
    $scope.promise = connDataOpService.getDepartments().then(function (datas) {
        departmentTreeSet.setTreeDataset(datas);
    });
})
//班别设置管理
.controller('attendAskLeaveCtrl', function ($scope, $modal, hrDataOpService, dataDicConfigTreeSet, connDataOpService) {
    ///视图模型
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        Department: null,
        LeaveType: null,
        LeaveHours: null,
        LeaveTimeRegion: null,
        LeaveDescription: null,
        LeaveMark: 0,
        LeaveMemo:null,
        StartLeaveDate: null,
        EndLeaveDate: null,
        LeaveTimeRegionStart: null,
        LeaveTimeRegionEnd: null,
        DepartmentText: null,
        OpSign: null,
        ClassType: null,
        OpCmdVisible: -1,
        ///请假数据集
        LeaveDataSet: [],
        id:0,
    }
    var askLeaveVM = {
        AttendanceDate:null,
        SlotCardTime: null,
        LeaveType: null,
        LeaveHours: null,
        LeaveTimeRegion: null,
        LeaveDescription: null,
        LeaveMemo: null,
        LeaveMark: 0,
        WorkerId: null,
        WorkerName: null,
        Department: null,
        DepartmentText: null,
        OpCmdVisible: false,
        OpSign:null,
    };
    //视图管理器
    var vmManager = {
        activeTab: 'initTab',
        workerId: null,
        yearMonth:null,
        opSign:null,
        //部门信息
        departments:[],
        leaveTypes: [],
        changeDatas: [],
        //存储到数据库中的数据集
        dbDataSet:[],
        workerIdList: [],
        addWorkerId: function ($event) {
            if ($event.keyCode===13) {
                var item = _.findWhere(vmManager.changeDatas, { WorkerId:vmManager.workerId });
                if (item === undefined) {
                    item = {
                        WorkerId: _.clone(vmManager.workerId),
                        WorkerName: null,
                        Department: null,
                        LeaveType: null,
                        LeaveHours: null,
                        LeaveTimeRegion: null,
                        LeaveDescription: null,
                        LeaveMark: 0,
                        StartLeaveDate: null,
                        EndLeaveDate: null,
                        LeaveTimeRegionStart: null,
                        LeaveTimeRegionEnd: null,
                        DepartmentText: null,
                        ClassType: null,
                        OpSign: null,
                        OpCmdVisible: -1,
                        LeaveDataSet: [],
                    };
                    vmManager.changeDatas.push(item);
                    vmManager.workerIdList.push(item.WorkerId);
                    vmManager.workerId = null;
                }
            }
        },
        searchLeaveData: function ($event) {
            if ($event.keyCode === 13)
            {
                vmManager.askLeaveDatas = [];
                $scope.askLeavePromise = hrDataOpService.getAskLeaveDataAbout(vmManager.workerId,vmManager.yearMonth).then(function (datas) {
                    angular.forEach(datas, function (item) {
                        var dataItem = _.clone(askLeaveVM);
                        leeHelper.copyVm(item, dataItem);
                        dataItem.OpCmdVisible = true;
                        dataItem.DepartmentText = leeHelper.getDepartmentText(vmManager.departments, item.Department);
                        vmManager.askLeaveDatas.push(dataItem);
                        vmManager.workerId = null;
                    })
                });
            }
        },
        //请假数据
        askLeaveDatas:[],
    };
    $scope.vmManager = vmManager;

    //业务逻辑操作对象
    var operate = Object.create(leeDataHandler.operateStatus);
    operate.vm = uiVM;
    $scope.operate = operate;
    
 
    operate.search = function () {
        $scope.workerPromise = hrDataOpService.getEmployeeByWorkerIds(vmManager.workerIdList, 0).then(function (data) {
            angular.forEach(data, function (item) {
                var queryItem = _.find(vmManager.changeDatas, { WorkerId: item.WorkerId });
                if (queryItem !== undefined) {
                    if (queryItem.OpCmdVisible !== 0) {
                        leeHelper.copyVm(item, queryItem);
                        queryItem.OpCmdVisible = 1;
                        queryItem.WorkerName = item.Name;
                        queryItem.OpSign = "init";
                        queryItem.DepartmentText = leeHelper.getDepartmentText(vmManager.departments, item.Department);
                    }
                }
            })
        });
    };

    operate.editModal = $modal({
        title: "操作窗口",
        templateUrl: 'HR/AskLeaveEditSelectTpl/',
        controller: function ($scope) {
            $scope.vm = uiVM;
            $scope.leaveTypes = vmManager.leaveTypes;

            var op = Object.create(leeDataHandler.operateStatus);
            op.vm = uiVM;
            $scope.operate = op;

            $scope.save = function (isvalidate) {
                leeDataHandler.dataOperate.add(op, isvalidate, function () {
                    var leaveItem = {
                        WorkerId: null,
                        WorkerName: null,
                        Department: null,
                        LeaveType: null,
                        LeaveHours: null,
                        LeaveTimeRegion: null,
                        LeaveDescription: null,
                        LeaveMark: 0,
                        LeaveMemo: null,
                        StartLeaveDate: null,
                        EndLeaveDate: null,
                        LeaveTimeRegionStart: null,
                        LeaveTimeRegionEnd: null,
                        DepartmentText: null,
                        ClassType: null,
                        id: 0
                    };
                    var item = _.clone(uiVM);
                    if (vmManager.opSign === "handle" || vmManager.opSign === "add") {
                        item.LeaveMark = 1;
                        item.OpCmdVisible = 0;
                        var rowItem = _.find(vmManager.changeDatas, { WorkerId: item.WorkerId });
                        leeHelper.copyVm(item, rowItem);
                        leeHelper.copyVm(item, leaveItem);
                        rowItem.LeaveDataSet.push(leaveItem);
                        leaveItem.id = rowItem.LeaveDataSet.length;

                        var litem = _.findWhere(vmManager.dbDataSet, { WorkerId: item.WorkerId, LeaveType: item.LeaveType, StartLeaveDate: item.StartLeaveDate, EndLeaveDate: item.EndLeaveDate });
                        if (litem === undefined)
                            litem = _.clone(leaveItem);
                            vmManager.dbDataSet.push(litem);
                    }
                    else if (vmManager.opSign === 'edit') {
                        var rowItem = _.find(vmManager.changeDatas, { WorkerId: item.WorkerId });
                        leaveItem = _.find(rowItem.LeaveDataSet, { id: item.id });
                        leeHelper.copyVm(item, leaveItem);
                    }
                    else if (vmManager.opSign === 'handleEdit') {
                        var rowItem = _.find(vmManager.askLeaveDatas, { WorkerId: item.WorkerId, AttendanceDate: item.StartLeaveDate });
                        if (rowItem !== undefined) {
                            leeHelper.copyVm(item, rowItem);
                            rowItem.LeaveTimeRegion = item.LeaveTimeRegionStart + '--' + item.LeaveTimeRegionEnd;
                        }
                    }
                    operate.editModal.$promise.then(operate.editModal.hide);
                });
            };
        },
        show: false,
    });
    operate.handleAskForLeave = function (item, opSign) {
        leeHelper.clearVM(uiVM);
        vmManager.opSign = opSign;
        if (opSign === 'handle' || opSign==='edit')
        {
            leeHelper.copyVm(item, uiVM);
        }
        else if (opSign === 'add')
        {
            leeHelper.copyVm(item, uiVM, ['LeaveType', 'LeaveHours', 'StartLeaveDate', 'EndLeaveDate']);
        }
        else if (opSign === 'del')
        {
            var rowItem = _.findWhere(vmManager.changeDatas, { WorkerId: item.WorkerId });
            if (rowItem !== undefined)
            {
                leeHelper.remove(rowItem.LeaveDataSet, item);
                leeHelper.remove(vmManager.dbDataSet, item);
               
            }
        }
        else if (opSign == 'handleEdit')
        {
            leeHelper.copyVm(item, uiVM);
            uiVM.StartLeaveDate = item.AttendanceDate;
            uiVM.EndLeaveDate = item.AttendanceDate;
            uiVM.OpSign = opSign;
        }

        if (opSign !== 'del')
            operate.editModal.$promise.then(operate.editModal.show);
    };

    operate.save = function () {
        if (vmManager.activeTab === 'initTab')
        {
            hrDataOpService.handleAskForLeave(vmManager.dbDataSet).then(function (opResult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                    vmManager.changeDatas = [];
                    vmManager.dbDataSet = [];
                });
            });
        }
        else if (vmManager.activeTab === 'manageTab')
        {
            hrDataOpService.updateAskForLeave(vmManager.askLeaveDatas).then(function (opResult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                    vmManager.askLeaveDatas = [];
                });
            });
        }
    };

    $scope.promise = hrDataOpService.getLeaveTypesConfigs().then(function (datas) {
        var leaveTypes = _.where(datas, { ModuleName: "AttendanceConfig", AboutCategory: "AskForLeaveType" });
        if (leaveTypes !== undefined)
        {
            angular.forEach(leaveTypes, function (item) {
                vmManager.leaveTypes.push({ name: item.DataNodeText, text: item.DataNodeText });
            })
        }
        var departments = _.where(datas, { TreeModuleKey: "Organization" });
        if (departments !== undefined) {
            vmManager.departments = departments;
        }
    });
})
//考勤异常数据处理
.controller('attendExceptionHandleCtrl', function ($scope, $modal, hrDataOpService, dataDicConfigTreeSet, connDataOpService) {
 ///视图模型
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        Department: null,
        DepartmentText: null,
        AttendanceDate: null,
        SlotCardTime:null,
        LeaveHours: null,
        LeaveTimeRegion: null,
        SlotExceptionType: null,
        SlotExceptionMemo: null,
        Id_Key:0
    };
    var askLeaveVM = {
        WorkerId: null,
        WorkerName: null,
        LeaveType: null,
        LeaveHours: null,
        LeaveMemo: null,
        StartLeaveDate: null,
        EndLeaveDate: null,
        LeaveTimeRegionStart: null,
        LeaveTimeRegionEnd: null,
    }
    var slotCardVM = {
        WorkerId: null,
        WorkerName: null,
        AttendanceDate:null,
        SlotCardTime1: null,
        SlotCardTime2: null,
        ForgetSlotReason:null,
    };
    //视图管理器
    var vmManager = {
        activeTab: 'autoCheckExceptionTab',
        opSign: null,
        leaveTypes:[],
        //部门信息
        departments: [],
        //存储到数据库中的数据集
        dbDataSet: [],
        //异常数据集
        dataItems: [],
        //选定的项
        selectedItem:null,
        autoCheckExceptionData: function () {
            vmManager.dataItems = [];
            $scope.handlePromise = hrDataOpService.autoCheckExceptionSlotData().then(function (datas) {
                angular.forEach(datas, function (item) {
                    var dataItem = _.clone(uiVM);
                    leeHelper.copyVm(item, dataItem);
                    dataItem.DepartmentText = leeHelper.getDepartmentText(vmManager.departments, item.Department);
                    vmManager.dataItems.push(dataItem);
                })
            });
        },
        loadExceptionData: function () {
            vmManager.dbDataSet = [];
            $scope.loadPromise = hrDataOpService.getExceptionSlotData().then(function (datas) {
                angular.forEach(datas, function (item) {
                    var dataItem = _.clone(uiVM);
                    leeHelper.copyVm(item, dataItem);
                    dataItem.DepartmentText = leeHelper.getDepartmentText(vmManager.departments, item.Department);
                    vmManager.dbDataSet.push(dataItem);
                })
            });
        },
    };
    $scope.vmManager = vmManager;

    //业务逻辑操作对象
    var operate = Object.create(leeDataHandler.operateStatus);
    operate.vm = uiVM;
    $scope.operate = operate;

    operate.handleAskLeaveModal = $modal({
        title: "请假处理操作窗口",
        templateUrl: 'HR/AskLeaveEditSelectTpl/',
        controller: function ($scope) {
            $scope.vm = askLeaveVM;
            $scope.leaveTypes = vmManager.leaveTypes;
            var op = Object.create(leeDataHandler.operateStatus);
            op.vm = askLeaveVM;
            $scope.operate = op;
            $scope.save = function (isvalid) {
                leeDataHandler.dataOperate.add(op, isvalid, function () {
                    var dataitem = _.find(vmManager.dbDataSet, { Id_Key: vmManager.selectedItem.Id_Key });
                    if (dataitem !== undefined) {
                        leeHelper.copyVm($scope.vm, dataitem);
                        dataitem.OpSign = vmManager.opSign;
                        dataitem.HandleSlotExceptionStatus = 2;
                        dataitem.LeaveTimeRegion = askLeaveVM.LeaveTimeRegionStart + '--' + askLeaveVM.LeaveTimeRegionEnd;
                        dataitem.OpSign = vmManager.opSign;
                        dataitem.SlotExceptionMemo = '请假处理';
                    }
                    operate.handleAskLeaveModal.$promise.then(operate.handleAskLeaveModal.hide);
                });
            };
        },
        show: false,
    });
    operate.handleSlotCardModal = $modal({
        title: "增补刷卡时间窗口",
        templateUrl: 'HR/AddSlotCardTimeSelectTpl/',
        controller: function ($scope) {
            $scope.vm = slotCardVM;
            var op = Object.create(leeDataHandler.operateStatus);
            op.vm = slotCardVM;
            $scope.operate = op;
            $scope.save = function (isvalid) {
                leeDataHandler.dataOperate.add(op, isvalid, function () {
                    var dataitem = _.find(vmManager.dbDataSet, { Id_Key: vmManager.selectedItem.Id_Key });
                    if (dataitem !== undefined) {
                        leeHelper.copyVm($scope.vm, dataitem);
                        dataitem.OpSign = vmManager.opSign;
                        dataitem.HandleSlotExceptionStatus = 2;
                        dataitem.SlotCardTime1 = dataitem.AttendanceDate + " " + slotCardVM.SlotCardTime1;
                        dataitem.SlotCardTime2 = dataitem.AttendanceDate + " " + slotCardVM.SlotCardTime2;
                        dataitem.SlotCardTime = slotCardVM.SlotCardTime1 + ',' + slotCardVM.SlotCardTime2;
                        dataitem.SlotExceptionMemo = '补刷卡时间';
                    }
                    operate.handleSlotCardModal.$promise.then(operate.handleSlotCardModal.hide);
                });
            };
        },
        show: false,
    });

    var handleAttendExceptionData = function (exceptionMemo)
    {
        var dataitem = _.find(vmManager.dbDataSet, { Id_Key: vmManager.selectedItem.Id_Key });
        if (dataitem !== undefined) {
            dataitem.OpSign = vmManager.opSign;
            dataitem.SlotExceptionMemo = exceptionMemo;
            dataitem.HandleSlotExceptionStatus = 2;
        }
    }

    operate.requestAttendLateModal = $modal({
        title: '迟到处理确认窗口',
        content: '您确定为迟到吗？',
        templateUrl: '/HR/AbsentSelectTpl',
        controller: function ($scope) {
            $scope.save = function () {
                handleAttendExceptionData('迟到处理');
                operate.requestAttendLateModal.$promise.then(operate.requestAttendLateModal.hide);
            };
        },
        show: false
    });
    operate.requestAttendAbsentModal = $modal({
        title: '旷工处理确认窗口',
        content: '您确定为旷工吗？',
        templateUrl: '/HR/AbsentSelectTpl',
        controller: function ($scope) {
            $scope.save = function () {
                handleAttendExceptionData('旷工处理');
                operate.requestAttendAbsentModal.$promise.then(operate.requestAttendAbsentModal.hide);
            };
        },
        show: false
    });
    operate.handleExceptionSlotData = function (item, opSign) {
        vmManager.opSign = opSign;
        vmManager.selectedItem = _.clone(item);
        if (opSign === 'handleAskLeave') {
            leeHelper.copyVm(item, askLeaveVM);
            askLeaveVM.StartLeaveDate = item.AttendanceDate;
            askLeaveVM.EndLeaveDate = item.AttendanceDate;
            operate.handleAskLeaveModal.$promise.then(operate.handleAskLeaveModal.show);
        }
        else if (opSign === 'handleForgetSlot') {
            leeHelper.copyVm(item, slotCardVM);
            operate.handleSlotCardModal.$promise.then(operate.handleSlotCardModal.show);
        }
        else if (opSign === 'handleAbsent') {
            operate.requestAttendAbsentModal.$promise.then(operate.requestAttendAbsentModal.show);
        }
        else if (opSign === 'handleLate') {
            operate.requestAttendLateModal.$promise.then(operate.requestAttendLateModal.show);
        }
    };

    operate.save = function () {
        hrDataOpService.handleExceptionSlotData(vmManager.dbDataSet).then(function (opResult) {
            leeDataHandler.dataOperate.handleSuccessResult(operate, opResult, function () {
                vmManager.changeDatas = [];
                vmManager.dbDataSet = [];
            });
        });
    };

    $scope.promise = hrDataOpService.getLeaveTypesConfigs().then(function (datas) {
        var leaveTypes = _.where(datas, { ModuleName: "AttendanceConfig", AboutCategory: "AskForLeaveType" });
        if (leaveTypes !== undefined) {
            angular.forEach(leaveTypes, function (item) {
                vmManager.leaveTypes.push({ name: item.DataNodeText, text: item.DataNodeText });
            })
        }
        var departments = _.where(datas, { TreeModuleKey: "Organization" });
        if (departments !== undefined) {
            vmManager.departments = departments;
        }
    });
})

//厂服管理
.controller('workClothesManageCtrl', function ($scope, $modal, hrDataOpService,dataDicConfigTreeSet,connDataOpService) {
    ///厂服管理模型
    var uiVM = {
        WorkerId: null,
        WorkerName: null,
        Department: null,
        ProductName: null,
        ProductSpecify: null,
        ProductCategory: null,
        PerCount: 1,
        Unit: "件",
        InputDate: null,
        DealwithType: null,
        OpSign: 'add',
        Id_Key: null,
    }
    $scope.vm = uiVM;
    var originalVM = _.clone(uiVM);

    //查询字段
    var queryFields = {
        workerId: null,
        department: null,
        receiveMonth: null,
    };

    $scope.query = queryFields;

    var vmManager = {
        activeTab: 'initTab',
        isLocal:true,
        init: function () {
            if (uiVM.OpSign === 'add') {

            }
            else {
                uiVM = _.clone(originalVM);
            }
            uiVM.OpSign = 'add';
            $scope.vm = uiVM;
            vmManager.canEdit = false;
        },
        searchedWorkers: [],
        isSingle: true,//是否搜寻到的是单个人
        closeSpecifies:[],
        productNames: [
            {
                id: "夏季厂服", text: "夏季厂服", specifyList: [ { id: "38", text: "38" }, { id: "39", text: "39" },{ id: "40", text: "40" },{ id: "41", text: "41" },]
            },
            {
                id: "冬季厂服", text: "冬季厂服", specifyList: [{ id: "M", text: "M" }, { id: "L", text: "L" }, { id: "XL", text: "XL" }, { id: "XXL", text: "XXL" }, ] 
            },
        ],
        selectProductName: function () {
            var product = _.find(vmManager.productNames, { id: uiVM.ProductName });
            if (!angular.isUndefined(product))
            {
                vmManager.closeSpecifies = product.specifyList;
            }
        },
        dealwithTypes: [
            { id: "领取新衣", text: "领取新衣" },
            { id: "以旧换新", text: "以旧换新" },
            { id: "以旧换旧", text: "以旧换旧" },
        ],
        getWorkerInfo: function () {
            if (uiVM.WorkerId === undefined) return;
            var strLen = leeHelper.checkIsChineseValue(uiVM.WorkerId) ? 2 : 6;
            if (uiVM.WorkerId.length >= strLen) {
                vmManager.searchedWorkers = [];
                $scope.searchedWorkersPrommise = connDataOpService.getWorkersBy(uiVM.WorkerId).then(function (datas) {
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
                uiVM.WorkerName = worker.Name;
                uiVM.WorkerId = worker.WorkerId;
                uiVM.Department = worker.Department;
            }
            else {
                uiVM.Department = null;
            }
        },
        storeDataset: [],
        searchDataset:[],
        //选择领取衣服记录
        selectReceiveClothesRecord: function (item) {
            vmManager.canEdit = true;
            uiVM = _.clone(item);
            uiVM.OpSign = 'edit';
            $scope.vm = uiVM;
        },
        searchBy: function () {
           $scope.searchPromise=hrDataOpService.getWorkerClothesReceiveRecords(queryFields.workerId, queryFields.department, queryFields.receiveMonth, 1).then(function (datas) {
                vmManager.storeDataset = datas;
            });
        },
        getReceiveClothesRecords: function (mode) {
            hrDataOpService.getWorkerClothesReceiveRecords(queryFields.workerId, queryFields.department, queryFields.receiveMonth, mode).then(function (datas) {
                vmManager.searchDataset = datas;
            });
        },
    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.saveAll = function (isValid) {
        hrDataOpService.storeWorkerClothesReceiveRecord(uiVM).then(function (opresult) {
            leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                var mdl = _.clone(uiVM);
                mdl.Id_Key = opresult.Id_Key;
                if (mdl.OpSign === 'add') {
                    vmManager.storeDataset.push(mdl);
                }
                else (mdl.OpSign == 'edit')
                {
                    var item = _.find(vmManager.storeDataset, { Id_Key: uiVM.Id_Key });
                    leeHelper.copyVm(uiVM, item);
                }
                vmManager.init();
            });
        });
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.inti();
        });
    };


    $scope.promise = connDataOpService.getConfigDicData('Organization').then(function (datas) {
        departmentTreeSet.setTreeDataset(datas);
    });
    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        queryFields.department = dto.DataNodeText;
    };
    $scope.ztree = departmentTreeSet;

})
