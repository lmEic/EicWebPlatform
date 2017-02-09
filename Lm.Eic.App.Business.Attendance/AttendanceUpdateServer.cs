using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml;

namespace Lm.Eic.App.Business.Attendance
{
    #region   Terminal
    /// <summary>
    /// 时间日志回调
    /// </summary>
    /// <returns></returns>
    public delegate Boolean TimeLogCallback(
        String TerminalType,
        Int32 TerminalID,
        String SerialNumber,
        Int32 TransactionID,
        DateTime LogTime,
        Int64 UserID,
        Int32 DoorID,
        String AttendanceStatus,
        String VerifyMode,
        Int32 JobCode,
        String Antipass,
        Byte[] Photo);


    public delegate Boolean AdminLogCallback(
        String TerminalType,
        Int32 TerminalID,
        String SerialNumber,
        Int32 TransactionID,
        DateTime LogTime,
        Int64 AdminID,
        Int64 UserID,
        String Action,
        Int32 Result);

    public delegate Boolean AlarmLogCallback(
        String TerminalType,
        Int32 TerminalID,
        String SerialNumber,
        Int32 TransactionID,
        DateTime LogTime,
        Int64 UserID,
        Int32 DoorID,
        String AlarmType);

    public delegate void PingCallback(
        String TerminalType,
        Int32 TerminalID,
        String SerialNumber,
        Int32 TransactionID);
    /// <summary>
    /// 接受终端
    /// </summary>
    public  class AttendanceUpdateTerminal : IDisposable
    {
        public Boolean m_Disposed;
        public TcpClient m_Client;
        public NetworkStream m_Stream;
        public Timer m_TimerAlive;
        public Byte[] m_TmpBuffer;
        public Byte[] m_RxBuffer;
        public int m_RxCount;

        private const int MaxMessageSize = 2048 + 8 * 1024 * 2;     // Maximum message size // 18K
        private const int PingTimeout = 30 * 1000;  // Ping timeout

        public TimeLogCallback m_TimeLogCallBack = null;
        public AdminLogCallback m_AdminLogCallBack = null;
        public AlarmLogCallback m_AlarmLogCallBack = null;
        public PingCallback m_PingCallBack = null;
        // Clean up client
        private void CleanUp(Boolean disposing)
        {
            if (m_Disposed)
                return;

            m_Disposed = true;

            if (disposing)
            {
                // Dispose client objects
                m_TimerAlive.Change(Timeout.Infinite, Timeout.Infinite);
                try
                {
                    m_Stream.Close();
                    m_Client.Close();
                }
                catch
                {
                }
            }
        }

        public void Dispose()
        {
            CleanUp(true);
        }

        public AttendanceUpdateTerminal(TimeLogCallback timeLogCallback,
            AdminLogCallback adminLogCallback,
            AlarmLogCallback alarmLogCallback,
            PingCallback pingCallback)
        {
            // Initialize objects.
            m_Disposed = false;

            m_TimeLogCallBack = timeLogCallback;
            m_AdminLogCallBack = adminLogCallback;
            m_AlarmLogCallBack = alarmLogCallback;
            m_PingCallBack = pingCallback;

            // Message buffer
            m_TmpBuffer = new Byte[MaxMessageSize];
            m_RxBuffer = new Byte[MaxMessageSize];
            m_RxCount = 0;

            m_TimerAlive = new Timer(new TimerCallback(this.OnAliveTimerExpired));
            RestartAliveTimer();
        }

        ~AttendanceUpdateTerminal()
        {
            CleanUp(false);
        }

        // When alive timer is expired
        public void OnAliveTimerExpired(Object stateInfo)
        {
            this.Dispose();
        }

        // Restart alive timer
        public void RestartAliveTimer()
        {
            m_TimerAlive.Change(PingTimeout, Timeout.Infinite);
        }

        // Establish connection to terminal
        public void EstablishConnect(TcpClient client)
        {
            m_Client = client;
            m_Stream = m_Client.GetStream();

            RestartAliveTimer();
            m_Stream.BeginRead(m_TmpBuffer, 0, MaxMessageSize,
                new AsyncCallback(AttendanceUpdateTerminal.OnReceive), this);
        }

        // Send the stream to terminal
        public static void OnSend(IAsyncResult iar)
        {
            AttendanceUpdateTerminal term = (AttendanceUpdateTerminal)iar.AsyncState;
            try
            {
                term.m_Stream.EndWrite(iar);
            }
            catch
            {

            }
        }

        // Check message is end
        private static Boolean CheckMessageEnd(Byte c)
        {
            return (c == 0);
        }

        private string GetElementValue(XmlDocument doc, string elementName)
        {
            foreach (XmlElement x in doc.DocumentElement.ChildNodes)
            {
                if (x.Name == elementName)
                    return x.InnerText;
            }
            throw new Exception();
        }

        private void SendReply(String replyType, Int32 transId)
        {
            string msg =
                "<?xml version=\"1.0\"?>" +
                "<Message>" +
                "<Request>" +
                replyType +
                "</Request>" +
                "<TransID>" +
                transId.ToString() +
                "</TransID>" +
                "</Message>";

            byte[] buffer = new byte[msg.Length + 1];
            System.Text.Encoding.ASCII.GetBytes(msg).CopyTo(buffer, 0);
            buffer[msg.Length] = 0;
            m_Stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(AttendanceUpdateTerminal.OnSend), this);
        }

        private void OnTimeLog(XmlDocument doc, String termType, Int32 termId, String serialNumber, Int32 transId)
        {
            Int32 year, month, day, hour, minute, second;
            Int64 userID;
            Int32 doorID;
            String attendStatus;
            String verifyMode;
            Int32 jobCode;
            String antipassStatus;
            Byte[] photo;

            //---------------------- Log Time
            try
            {
                year = int.Parse(GetElementValue(doc, "Year"));
                month = int.Parse(GetElementValue(doc, "Month"));
                day = int.Parse(GetElementValue(doc, "Day"));
                hour = int.Parse(GetElementValue(doc, "Hour"));
                minute = int.Parse(GetElementValue(doc, "Minute"));
                second = int.Parse(GetElementValue(doc, "Second"));
            }
            catch (System.Exception)
            {
                year = 0;
                month = 0;
                day = 0;
                hour = 0;
                minute = 0;
                second = 0;
            }

            //---------------------- User ID
            try
            {
                userID = Int64.Parse(GetElementValue(doc, "UserID"));
            }
            catch (System.Exception)
            {
                userID = -1;
            }

            //---------------------- Door ID
            try
            {
                doorID = Int32.Parse(GetElementValue(doc, "DoorID"));
            }
            catch (System.Exception)
            {
                doorID = -1;
            }

            //---------------------- Time attendance status
            try
            {
                attendStatus = GetElementValue(doc, "AttendStat");
            }
            catch (System.Exception)
            {
                attendStatus = "";
            }

            //---------------------- Verification mode
            try
            {
                verifyMode = GetElementValue(doc, "VerifMode");
            }
            catch (System.Exception)
            {
                verifyMode = "";
            }

            //---------------------- Jobcode
            try
            {
                jobCode = Int32.Parse(GetElementValue(doc, "JobCode"));
            }
            catch (System.Exception)
            {
                jobCode = -1;
            }

            //---------------------- Antipass status
            try
            {
                antipassStatus = GetElementValue(doc, "APStat");
            }
            catch (System.Exception)
            {
                antipassStatus = "";
            }

            // Photo taken
            photo = null;
            try
            {
                if (GetElementValue(doc, "Photo") == "Yes")
                {
                    String logImage = GetElementValue(doc, "LogImage");
                    if (logImage != null)
                        photo = Convert.FromBase64String(logImage);
                }
            }
            catch (System.Exception)
            {
                photo = null;
            }

            Boolean logProcessed = false;
            try
            {
                if (m_TimeLogCallBack != null)
                    logProcessed = m_TimeLogCallBack(termType, termId, serialNumber, transId, new DateTime(year, month, day, hour, minute, second), userID, doorID, attendStatus, verifyMode, jobCode, antipassStatus, photo);
            }
            catch (Exception)
            {
            }
            if (logProcessed)
                SendReply("UploadedLog", transId);
            else
                SendReply("Error", transId);
        }

        private void OnAdminLog(XmlDocument doc, String termType, Int32 termId, String serialNumber, Int32 transId)
        {
            Int32 year, month, day, hour, minute, second;
            Int64 adminID;
            Int64 userID;
            String action;
            Int32 result;

            //--------------------- Log Time
            try
            {
                year = int.Parse(GetElementValue(doc, "Year"));
                month = int.Parse(GetElementValue(doc, "Month"));
                day = int.Parse(GetElementValue(doc, "Day"));
                hour = int.Parse(GetElementValue(doc, "Hour"));
                minute = int.Parse(GetElementValue(doc, "Minute"));
                second = int.Parse(GetElementValue(doc, "Second"));
            }
            catch (System.Exception)
            {
                year = 0;
                month = 0;
                day = 0;
                hour = 0;
                minute = 0;
                second = 0;
            }

            //--------------------- Administrator ID
            try
            {
                adminID = Int64.Parse(GetElementValue(doc, "AdminID"));
            }
            catch (System.Exception)
            {
                adminID = -1;
            }

            //---------------------- User ID
            try
            {
                userID = Int64.Parse(GetElementValue(doc, "UserID"));
            }
            catch (System.Exception)
            {
                userID = -1;
            }

            //---------------------- Action
            try
            {
                action = GetElementValue(doc, "Action");
            }
            catch (System.Exception)
            {
                action = "";
            }

            //---------------------- Result
            try
            {
                result = Int32.Parse(GetElementValue(doc, "Stat"));
            }
            catch (System.Exception)
            {
                result = -1;
            }

            Boolean logProcessed = false;
            try
            {
                if (m_AdminLogCallBack != null)
                    logProcessed = m_AdminLogCallBack(termType, termId, serialNumber, transId, new DateTime(year, month, day, hour, minute, second), adminID, userID, action, result);
            }
            catch (Exception)
            {
            }
            if (logProcessed)
                SendReply("UploadedLog", transId);
            else
                SendReply("Error", transId);
        }

        private void OnAlarmLog(XmlDocument doc, String termType, Int32 termId, String serialNumber, Int32 transId)
        {
            Int32 year, month, day, hour, minute, second;
            Int64 userID;
            Int32 doorID;
            String alarmType;

            //--------------------- Log Time
            try
            {
                year = int.Parse(GetElementValue(doc, "Year"));
                month = int.Parse(GetElementValue(doc, "Month"));
                day = int.Parse(GetElementValue(doc, "Day"));
                hour = int.Parse(GetElementValue(doc, "Hour"));
                minute = int.Parse(GetElementValue(doc, "Minute"));
                second = int.Parse(GetElementValue(doc, "Second"));
            }
            catch (System.Exception)
            {
                year = 0;
                month = 0;
                day = 0;
                hour = 0;
                minute = 0;
                second = 0;
            }

            //---------------------- User ID
            try
            {
                userID = Int64.Parse(GetElementValue(doc, "UserID"));
            }
            catch (System.Exception)
            {
                userID = -1;
            }

            //---------------------- Door ID
            try
            {
                doorID = Int32.Parse(GetElementValue(doc, "DoorID"));
            }
            catch (System.Exception)
            {
                doorID = -1;
            }

            //---------------------- Alarm Type
            try
            {
                alarmType = GetElementValue(doc, "Type");
            }
            catch (System.Exception)
            {
                alarmType = "";
            }

            Boolean logProcessed = false;
            try
            {
                if (m_AlarmLogCallBack != null)
                    logProcessed = m_AlarmLogCallBack(termType, termId, serialNumber, transId, new DateTime(year, month, day, hour, minute, second), userID, doorID, alarmType);
            }
            catch (Exception)
            {
            }
            if (logProcessed)
                SendReply("UploadedLog", transId);
            else
                SendReply("Error", transId);
        }

        private void OnPing(XmlDocument doc, String termType, Int32 termId, String serialNumber, Int32 transId)
        {
            SendReply("KeptAlive", transId);
            if (m_PingCallBack != null)
                m_PingCallBack(termType, termId, serialNumber, transId);
        }

        // Parse a message
        private void ParseMessage(string message)
        {
            XmlDocument doc = new XmlDocument();

            String termType;
            Int32 termId;
            String serialNumber;
            String eventType;
            Int32 transId;

            doc.Load(new System.IO.StringReader(message));

            //----------------- Terminal type
            try
            {
                termType = GetElementValue(doc, "TerminalType");
            }
            catch (System.Exception)
            {
                termType = "";
            }

            //----------------- Terminal ID
            try
            {
                termId = Int32.Parse(GetElementValue(doc, "TerminalID"));
            }
            catch (System.Exception)
            {
                termId = -1;
            }

            //----------------- Serial Number
            try
            {
                serialNumber = GetElementValue(doc, "DeviceSerialNo");
            }
            catch (System.Exception)
            {
                serialNumber = "";
            }

            //----------------- Transaction ID
            try
            {
                transId = Int32.Parse(GetElementValue(doc, "TransID"));
            }
            catch (System.Exception)
            {
                transId = -1;
            }

            //------------------ Event
            try
            {
                eventType = GetElementValue(doc, "Event");
            }
            catch (System.Exception)
            {
                eventType = "";
            }

            switch (eventType)
            {
                case "TimeLog":
                    OnTimeLog(doc, termType, termId, serialNumber, transId);
                    break;

                case "AdminLog":
                    OnAdminLog(doc, termType, termId, serialNumber, transId);
                    break;

                case "Alarm":
                    OnAlarmLog(doc, termType, termId, serialNumber, transId);
                    break;

                case "KeepAlive":
                    OnPing(doc, termType, termId, serialNumber, transId);
                    break;
            }
        }

        public Boolean ParseBuffer(out Int32 consumed)
        {
            Byte[] data = m_RxBuffer;
            int size = m_RxCount;
            consumed = 0;

            if (m_RxCount == MaxMessageSize)
                return false;

            Int32 end = Array.FindIndex(data, 0, m_RxCount, CheckMessageEnd);
            if (end == -1)
            {
                consumed = 0;
                return true;
            }

            ParseMessage(System.Text.Encoding.ASCII.GetString(data, 0, end));

            for (; end < m_RxCount; end++)
            {
                if (data[end] != 0)
                    break;
            }
            if (end != m_RxCount)
                end++;
            consumed = end;

            return true;
        }

        public static void OnReceive(IAsyncResult iar)
        {
            AttendanceUpdateTerminal term = (AttendanceUpdateTerminal)iar.AsyncState;

            int recieved;
            try
            {
                recieved = term.m_Stream.EndRead(iar);
                if (recieved <= 0)
                    throw new Exception("connection closed");

                if (recieved > MaxMessageSize - term.m_RxCount)
                    recieved = MaxMessageSize - term.m_RxCount;

                Array.Copy(term.m_TmpBuffer, 0, term.m_RxBuffer, term.m_RxCount, recieved);
                term.m_RxCount += recieved;

                while (term.m_RxCount > 0)
                {
                    int consumed;

                    // Parse Buffer
                    if (!term.ParseBuffer(out consumed))
                        throw new Exception("handle failed");

                    // Remove Consumed Part of Buffer
                    if (consumed > 0)
                    {
                        term.m_RxCount -= consumed;
                        Array.Copy(term.m_RxBuffer, consumed, term.m_RxBuffer, 0, term.m_RxCount);
                    }
                    else
                    {
                        break;
                    }
                }

                // Restart alive timer
                term.RestartAliveTimer();
                term.m_Stream.BeginRead(term.m_TmpBuffer, 0, MaxMessageSize,
                    new AsyncCallback(AttendanceUpdateTerminal.OnReceive), term);
            }
            catch
            {
                term.Dispose();
            }
        }
    }

    #endregion


    #region  LogServer
/// <summary>
/// 数据上传服务
/// </summary>
    public class AttendanceUpdateLogServer : IDisposable
    {
        public Boolean m_Disposed;
        public UInt16 m_PortNo;
        public TcpListener m_Listner;
        static LinkedList<AttendanceUpdateTerminal> m_TerminalList = new LinkedList<AttendanceUpdateTerminal>();

        public TimeLogCallback m_TimeLogCallBack = null;
        public AdminLogCallback m_AdminLogCallBack = null;
        public AlarmLogCallback m_AlarmLogCallBack = null;
        public PingCallback m_PingCallBack = null;

        public AttendanceUpdateLogServer(UInt16 portNo,
            TimeLogCallback timeLogCallback,
            AdminLogCallback adminLogCallback,
            AlarmLogCallback alarmLogCallback,
            PingCallback pingCallback)
        {
            // Initialize objects.
            m_Disposed = false;
            m_PortNo = portNo;
            m_TimeLogCallBack = timeLogCallback;
            m_AdminLogCallBack = adminLogCallback;
            m_AlarmLogCallBack = alarmLogCallback;
            m_PingCallBack = pingCallback;

            // Start TCP Listner.
            m_Listner = new TcpListener(IPAddress.Any, m_PortNo);
            m_Listner.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            m_Listner.Start();

            // Begin Accept.
            m_Listner.BeginAcceptTcpClient(new AsyncCallback(AttendanceUpdateLogServer.OnAccept), this);
        }

        ~AttendanceUpdateLogServer()
        {
            CleanUp(false);
        }

        private void CleanUp(bool dispose)
        {
            if (m_Disposed)
                return;

            m_Disposed = true;

            if (dispose)
            {
                // Dispose the listener and terminals.
                try
                {
                    m_Listner.Stop();
                    foreach (AttendanceUpdateTerminal e in m_TerminalList)
                    {
                        if (e != null)
                            e.Dispose();
                    }
                }
                catch
                {

                }
            }
        }

        public void Dispose()
        {
            CleanUp(true);
        }

        public static void OnAccept(IAsyncResult iar)
        {
            AttendanceUpdateLogServer server = (AttendanceUpdateLogServer)iar.AsyncState;
            AttendanceUpdateTerminal term = new AttendanceUpdateTerminal(server.m_TimeLogCallBack,
                server.m_AdminLogCallBack,
                server.m_AlarmLogCallBack,
                server.m_PingCallBack);

            try
            {
                // Establish connection and add a terminal into the list.
                term.EstablishConnect(server.m_Listner.EndAcceptTcpClient(iar));
                m_TerminalList.AddLast(term);
            }
            catch
            {
                term.Dispose();
            }

            try
            {
                // For disposed listener.
                server.m_Listner.BeginAcceptTcpClient(new AsyncCallback(AttendanceUpdateLogServer.OnAccept), server);
            }
            catch
            {

            }
        }
    }


    #endregion
}
