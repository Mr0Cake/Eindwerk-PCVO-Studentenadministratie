using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLLOpleiding
/// </summary>
public static class clsBLLStudieGebied
{
    public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();
    

    public static ObservableCollection<clsStudieGebied> Select()
    {
        return BLL.GetData<clsStudieGebied>();
    }

    public static clsStudieGebied Select(int id)
    {
        return BLL.GetData<clsStudieGebied>(id);
    }

    public static ObservableCollection<clsStudieGebied> SelectByFK(int fk)
    {
        return BLL.GetDataListSpecific<clsStudieGebied>(fk);
    }

    public static void Update(clsStudieGebied opl)
    {
        BLL.UpdateData(opl);
    }

    public static void Delete(clsStudieGebied opl)
    {
        BLL.DeleteData(opl);
    }

    public static void Insert(clsStudieGebied opl)
    {
        BLL.InsertData(opl);
    }

}