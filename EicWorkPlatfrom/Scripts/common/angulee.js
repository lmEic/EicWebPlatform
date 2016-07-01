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
        clearVm: function (vm,notKeys) {
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
        },
    };
    ///数据操作
    var leeDataOperate = {
        ///添加操作行为
        add: function (opstatus,isvalid, addfn)
        {
            opstatus.showValidation = !isvalid;
            if (isvalid)
            {
                if (addfn !== undefined)
                    addfn();
            }
        },
        ///处理操作结果
        handleSuccessResult: function (opstatus,opresult,customFn)
        {
            opstatus.showValidation = false;
            opstatus.result = opresult.Result;
            opstatus.message = opresult.Message;
            opstatus.msgDisplay = true;
   
            (function () {
                setTimeout(function () {
                    opstatus.msgDisplay = false;
                }, 100);
            })();
            if (customFn !== undefined)
                customFn();
        },
        ///显示错误信息
        displayMessage: function (opstatus, message, customFn) {
            opstatus.showValidation = false;
            opstatus.result = false;
            opstatus.message = message
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
        refresh: function (opstatus,cancelfn)
        {
            opstatus.showValidation = false;
            opstatus.msgDisplay = false;
            opstatus.result = false;
            if (cancelfn !== undefined)
            {
                cancelfn();
            }
        }
    };


    return {
        ///操作状态
        operateStatus: leeOperateStatus,
        ///数据操作行为
        dataOperate:leeDataOperate,
    };
})();
///常用操作助手
var leeHelper = (function () {
    var modalTpl = {
        msgModalUrl: '/CommonTpl/InfoMsgModalTpl',
        deleteModalUrl: '/CommonTpl/DeleteModalTpl'
        
    };
    var commonTplUrl={
        treeSelectTplUrl: '/CommonTpl/TreeSelectTpl',
    };
    var controllerNames = {
        equipment: 'Equipment',
        systemManage: 'EicSystemManage',
        configManage: 'SysConfig',
        itilManage:'SysITIL'
    };
    return {
        ///控制器名称
        controllers:controllerNames,
        ///清空视图对象的每个属性值
        //vm:视图对象
        //notKeys:不包含的要清楚值的键值
        clearVM: function (vm,notKeys)
        {
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
        },
        ///将srcObj对象中各个属性值赋值到desObj中去
        ///notKeys表示不包含要复制的键值集合
        copyVm: function (srcObj, desObj,notKeys) {
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
        remove: function (ary, item)
        {
            if (!Array.isArray(ary)) return;
            for (var i = 0, len = ary.length; i < len;i++)
            {
                if (ary[i] === item)
                {
                    ary.splice(i, 1);
                    break;
                }
                  
            };
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
                    })
                }
                else {
                    ckey = item[uniqProperties];
                }
                if (_.isUndefined(keys[ckey]))
                {
                    keys[ckey] = true;
                    datas.push(item);
                }
            });
            return datas;
        },
        ///模态窗口模板URL路径对象
        modalTplUrl: modalTpl,
        ///获取部门名称
        getDepartmentText: function (datas,department) {
            var departmentItem = _.findWhere(datas, { DataNodeName: department});
            if (departmentItem !== undefined)
            {
                return departmentItem.DataNodeText;
            }
        },
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
        checkAllNodes: function (treeId,checked) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            treeObj.checkAllNodes(checked);
        },
        //将 zTree 使用的标准 JSON 嵌套格式的数据转换为简单 Array 格式
        transformToArray: function (treeId, treeNodes) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            return treeObj.transformToArray(treeNodes);
        },
    };
    return ztree;
})();


