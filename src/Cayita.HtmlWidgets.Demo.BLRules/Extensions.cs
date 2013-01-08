using ServiceStack.FluentValidation.Results;
using ServiceStack.Text;

namespace Cayita.HtmlWidgets.Demo.BLRules
{
	public static class Extensions
	{
		public static string BuildErrorMessage(this ValidationResult validationResult)
		{
			return validationResult.Errors.SerializeToString();	
		}
	}
}

