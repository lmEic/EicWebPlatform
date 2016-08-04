﻿using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs
{
    public class GeneralAffairsFactory
    {
        /// <summary>
        /// 厂服管理CRUD
        /// </summary>
        internal static WorkerClothesCrud WorkerClothesCrud
        {
            get { return OBulider.BuildInstance<WorkerClothesCrud>(); }
        }
    }
}
