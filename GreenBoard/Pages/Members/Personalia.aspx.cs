using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentApplication.Model;
using BLL;
using System.Web.Services;
using System.Collections.ObjectModel;

public partial class Pages_Members_Personalia : BasePage
{
    public clsGebruiker geb;
    public clsPostcode pc;
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
        int id = getUserID();
        geb = clsBLLGebruiker.Select(id);
        if (geb == null)
        {
            Response.Redirect("/Pages/Login.aspx");
        }

        if (geb.Photo != null)
            imgGebruiker.ImageUrl = GetImage(geb.Photo);
        else
            imgGebruiker.ImageUrl = @"/Images/default-user-profile.jpg";
        if (!IsPostBack)
        {
            LaadGebruikersData(geb);
        }


    }

    private void LaadGebruikersData(clsGebruiker gebruiker)
    {
        pc = getPostcode(gebruiker.IDPostCode);
        lblNaam.Text = gebruiker.Voornaam.ToUpper() + " " + gebruiker.Naam.ToUpper();
        //txtGemeente.Text = pc.Plaatsnaam.ToUpper();
        txtPostcode.Text = pc.Postcode;
        txtStraat.Text = gebruiker.Straat;
        txtNummer.Text = gebruiker.HuisNummer;
        txtMail.Text = gebruiker.EmailPersoonlijk;
        txtTelefoon.Text = gebruiker.TelefoonNummer;
        txtGSM.Text = gebruiker.GSMNummer;
        ddlTaal.SelectedValue = gebruiker.IDTaal.ToString();
    }
    private clsPostcode getPostcode(int id)
    {
        return clsBLLPostcode.Select(id);
    }

    protected void lnkWijzigData_Click(object sender, EventArgs e)
    {
        lblBevestiging.CssClass = "hidden";
        txtPostcode.ReadOnly = false;
        txtNummer.ReadOnly = false;
        txtMail.ReadOnly = false;
        txtTelefoon.ReadOnly = false;
        txtStraat.ReadOnly = false;
        txtGSM.ReadOnly = false;
        ddlGemeente.Enabled = true;
        ddlTaal.Enabled = true;
        lnkCancel.CssClass = "btn btn-default";
        lnkWijzigData.CssClass = "btn btn-default hidden";
        lnkBevistigWijzigen.CssClass = "btn btn-default";

    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        lblBevestiging.CssClass = "hidden";

        LaadGebruikersData(geb);
        txtPostcode.ReadOnly = true;
        txtNummer.ReadOnly = true;
        txtMail.ReadOnly = true;
        txtTelefoon.ReadOnly = true;
        txtStraat.ReadOnly = true;
        txtGSM.ReadOnly = true;
        ddlGemeente.Enabled = false;
        ddlTaal.Enabled = false;
        lnkCancel.CssClass = "btn btn-default hidden";
        lnkWijzigData.CssClass = "btn btn-default";
        lnkBevistigWijzigen.CssClass = "hidden";

    }

    protected void lnkWijzigFoto_Click(object sender, EventArgs e)
    {
        lblBevestiging.CssClass = "hidden";
        lnkBevestigFoto.CssClass = "btn btn-default";
        fuUploadFoto.CssClass = "btn btn-default btn-file";
        lnkCancelFoto.CssClass = "btn btn-default";
        lnkWijzigFoto.CssClass = "hidden";
    }

    protected void lnkCancelFoto_Click(object sender, EventArgs e)
    {
        lblBevestiging.CssClass = "hidden";
        lnkBevestigFoto.CssClass = "hidden";
        fuUploadFoto.CssClass = "hidden";
        lnkCancelFoto.CssClass = "hidden";
        lnkWijzigFoto.CssClass = "btn btn-default";
    }

    protected void lnkBevistigWijzigen_Click(object sender, EventArgs e)
    {
            clsGebruikerWebUpdate update = new clsGebruikerWebUpdate();
            update.IDGebruiker = Convert.ToInt32(Session["CurrentUserID"]);
            update.Straat = txtStraat.Text;
            update.Huisnummer = txtNummer.Text;
            update.EmailPersoonlijk = txtMail.Text;
            update.TelefoonNummer = txtTelefoon.Text;
            update.GSMNummer = txtGSM.Text;
            update.IDPostcode = Convert.ToInt32(ddlGemeente.SelectedValue);
            update.IDTaal = Convert.ToInt32(ddlTaal.SelectedValue);


            clsBLLGebruikerWebUpdate.Insert(update);
            lblBevestiging.CssClass = "";
            lnkCancel_Click(sender, e);


    }

    protected void lnkBevestigFoto_Click(object sender, EventArgs e)
    {
        FileUpload fu = fuUploadFoto;
        if (fu.HasFile)
        {
            lnkCancelFoto_Click(sender, e);
            lblBevestiging.CssClass = "";
            clsGebruikerWebUpdate Foto = new clsGebruikerWebUpdate();
            Foto.IDGebruiker = Convert.ToInt32(Session["CurrentUserID"]);
            Foto.Photo = fu.FileBytes;
            clsBLLGebruikerWebUpdate.Insert(Foto);

        }
    }
}