using System;
using System.Web;

namespace Cayita.HtmlWidgets.Demo
{
	public class Global : HttpApplication
	{
		
		protected virtual void Application_Start (Object sender, EventArgs e)
		{
			AppHost appHost= new AppHost();
			appHost.Init();
		}
		
		protected virtual void Session_Start (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_BeginRequest (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_EndRequest (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_AuthenticateRequest (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_Error (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Session_End (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_End (Object sender, EventArgs e)
		{
		}
	}
}

