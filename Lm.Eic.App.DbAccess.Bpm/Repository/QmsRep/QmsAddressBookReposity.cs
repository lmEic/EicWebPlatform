using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep
{
    public class QmsAddressBookReposity
    {
    }

    /// <summary>
    /// 通讯录
    /// </summary>
    public interface IAddressBookRepository : IRepository<AddressBookModel>
    {

    }
    public class AddressBookRepository : BpmRepositoryBase<AddressBookModel>, IAddressBookRepository
    {

    }
}
