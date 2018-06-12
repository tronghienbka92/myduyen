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
    public class DiemChotKhachModel : BaseNopEntityModel
    {
        public string TenDiemDon { get; set; }
        public int DiaChiId { get; set; }
        public string DiaChiText { get; set; }
        public string ThongTinThanhTra { get; set; }
    }
}