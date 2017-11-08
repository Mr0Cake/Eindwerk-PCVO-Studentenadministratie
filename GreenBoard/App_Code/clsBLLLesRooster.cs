using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using StudentApplication.Model;

/// <summary>
/// Summary description for clsBLLLesRooster
/// </summary>
public static class clsBLLLesRooster
{

    public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();


    public static ObservableCollection<clsKlasRooster> Select()
    {
        return BLL.GetData<clsKlasRooster>();
    }

    public static clsKlasRooster Select(int id)
    {
        return BLL.GetData<clsKlasRooster>(id);
    }

    public static void Update(clsKlasRooster geb)
    {
        BLL.UpdateData(geb);
    }

    public static void Delete(clsKlasRooster geb)
    {
        BLL.DeleteData(geb);
    }

    public static void Insert(clsKlasRooster geb)
    {
        BLL.InsertData(geb);
    }
}

