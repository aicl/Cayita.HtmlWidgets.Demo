using Cayita.HtmlWidgets.Demo.Models;

namespace Cayita.HtmlWidgets.Demo.Interface
{
	public class AuthorService:AppServiceBase
	{

		#region create - update
		public BLResponse<Author> Post(SaveAuthor request)
		{
			return Controller.SaveAuthor(request,BLRequest);
		}

		public BLResponse<Author> Put(SaveAuthor request)
		{
			return Post(request);
		}
		#endregion create - update


		#region read
		public BLResponse<Author> Post(GetAuthors request)
		{
			return Controller.GetAuthors( BLRequest);
		}

		public BLResponse<Author> Get(GetAuthors request)
		{
			return Post (request);
		}

		public BLResponse<Author> Post ( GetAuthor request)
		{
			return  Controller.GetAuthor(request, BLRequest);
		}

		public BLResponse<Author> Get (GetAuthor request)
		{
			return Post (request);
		}
		#endregion read


		#region delete
		public BLResponse<Author> Any(DeleteAuthor request)
		{
			return Controller.DeleteAuthor(request, BLRequest);
		}
		#endregion delete


	}
}

