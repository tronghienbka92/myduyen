using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Core.Domain.NhaXes
{
    public class GiaoDichKeVe : BaseEntity
    {
        public GiaoDichKeVe()
        {
            NgayTao = DateTime.Now;
            TrangThai = ENGiaoDichKeVeTrangThai.MOI_TAO;
            PhanLoai = ENGiaoDichKeVePhanLoai.KE_VE;
        }
        public string Ma { get; set; }
        public DateTime NgayKe { get; set; }
        public DateTime NgayTao { get; set; }
        public string GhiChu { get; set; }
        public int NguoiGiaoId { get; set; }
        public virtual NhanVien nguoigiao { get; set; }
        public int? NguoiNhanId { get; set; }
        public int? VanPhongId { get; set; }
        public virtual VanPhong quaybanve { get; set; }
        public int? LoaiVeId { get; set; }
        public ENLoaiVeXeItem LoaiVe {
            get 
            {
                return (ENLoaiVeXeItem)LoaiVeId.GetValueOrDefault(5);
            }
            set {
                LoaiVeId = (int)value;
            } 
        }
        public virtual NhanVien nguoinhan { get; set; }
        public int TrangThaiId { get; set; }

        public ENGiaoDichKeVeTrangThai TrangThai
        {
            get
            {
                return (ENGiaoDichKeVeTrangThai)TrangThaiId;
            }
            set
            {
                TrangThaiId = (int)value;
            }
        }
        public string SessionId { get; set; }
        public int NhaXeId { get; set; }
        public virtual NhaXe nhaxe { get; set; }
     
        private ICollection<GiaoDichKeVeMenhGia> _kevemenhgias;
        public virtual ICollection<GiaoDichKeVeMenhGia> kevemenhgias
        {
            get { return _kevemenhgias ?? (_kevemenhgias = new List<GiaoDichKeVeMenhGia>()); }
            protected set { _kevemenhgias = value; }
        }
        public int PhanLoaiId { get; set; }
        public ENGiaoDichKeVePhanLoai PhanLoai
        {
            get
            {
                return (ENGiaoDichKeVePhanLoai)PhanLoaiId;
            }
            set
            {
                PhanLoaiId = (int)value;
            }
        }
    }
}
