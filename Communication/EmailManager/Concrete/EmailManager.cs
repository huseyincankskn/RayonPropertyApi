using Communication.Entities;
using Entities.VMs.User;
using Helper.AppSetting;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;
using System.Net;
using MimeKit;
using Communication.EmailManager.Abstract;

namespace Communication.EmailManager.Concrete
{
    public class EmailManager : IEmailManager
    {
        private readonly IWebHostEnvironment _webHostEnvironment;



        public EmailManager(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        private readonly Dictionary<int, SendMailInfo> _sendMailInfos = new()
        {
            {
                0,
                new SendMailInfo
                {
                    Id = 0,
                    Name = "RayonProperty",
                    Logo = "Rayon_Property_Logo_EN@4x.png",
                    FrontEndUrl  = "www.rayonproperty.com",


                    FromMail = "info@rayonproperty.com",
                    Host = "srvm11.trwww.com",
                    Port = 587,
                    UseSsl = true,
                    UserName = "info@rayonproperty.com",
                    Password = "U!3h!Rrn"
                }
            }
        };

        public void SendForgotPasswordEmail(ForgotPasswordVm mailModel)
        {
            var dealerInfo = GetDealerInfo(0);

            var pathToFile = _webHostEnvironment.WebRootPath + "/email-content.html";
            var builder = new BodyBuilder();
            var logoUrl = AppSettings.BackEndUrl + "/Logo/" + dealerInfo.Logo;
            using (StreamReader sourceReader = File.OpenText(pathToFile))
            {
                builder.HtmlBody = sourceReader.ReadToEnd();
            }

            var host = dealerInfo.FrontEndUrl + "/reset-password/" + mailModel.PsrGuid;
            var htmlBody = builder.HtmlBody.Replace("{{LINK}}", host).Replace("{{LOGO}}", logoUrl);

            var mailMessage = new MailMessage(dealerInfo.FromMail, mailModel.Email)
            {
                Subject = $"{dealerInfo.Name} Şifre Güncelleme",
                IsBodyHtml = true,
                Body = htmlBody
            };
            EmailInformation(mailMessage, dealerInfo);
        }

        private static void EmailInformation(MailMessage mail, SendMailInfo sendMailInfo)
        {
            try
            {
                var client = new SmtpClient
                {
                    Port = sendMailInfo.Port,
                    EnableSsl = sendMailInfo.UseSsl,
                    Host = sendMailInfo.Host,
                    Timeout = 20000
                };

                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(sendMailInfo.UserName, sendMailInfo.Password);

                client.Send(mail);
            }
            catch (Exception)
            {
                // To do
            }
        }

        private SendMailInfo GetDealerInfo(int sendMailInfoId)
        {
            if (_sendMailInfos.TryGetValue(sendMailInfoId, out var dealerInfo))
            {
                return dealerInfo;
            }
            return _sendMailInfos[0];
        }
    }
}