using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Validators.NhaXes
{
    public class HangHoaValidator  : BaseNopValidator<PhieuGuiHangModel.HangHoaModel>
    {
        public HangHoaValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.TenHangHoa).NotEmpty().WithMessage(localizationService.GetResource("Thông tin yêu cầu "));            
        }
    }
}