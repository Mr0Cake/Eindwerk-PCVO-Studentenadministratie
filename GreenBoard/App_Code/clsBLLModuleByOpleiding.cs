using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for clsBLLModuleByOpleiding
/// </summary>
public static class clsBLLModuleByOpleiding
{
    public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();


    public static ObservableCollection<clsModuleByOpleiding> Select()
    {
        return BLL.GetData<clsModuleByOpleiding>();
    }

    public static clsModuleByOpleiding Select(int id)
    {
        return BLL.GetData<clsModuleByOpleiding>(id);
    }

    public static ObservableCollection<clsModuleByOpleiding> SelectByFK(int fk)
    {
        return BLL.GetDataListSpecific<clsModuleByOpleiding>(fk);
    }
    public static ObservableCollection<clsModuleByOpleiding> GetModuleByOpleidingID(int fk, int id)
    {
        return BLL.GetModuleByOpleidingID<clsModuleByOpleiding>(fk, id);
    }
    public static ObservableCollection<clsModuleByOpleiding> GetModuleByOpleiding(int fk)
    {
        return BLL.GetModuleByOpleidingID<clsModuleByOpleiding>(fk);
    }
    public static ObservableCollection<clsModuleByOpleiding> GetModuleByStudentID(int fk)
    {
        return BLL.GetModuleByGebruikerID<clsModuleByOpleiding>(fk);
    }
    public static ObservableCollection<clsModuleByOpleiding> GetIngeschrevenModulesStudentNietGevalideerd(int fk)
    { //usp_InschrijvingSelectStudentIDNotValidated
        return BLL.GetIngeschrevenModulesStudentNietGevalideerd<clsModuleByOpleiding>(fk);
    }
    public static ObservableCollection<clsModuleByOpleiding> GetTicketInschrijvingByStudentID(int fk)
    { //usp_TicketInschrijvingSelectStudentID
        return BLL.GetTicketInschrijvingByStudentID<clsModuleByOpleiding>(fk);
    }

    public static void Update(clsModuleByOpleiding modtyp)
    {
        BLL.UpdateData(modtyp);
    }

    public static void Delete(clsModuleByOpleiding modtyp)
    {
        BLL.DeleteData(modtyp);
    }

    public static void Insert(clsModuleByOpleiding modtyp)
    {
        BLL.InsertData(modtyp);
    }
}