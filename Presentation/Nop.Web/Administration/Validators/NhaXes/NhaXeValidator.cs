using FluentValidation;
using Nop.Admin.Models.Catalog;
using Nop.Admin.Models.NhaXes;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;


namespace Nop.Admin.Validators.NhaXes
{
    public class NhaXeValidator : BaseNopValidator<NhaXeModel>
    {
        public NhaXeValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.TenNhaXe).NotEmpty().WithMessage(localizationService.GetResource("Admin.ChonVe.NhaXe.Fields.TenNhaXe.Required"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("Admin.ChonVe.NhaXe.Fields.Email.KoHopLe"));
        }
    }
}