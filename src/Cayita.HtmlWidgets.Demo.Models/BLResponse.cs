using System.Collections.Generic;

namespace Cayita.HtmlWidgets.Demo.Models
{

	public class BLResponse<T>{

		public BLResponse(){
			Result= new List<T>();
		}

		public BLResponse(T data){
			Result= new List<T>();
			Result.Add(data);
		}

		public List<T> Result {get;set;}
		public string Html {get;set;}
	}
}
