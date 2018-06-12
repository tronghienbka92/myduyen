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
    public class QuanLyMauVeKyHieuModel : BaseNopEntityModel
    {
        public string MauVe { get; set; }
        public string KyHieu { get; set; }
        public int NhaXeId { get; set; }
        public string TenNhaXe { get; set; }
    }
}