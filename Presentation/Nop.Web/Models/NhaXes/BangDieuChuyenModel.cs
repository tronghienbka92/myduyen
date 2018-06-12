using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.NhaXes
{
    public class BangDieuChuyenModel
    {
        public DateTime NgayDi { get; set; }
        public List<BanDieuChuyenHanhTrinh> arrBangDieuChuyen { get; set; }
        public class BanDieuChuyenHanhTrinh
        {
            public BanDieuChuyenHanhTrinh()
            {
                LichTrinhItems = new List<BangDieuChuyenItem>();
            }
            public HanhTrinh hanhtrinhinfo { get; set; }
            public List<BangDieuChuyenItem> LichTrinhItems { get; set; }
        }
        public class BangDieuChuyenItem
        {
            public BangDieuChuyenItem(int _id, int _hantrinhid,string _tenhanhtrinh,int _lichtrinhid,string _tenlichtrinh)
            {
                Id = _id;
                HanhTrinhId = _hantrinhid;
                TenHanhTrinh = _tenhanhtrinh;
                LichTrinhId = _lichtrinhid;
                TenLichTrinh = _tenlichtrinh;
            }
            public int Id { get; set; }
            public int HanhTrinhId { get; set; }
            public string TenHanhTrinh { get; set;}
            public int LichTrinhId { get; set; }
            public string TenLichTrinh { get; set; }
            public List<XeXuatBenItemModel> chuyendis { get; set; }
        }
    }
}