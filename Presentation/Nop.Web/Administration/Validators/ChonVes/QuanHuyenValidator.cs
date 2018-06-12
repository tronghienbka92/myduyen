using FluentValidation;
using Nop.Admin.Models.Catalog;
using Nop.Admin.Models.ChonVes;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;


namespace Nop.Admin.Validators.ChonVes
{
    public class QuanHuyenValidator : BaseNopValidator<QuanHuyenModel>
    {
        public QuanHuyenValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Ten).NotEmpty().WithMessage(localizationService.GetResource("Admin.ChonVe.QuanHuyen.Fields.Ten.Required"));            
        }
    }
}