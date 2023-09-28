using System.ComponentModel.DataAnnotations;

namespace IBCL.Application.Common.Models.Request.Account
{
    public class AccountRegistrationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string PhoneCode { get; set; }
        public string PhoneNumber { get; set; }

        public string IdentityNumber { get; set; }
        public string TelegramChatId { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
