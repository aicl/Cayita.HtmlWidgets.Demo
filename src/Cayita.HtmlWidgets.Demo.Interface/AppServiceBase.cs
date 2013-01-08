using ServiceStack.ServiceInterface;
using Cayita.HtmlWidgets.Demo.BL;

namespace Cayita.HtmlWidgets.Demo.Interface
{
	public class AppServiceBase:Service
	{
		public Controller Controller {get;set;}

		protected BLRequest BLRequest {
			get{
				return new BLRequest{
					RequestContext=RequestContext
				};
			}
		}
	}




}

