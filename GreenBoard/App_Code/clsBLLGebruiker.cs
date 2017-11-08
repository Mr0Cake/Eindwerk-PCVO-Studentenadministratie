using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using StudentApplication.Model;

/// <summary>
/// Summary description for clsBLLGebruiker
/// </summary>
public static class clsBLLGebruiker
{
       public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();


    public static ObservableCollection<clsGebruiker> Select()
    {
        return BLL.GetData<clsGebruiker>();
    }

    public static clsGebruiker Select(int id)
    {
        return BLL.GetData<clsGebruiker>(id);
    }

    public static void Update(clsGebruiker geb)
    {
        BLL.UpdateData(geb);
    }

    public static void Delete(clsGebruiker geb)
    {
        BLL.DeleteData(geb);
    }

    public static void Insert(clsGebruiker geb)
    {
        BLL.InsertData(geb);
    }
}
