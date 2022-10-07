using FluentValidation;
using SSTTEK.Contact.Business.Constants;
using SSTTEK.Contact.Entities.Poco.ContactDto;
namespace SSTTEK.Contact.Business.Validators.Contact
{
    public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
    {
        public CreateContactRequestValidator()
        {
            RuleFor(w => w.Name).NotEmpty().NotNull().WithMessage(string.Format(ValidationMessage.NullOrEmptyMessage, nameof(CreateContactRequest.Name)));
            RuleFor(w => w.LastName).NotEmpty().NotNull().WithMessage(string.Format(ValidationMessage.NullOrEmptyMessage, nameof(CreateContactRequest.LastName)));
            RuleFor(w => w.Firm).NotEmpty().NotNull().WithMessage(string.Format(ValidationMessage.NullOrEmptyMessage, nameof(CreateContactRequest.Firm)));
        }
    }
}
