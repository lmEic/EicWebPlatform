using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using System.Collections.Generic;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeMessage.Email;
using System.Linq;

namespace Lm.Eic.Framework.ProductMaster.Business.Itil
{
    public class ItilDevelopModuleManager
    {

        public ItilDevelopModuleManager()
        {
            _userMailAddsDic.Add("万晓桥", "wxq520@ezconn.cn");
        }

        /// <summary>
        /// 用户邮箱地址列表
        /// </summary>
        Dictionary<string, string> _userMailAddsDic = new Dictionary<string, string>();
        
        /// <summary>
        /// 获取开发任务列表  1.依据状态列表查询 2.依据函数名称查询 
        /// </summary>
        /// <param name="progressSignList">进度标识列表</param>
        /// <returns></returns>
        public List<ItilDevelopModuleManageModel> GetDevelopModuleManageListBy(ItilDto dto)
        {
            return ItilCrudFactory.ItilDevelopModuleManageCrud.GetDevelopModuleManageListBy(dto);
        }

        /// <summary>
        /// 仓储操作 model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(ItilDevelopModuleManageModel model)
        {
            return ItilCrudFactory.ItilDevelopModuleManageCrud.Store(model);
        }
        /// <summary>
        /// 修改开发进度
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OpResult ChangeProgressStatus(ItilDevelopModuleManageModel model)
        {
            return ItilCrudFactory.ItilDevelopModuleManageCrud.ChangeProgressStatus(model);
        }
        /// <summary>
        /// 获取开发任务进度变更明细
        /// </summary>
        /// <param name="model">开发任务</param>
        /// <returns></returns>
        public List<ItilDevelopModuleManageChangeRecordModel> GetChangeRecordListBy(ItilDevelopModuleManageModel model)
        {
            return ItilCrudFactory.ItilDevelopModuleManageCrud.GetChangeRecordListBy(model);
        }
        /// <summary>
        /// 发送邮件通知
        /// </summary>
        /// <returns></returns>
        public OpResult SendMail()
        {
            OpResult opResult = OpResult.SetErrorResult("未执行任何邮件发送！");
          ///  EmailMessageHelper email = new EmailMessageHelper("softwareadmin@ezconn.cn", "Echo4u", true);
            //email.MailSubject = "开发任务进度变更";

            //生成待发送邮件的人员列表
            List<string> waittingSendMailUserList = new List<string>();
            ItilCrudFactory.ItilDevelopModuleManageCrud.WaittingSendMailList.ForEach((model) =>
            {
                if (!waittingSendMailUserList.Contains(model.Executor))
                    waittingSendMailUserList.Add(model.Executor);
            });

            //依据人员列表进行邮件发送  
            foreach (var user in waittingSendMailUserList)
            {
                if (_userMailAddsDic.ContainsKey(user))
                {
                    var mailAdds = _userMailAddsDic[user];
                    //email.MailToArray = new string[] { mailAdds };//收件人邮件集合
                    //email.MailBody = BulidMailContext(ItilCrudFactory.ItilDevelopModuleManageCrud.WaittingSendMailList.Where(m => m.Executor == user).ToList());
                    //opResult = email.Send() ? OpResult.SetSuccessResult("邮件发送成功！", true) : OpResult.SetErrorResult("邮件发送失败！");
                }
            }
            return opResult;
        }
        /// <summary>
        /// 生成邮件正文
        /// </summary>
        /// <param name="waittingSendDmm">待发送的开发任务列表</param>
        /// <returns></returns>
        private string BulidMailContext(List<ItilDevelopModuleManageModel> waittingSendDmm)
        {
            string mailContext = "<h2>已修改进度的任务列表如下：</h2>";
            mailContext += "<style type=" + "text/css" + ">table.gridtable {	font-family: verdana,arial,sans-serif;	font-size:13px;	color:#333333;	border-width: 1px;	border-color: #666666;	border-collapse: collapse;}table.gridtable th {	border-width: 1px;	padding: 8px;	border-style: solid;	border-color: #666666;	background-color: #dedede;}table.gridtable td {	border-width: 1px;	padding: 8px;	border-style: solid;	border-color: #666666;	background-color: #ffffff;}</style><!-- Table goes in the document BODY --><table class=" + "gridtable" + "><tr><th>模块名称</th><th>类名称</th><th>函数名称</th><th>功能描述</th><th>难度系数</th><th>开始日期</th><th>当前进度</th><th>执行人</th><th>变更日期</th></tr>";

            foreach (var tem in waittingSendDmm)
            {
                mailContext += "<tr>";
                mailContext += string.Format("<td>{0}</td>", tem.ModuleName);
                mailContext += string.Format("<td>{0}</td>", tem.MClassName);
                mailContext += string.Format("<td>{0}</td>", tem.MFunctionName);
                mailContext += string.Format("<td>{0}</td>", tem.FunctionDescription);
                mailContext += string.Format("<td>{0}</td>", tem.DifficultyCoefficient);
                mailContext += string.Format("<td>{0}</td>", tem.StartDate);
                mailContext += string.Format("<td>{0}</td>", tem.CurrentProgress);
                mailContext += string.Format("<td>{0}</td>", tem.Executor);
                mailContext += string.Format("<td>{0}</td>", tem.FinishDate);
                mailContext += "</tr>";
            }

            mailContext += "</table>";
            return mailContext;
        }
    }
}
