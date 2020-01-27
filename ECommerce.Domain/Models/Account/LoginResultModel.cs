namespace ECommerce.Domain.Models.Account
{

    public enum LoginStatus
    {
        Success,
        Fail,
        TwoFactor,
        LockOut
    }

    public class LoginResultModel
    {
        public LoginStatus Status { get; set; }
        public string Message { get; set; }
    }
}