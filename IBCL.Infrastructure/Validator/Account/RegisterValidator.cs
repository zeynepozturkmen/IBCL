using FluentValidation;
using IBCL.Application.Common.Models.Request.Account;

namespace IBCL.Infrastructure.Validator.Account
{
    public class RegisterValidator : AbstractValidator<AccountRegistrationModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email).MaximumLength(256)
             .NotNull().NotEmpty();

            RuleFor(x => x.PhoneCode).MaximumLength(4)
                .NotNull().NotEmpty();

            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty()
                .WithMessage("Invalid PhoneNumber format");

            RuleFor(x => x.FirstName)
                .MaximumLength(50).NotNull().NotEmpty();

            RuleFor(x => x.LastName)
                .MaximumLength(50).NotNull().NotEmpty();

            RuleFor(x => x.TelegramChatId).MaximumLength(20)
            .NotNull().NotEmpty();

            RuleFor(x => x.Password)
                .NotNull().NotEmpty();

            When(m => m.IdentityNumber is not null,
                () => { RuleFor(x => x.IdentityNumber).MaximumLength(20); });
        }
    }
}
