using FluentValidation;
using Nop.Admin.Models.Catalog;
using Nop.Admin.Models.ChonVes;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;


namespace Nop.Admin.Validators.ChonVes
{
    public class BenXeValidator : BaseNopValidator<BenXeModel>
    {
        public BenXeValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.TenBenXe).NotEmpty().WithMessage(localizationService.GetResource("ChonVe.BenXe.TenBenXe.Required"));            
        }
    }
}