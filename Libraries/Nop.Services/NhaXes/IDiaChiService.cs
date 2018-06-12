using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.NhaXes
{
    public partial interface IDiaChiService
    {
        DiaChi GetById(int itemid);
        void Insert(DiaChi _item);
        void Update(DiaChi _item);
        void Delete(DiaChi _item);
        List<QuanHuyen> GetQuanHuyenByProvinceId(int ProvinceId);
        QuanHuyen GetQuanHuyenById(int QuanHuyenId);
    }
}
