using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Validators.NhaXes
{
    public class HanhTrinhValidator : BaseNopValidator<HanhTrinhModel>
    {
        public HanhTrinhValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.MaHanhTrinh).NotEmpty().WithMessage(localizationService.GetResource("ChonVe.NhaXe.HanhTrinh.MaHanhTrinh.ChuaNhap"));
        }
    }
}