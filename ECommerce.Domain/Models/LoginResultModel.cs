namespace ECommerce.Domain.Models {

    public enum LoginStatus {
        Success,
        Fail,
        TwoFactor,
        LockOut
    }

    public class LoginResultModel {
        public bool IsSuccess { get; set; }
        public LoginStatus Status { get; set; }
        public string Message { get; set; }
    }
}