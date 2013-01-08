using System.Linq;
using Cayita.HtmlWidgets.Common;
using Cayita.HtmlWidgets.Demo.Models;
using Cayita.HtmlWidgets.Demo.DAL;
using System.Collections.Generic;
using ServiceStack.Common;
using ServiceStack.Text;
using System.Net.Mail;

namespace Cayita.HtmlWidgets.Demo.BL
{
	public  partial class Controller
	{

		decimal targetByVendor=85000;

		public BLResponse<Sale> GetSales(GetSales request, BLRequest blRequest){
			var sales = GetSalesFromRepo(request, blRequest);

			var r = new BLResponse<Sale>();
			if( !IsCayita(blRequest))
			{
				r.Result= sales;
			}
			else
			{
				r.Html = BuildSalesGrid(sales).ToString();
			}
		
			return r;

		}

		public BLResponse<Sale> SendSalesRepo(SendSales request, BLRequest blRequest){

			var res = new BLResponse<Sale>();

			var mailTarget = request.Email; //MailTarget(blRequest);

			var sales = GetSalesFromRepo(request, blRequest);

			var gsbv = (from g in sales group g by g.Vendor into r 
			select new SalesByVendor {
				Vendor=  r.Key, 
				Total= r.Sum(p=>p.Price)
			}).OrderByDescending(f=>f.Total).ToList();

			HtmlDiv div = new HtmlDiv();

			var gridSalesByVendor =BuildSalesByVendorGrid(gsbv, mailTarget);

			div.AddHtmlTag(gridSalesByVendor);
			div.AddHtmlTag( new HtmlLineBreak());

			foreach(var sv in gsbv ){
				div.AddHtmlTag(( BuildDetails(sv.Vendor, sales.Where(f=>f.Vendor== sv.Vendor).ToList(), mailTarget)));
				div.AddHtmlTag( new HtmlLineBreak());
			}

			if(mailTarget.IsNullOrEmpty()){
				res.Html= div.ToString();
				return res;
			}

			SendSalesRepoByMail(div, mailTarget);
			return res;
		}


		HtmlGrid<SalesByVendor> BuildSalesByVendorGrid(List<SalesByVendor> sbv, string mailTarget=null){

			HtmlGrid<SalesByVendor> g = new HtmlGrid<SalesByVendor>();
			g.DataSource= sbv;

			if(! mailTarget.IsNullOrEmpty())
				g.GridStyle= new GreyGridStyle();  // css = Inline Style
			else
				g.Css = new CayitaGridGrey();      // css = Style Sheet

			g.Title= "Sales by Vendor";
			g.Id="sales-by-vendor";
			g.FootNote= "Target by vendor : {0}".Fmt(targetByVendor.Format());
			g.AddGridColum( c=> {
				c.HeaderText="Vendor";
				c.CellRenderFunc=(row,index,dt)=> row.Vendor;
				c.FooterRenderFunc= ()=>"Total Sales ===>";

			});

			g.AddGridColum( c=> {
				c.HeaderText="Total";
				c.CellRenderFunc=(row,index,dt)=> row.Total.Format();
				c.CellStyle.TextAlign="right";
				c.FooterRenderFunc=()=> sbv.Sum(f=>f.Total).Format();
				c.FooterCellStyle.TextAlign="right";
			});

			g.AddGridColum( c=> {
				c.CellRenderFunc=(row,index,dt)=>{

					Cayita.HtmlWidgets.Core.TagBase r ;
					if(mailTarget.IsNullOrEmpty())
					{
						r= new HtmlIcon(){Class="icon-circle"};
					}
					else{
						r= new HtmlParagragh(){Text= row.Total>=targetByVendor?"+":"x" };
					}
					r.Style.Color= row.Total>targetByVendor?"green":"red";
					return r;

				};
			});

			g.AddGridColum( c=> {
				c.CellRenderFunc=(row,index,dt)=>{
					HtmlLink link = new HtmlLink();
					link.Url= "#"+row.Vendor.ReplaceAll(" ","-");
					link.Text="see details";
					return link.ToString();
				};
			});

			return g;
		}


		HtmlGrid<Sale> BuildDetails(string vendor, List<Sale> sales, string mailTarget=null){

			HtmlGrid<Sale> g = new HtmlGrid<Sale>();
			g.DataSource=sales;

			if(! mailTarget.IsNullOrEmpty())
				g.GridStyle= new GreyGridStyle();  // css = Inline Style
			else
				g.Css = new CayitaGridGrey();      // css = Style Sheet

			g.Title= "Sales by: {0}".Fmt(vendor);
			g.Id = vendor.ReplaceAll(" ", "-");

			g.AddGridColum( c=> {
				c.HeaderText="Customer";
				c.CellRenderFunc=(row,index,dt)=> row.Customer;
				c.FooterRenderFunc= ()=>"Total Sales ===>";
				c.FooterCellColumnSpan=1;
			});

			g.AddGridColum( c=> {
				c.HeaderText="Product";
				c.CellRenderFunc=(row,index,dt)=> row.Product;
			});

			g.AddGridColum( c=> {
				c.HeaderText="Date";
				c.CellRenderFunc=(row,index,dt)=> row.Date.Format("MM/dd/yyyy");
			});

			g.AddGridColum( c=> {
				c.HeaderText="Price";
				c.CellRenderFunc=(row,index,dt)=> row.Price.Format();
				c.CellStyle.TextAlign="right";
				c.FooterRenderFunc= ()=> sales.Sum(f=>f.Price).Format();
				c.FooterCellStyle.TextAlign="right";
			});

			return g;
		}


		void SendSalesRepoByMail(HtmlDiv div, string mailTarget){

			Mailer.Send(message=>{
				message.Subject=  "Cayita.Widgets - Demo: Sales Repo";
				message.From= new MailAddress("cayita.widgets@no-mail.com");
				message.To.Add(mailTarget);
				message.Body= div.ToString();
				message.IsBodyHtml=true;
			});
		}

		List<Sale> GetSalesFromRepo(IHasVendor request, BLRequest blRequest){

			return Client.Execute(proxy=>{
				return request.Vendor.IsNullOrEmpty()?
					Sales.Get(proxy):
					Sales.Get(proxy, f=>f.Vendor==request.Vendor.Trim());
			});
		}		

		HtmlGrid<Sale> BuildSalesGrid (List<Sale> sales)
		
		{
			HtmlGrid<Sale> g = new HtmlGrid<Sale>();

			g.Css = new CayitaGridGrey();      // css = Style Sheet

			g.Title= "All Sales";

			g.AddGridColum( c=> {
				c.CellRenderFunc=(row,index,dt)=> row.Customer;
				c.HeaderText="Vendor";
				c.FooterRenderFunc= ()=>"Total Sales ===>";
				c.FooterCellColumnSpan=3;
			});


			g.AddGridColum( c=> {
				c.CellRenderFunc=(row,index,dt)=> row.Customer;
				c.HeaderText="Customer";

			});

			g.AddGridColum( c=> {
				c.CellRenderFunc=(row,index,dt)=> row.Product;
				c.HeaderText="Product";

			});

			g.AddGridColum( c=> {
				c.CellRenderFunc=(row,index,dt)=> row.Date.Format("MM/dd/yyyy");
				c.HeaderText="Date";
			});

			g.AddGridColum( c=> {
				c.CellRenderFunc=(row,index,dt)=> row.Price;
				c.HeaderText="Price";
				c.FooterRenderFunc= ()=> sales.Sum(f=>f.Price).Format();
			});

			return g;
		}






	}
}

