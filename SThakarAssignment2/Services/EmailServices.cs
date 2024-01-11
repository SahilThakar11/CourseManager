using System.Net;
using System.Net.Mail;

namespace SThakarAssignment2.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;
        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //To send the mail to students
        public void SendEmail(string to, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            //To set up smtp client
            using (var smtpClient = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"])))
            {
                //Smtp username password and method
                smtpClient.Credentials = new NetworkCredential(smtpSettings["UserName"], smtpSettings["Password"]);
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                var message = new MailMessage(new MailAddress(smtpSettings["UserName"],"Course Manager App"),new MailAddress(to,to))
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                smtpClient.Send(message);
            }
        }
    }
}
