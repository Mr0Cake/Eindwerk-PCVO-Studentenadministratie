using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mime;

namespace MessageProvider
{
    public static class SendMessage
    {
        public const string smtpServer = "192.168.18.144";
        public const string Domain = "pcvo4.be";
        public const int port = 1234;
        public const string Username = "Mailuser3";
        public const string Password = "P@ssw0rd";
        public static string From = Username + "@" + Domain;

        public const string smtpGmail = "smtp.gmail.com";
        public const string GmailPassword = "Pcvogr4.";
        public const string GmailFrom = "secretariaat.pcvo4@gmail.com";
        

        public const int GmailPort = 587;

        /// <summary>
        /// Stuur Exchange Mail, 
        /// 30 seconden timeout, bij problemen gebruik TaskFactory.StartNew(() => {Mail("email", "text");});
        /// Dit verzend een mail in een andere thread waardoor uw programma niet wordt opgehouden.
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool Mail(string mail, string text, string filename = "", string contenttype = "",  MemoryStream attachment = null )
        {
            try
            {
                //SmtpClient SmtpServer = new SmtpClient(smtpServer, port);
                SmtpClient SmtpServer = new SmtpClient(smtpServer);
                NetworkCredential basicCredential = new NetworkCredential("pcvo4\\"+Username, Password, Domain);
                SmtpServer.Host = smtpServer;
                MailAddress fromAddress = new MailAddress(From);
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = basicCredential;
                MailMessage message = new MailMessage(From, mail, "Administratie PCVO", text.Replace("\r\n", "<br />"));
                //SmtpServer.Timeout = (60 * 1 * 500); //30s



                if (attachment != null && !string.IsNullOrEmpty(filename) && !string.IsNullOrEmpty(contenttype))
                {
                    ContentType ctype = new ContentType(contenttype);
                    message.Attachments.Add(new Attachment(attachment, filename, contenttype));
                }


                message.IsBodyHtml = true;
                //SmtpServer.EnableSsl = true;
                SmtpServer.Send(message);
                if (attachment != null)
                    attachment.Dispose();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                try
                {
                    //SmtpClient SmtpServer = new SmtpClient(smtpGmail);
                    //NetworkCredential basicCredential = new NetworkCredential(GmailFrom, Password);
                    //SmtpServer.Host = smtpServer;
                    ////MailAddress fromAddress = new MailAddress(GmailFrom, "Secretariaat PCVO");
                    //SmtpServer.UseDefaultCredentials = false;
                    //SmtpServer.Credentials = basicCredential;
                    //MailMessage message = new MailMessage(GmailFrom, mail, "Administratie PCVO", text.Replace("\r\n", "<br />"));
                    ////SmtpServer.Timeout = (60 * 1 * 500); //30s
                    //SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //message.IsBodyHtml = true;
                    //SmtpServer.EnableSsl = true;
                    //SmtpServer.Send(message);
                    var fromAddress = new MailAddress(GmailFrom, "Secretariaat");
                    var toAddress = new MailAddress("verheyenkurt90@hotmail.com", "Kurt");
                    const string fromPassword = GmailPassword;
                    const string subject = "Secretariaat PCVO4";
                    string body = text;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(fromAddress.Address, GmailPassword),
                        Timeout = 20000
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return false;
            }
        }


        public static bool SMS(string number, string text)
        {
            return false;
        }


    }
}
