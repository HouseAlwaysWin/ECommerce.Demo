namespace ECommerce.Domain.Models.Account
{
    public enum RegisterStatus
    {
        Success,
        Fail,
        RequireConfirmedAccount,

    }
    public class RegisterResultModel
    {
        public RegisterStatus Status { get; set; }
        public string Message { get; set; }
    }
}