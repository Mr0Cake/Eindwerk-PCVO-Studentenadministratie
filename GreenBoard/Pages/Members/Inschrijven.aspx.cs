using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentApplication.Model;
using BLL;

public partial class Pages_Members_Inschrijven : BasePage
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

    protected void lnkSchrijfMeIn_Click(object sender, EventArgs e)
    {
        clsInschrijving ins = new clsInschrijving();
        ins.IDGebruiker = Convert.ToInt32(Session["CurrentUserID"]);
        ins.IDModule = Convert.ToInt32(ddlModuleTypes.SelectedValue);
        ins.InschrijvingsDatum = DateTime.Now;
        ins.IsValidated = false;
        ins.AangemaaktOp = DateTime.Now;
        ins.AangepastOp = DateTime.Now;
        clsBLLInschrijving.Insert(ins);
        
        lblBevestiging.CssClass = "";

    }

    protected void ddlModuleTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["IDModule"] = 0;
        lblBevestiging.CssClass = "hidden";
    }

    protected void ddlModuleTypes_DataBound(object sender, EventArgs e)
    {
  if (ddlModuleTypes.Items.Count == 0)
        {
            ddlModuleTypes.Items.Add("Er zijn geen modules gevonden waarvoor u zich kunt inschrijven!");
        }
    }
}