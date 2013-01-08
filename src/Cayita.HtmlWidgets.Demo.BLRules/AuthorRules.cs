using System;
using Cayita.HtmlWidgets.Demo.Models;
using ServiceStack.FluentValidation.Results;
using ServiceStack.FluentValidation;
using ServiceStack.Text;

namespace Cayita.HtmlWidgets.Demo.BLRules
{
	public class AuthorRules
	{
		public AuthorRules ()
		{
			Load();
			CheckMaxAuthors= f=> f<=MaxAuthors;
		}

		public int MaxAuthors {get;  set;}
		public Func<int,bool> CheckMaxAuthors {get; set;}

		public void Load ()
		{
			MaxAuthors=50;
		}


		public void ValidateOnSave(Author author, int count=0)
		{

			if(! CheckMaxAuthors(count)){
				var vf = new ValidationFailure("None","User.Count:{0}  must be <= {1}".Fmt(count, MaxAuthors),"MaxCount");
				throw new ValidationException( new ValidationFailure[]{vf} );
			}

			//DefaultValidatorExtensions.ValidateAndThrow(this, author, "SaveAuthor");

		}

	}
}

