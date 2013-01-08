using System;
using ServiceStack.DesignPatterns.Model;
using ServiceStack.ServiceHost;

namespace Cayita.HtmlWidgets.Demo.Models
{

	public  class User:IHasIntId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
		public string City { get; set; }
        public string Email { get; set; }
        public DateTime DoB { get; set; }
        public bool IsActive { get; set; }
		public string Level { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Rating { get; set; }
    }

	//[Route("/User/create","POST")]
	//[Route("/User/update/{Id}","POST,PUT")]
	[Route("/User/save","POST,PUT")]
	public  class SaveUser: User,   IReturn<BLResponse<User>>{}


	[Route("/User/read","POST,GET")]
	public class GetUsers:  IReturn<BLResponse<User>>{

	}

	[Route("/User/read/{Id}","POST,GET")]
	public class GetUser: IReturn<BLResponse<User>>{

		public GetUser(int id){
			Id=id;
		}
		public int Id {get;set;}
	}

	[Route("/User/destroy/{Id}")]
	public class DeleteUser: IReturn<BLResponse<User>>{

		public DeleteUser(int id){
			Id=id;
		}
		public int Id {get;set;}
	}

	[Route("/User/form","POST,GET")]
	public class UserForm:  IReturn<BLResponse<User>>{

	}

}

