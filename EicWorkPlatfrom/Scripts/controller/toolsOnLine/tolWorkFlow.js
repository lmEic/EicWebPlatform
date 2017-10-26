/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
var officeAssistantModule = angular.module('bpm.toolsOnlineApp');
officeAssistantModule.factory('wfDataOpService', function (ajaxService) {
    var wfDataOp = {};
    var wfUrlPrefix = '/' + leeHelper.controllers.TolWorkFlow + '/';
    //获取人员邮箱信息
    wfDataOp.getWorkerMails = function (department) {
        var url = wfUrlPrefix + 'GetWorkerMails';
        return ajaxService.getData(url, {
            department: department,
        });
    };
    //获取内部联络单号
    wfDataOp.getInternalContactFormId = function (department) {
        var url = wfUrlPrefix + 'GetInternalContactFormId';
        return ajaxService.getData(url, {
            department: department,
        });
    };
    //创建内部联络单
    wfDataOp.createInternalForm = function (entity) {
        var url =
            wfUrlPrefix + 'CreateInternalForm';
        return ajaxService.postData(url, {
            entity: entity,
        });
    };
    //上传内部联络单附件
    wfDataOp.uploadInternalContactFormAttachFile = function (file) {
        var url = wfUrlPrefix + 'UploadInternalContactFormAttachFile';
        return ajaxService.uploadFile(url, file);
    };
    return wfDataOp;
});
///内部联络单
officeAssistantModule.controller('wfInternalContactFormCtrl', function ($scope, ajaxService, dataDicConfigTreeSet, connDataOpService, wfDataOpService) {
    var editor = leeUeditor.createEditor('formContent');
    //流程参与者人员信息
    var participantInfo = $scope.participantInfo = {
        Applicant: null,//申请人,
        Confirmor: null,//确认人
    }
    //人员邮箱信息模型
    var workerEmailInfo = {
        WorkerId: null,
        Name: null,
        Department: null,
        Email: null,
        //参与者的角色
        Role: null,
        IsChecked: false
    };
    ///联络单视图模型
    var uiVM = {
        FormId: null,
        FormSubject: null,
        FormContent: null,
        ApplyDate: null,
        NeedDate: new Date(),
        ParticipantInfo: null,
        Department: null,
        YearMonth: null,
        OpDate: null,
        OpTime: null,
        OpPerson: null,
        OpSign: leeDataHandler.dataOpMode.add,
        Id_Key: null,
    }
    $scope.vm = uiVM;
    var initVM = _.clone(uiVM);

    ///表单附件模型
    var attachFileVM = {
        ModuleName: null,
        FormId: null,
        FileName: null,
        DocumentFilePath: null,
        OpSign: leeDataHandler.dataOpMode.uploadFile,
        Department: null,
        OpPerson: null,
    }

    var dialog = $scope.dialog = leePopups.dialog();
    var vmManager = $scope.vmManager = {
        isCreatedFormId: false,
        createFormId: function () {
            leePopups.inquire("创建表单编号", "系统自动为您创建表单编号号，您确定要继续作业吗？", function () {
                wfDataOpService.getInternalContactFormId('EIC').then(function (id) {
                    uiVM.FormId = id;
                    vmManager.isCreatedFormId = true;
                });
            });
        },
        init: function () {
            $scope.vm = uiVM = _.clone(initVM);
            vmManager.participants = [];
            leeHelper.clearVM(participantInfo, ["Applicant"]);
            $scope.uploadFileName = null;
            vmManager.isCreatedFormId = false;
            editor.clearContent();
        },
        participants: [],
        currentParticipantRole: null,
        //根据角色选择参与人员
        showParticipantView: function (role) {
            vmManager.dataset = [];
            vmManager.currentParticipantRole = role;
            dialog.show();
        },
        //选择参与人
        selectParticipant: function (participant) {
            participant.IsChecked = !participant.IsChecked;
            if (!participant.IsChecked) return;
            participant.Role = vmManager.currentParticipantRole;
            leeWorkFlow.addParticipant(vmManager.participants, participant);
            participantInfo[vmManager.currentParticipantRole] = leeWorkFlow.getParticipantMappedRole(vmManager.participants, vmManager.currentParticipantRole);
        },
        dataset: [],
    };

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.save = function (isValid) {
        if (!vmManager.isCreatedFormId) {
            leePopups.alert("对不起，请先创建表单单号！", 3);
            return;
        }
        if (!editor.hasContent()) {
            leePopups.alert("对不起，表单主题不能为空！", 3);
            return;
        }
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            uiVM.ParticipantInfo = JSON.stringify(vmManager.participants);
            uiVM.OpSign = leeDataHandler.dataOpMode.add;
            uiVM.FormContent = editor.getContent();
            leeHelper.setUserData(uiVM);
            $scope.opPromise = wfDataOpService.createInternalForm(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {
                    vmManager.init();
                })
            });
        })
    };
    operate.refresh = function () {
        leeDataHandler.dataOperate.refresh(operate, function () {
            vmManager.init();
        });
    };

    $scope.promise = connDataOpService.getConfigDicData('Organization').then(function (datas) {
        departmentTreeSet.setTreeDataset(datas);
        var user = leeLoginUser;
        participantInfo[leeWorkFlow.participantRole.Applicant] = leeWorkFlow.toParticipant(user);
    });
    var departmentTreeSet = dataDicConfigTreeSet.getTreeSet('departmentTree', "组织架构");
    departmentTreeSet.bindNodeToVm = function () {
        var dto = _.clone(departmentTreeSet.treeNode.vm);
        var department = dto.DataNodeText;
        vmManager.dataset = [];
        $scope.searchPromise = wfDataOpService.getWorkerMails(department).then(function (datas) {
            angular.forEach(datas, function (dataItem) {
                var item = _.clone(workerEmailInfo);
                leeHelper.copyVm(dataItem, item);
                vmManager.dataset.push(item);
            });
        });
    };
    $scope.ztree = departmentTreeSet;
    //上传文件
    $scope.selectFile = function (el) {
        leeHelper.upoadFile(el, function (fd) {
            var dto = leeWorkFlow.createFormFileAttachDto(attachFileVM, uiVM.FormId, "InternalContactForm");
            fd.append("attachFileDto", JSON.stringify(dto));
            wfDataOpService.uploadInternalContactFormAttachFile(fd).then(function (uploadResult) {
                if (uploadResult.Result) {
                    $scope.uploadFileName = uploadResult.FileName;
                }
            });
        });
    };





    var hw = $scope.hw = {
        postData: function () {
            // leePopups.alert("华为API数据对接", 1);
            $scope.opPromise = ajaxService.getData("/TolCooperateWithHw/GetManPower").then(function (rtndata) {
                leePopups.alert(JSON.stringify(rtndata));
            });
        },
    };
});