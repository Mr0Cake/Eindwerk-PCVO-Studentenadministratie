using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Members_Vragen : BasePage
{
    clsGebruiker geb = new clsGebruiker();

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
        geb = clsBLLGebruiker.Select(getUserID());
       
    }

    protected void lnkVerstuur_Click(object sender, EventArgs e)
    {
        lblBevestiging.CssClass = "";

        StuurMail();

    }

    private void StuurMail()
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.To.Add("verheyenkurt90@hotmail.com");
        mailMessage.From = new MailAddress("aspusertestfromkurtverheyen@outlook.com");
        mailMessage.Subject = "Vraag van " + geb.Gebruikersnaam;
        mailMessage.Body = txtVraag.Text;
        SmtpClient smtpClient = new SmtpClient("smtp.live.com");
        smtpClient.EnableSsl = true;
        smtpClient.Port = 587;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential("aspusertestfromkurtverheyen@outlook.com", "ASPdotnet");
        smtpClient.Send(mailMessage);
        lblBevestiging.Text = "Vraag is verzonden";
    }
}