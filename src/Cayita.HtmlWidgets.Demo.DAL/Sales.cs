using System;
using Cayita.HtmlWidgets.Demo.Models;
using System.Collections.Generic;

namespace Cayita.HtmlWidgets.Demo.DAL
{
	public static class Sales
	{
		public static void CreateTable(  RepositoryProxy proxy ){
			proxy.Repository.Create<Sale>();
		}

		public static List<Sale> Get( RepositoryProxy proxy, Predicate<Sale> filter=null ){
			return proxy.Repository.GetList<Sale>();
		}

		public static void Populate( RepositoryProxy proxy){
			 var sales= new List<Sale>(){ 
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,01,01), Price=1000, Product="Keyboard", Customer="AB Pcs"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,02,01), Price=3000, Product="Keyboard", Customer="AB Pcs"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,03,01), Price=2500, Product="Keyboard", Customer="AB Pcs"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,04,01), Price= 500, Product="Keyboard", Customer="AB Pcs"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,01,01), Price= 700, Product="Monitor",  Customer="AB Pcs"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,02,01), Price= 3600, Product="Monitor", Customer="AB Pcs"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,03,01), Price= 1400, Product="Monitor", Customer="AB Pcs"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,04,01), Price= 2400, Product="Monitor", Customer="GigaHard Inc"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,01,01), Price=3400, Product="Keyboard", Customer="GigaHard Inc"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,02,01), Price=6000, Product="Keyboard", Customer="GigaHard Inc"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,03,01), Price=2800, Product="Keyboard", Customer="GigaHard Inc"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,04,01), Price= 900, Product="Keyboard", Customer="GigaHard Inc"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,01,01), Price= 7000, Product="Monitor",  Customer="GigaHard Inc"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,02,01), Price= 1600, Product="Monitor", Customer="GigaHard Inc"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,03,01), Price= 4400, Product="Monitor", Customer="GigaHard Inc"  },
				new Sale(){ Vendor ="Jim Black", Date= new DateTime(2012,04,01), Price= 2900, Product="Monitor", Customer="GigaHard Inc"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,01,01), Price=900, Product="Keyboard", Customer="SuperPC"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,02,01), Price=6000, Product="Keyboard", Customer="SuperPC"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,03,01), Price=8500, Product="Keyboard", Customer="SuperPC"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,04,01), Price= 6500, Product="Keyboard", Customer="SuperPC"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,01,01), Price= 7700, Product="Monitor",  Customer="SuperPC"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,02,01), Price= 600, Product="Monitor", Customer="SuperPC"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,03,01), Price= 7400, Product="Monitor", Customer="SuperPC"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,04,01), Price= 4400, Product="Monitor", Customer="Gato Systems"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,01,01), Price=6400, Product="Keyboard", Customer="Gato Systems"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,02,01), Price=6300, Product="Keyboard", Customer="Gato Systems"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,03,01), Price=7800, Product="Keyboard", Customer="Gato Systems"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,04,01), Price= 6700, Product="Keyboard", Customer="Gato Systems"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,01,01), Price= 5700, Product="Monitor",  Customer="Gato Systems"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,02,01), Price= 7400, Product="Monitor", Customer="Gato Systems"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,03,01), Price= 900, Product="Monitor", Customer="Gato Systems"  },
				new Sale(){ Vendor ="Martha White", Date= new DateTime(2012,04,01), Price= 6300, Product="Monitor", Customer="Gato Systems"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,01,01), Price=2900, Product="Keyboard", Customer="MaxPower Pc"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,02,01), Price=600, Product="Mouse", Customer="MaxPower Pc"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,03,01), Price=4300, Product="Keyboard", Customer="MaxPower Pc"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,04,01), Price= 6100, Product="Moues", Customer="MaxPower Pc"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,01,01), Price= 2700, Product="Monitor",  Customer="MaxPower Pc"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,02,01), Price= 6600, Product="Mouse", Customer="MaxPower Pc"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,03,01), Price= 7400, Product="Hard Disk", Customer="MaxPower Pc"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,04,01), Price= 5400, Product="Monitor", Customer="SKY Systems"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,01,01), Price=1500, Product="Keyboard", Customer="SKY Systems"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,02,01), Price=5800, Product="Keyboard", Customer="SKY Systems"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,03,01), Price=8500, Product="Hard Disk", Customer="SKY Systems"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,04,01), Price= 7300, Product="Keyboard", Customer="SKY Systems"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,01,01), Price= 5400, Product="Hard Disk",  Customer="SKY Systems"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,02,01), Price= 8600, Product="Monitor", Customer="SKY Systems"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,03,01), Price= 6900, Product="Monitor", Customer="SKY Systems"  },
				new Sale(){ Vendor ="Samantha Blanco", Date= new DateTime(2012,04,01), Price= 6500, Product="Hard Disk", Customer="SKY Systems"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,01,01), Price=4300, Product="Keyboard", Customer="OSI Systems"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,02,01), Price=8000, Product="Mouse", Customer="OSI Systems"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,03,01), Price=1200, Product="Keyboard", Customer="OSI Systems"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,04,01), Price= 5300, Product="Moues", Customer="OSI Systems"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,01,01), Price= 3200, Product="Monitor",  Customer="OSI Systems"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,02,01), Price= 4300, Product="Mouse", Customer="OSI Systems"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,03,01), Price= 5400, Product="Hard Disk", Customer="OSI Systems"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,04,01), Price= 2100, Product="Monitor", Customer=" PC & PC"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,01,01), Price=6500, Product="Keyboard", Customer=" PC & PC"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,02,01), Price=6400, Product="Keyboard", Customer=" PC & PC"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,03,01), Price=4800, Product="Hard Disk", Customer=" PC & PC"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,04,01), Price= 700, Product="Keyboard", Customer=" PC & PC"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,01,01), Price= 600, Product="Hard Disk",  Customer=" PC & PC"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,02,01), Price= 3600, Product="Monitor", Customer=" PC & PC"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,03,01), Price= 6400, Product="Monitor", Customer=" PC & PC"  },
				new Sale(){ Vendor ="Joe Brown", Date= new DateTime(2012,04,01), Price= 300, Product="Hard Disk", Customer=" PC & PC"  },


			};

			foreach(var sale in sales)
				proxy.Repository.Post(sale);
		}
	}
}

