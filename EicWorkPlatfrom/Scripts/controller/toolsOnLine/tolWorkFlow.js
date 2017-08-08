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
    return wfDataOp;
});
///内部联络单
officeAssistantModule.controller('wfInternalContactFormCtrl', function ($scope, dataDicConfigTreeSet, connDataOpService, wfDataOpService) {
    var ue = leeUeditor.getEditor('formContent');

    //流程参与者人员信息
    var participantInfo = $scope.participantInfo = {
        Applicant: null,//申请人,
        Confirmor: null,//确认人
    }
    //参与者信息模型
    var participantVM = {
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

    var dialog = $scope.dialog = leePopups.dialog();

    var vmManager = $scope.vmManager = {
        createFormId: function () {
            leePopups.inquire("创建表单编号", "系统自动为您创建表单编号号，您确定要继续作业吗？", function () {
                wfDataOpService.getInternalContactFormId('EIC').then(function (id) {
                    uiVM.FormId = id;
                });
            });
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
            participant.Role = vmManager.currentParticipantRole;
            leeWorkFlow.addParticipant(vmManager.participants, participant);
            participantInfo[vmManager.currentParticipantRole] = leeWorkFlow.getParticipantMappedRole(vmManager.participants, vmManager.currentParticipantRole);
        },
        dataset: [],
    };

    var operate = Object.create(leeDataHandler.operateStatus);
    $scope.operate = operate;
    operate.save = function (isValid) {
        leeDataHandler.dataOperate.add(operate, isValid, function () {
            uiVM.ParticipantInfo = JSON.stringify(vmManager.participants);
            uiVM.OpSign = leeDataHandler.dataOpMode.add;
            uiVM.FormContent = ue.getContent();
            leeHelper.setUserData(uiVM);
            $scope.opPromise = wfDataOpService.createInternalForm(uiVM).then(function (opresult) {
                leeDataHandler.dataOperate.handleSuccessResult(operate, opresult, function () {

                })
            });
        })
    };
    operate.refresh = function () { };


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
            angular.forEach(datas, function (dataitem) {
                var item = _.clone(participantVM);
                leeHelper.copyVm(dataitem, item);
                vmManager.dataset.push(item);
            });
        });
    };
    $scope.ztree = departmentTreeSet;


    $scope.show = function () {

    };
});