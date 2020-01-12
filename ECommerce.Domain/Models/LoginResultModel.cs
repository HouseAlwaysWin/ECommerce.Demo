namespace ECommerce.Domain.Models
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