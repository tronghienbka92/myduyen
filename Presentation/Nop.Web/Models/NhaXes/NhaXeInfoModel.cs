using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Validators.NhaXes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
    [Validator(typeof(NhaXeInfoValidator))]
    public class NhaXeInfoModel : BaseNopEntityModel
    {
        public NhaXeInfoModel()
        {            
            ThongTinDiaChi = new DiaChiInfoModel();
            AddPictureModel = new NhaXePictureModel();
            NhaXePictureModels = new List<NhaXePictureModel>();
        }
        [NopResourceDisplayName("Admin.ChonVe.NhaXe.MaNhaXe")]        
        public string MaNhaXe { get; set; }
        
        [NopResourceDisplayName("Admin.ChonVe.NhaXe.TenNhaXe")]        
        public string TenNhaXe { get; set; }
        
        [NopResourceDisplayName("Admin.ChonVe.NhaXe.GioiThieu")]
        [AllowHtml]
        public string GioiThieu { get; set; }
        [NopResourceDisplayName("Admin.ChonVe.NhaXe.DieuKhoanGuiHang")]
        [AllowHtml]
        public string DieuKhoanGuiHang { get; set; }

        [UIHint("PicturePublish")]
        [NopResourceDisplayName("Admin.ChonVe.NhaXe.Logo")]
        public int LogoID { get; set; }

        [UIHint("PicturePublish")]
        [NopResourceDisplayName("Admin.ChonVe.NhaXe.AnhDaiDien")]
        public int AnhDaiDienID { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.NhaXe.Email")]
        public string Email { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.NhaXe.DienThoai")]
        public string DienThoai { get; set; }
        

        [NopResourceDisplayName("Admin.ChonVe.NhaXe.Fax")]
        public string Fax { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.NhaXe.HotLine")]
        public string HotLine { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.NhaXe.DiaChi")]
        public int DiaChiID { get; set; }
        public DiaChiInfoModel ThongTinDiaChi { get; set; }

        [NopResourceDisplayName("Admin.ChonVe.NhaXe.NguoiTao")]
        public int NguoiTaoID { get; set; }
        public List<HanhTrinhInNhaXeModel> HanhTrinhs { get; set; }

        //pictures
        public NhaXePictureModel AddPictureModel { get; set; }
        public IList<NhaXePictureModel> NhaXePictureModels { get; set; }

        public partial class NhaXePictureModel : BaseNopEntityModel
        {
            public int NhaXe_Id { get; set; }

            [UIHint("PicturePublish")]
            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.Picture")]
            public int Picture_Id { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.Picture")]
            public string PictureUrl { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.DisplayOrder")]
            public int DisplayOrder { get; set; }
        }
        public partial class HanhTrinhInNhaXeModel : BaseNopEntityModel
        {


            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.Mota")]
            public string Mota { get; set; }
            public Decimal GiaVeToanTuyen { get; set; }
            public string GiaVeToanTuyenText { get; set; }

          

            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.SoChuyenTrongNgay")]
            public string SoChuyenTrongNgay { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.LoaiXe")]
            public string LoaiXe { get; set; }
            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.HangXe")]
            public string HangXe { get; set; }
        }

    }
}