using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Protocols;
using System.ServiceModel.Security;
using System.DirectoryServices;
using System.Security.Principal;
using BLL;
using StudentApplication.Model;

namespace ADServices
{
    public static class clsServices
    {
        // set up domain context
        
        private static string LDAP = "gr4test.local";

        // find current user
        private static UserPrincipal user;

        private static clsCustomBLL BLL = new clsCustomBLL();
        /// <summary>
        /// http://stackoverflow.com/questions/290548/validate-a-username-and-password-against-active-directory
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ValidateUser(string username, string password)
        {
            try
            {
                ////secure --> Kerberos/NTLM encryption http://www.codeproject.com/Articles/18742/Simple-Active-Directory-Authentication-Using-LDAP
                //LDAP://172.16.99.153
                DirectoryEntry dr = new DirectoryEntry("LDAP://192.168.18.140", username, password, AuthenticationTypes.Secure);
                DirectorySearcher ds = new DirectorySearcher(dr);
                ds.FindOne();
                return true;
            }
            catch {
                try
                {
                    DirectoryEntry dr = new DirectoryEntry("LDAP://192.168.18.141", username, password, AuthenticationTypes.Secure);
                    DirectorySearcher ds = new DirectorySearcher(dr);
                    ds.FindOne();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }

        public static List<clsADGroep> getGroups()
        {
            List<clsADGroep> groepen = new List<clsADGroep>();
            foreach (Principal p  in user.GetAuthorizationGroups())
            {
                if (p is GroupPrincipal)
                    groepen.Add(new clsADGroep { GroepNaam = ((GroupPrincipal)p).Name });
            }
            return groepen;
        }

        
        

        public static clsGebruiker getCurrentUser()
        {
            clsGebruiker currentuser = null;
            user = UserPrincipal.Current;
            string username = "";
            if (user != null)
            {
                try
                {
                    username = user.SamAccountName;
                    string email = user.EmailAddress;
                    currentuser = BLL.GetUserByAccountName<clsGebruiker>(username, "Personeel");
                }
                catch(Exception e)
                {
                    Console.WriteLine("getCurrentUser() Failed\r\n"+e.Message);
                }
            }else
            {
                return new clsGebruiker("Niet ingelogd");
            }
            
            return currentuser;
        }

        public static string getCurrentUserName()
        {
            user = UserPrincipal.Current;
            return user.Name + " " + user.DisplayName;
        }
        

    }
}
