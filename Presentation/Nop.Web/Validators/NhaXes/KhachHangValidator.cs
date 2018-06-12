using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Validators.NhaXes
{
    public class KhachHangValidator: BaseNopValidator<PhieuGuiHangModel.KhachHangModel>
    {
        public KhachHangValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.HoTen).NotEmpty().WithMessage(localizationService.GetResource("Thông tin yêu cầu "));
            RuleFor(x => x.SoDienThoai).NotEmpty().WithMessage(localizationService.GetResource("Thông tin yêu cầu"));
           
        }
    }
}