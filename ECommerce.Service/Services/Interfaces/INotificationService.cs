namespace ECommerce.Service.Services.Interfaces
{
    public interface INotificationService
    {
        bool SendEmail(string from, string to, string subject, string body);
        bool SendSms(string phoneNumber, string message);
    }
}