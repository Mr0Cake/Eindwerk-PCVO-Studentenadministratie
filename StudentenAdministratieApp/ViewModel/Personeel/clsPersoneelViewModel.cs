using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApplication.Model;
using System.Collections.ObjectModel;
using BLL.Extensions;

namespace StudentenAdministratieApp.ViewModel.Personeel
{
    public class clsPersoneelViewModel:ViewModelBase
    {
        private ObservableCollection<clsGebruiker> _Personeel;

        public ObservableCollection<clsGebruiker> Personeel
        {
            get { return _Personeel = _Personeel ?? Gebruikers.Where(x => Modules.ToList().FindIndex(p => p.IDGebruikerCoordinator == x.getID) > -1 || KlasRoosters.ToList().FindIndex(p => p.IDGebruikerLeraar == x.getID && p.EindDatum>DateTime.Now.AddYears(-1)) > -1).ToList().ToObservableCollection(); }
            set { _Personeel = value; }
        }

        private clsGebruiker _SelectedLeerkracht;

        public clsGebruiker SelectedLeerkracht
        {
            get { return _SelectedLeerkracht; }
            set { _SelectedLeerkracht = value; NotifyPropertyChanged("SelectedLeerkracht"); }
        }

        private clsGebruiker _SelectedPersoneel;

        public clsGebruiker SelectedPersoneel
        {
            get { return _SelectedPersoneel; }
            set { _SelectedPersoneel = value; NotifyPropertyChanged("SelectedPersoneel"); }
        }

        
        


    }
}
