using ServiceStack.FluentValidation;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.FluentValidation.Results;

namespace Cayita.HtmlWidgets.Demo.BL
{
	public class BLException: ValidationException , IResponseStatusConvertible 
	{
		public BLException (string message):base(
			new ValidationFailure[]{new ValidationFailure("None",message,"BLException")}) {	}

	}
}

