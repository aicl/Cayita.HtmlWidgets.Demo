using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System;

namespace Cayita.HtmlWidgets.Demo.DAL
{
	public class Mailer
	{

		SmtpClient SmtpServer {get ;set;}
		
		public Mailer ( string server, int port, string user, string password,bool ssl=true)
		{
						
			SmtpServer = new SmtpClient(server);
			SmtpServer.Port = port;
			SmtpServer.Credentials = 
				new NetworkCredential(user, password);
			SmtpServer.EnableSsl = ssl;
			ServicePointManager.ServerCertificateValidationCallback =
				delegate(object s, X509Certificate certificate,
				X509Chain chain, SslPolicyErrors sslPolicyErrors)
				{ return true; };
			
		}
		
		
		public void Send(Action<MailMessage> config ){
			MailMessage message = new MailMessage();
			config(message);
			SmtpServer.Send(message);
		}

	}
}

