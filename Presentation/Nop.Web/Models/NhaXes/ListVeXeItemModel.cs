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
    public class ListVeXeItemModel
    {
        public ListVeXeItemModel()
        {
            NumRow = 100;
        }
        public string ThongTin { get; set; }
        public int MauVeId { get; set; }
        public IList<SelectListItem> ddlmauves { get; set; }
        public int LoaiVeId { get; set; }
        public IList<SelectListItem> ddlLoaiVes { get; set; }
        public int NguoiNhanId { get; set; }
        public int VanPhongId { get; set; }
        public IList<SelectListItem> dllVanPhongs { get; set; }
        public int TrangThaiId { get; set; }
        public ENVeXeItemTrangThai trangthai
        {
            get
            {
                return (ENVeXeItemTrangThai)TrangThaiId;
            }
        }
        public IList<SelectListItem> dlltrangthais { get; set; }
        public int NumRow { get; set; }
    }
}