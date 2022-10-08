using FluentValidation;
using RestHelpers.Constacts;
using SSTTEK.Contact.Entities.Poco.ContactDto;
namespace SSTTEK.Contact.Business.Validators.Contact
{
    public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
    {
        public UpdateContactRequestValidator()
        {
            RuleFor(w => w.Name).NotEmpty().NotNull().WithMessage(string.Format(CommonMessage.NullOrEmptyMessage, nameof(UpdateContactRequest.Name)));
            RuleFor(w => w.LastName).NotEmpty().NotNull().WithMessage(string.Format(CommonMessage.NullOrEmptyMessage, nameof(UpdateContactRequest.LastName)));
            RuleFor(w => w.Firm).NotEmpty().NotNull().WithMessage(string.Format(CommonMessage.NullOrEmptyMessage, nameof(UpdateContactRequest.Firm)));
        }
    }
}
