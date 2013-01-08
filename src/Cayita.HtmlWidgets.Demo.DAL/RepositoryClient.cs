using System;
using Cayita.Repository;

namespace Cayita.HtmlWidgets.Demo.DAL
{
	public class RepositoryClient
	{
		IRepository repo;

		public RepositoryClient(IRepository repository){
			repo=repository;
		}


		public void Execute( Action<RepositoryProxy> commands)
        {
            using(RepositoryProxy proxy = new RepositoryProxy(repo))
            {
                commands(proxy);
            }
        }

		public T Execute<T>( Func<RepositoryProxy,T> commands)
        {

            using(RepositoryProxy proxy = new RepositoryProxy(repo))
            {
                return commands(proxy);
            }
        }

	}
}

