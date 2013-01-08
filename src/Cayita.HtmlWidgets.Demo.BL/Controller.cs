using Cayita.HtmlWidgets.Demo.DAL;
using Cayita.HtmlWidgets.Demo.BLRules;
using ServiceStack.ServiceHost;

namespace Cayita.HtmlWidgets.Demo.BL
{
	public partial class Controller
	{


		public Controller (RepositoryClient client, Mailer mailer)
		{
			Client= client;
			Mailer= mailer;
			LoadRules();
		}

		protected internal RepositoryClient Client {get;set;}


		protected Mailer Mailer {get;  set;}

		public Rules Rules {get; private set;}


		public void LoadRules(){
			Rules = new Rules();
		}

		public void InitRepo(){
			Client.Execute(proxy=>{
				Users.CreateTable(proxy);
				Users.Populate(proxy);
				Authors.CreateTable(proxy);
				Sales.CreateTable(proxy);
				Sales.Populate(proxy);
			});
		}

		protected bool IsCayita (BLRequest request)
		{
			var httpReq= request.RequestContext.Get<IHttpRequest>();

			bool cayita=false;
			bool.TryParse(httpReq.QueryString["cayita"], out cayita);
			return cayita;
		}

		/*
		protected string MailTarget (BLRequest request)
		{
			var httpReq= request.RequestContext.Get<IHttpRequest>();

			return httpReq.GetParam("Email") ?? httpReq.QueryString.Get("Email");

		}
		*/

	}
}

