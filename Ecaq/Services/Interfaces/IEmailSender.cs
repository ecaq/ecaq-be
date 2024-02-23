namespace Ecaq.Services.Interfaces;
public interface IEmailSender
{
    ValueTask<string> SendEmailAsHtmlAsync(string name, string sendToThisEmail, string subject, string message, string htmlTemplate = "", string optparam1 = "", string optparam2 = "", string link = "");
    ValueTask<string> SendEmailAsync(string name, string sendToThisEmail, string subject, string message, bool isHtmlBody = false);

}

