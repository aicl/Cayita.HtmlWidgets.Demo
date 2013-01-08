using System;
using ServiceStack.DesignPatterns.Model;
using ServiceStack.ServiceHost;

namespace Cayita.HtmlWidgets.Demo.Models
{

	public class Author:IHasIntId
	{
		public Author(){}


		public int Id { get; set;}

		public string Name { get; set;}

		public DateTime Birthday { get; set;}

		public DateTime ? LastActivity  { get; set;}

		public Decimal? Earnings { get; set;}  

		public bool Active { get; set; } 

		public string City { get; set;}

		public string Comments { get; set;}

		public int Rate{ get; set;}
	}


	[Route("/Author/save","POST,PUT")]
	public  class SaveAuthor: Author,   IReturn<BLResponse<Author>>{}


	[Route("/Author/read","POST,GET")]
	public class GetAuthors:  IReturn<BLResponse<Author>>{

	}

	[Route("/Author/read/{Id}","POST,GET")]
	public class GetAuthor: IReturn<BLResponse<Author>>{

		public GetAuthor(int id){
			Id=id;
		}
		public int Id {get;set;}
	}

	[Route("/Author/destroy/{Id}")]
	public class DeleteAuthor: IReturn<BLResponse<Author>>{

		public DeleteAuthor(int id){
			Id=id;
		}
		public int Id {get;set;}
	}


}

