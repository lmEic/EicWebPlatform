/// <reference path="../../Content/ztree/dist/js/jquery.ztree.all.min.js" />
///数据处理器
/// <reference path="../../Content/pnotify/dist/pnotify.js" />
/// <reference path="../../Content/underscore/underscore-min.js" />
/// <reference path="../jquery-2.1.4.min.js" />
var leeDataHandler = (function () {
    var leeOperateStatus = {
        showValidation: false,//显示验证结果
        result: false,//操作结果标志
        message: '',//操作结果信息
        msgDisplay: false,//结果信息显示标志
        vm: null,
        //清空视图对象
        clearVm: function (vm, notKeys) {
            for (var key in vm) {
                if (_.isArray(notKeys)) {
                    if (!_.contains(notKeys, key)) {
                        vm[key] = null;
                    }
                }
                else {
                    vm[key] = null;
                }
            }
        }
    };
    ///数据操作
    var leeDataOperate = {
        ///添加操作行为
        add: function (opstatus, isvalid, addfn) {
            opstatus.showValidation = !isvalid;
            if (isvalid) {
                if (addfn !== undefined)
                    addfn();
            }
        },
        ///处理操作结果,opstatus:操作器，opresult:操作结果,successFn:成功回调函数
        handleSuccessResult: function (opstatus, opresult, successFn) {
            opstatus.showValidation = false;
            opstatus.result = opresult.Result;
            opstatus.message = opresult.Message;
            opstatus.msgDisplay = true;

            var msgtype = opresult.Result === true ? "success" : "error";
            new PNotify({
                title: "提示",
                text_escape: true,
                text: opresult.Message,
                type: msgtype,
                delay: 3000,
                styling: 'brighttheme',
                addclass: "stack-bar-top",
                cornerclass: "",
                width: "82%",
                stack: { "dir1": "down", "dir2": "right", "push": "top", "spacing1": 0, "spacing2": 0 },
                after_close: function (notice, timer_hide) {
                    opstatus.msgDisplay = false;
                }
            });

            if (opresult.Result === true) {
                if (successFn !== undefined && _.isFunction(successFn))
                    successFn();
            }
            else {
                if (opresult.ExceptionId !== null && opresult.ExceptionId.length > 1) {
                    var loadUrl = "/Account/GetExceptionFile?exceptionId=" + opresult.ExceptionId;
                    return loadUrl;
                }
            }
        },
        ///显示信息
        displayMessage: function (opstatus, opresult, message) {
            if (message == undefined) {
                opstatus.message = opresult.Message;
            }
            else {
                opstatus.message = message;
            }
            var msgtype = opresult.Result === true ? "success" : "error";
            new PNotify({
                title: "提示",
                text_escape: true,
                text: opresult.Message,
                type: msgtype,
                delay: 3000,
                styling: 'brighttheme',
                addclass: "stack-bar-top",
                cornerclass: "",
                width: "82%",
                stack: { "dir1": "down", "dir2": "right", "push": "top", "spacing1": 0, "spacing2": 0 },
                after_close: function (notice, timer_hide) {
                    opstatus.msgDisplay = false;
                }
            });
        },
        ///刷新操作
        refresh: function (opstatus, cancelfn) {
            opstatus.showValidation = false;
            opstatus.msgDisplay = false;
            opstatus.result = false;
            if (cancelfn !== undefined) {
                cancelfn();
            }
        }
    };
    ///数据存储
    var leeDataStorage = {
        ///存储登录用户信息
        setLoginedUser: function (userDatas) {
            if (_.isObject(userDatas)) {
                localStorage.setItem("loginUser", JSON.stringify(userDatas));
            }
        },
        //获取登录用户信息,本地有存储，则返回用户对象，
        //没有，则返回null
        getLoginedUser: function () {
            var loginedUser = {
                //账号
                userId: null,
                //姓名
                userName: null,
                //头像
                headPortrait: null,
                //部门
                department: null,
                //组织
                organization: {
                    //课级
                    K: null,
                    //部级
                    B: null,
                    //处级
                    C: null
                },
                //网站物理路径
                webSitePhysicalApplicationPath: null,
                serverName: null
            };
            var loginInfoJson = localStorage.getItem("loginUser");
            if (!_.isUndefined(loginInfoJson) && !_.isNull(loginInfoJson)) {
                var loginInfo = JSON.parse(loginInfoJson);
                if (_.isObject(loginInfo)) {
                    //保存用户信息
                    if (loginInfo.loginUser !== undefined && loginInfo.loginUser !== null) {
                        var user = loginInfo.loginUser;
                        if (user.LoginedUser !== null) {
                            loginedUser.userId = user.LoginedUser.UserId;
                            loginedUser.userName = user.LoginedUser.UserName;

                            loginedUser.headPortrait = user.LoginedUser.HeadPortrait;
                            if (!_.isUndefined(user.LoginedUser.Department))
                                loginedUser.department = user.LoginedUser.Department;
                            if (!_.isUndefined(user.LoginedUser.DepartmentText))
                                loginedUser.departmentText = user.LoginedUser.DepartmentText;
                            if (!_.isUndefined(user.LoginedUser.Organizetion)) {
                                var fds = user.LoginedUser.Organizetion.split(',');
                                var organization;
                                if (fds.length === 1) {
                                    organization = { K: user.LoginedUser.Organizetion, B: user.LoginedUser.Organizetion, C: user.LoginedUser.Organizetion };
                                }
                                else if (fds.length === 2) {
                                    organization = { K: fds[0], B: fds[0], C: fds[1] };
                                }
                                else if (fds.length === 3) {
                                    organization = { K: fds[0], B: fds[1], C: fds[2] };
                                }
                                loginedUser.organization = organization;
                            }
                        }

                    }
                    //保存站点信息
                    if (loginInfo.webSite !== undefined && loginInfo.webSite !== null) {
                        var webSite = loginInfo.webSite;
                        loginedUser.webSitePhysicalApplicationPath = webSite.PhysicalApplicationPath;
                        loginedUser.serverName = webSite.ServerName;
                    }
                    return loginedUser;
                }
            }
            return null;
        }
    };
    ///数据操作类型
    var leeDataOpMode = {
        none: 'none',
        add: 'add',
        edit: 'edit',
        update: 'update',
        delete: 'delete',
        uploadFile: 'uploadFile',
        deleteFile: 'deleteFile',
        IdKey: 'Id_Key'
    };
    return {
        ///操作状态
        operateStatus: leeOperateStatus,
        ///数据操作行为
        dataOperate: leeDataOperate,
        ///本地数据存储
        dataStorage: leeDataStorage,
        ///数据操作类型
        dataOpMode: leeDataOpMode
    };
})();
//常用操作助手
var leeHelper = (function () {
    var modalTpl = {
        //消息提示窗口
        msgModalUrl: '/CommonTpl/InfoMsgModalTpl',
        //删除提示窗口
        deleteModalUrl: '/CommonTpl/DeleteModalTpl',
        //文件预览模态窗口
        imgFilePreviewModalUrl: '/CommonTpl/ImageFilePreviewTpl'
    };
    var commonTplUrl = {
        //树组件选择窗口
        treeSelectTplUrl: '/CommonTpl/TreeSelectTpl'
    };
    var controllerNames = {
        //设备管理
        equipment: 'Equipment',
        systemManage: 'EicSystemManage',
        configManage: 'SysConfig',
        //进程管理
        itilManage: 'SysITIL',
        //生产看板
        productBoard: 'ProBoard',
        //日报管理
        dailyReport: 'ProDailyReport',
        //工单管理
        mocManage: 'ProMocManage',
        //采购管理
        purchaseManage: 'Purchase',
        //采购供应商管理
        supplierManage: 'PurSupplierManage',
        //质量管理控制器
        quality: 'Quality',
        //质量抽样检验控制器
        quaInspectionManage: 'QuaInspectionManage',
        //质量RMA控制器
        quaRmaManage: 'QuaRmaManage',
        //质量8D控制器
        qua8DManage: 'Qua8DManage',
        //办公助手控制器
        TolOfficeAssistant: 'TolOfficeAssistant',
        //工作流电子签核控制器
        TolWorkFlow: 'TolWorkFlow',
        ///在线工具
        ToolsOnLine: 'ToolsOnLine',
        //华为协同
        TolCooperateWithHw: 'TolCooperateWithHw'

    };
    return {
        ///控制器名称
        controllers: controllerNames,
        ///清空视图对象的每个属性值
        //vm:视图对象
        //notKeys:不包含的要清楚值的键值
        //defValue:清除后，设定每个字段的默认值
        clearVM: function (vm, notKeys, defValue) {
            for (var key in vm) {
                if (_.isArray(notKeys)) {
                    if (!_.contains(notKeys, key)) {
                        if (defValue !== undefined)
                            vm[key] = defValue;
                        else
                            vm[key] = null;
                    }
                }
                else {
                    if (defValue !== undefined)
                        vm[key] = defValue;
                    else
                        vm[key] = null;
                }
            }
        },
        ///将srcObj对象中各个属性值赋值到desObj中去
        ///notKeys表示不包含要复制的键值集合
        copyVm: function (srcObj, desObj, notKeys) {
            if (srcObj === undefined) return;
            for (var key in desObj) {
                if (_.isArray(notKeys)) {
                    if (!_.contains(notKeys, key)) {
                        if (srcObj[key] !== undefined)
                            desObj[key] = srcObj[key];
                    }
                }
                else {
                    if (srcObj[key] !== undefined)
                        desObj[key] = srcObj[key];
                }
            }
        },
        ///从数组中移除指定选项,Item对象与数组中的对象需要一样，即每个属性值都没有变化的情况
        remove: function (ary, item) {
            if (!Array.isArray(ary)) return;
            for (var i = 0, len = ary.length; i < len; i++) {
                if (ary[i] === item) {
                    ary.splice(i, 1);
                    break;
                }
            }
        },
        ///从数组中删除指定选项,Item对象中必须包含Id属性
        delWithId: function (ary, item) {
            if (!Array.isArray(ary)) return;
            var data = _.find(ary, { Id: item.Id });
            if (data !== undefined)
                leeHelper.remove(ary, data);
        },
        ///将Item插入到数组ary中，Item对象中必须包含Id属性
        insertWithId: function (ary, item) {
            if (!Array.isArray(ary)) return;
            var data = _.find(ary, { Id: item.Id });
            if (data === undefined)
                ary.push(item);
        },
        ///数组中是否存在含有Id属性的item
        isExist: function (ary, item) {
            if (!Array.isArray(ary)) return false;
            var data = _.findWhere(ary, { Id: item.Id });
            return data !== undefined;
        },
        ///在数组指定位置插入项
        insert: function (ary, index, item) {
            ary.splice(index, 0, item);
        },
        ///清空数组
        emptyAry: function (ary) {
            ary = [];
            ary.length = 0;
        },
        ///根据所给属性，获取该属性组合的唯一值
        getUniqDatas: function (ary, uniqProperties) {
            if (!_.isArray(ary)) return;
            var keys = [];
            var datas = [];
            _.forEach(ary, function (item) {
                var ckey = null;
                if (_.isArray(uniqProperties)) {
                    _.forEach(uniqProperties, function (fieldName) {
                        ckey = ckey + "_" + item[fieldName];
                    });
                }
                else {
                    ckey = item[uniqProperties];
                }
                if (_.isUndefined(keys[ckey])) {
                    keys[ckey] = true;
                    datas.push(item);
                }
            });
            return datas;
        },
        ///模态窗口模板URL路径对象
        modalTplUrl: modalTpl,
        ///获取部门名称
        getDepartmentText: function (datas, department) {
            var departmentItem = _.findWhere(datas, { DataNodeName: department });
            if (departmentItem !== undefined) {
                return departmentItem.DataNodeText;
            }
        },
        ///获取转变后的部门人员信息
        getWorkersAboutChangedDepartment: function (workerDatas, departments) {
            angular.forEach(workerDatas, function (item) {
                var dep = _.find(departments, { DataNodeName: item.Department });
                if (dep !== undefined) {
                    item.Department = dep.DataNodeText;
                }
            });
            return workerDatas;
        },
        ///判断是否是中文
        checkIsChineseValue: function (val) {
            var reg = new RegExp("[\\u4E00-\\u9FFF]+", "g");
            return reg.test(val);
        },
        ///读入图片文件并预览 imgId:图片控件Id,file：file元素
        readFile: function (imgId, file) {
            var reader = new FileReader();
            var img = document.getElementById(imgId);
            //载入事件
            reader.onload = (function (aimg) {
                return function (e) {
                    aimg.src = e.target.result;
                };
            })(img);
            reader.readAsDataURL(file);
        },
        ///上传文件
        upoadFile: function (el, handler) {
            var files = el.files;
            if (files.length > 0) {
                var file = files[0];
                var fd = new FormData();
                fd.append('file', file);
                if (handler && _.isFunction(handler)) {
                    fd.name = file.name;
                    handler(fd);
                }
            }
        },
        ///上传多个文件
        upoadFiles: function (el, handler) {
            var files = el.files;
            var fd = new FormData();
            var fileNameList = [];
            if (files.length > 0) {
                _.forEach(files, function (file) {
                    fd.append('files', file);
                    fileNameList.push(file.name);
                });
                if (handler && _.isFunction(handler)) {
                    fd.fileNameList = fileNameList;
                    handler(fd);
                }
            }
        },
        ///设置用户数据
        setUserData: function (uiVm) {
            var user = leeDataHandler.dataStorage.getLoginedUser();
            if (user !== null) {
                if (uiVm.OpPerson !== undefined) {
                    uiVm.OpPerson = user.userName;
                }
                if (uiVm.Department !== undefined) {
                    uiVm.Department = user.department;
                }
            }
        },
        //获取用户组织信息
        getUserOrganization: function () {
            var user = leeDataHandler.dataStorage.getLoginedUser();
            if (user !== null) {
                return user.organization;
            }
            return "";
        },
        ///打印视图
        printView: function (elId) {
            if (confirm('确定打印吗？')) {
                var newstr = document.getElementById(elId).innerHTML;
                var printWindow = window.open();
                printWindow.document.write(newstr);
                printWindow.print();
                return false;
            }
        },
        //将小数转换为百分比
        toPercent: function (data, len) {
            var strData = (parseFloat(data) * 100).toFixed(len);
            var ret = strData.toString() + "%";
            return ret;
        },
        //对数组对象进行排序
        sortArrOfObjectsByPtName: function (arrToSort, strObjPropertyName, sortAscending) {
            if (sortAscending === undefined) sortAscending = true;  // default to true
            if (sortAscending) {
                //arrToSort.sort(function (a, b) {
                //    return a[strObjPropertyName].toLowerCase() > b[strObjPropertyName].toLowerCase();
                //});
                return _.sortBy(arrToSort, strObjPropertyName);
            }
            else {
                //arrToSort.sort(function (a, b) {
                //    return a[strObjPropertyName].toLowerCase() < b[strObjPropertyName].toLowerCase();
                //});
                //return _(arrToSort).sortBy(function (a) { return -a[strObjPropertyName] });
                return _.sortBy(arrToSort, strObjPropertyName).reverse();
            }
        },
        ///根据总数量和列数量创建输入数据源
        ///totalCount：总数量,colCount:列数量
        ///defaultDatas:默认传入的数据列表
        ///handler：处理句柄
        createDataInputs: function (totalCount, colCount, defaultDatas, handler) {
            var inputDatas = [];
            var id = 0;
            var modData = totalCount % colCount;
            var rows = parseInt(totalCount / colCount);
            var rowItem, colItem, len, colIndex;
            for (var rowIndex = 1; rowIndex <= rows; rowIndex++) {
                rowItem = { rowId: rowIndex, cols: [] };
                for (colIndex = 1; colIndex <= colCount; colIndex++) {
                    id += 1;
                    colItem = { index: id, rowId: rowIndex, colId: colIndex, indata: null, focus: false, nextColId: colIndex + 1, result: true };
                    if (_.isArray(defaultDatas))//检测传入的数据是否是数组
                    {
                        len = defaultDatas.length + 1;
                        if (id <= len) {
                            var idata = defaultDatas[id - 1];
                            if (idata !== undefined && idata !== "")
                                colItem.indata = idata;
                            if (_.isFunction(handler) && colItem.indata !== null)
                                handler(colItem);
                        }
                    }
                    if (colIndex === colCount) {
                        colItem.rowId += 1;
                        colItem.nextColId = 1;
                    }
                    if (id === totalCount) {
                        colItem.nextColId = "last";
                    }
                    rowItem.cols.push(colItem);
                }
                if (rowIndex === rows) {
                    rowIndex = rows + 1;
                }
                inputDatas.push(rowItem);
            }
            //添加余数部分数据
            rowItem = { rowId: rows + 1, cols: [] };
            for (colIndex = 1; colIndex <= modData; colIndex++) {
                id += 1;
                colItem = { index: id, rowId: rowItem.rowId, colId: colIndex, indata: null, focus: false, nextColId: colIndex + 1, result: true };
                if (colIndex === modData) {
                    colItem.nextColId = "last";
                }
                if (_.isArray(defaultDatas))//检测传入的数据是否是数组
                {
                    len = defaultDatas.length + 1;
                    if (id <= len) {
                        colItem.indata = defaultDatas[id - 1];
                        if (_.isFunction(handler) && colItem.indata !== null)
                            handler(colItem);
                    }
                }
                rowItem.cols.push(colItem);
            }
            inputDatas.push(rowItem);
            return inputDatas;
        },
        ///max规格上限,min规格下限,targetValue目标值，compareSign比较操作符
        checkValue: function (max, min, targetValue, compareSign) {
            return targetValue >= min && targetValue <= max;
        },
        ///设置网站标题
        setWebSiteTitle: function (title, subTitle) {
            document.title = title + "---【" + subTitle + "】";
        },
        //获取文件后缀名 fileName:包含后缀名的文件名
        getFileExtensionIcon: function (fileName) {
            var fileIcon = "fa fa-file-pdf-o";
            var index1 = fileName.lastIndexOf('.');
            var index2 = fileName.length;
            var postf = fileName.substring(index1, index2).toLowerCase();
            if (postf === ".pdf") {
                fileIcon = "fa fa-file-pdf-o";
            }
            else if (postf === ".txt") {
                fileIcon = "fa fa-file-text";
            }
            else if (postf === ".doc" || postf === ".docx") {
                fileIcon = "fa fa-file-word-o";
            }
            else if (postf === ".xls" || postf === ".xlsx") {
                fileIcon = "fa fa-file-excel-o";
            }
            else if (postf === ".ppt" || postf === ".pptx") {
                fileIcon = "fa fa-file-powerpoint-o";
            }
            else if (postf === ".jpg" || postf === ".jpeg" || postf === ".bpm" || postf === ".png") {
                fileIcon = "fa fa-file-image-o";
            }
            return fileIcon;
        },
        //生成全局唯一标识符
        newGuid: function () {
            var d = new Date().getTime();
            var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                var r = (d + Math.random() * 16) % 16 | 0;
                d = Math.floor(d / 16);
                return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
            });
            return uuid;
        },
        //给对象设置唯一键值Id
        setObjectGuid: function (obj) {
            if (_.isUndefined(obj.Id)) {
                obj.Id = 0;
            }
            obj.Id = leeHelper.newGuid();
        },
        //给对象设置服务器标志，标志该对象是从服务器端传来的数据
        setObjectServerSign: function (obj) {
            if (_.isUndefined(obj.isServer)) {
                obj.isServer = true;
            }
            obj.isServer = true;
        },
        //给对象设置客户端标志，标志该对象是从客户端端本地产生的数据
        setObjectClentSign: function (obj) {
            if (_.isUndefined(obj.isServer)) {
                obj.isServer = false;
            }
            obj.isServer = false;
        },
        //判断是否是服务器端数据对象
        isServerObject: function (obj) {
            if (_.isUndefined(obj.isServer)) return false;
            return obj.isServer;
        }
    };
})();
// 弹出框助手
var leePopups = (function () {
    var mmPopup = {
        //对话框
        dialog: function (title, content) {
            return new myDialog(title, content);
        },
        //提醒信息,type：消息类型 1：info;2:notice;3:error;4:success
        alert: function (text, type) {
            var infoType;
            if (type === 1)
                infoType = "info";
            else if (type === 2)
                infoType = "notice";
            else if (type === 3)
                infoType = "error";
            else if (type === 4)
                infoType = "success";
            else
                infoType = "notice";

            new PNotify({
                title: "提示",
                text_escape: true,
                text: text,
                type: infoType,
                delay: 5000,
                width: '460px',
                styling: 'brighttheme',
                addclass: 'stack-modal',
                stack: { "dir1": "down", "dir2": "right", "push": "top", "modal": true, "overlay_close": true },
            });
        },
        //错误信息提示确认对话框，title:标题;text:文本内容;okFn:确认函数；cancelFn:取消Fn
        confirm: function (title, text, okFn, cancelFn) {
            (new PNotify({
                title: title,
                text: text,
                icon: 'glyphicon glyphicon-question-sign',
                hide: false,
                type: 'error',
                width: '460px',
                styling: 'brighttheme',
                confirm: {
                    confirm: true,
                    buttons: [
                      {
                          text: '确   定',
                          addClass: 'btn-info',
                          click: function (notice) {
                              if (!_.isUndefined(okFn) && _.isFunction(okFn))
                                  okFn();
                              notice.remove();
                          }
                      },
                      {
                          text: '取   消',
                          addClass: 'btn-default',
                          click: function (notice) {
                              if (!_.isUndefined(cancelFn) && _.isFunction(cancelFn))
                                  cancelFn();
                              notice.remove();
                          }
                      },
                    ]
                },
                buttons: {
                    closer: false,
                    sticker: false
                },
                history: {
                    history: false
                },
                addclass: 'stack-modal',
                stack: { 'dir1': 'down', 'dir2': 'right', 'modal': true }
            }));
        },
        //消息询问对话框，title:标题;text:文本内容;okFn:确认函数；cancelFn:取消Fn
        inquire: function (title, text, okFn, cancelFn) {
            (new PNotify({
                title: title,
                text: text,
                icon: 'glyphicon glyphicon-question-sign',
                hide: false,
                type: 'info',
                width: '460px',
                styling: 'brighttheme',
                confirm: {
                    confirm: true,
                    buttons: [
                      {
                          text: '确   定',
                          addClass: 'btn-info',
                          click: function (notice) {
                              if (!_.isUndefined(okFn) && _.isFunction(okFn))
                                  okFn();
                              notice.remove();
                          }
                      },
                      {
                          text: '取   消',
                          addClass: 'btn-default',
                          click: function (notice) {
                              if (!_.isUndefined(cancelFn) && _.isFunction(cancelFn))
                                  cancelFn();
                              notice.remove();
                          }
                      },
                    ]
                },
                buttons: {
                    closer: false,
                    sticker: false
                },
                history: {
                    history: false
                },
                addclass: 'stack-modal',
                stack: { 'dir1': 'down', 'dir2': 'right', 'modal': true }
            }));
        },
    };
    function myDialog(title, content) {
        this.open = false;
        this.title = title;
        this.content = content;
    };
    myDialog.prototype.show = function () { this.open = true; };
    myDialog.prototype.close = function () { this.open = false; };
    return mmPopup;
})();
// 登陆用户
var leeLoginUser = (function () {
    var user = {
        //账号
        userId: null,
        //姓名
        userName: null,
        //部门
        department: null,
        //部门标题名称
        departmentText: null,
        ///个人头像
        headPortrait: "../Content/login/profilepicture.jpg",
        ///载入个人头像
        loadHeadPortrait: function () {
            var loginUser = leeDataHandler.dataStorage.getLoginedUser();
            if (loginUser !== null) {
                user.userId = loginUser.userId;
                user.userName = loginUser.userName;
                user.department = loginUser.department;
                user.departmentText = loginUser.departmentText;
            }
            user.headPortrait = loginUser === null ? '../Content/login/profilepicture.jpg' : loginUser.headPortrait;
        },
    };
    return user;
})();
//电子流程操作模块助手
var leeWorkFlow = (function () {
    var convertToParticipant = function (participant) {
        if (participant.hasOwnProperty("userName") && participant.hasOwnProperty("departmentText")) {
            return participant.userName + "(" + participant.departmentText + ")";
        }
        else if (participant.hasOwnProperty("Name") && participant.hasOwnProperty("Department")) {
            return participant.Name + "(" + participant.Department + ")";
        }
        else {
            return "";
        }
    };

    return {
        //参与者角色
        participantRole: {
            Approver: "Approver",//核准人
            Confirmor: "Confirmor",//确认人
            Applicant: "Applicant",//申请人
        },
        //转换成参与者的信息
        toParticipant: convertToParticipant,
        //将多个参与者信息连接成字符串形式
        concatParticipant: function (participants) {
            if (_.isArray(participants)) {
                var persons = [];
                _.forEach(participants, function (p) {
                    var pstr = convertToParticipant(p);
                    persons.push(pstr);
                })
                return persons.join('|');
            }
            return "";
        },
        //获取对应角色的参与者信息
        getParticipantMappedRole(participants, role) {
            var datas = _.where(participants, { Role: role });
            if (datas.length > 0) {
                return leeWorkFlow.concatParticipant(datas);
            }
            return "";
        },
        //将参与者添加到集合中
        addParticipant: function (dataset, participant) {
            var actor = _.clone(participant);
            var item = _.find(dataset, { WorkerId: participant.WorkerId, Role: actor.Role });
            if (item === undefined) {
                delete actor.IsChecked;
                dataset.push(actor);
            };
        },
        //创建表单附件Dto
        createFormFileAttachDto: function (vm, formId, moduleName) {
            var dto = _.clone(vm);
            leeHelper.setUserData(dto);
            dto.FormId = formId;
            dto.ModuleName = moduleName;
            return dto;
        }
    };
})();
///zTree 助手
var leeTreeHelper = (function () {
    var ztree = {
        //添加子节点
        addChildrenNode: function (treeId, parentNode, newNode) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            newNode = treeObj.addNodes(parentNode, newNode);
        },
        //添加同级节点
        addNode: function (treeId, currentNode, newNode) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            var parentNode = currentNode.getParentNode();
            newNode = treeObj.addNodes(parentNode, newNode);
        },
        //移除节点
        removeNode: function (treeId, treeNode) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            treeObj.removeNode(treeNode);
        },
        //修改节点
        updateNode: function (treeId, treeNode) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            treeObj.updateNode(treeNode);
        },
        //获取树的所有节点
        getTreeNodes: function (treeId) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            return treeObj.getNodes();
        },
        //checkTypeFlag = true 表示按照 setting.check.chkboxType 属性进行父子节点的勾选联动操作
        //checkTypeFlag = false 表示只修改此节点勾选状态，无任何勾选联动操作
        //callbackFlag = true 表示执行此方法时触发 beforeCheck & onCheck 事件回调函数
        //callbackFlag = false 表示执行此方法时不触发事件回调函数
        checkNode: function (treeId, treeNode, checked, checkTypeFlag, callbackFlag) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            treeObj.checkNode(treeNode, checked, checkTypeFlag, callbackFlag);
        },
        //勾选 或 取消勾选 全部节点
        checkAllNodes: function (treeId, checked) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            treeObj.checkAllNodes(checked);
        },
        //将 zTree 使用的标准 JSON 嵌套格式的数据转换为简单 Array 格式
        transformToArray: function (treeId, treeNodes) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            return treeObj.transformToArray(treeNodes);
        }
    };
    return ztree;
})();
///百度Html ueditor编辑助手
var leeUeditor = (function () {
    //公共配置
    var commonConfig = {
        toolbars: [
            [
                'fontfamily', //字体
                'fontsize', //字号
                'forecolor', //字体颜色
                'bold', //加粗
                'italic', //斜体
                'underline', //下划线
                'strikethrough', //删除线
                'touppercase', //字母大写
                'tolowercase', //字母小写
                'subscript', //下标
                'fontborder', //字符边框
                'superscript', //上标
                'spechars', //特殊字符
                'emotion', //表情
                '|',
                'customstyle', //自定义标题
                'paragraph', //段落格式
                'justifyleft', //居左对齐
                'justifyright', //居右对齐
                'justifycenter', //居中对齐
                'justifyjustify', //两端对齐
                'insertorderedlist', //有序列表
                'insertunorderedlist', //无序列表
                'directionalityltr', //从左向右输入
                'directionalityrtl', //从右向左输入
                'rowspacingtop', //段前距
                'rowspacingbottom', //段后距
                'indent', //首行缩进
                'pagebreak', //分页
                //'insertframe', //插入Iframe
                // 'imagenone', //默认
                // 'imageleft', //左浮动
                // 'imageright', //右浮动
                // 'attachment', //附件
                //'imagecenter', //居中
                //'wordimage', //图片转存
                'lineheight', //行间距
                'edittip ', //编辑提示
                'autotypeset', //自动排版
            ],
            [
              'undo', //撤销
              'redo', //重做
              'selectall', //全选
              'pasteplain', //纯文本粘贴模式
              'cleardoc', //清空文档
              'horizontal', //分隔线
              'formatmatch', //格式刷
              'removeformat', //清除格式
              'blockquote', //引用
              'searchreplace', //查询替换
              '|',
               'print', //打印
               'preview', //预览
               'time', //时间
               'date', //日期
               'anchor', //锚点
               'link', //超链接
               'unlink', //取消链接
               'snapscreen', //截图
               'backcolor', //背景色
               'background', //背景
               'template', //模板
               // 'source', //源代码
               '|',
               'inserttable', //插入表格
               'insertrow', //前插入行
               'insertcol', //前插入列
               'mergeright', //右合并单元格
               'mergedown', //下合并单元格
               'deleterow', //删除行
               'deletecol', //删除列
               'splittorows', //拆分成行
               'splittocols', //拆分成列
               'splittocells', //完全拆分单元格
               'deletecaption', //删除表格标题
               'inserttitle', //插入标题
               'mergecells', //合并多个单元格
               'deletetable', //删除表格
               'insertparagraphbeforetable', //"表格前插入行"
               'edittable', //表格属性
               'edittd', //单元格属性
               // 'insertcode', //代码语言
               //'simpleupload', //单图上传
               // 'insertimage', //多图上传
               //'map', //Baidu地图
               //'gmap', //Google地图
               //'insertvideo', //视频
               '|',
               'help', //帮助
                // 'webapp', //百度应用
               'fullscreen', //全屏
               //'scrawl', //涂鸦
               // 'music', //音乐
               //'drafts', // 从草稿箱加载
               // 'charts', // 图表
            ]
        ],
        autoHeightEnabled: true,
        autoFloatEnabled: true,
        elementPathEnabled: false,
    };

    var myEditor = {
        ///公共默认配置
        commonConfig: commonConfig,
        //编辑器
        createEditor: function (id) {
            var editor = new ueEditor(id);
            editor.createInstance();
            return editor;
        },
    };
    function ueEditor(id) {
        //控件Id
        this.id = id;
        //控件实例
        this.instance = null;
        //创建实例
        this.createInstance = function () {
            var editor = UE.getEditor(id, commonConfig);
            this.instance = editor;
            return editor;
        };
        //获取内容
        this.getContent = function () {
            return this.instance.getContent();
        };
        //清空内容
        this.clearContent = function () {
            this.instance.setContent("");
        };
        //判断是否有内容
        this.hasContent = function () {
            return this.instance.hasContents();
        };
    };
    return myEditor;
})();
///日期格式化扩展
Date.prototype.pattern = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours() % 12 === 0 ? 12 : this.getHours() % 12, //小时
        "H+": this.getHours(), //小时
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    var week = {
        "0": "/u65e5",
        "1": "/u4e00",
        "2": "/u4e8c",
        "3": "/u4e09",
        "4": "/u56db",
        "5": "/u4e94",
        "6": "/u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "/u661f/u671f" : "/u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length === 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
};
///数组去重方法扩展
Array.prototype.unique = function () {
    var n = [this[0]]; //结果数组
    for (var i = 1; i < this.length; i++) //从第二项开始遍历
    {
        //如果当前数组的第i项在当前数组中第一次出现的位置不是i，
        //那么表示第i项是重复的，忽略掉。否则存入结果数组
        if (this.indexOf(this[i]) === i) n.push(this[i]);
    }
    return n;
};