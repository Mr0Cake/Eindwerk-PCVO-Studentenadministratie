using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Members_Afwezigheid : BasePage
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
        this.txtDatum.Text = DateTime.Now.ToString("yyyy-MM-dd");
        //txtDatum.TextMode = TextBoxMode.Date;

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

        if (FileUpload1.HasFile)
        {

            // Create the file attachment for this e-mail message.


            Attachment data = new Attachment(FileUpload1.FileContent, FileUpload1.FileName);

            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(FileUpload1.FileName);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(FileUpload1.FileName);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(FileUpload1.FileName);

            // Add the file attachment to this e-mail message.
            mailMessage.Attachments.Add(data);

        }
        mailMessage.Body = txtReden.Text + Environment.NewLine + "Datum: " + txtDatum.Text;
        SmtpClient smtpClient = new SmtpClient("smtp.live.com");
        smtpClient.EnableSsl = true;
        smtpClient.Port = 587;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential("aspusertestfromkurtverheyen@outlook.com", "ASPdotnet");
        smtpClient.Send(mailMessage);
        lblBevestiging.Text = "Afwezigheid is verzonden";
        txtReden.Text = "";
    }


}