using System;
using System.Collections.Generic;
using System.Linq;
using Cayita.HtmlWidgets.Demo.Models;
using ServiceStack.Text;
using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Results;

namespace Cayita.HtmlWidgets.Demo.BLRules
{
	public class UserRules:AbstractValidator<User>
	{

		public UserRules ()
		{
			Load();

			CheckMaxUsers= f=> f<=MaxUsers;
			CheckRating= f=> f>=MinRating && f<=MaxRating;
			CheckLevel = f=> Levels.Any(l=>l==f);

			RuleSet("SaveUser", () => {

				RuleFor(x => x.Rating).Must(r=> CheckRating(r) ).
					WithMessage("Rating must be betwen {0} and {1}".Fmt(MinRating, MaxRating)).
					WithErrorCode("InvalidRating");

				RuleFor(x => x.Level).Must(r=>CheckLevel(r) ).
					WithMessage("Invalid Level").WithErrorCode("InvalidLevel");


			});

		}

		public int MaxUsers {get;  protected internal set;}
		public int MinRating {get; protected internal set;}
		public int MaxRating {get; protected internal set;}
		public  List<string> Levels {get; protected internal set;}

		public Func<int,bool> CheckMaxUsers {get; set;}
		public Func<int,bool> CheckRating {get;  set;}
		public Func<string,bool> CheckLevel {get;  set;}

		public void Load ()
		{
			MaxUsers=20;
			MinRating =1;
			MaxRating= 10;
			Levels = new List<string>(){"A", "B", "C"};
		}


		public void ValidateOnSave(User user, int count=0)
		{

			if(! CheckMaxUsers(count)){
				var vf = new ValidationFailure("MaxCount","User.Count:{0}  must be <= {1}".Fmt(count, MaxUsers),"MaxCount");
				throw new ValidationException( new ValidationFailure[]{vf} );
			}

			DefaultValidatorExtensions.ValidateAndThrow(this, user, "SaveUser");

		}


	}
}

