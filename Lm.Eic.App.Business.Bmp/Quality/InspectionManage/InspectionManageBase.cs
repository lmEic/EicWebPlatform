﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    public class InspectionDateGatherManageBase
    {
        /// <summary>
        /// 得到已经完成的数量
        /// </summary>
        /// <param name="inspectionItemResult">完成状态</param>
        /// <param name="InspectionItemDatas">测试的数据</param>
        /// <param name="needFinishDataNumber">需完成的数量</param>
        /// <returns></returns>
        public int DoHaveFinishDataNumber(string inspectionItemResult, string InspectionItemDatas, int needFinishDataNumber)
        {
            return (inspectionItemResult == "OK") ? needFinishDataNumber : GetHaveFinishDataNumber(InspectionItemDatas);
        }
        /// <summary>
        /// 分解录入数据得到个数
        /// </summary>
        /// <param name="inspectionDatas"></param>
        /// <returns></returns>
        public int GetHaveFinishDataNumber(string inspectionDatas)
        {
            if (inspectionDatas == null) return 0;
            if ((!inspectionDatas.Contains(",")) || inspectionDatas == string.Empty) return 0;
            string[] mm = inspectionDatas.Split(',');
            return mm.Count();
        }
        public List<string> GetHaveFinishDatas(string inspectionDatas)
        {
            if (inspectionDatas == null || inspectionDatas == string.Empty) return new List<string>();
            if ((!inspectionDatas.Contains(","))) return new List<string>() { inspectionDatas };
            string[] mm = inspectionDatas.Split(',');
            return mm.ToList();
        }
        public List<string> GetHaveItemDetialDatas(string inspectionDatas)
        {
            if (inspectionDatas == null || inspectionDatas == string.Empty) return new List<string>();
            if ((!inspectionDatas.Contains("&"))) return new List<string>() { inspectionDatas };
            string[] mm = inspectionDatas.Split('&');
            return mm.ToList();
        }
        /// <summary>
        /// 得到抽样物料信息
        /// </summary>
        /// <param name="orderId">ERP单号</param>
        /// <returns></returns>
        public List<MaterialModel> GetPuroductSupplierInfo(string orderId)
        {
            return QualityDBManager.OrderIdInpectionDb.FindMaterialBy(orderId);
        }
        /// <summary>
        /// 由检验项目得到检验方式模块
        /// </summary>
        /// <param name="iqcInspectionItemConfig"></param>
        /// <param name="inMaterialCount"></param>
        /// <returns></returns>
        public InspectionModeConfigModel GetInspectionModeConfigDataBy(string inspectionLevel, string inspectionAQL, double inMaterialCount, string inspectionMode = "正常")
        {
            if (inspectionLevel == null) return new InspectionModeConfigModel();
            var maxs = new List<Int64>();
            var mins = new List<Int64>();
            double maxNumber = 0;
            double minNumber = 0;

            var models = InspectionManagerCrudFactory.InspectionModeConfigCrud.GetInspectionStartEndNumberBy(
                inspectionMode,
                inspectionLevel,
                inspectionAQL).ToList();
            if (models == null || models.Count <= 0) return new InspectionModeConfigModel();
            models.ForEach(e => { maxs.Add(e.EndNumber); mins.Add(e.StartNumber); });
            if (maxs.Count > 0)
                maxNumber = GetMaxNumber(maxs, inMaterialCount);
            if (mins.Count > 0)
                minNumber = GetMinNumber(mins, inMaterialCount);
            var model = models.FirstOrDefault(e => e.StartNumber == minNumber && e.EndNumber == maxNumber);
            if (model != null)
            {
                model.InspectionMode = inspectionMode;
                //如果为负数 则全检
                model.InspectionCount = model.InspectionCount < 0 ? Convert.ToInt32(inMaterialCount) : model.InspectionCount;
                return model;
            }
            else return null;
            // InspectionCount, AcceptCount, RefuseCount,
        }
        private Int64 GetMaxNumber(List<Int64> maxNumbers, double number)
        {
            List<Int64> IntMaxNumbers = new List<Int64>();
            foreach (var max in maxNumbers)
            {
                if (max != -1)
                {
                    if (max >= number)
                    {
                        IntMaxNumbers.Add(max);
                    }
                }
            }
            if (IntMaxNumbers.Count > 0)
            { return IntMaxNumbers.Min(); }
            else return -1;
        }
        private Int64 GetMinNumber(List<Int64> minNumbers, double mumber)
        {
            List<Int64> IntMinNumbers = new List<Int64>();
            foreach (var min in minNumbers)
            {
                if (min != -1)
                {

                    if (min <= mumber)
                    {
                        IntMinNumbers.Add(min);
                    }
                }
                else return -1;
            }
            return IntMinNumbers.Max();
        }
    }

    public static class InspectionConstant
    {

        public static class InspectionMode
        {
            public const string Normal = "正常";
            public const string Stricter = "加严";
            public const string Broaden = "放宽";
        }
        public static class InspectionResult
        {
            public const string Pass = "OK";
            public const string NoPass = "NG";
            public const string unfinished = "未完成";
        }
        public static class InspectionStatus
        {
            public const string WaitCheck = "待审核";
            public const string HaveCheck = "已审核";
            public const string unfinished = "未完成";
            public const string WaitInspect = "待检验";
            public const string NotInspect = "未抽验";
            public const string AllState = "全部";
        }
        public static class InspectionItemStatus
        {
            public const string Done = "Done";
            public const string Doing = "doing";
        }
    }
}
