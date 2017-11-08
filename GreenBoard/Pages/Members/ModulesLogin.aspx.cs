using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentApplication.Model;
using BLL;

public partial class Pages_Members_ModulesLogin : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CurrentUserID"] != null)
        {
            if (Session["CurrentUserID"].ToString() == "")
            {
                Response.Redirect("/Pages/Login.aspx");
            }
        }
        else
        {
            Response.Redirect("/Pages/Login.aspx");
        }
    }

    protected void ddlModules_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(ddlModules.SelectedValue.ToString());
    }
}