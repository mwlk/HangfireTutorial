using System;
using System.Net.Mail;

namespace HangfireTutorial.Service
{
    public class ErrorNotifyService
    {
        private string _sender = string.Empty;
        private string _receptor = string.Empty;
        private string _server = string.Empty;
        private string _password = string.Empty;
        private int _port = 0;

        public ErrorNotifyService()
        {
            _sender = System.Configuration.ConfigurationManager.AppSettings.Get("Username");
            _receptor = System.Configuration.ConfigurationManager.AppSettings.Get("Receptor");
            _server = System.Configuration.ConfigurationManager.AppSettings.Get("Server");
            _password = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
            _port = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("Port"));
        }

        public void AlertException(Exception e)
        {
            try
            {

                MailMessage mailMessage = new MailMessage(_sender, _receptor, "Notificaciones FECAC Excepción", e.Message);

                mailMessage.IsBodyHtml = true;


                SmtpClient smtpClient = new SmtpClient(_server);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Host = _server;
                smtpClient.Port = _port;
                smtpClient.Credentials = new System.Net.NetworkCredential(_sender, _password);

                smtpClient.Send(mailMessage);

                smtpClient.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}