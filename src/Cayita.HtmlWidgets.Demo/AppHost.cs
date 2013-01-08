using System;
using ServiceStack.WebHost.Endpoints;
using Cayita.HtmlWidgets.Demo.Interface;
using Funq;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.Logging;
using Cayita.HtmlWidgets.Demo.BL;
using Cayita.HtmlWidgets.Demo.DAL;
using Cayita.Repository;
using ServiceStack.Common.Web;
using ServiceStack.Configuration;


namespace Cayita.HtmlWidgets.Demo
{
	public class AppHost:AppHostBase
	{
		static ILog log;

		public AppHost ():base("Cayita.HtmlWidgets - Demo", typeof(UserService).Assembly)
		{
			LogManager.LogFactory = new ConsoleLogFactory();
			log = LogManager.GetLogger(typeof (AppHost));

		}

		public override void Configure(Container container)
		{
			//Permit modern browsers (e.g. Firefox) to allow sending of any REST HTTP Method
			base.SetConfig(new EndpointHostConfig
			{
				GlobalResponseHeaders =
					{
						{ "Access-Control-Allow-Origin", "*" },
						{ "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, PATCH" },
					},
				  DefaultContentType = ContentType.Json 
			});
							
									
			ConfigureApp(container);
			log.InfoFormat("AppHost Configured: " + DateTime.Now);
		}

		void ConfigureApp(Container container)
		{
			var appSettings = new ConfigurationResourceManager();
                    
            string smtpServer= appSettings.Get("MAILGUN_SMTP_SERVER", "localhost");
			string smtpLogin= appSettings.Get("MAILGUN_SMTP_LOGIN", "username");
			string smtpPassword= appSettings.Get("MAILGUN_SMTP_PASSWORD", "PASSWORD");
			int smtpPort= appSettings.Get("MAILGUN_SMTP_PORT", 587);

			Mailer mailer = new Mailer(smtpServer, smtpPort, smtpLogin, smtpPassword);

			IRepository rp = new MemRepo();
			RepositoryClient rc = new RepositoryClient(rp);
			Controller controller = new Controller(rc,mailer);
			controller.InitRepo();
            container.Register<Controller>( controller );
            

            
						
		}



	}
}

