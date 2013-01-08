using Cayita.HtmlWidgets.Demo.Models;
using System.Collections.Generic;
using Cayita.HtmlWidgets.Demo.DAL;
using ServiceStack.Text;

namespace Cayita.HtmlWidgets.Demo.BL
{
	public partial class Controller
	{

		public BLResponse<Author>  SaveAuthor(SaveAuthor author, BLRequest blRequest){

			Client.Execute(proxy=>{

				if(author.Id==default(int)){
					Rules.AuthorRules.ValidateOnSave(author, Authors.Count(proxy));
					Authors.Post(proxy,author);
				}
				else{
					Rules.AuthorRules.ValidateOnSave(author);
					Authors.Put(proxy,author);
				}

			});

			var r = new BLResponse<Author>();
			if(!IsCayita(blRequest)) 
				r.Result.Add(author);
			else
			{
				var grid = BuildAuthorGrid(new List<Author>());
				var dr =grid.CreateRow(author);
				r.Html= dr.ToString();
			}
			return r;
		
		}

		public BLResponse<Author> DeleteAuthor(DeleteAuthor author, BLRequest blRequest){

			Client.Execute(proxy=>{
				Authors.Destroy(proxy, author.Id);
			});

			var r = new BLResponse<Author>();
			if(IsCayita(blRequest)) 
				r.Html= "record deleted";
			return r;
		}


		public BLResponse<Author> GetAuthors(BLRequest blRequest){

			return Client.Execute(proxy=>{
				var u= Authors.Get(proxy);
				var r = new BLResponse<Author>();
				if(!IsCayita(blRequest)) 
					r.Result=u;
				else
				{
					HtmlDiv div = new HtmlDiv( ){Name="Author"};

					var itag = new HtmlIcon(){
						Class="button-add icon-plus-sign",
						Name="Author"
					};

					div.AddHtmlTag(itag);

					div.AddHtmlTag(BuildAuthorGrid(u));

					HtmlDiv formDiv = new HtmlDiv(){Id="div-form-edit"};
					formDiv.Style.Hidden=true;

					var form = BuildAuthorForm();
					form.AddCssClass("span6");
					form.Title="Author's Data";
					formDiv.AddHtmlTag(form);
				
					r.Html= div.ToString()+formDiv.ToString();

				}
				return r;
			});
		}

		public BLResponse<Author> GetAuthor(GetAuthor request, BLRequest blRequest){
			return Client.Execute(proxy=>{
				var u= Authors.FirstOrDefault(proxy, f=>f.Id==request.Id);
				if( u==default(Author))
					throw new BLException("Author not found. Id:'{0}'".Fmt(request.Id));

				var r = new BLResponse<Author>();
				if(!IsCayita(blRequest)) r.Result.Add(u);
				else
				{
					var grid = BuildAuthorGrid(new List<Author>());
					var dr =grid.CreateRow(u);
					r.Html= dr.ToString();
				}
				return r;
			});
		}


		HtmlGrid<Author> BuildAuthorGrid (List<Author> users){

			var grid = new HtmlGrid<Author>(){Name="Author"};
			grid.DataSource= users;
			grid.Css = new Bootstrap();

			grid.AddGridColum( c=> {
				c.CellRenderFunc=(row,index,dt)=> row.Id;
				c.HeaderText="Id";
				c.Hidden=true; 
			});

			grid.AddGridColum( c=> {
				c.CellRenderFunc= (row,index,dt)=> row.Name;
				c.HeaderText="Name";
			});

			grid.AddGridColum( c=> {
				c.HeaderText="City";
				c.CellRenderFunc=(row,index,dt)=> row.City;
			});

			grid.AddGridColum( c=> {
				c.CellRenderFunc= (row,index,dt)=> row.Comments;
				c.HeaderText="Comments";
			});

			grid.AddGridColum( c=> {
				c.HeaderText="Active?";
				c.CellRenderFunc=(row,index,dt)=> {
					dt.Parent.Style.Color=row.Active?"black":"grey";
					var itag = new HtmlIcon(){
						Class= row.Active? "icon-ok-circle":"icon-ban-circle",
					};
					return itag;
				};
				c.CellStyle.TextAlign="center";
			});


			grid.AddGridColum( c=> {
				c.HeaderText="";
				c.CellStyle.Width=new WidthProperty(){Unit="%",Value=10};
				c.CellRenderFunc=(row,index,dt)=>{
					var idiv = new HtmlDiv();
					var itag = new HtmlIcon(){
						Name="Author",
						Class="button-edit icon-edit",
						Url= "api/Author/read/{0}".Fmt( row.Id)
					};
					itag.Style.Margin.Left=5;
					idiv.AddHtmlTag(itag);

					itag = new HtmlIcon(){
						Class="button-delete icon-remove",
						Url= "api/Author/destroy/{0}".Fmt( row.Id)
					};
					itag.Style.Margin.Left=5;
					itag.Style.Color="red";
					idiv.AddHtmlTag(itag);
					return idiv;
				};
			});

			return grid;
		}


		HtmlForm BuildAuthorForm(){

			HtmlForm form = new HtmlForm(){Name="Author", Action="api/Author/save?cayita=true"};

			form.Id="form-edit";

			form.AddHtmlHiddenField( (field)=>{
				field.AddHtmlTextInput((input)=>{
				input.Name="Id";
				});
			});

			form.AddHtmlField( (field)=>{
				field.Label.InnerHtml="Name";
				field.AddHtmlTextInput((input)=>{
				input.Required=true;
				input.Name="Name";
				input.Placeholder="Author's name";
				});
			});

			form.AddHtmlField( (field)=>{
				field.Label.InnerHtml="City";
				field.AddHtmlTextInput((input)=>{
				input.Required=true;
				input.Name="City";
				input.Placeholder="city";
				});
			});

			form.AddHtmlField( (field)=>{
				field.Label.InnerHtml="Comments";
				field.AddHtmlTextInput((input)=>{
					input.Name="Comments";
				});
			});



			form.AddHtmlField( (field)=>{
				field.Label.InnerHtml="Active";
				field.AddHtmlCheckboxInput((input)=>{
					input.Name="Active";
				});
			});


			form.AddActionButton( new HtmlButton(){
				Type="submit", Text="Save", Class="button-save btn btn-primary",
				Name="Author"
			});
			form.AddActionButton( new HtmlButton(){Text="Cancel", Class="button-cancel btn", Name="Author"});

			return form;
		}

	}
}

