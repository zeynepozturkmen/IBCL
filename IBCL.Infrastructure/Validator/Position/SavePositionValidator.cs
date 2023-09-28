using FluentValidation;
using IBCL.Application.Common.Models.Request.Positions;

namespace IBCL.Infrastructure.Validator.Position
{
    public class SavePositionValidator : AbstractValidator<SavePositionRequest>
    {
        public SavePositionValidator()
        {
            RuleFor(x => x.Amount)
              .NotNull().NotEmpty();

            RuleFor(x => x.AccountId)
           .NotNull().NotEmpty();

            RuleFor(x => x.AssetId)
          .NotNull().NotEmpty();
        }
    }
}