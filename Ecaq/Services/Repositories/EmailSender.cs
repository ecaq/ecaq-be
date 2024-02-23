using Ecaq.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Ecaq.Services.Repositories
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private IWebHostEnvironment _hostingEnv;
        private readonly IConfiguration _config;
        public EmailSender(IWebHostEnvironment hostingEnv, IConfiguration config)
        {
            _hostingEnv = hostingEnv;
            _config = config;
        }

        public async ValueTask<string> SendEmailAsHtmlAsync(string name, string email, string subject, string message, string htmlTemplate, string optparam1 = "", string optparam2 = "", string link = "")
        {
            //calling for creating the email body with html template

            string bodyMessage = CreateEmailBody(name, email, htmlTemplate, optparam1, optparam2, link);

            var result = await SendEmailAsync(name, email, subject, message, true);

            return await ValueTask.FromResult(result);
        }

        private string CreateEmailBody(string name, string email, string htmlTemplate, string optparam1 = "", string optparam2 = "", string link = "")
        {
            string webRootPath = _hostingEnv.WebRootPath;

            string body = string.Empty;
            //using streamreader for reading my htmltemplate
            if(string.IsNullOrEmpty(htmlTemplate))
            {
                htmlTemplate = "Default";
            }

            using (StreamReader reader = new StreamReader(webRootPath + "/emailtemplate/" + htmlTemplate + ".html"))
            {
                body = reader.ReadToEnd();
            }
            if (!string.IsNullOrEmpty(email))
            {
                body = body.Replace("{email}", email); //replacing the required things
            }
            if (!string.IsNullOrEmpty(name))
            {
                body = body.Replace("{Name}", name); //replacing the required things
            }
            if (!string.IsNullOrEmpty(link))
            {
                body = body.Replace("{Link}", link);
            }
            if (!string.IsNullOrEmpty(optparam1))
            {
                body = body.Replace("{prm1}", optparam1);
            }
            if (!string.IsNullOrEmpty(optparam2))
            {
                body = body.Replace("{prm2}", optparam2);
            }
            return body;

        }

        public ValueTask<string> SendEmailAsync(string name, string email, string subject, string message, bool isBodyHtml = false)
        {
            string msgToReturn = "";
            MailMessage mail = new
                (
                    _config["Smtp:fromEmail"]?.ToString()!, email, subject, message
                );
            mail.IsBodyHtml = isBodyHtml;

            try
            {
                using (var smtp = new SmtpClient())
                {

                    //// Send-MailMessage -To "felixm.jr01@gmail.com" -From "customerservice@aabqatar.com" -Subject "Testing" -Body "Testing" -Credential (Get-Credential) -SmtpServer "mail.aabqatar.com" -Port 587 
                    //// System.Net.Mail.SmtpFailedRecipientException: Mailbox unavailable. 
                    //// The server response was: 5.7.54 SMTP; Unable to relay recipient in non-accepted domain\r\n   at System.Net.Mail.SmtpTransport.SendMail(MailAddress sender, MailAddressCollection recipients, String deliveryNotify, Boolean allowUnicode, SmtpFailedRecipientException& exception)\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at AABLexus.AccountServices.Email.SmtpSettings(MailMessage objMail) in D:\\AAB Web Project\\AABLexusQatar\\AABLexus\\AABLexus\\AccountServices\\MailSettings.cs:line 63"	string


                    //// Localhost
                    //smtp.Host = "172.30.52.51";
                    //smtp.Port = 25;

                    // Live
                    smtp.Host = _config["Smtp:host"]?.ToString()!;
                    smtp.Port = int.Parse(_config["Smtp:prt"]?.ToString()!);
                    smtp.Credentials = new NetworkCredential(_config["Smtp:usr"]?.ToString(), _config["Smtp:pwd"]?.ToString());

                    smtp.Send(mail);
                    mail = null!;
                }

                msgToReturn = "success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                msgToReturn = $"Sending email failed, {ex.Message}";
            }
            return ValueTask.FromResult(msgToReturn);
        }

    }
}
