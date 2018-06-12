using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Validators.NhaXes
{
    public class NhaXeInfoValidator : BaseNopValidator<NhaXeInfoModel>
    {
        public NhaXeInfoValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("Admin.ChonVe.NhaXe.Fields.Email.KoHopLe"));
        }
    }
}