using Cayita.HtmlWidgets.Demo.Models;
using System.Collections.Generic;
using Cayita.HtmlWidgets.Demo.DAL;
using ServiceStack.Text;
using Cayita.HtmlWidgets.Common;

namespace Cayita.HtmlWidgets.Demo.BL
{
	public partial class Controller
	{

		public BLResponse<User>  SaveUser(SaveUser user, BLRequest blRequest){

			Client.Execute(proxy=>{

				if(user.Id==default(int)){
					Rules.UserRules.ValidateOnSave(user, Users.Count(proxy));
					Users.Post(proxy,user);
				}
				else{
					Rules.UserRules.ValidateOnSave(user);
					Users.Put(proxy,user);
				}

			});

			var r = new BLResponse<User>();
			if(!IsCayita(blRequest)) 
				r.Result.Add(user);
			else
			{
				var grid = BuildUserGrid(new List<User>());
				var dr =grid.CreateRow(user);
				r.Html= dr.ToString();
			}
			return r;
		
		}

		public BLResponse<User> DeleteUser(DeleteUser user, BLRequest blRequest){

			Client.Execute(proxy=>{
				Users.Destroy(proxy, user.Id);
			});

			var r = new BLResponse<User>();
			if(IsCayita(blRequest)) 
				r.Html= "record deleted";
			return r;
		}


		public BLResponse<User> GetUsers(BLRequest blRequest){

			return Client.Execute(proxy=>{
				var u= Users.Get(proxy);
				var r = new BLResponse<User>();
				if(!IsCayita(blRequest)) 
					r.Result=u;
				else
				{
					HtmlDiv div = new HtmlDiv( ){Name="User"};

					var itag = new HtmlIcon(){
						Class="button-add icon-plus-sign",
						Name="User"
					};

					div.AddHtmlTag(itag);

					div.AddHtmlTag(BuildUserGrid(u));

					HtmlDiv formDiv = new HtmlDiv(){Id="div-form-edit"};
					formDiv.Style.Hidden=true;

					var form = BuildUserForm();
					form.AddCssClass("span6");
					form.Title="User's Data";
					formDiv.AddHtmlTag(form);
				
					r.Html= div.ToString()+formDiv.ToString();

				}
				return r;
			});
		}

		public BLResponse<User> GetUser(GetUser request, BLRequest blRequest){
			return Client.Execute(proxy=>{
				var u= Users.FirstOrDefault(proxy, f=>f.Id==request.Id);
				if( u==default(User))
					throw new BLException("User not found. Id:'{0}'".Fmt(request.Id));

				var r = new BLResponse<User>();
				if(!IsCayita(blRequest)) r.Result.Add(u);
				else
				{
					var grid = BuildUserGrid(new List<User>());
					var dr =grid.CreateRow(u);
					r.Html= dr.ToString();
				}
				return r;
			});
		}

		public BLResponse<User> SendUserForm(UserForm request, BLRequest blRequest)
		{
			return new BLResponse<User>(){Html = BuildUserForm().ToString()};
		}


		HtmlGrid<User> BuildUserGrid (List<User> users){

			var grid = new HtmlGrid<User>(){Name="User"};
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
				c.CellRenderFunc= (row,index,dt)=> row.Address;
				c.HeaderText="Address";
			});

			grid.AddGridColum( c=> {
				c.HeaderText="Birthday";
				c.CellRenderFunc=(row,index,dt)=> row.DoB.Format("MM/dd/yyy");
				c.CellStyle.TextAlign="center";
			});

			grid.AddGridColum( c=> {
				c.HeaderText="E-Mail";
				c.CellRenderFunc=(row,index,dt)=> row.Email;
			});

			grid.AddGridColum( c=> {
				c.HeaderText="Rating";
				c.CellRenderFunc=(row,index,dt)=>{ 
					dt.Parent.AddCssClass(row.Rating==10?"success":row.Rating<6?"warning":"" );
					return row.Rating;
				};
				c.CellStyle.TextAlign="center";
			});

			grid.AddGridColum( c=> {
				c.HeaderText="Level";
				c.CellRenderFunc=(row,index,dt)=> {
					c.CellStyle.Color= row.Level=="A"?"green": row.Level=="B"?"orange":"red";
					return row.Level;
				};
				c.CellStyle.TextAlign="center";

			});

			grid.AddGridColum( c=> {
				c.HeaderText="Active?";
				c.CellRenderFunc=(row,index,dt)=> {
					dt.Parent.Style.Color=row.IsActive?"black":"grey";
					//return row.IsActive.Format();
					var itag = new HtmlIcon(){
						Class= row.IsActive? "icon-ok-circle":"icon-ban-circle",
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
						Name="User",
						Class="button-edit icon-edit",
						Url= "api/User/read/{0}".Fmt( row.Id)
					};

					itag.Style.Margin.Left= 5;
					idiv.AddHtmlTag(itag);

					itag = new HtmlIcon(){
						Class="button-delete icon-remove",
						Url= "api/User/destroy/{0}".Fmt( row.Id)
					};
					itag.Style.Margin.Left= 5;
					itag.Style.Color="red";

					idiv.AddHtmlTag(itag);
					return idiv;
				};
			});

			return grid;
		}


		HtmlForm BuildUserForm(){

			HtmlForm form = new HtmlForm(){Name="User", Action="api/User/save?cayita=true"};

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
				input.Placeholder="User's name";
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
				field.Label.InnerHtml="Address";
				field.AddHtmlTextInput((input)=>{
					input.Name="Address";
				});
			});

			form.AddHtmlField( (field)=>{
				field.Label.InnerHtml="Birthday";
				field.AddHtmlTextInput((input)=>{
					input.Name="DoB";
					input.Type="date";
					input.Required=true;
				});
			});

			form.AddHtmlField( (field)=>{
				field.Label.InnerHtml="E-Mail";
				field.AddHtmlTextInput((input)=>{
				input.Required=true;
				input.Name="Email";
				input.Placeholder="e-mail";
				input.Type="email";
				input.Attributes["data-validation-email-message"]="no valid e-mail";
				});
			});

			form.AddHtmlField( (field)=>{
				field.Label.InnerHtml="Rating";
				field.AddHtmlTextInput((input)=>{
				input.Required=true;
				input.Name="Rating";
				input.Placeholder="User's rating";
				});
			});


			form.AddHtmlField( (field)=>{
				field.Label.InnerHtml="Level";
				field.AddHtmlRadioInput((input)=>{
					input.Name="Level";
					input.Inline=true;
					input.Value="A";
					input.Label="A";
				});

				field.AddHtmlRadioInput((input)=>{
					input.Name="Level";
					input.Inline=true;
					input.Value="B";
					input.Label="B";
				});

				field.AddHtmlRadioInput((input)=>{
					input.Name="Level";
					input.Inline=true;
					input.Value="C";
					input.Label="C";
				});
			});

			form.AddHtmlField( (field)=>{
				field.Label.InnerHtml="Active";
				field.AddHtmlCheckboxInput((input)=>{
					input.Name="IsActive";
				});
			});

			form.AddActionButton(b=>{
				b.ButtonType=ButtonType.Submit;
				b.Text="Save";
				b.Class="button-save btn btn-primary";
				b.Name="User";
			});

			form.AddActionButton(b=>{
				b.Text="Cancel";
				b.ButtonType=ButtonType.Reset;
				b.Class="button-cancel btn";
				b.Name="User";
			});

			return form;
		}



	}
}

