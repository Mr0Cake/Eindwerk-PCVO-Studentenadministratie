using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsBLLModule
/// </summary>
public static class clsBLLUitschrijven
{
    public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();


    public static ObservableCollection<clsTicketInschrijving> Select()
    {
        return BLL.GetData<clsTicketInschrijving>();
    }

    public static clsTicketInschrijving Select(int id)
    {
        return BLL.GetData<clsTicketInschrijving>(id);
    }

    public static void Update(clsTicketInschrijving geb)
    {
        BLL.UpdateData(geb);
    }

    public static void Delete(clsTicketInschrijving geb)
    {
        BLL.DeleteData(geb);
    }

    public static void Insert(clsTicketInschrijving geb)
    {
        BLL.InsertData(geb);
    }
}