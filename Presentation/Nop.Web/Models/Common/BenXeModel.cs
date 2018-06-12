using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.Common
{
    public class ComMonSlideModel:BaseNopEntityModel
    {

        public List<BenXeComMonModel> benxes { get; set; }
        public List<NhaXeComMonModel> nhaxes { get; set; }
        public class BenXeComMonModel : BaseNopEntityModel
        {

            [NopResourceDisplayName("ChonVe.BenXe.TenBenXe")]
            public string TenBenXe { get; set; }
            [NopResourceDisplayName("ChonVe.BenXe.DiaChiId")]
            public int DiaChiId { get; set; }
            public string DiaChiText { get; set; }

            [UIHint("PicturePublish")]
            [NopResourceDisplayName("ChonVe.BenXe.PictureId")]
            public int PictureId { get; set; }
            public string PictureUrl { get; set; }
            [NopResourceDisplayName("ChonVe.BenXe.HienThi")]
            public bool HienThi { get; set; }

        }
        public class NhaXeComMonModel : BaseNopEntityModel
        {          
            [NopResourceDisplayName("ChonVe.BenXe.TenNhaXe")]
            public string TenNhaXe { get; set; }

            [UIHint("PicturePublish")]
            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.Picture")]
            public int Picture_Id { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.Picture")]
            public string PictureUrl { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.DisplayOrder")]
            public int DisplayOrder { get; set; }

        }
    }
   
}