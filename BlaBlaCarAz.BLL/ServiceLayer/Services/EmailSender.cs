using BlaBlaCarAz.BLL.HelperClasses;
using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.ServiceLayer.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));
            message.Subject = subject;
            message.Body = htmlMessage;
            message.IsBodyHtml = true;
            //if (model.Upload != null && model.Upload.ContentLength > 0)
            //{
            //    message.Attachments.Add(new Attachment(model.Upload.InputStream, Path.GetFileName(model.Upload.FileName)));
            //}
            var smtp = new SmtpClient(_emailSettings.Host)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true,
            };
            
                message.From = new MailAddress(_emailSettings.Username);
                ThreadPool.QueueUserWorkItem(new WaitCallback(async delegate (object state) { await smtp.SendMailAsync(message);smtp.Dispose(); }), null);
            


        }
    }
}
