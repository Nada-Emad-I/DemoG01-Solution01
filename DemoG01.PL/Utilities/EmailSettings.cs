using System.Net;
using System.Net.Mail;

namespace DemoG01.PL.Utilities
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("nada3mad3@gmail.com", "wlynihiyoblsvcoe");
            Client.Send("nada3mad3@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
