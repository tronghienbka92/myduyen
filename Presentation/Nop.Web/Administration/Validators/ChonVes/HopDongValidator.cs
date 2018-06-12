using FluentValidation;
using Nop.Admin.Models.Catalog;
using Nop.Admin.Models.ChonVes;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Admin.Validators.ChonVes
{
    public class HopDongValidator : BaseNopValidator<HopDongModel>
    {
        public HopDongValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.TenHopDong).NotEmpty().WithMessage(localizationService.GetResource("Admin.ChonVe.HopDong.Fields.TenHopDong.Required"));
            //RuleFor(x => x.KhachHang.Email).EmailAddress().WithMessage(localizationService.GetResource("Admin.ChonVe.HopDong.Fields.KhachHang.Email"));
        }
    }
}