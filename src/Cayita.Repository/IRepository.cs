using System;
using ServiceStack.DesignPatterns.Model;
using System.Collections.Generic;

namespace Cayita.Repository
{
	public interface IRepository
	{
		void Create<T>() where T:IHasIntId;
		int Count<T>(Predicate<T> filter=null);
		List<T> GetList<T>(Predicate<T> filter=null);
		T FirstOrDefault<T>(Predicate<T> filter =null);
		void Post<T>(T record) where T:IHasIntId;
		void Put<T>(T record) where T:IHasIntId;
		void Destroy<T>(int id) where T:IHasIntId ;
	}
}

