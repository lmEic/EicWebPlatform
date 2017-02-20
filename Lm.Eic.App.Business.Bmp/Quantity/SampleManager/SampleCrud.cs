using System.Collections.Generic;
using System.Linq;
using Lm.Eic.App.DbAccess.Bpm.Repository.QuantityRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;

namespace Lm.Eic.App.Business.Bmp.Quantity.SampleManager
{

    /// <summary>
    /// 抽样物料记录管理
    /// </summary>
    public class SampleRecordManager
    {
        ISampleIqcRecordReposity irep = null;
        public SampleRecordManager()
        { irep = new SampleIqcRecordReposity(); }
         /// <summary>
         /// 
         /// </summary>
         /// <param name="sampleMaterial">物料品号</param>
         /// <returns></returns>
        public List<SampleIqcRecordModel> GetIQCSampleRecordModelsBy(string sampleMaterial)
        {
            return irep.Entities.Where(e => e.SampleMaterial == sampleMaterial).ToList();
        }
    }
}

