using InsurTech.Core.Service;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.Extensions.Logging;

namespace InsurTech.Service
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string messageContent)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("InsurTech", _configuration["EmailSettings:FromEmail"]));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = messageContent,
                    TextBody = "Please view this email in an HTML-compatible email client."
                };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                   
                    await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"],
                                             int.Parse(_configuration["EmailSettings:Port"]),
                                             SecureSocketOptions.StartTls);

                    
                    await client.AuthenticateAsync(_configuration["EmailSettings:Username"],
                                                   _configuration["EmailSettings:Password"]);

                   
                    await client.SendAsync(message);
                    

                    await client.DisconnectAsync(true);
                }
            }
           
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while sending email.");
                throw;
            }
        }

        public async Task SendPasswordResetEmail(string toEmail, string resetUrl)
        {
            var subject = "Reset your password";
            var message = $"<p>Please reset your password by <a href='{resetUrl}'>clicking here</a>.</p>";
            await SendEmailAsync(toEmail, subject, message);
        }

        public async Task SendConfirmationEmail(string toEmail, string confirmationUrl)
        {
            var subject = "Confirm your email";
            var message = $"<h1>Welcome to InsurTech</h1><p>Please confirm your email by <a href='{confirmationUrl}'>clicking here</a>.</p>";
            await SendEmailAsync(toEmail, subject, message);
        }
    }
}
