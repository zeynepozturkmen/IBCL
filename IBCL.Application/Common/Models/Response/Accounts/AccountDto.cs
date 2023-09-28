namespace IBCL.Application.Common.Models.Response.Accounts
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelegramChatId { get; set; }
    }
}
