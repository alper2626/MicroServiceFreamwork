using FluentValidation;
using RestHelpers.Constacts;
using SSTTEK.ContactInformation.Entities.Enum;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;
using Tools.ObjectHelpers;

namespace SSTTEK.ContactInformation.Business.Validators.ContactInformation
{
    public class CreateContactInformationRequestValidator : AbstractValidator<CreateContactInformationRequest>
    {
        public CreateContactInformationRequestValidator()
        {
            RuleFor(w => w.ContactEntityId).NotEmpty().NotNull().WithMessage(string.Format(CommonMessage.NullOrEmptyMessage, nameof(CreateContactInformationRequest.ContactEntityId)));
            RuleFor(w => w.Content).NotEmpty().NotNull().WithMessage(string.Format(CommonMessage.NullOrEmptyMessage, nameof(CreateContactInformationRequest.Content)));
            RuleFor(w => w.Content).Must(StringHelper.NotContainSpace).WithMessage(string.Format(CommonMessage.ConnotContainsSpace, nameof(CreateContactInformationRequest.Content)));
            RuleFor(w => w).Must(ValidatePhoneOrMailAddress).WithMessage(string.Format(CommonMessage.WrongFormat, nameof(CreateContactInformationRequest.Content)));
        }

        public bool ValidatePhoneOrMailAddress(CreateContactInformationRequest model)
        {
            switch (model.ContactInformationType)
            {
                case ContactInformationType.PhoneNumber:
                    return StringHelper.IsValidPhoneNumber(model.Content);
                case ContactInformationType.MailAddress:
                    return StringHelper.IsValidMailAddress(model.Content);
                case ContactInformationType.Location:
                    return true;
                default:
                    return false;
            }
        }
    }
}
