using IBCL.Application.Common.Models;

namespace IBCL.Application.Common.Interfaces
{
    public interface IAccountService
    {
        Task<AccountRegistrationModel> RegisterAsync(AccountRegistrationModel userModel);
        Task<AccountTokenDto> LoginAsync(AccountLoginModel request);
    }
}
