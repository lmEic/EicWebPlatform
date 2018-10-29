using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Quality.RmaManage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Web;
using Newtonsoft.Json.Linq;
using Xfrog.Net;

namespace Lm.Eic.App.Business.BmpTests.Quantity
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void QsTestMethod()
        {
            for (int i=1;i<=31;i++)
            {
                string days = i.ToString("00");
              //  InspectionService.DataGatherManager.FqcDataGather.AddonlyDataDatialDatas("2018-01-"+ days);
            }
             
            //  /// 测工单从ERP中得到物料信息


            //DateTime statDate = DateTime.Now .Date ;
            //DateTime endDate = DateTime.Now.Date;
            // var m= InspectionService.DataGatherManager.IqcDataGather.GetOrderIdList(statDate, endDate);
            //var mm =QuantityServices. SampleManger.SampleItemsIqcRecordManager.GetPringSampleItemBy("591-1607032", "32AAP00001200RM");
            // var ms = QuantityServices.SampleManger.MaterialSampleItemsManager.GetMaterilalSampleItemBy("32AAP00001200RM");
            // System.IO.MemoryStream stream= QuantityServices. SampleManger.SampleItemsIqcRecordManager.ExportPrintToExcel(mm);
            //#region 输出到Excel
            //string path = @"E:\\IQC.xls";
            // using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            // {
            //     byte[] bArr = stream.ToArray();
            //     fs.Write(bArr, 0, bArr.Length);
            //     fs.Flush();

            // }
            // #endregion
            var listDatas = RmaService.RmaManager.BusinessManageProcessor.GetErpBusinessInfoDatasBy("241-160909001");
            if (listDatas == null)
                Assert.Fail();
        }




    }


}




 
//----------------------------------
// 常用快递调用示例代码 － 聚合数据
// 在线接口文档：http://www.juhe.cn/docs/43
// 代码中JsonObject类下载地址:http://download.csdn.net/download/gcm3206021155665/7458439
//----------------------------------
 

// {{resultcode: '200',reason: '成功的返回',
//result: {company: '顺丰',com: 'sf',no: '765465920545',status: '1',
// list: [
// {datetime: '2018-06-2219:04:23', remark: '顺丰速运已收取快件', zone: ''},
// {datetime: '2018-06-2219:15:23',remark: '顺丰速运已收取快件',zone: ''},
// {datetime: '2018-06-2219:48:18',remark: '"快件在【宁波市北仑区庐山中路营业点】已装车'},
// {datetime: '2018-06-2219:54:01',remark: '快件已发车',zone: ''},
// {datetime: '2018-06-2220:49:06',remark: '快件到达【宁波姜山集散点】',zone: ''},
// {datetime: '2018-06-2300:03:02',remark: '"快件在【宁波姜山集散点】已装车'}, 
// {datetime: '2018-06-2300:28:19',remark: '快件已发车',zone: ''},
// {datetime: '2018-06-2300:56:03',remark: '快件到达【宁波总集散中心】',zone: ''},
// {datetime: '2018-06-2303:55:28',remark: '"快件在【宁波总集散中心】已装车'}, 
// {datetime: '2018-06-2310:10:47',remark: '"快件在【广州白云集散中心】已装车'}, 
// {datetime: '2018-06-2310:10:47',remark: '快件到达【广州白云集散中心】',zone: ''},
// {datetime: '2018-06-2311:56:53',remark: '快件已发车',zone: ''},
// {datetime: '2018-06-2314:27:14',remark: '快件到达【惠州陈江集散中心】',zone: ''},
// {datetime: '2018-06-2315:19:40',remark: '"快件在【惠州陈江集散中心】已装车'},
// {datetime: '2018-06-2315:49:25',remark: '快件已发车',zone: ''}, 
// {datetime: '2018-06-2316:57:06',remark: '快件到达【惠州市惠城区三栋速运营业点】',zone: ''}, 
// {datetime: '2018-06-2317:01:41',remark: '"正在派送途中',请您准备签收(派件人: '何峰', 电话: '18507524446)"', zone: ''},
// {datetime: '2018-06-2317:06:09',remark: '快件交给何峰，正在派送途中（联系电话：18507524446）',zone: ''},
// {datetime: '2018-06-2317:32:26',remark: '感谢使用顺丰'}
//]},error_code: 0}}

namespace ConsoleAPI
{
    class Program
    {
        static void ExperssQueryInfo(string com,string orderid)
        {
            string appkey = "632ef06adef10eecfd1921ead5226c42"; //配置您申请的appkey


            //1.常用快递查询API
            string url1 = "http://v.juhe.cn/exp/index";

            var parameters1 = new Dictionary<string, string>();

            parameters1.Add("com", "sf"); //需要查询的快递公司编号
            parameters1.Add("no", "765465920545"); //需要查询的订单号
            parameters1.Add("key", appkey);//你申请的key
            parameters1.Add("dtype", ""); //返回数据的格式,xml或json，默认json

            string result1 = sendPost(url1, parameters1, "get");

            JsonObject newObj1 = new JsonObject(result1);
            String errorCode1 = newObj1["error_code"].Value;
         
            if (errorCode1 == "0")
            {

                Debug.WriteLine("成功");
                Debug.WriteLine(newObj1);
            }
            else
            {
                Debug.WriteLine("失败");
                Debug.WriteLine(newObj1["error_code"].Value + ":" + newObj1["reason"].Value);
            }


            //2.快递公司编号对照表
            string url2 = "http://v.juhe.cn/exp/com";

            var parameters2 = new Dictionary<string, string>();


            string result2 = sendPost(url2, parameters2, "get");

            JsonObject newObj2 = new JsonObject(result2);
            String errorCode2 = newObj2["error_code"].Value;
          
            if (errorCode2 == "0")
            {
                Debug.WriteLine("成功");
                //Debug.WriteLine(newObj2);
            }
            else
            {
                Debug.WriteLine("失败");
                Debug.WriteLine(newObj2["error_code"].Value + ":" + newObj2["reason"].Value);
            }


        }

        /// <summary>
                /// Http (GET/POST)
                /// </summary>
                /// <param name="url">请求URL</param>
                /// <param name="parameters">请求参数</param>
                /// <param name="method">请求方法</param>
                /// <returns>响应内容</returns>
        static string sendPost(string url, IDictionary<string, string> parameters, string method)
        {
            if (method.ToLower() == "post")
            {
                HttpWebRequest req = null;
                HttpWebResponse rsp = null;
                System.IO.Stream reqStream = null;
                try
                {
                    req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = method;
                    req.KeepAlive = false;
                    req.ProtocolVersion = HttpVersion.Version10;
                    req.Timeout = 5000;
                    req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                    byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
                    reqStream = req.GetRequestStream();
                    reqStream.Write(postData, 0, postData.Length);
                    rsp = (HttpWebResponse)req.GetResponse();
                    Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                    return GetResponseAsString(rsp, encoding);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (reqStream != null) reqStream.Close();
                    if (rsp != null) rsp.Close();
                }
            }
            else
            {
                //创建请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + BuildQuery(parameters, "utf8"));

                //GET请求
                request.Method = "GET";
                request.ReadWriteTimeout = 5000;
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                //返回内容
                string retString = myStreamReader.ReadToEnd();
                return retString;
            }
        }

        /// <summary>
                /// 组装普通文本请求参数。
                /// </summary>
                /// <param name="parameters">Key-Value形式请求参数字典</param>
                /// <returns>URL编码后的请求数据</returns>
        static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))//&& !string.IsNullOrEmpty(value)
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }
            return postData.ToString();
        }

        /// <summary>
                /// 把响应流转换为文本。
                /// </summary>
                /// <param name="rsp">响应流对象</param>
                /// <param name="encoding">编码方式</param>
                /// <returns>响应文本</returns>
        static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            System.IO.Stream stream = null;
            StreamReader reader = null;
            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }
    }
}
