using FluentValidation;
using IBCL.Application.Common.Models.Request.Positions;

namespace IBCL.Infrastructure.Validator.Position
{
    public class GetAllPositionValidator : AbstractValidator<GetAllPositionRequest>
    {
        public GetAllPositionValidator()
        {
            RuleFor(x => x.AccountId)
              .NotNull().NotEmpty();
        }
    }
}
