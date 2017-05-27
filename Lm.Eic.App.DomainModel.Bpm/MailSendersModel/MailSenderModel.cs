﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.MailSendersModel
{
    /// <summary>
    /// 发送者基本数据
    /// </summary>
    public class SendersMailModel
    {
        /// <summary>
        /// 发邮人地址
        /// </summary>
        public string SenderMailAddess
        {
            set; get;
        }
        /// <summary>
        /// 发件密码
        /// </summary>
        public string SenderMailPwd
        {
            set; get;
        }
        /// <summary>
        /// 发件人服务器
        /// </summary>
        public string SenderSeverHost
        {
            set; get;
        }
        public MailPriority _mailPriority = MailPriority.Normal;
        /// <summary>
        /// 指定邮件的优先级
        /// </summary>
        public MailPriority MailPriority
        {
            set { _mailPriority=value ; }
            get { return _mailPriority; }
        }
    }
    /// <summary>
    /// 收件人
    /// </summary>
    public class RecipientsMailModel
    {
        /// <summary>
        /// 收件人
        /// </summary>
        public List<string> RecipientMailAddress { get; set; }
        /// <summary>
        /// 抄送
        /// </summary>
        public List<string> CopyRecipientMailAddress { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string MailTitle { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string MailBody { get; set; }
        /// <summary>
        /// 正文是否有安Html格式发送
        /// </summary>
        public bool IsBodyHtml
        { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public List<string> AttachmentsPath { get; set; }
        /// <summary>
        /// 发送内容转化为Html 格式
        /// </summary>
        /// <param name="waittingSendDmm"></param>
        /// <returns></returns>
        public string SendMailContentToHtml(List<string> waittingSendDmm)
        {
            string mailContext = "<h2>" + MailTitle + "：</h2>";
            mailContext += "<style type=" + "text/css" + ">table.gridtable {" +
            " font-family: verdana,arial,sans-serif;" +
            " font-size:13px;	color:#333333;" +
            "border-width: 1px;	" +
             " border-color: #666666;	" +
            " border-collapse: collapse;}" +
            "table.gridtable th {	border-width: 1px;	padding: 8px;" +
            "border-style: solid;	border-color: #666666;" +
            " background-color: #dedede;}" +
            "table.gridtable td {	border-width: 1px;	padding: 8px;	border-style: solid;" +
            " border-color: #666666;	background-color: #ffffff;}" +
            " </style><!-- Table goes in the document BODY --><table class=" + "gridtable" + ">" +
            "<tr></tr>";
            foreach (var tem in waittingSendDmm)
            {
                mailContext += "<tr>";
                mailContext += string.Format("<td>{0}</td>", tem);
                mailContext += "</tr>";
            }
            mailContext += "</table>";
            return mailContext;
        }
    }
}
