using Ecaq.Data;
using Ecaq.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Ecaq.Services.Repositories
{
    public class EmailSenderRepository : IEmailSender<ApplicationUser>
    {
        private readonly IEmailSenderApp _emailSender;

        public EmailSenderRepository(IEmailSenderApp emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            //await SendEmailAsHtmlAsync(user.Name, email, "Forgot Password", "", "ForgotPassword", "", "", resetLink);
        }

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            throw new NotImplementedException();
        }

        public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            await _emailSender.SendEmailAsHtmlAsync(user.Name, email, "Forgot Password", "", "ForgotPassword", "", "", resetLink);
        }
    }
}
