using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLLOpleiding
/// </summary>
public static class clsBLLOpleiding
{
    public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();
    

    public static ObservableCollection<clsOpleiding> Select()
    {
        return BLL.GetData<clsOpleiding>();
    }

    public static clsOpleiding Select(int id)
    {
        return BLL.GetData<clsOpleiding>(id);
    }

    public static ObservableCollection<clsOpleiding> SelectByFK(int fk)
    {
        return BLL.GetDataListSpecific<clsOpleiding>(fk);
    }

    public static void Update(clsOpleiding opl)
    {
        BLL.UpdateData(opl);
    }

    public static void Delete(clsOpleiding opl)
    {
        BLL.DeleteData(opl);
    }

    public static void Insert(clsOpleiding opl)
    {
        BLL.InsertData(opl);
    }

}