using FluentValidation;
using FluentValidation.Results;
using Nop.Core.Domain.Customers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.Customer;

namespace Nop.Web.Validators.Customer
{
    public class CustomerInfoValidator : BaseNopValidator<CustomerInfoModel>
    {
        public CustomerInfoValidator(ILocalizationService localizationService,
            IStateProvinceService stateProvinceService, 
            CustomerSettings customerSettings)
        {           
            RuleFor(x => x.FullName).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Fullname.required"));
          

            if (customerSettings.PhoneRequired && customerSettings.PhoneEnabled)
            {
                RuleFor(x => x.Phone).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Phone.Required"));
            }
           
        }
    }
}