using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Framework.ProductMaster.Model.CommonManage;
using Lm.Eic.Framework.ProductMaster.DbAccess.Repository.CommonManageRepository;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.Framework.ProductMaster.Business.Common
{
    /// <summary>
    /// 表单管理数据操作助手
    /// </summary>
    internal class FormIdManageCrud : CrudBase<FormIdManageModel, IFormIdManageRepository>
    {
        public FormIdManageCrud() : base(new FormIdManageRepository(), "表单编号")
        {

        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
        }

        #region create formId method
        /// <summary>
        /// 自动创建表单编号模型
        /// </summary>
        /// <param name="department"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        internal FormIdComposeModel CreateFormIdModel(string department, string moduleName)
        {
            FormIdManageModel formIdMdl = null;
            string yearMonth = DateTime.Now.ToString("yyyyMM");
            string subId = string.Empty;
            string primaryKey = string.Empty;
            var formIdList = GetFormIds(department, moduleName);
            //检测本月是否存在表单编号，如果没有则直接返回新创建的
            if (formIdList == null || formIdList.Count == 0)
            {
                //删除上月份非正常的表单编号
                DeleteLastMonthUnnormalFormIds(department, moduleName);
                subId = "001";
                return CreateNewFormId(moduleName, department, subId);
            }
            if (IsExistUnnormalFormId(formIdList, out formIdMdl))
            {
                return FetchExistFormId(department, GetFormSubId(formIdMdl.FormId), formIdMdl.PrimaryKey);
            }
            var normalFormIdList = formIdList.FindAll(e => e.FormStatus == FormIdStatus.Normal);
            if (normalFormIdList == null || normalFormIdList.Count == 0)
            {
                subId = (formIdList.Max(f => f.SubId) + 1).ToString().PadLeft(3, '0');
                return CreateNewFormId(moduleName, department, subId);
            }
            var m = normalFormIdList.Max(e => e.SubId) + 1;
            subId = m.ToString().PadLeft(3, '0');
            return CreateNewFormId(moduleName, department, subId);
        }
        private FormIdComposeModel FetchExistFormId(string department, string subId, string primaryKey)
        {
            return CreateFormIdComposeModel(department, subId, primaryKey);
        }
        /// <summary>
        /// 创建新的表单编号
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="department"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        private FormIdComposeModel CreateNewFormId(string moduleName, string department, string subId)
        {
            string yearMonth = DateTime.Now.ToString("yyyyMM");
            string primaryKey = string.Format("{0}&{1}&{2}&{3}", moduleName, department, yearMonth, subId);
            if (this.Store(new FormIdManageModel()
            {
                CreateDate = DateTime.Now.ToDate(),
                Department = department,
                FormId = "_" + subId,
                FormStatus = FormIdStatus.Unnormal,
                ModuleName = moduleName,
                SubId = subId.ToInt(),
                YearMonth = yearMonth,
                PrimaryKey = primaryKey,
                OpTime = DateTime.Now,
                OpDate = DateTime.Now.ToDate(),
                OpPerson = "system",
                OpSign = "add"

            }).Result)
            {
                return CreateFormIdComposeModel(department, subId, primaryKey);
            }

            return null;
        }
        private string GetFormSubId(string formId)
        {
            return formId.Substring(formId.Length - 3, 3);
        }
        private FormIdComposeModel CreateFormIdComposeModel(string department, string formSubId, string primaryKey)
        {
            return new FormIdComposeModel(primaryKey)
            {
                Department = department,
                SubId = formSubId,
                YearMonth = DateTime.Now.ToString("yyyyMM")
            };
        }
        /// <summary>
        /// 检测是否存在非正常编号的表单编号
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="existMdl"></param>
        /// <returns></returns>
        private bool IsExistUnnormalFormId(List<FormIdManageModel> datas, out FormIdManageModel existMdl)
        {
            existMdl = null;
            bool isExist = false;
            DateTime today = DateTime.Now.AddHours(-4).ToDate();
            var unnormalDatas = datas.FindAll(e => e.FormStatus == FormIdStatus.Unnormal);
            if (unnormalDatas == null || unnormalDatas.Count == 0) return isExist;
            //获取4个小时前的非正常表单创建的数据
            existMdl = unnormalDatas.FirstOrDefault(e => e.CreateDate < today);
            return existMdl != null;
        }
        private List<FormIdManageModel> GetFormIds(string department, string moduleName)
        {
            string yearMonth = DateTime.Now.ToString("yyyyMM");
            return this.irep.Entities.Where(e => e.Department == department && e.ModuleName == moduleName && e.YearMonth == yearMonth).ToList();
        }
        /// <summary>
        /// 删除上个月份非正常的表单单号
        /// </summary>
        /// <param name="department"></param>
        /// <param name="moduleName"></param>
        private void DeleteLastMonthUnnormalFormIds(string department, string moduleName)
        {
            string lastMonth = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
            this.irep.Delete(e => e.Department == department && e.ModuleName == moduleName && e.YearMonth == lastMonth && e.FormStatus == FormIdStatus.Unnormal);
        }

        private OpResult Add(FormIdManageModel m)
        {
            if (!irep.IsExist(e => e.PrimaryKey == m.PrimaryKey))
                return irep.Insert(m).ToOpResult_Add(this.OpContext);
            return OpResult.SetResult("创建表单编号成功！", true);
        }
        #endregion

        #region update formId method
        /// <summary>
        /// 更改表单编号
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        internal int UpdateFormId(string formId, string primaryKey)
        {
            return this.irep.Update(f => f.PrimaryKey == primaryKey, u => new FormIdManageModel
            {
                FormId = formId
            });
        }
        /// <summary>
        /// 更改表单编号
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        internal int UpdateFormIdStatus(string formId, string status)
        {
            return this.irep.Update(f => f.FormId == formId, u => new FormIdManageModel
            {
                FormStatus = status
            });
        }
        #endregion
    }
    public static class FormIdCreateExtension
    {
        /// <summary>
        /// 同步数据库中的表单编号
        /// </summary>
        /// <param name="formId"></param>
        public static int SynchronizeFormId(this string formId, string primaryKey)
        {
            return CommonManageCurdFactory.FormIdCrud.UpdateFormId(formId, primaryKey);
        }
        /// <summary>
        /// 设置表单编号的状态为正常
        /// </summary>
        /// <param name="formId"></param>
        public static int SetFormIdNormalStatus(this string formId)
        {
            return CommonManageCurdFactory.FormIdCrud.UpdateFormIdStatus(formId, FormIdStatus.Normal);
        }
    }
    /// <summary>
    /// 表单编号状态
    /// </summary>
    internal class FormIdStatus
    {

        /// <summary>
        /// 正常
        /// </summary>
        internal const string Normal = "Normal";
        /// <summary>
        /// 非正常
        /// </summary>
        internal const string Unnormal = "Unnormal";
    }
    /// <summary>
    /// 表单编号组合模型
    /// </summary>
    public class FormIdComposeModel
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 年月份
        /// </summary>
        public string YearMonth { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string SubId { get; set; }

        public string PrimaryKey { get; private set; }

        public FormIdComposeModel(string primaryKey)
        {
            this.PrimaryKey = primaryKey;
        }
    }
}
