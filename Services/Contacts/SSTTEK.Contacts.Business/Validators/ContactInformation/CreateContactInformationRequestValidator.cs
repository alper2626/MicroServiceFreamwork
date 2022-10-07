using FluentValidation;
using SSTTEK.Contacts.Business.Constants;
using SSTTEK.Contacts.Entities.Poco.ContactInformationDto;
using Tools.ObjectHelpers;

namespace SSTTEK.Contacts.Business.Validators.ContactInformation
{
    public class CreateContactInformationRequestValidator : AbstractValidator<CreateContactInformationRequest>
    {
        public CreateContactInformationRequestValidator()
        {
            RuleFor(w => w.ContactEntityId).NotEmpty().NotNull().WithMessage(string.Format(ValidationMessage.NullOrEmptyMessage, nameof(CreateContactInformationRequest.ContactEntityId)));
            RuleFor(w => w.ContactInformationType).NotEmpty().NotNull().WithMessage(string.Format(ValidationMessage.NullOrEmptyMessage, nameof(CreateContactInformationRequest.ContactInformationType)));
            RuleFor(w => w.Content).NotEmpty().NotNull().WithMessage(string.Format(ValidationMessage.NullOrEmptyMessage, nameof(CreateContactInformationRequest.Content)));
            RuleFor(w => w.Content).Must(StringHelper.NotContainSpace).WithMessage(string.Format(ValidationMessage.ConnotContainsSpace, nameof(CreateContactInformationRequest.Content)));
            RuleFor(w => w).Must(ValidatePhoneOrMailAddress).WithMessage(string.Format(ValidationMessage.WrongFormat, nameof(CreateContactInformationRequest.Content)));
        }

        public bool ValidatePhoneOrMailAddress(CreateContactInformationRequest model)
        {
            switch (model.ContactInformationType)
            {
                case Entities.Enum.ContactInformationType.PhoneNumber:
                    return StringHelper.IsValidPhoneNumber(model.Content);
                case Entities.Enum.ContactInformationType.MailAddress:
                    return StringHelper.IsValidMailAddress(model.Content);
                case Entities.Enum.ContactInformationType.Location:
                default:
                    return true;
            }
        }
    }
}
