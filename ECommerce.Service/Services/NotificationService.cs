using System;
using System.Net.Mail;
using ECommerce.Service.Services.Interfaces;

namespace ECommerce.Service.Services
{
    public class NotificationService : INotificationService
    {
        public bool SendEmail(string from, string to, string subject, string body)
        {
            string server = string.Empty;
            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;
            SmtpClient client = new SmtpClient(server);
            // Credentials are necessary if the server requires the client 
            // to authenticate before it will send email on the client's behalf.
            client.UseDefaultCredentials = true;

            try
            {
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool SendSms(string phoneNumber, string message)
        {
            throw new System.NotImplementedException();
        }
    }
}