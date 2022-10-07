using FluentValidation;
using SSTTEK.Contacts.Business.Constants;
using SSTTEK.Contacts.Entities.Poco.ContactDto;

namespace SSTTEK.Contacts.Business.Validators.Contact
{
    public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
    {
        public UpdateContactRequestValidator()
        {
            RuleFor(w => w.Name).NotEmpty().NotNull().WithMessage(string.Format(ValidationMessage.NullOrEmptyMessage, nameof(UpdateContactRequest.Name)));
            RuleFor(w => w.LastName).NotEmpty().NotNull().WithMessage(string.Format(ValidationMessage.NullOrEmptyMessage, nameof(UpdateContactRequest.LastName)));
            RuleFor(w => w.Firm).NotEmpty().NotNull().WithMessage(string.Format(ValidationMessage.NullOrEmptyMessage, nameof(UpdateContactRequest.Firm)));
        }
    }
}
