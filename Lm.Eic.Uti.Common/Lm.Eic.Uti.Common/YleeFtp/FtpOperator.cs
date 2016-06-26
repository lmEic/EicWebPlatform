using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using Lm.Eic.Uti.LogMessageNet.LeeLog4Net;

namespace Lm.Eic.Uti.Common.YleeFtp
{
    /// <summary>
    /// Ftp操作器
    /// </summary>
    public class FtpOperator
    {
        #region Member
        private FtpWebRequest request;
        private FtpWebResponse response;
        #endregion

        #region Property
        private FtpConnectPassport passport = null;
        /// <summary>
        /// Ftp连接凭证
        /// </summary>
        public FtpConnectPassport ConnectPassport
        {
            get { return passport; }
        }

        private FtpOpreateDirectoryPath operateDirectoryPath = null;
        /// <summary>
        /// Ftp操作文件路径信息
        /// </summary>
        public FtpOpreateDirectoryPath OperateDirectoryPath
        {
            get { return operateDirectoryPath; }
        }
        /// <summary>
        /// 是否需要删除临时文件
        /// </summary>
        private bool _isDeleteTempFile = false;
        /// <summary>
        /// 异步上传所临时生成的文件
        /// </summary>
        private string _UploadTempFile = "";
        #endregion

        #region try
        private void Trydo(Action dohanlder)
        {
            try
            {
                dohanlder();
            }
            catch (Exception ex)
            {
                Log4NetHelper.ErrorLogger.LogError(ex);
            }
        }
        private void Trydo(string operationname, Action dohandler)
        {
            try
            {
                dohandler();
            }
            catch (Exception ex)
            {
                Log4NetHelper.ErrorLogger.LogError(operationname, ex);
            }
        }
        private bool Trydo(string operationname, Func<bool> dohandler)
        {
            bool isopsuccess = false;
            try
            {
                return dohandler();
            }
            catch (Exception ex)
            {
                Log4NetHelper.ErrorLogger.LogError(ex);
            }
            return isopsuccess;
        }

        private void Logmsg(string msg)
        {
            Logmsg(msg);
        }
        #endregion

        #region Constructor
        public FtpOperator(FtpConnectPassport passport)
        {
            Trydo(() => {
                this.passport = passport;
                this.operateDirectoryPath = new FtpOpreateDirectoryPath();
                string _DirectoryPath = passport.FtpUri.AbsolutePath;
                if (!_DirectoryPath.EndsWith("/"))
                {
                    _DirectoryPath += "/";
                }
                this.OperateDirectoryPath.CurrentDirectoryPath = _DirectoryPath;
                if (passport != null && passport.FtpUri != null)
                {
                    passport.FtpUri = new Uri(passport.FtpUri.GetLeftPart(UriPartial.Authority));
                }
            });
        }
        public FtpOperator()
        {
            this.passport = new FtpConnectPassport()
            {
                FtpUri = null,
                Proxy = null,
                UserName = "anonymous",
                Password = "@anonymous"
            };
        }
        ~FtpOperator()
        {
            if (response != null)
            {
                response.Close();
                response = null;
            }
            if (request != null)
            {
                request.Abort();
                request = null;
            }
        }
        #endregion

        #region 建立连接
        /// <summary>
        /// 建立FTP链接,返回响应对象
        /// </summary>
        /// <param name="ftpMethod">操作命令</param>
        /// <returns></returns>
        private FtpWebResponse GetFtpReponse(Uri uri, string ftpMethod)
        {
            FtpWebResponse rep=null;
            Trydo("GetFtpReponse", () => {
                request = (FtpWebRequest)WebRequest.Create(uri);
                request.Method = ftpMethod;
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(passport.UserName, passport.Password);
                if (passport.Proxy != null)
                {
                    request.Proxy = passport.Proxy;
                }
                rep= (FtpWebResponse)request.GetResponse();
            });
            return rep;
        }
        /// <summary>
        /// 建立FTP链接,返回请求对象
        /// </summary>
        /// <param name="ftpMethod">操作命令</param>
        /// <returns></returns>
        private FtpWebRequest CreateFtpRequest(Uri uri, string ftpMethod)
        {
            Trydo("CreateFtpRequest", () =>
            {
                request = (FtpWebRequest)WebRequest.Create(this.passport.FtpUri);
                request.Method = ftpMethod;
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(passport.UserName, passport.Password);
                if (passport.Proxy != null)
                {
                    request.Proxy = passport.Proxy;
                }
            });
            return request;
        }
        #endregion

        #region 事件
        public delegate void De_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e);
        public delegate void De_DownloadDataCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
        public delegate void De_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e);
        public delegate void De_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e);

        /// <summary>
        /// 异步下载进度发生改变触发的事件
        /// </summary>
        public event De_DownloadProgressChanged DownloadProgressChanged;
        /// <summary>
        /// 异步下载文件完成之后触发的事件
        /// </summary>
        public event De_DownloadDataCompleted DownloadDataCompleted;
        /// <summary>
        /// 异步上传进度发生改变触发的事件
        /// </summary>
        public event De_UploadProgressChanged UploadProgressChanged;
        /// <summary>
        /// 异步上传文件完成之后触发的事件
        /// </summary>
        public event De_UploadFileCompleted UploadFileCompleted;
        #endregion

        #region common method
        private string CreateDownOrUpFileUri(string downOrUpPath, string filename)
        {
            string uri = downOrUpPath != null ? this.passport.FtpUri.ToString() + downOrUpPath + "\\" + filename : this.passport.FtpUri.ToString() + operateDirectoryPath.CurrentDirectoryPath + "\\" + filename;
            return uri;
        }
        #endregion

        #region 异步下载文件
        /// <summary>
        /// 从FTP服务器异步下载文件，指定本地路径和本地文件名
        /// </summary>
        /// <param name="RemoteFileName">远程文件名</param>        
        /// <param name="LocalPath">保存文件的本地路径,后面带有"\"</param>
        /// <param name="LocalFileName">保存本地的文件名</param>
        public void DownloadFileAsync(string RemoteFileName, string LocalPath, string LocalFileName)
        {
            Trydo("DownloadFileAsync", () =>
            {
                if (!IsValidFileChars(RemoteFileName) || !IsValidFileChars(LocalFileName) || !IsValidPathChars(LocalPath))
                {
                    Logmsg("非法文件名或目录名!");
                    return;
                }
                if (!Directory.Exists(LocalPath))
                {
                    Logmsg("本地文件路径不存在!");
                    return;
                }
                string LocalFullPath = Path.Combine(LocalPath, LocalFileName);
                if (File.Exists(LocalFullPath))
                {
                    Logmsg("当前路径下已经存在同名文件！");
                    return;
                }
                DownloadFileAsync(RemoteFileName, LocalFullPath);
            });
          
        }

        /// <summary>
        /// 从FTP服务器异步下载文件，指定本地完整路径文件名
        /// </summary>
        /// <param name="RemoteFileName">远程文件名</param>
        /// <param name="LocalFullPath">本地完整路径文件名</param>
        public void DownloadFileAsync(string RemoteFileName, string LocalFullPath)
        {
            Trydo("DownloadFileAsync", () =>
            {
                if (!IsValidFileChars(RemoteFileName))
                {
                    Logmsg("非法文件名或目录名!");
                    return;
                }
                if (File.Exists(LocalFullPath))
                {
                    Logmsg("当前路径下已经存在同名文件！");
                    return;
                }
                MyWebClient client = new MyWebClient();

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.Credentials = new NetworkCredential(this.passport.UserName, this.passport.Password);
                if (this.passport.Proxy != null)
                {
                    client.Proxy = this.passport.Proxy;
                }
                client.DownloadFileAsync(new Uri(this.passport.FtpUri.ToString() + RemoteFileName), LocalFullPath);
            });
        }

        /// <summary>
        /// 异步下载文件完成之后触发的事件
        /// </summary>
        /// <param name="sender">下载对象</param>
        /// <param name="e">数据信息对象</param>
        void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (DownloadDataCompleted != null)
            {
                DownloadDataCompleted(sender, e);
            }
        }

        /// <summary>
        /// 异步下载进度发生改变触发的事件
        /// </summary>
        /// <param name="sender">下载对象</param>
        /// <param name="e">进度信息对象</param>
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (DownloadProgressChanged != null)
            {
                DownloadProgressChanged(sender, e);
            }
        }
        #endregion

        #region 下载文件
        /// <summary>
        /// 从FTP服务器下载文件，指定本地路径和本地文件名
        /// </summary>
        /// <param name="remoteFileName">远程文件名</param>
        /// <param name="localPath">本地路径</param>
        /// <param name="localFilePath">保存文件的本地路径,后面带有"\"</param>
        /// <param name="localFileName">保存本地的文件名</param>
        public bool DownloadFile(string remoteFileName, string localPath, string localFileName)
        {
            bool issuccess = false;
            byte[] bt = null;
            Trydo("DownloadFile", () => {
                if (!IsValidFileChars(remoteFileName) || !IsValidFileChars(localFileName) || !IsValidPathChars(localPath))
                {
                    Logmsg("非法文件名或目录名!");
                    return;
                }
                if (!Directory.Exists(localPath))
                {
                    Logmsg("本地文件路径不存在!");
                    return;
                }

                string LocalFullPath = Path.Combine(localPath, localFileName);
                if (File.Exists(LocalFullPath))
                {
                    Logmsg("当前路径下已经存在同名文件！");
                    return;
                }
                bt = DownloadFile(remoteFileName);
                if (bt != null)
                {
                    FileStream stream = new FileStream(LocalFullPath, FileMode.Create);
                    stream.Write(bt, 0, bt.Length);
                    stream.Flush();
                    stream.Close();
                    issuccess = true;
                }
                else
                {
                    issuccess = false;
                }
            });
            return issuccess;
        }

        /// <summary>
        /// 从FTP服务器下载文件，返回文件二进制数据
        /// </summary>
        /// <param name="RemoteFileName">远程文件名</param>
        private byte[] DownloadFile(string remoteFileName)
        {
            byte[] filebytes = null;
            Trydo("DownloadFile", () =>
            {
                if (!IsValidFileChars(remoteFileName))
                {
                    Logmsg("非法文件名或目录名!");
                    return;
                }
                string uri = CreateDownOrUpFileUri(this.OperateDirectoryPath.DownLoadDirectoryPath,remoteFileName);
                response = GetFtpReponse(new Uri(uri), WebRequestMethods.Ftp.DownloadFile);
                Stream Reader = response.GetResponseStream();

                MemoryStream mem = new MemoryStream(1024 * 500);
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                int TotalByteRead = 0;
                while (true)
                {
                    bytesRead = Reader.Read(buffer, 0, buffer.Length);
                    TotalByteRead += bytesRead;
                    if (bytesRead == 0)
                        break;
                    mem.Write(buffer, 0, bytesRead);
                }
                if (mem.Length > 0)
                {
                    filebytes = mem.ToArray();
                }
            });
            return filebytes;
        }
        #endregion

        #region 异步上传文件
        /// <summary>
        /// 异步上传文件到FTP服务器
        /// </summary>
        /// <param name="LocalFullPath">本地带有完整路径的文件名</param>
        public void UploadFileAsync(string LocalFullPath)
        {
            UploadFileAsync(LocalFullPath, Path.GetFileName(LocalFullPath), false);
        }
        /// <summary>
        /// 异步上传文件到FTP服务器
        /// </summary>
        /// <param name="LocalFullPath">本地带有完整路径的文件</param>
        /// <param name="OverWriteRemoteFile">是否覆盖远程服务器上面同名的文件</param>
        public void UploadFileAsync(string LocalFullPath, bool OverWriteRemoteFile)
        {
            UploadFileAsync(LocalFullPath, Path.GetFileName(LocalFullPath), OverWriteRemoteFile);
        }
        /// <summary>
        /// 异步上传文件到FTP服务器
        /// </summary>
        /// <param name="LocalFullPath">本地带有完整路径的文件</param>
        /// <param name="RemoteFileName">要在FTP服务器上面保存文件名</param>
        public void UploadFileAsync(string LocalFullPath, string RemoteFileName)
        {
            UploadFileAsync(LocalFullPath, RemoteFileName, false);
        }
        /// <summary>
        /// 异步上传文件到FTP服务器
        /// </summary>
        /// <param name="LocalFullPath">本地带有完整路径的文件名</param>
        /// <param name="RemoteFileName">要在FTP服务器上面保存文件名</param>
        /// <param name="OverWriteRemoteFile">是否覆盖远程服务器上面同名的文件</param>
        public void UploadFileAsync(string LocalFullPath, string RemoteFileName, bool OverWriteRemoteFile)
        {
            Trydo("UploadFileAsync", () => {
                if (!IsValidFileChars(RemoteFileName) || !IsValidFileChars(Path.GetFileName(LocalFullPath)) || !IsValidPathChars(Path.GetDirectoryName(LocalFullPath)))
                {
                    Logmsg("非法文件名或目录名!");
                    return;
                }
                if (!OverWriteRemoteFile && FileExist(RemoteFileName))
                {
                    Logmsg("FTP服务上面已经存在同名文件！");
                    return;
                }
                if (File.Exists(LocalFullPath))
                {
                    MyWebClient client = new MyWebClient();

                    client.UploadProgressChanged += new UploadProgressChangedEventHandler(client_UploadProgressChanged);
                    client.UploadFileCompleted += new UploadFileCompletedEventHandler(client_UploadFileCompleted);
                    client.Credentials = new NetworkCredential(this.passport.UserName, this.passport.Password);
                    if (this.passport.Proxy != null)
                    {
                        client.Proxy = this.passport.Proxy;
                    }
                    client.UploadFileAsync(new Uri(this.passport.FtpUri.ToString() + RemoteFileName), LocalFullPath);
                }
                else
                {
                    Logmsg("本地文件不存在!");
                    return;
                }
            });
        }
        /// <summary>
        /// 异步上传文件到FTP服务器
        /// </summary>
        /// <param name="FileBytes">上传的二进制数据</param>
        /// <param name="RemoteFileName">要在FTP服务器上面保存文件名</param>
        public void UploadFileAsync(byte[] FileBytes, string RemoteFileName)
        {
            if (!IsValidFileChars(RemoteFileName))
            {
                Logmsg("非法文件名或目录名!");
                return;
            }
            UploadFileAsync(FileBytes, RemoteFileName, false);
        }
        /// <summary>
        /// 异步上传文件到FTP服务器
        /// </summary>
        /// <param name="FileBytes">文件二进制内容</param>
        /// <param name="RemoteFileName">要在FTP服务器上面保存文件名</param>
        /// <param name="OverWriteRemoteFile">是否覆盖远程服务器上面同名的文件</param>
        public void UploadFileAsync(byte[] FileBytes, string RemoteFileName, bool OverWriteRemoteFile)
        {
            Trydo("UploadFileAsync", () => {
                if (!IsValidFileChars(RemoteFileName))
                {
                    Logmsg("非法文件名！");
                    return;
                }
                if (!OverWriteRemoteFile && FileExist(RemoteFileName))
                {
                    Logmsg("FTP服务上面已经存在同名文件！");
                }
                string TempPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Templates);
                if (!TempPath.EndsWith("\\"))
                {
                    TempPath += "\\";
                }
                string TempFile = TempPath + Path.GetRandomFileName();
                TempFile = Path.ChangeExtension(TempFile, Path.GetExtension(RemoteFileName));
                FileStream Stream = new FileStream(TempFile, FileMode.CreateNew, FileAccess.Write);
                Stream.Write(FileBytes, 0, FileBytes.Length);   //注意，因为Int32的最大限制，最大上传文件只能是大约2G多一点
                Stream.Flush();
                Stream.Close();
                Stream.Dispose();
                _isDeleteTempFile = true;
                _UploadTempFile = TempFile;
                FileBytes = null;
                UploadFileAsync(TempFile, RemoteFileName, OverWriteRemoteFile);
            });
        }

        /// <summary>
        /// 异步上传文件完成之后触发的事件
        /// </summary>
        /// <param name="sender">下载对象</param>
        /// <param name="e">数据信息对象</param>
        void client_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            if (_isDeleteTempFile)
            {
                if (File.Exists(_UploadTempFile))
                {
                    File.SetAttributes(_UploadTempFile, FileAttributes.Normal);
                    File.Delete(_UploadTempFile);
                }
                _isDeleteTempFile = false;
            }
            if (UploadFileCompleted != null)
            {
                UploadFileCompleted(sender, e);
            }
        }

        /// <summary>
        /// 异步上传进度发生改变触发的事件
        /// </summary>
        /// <param name="sender">下载对象</param>
        /// <param name="e">进度信息对象</param>
        void client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if (UploadProgressChanged != null)
            {
                UploadProgressChanged(sender, e);
            }
        }
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件到FTP服务器
        /// </summary>
        /// <param name="fileBytes">文件二进制内容</param>
        /// <param name="remoteFileName">要在FTP服务器上面保存文件名</param>
        /// <param name="overWriteRemoteFile">是否覆盖远程服务器上面同名的文件</param>
        private bool UploadFile(byte[] fileBytes, string remoteFileName, bool overWriteRemoteFile)
        {
            bool isopsuccess = false;
            Trydo("UploadFile", () =>
            {
                if (!IsValidFileChars(remoteFileName))
                {
                    Logmsg("非法文件名！");
                    isopsuccess = false;
                    return;
                }
                if (!overWriteRemoteFile && FileExist(remoteFileName))
                {
                    Logmsg("FTP服务上面已经存在同名文件！");
                    isopsuccess = false;
                    return;
                }
                string uri = CreateDownOrUpFileUri(this.OperateDirectoryPath.UploadDirectoryPath,remoteFileName);
                response = GetFtpReponse(new Uri(uri), WebRequestMethods.Ftp.UploadFile);
                Stream requestStream = request.GetRequestStream();
                MemoryStream mem = new MemoryStream(fileBytes);

                byte[] buffer = new byte[4056];
                int bytesRead = 0;
                int TotalRead = 0;
                while (true)
                {
                    bytesRead = mem.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;
                    TotalRead += bytesRead;
                    requestStream.Write(buffer, 0, bytesRead);
                }
                requestStream.Close();
                response = (FtpWebResponse)request.GetResponse();
                mem.Close();
                mem.Dispose();
                fileBytes = null;
                isopsuccess = true;
            });
            return isopsuccess;
        }
        /// <summary>
        /// 上传文件到FTP服务器,上传前需要先设置上传至指定的文件夹路径信息
        /// 如果没有指定，则上传至根目录中
        /// </summary>
        /// <param name="LocalFileName">本地带有完整路径的文件名</param>
        /// <param name="remoteFileName">要在FTP服务器上面保存文件名</param>
        /// <param name="overWriteRemoteFile">是否覆盖远程服务器上面同名的文件</param>
        public bool UploadFile(string localFileName, string remoteFileName, bool overWriteRemoteFile)
        {
            bool isopsuccess = false;
            Trydo("UploadFile", () => {
                if (!IsValidFileChars(remoteFileName) || !IsValidFileChars(Path.GetFileName(localFileName)) || !IsValidPathChars(Path.GetDirectoryName(localFileName)))
                {
                    Logmsg("非法文件名或目录名!");
                    return;
                }
                if (File.Exists(localFileName))
                {
                    FileStream Stream = new FileStream(localFileName, FileMode.Open, FileAccess.Read);
                    byte[] bt = new byte[Stream.Length];
                    Stream.Read(bt, 0, (Int32)Stream.Length);   //注意，因为Int32的最大限制，最大上传文件只能是大约2G多一点
                    Stream.Close();
                    isopsuccess = UploadFile(bt, remoteFileName, overWriteRemoteFile);
                }
                else
                {
                    Logmsg("本地文件不存在!");
                    return;
                }
            });
            return isopsuccess;
        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 从FTP服务器上面删除一个文件
        /// </summary>
        /// <param name="RemoteFileName">远程文件名,全路径名称</param>
        public bool DeleteFile(string RemoteFileName)
        {
            bool isopsuccess = false;
            Trydo("DeleteFile", () => {
                if (!IsValidFileChars(RemoteFileName))
                {
                    Logmsg("文件名非法！");
                    return;
                }
                response = GetFtpReponse(new Uri(this.passport.FtpUri.ToString() + RemoteFileName), WebRequestMethods.Ftp.DeleteFile);
                isopsuccess = true;
            });
            return isopsuccess;
        }
        #endregion

        #region 重命名文件
        /// <summary>
        /// 更改一个文件的名称或一个目录的名称
        /// </summary>
        /// <param name="RemoteFileName">原始文件或目录名称</param>
        /// <param name="NewFileName">新的文件或目录的名称</param>
        public bool ReName(string RemoteFileName, string NewFileName)
        {
            bool isopsuccess = false;
            Trydo("ReName", () => {
                if (!IsValidFileChars(RemoteFileName) || !IsValidFileChars(NewFileName))
                {
                    Logmsg("文件名非法");
                    return;
                }
                if (RemoteFileName == NewFileName)
                {
                    isopsuccess = true;
                    return;
                }
                if (FileExist(RemoteFileName))
                {
                    request = CreateFtpRequest(new Uri(this.passport.FtpUri.ToString() + RemoteFileName), WebRequestMethods.Ftp.Rename);
                    request.RenameTo = NewFileName;
                    response = (FtpWebResponse)request.GetResponse();

                }
                else
                {
                    Logmsg("文件在服务器上不存在！");
                    return;
                }
                isopsuccess = true;
            });
            return isopsuccess;
        }
        #endregion

        #region 拷贝、移动文件
        /// <summary>
        /// 把当前目录下面的一个文件拷贝到服务器上面另外的目录中，注意，拷贝文件之后，当前工作目录还是文件原来所在的目录
        /// </summary>
        /// <param name="RemoteFile">当前目录下的文件名</param>
        /// <param name="DirectoryName">新目录名称。
        /// 说明：如果新目录是当前目录的子目录，则直接指定子目录。如: SubDirectory1/SubDirectory2 ；
        /// 如果新目录不是当前目录的子目录，则必须从根目录一级一级的指定。如： ./NewDirectory/SubDirectory1/SubDirectory2
        /// </param>
        /// <returns></returns>
        public bool CopyFileToAnotherDirectory(string RemoteFile, string DirectoryName)
        {
            string CurrentWorkDir = this.OperateDirectoryPath.CurrentDirectoryPath;
            bool Success = false;
            Trydo("CopyFileToAnotherDirectory", () =>
            {
                byte[] bt = DownloadFile(RemoteFile);
                GotoDirectory(DirectoryName);
                Success = UploadFile(bt, RemoteFile, false);
                this.OperateDirectoryPath.CurrentDirectoryPath = CurrentWorkDir;
            });
            return Success;
        }
        /// <summary>
        /// 把当前目录下面的一个文件移动到服务器上面另外的目录中，注意，移动文件之后，当前工作目录还是文件原来所在的目录
        /// </summary>
        /// <param name="RemoteFile">当前目录下的文件名</param>
        /// <param name="DirectoryName">新目录名称。
        /// 说明：如果新目录是当前目录的子目录，则直接指定子目录。如: SubDirectory1/SubDirectory2 ；
        /// 如果新目录不是当前目录的子目录，则必须从根目录一级一级的指定。如： ./NewDirectory/SubDirectory1/SubDirectory2
        /// </param>
        /// <returns></returns>
        public bool MoveFileToAnotherDirectory(string RemoteFile, string DirectoryName)
        {
            string CurrentWorkDir = this.OperateDirectoryPath.CurrentDirectoryPath;
            bool Success = false;
            Trydo("MoveFileToAnotherDirectory", () =>
            {
                if (DirectoryName == "")
                {
                    Success = false;
                    return;
                }
                if (!DirectoryName.StartsWith("/"))
                    DirectoryName = "/" + DirectoryName;
                if (!DirectoryName.EndsWith("/"))
                    DirectoryName += "/";
                 Success = ReName(RemoteFile, DirectoryName + RemoteFile);
                 this.OperateDirectoryPath.CurrentDirectoryPath = CurrentWorkDir;
            });
            return Success;
        }
        #endregion

        #region 建立、删除子目录
        /// <summary>
        /// 在FTP服务器上当前工作目录建立一个子目录
        /// </summary>
        /// <param name="DirectoryName">子目录名称</param>
        public bool MakeDirectory(string DirectoryName)
        {
            bool issuccess = false;
            Trydo("MakeDirectory", () =>
            {
                if (!IsValidPathChars(DirectoryName))
                {
                    Logmsg("目录名非法！");
                    return;
                }
                if (DirectoryExist(DirectoryName))
                {
                    Logmsg("服务器上面已经存在同名的文件名或目录名！");
                    return;
                }
                response = GetFtpReponse(new Uri(this.passport.FtpUri.ToString() + DirectoryName), WebRequestMethods.Ftp.MakeDirectory);
                issuccess = true;
            });
            return issuccess;
        }
        /// <summary>
        /// 从当前工作目录中删除一个子目录
        /// </summary>
        /// <param name="DirectoryName">子目录名称</param>
        public bool RemoveDirectory(string DirectoryName)
        {
            bool issucess = false;
            Trydo("RemoveDirectory", () =>
            {
                if (!IsValidPathChars(DirectoryName))
                {
                    Logmsg("目录名非法！");
                    return;
                }
                if (!DirectoryExist(DirectoryName))
                {
                    Logmsg("服务器上面不存在指定的文件名或目录名！");
                    return;
                }
                response = GetFtpReponse(new Uri(this.passport.FtpUri.ToString() + DirectoryName), WebRequestMethods.Ftp.RemoveDirectory);
                issucess = true;
            });
            return issucess;
        }
        #endregion

        #region 目录或文件存在的判断
        /// <summary>
        /// 判断一个远程文件是否存在服务器当前目录下面
        /// </summary>
        /// <param name="RemoteFileName">远程文件名</param>
        public bool FileExist(string RemoteFileName)
        {
            bool issuccess = false;
            Trydo("FileExist", () => {
                if (!IsValidFileChars(RemoteFileName))
                {
                    Logmsg("文件名非法");
                    return;
                }
                FileStruct[] listFile = ListFiles();
                foreach (FileStruct file in listFile)
                {
                    if (file.Name == RemoteFileName)
                    {
                        issuccess= true;
                    }
                }
                issuccess = false;
            });
            return issuccess;
        }
        /// <summary>
        /// 判断当前目录下指定的子目录是否存在
        /// </summary>
        /// <param name="RemoteDirectoryName">指定的目录名</param>
        public bool DirectoryExist(string RemoteDirectoryName)
        {
            bool issucess = false;
            Trydo("DirectoryExist", () =>
            {
                if (!IsValidPathChars(RemoteDirectoryName))
                {
                    Logmsg("目录名非法！");
                    return;
                }
                FileStruct[] listDir = ListDirectories();
                foreach (FileStruct dir in listDir)
                {
                    if (dir.Name == RemoteDirectoryName)
                    {
                        issucess = true;
                    }
                }
                issucess = false;
            });
            return issucess;
        }
        #endregion

        #region 列出目录文件信息
        /// <summary>
        /// 列出FTP服务器上面当前目录的所有文件和目录
        /// </summary>
        public FileStruct[] ListFilesAndDirectories()
        {
            FileStruct[] list = null;
            Trydo("ListFilesAndDirectories", () =>
            {
                response = GetFtpReponse(this.passport.FtpUri, WebRequestMethods.Ftp.ListDirectoryDetails);
                StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string Datastring = stream.ReadToEnd();
                list = GetList(Datastring);
            });
            return list;
        }
        /// <summary>
        /// 列出FTP服务器上面当前目录的所有文件
        /// </summary>
        public FileStruct[] ListFiles()
        {
            List<FileStruct> listFile = new List<FileStruct>();
            Trydo("ListFiles", () => {
                FileStruct[] listAll = ListFilesAndDirectories();

                foreach (FileStruct file in listAll)
                {
                    if (!file.IsDirectory)
                    {
                        listFile.Add(file);
                    }
                }
            });
            return listFile.ToArray();
        }
        /// <summary>
        /// 列出FTP服务器上面当前目录的所有的目录
        /// </summary>
        public FileStruct[] ListDirectories()
        {
            List<FileStruct> listDirectory = new List<FileStruct>();
            Trydo("ListDirectories", () =>
            {
                FileStruct[] listAll = ListFilesAndDirectories();
                foreach (FileStruct file in listAll)
                {
                    if (file.IsDirectory)
                    {
                        listDirectory.Add(file);
                    }
                }
            });
            return listDirectory.ToArray();
        }
        /// <summary>
        /// 获得文件和目录列表
        /// </summary>
        /// <param name="datastring">FTP返回的列表字符信息</param>
        private FileStruct[] GetList(string datastring)
        {
            List<FileStruct> myListArray = new List<FileStruct>();
            Trydo("GetList", () =>
            {
                string[] dataRecords = datastring.Split('\n');
                FileListStyle _directoryListStyle = GuessFileListStyle(dataRecords);
                foreach (string s in dataRecords)
                {
                    if (_directoryListStyle != FileListStyle.Unknown && s != "")
                    {
                        FileStruct f = new FileStruct();
                        f.Name = "..";
                        switch (_directoryListStyle)
                        {
                            case FileListStyle.UnixStyle:
                                f = ParseFileStructFromUnixStyleRecord(s);
                                break;
                            case FileListStyle.WindowsStyle:
                                f = ParseFileStructFromWindowsStyleRecord(s);
                                break;
                        }
                        if (!(f.Name == "." || f.Name == ".."))
                        {
                            myListArray.Add(f);
                        }
                    }
                }
            });
            return myListArray.ToArray();
        }
        /// <summary>
        /// 从Windows格式中返回文件信息
        /// </summary>
        /// <param name="Record">文件信息</param>
        private FileStruct ParseFileStructFromWindowsStyleRecord(string Record)
        {
            FileStruct f = new FileStruct();
            Trydo("", () =>
            {
                string processstr = Record.Trim();
                string dateStr = processstr.Substring(0, 8);
                processstr = (processstr.Substring(8, processstr.Length - 8)).Trim();
                string timeStr = processstr.Substring(0, 7);
                processstr = (processstr.Substring(7, processstr.Length - 7)).Trim();
                DateTimeFormatInfo myDTFI = new CultureInfo("en-US", false).DateTimeFormat;
                myDTFI.ShortTimePattern = "t";
                f.CreateTime = DateTime.Parse(dateStr + " " + timeStr, myDTFI);
                if (processstr.Substring(0, 5) == "<DIR>")
                {
                    f.IsDirectory = true;
                    processstr = (processstr.Substring(5, processstr.Length - 5)).Trim();
                }
                else
                {
                    string[] strs = processstr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);   // true);
                    processstr = strs[1];
                    f.IsDirectory = false;
                }
                f.Name = processstr;
            });
            return f;
        }
        /// <summary>
        /// 从Unix格式中返回文件信息
        /// </summary>
        /// <param name="Record">文件信息</param>
        private FileStruct ParseFileStructFromUnixStyleRecord(string Record)
        {
            FileStruct f = new FileStruct();
            Trydo("ParseFileStructFromUnixStyleRecord", () =>
            {
                string processstr = Record.Trim();
                if (processstr.Length < 10)
                {
                    f.Name = ".";
                    return;
                }
                f.Flags = processstr.Substring(0, 10);
                f.IsDirectory = (f.Flags[0] == 'd');
                processstr = (processstr.Substring(11)).Trim();
                _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);   //跳过一部分
                f.Owner = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
                f.Group = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
                _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);   //跳过一部分
                string yearOrTime = processstr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2];
                if (yearOrTime.IndexOf(":") >= 0)  //time
                {
                    processstr = processstr.Replace(yearOrTime, DateTime.Now.Year.ToString());
                }
                f.CreateTime = DateTime.Parse(_cutSubstringFromStringWithTrim(ref processstr, ' ', 8));
                f.Name = processstr;   //最后就是名称
            });
            return f;
        }
        /// <summary>
        /// 按照一定的规则进行字符串截取
        /// </summary>
        /// <param name="s">截取的字符串</param>
        /// <param name="c">查找的字符</param>
        /// <param name="startIndex">查找的位置</param>
        private string _cutSubstringFromStringWithTrim(ref string s, char c, int startIndex)
        {
            int pos1 = s.IndexOf(c, startIndex);
            string retString = s.Substring(0, pos1);
            s = (s.Substring(pos1)).Trim();
            return retString;
        }
        /// <summary>
        /// 判断文件列表的方式Window方式还是Unix方式
        /// </summary>
        /// <param name="recordList">文件信息列表</param>
        private FileListStyle GuessFileListStyle(string[] recordList)
        {
            foreach (string s in recordList)
            {
                if (s.Length > 10
                 && Regex.IsMatch(s.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
                {
                    return FileListStyle.UnixStyle;
                }
                else if (s.Length > 8
                 && Regex.IsMatch(s.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
                {
                    return FileListStyle.WindowsStyle;
                }
            }
            return FileListStyle.Unknown;
        }
        #endregion

        #region 文件、目录名称有效性判断
        /// <summary>
        /// 判断文件名中字符是否合法
        /// </summary>
        /// <param name="FileName">文件名称</param>
        private bool IsValidFileChars(string FileName)
        {
            char[] invalidFileChars = Path.GetInvalidFileNameChars();
            char[] NameChar = FileName.ToCharArray();
            foreach (char C in NameChar)
            {
                if (Array.BinarySearch(invalidFileChars, C) >= 0)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 判断目录名中字符是否合法
        /// </summary>
        /// <param name="DirectoryName">目录名称</param>
        private bool IsValidPathChars(string DirectoryName)
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            char[] DirChar = DirectoryName.ToCharArray();
            foreach (char C in DirChar)
            {
                if (Array.BinarySearch(invalidPathChars, C) >= 0)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 目录切换操作
        /// <summary>
        /// 进入一个目录
        /// </summary>
        /// <param name="DirectoryName">
        /// 新目录的名字。 
        /// 说明：如果新目录是当前目录的子目录，则直接指定子目录。如: SubDirectory1/SubDirectory2 ； 
        /// 如果新目录不是当前目录的子目录，则必须从根目录一级一级的指定。如： ./NewDirectory/SubDirectory1/SubDirectory2
        /// </param>
        public bool GotoDirectory(string DirectoryName)
        {
            string CurrentWorkPath = this.OperateDirectoryPath.CurrentDirectoryPath;
            bool issuccess = false;
            Trydo("", () => {
                DirectoryName = DirectoryName.Replace("\\", "/");
                string[] DirectoryNames = DirectoryName.Split(new char[] { '/' });
                if (DirectoryNames[0] == ".")
                {
                    this.OperateDirectoryPath.CurrentDirectoryPath = "/";
                    if (DirectoryNames.Length == 1)
                    {
                        issuccess = true;
                        return;
                    }
                    Array.Clear(DirectoryNames, 0, 1);
                }
                bool Success = false;
                foreach (string dir in DirectoryNames)
                {
                    if (dir != null)
                    {
                        Success = EnterOneSubDirectory(dir);
                        if (!Success)
                        {
                            this.OperateDirectoryPath.CurrentDirectoryPath = CurrentWorkPath;
                            issuccess = false;
                            return;
                        }
                    }
                }
            });
            return issuccess;
        }
        /// <summary>
        /// 从当前工作目录进入一个子目录
        /// </summary>
        /// <param name="DirectoryName">子目录名称</param>
        private bool EnterOneSubDirectory(string DirectoryName)
        {
            bool issuccess = false;
            Trydo("EnterOneSubDirectory", () =>
            {
                if (DirectoryName.IndexOf("/") >= 0 || !IsValidPathChars(DirectoryName))
                {
                    Logmsg("目录名非法!");
                    return;
                }
                if (DirectoryName.Length > 0 && DirectoryExist(DirectoryName))
                {
                    if (!DirectoryName.EndsWith("/"))
                    {
                        DirectoryName += "/";
                    }
                    OperateDirectoryPath.CurrentDirectoryPath += DirectoryName;
                    issuccess = true;
                    return;
                }
                else
                {
                    issuccess = false;
                    return;
                }
            });
            return issuccess;
        }
        /// <summary>
        /// 从当前工作目录往上一级目录
        /// </summary>
        public bool ComeoutDirectory()
        {
           return Trydo("ComeoutDirectory", () =>
            {
                if (OperateDirectoryPath.CurrentDirectoryPath == "/")
                {
                    //ErrorMsg = "当前目录已经是根目录！";
                    Logmsg("当前目录已经是根目录！");
                    return false;
                }
                char[] sp = new char[1] { '/' };
                string[] strDir = OperateDirectoryPath.CurrentDirectoryPath.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                if (strDir.Length == 1)
                {
                    OperateDirectoryPath.CurrentDirectoryPath = "/";
                }
                else
                {
                    OperateDirectoryPath.CurrentDirectoryPath = String.Join("/", strDir, 0, strDir.Length - 1);
                }
                return true;
            });
        }
        #endregion

        #region 重载WebClient，支持FTP进度
        internal class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                FtpWebRequest req = (FtpWebRequest)base.GetWebRequest(address);
                req.UsePassive = false;
                return req;
            }
        }
        #endregion
    }

    #region 文件信息结构
    /// <summary>
    /// 文件信息结构
    /// </summary>
    public struct FileStruct
    {
        public string Flags;
        public string Owner;
        public string Group;
        public bool IsDirectory;
        public DateTime CreateTime;
        public string Name;
    }
    /// <summary>
    /// 文件列表类型
    /// </summary>
    public enum FileListStyle
    {
        UnixStyle,
        WindowsStyle,
        Unknown
    }
    #endregion
}
