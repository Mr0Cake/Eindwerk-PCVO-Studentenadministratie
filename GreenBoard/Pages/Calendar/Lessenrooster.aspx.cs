using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentApplication.Model;
using System.Collections.ObjectModel;

public partial class Pages_Members_Lessenroosters : BasePage
{
    private static BLL.clsCommonBLL bllCommon = new BLL.clsCommonBLL();
    private static BLL.clsCustomBLL bllCustom = new BLL.clsCustomBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userID = Convert.ToInt32(Session["CurrentUserID"]);
        ObservableCollection<clsInschrijving> inschrijvingen = bllCustom.InschrijvingSelectStudentIDValidated<clsInschrijving>(userID);
        ObservableCollection<clsKlasRooster> Klasroosters = new ObservableCollection<clsKlasRooster>();
        ObservableCollection<clsKlas> Klassen = bllCommon.GetData<clsKlas>();
        for (int j = 0; j < inschrijvingen.Count; j++)
        {
            for (int i = 0; i < inschrijvingen[j].KlasRooster.Count; i++)
            {
                clsKlasRooster kl = inschrijvingen[j].KlasRooster[i];
                kl.Klas = Klassen.ToList().Find(p => p.IDKlas == kl.IDKlas);
                Klasroosters.Add(kl);
            }
        }

        WeekViewCalendar.DataSource = Klasroosters;
        if (!Page.IsPostBack)
        {
            WeekViewCalendar.StartDate = DateTime.Now;
            //ViewState["StartDatumKalender"] = 1;

        }
        else
        {
            //if(Convert.ToInt32(ViewState["StartDatumKalender"]) == 0)
            //{
            //    WeekViewCalendar.StartDate = WeekViewCalendar.StartDate.AddDays(-7);
            //}
            //else if(Convert.ToInt32(ViewState["StartDatumKalender"]) == 1)
            //{
            //    WeekViewCalendar.StartDate = DateTime.Now;
            //}
            //else if(Convert.ToInt32(ViewState["StartDatumKalender"]) == 2)
            //{
            //    WeekViewCalendar.StartDate = WeekViewCalendar.StartDate.AddDays(+7);
            //}
            //WeekViewCalendar.StartDate = Convert.ToDateTime(ViewState["StartDatumKalender"]);
        }
        DataBind();

    }

    protected void lnkVorigeWeek_Click(object sender, EventArgs e)
    {
        WeekViewCalendar.StartDate = WeekViewCalendar.StartDate.AddDays(-7);
        Calendar1.SelectedDate = WeekViewCalendar.StartDate;
        //ViewState["StartDatumKalender"] = 0;
    }

    protected void lnkDezeWeek_Click(object sender, EventArgs e)
    {
        WeekViewCalendar.StartDate = WeekViewCalendar.StartDate = DateTime.Now;
        Calendar1.SelectedDate = WeekViewCalendar.StartDate;

        //ViewState["StartDatumKalender"] = 1;

    }

    protected void lnkVolgendeWeek_Click(object sender, EventArgs e)
    {
        WeekViewCalendar.StartDate = WeekViewCalendar.StartDate.AddDays(7);
        Calendar1.SelectedDate = WeekViewCalendar.StartDate;

        //ViewState["StartDatumKalender"] = 2;

    }


    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        WeekViewCalendar.StartDate = Calendar1.SelectedDate;
    }
}