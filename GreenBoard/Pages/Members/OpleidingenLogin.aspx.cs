using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using StudentApplication.Model;

public partial class Pages_Members_Opleidingen : BasePage
{
    //public static BLL.clsBLLAdapter<clsStudieGebied> BLLStudieGebied = new BLL.clsBLLAdapter<clsStudieGebied>();
    //public static BLL.clsBLLAdapter<clsOpleiding> BLLL = new BLL.clsBLLAdapter<clsOpleiding>();
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
}