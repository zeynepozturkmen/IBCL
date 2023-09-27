using IBCL.Application.Common.Models;

namespace IBCL.Application.Common.Interfaces
{
    public interface IAccountService
    {
        Task<AccountRegistrationModel> Register(AccountRegistrationModel userModel);
    }
}
