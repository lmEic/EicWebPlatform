using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DbAccess.Bpm.Repository.QuantityRep;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.App.DomainModel.Bpm.Quanity;

namespace Lm.Eic.App.Business.Bmp.Quantity.SampleManager
{
    public class SampleCrudFactory
    {
    }

    /// <summary>
    /// 抽样物料记录管理
    /// </summary>
    public class SampleRecordManager
    {
        IIQCSampleRecordReposity irep = null;
        public SampleRecordManager()
        { irep = new IQCSampleRecordReposity(); }
         /// <summary>
         /// 
         /// </summary>
         /// <param name="sampleMaterial">物料品号</param>
         /// <returns></returns>
        public List<IQCSampleRecordModel> GetIQCSampleRecordModelsBy(string sampleMaterial)
        {
            return irep.Entities.Where(e => e.SampleMaterial == sampleMaterial).ToList();
        }



    }
}

