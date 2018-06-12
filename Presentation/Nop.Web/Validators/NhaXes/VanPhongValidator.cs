using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Validators.NhaXes
{
    public class VanPhongValidator : BaseNopValidator<VanPhongModel>
    {
        public VanPhongValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.TenVanPhong).NotEmpty().WithMessage(localizationService.GetResource("ChonVe.NhaXe.VanPhong.TenVanPhong.ChuaNhap"));
            RuleFor(x => x.Ma).NotEmpty().WithMessage("Bạn chưa nhập mã văn phòng");
        }
    }
}