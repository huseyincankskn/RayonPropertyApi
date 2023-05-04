using Communication.Entities;
using Entities.VMs.User;
using Helper.AppSetting;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;
using System.Net;
using MimeKit;
using Communication.EmailManager.Abstract;
using Entities.VMs;
using System.Globalization;
using System.Text;
using Helper.Helpers.HtmlTableHelper;
using Entities.Concrete;
using DataAccess.Abstract.EntityFramework.Repository;

namespace Communication.EmailManager.Concrete
{
    public class EmailManager : IEmailManager
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IContactRequestRepository _contactRequestRepository;

        public EmailManager(IWebHostEnvironment webHostEnvironment,
                            IContactRequestRepository contactRequestRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _contactRequestRepository = contactRequestRepository;
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
                    Password = "Tf5FyFCYA$.1"
                }
            }
        };

        public void SendForgotPasswordEmail(ForgotPasswordVm mailModel)
        {
            var dealerInfo = GetDealerInfo(0);

            var pathToFile = _webHostEnvironment.WebRootPath + "/email-content.html";
            var builder = new BodyBuilder();
            var logoUrl = AppSettings.AdminPanelUrl + "/Logo/" + dealerInfo.Logo;
            using (StreamReader sourceReader = File.OpenText(pathToFile))
            {
                builder.HtmlBody = sourceReader.ReadToEnd();
            }

            var host = dealerInfo.FrontEndUrl + "/reset-password/" + mailModel.PsrGuid;
            var htmlBody = builder.HtmlBody.Replace("{{LINK}}", host).Replace("{{LOGO}}", logoUrl);

            var mailMessage = new MailMessage(dealerInfo.FromMail, mailModel.Email)
            {
                Subject = $"{dealerInfo.Name} Update Password",
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

        public void SendContactRequestMail(ContactRequestVm contactRequestVm)
        {
            var dealerInfo = GetDealerInfo(0);
            var toMail = AppSettings.ContactMail;

            var pathToFile = _webHostEnvironment.WebRootPath + "/contact-request-mail-template.html";
            var builder = new BodyBuilder();
            var logoUrl = AppSettings.BackEndUrl + "/Logo/" + dealerInfo.Logo;
            using (StreamReader sourceReader = File.OpenText(pathToFile))
            {
                builder.HtmlBody = sourceReader.ReadToEnd();
            }

            StringBuilder sb = new StringBuilder();
            using (HtmlTableHelper.Table table = new HtmlTableHelper.Table(sb, id: "tablestyle"))
            {
                table.StartBody();

                if (!string.IsNullOrEmpty(contactRequestVm.Name))
                {

                    using (var tr = table.AddRow(classAttributes: "row"))
                    {
                        tr.AddCell("İsim", "", "tablestyle");
                        tr.AddCell(contactRequestVm.Name, "", "tablestyle");
                    }
                }

                if (!string.IsNullOrEmpty(contactRequestVm.PhoneNumber))
                {
                    using (var tr = table.AddRow(classAttributes: "row"))
                    {
                        tr.AddCell("Telefon", "", "tablestyle");
                        tr.AddCell(contactRequestVm.PhoneNumber, "", "tablestyle");
                    }
                }

                if (!string.IsNullOrEmpty(contactRequestVm.Description))
                {
                    using (var tr = table.AddRow(classAttributes: "row"))
                    {
                        tr.AddCell("Mesaj", "", "tablestyle");
                        tr.AddCell(contactRequestVm.Description, "", "tablestyle");
                    }
                }

                using (var tr = table.AddRow(classAttributes: "row"))
                {
                    tr.AddCell("Email", "", "tablestyle");
                    tr.AddCell(contactRequestVm.Email, "", "tablestyle");
                }

                table.EndBody();
            }

            var finishedTable = sb.ToString();


            string htmlBody = builder.HtmlBody.Replace("{{TABLE}}", finishedTable).Replace("{{LOGO}}", logoUrl);

            var mailMessage = new MailMessage(dealerInfo.FromMail, toMail)
            {
                Subject = $"WebSite İletişim Talebi",
                IsBodyHtml = true,
                Body = htmlBody
            };
            EmailInformation(mailMessage, dealerInfo);

            var contactRequestEntity = new ContactRequest()
            {
                Name = StringControlWithLength(contactRequestVm.Name, 100),
                Email = StringControlWithLength(contactRequestVm.Email, 100),
                PhoneNumber = StringControlWithLength(contactRequestVm.PhoneNumber, 20),
                Description = StringControlWithLength(contactRequestVm.Description, 500),
                AddUserId = Guid.Empty
            };

            _contactRequestRepository.Add(contactRequestEntity);
        }

        public string StringControlWithLength(string? input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                var output = input.Length > maxLength ? input[..maxLength] : input;
                return output;
            }
        }

        public void SendNewPasswordEmail(ForgotPasswordVm mailModel)
        {
            var dealerInfo = GetDealerInfo(0);

            var pathToFile = _webHostEnvironment.WebRootPath + "/new-password-email.html";
            var builder = new BodyBuilder();
            var logoUrl = AppSettings.AdminPanelUrl + "/Logo/" + dealerInfo.Logo;
            using (StreamReader sourceReader = File.OpenText(pathToFile))
            {
                builder.HtmlBody = sourceReader.ReadToEnd();
            }

            var host = dealerInfo.FrontEndUrl + "/new-password/" + mailModel.PsrGuid;
            var htmlBody = builder.HtmlBody.Replace("{{LINK}}", host).Replace("{{LOGO}}", logoUrl);

            var mailMessage = new MailMessage(dealerInfo.FromMail, mailModel.Email)
            {
                Subject = $"{dealerInfo.Name} Register",
                IsBodyHtml = true,
                Body = htmlBody
            };
            EmailInformation(mailMessage, dealerInfo);
        }
    }
}