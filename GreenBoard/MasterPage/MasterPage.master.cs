using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_MasterPage : System.Web.UI.MasterPage
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

        if (!Page.IsPostBack)
        {
            string selectedTheme = Page.Theme;
            HttpCookie preferredTheme = Request.Cookies.Get("PreferredTheme");
            if (preferredTheme != null)
            {
                selectedTheme = preferredTheme.Value;
                ThemeList.SelectedValue = preferredTheme.Value;
            }
        }
    }
    protected void ThemeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        HttpCookie preferredTheme = new HttpCookie("PreferredTheme");
        preferredTheme.Expires = DateTime.Now.AddMonths(3);
        preferredTheme.Value = ThemeList.SelectedValue;
        Response.Cookies.Add(preferredTheme);
        Response.Redirect(Request.Url.ToString());
    }

    protected void lnkLoguit_Click(object sender, EventArgs e)
    {
        Session["IDModule"] = "";
        Session["CurrentUserID"] = "";
        Response.Redirect("/Pages/Login.aspx");
    }

    protected void lnkModuleInBehandeling_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Pages/Members/ModuleInBehandeling.aspx");
    }
}
