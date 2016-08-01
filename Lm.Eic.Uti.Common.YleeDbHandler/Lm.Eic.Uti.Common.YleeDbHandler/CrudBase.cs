using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Uti.Common.YleeDbHandler
{
    public abstract class CrudBase<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 修改数据仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         public abstract OpResult Store(TEntity model);

        
    }
}
