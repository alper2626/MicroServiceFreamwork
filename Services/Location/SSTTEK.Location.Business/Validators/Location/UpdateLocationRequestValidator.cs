using FluentValidation;
using RestHelpers.Constacts;
using SSTTEK.Location.Entities.Poco.Location;

namespace SSTTEK.Location.Business.Validators.Location
{
    public class UpdateLocationRequestValidator : AbstractValidator<UpdateLocationRequest>
    {
        public UpdateLocationRequestValidator()
        {
            RuleFor(w => w.Name).NotEmpty().NotNull().WithMessage(string.Format(CommonMessage.NullOrEmptyMessage, nameof(UpdateLocationRequest.Name)));
        }
    }
}
