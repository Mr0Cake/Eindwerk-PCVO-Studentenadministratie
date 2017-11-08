using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADServices;
using System.Collections.ObjectModel;
using StudentApplication.Model;

public partial class Pages_Login : BasePage
{
    private static BLL.clsCustomBLL bll = new BLL.clsCustomBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Request.IsSecureConnection && !Request.IsLocal)
            {
                UriBuilder secureUrl = new UriBuilder(Request.Url);
                secureUrl.Scheme = "https";
                secureUrl.Port = 443;
                Response.Redirect(secureUrl.ToString(), false);
            }
                
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        
        try
        {
            
            //bool isvalid = clsServices.ValidateUser(txtEmail.Text, txtWachtwoord.Text);
            
            if (false)
            {
                
                try
                {
                    
                    string username = txtEmail.Text;

                    ObservableCollection<clsGebruiker> user = bll.GetDataListSpecific<clsGebruiker>(username);
                    Session["CurrentUserID"] = user[0].IDGebruiker;
                    lblFailedLogin.Text = "stap 5 opvragen user, ID ="+user[0].IDGebruiker;
                    try
                    {
                        Response.Redirect(@"/Pages/Members/Personalia.aspx", false);
                    }
                    catch(Exception p)
                    {
                        
                    }
                }
                catch(Exception ex) 
                {
                    
                }

            }
            else
            {
                //lblFailedLogin.CssClass = "User is not valid";
            }
            ////Enkel voor te testen zonder ActiveDirectory
            Session["CurrentUserID"] = 2;
            Response.Redirect(@"/Pages/Members/Personalia.aspx");

        }
        catch
        {
            ////Enkel voor te testen zonder ActiveDirectory

            //Session["CurrentUserID"] = 2;

            //Response.Redirect(@"/Pages/Members/Personalia.aspx");

            lblFailedLogin.CssClass = "";
            lblFailedLogin.Text = "Er ging iets grilligs fout";
        }
        
    }
}