using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Validators.NhaXes
{
    public class NhanVienValidator : BaseNopValidator<NhanVienModel>
    {
        public NhanVienValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.HoVaTen).NotEmpty().WithMessage(localizationService.GetResource("ChonVe.NhaXe.NhanVien.HoVaTen.YeuCau"));
            RuleFor(x => x.Email).NotEmpty().WithMessage(localizationService.GetResource("ChonVe.NhaXe.Email.YeuCau"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("ChonVe.NhaXe.Email.KoHopLe"));
        }
    }
}