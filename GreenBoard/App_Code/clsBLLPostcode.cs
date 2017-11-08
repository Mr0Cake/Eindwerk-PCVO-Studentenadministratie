using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsBLLPostcode
/// </summary>
public static class clsBLLPostcode
{
    public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();

    public static clsPostcode Select(int id)
    {
        return BLL.GetData<clsPostcode>(id);
    }
    public static ObservableCollection<clsPostcode> SelectByFK(string fk)
    {
        return BLL.GetDataListSpecific<clsPostcode>(fk);
    }

    public static void Update(clsPostcode geb)
    {
        BLL.UpdateData(geb);
    }

    public static void Delete(clsPostcode geb)
    {
        BLL.DeleteData(geb);
    }

    public static void Insert(clsPostcode geb)
    {
        BLL.InsertData(geb);
    }

    //public static ObservableCollection<string> GetCompletionList(string prefixText, int count)
    //{
    //    ObservableCollection<clsPostcode> lijst = BLL.GetDataListSpecific<clsPostcode>(prefixText);
    //    ObservableCollection<string> returnLijst = new ObservableCollection<string>();
    //    foreach(clsPostcode item in lijst)
    //    {
    //        returnLijst.Add(item.Postcode);
    //    }
    //    return returnLijst;

    //}

 
}