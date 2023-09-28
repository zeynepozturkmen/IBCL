using IBCL.Application.Common.Models.Response.Account;
using IBCL.Application.Common.Models.Response.Accounts;

namespace IBCL.Application.Common.Interfaces
{
    public interface IAccountService
    {
        Task<AccountRegistrationModel> RegisterAsync(AccountRegistrationModel userModel);
        Task<AccountTokenDto> LoginAsync(AccountLoginModel request);
    }
}
