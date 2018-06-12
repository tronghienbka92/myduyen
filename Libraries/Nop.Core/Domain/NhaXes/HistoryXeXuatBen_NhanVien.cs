using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class HistoryXeXuatBen_NhanVien : BaseEntity
    {
        public int NhanVien_Id { get; set; }
        public virtual NhanVien nhanvien { get; set; }
        public int HistoryXeXuatBen_Id { get; set; }
        public virtual HistoryXeXuatBen xexuatben { get; set; }
        public int KieuNhanVienID { get; set; }
        public ENKieuNhanVien KieuNhanVien { 
            get
            {
                return (ENKieuNhanVien)KieuNhanVienID;
            }
            set {
                KieuNhanVienID = (int)value;
            }
        }

    }
}
