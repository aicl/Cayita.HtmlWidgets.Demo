using Cayita.HtmlWidgets.Demo.Models;

namespace Cayita.HtmlWidgets.Demo.Interface
{
	public class SaleService:AppServiceBase
	{
		public BLResponse<Sale> Post ( SendSales request)
		{
			return  Controller.SendSalesRepo(request, BLRequest);
		}

		public BLResponse<Sale> Get (SendSales request)
		{
			return Post (request);
		}

	}
}

