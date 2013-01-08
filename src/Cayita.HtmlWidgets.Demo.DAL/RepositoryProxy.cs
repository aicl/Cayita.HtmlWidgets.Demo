using System;
using Cayita.Repository;

namespace Cayita.HtmlWidgets.Demo.DAL
{
	public class RepositoryProxy:IDisposable
	{

		internal IRepository Repository {get;set;}

		public RepositoryProxy(IRepository repo){
			Repository =repo; 
		}

		#region IDisposable implementation
		public void Dispose ()
		{
			// do your dispose actions! 
		}
		#endregion

	}
}

