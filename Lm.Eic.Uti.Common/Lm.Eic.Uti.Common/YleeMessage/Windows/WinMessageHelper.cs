using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lm.Eic.Uti.Common.YleeMessage.Windows
{
  public class WinMessageHelper
    {
        /// <summary>
        /// 系统提示信息
        /// </summary>
        /// <param name="msg">信息内容</param>
        /// <param name="tipType">提示信息类型：
        /// 1.信息提示；2.系统警告；3.错误提示</param>
        public static void ShowMsg(string msg, int tipType)
        {
            switch (tipType)
            {
                case 1:
                    MessageBox.Show(msg, "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 2:
                    MessageBox.Show(msg, "系统警告！", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    break;
                case 3:
                    MessageBox.Show(msg, "错误提示！", MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 异常处理拦截器(简易AOP)
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="excetionHandleMode">
        /// 异常消息处理模式
        /// 0：代表Windows默认的显示消息框
        /// </param>
        public static void ATry(Action<int> handler,int excetionHandleMode=0)
        {
            try
            {
                handler(excetionHandleMode);
            }
            catch (Exception ex)
            {
                if (excetionHandleMode == 0)
                {
                    ShowMsg(ex.Message, 3);
                }
            }
        }
        /// <summary>
        /// 异常处理拦截器(简易AOP)
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="excetionHandleMode">
        /// 异常消息处理模式
        /// 0：代表Windows默认的显示消息框
        /// </param>
        public static T ATry<T>(Func<int,T> handler, int excetionHandleMode = 0)
        {
            T result = default(T);
            try
            {
                 result= handler(excetionHandleMode);
            }
            catch (Exception ex)
            {
                if (excetionHandleMode == 0)
                {
                    ShowMsg(ex.Message, 3);
                }
            }
            return result;
        }
    }
}
