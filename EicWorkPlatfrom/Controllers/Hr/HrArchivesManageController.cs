using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using System;
using System.Collections.Generic;
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
                    vm = new IdentityViewModel() { Identity = m, IsExpire = ArchiveService.ArchivesManager.IdentityManager.IdentityLimitDateIsExpired(m) };
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
            if (datas != null && datas.Count > 0)
            {
                datas.ForEach(e =>
                {
                    e.Department = ArchiveService.ArchivesManager.DepartmentMananger.GetDepartmentText(e.Department);
                });
            }
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
    }

    public class IdentityViewModel
    {
        public ArchivesIdentityModel Identity { get; set; }

        /// <summary>
        /// 身份证是否过期
        /// </summary>
        public bool IsExpire { get; set; }
    }
}