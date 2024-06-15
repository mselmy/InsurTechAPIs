using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
        Task SendPasswordResetEmail(string toEmail, string resetUrl);
        Task SendConfirmationEmail(string toEmail, string confirmationUrl);
    }
}
