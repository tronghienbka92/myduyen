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
    public class ChotKhachModel : BaseNopEntityModel
    {
        public string Ma { get; set; }
        public DateTime NgayChot { get; set; }
        public int NguoiChotId { get; set; }
        public string nguoichot { get; set; }
        public int HistoryXeXuatBenId { get; set; }
        public string historychuyenxe { get; set; }
        public int SoLuongPhanMem { get; set; }
        public int SoLuongThucTe { get; set; }
        public int DiemDonId { get; set; }
        public string diemchot { get; set; }
        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
        public string ViTriChot { get; set; }
    }
}