using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsBLLModule
/// </summary>
public static class clsBLLModule
{
    public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();


    public static ObservableCollection<clsModule> Select()
    {
        return BLL.GetData<clsModule>();
    }

    public static clsModule Select(int id)
    {
        return BLL.GetData<clsModule>(id);
    }

    public static void Update(clsModule geb)
    {
        BLL.UpdateData(geb);
    }

    public static void Delete(clsModule geb)
    {
        BLL.DeleteData(geb);
    }

    public static void Insert(clsModule geb)
    {
        BLL.InsertData(geb);
    }
}