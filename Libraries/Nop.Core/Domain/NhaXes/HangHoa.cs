using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class HangHoa : BaseEntity
    {
        public string TenHangHoa { get; set; }
        public int LoaiHangHoaId { get; set; }
        public decimal CanNang { get; set; }
        public decimal GiaTri { get; set; }
        public decimal GiaCuoc { get; set; }
        public int SoLuong { get; set; }
        public int PhieuGuiHangId { get; set; }
        public virtual PhieuGuiHang phieuguihang { get; set; }
        public string GhiChu { get; set; }
        public ENLoaiHangHoa LoaiHangHoa
        {
            get
            {
                return (ENLoaiHangHoa)this.LoaiHangHoaId;
            }
            set
            {
                this.LoaiHangHoaId = (int)value;
            }
        }

     

    }
}
