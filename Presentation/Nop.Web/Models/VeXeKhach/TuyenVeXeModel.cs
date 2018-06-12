using FluentValidation.Attributes;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.NhaXes;
using Nop.Web.Validators.NhaXes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.VeXeKhach
{
    public class TuyenVeXeHomeModel
    {
        public TuyenVeXeHomeModel()
        {
            tuyenxes1 = new List<TuyenVeXeModel>();
            tuyenxes2 = new List<TuyenVeXeModel>();
            tuyenxes3 = new List<TuyenVeXeModel>();
        }
        public string TenTuyenXe1 { get; set; }
        public IList<TuyenVeXeModel> tuyenxes1 { get; set; }
        public string TenTuyenXe2 { get; set; }
        public IList<TuyenVeXeModel> tuyenxes2 { get; set; }
        public string TenTuyenXe3 { get; set; }
        public IList<TuyenVeXeModel> tuyenxes3 { get; set; }
    }
    public class TuyenVeXeModel : BaseNopEntityModel
    {

        public int Province1Id { get; set; }
        public string TenTinhDi { get; set; }
        public int Province2Id { get; set; }
        public string TenTinhDen { get; set; }
        public string SeName { get; set; }
        public Decimal PriceOld { get; set; }
        public string PriceOldText { get; set; }      
        public Decimal PriceNew { get; set; }
        public string PriceNewText { get; set; }
        public int KieuXeId { get; set; }
        public ENKieuXe KieuXe
        {
            get
            {
                return (ENKieuXe)KieuXeId;
            }
            set
            {
                KieuXeId = (int)value;
            }
        }

    }
}