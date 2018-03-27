using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage.Ipqc
{
    internal class InspectionIpqcDatailCrud : CrudBase<InspectionIpqcDetailModel, IpqcInspectionDatailRepository>
    {
        public InspectionIpqcDatailCrud() : base(new IpqcInspectionDatailRepository(), "IPQC详细表单")
        { }
        #region Crud
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
        }



        private OpResult Add(InspectionIpqcDetailModel model)
        {
           
            return null;
        }
    
        #endregion

    }
}
