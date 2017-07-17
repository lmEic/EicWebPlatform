using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Uti.Common.YleeExcelHanlder.ExcelParse
{
    /// <summary>
    /// 模板规则类
    /// </summary>
    public class Regular
    {
        /// <summary>
        /// 表头文本
        /// </summary>
        public string HeaderText { set; get; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { set; get; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { set; get; }
        /// <summary>
        //
        /// </summary>
        public Dictionary<string, int> HeaderRegular
        { get; set; }
    }

    /// <summary>
    ///要插入的到新那建EXCEL的 固定格式
    /// </summary>
    public class FixInsertRegular
    {
        // HeaderText PropertyName DataType FillText RowIndexStart RowIndexEnd 
        //ColumnIndexStart RowIndexEnd ColumnIndexStart  Ismerge 


        #region  填充内容  固定输入的内空
        /// <summary>
        /// 表头文本
        /// </summary>
        public string HeaderText { set; get; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { set; get; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { set; get; }
        /// <summary>
        /// 填充内容 value
        /// </summary>
        public string FillText { set; get; }
        /// <summary>
        /// 行开始
        /// </summary>
        public int RowIndexStart { set; get; }
        /// <summary>
        /// 行结束
        /// </summary>
        public int RowIndexEnd { set; get; }
        /// <summary>
        /// 列开始
        /// </summary>
        public int ColumnIndexStart { set; get; }
        /// <summary>
        /// 列结束
        /// </summary>
        public int ColumnIndexEnd { set; get; }
        /// <summary>
        /// 是否合并
        /// </summary>
        public bool Ismerge { set; get; }


        /// <summary>
        /// 字体高度
        /// </summary>
        public int fontHeight { set; get; }
        /// <summary>
        /// 字体
        /// </summary>
        public string FontName { set; get; }
        /// <summary>
        /// 字体色
        /// </summary>
        public short Color { set; get; }
        /// <summary>
        /// 垂直对齐方式
        /// </summary>
        public int VerticalAlignment { set; get; }
        /// <summary>
        /// 水平对齐式
        /// </summary>
        public int Alignment { set; get; }
        #endregion

    }
}
