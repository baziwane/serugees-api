using System.Diagnostics;

namespace Serugees.Api.Services
{
    public class LocalMailService
    {
        private string _mailTo = "admin@serugees.com";
        private string _mailFrom = "no-reply@serugees.com";

        public void Send(string subject, string message)
        {
            // send mail - output to debug window
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}