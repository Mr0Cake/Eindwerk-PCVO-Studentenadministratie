using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Custom Methods:
    /// Methode bevat de Stored Procedure naam. 
    /// Verder kan men gebruik maken van de Common Methods, geef de parameters door als object[]
    /// GetModuleTypeNaam
    /// </summary>
    public static class clsCustomMethods
    {
        public static DataTable getUserByMail<T>(string mail)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_gebruikerSelectMail", mail);
        }
        public static DataTable getKlasByInschrijving<T>(int id)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_getKlasByInschrijving", id);
        }
        public static DataTable GetAlleModulesNietIngeschreven<T>(int id)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_ModuleModuleTypeNietIngeschreven",id);
        }
        public static DataTable GetAlleModulesIngeschreven<T>(int id)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_ModuleModuleTypeIngeschreven", id);
        }
        public static DataTable getModuleByOpleidingID<T>(int iDOpleiding, int iDGebruiker)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_ModuleSelectByOpleidingIDGebruikerID", iDOpleiding, iDGebruiker);
        }
        public static DataTable getInschrijvingSelectStudentIDValidated<T>(int iDGebruiker)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_InschrijvingSelectStudentIDValidated", iDGebruiker);
        }
        public static DataTable getModuleByOpleidingID<T>(int iDOpleiding)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_ModuleSelectByOpleidingID", iDOpleiding);
        }
        public static DataTable getModuleByGebruikerID<T>(int iDGebruiker)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_ModuleSelectByGebruikerID", iDGebruiker);
        }

        public static DataTable getUserByAccountName<T>(string username, string expectedType = null) where T : class, new()
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_gebruikerSelectUserName", username, expectedType);
        }

        public static DataTable GetIngeschrevenModulesStudentNietGevalideerd<T>(int iDGebruiker)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_InschrijvingSelectStudentIDNotValidated", iDGebruiker);
        }
        public static DataTable GetLerarenByModuleID<T>(int iDModule)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_Modules_GebruikersLeraarsSelectByModuleID", iDModule);
        }
        public static DataTable GetInschrijvingByModuleIDGebruikerID<T>(int iDModule, int iDGebruiker)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_InschrijvingSelectByModuleIDAndGebruikerID", iDModule, iDGebruiker);
        }
        public static DataTable GetTicketInschrijvingByStudentID<T>(int iDGebruiker)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "usp_TicketInschrijvingSelectStudentID", iDGebruiker);
        }
        #region Tickets
        public static DataTable TicketInschrijvingRequestLock(int idInschrijvingTicket, int idGebruiker)
        {
            return DAL.clsCommonMethods.GetDataTableParams(enSqlCommandType.Custom, "usp_InschrijvingTicketLock", idInschrijvingTicket, idGebruiker);
        }

        public static DataTable TicketInschrijvingSelectByDate<T>(DateTime updatedDT)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Custom, "usp_InschrijvingTicketSelectByDate", updatedDT);
        }

        public static DataTable TicketAanwezigheidRequestLock(int idInschrijvingTicket, int idGebruiker)
        {
            return DAL.clsCommonMethods.GetDataTableParams(enSqlCommandType.Custom, "usp_AanwezigheidTicketLock", idInschrijvingTicket, idGebruiker);
        }

        public static DataTable TicketAanwezigheidSelectByDate<T>(DateTime updatedDT)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Custom, "usp_AanwezigheidTicketSelectByDate", updatedDT);
        }

        public static DataTable TicketWijzigingenRequestLock(int idTicket, int idGebruiker)
        {
            return DAL.clsCommonMethods.GetDataTableParams(enSqlCommandType.Custom, "usp_GebruikerWebUpdateLock", idTicket, idGebruiker);
        }

        public static DataTable TicketWijzigingSelectByDate<T>(DateTime updatedDT)
        {
            return DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Custom, "usp_GebruikerWebUpdateSelectByDate", updatedDT);
        }
        #endregion
    }
}
