using System;
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
        /// 
        /// </summary>
        /// <param name="MarterialId"></param>
        /// <returns></returns>

        private List<InspectionFqcMasterModel> GetFqcMasterModeListlBy(string MarterialId)
        {
            return InspectionManagerCrudFactory.FqcMasterCrud.GetFqcInspectionMasterListBy(MarterialId);
        }


        /// <summary>
        /// 判断是否按提正常还放宽加严
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public string GetJudgeInspectionMode(string inspectionClass, string currentStatus, int nGnumber)
        {
            ///3，比较 对比
            ///4，返回一个 转换的状态
            ///1,通过料号 和 抽检验项目  得到当前的最后一次抽检的状态
            string retrunstirng = "正常";
            ///2，通当前状态 得到抽样规则 抽样批量  拒受数
            var modeSwithParameterList = InspectionManagerCrudFactory.InspectionModeSwithConfigCrud.GetInspectionModeSwithConfiglistBy(inspectionClass, currentStatus);
            if (modeSwithParameterList == null || modeSwithParameterList.Count <= 0) return retrunstirng;
            int sampleNumberVauleMin = modeSwithParameterList.FindAll(e => e.SwitchProperty == "SampleNumber").Select(e => e.SwitchVaule).Min();
            int AcceptNumberVauleMax = modeSwithParameterList.FindAll(e => e.SwitchProperty == "AcceptNumber").Select(e => e.SwitchVaule).Max();
            int sampleNumberVauleMax = modeSwithParameterList.FindAll(e => e.SwitchProperty == "SampleNumber").Select(e => e.SwitchVaule).Max();
            int AcceptNumberVauleMin = modeSwithParameterList.FindAll(e => e.SwitchProperty == "AcceptNumber").Select(e => e.SwitchVaule).Min();
            switch (currentStatus)
            {
                case "加严":
                    retrunstirng = (nGnumber >= AcceptNumberVauleMin) ? "正常" : currentStatus;
                    break;
                case "放宽":
                    retrunstirng = (nGnumber <= AcceptNumberVauleMin) ? "正常" : currentStatus;
                    break;
                case "正常":
                    if (nGnumber <= AcceptNumberVauleMin) retrunstirng = "放宽";
                    if (nGnumber >= AcceptNumberVauleMax) retrunstirng = "加严";
                    else retrunstirng = "正常";
                    break;
                default:
                    retrunstirng = "正常";
                    break;
            }
            return retrunstirng;
        }


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

    public interface IlistInspectionItemConfigDatas
    {
        List<InspectionModeConfigModel> Condition(MaterialModel Materialinfo);
        void AddItemConfigDatas();
        void MoveItemConfigDatas();
    }


    public abstract class ItemConfigDatas
    {

    }
    public class ListInspectionItemConfigDatas : IlistInspectionItemConfigDatas
    {
        public void AddItemConfigDatas()
        {
            throw new NotImplementedException();
        }

        public List<InspectionModeConfigModel> Condition(MaterialModel Materialinfo)
        {
            throw new NotImplementedException();
        }

        public void MoveItemConfigDatas()
        {
            throw new NotImplementedException();
        }
    }
}
