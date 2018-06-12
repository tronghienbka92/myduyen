using FluentValidation.Attributes;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
    public class CapNhatVeChuyenModel 
    {
        public CapNhatVeChuyenModel()
        {
            chuyendi = new VeXeChuyen();
            chuyenve = new VeXeChuyen();
        }
        public VeXeChuyen chuyendi { get; set; }
        public VeXeChuyen chuyenve { get; set; }
        public class VeXeChuyen
        {
            public VeXeChuyen()
            {
                arrVeXeQuay = new List<VeXeItem>();
                arrVeXeTrenXe = new List<VeXeItem>();                
            }
            public int ChuyenId { get; set; }
            public HistoryXeXuatBen chuyenxe { get; set; }
            public string SeriVeQuayText { get; set; }
            public List<VeXeItem> arrVeXeQuay { get; set; }
            public List<VeXeItem> arrVeXeTrenXe { get; set; }
            public List<HanhTrinhDiemDon> arrDiemDon { get; set; }           
            public VeXeChang[,] arrVeXeChang { get; set; }
            public string SeriKhongHopLe { get; set; }
        }
        public class VeXeChang
        {
            public int ChangId { get; set; }
            public HanhTrinhGiaVe changgiave { get; set; }
            public int SoLuong { get; set; }
            public string SeriVeXeText { get; set; }
            public List<VeXeItem> arrVeXe { get; set; }
        }
       

    }
   
   
}