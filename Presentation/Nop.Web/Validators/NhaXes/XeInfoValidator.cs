using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Validators.NhaXes
{
    public class XeInfoValidator : BaseNopValidator<XeInfoModel>
    {
        public XeInfoValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.TenXe).NotEmpty().WithMessage(localizationService.GetResource("ChonVe.NhaXe.XeInfo.TenXe.YeuCau"));
            RuleFor(x => x.BienSo).NotEmpty().WithMessage(localizationService.GetResource("ChonVe.NhaXe.XeInfo.BienSo.YeuCau")); 
        }
    }
}