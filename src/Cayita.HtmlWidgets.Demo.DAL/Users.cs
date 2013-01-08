using System;
using System.Collections.Generic;
using Cayita.HtmlWidgets.Demo.Models;

namespace Cayita.HtmlWidgets.Demo.DAL
{
	public static class Users
	{

		public static void CreateTable(  RepositoryProxy proxy ){
			proxy.Repository.Create<User>();
		}

		public static List<User> Get( RepositoryProxy proxy, Predicate<User> filter=null ){
			return proxy.Repository.GetList<User>();
		}

		public static User FirstOrDefault( RepositoryProxy proxy, Predicate<User> filter=null ){
			return proxy.Repository.FirstOrDefault<User>(filter);
		}

		public static void Post( RepositoryProxy proxy, User user){
			proxy.Repository.Post(user);
		}

		public static void Put( RepositoryProxy proxy, User user){
			proxy.Repository.Put(user);
		}

		public static void Destroy( RepositoryProxy proxy, int id){
			proxy.Repository.Destroy<User>(id);
		}

		public static int Count(RepositoryProxy proxy, Predicate<User> filter=null){
			return proxy.Repository.Count<User>(filter);
		}

		public static void Populate( RepositoryProxy proxy){
			 var users= new List<User>(){ 
				new User(){ Name = "Angel Colmenares",Address = "Carrera 74", City = "Bogota",	Rating = 8,	DoB = new System.DateTime(1966,11,25),	UserName="billyC",Password= "123", Level="A", IsActive=true,Email="ab@mail.com"},
				new User(){ Name = "Claudia Espinel",Address = "Carrera 74", City = "Bogota",	Rating = 10,DoB = new System.DateTime(1970,03,08),	UserName="cayitax",Password= "432123", Level="B", IsActive=true,Email="cc@mail.com"},
				new User(){ Name = "Magaly Colmenares",Address = "Avenida 10A", City = "Cucuta",Rating = 7,DoB = new System.DateTime(1967,12,20),	UserName="magax",Password= "432123", Level="C", IsActive=true,Email="db@mail.com"},
				new User(){ Name = "Ruth Colmenares",Address = "Street 5", City = "Cucuta",Rating = 10, DoB = new System.DateTime(1970,06,08),	UserName="ruth.c",Password= "shak", Level="A", IsActive=false, Email="zz@mail.com"},
				new User(){ Name = "Javier Espinel",Address = "Street 8", City = "Bucaramanga",Rating = 6, DoB = new System.DateTime(1970,05,01),	UserName="tino",Password= "rex", Level="A", IsActive=false, Email="l31@mail.com"},
				new User(){ Name = "Edgar Espinel",Address = "Street 8", City = "Bucaramanga",Rating = 8, DoB = new System.DateTime(1975,06,30),	UserName="ragde",Password= "web", Level="B", IsActive=true, Email="m89@mail.com"},
				new User(){ Name = "Martha Gualdron",Address = "5th Avenue", City = "Bogota",Rating = 5, DoB = new System.DateTime(1975,06,30),	UserName="waldron",Password= "toby", Level="C", IsActive=true, Email="ya@mail.com"},
				new User(){ Name = "Alfredo Ramon",Address = "5th Avenue", City = "Bogota",Rating = 7, DoB = new System.DateTime(1975,06,30),	UserName="ystst",Password= "werty", Level="A", IsActive=true, Email="ab123@mail.com"}
			};

			foreach(var user in users)
				proxy.Repository.Post(user);
		}

	}
}

