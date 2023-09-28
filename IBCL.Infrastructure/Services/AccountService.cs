using IBCL.Application.Common.Interfaces;
using IBCL.Application.Common.Models.Request.Account;
using IBCL.Application.Common.Models.Response.Accounts;
using IBCL.Domain.Entities;
using IBCL.Domain.Enums;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IBCL.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private const decimal Balance= 10000;

        public AccountService(UserManager<Account> userManager, SignInManager<Account> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public async Task<AccountRegistrationModel> RegisterAsync(AccountRegistrationModel userModel)
        {

            var user = userModel.Adapt<Account>();
            user.UserName = userModel.Email;
            user.CreatedBy = Guid.Empty.ToString();
            user.RecordStatus = RecordStatus.Active;
            user.Balance = Balance;

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

        public async Task<AccountTokenDto> LoginAsync(AccountLoginModel request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user is null)
            {
                throw new Exception("UserNotFoundException");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            string accessToken = GenerateJwtTokenAsync(user);

            return new AccountTokenDto
            {
                AccountId = user.Id,
                AccessToken = accessToken
            };
        }

        private string GenerateJwtTokenAsync(Account account)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecretsecretsecretsecretsecret"));
            var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "ibcl.com",
                audience: "ibcl.com",
                expires: DateTime.Now.AddMinutes(30),
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, account.UserName), new Claim(ClaimTypes.Email, !string.IsNullOrEmpty(account?.Email) ? account?.Email : "test@gmail.com"),
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())
                },
                signingCredentials: signInCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
