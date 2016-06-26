using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace Lm.Eic.Uti.Common.YleeFtp
{
    /// <summary>
    /// Ftp连接同行证
    /// </summary>
    public class FtpConnectPassport
    {
        /// <summary>
        /// Ftp Uri 为服务器地址加端口号
        /// 例如：ftp://192.168.0.237:21/
        /// </summary>
        public Uri FtpUri { get; set; }
        /// <summary>
        /// 登陆用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 服务代理
        /// </summary>
        public WebProxy Proxy { get; set; }
    }

    
}
