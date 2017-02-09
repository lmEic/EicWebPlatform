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
    /// 设备维修管理
    /// </summary>
    public class EquipmentRepairedManager
    {

        /// <summary>
        /// 添加一条设备维修记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult AddEquipmentRepairedRecord(EquipmentRepairedRecordModel model)
        {
           
            return CrudFactory.EquipmentRepairedRecordCrud.Store(model);
        }

        /// <summary>
        /// 获取设备维修记录
        /// </summary>
        /// <param name="assetNumber">财产编号</param>
        /// <returns></returns>
        public List<EquipmentRepairedRecordModel> GetEquipmentRepairedRecordBy(string assetNumber)
        {
            return CrudFactory.EquipmentRepairedRecordCrud.GetEquipmentRepairedRecordBy(assetNumber);
        }

        /// <summary>
        /// 获取设备维修记录
        /// </summary>
        /// <param name="assetNumber">财产编号</param>
        /// <returns></returns>
        public List<EquipmentRepairedRecordModel> GetEquipmentRepairedRecordFormBy(string FormId)
        {
            return CrudFactory.EquipmentRepairedRecordCrud.GetEquipmentRepairedRecordFormIdBy(FormId);
        }
        /// <summary>
        /// 获取设备维修总览表
        /// </summary>
        /// <returns></returns>
        public List<EquipmentRepairedRecordModel> GetEquipmentRepairedOverView()
        {
            return CrudFactory.EquipmentRepairedRecordCrud.GetEquipmentRepairedOverView();
        }
    }
}
