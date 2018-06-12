using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class NhanVienPhuTrachChuyen : BaseEntity
    {
        public int NhanVienID { get; set; }
        public int NguonVeXeID { get; set; }
        public virtual NhanVien PhuTrachChuyen { get; set; }
        public virtual NguonVeXe NguonVe { get; set; }
    }
}
