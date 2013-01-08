
namespace Cayita.HtmlWidgets.Demo.BLRules
{
	public class Rules
	{

		public UserRules UserRules {get;  protected internal set;}
		public AuthorRules AuthorRules {get;  protected internal  set;}

		public Rules ()
		{
			Load();
		}

		public void Load(){

			UserRules = new UserRules();
			AuthorRules = new AuthorRules();
		}






	}
}

