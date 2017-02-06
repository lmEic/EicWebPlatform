using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Lm.Eic.App.Business.Attendance
{
    public  class AttendanceBusiness
    {

        /// <summary>
        /// 考勤机打开连接
        /// </summary>
        /// <param name="ipAddress">Ip</param>
        /// <param name="machineNumber">机器Number</param>
        /// <param name="strMessage">返回信息</param>
        /// <returns></returns>
       public bool OpenConnent(string ipAddress, int machineNumber, out string strMessage)
        {
            bool isPing = PingIPAdress(ipAddress);
            bool vResult = false;
            if (isPing)
            {
                //先关闭 机器端口数据
                AttendanceMachineDll.Disconnect(machineNumber);

                //打开 联结
                vResult = AttendanceMachineDll.ConnectTcpip(machineNumber, ipAddress, 5005, 0);
                if (!vResult)
                {
                    strMessage = (ipAddress + "——端口或机器名错误");
                }
                else strMessage = (ipAddress + "——连接成功");
            }
            else
            {
                strMessage = (ipAddress + "——Ping不通，未连接至网咯！");
            }

            return isPing && vResult;
        }
        /// <summary>
        /// Ip地址是否Ping通
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool PingIPAdress(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000; // Timeout 时间，单位：毫秒
            System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceMachineNumber"></param>
        /// <param name="sourceDataTablestructure">数据源结构</param>
        /// <returns></returns>
        private DataTable GetNewAddEnrollSourceData(int sourceMachineNumber, DataTable sourceDataTablestructure)
        {

            DataTable dbEnrollTble = sourceDataTablestructure;

            int[] gTemplngEnrollDataInt = new int[6917];
            Byte[] gbytEnrollDataInt = new Byte[34585];



            int glngEnrollPData;

            int wokerId = 0;
            int machineNumber = 0;
            //指纹数
            int vFingerNumber = 0;
            int vPrivilege = 0;
            string vName = string.Empty;


            int vEnable = 0;

            int vMsgRet;
            //输出错误信息代码
            int vErrorCode = 0;


            GCHandle gh;
            //Application.DoEvents();

            Boolean vRet;
            //锁定机器
            vRet = AttendanceMachineDll.EnableDevice(sourceMachineNumber, 0); // 0 : false
            if (vRet)
            {
                //准备机器读取状态
                vRet = AttendanceMachineDll.ReadAllUserID(sourceMachineNumber);
                //WriteLog("ReadAllUserID OK");
            }
            else
            {
                //得到错误的状态码
                AttendanceMachineDll.GetLastError(sourceMachineNumber, out vErrorCode);
                //解除锁定
                AttendanceMachineDll.EnableDevice(sourceMachineNumber, 1); // 1 : true
                return null;
            }

            //*'*/---- Get Enroll data and save into database -------------
            //Cursor = System.Windows.Forms.Cursors.WaitCursor;
            //dbEnrollTble = dsEnrollDatas.Tables[0];
            //gGetState = true;

            while (true)
            {
                vRet = AttendanceMachineDll.GetAllUserID(sourceMachineNumber, out wokerId, out machineNumber, out vFingerNumber, out vPrivilege, out vEnable);
                if (!vRet) break;
                EEE:
                //                if (vFingerNumber == 10) vFingerNumber = 15;
                gh = GCHandle.Alloc(gTemplngEnrollDataInt, GCHandleType.Pinned);
                IntPtr AddrOfTemplngEnrollData = gh.AddrOfPinnedObject();

                if (vFingerNumber >= 50)
                    continue;
                vRet = AttendanceMachineDll.GetEnrollData1(sourceMachineNumber, wokerId, vFingerNumber, out vPrivilege, AddrOfTemplngEnrollData, out glngEnrollPData);
                if (!vRet)
                {


                    AttendanceMachineDll.GetLastError(sourceMachineNumber, out vErrorCode);
                    vMsgRet = AttendanceMachineDoMessage.MessageBox(new IntPtr(0), AttendanceMachineDoMessage.ErrorPrint(vErrorCode) + ": Continue ?", "GetEnrollData", 4);
                    if (vMsgRet == 6/*MsgBoxResult.Yes*/)
                    {
                        goto EEE;
                    }
                    else if (vMsgRet == 7/*MsgBoxResult.Cancel*/)
                    {
                        //Cursor = System.Windows.Forms.Cursors.Default;
                        AttendanceMachineDll.EnableDevice(sourceMachineNumber, 1); // 1 : true
                        //                        gGetState = false;
                        return null;
                    }
                }
                DataRow dbRow = dbEnrollTble.NewRow();
                dbRow["EMachineNumber"] = machineNumber;
                dbRow["EnrollNumber"] = wokerId;
                dbRow["FingerNumber"] = vFingerNumber;
                dbRow["Privilige"] = vPrivilege;
                if (vFingerNumber == 10 || vFingerNumber == 15 || vFingerNumber == 11)
                {
                    dbRow["Password1"] = glngEnrollPData;
                }
                else
                { dbRow["Password1"] = 0; }
                dbRow["FPdata"] = doFingerData(gTemplngEnrollDataInt, gbytEnrollDataInt);
                //得用户姓名
                AttendanceMachineDll.GetUserName1(sourceMachineNumber, wokerId, out vName);
                dbRow["EnrollName"] = vName;

                dbEnrollTble.Rows.Add(dbRow);
               // Application.DoEvents();
            }
            //WriteLog("Total : " + dsEnrollDatas.Tables["tblEnroll"].Rows.Count);
            
            //dsChange = dsEnrollDatas.GetChanges();
            //保存到数据库中

            //SQLEnrollData.DataModule.SaveEnrolls(dbEnrollTble);


            //Application.DoEvents();
            AttendanceMachineDll.EnableDevice(sourceMachineNumber, 1); // 1 : true
            return dbEnrollTble;
        }


        /// <summary>
        /// 处理采集的数据
        /// </summary>
        /// <param name="gTemplngEnrollDataInt"></param>
        /// <param name="gbytEnrollDataInt"></param>
        /// <returns></returns>
        private byte[] doFingerData(int[] gTemplngEnrollDataInt, byte[] gbytEnrollDataInt)
        {
            byte[] gbyt = new Byte[34585];
            for (int i = 0; i < 6917; i++)
            {
                gbytEnrollDataInt[i * 5] = 1;
                if (gTemplngEnrollDataInt[i] == unchecked((int)0x80000000u))
                {
                    gbytEnrollDataInt[i * 5] = 0;
                    gTemplngEnrollDataInt[i] = 0;
                }
                else if (gTemplngEnrollDataInt[i] < 0)
                {
                    gbytEnrollDataInt[i * 5] = 0;
                    gTemplngEnrollDataInt[i] = -gTemplngEnrollDataInt[i];
                }
                gbytEnrollDataInt[i * 5 + 1] = (Byte)(gTemplngEnrollDataInt[i] / 256 / 256 / 256);
                gbytEnrollDataInt[i * 5 + 2] = (Byte)((gTemplngEnrollDataInt[i] / 256 / 256) % 256);
                gbytEnrollDataInt[i * 5 + 3] = (Byte)((gTemplngEnrollDataInt[i] / 256) % 256);
                gbytEnrollDataInt[i * 5 + 4] = (Byte)(gTemplngEnrollDataInt[i] % 256);
            }
            for (int i = 0; i < 34585; i++)
                gbyt[i] = gbytEnrollDataInt[i];
            return gbyt;
        }
    }
}
