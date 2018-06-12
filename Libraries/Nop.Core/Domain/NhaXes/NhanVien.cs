using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class NhanVien : BaseEntity
    {
        public string HoVaTen { get; set; }
        public string TenVaHo { 
            get
            {
                string _hovaten = HoVaTen.Trim();
                int _pos = _hovaten.LastIndexOf(' ');
                if (_pos > 0)
                {
                    string _ten = _hovaten.Substring(_pos).Trim();
                    string _ho = _hovaten.Substring(0, _pos).Trim();
                    return _ten + " " + _ho;
                }
                return HoVaTen;
            }
                 
        }
        public string Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public int KieuNhanVienID { get; set; }
        public string CMT_Id { get; set; }
        public DateTime? CMT_NgayCap { get; set; }
        public string CMT_NoiCap { get; set; }
        public string DienThoai { get; set; }
        public int GioiTinhID { get; set; }
        public int DiaChiID { get; set; }
        public virtual DiaChi diachilienlac { get; set; }
        public int TrangThaiID { get; set; }
        public DateTime? NgayBatDauLamViec { get; set; }
        public DateTime? NgayNghiViec { get; set; }
        public int? CustomerID { get; set; }
        public int NhaXeID { get; set; }
        public int? VanPhongID { get; set; }
        public bool isDelete { get; set; }
        public DateTime CreatedOn { get; set; }

        public ENKieuNhanVien KieuNhanVien
        {
            get
            {
                return (ENKieuNhanVien)KieuNhanVienID;
            }
            set
            {
                KieuNhanVienID = (int)value;
            }
        }
        public ENGioiTinh GioiTinh
        {
            get
            {
                return (ENGioiTinh)GioiTinhID;
            }
            set
            {
                GioiTinhID = (int)value;
            }
        }
        public ENTrangThaiNhanVien TrangThai
        {
            get
            {
                return (ENTrangThaiNhanVien)TrangThaiID;
            }
            set
            {
                TrangThaiID = (int)value;
            }
        }
        public string SoGiayPhepLaiXe { get; set; }
        public DateTime? NgayCapGiayPhep { get; set; }

        private ICollection<VanPhong> _vanphongs;
        /// <summary>
        /// Quan ly cac van phong
        /// dung trong cac truong hop tao dropdownlist van phong, cac hanh trinh theo van phong
        /// </summary>
        public virtual ICollection<VanPhong> VanPhongs
        {
            get { return _vanphongs ?? (_vanphongs = new List<VanPhong>()); }
            protected set { _vanphongs = value; }
        }

        /// <summary>
        /// Thong tin diem chot
        /// </summary>
        private ICollection<DiemDon> _diemdons;
        /// <summary>
        /// Diem chot        
        /// </summary>
        public virtual ICollection<DiemDon> DiemDons
        {
            get { return _diemdons ?? (_diemdons = new List<DiemDon>()); }
            protected set { _diemdons = value; }
        }
    }
}
