using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using StudentApplication.Model;

/// <summary>
/// Summary description for clsBLLModuleType
/// </summary>
public static class clsBLLModuleType
{
    public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();


    public static ObservableCollection<clsModuleType> Select()
    {
        return BLL.GetData<clsModuleType>();
    }

    public static clsModuleType Select(int id)
    {
        return BLL.GetData<clsModuleType>(id);
    }

    public static ObservableCollection<clsModuleType> SelectByFK(int fk)
    {
        return BLL.GetDataListSpecific<clsModuleType>(fk);
    }

    public static void Update(clsModuleType modtyp)
    {
        BLL.UpdateData(modtyp);
    }

    public static void Delete(clsModuleType modtyp)
    {
        BLL.DeleteData(modtyp);
    }

    public static void Insert(clsModuleType modtyp)
    {
        BLL.InsertData(modtyp);
    }
}