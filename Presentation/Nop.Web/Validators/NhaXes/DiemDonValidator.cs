using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Validators.NhaXes
{
    public class DiemDonValidator : BaseNopValidator<DiemDonModel>
    {
        public DiemDonValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.TenDiemDon).NotEmpty().WithMessage(localizationService.GetResource("ChonVe.NhaXe.DiemDon.TenDiemDon.ChuaNhap"));
        }
    }
}