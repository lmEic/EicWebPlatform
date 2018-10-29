using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Pms.BoardManagment;
using Lm.Eic.App.Erp.Bussiness.MocManage;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Web;
using System.Security.Cryptography;
using System.Configuration;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Lm.Eic.App.Business.BmpTests.Pms.BoardManagmentTest
{
    [TestClass]
    public class BoardManagmentUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var m = BoardService.MaterialBoardManager.GetMaterialSpecBoardBy("512-1607086");
            //if (m != null)
            //    BoardService.MaterialBoardManager.AddMaterialSpecBoard(m);
            KdApiEOrderDemo test = new KdApiEOrderDemo();
            test.orderTracesSubByJson();
            //getContent();
           
        }

        private void orderTracesSubByJson()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestStore()
        {
            //MaterialSpecBoardModel model = new MaterialSpecBoardModel()
            //{
            //    ProductID = "24JKA014800M0RN",
            //    MaterialID = "32C0P99999211RM,34C0E99999540RM,35Z0I99989274RM,35Z7I99996434RM",
            //    OpSign = "Add",
            //    Remarks = "暂时没有图号"
            //};
            //var result = BoardService.MaterialBoardManager.AddMaterialSpecBoard(model);
            BomManage model = new BomManage();
          
            var models = model.GetBomMaterialList("24JKA015100M0RN");
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ss = HttpPost("http://localhost:41558/api/Demo/PostXXX", "{Code:\"test089\",Name:\"test1\"}");
        }

        public static string HttpPost(string url, string body)
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

            byte[] buffer = encoding.GetBytes(body);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }


        private void button111_Click(object sender, EventArgs e)
        {
            string ss = HttpGet("http://localhost:41558/api/Demo/GetXXX?Name=北京");
        }

        public static string HttpGet(string url)
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }


        /*
        * HTTP的Post请求方式(推荐)
        * strUrl 请求地址
        * param 请求数据
        */
        public string requestPost(string strUrl, string param)
        {
            HttpWebRequest httpWebRequest = WebRequest.Create(strUrl) as HttpWebRequest;
            httpWebRequest.Method = "POST";      //指定允许数据发送的请求的一个协议方法
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";       //设置 ContentType 属性设置为适当的值
            byte[] data = Encoding.UTF8.GetBytes(param);
            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);     //写入数据
            }
            WebResponse webResponse = httpWebRequest.GetResponse() as HttpWebResponse;        //发起请求,得到返回对象
            Stream dataStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
            string returnStr = reader.ReadToEnd();
            // Clean up the streams and the response.
            reader.Close();
            webResponse.Close();
            return returnStr;
        }
        /**
        * 获取内容
        */
     
        public void getContent()
        {
            string url = "http://api.kdniao.cc/api/Eorderservice";
            string param = "key="+ "f8f2eca7839542bcb94dec48cef173dc" + "&com=EMS&"+ "EBusinessID="+ "102120"+ "&DataSign=" + "1007&"+ "DataSign="+ "12121323&RequestData="+"";
            string returnStr = null;
            returnStr = "post result:" + this.requestPost(url, param);
            Console.WriteLine(returnStr);
        }



        [DataContract]
        public class SyncResponseEntity
        {
            public SyncResponseEntity() { }
            /// <summary>
            /// 需要查询的快递代号
            /// </summary>
            [DataMember(Order = 0, Name = "id")]
            public string ID { get; set; }

            /// <summary>
            /// 需要查询的快递名称
            /// </summary>
            [DataMember(Order = 1, Name = "name")]
            public string Name { get; set; }

            /// <summary>
            /// 需要查询的快递单号
            /// </summary>
            [DataMember(Order = 2, Name = "order")]
            public string Order { get; set; }

            /// <summary>
            /// 消息内容
            /// </summary>
            [DataMember(Order = 5, Name = "message")]
            public string Message { get; set; }

            /// <summary>
            /// 服务器状态
            /// </summary>
            [DataMember(Order = 6, Name = "errcode")]
            public string ErrCode { get; set; }

            /// <summary>
            /// 运单状态
            /// </summary>
            [DataMember(Order = 7, Name = "status")]
            public int Status { get; set; }

            /// <summary>
            /// 跟踪记录
            /// </summary>
            [DataMember(Order = 8, Name = "data")]
            public List<Order> Data { get; set; }
        }

        [DataContract(Name = "data")]
        public class Order
        {
            public Order() { }
            public Order(string time, string content)
            {
                this.Time = time;
                this.Content = content;
            }

            [DataMember(Order = 0, Name = "time")]
            public string Time { get; set; }

            [DataMember(Order = 1, Name = "content")]
            public string Content { get; set; }

        }
 
       //---------调用方法
        public static int uid = Utils.GetAppConfig<int>("KUAIDIAPI_UID", 0);
        public static string sync_url = Utils.GetAppConfig<string>("KUAIDIAPI_SYNC_URL", string.Empty);
        public static string key = Utils.GetAppConfig<string>("KUAIDIAPI_KEY", string.Empty);

        /// <summary>
        /// 同步单号查询方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="isSign"></param>
        /// <param name="isLast"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T APIQueryDataSYNC<T>(string id,
                                             string order,
                                             bool isSign,
                                             bool isLast,
                                             T defaultValue)
        {
            try
            {
                string currTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string currKey = key;
                if (isSign)
                {
                    currKey = Utils.GetSign(uid, key, id, order, currTime);
                    currKey += "&issign=true";
                }

                string url = sync_url + string.Format("?uid={0}&key={1}&id={2}&order={3}&time={4}",
                                            uid, currKey, id, order, HttpUtility.UrlEncode(currTime));

                string html = Utils.GET_WebRequestHTML("utf-8", url);

                if (!string.IsNullOrEmpty(html))
                    return Utils.JsonToObj<T>(html, defaultValue);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return defaultValue;
        }

    }

    /// <summary>
    /// 辅助工具类
    /// </summary>
    public class Utils
    {
        public static string GetSign(int uid, string key, string id, string order, string time)
        {
            string sign = string.Format("uid={0}&key={1}&id={2}&order={3}&time={4}",
                                        uid,
                                        key,
                                        id,
                                        HttpUtility.UrlEncode(order.ToLower()),
                                        HttpUtility.UrlEncode(time));

            return Md5Encrypt(sign.ToLower(), "utf-8");
        }

        public static string Md5Encrypt(string strToBeEncrypt, string encodingName)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] FromData = System.Text.Encoding.GetEncoding(encodingName).GetBytes(strToBeEncrypt);
            Byte[] TargetData = md5.ComputeHash(FromData);
            string Byte2String = "";
            for (int i = 0; i < TargetData.Length; i++)
            {
                Byte2String += TargetData[i].ToString("x2");
            }
            return Byte2String;
        }

        public static T GetRequest<T>(string key, T defaultValue)
        {
            string value = HttpContext.Current.Request[key];

            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        public static T GetAppConfig<T>(string key, T defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        public static string ObjToJson<T>(T data)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(data.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, data);
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
                return null;
            }
        }

        public static T JsonToObj<T>(string json, T defaultValue)
        {
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    object obj = serializer.ReadObject(ms);

                    return (T)Convert.ChangeType(obj, typeof(T));
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T XmlToObj<T>(string xml, T defaultValue)
        {
            try
            {
                System.Runtime.Serialization.DataContractSerializer serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    object obj = serializer.ReadObject(ms);

                    return (T)Convert.ChangeType(obj, typeof(T));
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string ObjToXml<T>(T data)
        {
            System.Runtime.Serialization.DataContractSerializer serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                return Encoding.UTF8.GetString(ms.ToArray());

            }
        }

        public static string GET_WebRequestHTML(string encodingName, string htmlUrl)
        {
            string html = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding(encodingName);

                WebRequest webRequest = WebRequest.Create(htmlUrl);
                HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);

                html = streamReader.ReadToEnd();

                httpWebResponse.Close();
                responseStream.Close();
                streamReader.Close();
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }

            return html;
        }

        /// <summary>
        /// 将网址类容转换成文本字符串 post请求
        /// </summary>
        /// <param name="data">要post的数据</param>
        /// <param name="url">目标url</param>
        /// <returns>服务器响应</returns>
        public static string POST_HttpWebRequestHTML(string encodingName,
                                                      string htmlUrl,
                                                      string postData)
        {
            string html = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding(encodingName);

                byte[] bytesToPost = encoding.GetBytes(postData);

                WebRequest webRequest = WebRequest.Create(htmlUrl);
                HttpWebRequest httpRequest = webRequest as System.Net.HttpWebRequest;

                httpRequest.Method = "POST";
                httpRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.ContentLength = bytesToPost.Length;
                httpRequest.Timeout = 15000;
                httpRequest.ReadWriteTimeout = 15000;
                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(bytesToPost, 0, bytesToPost.Length);
                requestStream.Close();

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);

                html = streamReader.ReadToEnd();
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }

            return html;
        }
    }

    /// <summary>
    /// 接口类型
    /// </summary>
    public enum APIType
    {
        //同步查询
        SYNC = 1
    }


    public class KdApiEOrderDemo
    {
        //电商ID
        private string EBusinessID = "102120";
        //电商加密私钥，注意保管，不要泄漏
        private string AppKey = "f8f2eca7839542bcb94dec48cef173dc";
        //请求url
        //正式环境地址
        // private string ReqURL = "http://api.kdniao.cc/api/Eorderservice";

        //测试环境地址
        private string ReqURL = "http://testapi.kdniao.cc:8081/api/EOrderService";

        /// <summary>
        /// Json方式  电子面单
        /// </summary>
        /// <returns></returns>
        public string orderTracesSubByJson()
        {
            string requestData = "{'OrderCode': '012657700222'," +
                                 "'ShipperCode':'SF'," +
                                  "'PayType':1," +
                                  "'ExpType':1," +
                                  "'Cost':1.0," +
                                  "'OtherCost':1.0," +
                                  "'Sender':" +
                                  "{" +
                                  "'Company':'LV','Name':'Taylor','Mobile':'15018442396','ProvinceName':'上海','CityName':'上海','ExpAreaName':'青浦区','Address':'明珠路73号'}," +
                                  "'Receiver':" +
                                  "{" +
                                  "'Company':'GCCUI','Name':'Yann','Mobile':'15018442396','ProvinceName':'北京','CityName':'北京','ExpAreaName':'朝阳区','Address':'三里屯街道雅秀大厦'}," +
                                  "'Commodity':" +
                                  "[{" +
                                  "'GoodsName':'鞋子','Goodsquantity':1,'GoodsWeight':1.0}]," +
                                  "'Weight':1.0," +
                                  "'Quantity':1," +
                                  "'Volume':0.0," +
                                  "'Remark':'小心轻放'," +
                                  "'IsReturnPrintTemplate':1}";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("RequestData", HttpUtility.UrlEncode(requestData, Encoding.UTF8));
            param.Add("EBusinessID", EBusinessID);
            param.Add("RequestType", "1007");
            string dataSign = encrypt(requestData, AppKey, "UTF-8");
            param.Add("DataSign", HttpUtility.UrlEncode(dataSign, Encoding.UTF8));
            param.Add("DataType", "2");

            string result = sendPost(ReqURL, param);

            //根据公司业务处理返回的信息......

            return result;
        }

        /// <summary>
        /// Post方式提交数据，返回网页的源代码
        /// </summary>
        /// <param name="url">发送请求的 URL</param>
        /// <param name="param">请求的参数集合</param>
        /// <returns>远程资源的响应结果</returns>
        private string sendPost(string url, Dictionary<string, string> param)
        {
            string result = "";
            StringBuilder postData = new StringBuilder();
            if (param != null && param.Count > 0)
            {
                foreach (var p in param)
                {
                    if (postData.Length > 0)
                    {
                        postData.Append("&");
                    }
                    postData.Append(p.Key);
                    postData.Append("=");
                    postData.Append(p.Value);
                }
            }
            byte[] byteData = Encoding.GetEncoding("UTF-8").GetBytes(postData.ToString());
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = url;
                request.Accept = "*/*";
                request.Timeout = 30 * 1000;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.Method = "POST";
                request.ContentLength = byteData.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(byteData, 0, byteData.Length);
                stream.Flush();
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream backStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(backStream, Encoding.GetEncoding("UTF-8"));
                result = sr.ReadToEnd();
                sr.Close();
                backStream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            throw new NotImplementedException();
        }


        ///<summary>
        ///电商Sign签名
        ///</summary>
        ///<param name="content">内容</param>
        ///<param name="keyValue">Appkey</param>
        ///<param name="charset">URL编码 </param>
        ///<returns>DataSign签名</returns>
        private string encrypt(String content, String keyValue, String charset)
        {
            if (keyValue != null)
            {
                return base64(MD5(content + keyValue, charset), charset);
            }
            return base64(MD5(content, charset), charset);
        }

        ///<summary>
        /// 字符串MD5加密
        ///</summary>
        ///<param name="str">要加密的字符串</param>
        ///<param name="charset">编码方式</param>
        ///<returns>密文</returns>
        private string MD5(string str, string charset)
        {
            byte[] buffer = System.Text.Encoding.GetEncoding(charset).GetBytes(str);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider check;
                check = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] somme = check.ComputeHash(buffer);
                string ret = "";
                foreach (byte a in somme)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("X");
                    else
                        ret += a.ToString("X");
                }
                return ret.ToLower();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="charset">编码方式</param>
        /// <returns></returns>
        private string base64(String str, String charset)
        {
            return Convert.ToBase64String(System.Text.Encoding.GetEncoding(charset).GetBytes(str));
        }
    }
}



    

