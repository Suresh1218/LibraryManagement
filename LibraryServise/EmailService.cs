using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServise
{
    public class EmailService : IEmaiService
    {
        private readonly string smtpAddress = "smtp.gmail.com";
        private readonly int portNumber = 587;
        private readonly bool enableSSL = true;
        private readonly string emailFrom = "suresh@thinkbridge.in";
        private readonly string password = "Suresh1218";

        public bool SendOrderEmail(string userMailId,string _userName,UserCart order,int orderid)
        {
            string userName = _userName;
            string emailTo = userMailId;

            // Here you can put subject of the mail
            string subject = "Order Placed Successfully";
            // Body of the mail
            string body = "<div style='border: medium solid grey; width: 500px; height: 266px;font-family: arial,sans-serif; font-size: 17px;'>";
            body += "<h3 style='background-color: blueviolet; margin-top:0px;'>Your Order</h3>";
            body += "<br />";
            body += "Dear " + userName + ",";
            body += "<br />";
            body += "<p>Your order id is - " + orderid + "</p>";
            body += "<br />";
            body += "<p>Your total amount is - "+ order.TotalAmount +"</p>";
            body += "<br />";
            body += "<p>";
            body += "Your Order will Shiped Shortly </p>";
            body += " <br />";
            body += "Thanks,";
            body += "<br />";
            body += "<b>The Team - British Library</b>";
            body += "</div>";
            try {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
    }

    public interface IEmaiService
    {
        bool SendOrderEmail(string userMailId,string userNAme, UserCart cart, int orderid);
    }
}
