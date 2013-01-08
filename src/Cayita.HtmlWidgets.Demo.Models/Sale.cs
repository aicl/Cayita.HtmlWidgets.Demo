using ServiceStack.DesignPatterns.Model;
using System;
using ServiceStack.ServiceHost;

namespace Cayita.HtmlWidgets.Demo.Models
{
	public class Sale:IHasIntId
	{
		public Sale ()
		{
		}

		public int Id { get; set;}
		public string Vendor {get;set;}
		public DateTime Date {get;set;}
		public decimal Price {get;set;}
		public string Product {get;set;}
		public string Customer {get;set;}
	}

	public interface IHasVendor{
		string Vendor {get;set;}
	}


	[Route("/Sale/read","POST,GET")]
	public class GetSales: IHasVendor,  IReturn<BLResponse<Sale>>{

		public string Vendor {get;set;}

	}

	[Route("/Sale/send","POST,GET")]
	public class SendSales: IHasVendor,  IReturn<BLResponse<Sale>>{

		public string Vendor {get;set;}
		public string Email {get;set;}

	}

	public class SalesByVendor{
		public string Vendor {get;set;}
		public decimal Total {get;set;}
	}

}

