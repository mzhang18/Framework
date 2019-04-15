using System;
using System.Linq;
using System.Net.Mail;
using System.Configuration;

namespace Dow.SSD.Framework.Common
{
    public static class EmailHelper
    {
        public static void SendEmail(MailMessage message)
        {
            var mailFrom = ConfigurationManager.AppSettings["MailFrom"];
            if (string.IsNullOrEmpty(mailFrom))
            {
                throw new NullReferenceException("Please add MailFrom settings in web.config!");
            }
            using (var mail = new MailMessage())
            {
                message.To.ToList().ForEach(item => mail.To.Add(item));
                if (message.CC != null)
                    message.CC.ToList().ForEach(item => mail.CC.Add(item));

                mail.Subject = message.Subject;
                mail.Body = message.Body;
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(mailFrom);

                using (var smtpClient = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SmtpServer"]))
                {
                    smtpClient.Send(mail);
                }
            }
        }
    }
}
