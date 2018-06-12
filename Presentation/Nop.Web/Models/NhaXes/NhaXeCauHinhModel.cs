using FluentValidation.Attributes;
using Nop.Core.Domain.NhaXes;
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
    [Validator(typeof(NhaXeCauHinhValidator))]
    public class NhaXeCauHinhModel : BaseNopEntityModel
    {
        public NhaXeCauHinhModel()
        {
            ItemPerPage = 18;
            KyTuRepeatStart = "<tr class=\"repeat\">";
            KyTuRepeatEnd = "</tr>";
            SoLien = 1;
        }
        public string Ten { get; set; }
        
        public int MaId { get; set; }
        public ENNhaXeCauHinh MaCauHinh
        {
            get
            {
                return (ENNhaXeCauHinh)MaId;
            }
            set
            {
                MaId = (int)value;
            }
        }
        
        [AllowHtml]
        public string GiaTri { get; set; }
        /// <summary>
        /// Su dung cho cac row (co vong lap)
        /// </summary>
        [AllowHtml]
        public string GiaTriItem { get; set; }
        /// <summary>
        /// Su dung cho cac row (co vong lap)
        /// </summary>
        [AllowHtml]
        public string GiaTriItem1 { get; set; }
        public int KieuDuLieuId { get; set; }
        public ENKieuDuLieu kieudulieu
        {
            get
            {
                return (ENKieuDuLieu)KieuDuLieuId;
            }
            set
            {
                KieuDuLieuId = (int)value;
            }
        }
        public int ItemPerPage { get; set; }
        [AllowHtml]
        public string KyTuRepeatStart { get; set; }
        [AllowHtml]
        public string KyTuRepeatEnd { get; set; }
        public int SoLien { get; set; }
    }
}