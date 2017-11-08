using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentApplication.Model;
using BLL;
using System.Collections.ObjectModel;

public partial class Pages_Members_ModuleInBehandeling : BasePage
{
    static clsCommonBLL bll = new clsCommonBLL();
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
        int IDModule = Convert.ToInt32(arg[0]);
        int IDInschrijving = Convert.ToInt32(arg[1]);
        bool ok = false;
        ObservableCollection<clsInschrijving> inschrijvingen = bll.GetData<clsInschrijving>();
        ObservableCollection<clsTicketInschrijving> tickets = bll.GetData<clsTicketInschrijving>();

        foreach (clsTicketInschrijving ticket in tickets)
        {
            if (IDInschrijving == ticket.IDInschrijving)
            {
                bll.DeleteData<clsTicketInschrijving>(ticket);
                ok = true;
            }
        }
        if (ok == false)
        {
            foreach (clsInschrijving ins in inschrijvingen)
            {
                if (IDInschrijving == ins.IDInschrijving && ins.IsValidated == false)
                {
                    bll.DeleteData<clsInschrijving>(ins);
                }
            }
        }


    }
}