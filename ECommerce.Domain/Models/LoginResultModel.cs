namespace ECommerce.Domain.Models {

    public enum LoginResultType {
        Default,
        TwoFactor,
        LockOut
    }

    public class LoginResultModel {
        public bool IsSuccess { get; set; }
        public LoginResultType Type { get; set; }
        public string Message { get; set; }
    }
}