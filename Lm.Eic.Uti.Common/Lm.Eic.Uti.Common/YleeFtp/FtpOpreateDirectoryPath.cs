namespace Lm.Eic.Uti.Common.YleeFtp
{
    /// <summary>
    /// Ftp操作文件夹路径信息
    /// </summary>
    public class FtpOpreateDirectoryPath
    {
        /// <summary>
        /// 当前文件夹路径
        /// </summary>
        public string CurrentDirectoryPath { get; set; }

        private string _UploadDirectoryPath;

        /// <summary>
        /// 上传至指定的文件夹路径
        /// </summary>
        public string UploadDirectoryPath
        {
            get { return CurrentDirectoryPath + _UploadDirectoryPath; }
            set { _UploadDirectoryPath = value; }
        }

        private string _DownLoadDirectoryPath;

        /// <summary>
        /// 从远程指定的文件夹路径中下载文件
        /// </summary>
        public string DownLoadDirectoryPath
        {
            get { return CurrentDirectoryPath + _DownLoadDirectoryPath; }
            set { _DownLoadDirectoryPath = value; }
        }

        public FtpOpreateDirectoryPath()
        {
        }
    }
}