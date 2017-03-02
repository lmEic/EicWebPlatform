using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    /// <summary>
    /// 进料检验数据采集器
    /// </summary>
    public class IqcInspectionDataGather
    {

        /// <summary>
        /// 得到抽样物料信息
        /// </summary>
        /// <param name="orderId">ERP单号</param>
        /// <returns></returns>
        public List<MaterialModel> GetPuroductSupplierInfo(string orderId)
        {
            return QuantityDBManager.QuantityPurchseDb.FindMaterialBy(orderId);
        }
        /// <summary>
        /// 得到IQC物料料号得到相应的规格参数 
        /// </summary>
        /// <param name="orderId">ERP单号</param>
        /// <param name="sampleMaterialId">物料料号</param>
        /// <returns></returns>
        public IqcInspectionItemConfigModel GetInspectionItemParameterBy(string sampleMaterialId,string inspectionItem)
        {
            return IqcInspectionManagerCrudFactory.InspectionItemConfigCrud.FindIqcInspectionItemConfigDatasBy(sampleMaterialId).FirstOrDefault(e => e.InspectionItem == inspectionItem);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleMaterialId"></param>
        /// <param name="inspectionItem"></param>
        /// <returns></returns>
        public InspectionItemParameterModel GetInspectionItemParameterModel(int inMaterialCount, string sampleMaterialId, string inspectionItem)
        {
            var mm = GetInspectionItemParameterBy(sampleMaterialId, inspectionItem);
            return new InspectionItemParameterModel(mm, inMaterialCount);
        }
        /// <summary>
        /// 存储Iqc检验数据
        /// </summary>
        /// <returns></returns>
        public OpResult StoreIqcInspectionDetailModel(IqcInspectionDetailModel model)
        {
            return IqcInspectionManagerCrudFactory.IqcInspectionDetailCrud.Store(model,true);
        }
        /// <summary>
        /// 存储Iqc检验项次
        /// </summary>
        /// <returns></returns>
        public OpResult StoreIqcInspectionMasterModel(IqcInspectionMasterModel model)
        {
            return IqcInspectionManagerCrudFactory.IqcInspectionMasterCrud.Store(model, true);
        }
    }




    /// <summary>
    /// 
    /// </summary>
    public class InspectionItemParameterModel
    {
        public InspectionItemParameterModel(IqcInspectionItemConfigModel iqcInspectionItemConfig, int inMaterialCount)
        {
            this.IqcInspectionItemConfig = iqcInspectionItemConfig;
            this._inMaterialCount = inMaterialCount;
            getInspectionAcceptRefuseCountBy();
        }
        private Int64 _inMaterialCount;
        public Int64 InMaterialCount
        {
            get { return _inMaterialCount; }
        }
        /// <summary>
        ///检验项目配置
        /// </summary>
        public IqcInspectionItemConfigModel IqcInspectionItemConfig
        {
            set; get;
        }
        double _inspectionCount = 0;
        /// <summary>
        /// 需要检验的数量
        /// </summary>
        public double InspectionCount
        {
            get
            {
                return _inspectionCount;
            }
        }
        double _acceptCount = 0;
        /// <summary>
        /// 检验接受的数量
        /// </summary>
        public double AcceptCount
        {
            get
            {
                return _acceptCount;
            }
        }
        double _refuseCount = 0;
        /// <summary>
        /// 所收的数量
        /// </summary>
        public double RefuseCount
        {
            get
            {
                return _refuseCount;
            }
        }
        /// <summary>
        /// 得到 抽验数量，接收数量，拒受数量
        /// </summary>
        /// <param name="inspectionMode">抽样方式</param>
        /// <param name="inspectionLevel">水平</param>
        /// <param name="inspectionAQL">规格</param>
        /// <param name="inMaterialCount">物料的总数量</param>
        /// <returns></returns>
        private void getInspectionAcceptRefuseCountBy()
        {
            var maxs = new List<Int64>(); var mins = new List<Int64>();
            double maxNumber; double minNumber;
            if (IqcInspectionItemConfig == null) return;
            var models = IqcInspectionManagerCrudFactory.InspectionModeConfigCrud.GetInspectionStartEndNumberBy(
                IqcInspectionItemConfig.InspectionMode,
                IqcInspectionItemConfig.InspectionLevel,
                IqcInspectionItemConfig.InspectionAQL);
            models.ForEach(e =>
            { maxs.Add(e.EndNumber); mins.Add(e.StartNumber); });
            if (maxs.Count > 0)
                maxNumber = GetMaxNumber(maxs, _inMaterialCount);
            else
                maxNumber = 0;
            if (mins.Count > 0)
                minNumber = GetMinNumber(mins, _inMaterialCount);
            else
                minNumber = 0;
            var model = models.Where(e => e.StartNumber == minNumber && e.EndNumber == maxNumber).ToList().FirstOrDefault();
            // InspectionCount, AcceptCount, RefuseCount,
            if (model == null) return;
            _inspectionCount= model.InspectionCount;
            _acceptCount=model.AcceptCount;
            _refuseCount = model.RefuseCount;

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
