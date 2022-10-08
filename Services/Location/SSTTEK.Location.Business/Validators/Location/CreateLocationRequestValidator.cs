using FluentValidation;
using RestHelpers.Constacts;
using SSTTEK.Location.Entities.Poco.Location;

namespace SSTTEK.Location.Business.Validators.Location
{
    public class CreateLocationRequestValidator : AbstractValidator<CreateLocationRequest>
    {
        public CreateLocationRequestValidator()
        {
            RuleFor(w => w.Name).NotEmpty().NotNull().WithMessage(string.Format(CommonMessage.NullOrEmptyMessage, nameof(CreateLocationRequest.Name)));
        }
    }
}
