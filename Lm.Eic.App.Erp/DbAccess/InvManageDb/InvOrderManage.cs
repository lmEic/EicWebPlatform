using Lm.Eic.Uti.Common.YleeObjectBuilder;
namespace Lm.Eic.App.Erp.DbAccess.InvManageDb
{

    internal class InvOrderCrudFactory
    {
        /// <summary>
        /// 订单Crud
        /// </summary>
        public static InvOrderManage InvManageDb
        {
            get { return OBulider.BuildInstance<InvOrderManage>(); }
        }


    }
  public  class InvOrderManage
    {
    }
}
