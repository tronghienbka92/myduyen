using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class NhaXeCustomer : BaseEntity
    {

        public int NhaXeId { get; set; }
        public int CustomerId { get; set; }
        public string HoTen { get; set; }
        public string DienThoai { get; set; }
        public string SearchInfo { get; set; }
        public string DiaChiLienHe { get; set; }
        public virtual NhaXe nhaxe { get; set; }
        
    }
}
