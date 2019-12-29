namespace ECommerce.Domain.Models {
    public enum RegisterResultType {
        Default,
        RequireConfirmedAccount,

    }
    public class RegisterResultModel {
        public bool IsSuccess { get; set; }
        // public RegisterResultType Type { get; set; }
        public string Message { get; set; }
    }
}