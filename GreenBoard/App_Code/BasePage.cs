using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public BasePage()
    {
        this.PreInit += BasePage_PreInit;
    }

    private void BasePage_PreInit(object sender, EventArgs e)
    {
        //Haal cookies op
        HttpCookie preferredTheme = Request.Cookies.Get("PreferredTheme");
        if (preferredTheme != null)
        {
            string folder = Server.MapPath("~/App_Themes/" + preferredTheme.Value);
            if (System.IO.Directory.Exists(folder))
            {
                Page.Theme = preferredTheme.Value;
            }
        }
    }
    public int getUserID()
    {
        return Convert.ToInt32(Session["CurrentUserID"]);
    }
    public string GetImage(object img)
    {
        if (img != null)
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        return null;
    }
    public bool isUser(object UserID)
    {
        return Convert.ToInt32(UserID) == getUserID();
    }
}