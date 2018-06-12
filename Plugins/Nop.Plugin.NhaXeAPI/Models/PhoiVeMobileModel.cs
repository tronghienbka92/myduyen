using Nop.Web.Framework.Mvc;
using System;

namespace Nop.Plugin.NhaXeAPI.Models
{
    public class PhoiVeMobileModel : BaseNopEntityModel
    {
        // ID la id cua phoi ve, >0 có giá trị tại vi trí, =0, là còn trống
        //thong tin vi tri cho ngoi tren so do xe
        public int SoDoGheXeQuyTacId { get; set; }
        public string Val { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int Tang { get; set; }
        public int LoaiXeId { get; set; }
        //thong tin nguon ve xe di tren hanh trinh nao
        public int NguonVeXeId { get; set; }
        public int GiaVe { get; set; }
        public string NgayDi { get; set; }
        //thong tin trang thai vitri cho ngoi
        public int TrangThaiId { get; set; }
        public int CustomerId { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        /// <summary>
        /// thong tin huyen, tinh - xuong
        /// </summary>
        public string ViTriXuong { get; set; }
        public int VeXeItemId { get; set; }
        public String MaVe { get; set; }
        public int ChangId { get; set; }
        public String SessionId { get; set; }
    }
}
