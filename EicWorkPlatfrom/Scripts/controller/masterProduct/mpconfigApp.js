/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="../../underscore/underscore-min.js" />

angular.module('mp.configApp', ['eicomm.directive', 'ngAnimate', 'ui.router', 'ngMessages', 'cgBusy', 'ngSanitize', 'mgcrea.ngStrap'])
.factory('dataDicConfigTreeSet', function () {
    var ztreeSets = [];
    var createTreeDataset = function (datas, root) {
        var treeNodes = [];
        var childrenNodes = _.where(datas, { ParentDataNodeText: root });
        if (childrenNodes !== undefined && childrenNodes.length > 0) {
            angular.forEach(childrenNodes, function (node) {
                var trnode = {
                    name: node.DataNodeText,
                    children: createTreeDataset(datas, node.DataNodeText),
                    vm: node
                };
                treeNodes.push(trnode);
            });
        }
        return treeNodes;
    };
    return {
        getTreeSet: function (treeId, treeNodeRoot) {
            var ztreeSetItem = _.find(ztreeSets, { treeId: treeId });
            if (ztreeSetItem === undefined) {
                var zTreeSet = {
                    root: treeNodeRoot,
                    treeId: treeId,
                    configDataNodes: [],
                    startLoad: false,
                    treeNode: null,
                    setTreeDataset: function (datas) {
                        zTreeSet.configDataNodes = createTreeDataset(datas, zTreeSet.root);
                        zTreeSet.startLoad = true;
                    }
                };

                ztreeSetItem = { treeId: treeId, ztreeSet: zTreeSet };
                ztreeSets.push(ztreeSetItem);
            }
            return ztreeSetItem.ztreeSet;
        }
    };
});