using StudentApplication.Model;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for clsBLLGebruikerWebUpdate
/// </summary>
public static class clsBLLGebruikerWebUpdate
{

        public static BLL.clsCustomBLL BLL = new BLL.clsCustomBLL();


    public static ObservableCollection<clsGebruikerWebUpdate> Select()
    {
        return BLL.GetData<clsGebruikerWebUpdate>();
    }

    public static clsGebruikerWebUpdate Select(int id)
    {
        return BLL.GetData<clsGebruikerWebUpdate>(id);
    }

    public static void Update(clsGebruikerWebUpdate geb)
    {
        BLL.UpdateData(geb);
    }

    public static void Delete(clsGebruikerWebUpdate geb)
    {
        BLL.DeleteData(geb);
    }

    public static void Insert(clsGebruikerWebUpdate geb)
    {
        BLL.InsertData(geb);
    }
}
