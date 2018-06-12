using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class ChuyenDiTaiChinhThuChi : BaseEntity
    {
        public ChuyenDiTaiChinhThuChi()
        {

        }
        public ChuyenDiTaiChinhThuChi(int _ChuyenDiTaiChinhId, ENLoaiTaiChinhThuChi _loaithuchi, decimal _sotien=0)
        {
            loaithuchi = _loaithuchi;
            ChuyenDiTaiChinhId = _ChuyenDiTaiChinhId;
            SoTien = _sotien;
        }
        public int ChuyenDiTaiChinhId { get; set; }
        public virtual ChuyenDiTaiChinh chuyenditaichinh { get; set; }
        public int LoaiThuChiId { get; set; }
        public ENLoaiTaiChinhThuChi loaithuchi
        {
            get
            {
                return (ENLoaiTaiChinhThuChi)LoaiThuChiId;
            }
            set
            {
                LoaiThuChiId = (int)value;
            }
        }
        public decimal SoTien { get; set; }
        public string GhiChu { get; set; }

    }
}
