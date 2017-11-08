using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsBLLModuleModuleType
/// </summary>
public class clsBLLModuleModuleType
{
    public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();


    public static ObservableCollection<clsModuleModuleType> Select()
    {
        return BLL.GetData<clsModuleModuleType>();
    }
    public static ObservableCollection<clsModuleModuleType> SelectAlleNietIngeschreven(int id)
    {
        return BLL.GetAlleModulesNietIngeschreven<clsModuleModuleType>(id);
    }
    public static ObservableCollection<clsModuleModuleType> SelectAlleIngeschreven(int id)
    {
        return BLL.GetAlleModulesIngeschreven<clsModuleModuleType>(id);
    }

    public static clsModuleModuleType Select(int id)
    {
        return BLL.GetData<clsModuleModuleType>(id);
    }

    public static void Update(clsModuleModuleType geb)
    {
        BLL.UpdateData(geb);
    }

    public static void Delete(clsModuleModuleType geb)
    {
        BLL.DeleteData(geb);
    }

    public static void Insert(clsModuleModuleType geb)
    {
        BLL.InsertData(geb);
    }
}