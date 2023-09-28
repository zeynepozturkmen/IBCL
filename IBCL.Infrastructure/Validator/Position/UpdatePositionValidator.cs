using FluentValidation;
using IBCL.Application.Common.Models.Request.Positions;

namespace IBCL.Infrastructure.Validator.Position
{
    public class UpdatePositionValidator : AbstractValidator<UpdatePositionRequest>
    {
        public UpdatePositionValidator()
        {

            RuleFor(x => x.Id)
              .NotNull().NotEmpty();

            RuleFor(x => x.Amount)
              .NotNull().NotEmpty();

        }
    }
}
