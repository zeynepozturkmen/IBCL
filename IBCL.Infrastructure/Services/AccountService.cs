using IBCL.Application.Common.Interfaces;
using IBCL.Application.Common.Models;
using IBCL.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace IBCL.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Account> _userManager;

        public AccountService(UserManager<Account> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AccountRegistrationModel> Register(AccountRegistrationModel userModel)
        {

            var user = userModel.Adapt<Account>();
            user.UserName = userModel.Email;
            user.CreatedBy = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception($"{error.Code} - {error.Description}");
                }
            }

            return userModel;
        }
    }
}
