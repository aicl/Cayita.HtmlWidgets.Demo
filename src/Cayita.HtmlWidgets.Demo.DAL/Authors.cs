using System;
using Cayita.HtmlWidgets.Demo.Models;
using System.Collections.Generic;

namespace Cayita.HtmlWidgets.Demo.DAL
{
	public static class Authors
	{
		public static void CreateTable(  RepositoryProxy proxy ){
			proxy.Repository.Create<Author>();
		}

		public static List<Author> Get( RepositoryProxy proxy, Predicate<Author> filter=null ){
			return proxy.Repository.GetList<Author>();
		}

		public static Author FirstOrDefault( RepositoryProxy proxy, Predicate<Author> filter=null ){
			return proxy.Repository.FirstOrDefault<Author>(filter);
		}

		public static void Post( RepositoryProxy proxy, Author user){
			proxy.Repository.Post(user);
		}

		public static void Put( RepositoryProxy proxy, Author user){
			proxy.Repository.Put(user);
		}

		public static void Destroy( RepositoryProxy proxy, int id){
			proxy.Repository.Destroy<Author>(id);
		}

		public static int Count(RepositoryProxy proxy, Predicate<Author> filter=null){
			return proxy.Repository.Count<Author>(filter);
		}

	}
}

