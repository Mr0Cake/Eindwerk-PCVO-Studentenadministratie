using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLLOpleiding
/// </summary>
public static class clsBLLTaal
{
    public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();


    public static ObservableCollection<clsTaal> Select()
    {
        return BLL.GetData<clsTaal>();
    }

    public static clsTaal Select(int id)
    {
        return BLL.GetData<clsTaal>(id);
    }

    public static ObservableCollection<clsTaal> SelectByFK(int fk)
    {
        return BLL.GetDataListSpecific<clsTaal>(fk);
    }

    public static void Update(clsTaal opl)
    {
        BLL.UpdateData(opl);
    }

    public static void Delete(clsTaal opl)
    {
        BLL.DeleteData(opl);
    }

    public static void Insert(clsTaal opl)
    {
        BLL.InsertData(opl);
    }

}
