using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADServices;
using StudentApplication.Model;
using System.Windows;
using System.ComponentModel;
using NetworkMessage;

namespace StudentenAdministratieApp.ViewModel
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            GetGebruiker();
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindowClosing);
        }

        void MainWindowClosing(object sender, CancelEventArgs e)
        {
            clsReceiveMessage.StopReceive = true;
            if (clsListUpdater.UpdateTask != null)
            {
                
                //clsListUpdater.UpdateTask.Dispose();
            }
                
        }

        private static string _Name;

        public static string Name
        {
            get { return _Name = string.IsNullOrEmpty(_Name) ? User.Gebruikersnaam : _Name; }
            set { _Name = value; }
        }

        private static clsGebruiker _User;

        public static clsGebruiker User
        {
            get { return _User; }
            set { _User = value; _Name = ""; }
        }


        private void GetGebruiker()
        {
            try
            {
                User = ADServices.clsServices.getCurrentUser();
                User.IDGebruiker = 2;
            }
            catch
            {
                if (MessageBox.Show("Kon niet inloggen.", "fout", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    
                    User = new clsGebruiker("Niet ingelogd");
                    User.IDGebruiker = 2;

                }
                else
                {
                    System.Environment.Exit(0);
                }
            }

        }
    }
}
