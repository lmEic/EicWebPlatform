using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Uti.Common.YleeExcelHanlder.ExcelParse
{
    // <summary>
    /// EXCEL文件上传检查返回数据
    /// </summary>
    public class UploadExcelFileResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 附带消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 文件基本信息
        /// </summary>
        public FileMessage FileMessage { get; set; }

        /// <summary>
        /// 解析失败后错误位置定位信息
        /// </summary>
        public List<ExcelFileErrorPosition> ExcelFileErrorPositions { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class FileMessage
    {
        /// <summary>
        /// 上传文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string Type { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ExcelFileErrorPosition
    {
        /// <summary>
        /// 错误行
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// 错误列集
        /// </summary>
        public List<int> CellIndex { get; set; }

        /// <summary>
        /// 错误列具体错误信息
        /// </summary>
        public List<string> ErrorMessage { get; set; }

        /// <summary>
        /// 错误行数据
        /// </summary>
        public List<string> RowContent { get; set; }
    }
}

