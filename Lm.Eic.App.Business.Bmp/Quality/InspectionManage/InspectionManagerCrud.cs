using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    /// <summary>
    /// IQC 检验管理工厂
    /// </summary>
    internal class IqcInspectionManagerCrudFactory
    {
        /// <summary>
        /// 检验方式配置CRUD
        /// </summary>
        public static InspectionModeConfigCrud InspectionModeConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionModeConfigCrud>(); }
        }
        /// <summary>
        /// IQC物料检验配置CRUD
        /// </summary>
        public static InspectionItemConfigCrud InspectionItemConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionItemConfigCrud>(); }
        }
        /// <summary>
        /// 物料检验项次CRUD
        /// </summary>
        public static IqcInspectionMasterCrud IqcInspectionMasterCrud
        {
            get { return OBulider.BuildInstance<IqcInspectionMasterCrud>(); }
        }
        /// <summary>
        ///  物料检验项次数据CRUD
        /// </summary>
        public static IqcInspectionDetailCrud IqcInspectionDetailCrud
        {
            get { return OBulider.BuildInstance<IqcInspectionDetailCrud>(); }
        }
    }
   
    
    /// <summary>
    /// 检验方式配置CRUD
    /// </summary>
    internal class InspectionModeConfigCrud : CrudBase<InspectionModeConfigModel, IInspectionModeConfigRepository>
    {
        public InspectionModeConfigCrud() : base(new InspectionModeConfigRepository(), "检验方式配置")
        {
        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddInspectionModeConfig);
            this.AddOpItem(OpMode.Edit, EidtInspectionModeConfig);
            this.AddOpItem(OpMode.Delete, DeleteInspectionModeConfig);
        }

        private OpResult DeleteInspectionModeConfig(InspectionModeConfigModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtInspectionModeConfig(InspectionModeConfigModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddInspectionModeConfig(InspectionModeConfigModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        public List<InspectionModeConfigModel> GetInspectionStartEndNumberBy(string inspectionMode,string inspectionLevel,string inspectionAQL)
        {
            return irep.Entities.Where(e => e.InspectionMode == inspectionMode && e.InspectionLevel == inspectionLevel && e.InspectionAQL == inspectionAQL).OrderBy (e=>e.StartNumber).ToList();
        }



        /// <summary>
        /// 得到 抽验数量，接收数量，拒受数量
        /// </summary>
        /// <param name="inspectionMode">抽样方式</param>
        /// <param name="inspectionLevel">水平</param>
        /// <param name="inspectionAQL">规格</param>
        /// <param name="inMaterialCount">物料的总数量</param>
        /// <returns></returns>
        public Dictionary<string, int> getInspectionAcceptRefuseCountBy(IqcInspectionItemConfigModel IqcInspectionItemConfigmodel, int inMaterialCount)
        {
            var maxs = new List<Int64>(); var mins = new List<Int64>();
            double maxNumber; double minNumber;
            Dictionary<string, int> retrunDic = new Dictionary<string, int>();
            if (IqcInspectionItemConfigmodel == null) return retrunDic;
            var models = GetInspectionStartEndNumberBy(
                IqcInspectionItemConfigmodel.InspectionMode,
                IqcInspectionItemConfigmodel.InspectionLevel,
                IqcInspectionItemConfigmodel.InspectionAQL);
            models.ForEach(e =>
            { maxs.Add(e.EndNumber); mins.Add(e.StartNumber); });
            if (maxs.Count > 0)
                maxNumber = GetMaxNumber(maxs, inMaterialCount);
            else
                maxNumber = 0;
            if (mins.Count > 0)
                minNumber = GetMinNumber(mins, inMaterialCount);
            else
                minNumber = 0;
            var model = models.Where(e => e.StartNumber == minNumber && e.EndNumber == maxNumber).ToList().FirstOrDefault();
            // InspectionCount, AcceptCount, RefuseCount,
            if (model == null) return null;
            retrunDic.Add("inspectionCount", model.InspectionCount);
            retrunDic.Add("acceptCount", model.AcceptCount);
            retrunDic.Add("refuseCount", model.RefuseCount);
            return retrunDic;
        }
        private Int64 GetMaxNumber(List<Int64> maxNumbers, Int64 number)
        {
            List<Int64> IntMaxNumbers = new List<Int64>();
            foreach (var max in maxNumbers)
            {
                if (max != -1)
                {

                    if (max >= number)
                    {
                        IntMaxNumbers.Add(max);
                    }
                }
            }
            if (IntMaxNumbers.Count > 0)
            { return IntMaxNumbers.Min(); }
            else return -1;
        }
        private Int64 GetMinNumber(List<Int64> minNumbers, Int64 mumber)
        {
            List<Int64> IntMinNumbers = new List<Int64>();
            foreach (var min in minNumbers)
            {
                if (min != -1)
                {

                    if (min <= mumber)
                    {
                        IntMinNumbers.Add(min);
                    }
                }
                else return -1;
            }
            return IntMinNumbers.Max();
        }
    }
}
