using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class clsCustomBLL : clsCommonBLL
    {

        /// <summary>
        /// Get by fk
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public ObservableCollection<T> GetDataListSpecific<T>(int id) where T : class, new()
        {
            //misschien henoemen naar SelectByFK ofzoiets?
            return GetDataList<T>(DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.SelectFilter, "", id));
        }
        public ObservableCollection<T> GetDataListSpecific<T>(int id, int idid) where T : class, new()
        {
            //misschien henoemen naar SelectByFK ofzoiets?
            return GetDataList<T>(DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.SelectFilter, "", id, idid));
        }
        public ObservableCollection<T> GetDataListSpecific<T>(string text) where T : class, new()
        {
            //misschien henoemen naar SelectByFK ofzoiets?
            return GetDataList<T>(DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.SelectFilter, "", text));
        }

        public T GetUserByEmail<T>(string email) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.getUserByMail<T>(email)).FirstOrDefault();
        }
        public T GetKlasByInschrijving<T>(int id) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.getKlasByInschrijving<T>(id)).FirstOrDefault();
        }

        public T GetUserByAccountName<T>(string username, string expectedType) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.getUserByAccountName<T>(username, expectedType)).FirstOrDefault();
        }

        public ObservableCollection<T> GetModuleByOpleidingID<T>(int iDOpleiding, int iDGebruiker) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.getModuleByOpleidingID<T>(iDOpleiding, iDGebruiker));
        }
        public ObservableCollection<T> InschrijvingSelectStudentIDValidated<T>(int iDGebruiker) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.getInschrijvingSelectStudentIDValidated<T>(iDGebruiker));
        }
        public ObservableCollection<T> GetModuleByOpleidingID<T>(int iDOpleiding) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.getModuleByOpleidingID<T>(iDOpleiding));
        }
        public ObservableCollection<T> GetModuleByGebruikerID<T>(int iDGebruiker) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.getModuleByGebruikerID<T>(iDGebruiker));
        }
        public ObservableCollection<T> GetIngeschrevenModulesStudentNietGevalideerd<T>(int iDGebruiker) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.GetIngeschrevenModulesStudentNietGevalideerd<T>(iDGebruiker));
        }
        public ObservableCollection<T> GetTicketInschrijvingByStudentID<T>(int iDGebruiker) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.GetTicketInschrijvingByStudentID<T>(iDGebruiker));
        }
        //idee: als de tussentabel niet veel nut heeft, een selectAll met ID meegeven aan de tussentabel, zodat we via een type kunnen filteren ofzoiets
        public ObservableCollection<T> GetLerarenByModuleID<T>(int iDGebruiker) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.GetLerarenByModuleID<T>(iDGebruiker));
        }
        public ObservableCollection<T> GetInschrijvingByModuleIDGebruikerID<T>(int iDModule, int iDGebruiker) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.GetInschrijvingByModuleIDGebruikerID<T>(iDModule, iDGebruiker));
        }
        public ObservableCollection<T> GetAlleModulesNietIngeschreven<T>(int id) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.GetAlleModulesNietIngeschreven<T>(id));
        }
        public ObservableCollection<T> GetAlleModulesIngeschreven<T>(int id) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.GetAlleModulesIngeschreven<T>(id));
        }



        #region Ticketing
        public int RequestTicketInschrijvingLock(int idTicket, int idGebruiker)
        {
            Dictionary<string, object> ticket = new Dictionary<string, object>();
            ticket = GetDataDictionary(DAL.clsCustomMethods.TicketInschrijvingRequestLock(idTicket, idGebruiker)).First();

            return getID(ticket);
        }


        private int getID(Dictionary<string, object> ticket)
        {
            object idmedewerker;
            if (ticket.TryGetValue("IDGebruikerMedewerker", out idmedewerker))
            {
                if (!idmedewerker.Equals(DBNull.Value))
                {
                    return Convert.ToInt32(idmedewerker);
                }
            }

            return 0;
        }

        public int RequestTicketAanwezigheidLock(int idTicket, int idGebruiker)
        {
            Dictionary<string, object> ticket = new Dictionary<string, object>();
            ticket = GetDataDictionary(DAL.clsCustomMethods.TicketAanwezigheidRequestLock(idTicket, idGebruiker)).FirstOrDefault();
            return getID(ticket);
        }

        public int RequestTicketWijzigingLock(int idTicket, int idGebruiker)
        {
            Dictionary<string, object> ticket = new Dictionary<string, object>();
            ticket = GetDataDictionary(DAL.clsCustomMethods.TicketWijzigingenRequestLock(idTicket, idGebruiker)).FirstOrDefault();
            return getID(ticket);
        }

        public ObservableCollection<T> TicketWijzigingSelectByDate<T>(DateTime updatedDT) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.TicketWijzigingSelectByDate<T>(updatedDT));
        }

        public ObservableCollection<T> TicketAanwezigheidSelectByDate<T>(DateTime updatedDT) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.TicketAanwezigheidSelectByDate<T>(updatedDT));
        }

        public ObservableCollection<T> TicketInschrijvingSelectByDate<T>(DateTime updatedDT) where T : class, new()
        {
            return GetDataList<T>(DAL.clsCustomMethods.TicketInschrijvingSelectByDate<T>(updatedDT));
        }

        #endregion
    }
}
