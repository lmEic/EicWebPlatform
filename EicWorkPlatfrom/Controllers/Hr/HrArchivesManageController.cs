using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Hr
{
    public class HrArchivesManageController : EicBaseController
    {
        //
        // GET: /HrArchivesManage/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HrEmployeeDataInput()
        {
            return View();
        }

        public ActionResult HrPostChange()
        {
            return View();
        }

        public ActionResult HrDepartmentChange()
        {
            return View();
        }

        public ActionResult HrStudyManage()
        {
            return View();
        }

        public ActionResult HrTelManage()
        {
            return View();
        }

        /// <summary>
        /// 离职管理
        /// </summary>
        /// <returns></returns>
        public ActionResult HrLeaveOffManage()
        {
            return View();
        }
        /// <summary>
        /// 办理离职数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]       
        [NoAuthenCheck]
        public JsonResult StoreLeaveOffData(ArLeaveOfficeModel leaveEntity)
        {
            var opResult = ArchiveService.ArchivesManager.LeaveOffManager.StoreLeaveOffInfo(leaveEntity);
            return Json(opResult);
        }

        [NoAuthenCheck]
        public JsonResult GetIdentityInfoBy(string lastSixIdWord)
        {
            List<IdentityViewModel> datas = null;
            var identityDatas = ArchiveService.ArchivesManager.IdentityManager.LoadBy(lastSixIdWord);
            if (identityDatas != null && identityDatas.Count > 0)
            {
                datas = new List<IdentityViewModel>();
                IdentityViewModel vm = null;
                identityDatas.ForEach(m =>
                {
                    if (m.PersonalPicture != null)
                        //将个人的图片信息转换为Base64字符串编码保存到此属性中
                        m.NewAddress = Convert.ToBase64String(m.PersonalPicture);
                    vm = new IdentityViewModel() { Identity = m, IsExpire = ArchiveService.ArchivesManager.IdentityManager.IdentityLimitDateIsExpired(m),Name=m.Name };
                    datas.Add(vm);
                });
            }
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        [NoAuthenCheck]
        public JsonResult GetArchiveConfigDatas()
        {
            var archiveConfigDatas = ArchiveService.ArchivesManager.ArchiveConfigDatas;

            return Json(archiveConfigDatas, JsonRequestBehavior.AllowGet);
        }

        [NoAuthenCheck]
        public JsonResult GetWorkerIdBy(string workerIdNumType)
        {
            string workerId = ArchiveService.ArchivesManager.CreateWorkerId(workerIdNumType);
            return Json(workerId, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取该工号列表的所有人员信息
        /// mode:0为部门或岗位信息
        /// 1:为学习信息;
        /// 2：为联系方式信息
        /// </summary>
        /// <param name="workerIdList"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult FindEmployeeByWorkerIds(List<string> workerIdList, int mode)
        {
            object datas = null;
            if (mode == 0)
            {
                datas = ArchiveService.ArchivesManager.FindEmployeeBy(workerIdList);
            }
            else if (mode == 1)
            {
                datas = ArchiveService.ArchivesManager.StudyManager.FindBy(workerIdList);
            }
            else if (mode == 2)
            {
                datas = ArchiveService.ArchivesManager.TelManager.FindBy(workerIdList);
            }
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 输入员工档案信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InputWorkerArchive(ArchivesEmployeeIdentityDto employee, ArchivesEmployeeIdentityDto oldEmployeeIdentity, string opSign)
        {
            employee.OpPerson = OnLineUser.UserName;
            var result = ArchiveService.ArchivesManager.Store(employee, oldEmployeeIdentity, opSign);
            return Json(result);
        }
        /// <summary>
        /// 获取档案数据
        /// </summary>
        /// <param name="searchMode"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetWorkerArchives(DateTime startRegistedDate, DateTime endRegistedDate, int searchMode)
        {
            var data = ArchiveService.ArchivesManager.FindWorkerArchivesInfoBy(new QueryWorkerArchivesDto
            {
                RegistedDateStart = startRegistedDate,
                RegistedDateEnd = endRegistedDate,
                SearchMode = searchMode
            }); //待填写
            TempData["archiveDatas"] = data;
            return DateJsonResult(data);
        }
        public ContentResult GetWorkerArchivesby(string query, int searchMode)
        {
            var data = ArchiveService.ArchivesManager.FindWorkerArchivesInfoBy(new QueryWorkerArchivesDto
            {
               
                SearchMode = searchMode
            }); //待填写
            TempData["archiveDatas"] = data;
            return DateJsonResult(data);
        }

        [NoAuthenCheck]
        public FileResult BuildWorkerArchivesList()
        {
            var datas = TempData["archiveDatas"] as List<ArchivesEmployeeIdentityModel>;
            var ms = ArchiveService.ArchivesManager.BuildWorkerArchivesInfoList(datas);
            return this.ExportToExcel(ms, "员工档案总表", "员工档案总表");
        }
        /// <summary>
        /// 变更部门信息
        /// </summary>
        /// <param name="changeDepartments"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeDepartment(List<ArDepartmentChangeLibModel> changeDepartments)
        {
            if (changeDepartments != null && changeDepartments.Count > 0)
            {
                changeDepartments.ForEach(d =>
                {
                    d.AssignDate = DateTime.Now.ToDate();
                    d.OpPerson = this.OnLineUser.UserName;
                });
            }
            var result = ArchiveService.ArchivesManager.ChangeDepartment(changeDepartments);
            return Json(result);
        }

        /// <summary>
        /// 变更岗位信息
        /// </summary>
        /// <param name="changePosts"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangePost(List<ArPostChangeLibModel> changePosts)
        {
            if (changePosts != null && changePosts.Count > 0)
            {
                changePosts.ForEach(d =>
                {
                    d.AssignDate = DateTime.Now.ToDate();
                    d.OpPerson = this.OnLineUser.UserName;
                });
            }
            var result = ArchiveService.ArchivesManager.ChangePost(changePosts);
            return Json(result);
        }

        /// <summary>
        /// 变更学习信息
        /// </summary>
        /// <param name="studyInfos"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeStudy(List<ArStudyModel> studyInfos)
        {
            if (studyInfos != null && studyInfos.Count > 0)
            {
                studyInfos.ForEach(d =>
                {
                    d.OpDate = DateTime.Now.ToDate();
                    d.OpPerson = this.OnLineUser.UserName;
                });
            }
            var result = ArchiveService.ArchivesManager.ChangeStudy(studyInfos);
            return Json(result);
        }

        /// <summary>
        /// 变更联系方式信息
        /// </summary>
        /// <param name="tels"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeTelInfo(List<ArTelModel> tels)
        {
            if (tels != null && tels.Count > 0)
            {
                tels.ForEach(d =>
                {
                    d.OpDate = DateTime.Now.ToDate();
                    d.OpPerson = this.OnLineUser.UserName;
                });
            }
            var result = ArchiveService.ArchivesManager.ChangeTel(tels);
            return Json(result);
        }

        /// <summary>
        /// 打印厂牌
        /// </summary>
        /// <returns></returns>
        public ActionResult HrPrintCard()
        {
            return View();
        }

        [NoAuthenCheck]
        public ActionResult HrPrintWorkerCard()
        {
            return View();
        }

        [NoAuthenCheck]
        public JsonResult GetWorkersInfo(QueryWorkersDto dto, int searchMode)
        {
            var datas = ArchiveService.ArchivesManager.FindWorkers(dto, searchMode);
            RebuildDepartmentContent(datas);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        private void RebuildDepartmentContent(List<ArWorkerInfo> datas)
        {
            if (datas != null && datas.Count > 0)
            {
                datas.ForEach(e =>
                {
                    e.Department = ArchiveService.ArchivesManager.DepartmentMananger.GetDepartmentText(e.Department);
                    
                });
            }
        }

        [NoAuthenCheck]
        public JsonResult GetDepartments()
        {
            var datas = ArchiveService.ArchivesManager.DepartmentMananger.Departments;
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        [NoAuthenCheck]
        public JsonResult GetWorkersBy(string workerIdOrName)
        {
            var datas = ArchiveService.ArchivesManager.FindWorkers(new QueryWorkersDto() { WorkerId = workerIdOrName }, 2);
            RebuildDepartmentContent(datas);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        [NoAuthenCheck]
        public JsonResult GetWorkerCardImages()
        {
            List<string> imgList = new List<string>();
            Bitmap bmp = new Bitmap(100, 35);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            g.FillRectangle(Brushes.Red, 2, 2, 65, 31);
            g.DrawString("学习MVC", new Font("黑体", 15f), Brushes.Yellow, new PointF(5f, 5f));
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var imgBytes = bmp.toByte();
            g.Dispose();
            bmp.Dispose();

            var data = GetBase64Url(imgBytes);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 变更工号
        /// </summary>
        /// <returns></returns>
        public ActionResult HrChangeWorkerId()
        {
            return View();
        }
        /// <summary>
        /// 变更工号办理
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="newWorkerId"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult ChangeWorkerId(string workerId, string newWorkerId)
        {
            var opResult = ArchiveService.ArchivesManager.StoreWorkerIdChangeInfo(new WorkerChangedModel() { OldWorkerId=workerId, NewWorkerId=newWorkerId ,OpSign="add"});
            return Json(opResult);
        }
    }

    public class IdentityViewModel
    {
        public ArchivesIdentityModel Identity { get; set; }

        /// <summary>
        /// 身份证是否过期
        /// </summary>
        public bool IsExpire { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 生成厂牌模板
    /// </summary>
    public class CreateCardImage
    {
        #region Private Member
        private const int startX = 30;
        private const int startY = 15;
        private const int decImageWidth = 340;
        private const int decImageHeight = 220;


        private const int midSpliter = 30;
        //厂牌上的数据信息，每页只允许打印10个，所以最多只能容纳10个人员的数据信息
        private List<ArWorkerInfo> _cardDatas = null;
        #endregion

        #region New Structure
        public CreateCardImage(List<ArWorkerInfo> cardDatas)
        {
            _cardDatas = cardDatas;
        }
        public Image CreateCardBmp()
        {
            //Image imageTemplate =Properties.Resources.DirectCardTemplate;
            //创建画布 
            Bitmap bmp = new Bitmap(800, 1200, PixelFormat.Format24bppRgb);

            //bmp.SetResolution(imageTemplate.HorizontalResolution, imageTemplate.VerticalResolution);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // 清空背景并以白色填充背景
                g.Clear(Color.White);
                // 设置画布的描绘质量
                g.CompositingQuality = CompositingQuality.HighQuality;
                // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                // 指定高质量、低速度呈现。 
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                int i = 0;
                while (i < _cardDatas.Count)
                {
                    var data = _cardDatas[i];
                    // 在指定位置绘制指定的文字
                    Brush drawBrush = new SolidBrush(Color.Black);
                    Font drawFont = new Font("宋体", 1.2F, FontStyle.Regular);
                    if (data != null)
                    {
                        //if (data.PostNature == "间接")
                        //{ imageTemplate = Properties.Resources.DirectCardTemplate; }
                        //else
                        //{ imageTemplate = Properties.Resources.IndirectCardTemplate; }
                        //DrawImage(imageTemplate, g, drawBrush, drawFont, i, _cardDatas[i]);
                        i++;
                    }
                }
                //bmp.Save(@"E:/label_date1.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                g.Dispose();
                return bmp;
            }
        }
        #endregion

        #region Draw Imange
        private void DrawImage(Image imageFrom, Graphics g, Brush brush, Font font, int index, ArWorkerInfo cardData)
        {
            if (index >= 5)
            {
                DrawImageRight(imageFrom, g, brush, font, index, cardData);
            }
            else
            {
                DrawImageLeft(imageFrom, g, brush, font, index, cardData);
            }
        }
        private void DrawImage(Image imageFrom, Graphics g, Brush brush, Font font, int x, int y, ArWorkerInfo cardData)
        {
            g.DrawImage(imageFrom, new Rectangle(x, y, decImageWidth, decImageHeight), new Rectangle(0, 0, imageFrom.Width, imageFrom.Height), GraphicsUnit.Pixel);
            g.DrawString("姓名：", font, brush, new RectangleF(x + 140, y + 92, 497, 40));
            g.DrawString(cardData.Name, font, brush, new RectangleF(x + 190, y + 92, 497, 40));
            g.DrawString("部门：", font, brush, new RectangleF(x + 140, y + 118, 497, 40));
            g.DrawString(cardData.Department, font, brush, new RectangleF(x + 190, y + 118, 497, 40));
            g.DrawString("工号：", font, brush, new RectangleF(x + 140, y + 143, 497, 40));
            g.DrawString(cardData.WorkerId, font, brush, new RectangleF(x + 190, y + 143, 497, 40));
        }
        private void DrawImageLeft(Image imageFrom, Graphics g, Brush brush, Font font, int index, ArWorkerInfo cardData)
        {
            if (index == 0)
            {
                DrawImage(imageFrom, g, brush, font, startX, startY, cardData);
            }
            else
            {
                DrawImage(imageFrom, g, brush, font, startX, startY + (decImageHeight + 3) * index, cardData);
            }
        }
        private void DrawImageRight(Image imageFrom, Graphics g, Brush brush, Font font, int index, ArWorkerInfo cardData)
        {
            if (index >= 5) index = index - 5;
            if (index == 0)
            {
                DrawImage(imageFrom, g, brush, font, startX + decImageWidth + midSpliter, startY, cardData);
            }
            else
            {
                DrawImage(imageFrom, g, brush, font, startX + decImageWidth + midSpliter, startY + (decImageHeight + 3) * index, cardData);
            }
        }
        #endregion
    }
}
