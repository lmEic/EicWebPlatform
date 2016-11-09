/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />
var hrModule = angular.module('bpm.hrApp');
hrModule.factory('hrArchivesDataOpService', function (ajaxService) {
    var archiveCtrl = "/HrArchivesManage/";
    var hrArchive = {};

    ///获取身份证信息
    hrArchive.getIdentityInfoBy = function (lastSixIdWord) {
        var url = archiveCtrl + "GetIdentityInfoBy";
        return ajaxService.getData(url, {
            lastSixIdWord: lastSixIdWord
        });
    };
    //获取作业工号
    hrArchive.getWorkerIdBy = function (workerIdNumType) {
        var url = archiveCtrl + "GetWorkerIdBy";
        return ajaxService.getData(url, {
            workerIdNumType: workerIdNumType
        });
    };
    //获取作业人员信息
    hrArchive.getWorkersInfo = function (vm, searchMode) {
        var url = archiveCtrl + "GetWorkersInfo";
        return ajaxService.getData(url, {
            workerId: vm.workerId,
            registedDateStart: vm.registedDateStart,
            registedDateEnd: vm.registedDateEnd,
            searchMode: searchMode
        });
    };

    ///获取档案信息配置数据
    hrArchive.getArchiveConfigDatas = function () {
        var url = archiveCtrl + "GetArchiveConfigDatas";
        return ajaxService.getData(url, {});
    };

    //获取该工号列表的所有人员信息
    //mode:0为部门或岗位信息
    //1:为学习信息;2：为联系方式信息
    hrArchive.getEmployeeByWorkerIds = function (workerIdList, mode) {
        var url = archiveCtrl + "FindEmployeeByWorkerIds";
        return ajaxService.getData(url, {
            workerIdList: workerIdList,
            mode: mode
        });
    };

    ///输入员工档案信息
    hrArchive.inputWorkerArchive = function (employee, oldEmployeeIdentity, opSign) {
        var url = archiveCtrl + "InputWorkerArchive";
        return ajaxService.postData(url, {
            employee: employee,
            oldEmployeeIdentity: oldEmployeeIdentity,
            opSign: opSign
        });
    };

    ///修改部门信息
    hrArchive.changeDepartment = function (changeDepartments) {
        var url = archiveCtrl + "ChangeDepartment";
        return ajaxService.postData(url, {
            changeDepartments: changeDepartments,
        });
    };

    ///修改岗位信息
    hrArchive.changePost = function (changePosts) {
        var url = archiveCtrl + "ChangePost";
        return ajaxService.postData(url, {
            changePosts: changePosts,
        });
    };

    ///修改学习信息
    hrArchive.changeStudy = function (studyInfos) {
        var url = archiveCtrl + "ChangeStudy";
        return ajaxService.postData(url, {
            studyInfos: studyInfos,
        });
    };

    ///修改联系方式信息
    hrArchive.changeTel = function (tels) {
        var url = archiveCtrl + "ChangeTelInfo";
        return ajaxService.postData(url, {
            tels: tels,
        });
    };

    //获取厂牌图片信息
    hrArchive.getWorkerCardImages = function () {
        var url = archiveCtrl + 'GetWorkerCardImages';
        return ajaxService.getData(url, {

        });
    };

    return hrArchive;
});
//-----------人员档案登记-----------------------
hrModule.controller('archiveInputCtrl', function ($scope, $modal, dataDicConfigTreeSet,hrArchivesDataOpService) {
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
                $scope.identityPromise = hrArchivesDataOpService.getIdentityInfoBy(employeeIdentity.IdentityID).then(function (datas) {
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
            hrArchivesDataOpService.getWorkerIdBy(employeeIdentity.WorkerIdNumType).then(function (data) {
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
    $scope.configPromise = hrArchivesDataOpService.getArchiveConfigDatas().then(function (datas) {
        
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
            hrArchivesDataOpService.inputWorkerArchive(employeeIdentity,pHelper.oldVm,archiveInput.opSign).then(function (opResult) {
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
});
hrModule.controller('arDepartmentChangeCtrl', function ($scope,hrArchivesDataOpService, dataDicConfigTreeSet, connDataOpService) {
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
        $scope.workerPromise = hrArchivesDataOpService.getEmployeeByWorkerIds(vmManager.workerIdList,0).then(function (data) {
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
        hrArchivesDataOpService.changeDepartment(vmManager.changeDatas).then(function (opResult) {
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
});
hrModule.controller('arPostChangeCtrl', function ($scope, hrArchivesDataOpService, dataDicConfigTreeSet, connDataOpService) {

    //视图管理器
    var vmManager = {
        postNatures: [{ name: '直接', text: '直接' }, { name: '间接', text: '间接' }],
        postNature: null,
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
        $scope.workerPromise = hrArchivesDataOpService.getEmployeeByWorkerIds(vmManager.workerIdList, 0).then(function (data) {
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
        hrArchivesDataOpService.changePost(vmManager.changeDatas).then(function (opResult) {
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
});
hrModule.controller('arStudyChangeCtrl', function ($scope, $modal, hrArchivesDataOpService, dataDicConfigTreeSet, connDataOpService) {
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
        id: 0
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
                        StudyDateFrom: null,
                        StudyDateTo: null,
                        SchoolName: null,
                        MajorName: null,
                        Qulification: null,
                        OpSign: null,
                        OpCmdVisible: false,
                        id: 0
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
        $scope.workerPromise = hrArchivesDataOpService.getEmployeeByWorkerIds(vmManager.workerIdList, 1).then(function (data) {
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
        templateUrl: 'HR/StudyEditSelectTpl/',
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
        hrArchivesDataOpService.changeStudy(vmManager.changeDatas).then(function (opResult) {
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
});
hrModule.controller('arTelChangeCtrl', function ($scope, $modal, hrArchivesDataOpService, dataDicConfigTreeSet, connDataOpService) {
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
        id: 0
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
                        id: 0
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
        $scope.workerPromise = hrArchivesDataOpService.getEmployeeByWorkerIds(vmManager.workerIdList, 2).then(function (data) {
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
        hrArchivesDataOpService.changeTel(vmManager.changeDatas).then(function (opResult) {
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
});
hrModule.controller('printCardCtrl', function ($scope, hrArchivesDataOpService) {
    var uiVM = {
        workerId: null,
        registedDateStart: new Date(),
        registedDateEnd: new Date(),
    };
    $scope.vm = uiVM;


    var vmManager = {
        dataSet: [],
        workerId: null,
        searchDatasBy: function (mode) {
            uiVM.workerId = vmManager.workerId;
            $scope.promise = hrArchivesDataOpService.getWorkersInfo($scope.vm, mode).then(function (datas) {
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
            var url = (item.PostNature === "直接") ? "../../Content/image/DirectCardTpl.jpg" : "../../Content/image/IndirectCardTpl.jpg";
            return url;
        },
    };
    $scope.vmManager = vmManager;


    $scope.printCard = function () {
        hrArchivesDataOpService.getWorkerCardImages().then(function (data) {
            var dd = {
                content: [

                    	{
                    	    image: data,
                    	    width: 200
                    	},
                    'Another paragraph, this time a little bit longer to make sure, this line will be divided into at least two lines'
                ]
            }
            pdfMake.createPdf(dd).open();
        })
    }
});
//离职管理控制器
hrModule.controller('arLeaveOffCtrl', function ($scope, hrArchivesDataOpService, connDataOpService) {
    ///离职视图模型
    var uiVM = {
        ID: null,
        WorkerId: null,
        WorkerName: null,
        Department: null,
        Post: null,
        LeaveDate: null,
        LeaveReason: null,
        Memo: null,
        OpPerson: null,
        OpDate: null,
        Id_Key: null,
    }
    $scope.vm = uiVM;
    var vmManager = {
        init: function () {

        },
        workerId:null,
        leaveReasonTypes: [],
        workerInfo:null,
        getWorker: function ($event) {
            if (event.keyCode == 13)
            {
                if (vmManager.workerId !== undefined && vmManager.workerId.length >= 6)
                {
                    $scope.searchPromise = connDataOpService.getWorkersBy(vmManager.workerId).then(function (datas) {
                        if (angular.isArray(datas) && datas.length > 0)
                        {
                            vmManager.workerInfo = datas[0];
                        }
                    });
                }
            }
        },

    };
    $scope.vmManager = vmManager;
    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.save = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {

        })
    };
    operate.refresh = function () { };

    $scope.promise = connDataOpService.loadConfigDicData('ArchiiveConfig', 'LeaveOff').then(function (datas) {
        vmManager.leaveReasonTypes = [];
        angular.forEach(datas, function (dataitem) {
            vmManager.leaveReasonTypes.push({ name: dataitem.DataNodeName, text: dataitem.DataNodeText });
        });
    });
    
})