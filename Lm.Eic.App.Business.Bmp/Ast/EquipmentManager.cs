using Lm.Eic.App.DbAccess.Bpm.Repository.AstRep;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.Business.Bmp.Ast
{

    /// <summary>
    /// 设备管理接口 只开发需要开放的接口
    /// </summary>
    public interface IEquipmentManager
    {
        /// <summary>
        /// 修改数据仓库
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="operationMode">操作模式 1.新增 2.修改 3.删除</param>
        /// <returns></returns>
        int ChangeStorage(EquipmentModel model, int operationMode);

        /// <summary>
        /// 修改数据仓库
        /// </summary>
        /// <param name="listModel">模型</param>
        /// <param name="operationMode">操作模式 1.新增 2.修改 3.删除</param>
        /// <returns></returns>
        int ChangeStorage(List<EquipmentModel> listModel, int operationMode);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="qryDto">查询参数Dto</param>
        /// <param name="searchMode">查询模式</param>
        /// <returns></returns>
        List<EquipmentModel> FindBy(QueryEquipmentDto qryDto, int searchMode);
    }


    /// <summary>
    /// 设备管理具体实现
    /// </summary>
    public class EquipmentManager : IEquipmentManager
    {
        IEquipmentRepository irep = null;

        public EquipmentManager()
        {
            irep = new EquipmentRepository();
        }

        public int ChangeStorage(EquipmentModel model, int OpSign)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="qryModel">设备查询数据传输对象</param>
        /// <param name="searchMode"> 1.依据部门查询 2.查询待校验设备 3.查询待维修设备 4.查询待保养设备</param>
        /// <returns></returns>
        public List<EquipmentModel> FindBy(QueryEquipmentDto qryDto, int searchMode)
        {
            try
            {
                switch (searchMode)
                {
                    case 1: //根据部门查询
                        return irep.FindAll<EquipmentModel>(m => m.SafekeepDepartment.StartsWith(qryDto.Department)).ToList();
                    case 2: //查询待校验设备
                        return irep.FindAll<EquipmentModel>(m => m.SafekeepDepartment.StartsWith(qryDto.Department)).ToList();
                    case 3: //查询待维修设备
                        return irep.FindAll<EquipmentModel>(m => m.SafekeepDepartment.StartsWith(qryDto.Department)).ToList();
                    case 4: //查询待保养设备
                        return irep.FindAll<EquipmentModel>(m => m.SafekeepDepartment.StartsWith(qryDto.Department)).ToList();
                    default: return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

      

    }


    public class QueryEquipmentDto
    {
        string department = string.Empty;
        public string Department
        {
            get { return department; }
            set { if (department != value) { department = value; } }
        }
    }
}
