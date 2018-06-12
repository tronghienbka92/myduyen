using Nop.Core;
using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Chonves
{
    public partial interface IQuanHuyenService
    {
        PagedList<QuanHuyen> GetAll(int ProvinceID = 0, string tenquanhuyen = "",
           int pageIndex = 0,
           int pageSize = int.MaxValue);
        List<QuanHuyen> GetAllByProvinceID(int ProvinceId);
        QuanHuyen GetById(int itemId);
        void Insert(QuanHuyen _item);
        void Update(QuanHuyen _item);
        void Delete(QuanHuyen _item);
    }
}
