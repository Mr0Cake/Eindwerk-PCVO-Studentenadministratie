using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using StudentApplication.Model;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using MessageProvider;
using ADServices;


namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            clsGebruiker gb = ADServices.clsServices.getCurrentUser();
            if (gb != null)
            {
                Console.WriteLine(gb.Gebruikersnaam);
            }else
            {
                Console.WriteLine("gb = null");
            }
            

            MessageProvider.SendMessage.Mail("verheyenkurt90@hotmail.com", "Test");
            Console.ReadLine();
        }
    }
}
