using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CrudFactory = Lm.Eic.App.Business.Bmp.Ast.EquipmentCrudFactory;

namespace Lm.Eic.App.Business.Bmp.Ast
{
    /// <summary>
    ///设备报废管理
    /// </summary>
    public class EquipmentDiscardManager
    {
        /// <summary>
        /// 获取设备报废记录
        /// </summary>
        /// <param name="assetNumber">财产编号</param>
        /// <returns></returns>
        public EquipmentDiscardRecordModel GetEquipmentDiscardRecord(string assetNumber)
        {
            return CrudFactory.EquipmentDiscarCrud.GetEquipmentDiscardRecord(assetNumber);
        }

        /// <summary>
        /// 仓储操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(EquipmentDiscardRecordModel model)
        {
            return CrudFactory.EquipmentDiscarCrud.Store(model);
        }

        /// <summary>
        /// 获取设备报废总览表
        /// </summary>
        /// <returns></returns>
        public List<EquipmentDiscardRecordModel> GetEquipmentDiscardOverView()
        {
            return CrudFactory.EquipmentDiscarCrud.GetEquipmentDiscardOverView();
        }

        /// <summary>
        /// 获取设备报废总览表
        /// </summary>
        /// <returns></returns>
        public List<EquipmentDiscardRecordModel> GetEquipmentDiscardDetails(string assetNumber)
        {
            return CrudFactory.EquipmentDiscarCrud.GetEquipmentDiscardRecordList(assetNumber);
        }
    }
}
