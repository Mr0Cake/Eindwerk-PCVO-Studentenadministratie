using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentApplication.Model;
using BLL;

public partial class Pages_Members_Module : BasePage
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

    protected void lnkInschrijving_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        string[] arg = new string[2];
        arg = lnk.CommandArgument.ToString().Split(';');
        string controle = arg[0];
        Session["IDModule"] = arg[1];
        if (controle == "False") // betekent inschrijving
        {
            clsInschrijving ins = new clsInschrijving();
            ins.IDGebruiker = Convert.ToInt32(Session["CurrentUserID"]);
            //ins.IDModule = Convert.ToInt32(ddlModules.SelectedValue);
            ins.IDModule = Convert.ToInt32(arg[1]);
            ins.InschrijvingsDatum = DateTime.Now;
            ins.IsValidated = false;
            ins.AangemaaktOp = DateTime.Now;
            ins.AangepastOp = DateTime.Now;
            clsBLLInschrijving.Insert(ins);

        }
        else if (controle == "True") // betekent uitschrijving
        {
            clsTicketInschrijving ins = new clsTicketInschrijving();
            ins.IDGebruikerStudent = Convert.ToInt32(Session["CurrentUserID"]);
            //ins.IDModule = Convert.ToInt32(ddlModules.SelectedValue);
            ins.IDModule = Convert.ToInt32(arg[1]);
            ins.AangemaaktOp = DateTime.Now;
            ins.AangepastOp = DateTime.Now;
            clsBLLUitschrijven.Insert(ins);
        }
    }
}