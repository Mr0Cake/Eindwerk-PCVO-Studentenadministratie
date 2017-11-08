using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsBLLModule
/// </summary>
public static class clsBLLInschrijving
{
    public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();


    public static ObservableCollection<clsInschrijving> Select()
    {
        return BLL.GetData<clsInschrijving>();
    }
   

    public static clsInschrijving Select(int id)
    {
        return BLL.GetData<clsInschrijving>(id);
    }

    public static void Update(clsInschrijving geb)
    {
        BLL.UpdateData(geb);
    }

    public static void Delete(clsInschrijving geb)
    {
        BLL.DeleteData(geb);
    }

    public static void Insert(clsInschrijving geb)
    {
        BLL.InsertData(geb);
    }
}