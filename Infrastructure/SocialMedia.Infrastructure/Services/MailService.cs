using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Configuration;
using SocialMedia.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new string[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] to, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            mail.Subject = subject;
            mail.Body = body;
            foreach (var item in to)
                mail.To.Add(item);
            mail.From = new(_configuration["Mail:Username"], "Connectify Social Media", Encoding.UTF8);

            //Send this mail
            SmtpClient smtpClient = new();
            smtpClient.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Host = _configuration["Mail:Host"];

            await smtpClient.SendMailAsync(mail);
        }
    }
}
