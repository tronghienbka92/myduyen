using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Validators.NhaXes
{
    public class NhaXeCauHinhValidator: BaseNopValidator<NhaXeCauHinhModel>
    {
        public NhaXeCauHinhValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Ten).NotEmpty().WithMessage("Tên không được để trống");
        }
    }
    
}