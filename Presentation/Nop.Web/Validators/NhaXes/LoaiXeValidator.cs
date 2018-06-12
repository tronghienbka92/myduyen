using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Validators.NhaXes
{
    public class LoaiXeValidator : BaseNopValidator<LoaiXeModel>
    {
        public LoaiXeValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.TenLoaiXe).NotEmpty().WithMessage(localizationService.GetResource("ChonVe.NhaXe.LoaiXe.TenLoaiXe.YeuCau"));
            RuleFor(x => x.MaLoaiXe).NotEmpty().WithMessage(localizationService.GetResource("Yêu cầu mã loại xe"));
            RuleFor(x => x.SoDoGheXeID).NotEmpty().WithMessage(localizationService.GetResource("Yêu cầu chọn sơ đồ ghế xe"));
            RuleFor(x => x.KieuXeID).NotEmpty().WithMessage(localizationService.GetResource("Yêu cầu chọn kiểu xe"));       
        }
    }
}