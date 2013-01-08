using System;
using Funq;
using System.Collections.Concurrent;
using ServiceStack.DesignPatterns.Model;
using System.Collections.Generic;
using ServiceStack.Common.Utils;
using System.Reflection;
using ServiceStack.Text;

namespace Cayita.Repository
{
	public class MemRepo:IRepository{

		Container containter;
		ConcurrentDictionary<Type, int> auto;

		public MemRepo (){
			containter = new Container();
			auto = new ConcurrentDictionary<Type,int>();
		}

		#region IRepository implementation
		public void Create<T> () where T:IHasIntId
		{
			var t= containter.TryResolve<List<T>>();
			if(t==default(List<T>)){
				containter.Register(new List<T>());
				auto.TryAdd(typeof(T),0);
			}
		}

		public int Count<T> (Predicate<T> filter=null)
		{
			var t= GetOrThrow<T>();
			return filter==null? t.Count: t.FindAll(filter).Count;
		}


		public List<T> GetList<T> (Predicate<T> filter=null)
		{
			var t= GetOrThrow<T>();
			return filter==null? t: t.FindAll(filter);
		}

		public T FirstOrDefault<T> ( Predicate<T> filter=null)
		{
			var t= GetOrThrow<T>();

			if (filter!=null) t= t.FindAll(filter);
			return t.Count>0? t[0]: default(T);
		}

		public void Post<T> (T record) where T:IHasIntId
		{
			var t= GetOrThrow<T>();
			Type type = typeof(T);
			PropertyInfo pi= ReflectionUtils.GetPropertyInfo(type, "Id");
			ReflectionUtils.SetProperty(record, pi,auto[typeof(T)]+1 );
			auto[typeof(T)]=record.Id;
			t.Add(record);

		}

		public void Put<T> (T record) where T:IHasIntId
		{
			var t= GetOrThrow<T>();
			var i = t.FindIndex(f=>f.Id== record.Id);
			if(i>=0) t[i]= record;
			else
				throw new Exception( "There is not record with Id:'{0}' in {1}".Fmt( record.Id, typeof(T).Name));
		}

		public void Destroy<T> (int id) where T:IHasIntId 
		{
			var t= GetOrThrow<T>();
			var i = t.FindIndex(f=>f.Id== id);
			if(i>=0) t.RemoveAt(i);
			else
				throw new Exception( "There is not record with Id:'{0}' in {1}".Fmt( id, typeof(T).Name));

		}
		#endregion

		List<T> GetOrThrow<T>(){
			var t= containter.TryResolve<List<T>>();
			if(t!=default(List<T>)) return t;

			throw new Exception( "There is not :'{0}'".Fmt( typeof(T).Name));
		}
	}

}
