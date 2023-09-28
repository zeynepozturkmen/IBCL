using FluentValidation;
using IBCL.Application.Common.Models.Request.Account;

namespace IBCL.Infrastructure.Validator.Account
{
    public class AccountLoginValidator : AbstractValidator<AccountLoginModel>
    {
        public AccountLoginValidator()
        {
            RuleFor(x => x.Username)
              .NotNull().NotEmpty();

            RuleFor(x => x.Password)
              .NotNull().NotEmpty();
        }
    }
}
