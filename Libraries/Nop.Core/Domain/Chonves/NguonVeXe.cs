using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Chonves
{
    public class NguonVeXe : BaseEntity
    {
        public NguonVeXe()
        {           
            HienThi = false;
            isDelete = false;
            ToWeb = false;
        }
        public int ProductId { get; set; }
        public int NhaXeId { get; set; }
        public int DiemDonId { get; set; }
        public virtual DiaDiem DiemDon { get; set; }
        public int DiemDenId { get; set; }
        public virtual DiaDiem DiemDen { get; set; }
        public int DiemDonGocId { get; set; }
        public virtual DiemDon DiemDonGoc { get; set; }
        public int DiemDenGocId { get; set; }
        public virtual DiemDon DiemDenGoc { get; set; }
        public int LichTrinhId { get; set; }
        public int TimeCloseOnline { get; set; }
        public int TimeOpenOnline { get; set; }        
        public DateTime ThoiGianDi { get; set; }
        public DateTime ThoiGianDen { get; set; }        
        public decimal GiaVeHienTai { get; set; }
        //public decimal GiaVeCu { get; set; }
        public string TenNhaXe { get; set; }
        public int LoaiXeId { get; set; }
        public virtual LoaiXe loaixe { get; set; }
        public string TenDiemDon { get; set; }
        public string TenDiemDen { get; set; }
        public string TenLoaiXe { get; set; }      
        public bool HienThi { get; set; }
        public bool ToWeb { get; set; }
        public bool isDelete { get; set; }
        public int ParentId { get; set; }
        public virtual Product ProductInfo { get; set; }
        public virtual LichTrinh LichTrinhInfo { get; set; }
        //public virtual NhanVien NhanVienInfo { get; set; }
        public DateTime ThoiGianDiHienTai
        {
            get
            {
                return Convert.ToDateTime(string.Format("{0} {1}:{2}:00", DateTime.Now.ToString("yyyy-MM-dd"), ThoiGianDi.ToString("HH"), ThoiGianDi.ToString("mm")));
            }
        }
    }
}
