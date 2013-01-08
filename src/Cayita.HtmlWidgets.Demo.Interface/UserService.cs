using Cayita.HtmlWidgets.Demo.Models;

namespace Cayita.HtmlWidgets.Demo.Interface
{
	public class UserService:AppServiceBase
	{

		#region create - update
		public BLResponse<User> Post(SaveUser request)
		{
			return Controller.SaveUser(request,BLRequest);
		}

		public BLResponse<User> Put(SaveUser request)
		{
			return Post(request);
		}
		#endregion create - update


		#region read
		public BLResponse<User> Post(GetUsers request)
		{
			return Controller.GetUsers( BLRequest);
		}

		public BLResponse<User> Get(GetUsers request)
		{
			return Post (request);
		}

		public BLResponse<User> Post ( GetUser request)
		{
			return  Controller.GetUser(request, BLRequest);
		}

		public BLResponse<User> Get (GetUser request)
		{
			return Post (request);
		}
		#endregion read


		#region delete
		public BLResponse<User> Any(DeleteUser request)
		{
			return Controller.DeleteUser(request, BLRequest);
		}
		#endregion delete


		public BLResponse<User> Post(UserForm request){
			return Controller.SendUserForm(request, BLRequest);
		}

		public BLResponse<User> Get(UserForm request){
			return Post(request);
		}

	}

}

