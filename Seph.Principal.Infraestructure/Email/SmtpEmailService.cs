using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Seph.Principal.Application.Common.Interfaces;

namespace Seph.Principal.Infraestructure.Email
{
    public sealed class SmtpEmailService(IConfiguration configuration) : IEmailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody, CancellationToken cancellationToken = default)
        {
            var smtpHost = configuration["Email:SmtpHost"]!;
            var smtpPort = int.Parse(configuration["Email:SmtpPort"]!);
            var senderEmail = configuration["Email:SenderEmail"]!;
            var senderName = configuration["Email:SenderName"];
            var username = configuration["Email:Username"]!;
            var password = configuration["Email:Password"]!;

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            using var message = new MailMessage
            {
                From = new MailAddress(senderEmail, senderName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            message.To.Add(toEmail);

            await client.SendMailAsync(message, cancellationToken);
        }
    }
}