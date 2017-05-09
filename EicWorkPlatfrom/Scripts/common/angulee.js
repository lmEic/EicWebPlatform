/// <reference path="../../Content/ztree/dist/js/jquery.ztree.all.min.js" />
///数据处理器
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
        ///处理操作结果
        handleSuccessResult: function (opstatus, opresult, customFn) {
            opstatus.showValidation = false;
            opstatus.result = opresult.Result;
            opstatus.message = opresult.Message;
            opstatus.msgDisplay = true;

            (function () {
                setTimeout(function () {
                    opstatus.msgDisplay = false;
                }, 2000);
            })();
            if (customFn !== undefined)
                customFn();
        },
        ///显示错误信息
        displayMessage: function (opstatus, message, customFn) {
            opstatus.showValidation = false;
            opstatus.result = false;
            opstatus.message = message;
            opstatus.msgDisplay = true;

            (function () {
                setTimeout(function () {
                    opstatus.msgDisplay = false;
                }, 100);
            })();
            if (customFn !== undefined)
                customFn();
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
        add: 'add',
        edit: 'edit',
        update: 'update',
        'delete': 'delete',
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
///常用操作助手
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
        equipment: 'Equipment',
        systemManage: 'EicSystemManage',
        configManage: 'SysConfig',
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
        quaRmaManage: 'QuaRmaManage'
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
        ///从数组中移除指定选项
        remove: function (ary, item) {
            if (!Array.isArray(ary)) return;
            for (var i = 0, len = ary.length; i < len; i++) {
                if (ary[i] === item) {
                    ary.splice(i, 1);
                    break;
                }
            }
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
            return (targetValue >= min && targetValue <= max);
        },
        ///设置网站标题
        setWebSiteTitle: function (title, subTitle) {
            document.title = title + "---" + subTitle;
        }
    };
})();
/// 对话框助手
var leeDialog = (function () {
    var dialog = {
        title: "消息提示",
        content: '',
        //打开关闭标志
        open: false,
        //打开窗体
        show: function () {
            dialog.open = true;
        },
        //关闭窗体
        close: function () {
            dialog.open = false;
        }
    };
    return dialog;
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