using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentApplication.Model;
using BLL;

public partial class Pages_Members_Uitschrijven : BasePage
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
        if (!string.IsNullOrEmpty(Session["IDModule"] as string))
        {
            string index = Session["IDModule"].ToString();
            ddlModuleTypes.SelectedValue = index;
        }
        Session["IDModule"] = null;
    }

    protected void lnkSchrijfMeuit_Click(object sender, EventArgs e)
    {
        

        clsTicketInschrijving ins = new clsTicketInschrijving();
        ins.IDGebruikerStudent = Convert.ToInt32(Session["CurrentUserID"]);
        ins.IDModule = Convert.ToInt32(ddlModuleTypes.SelectedValue);
        
        ins.AangemaaktOp = DateTime.Now;
        ins.AangepastOp = DateTime.Now;
        clsBLLUitschrijven.Insert(ins);


        Session["Uitschrijving"] = ddlModuleTypes.SelectedValue;
        lblBevestiging.CssClass = "";
    }

    protected void ddlModuleTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Uitschrijving"] = 0;
        lblBevestiging.CssClass = "hidden";

    }
}