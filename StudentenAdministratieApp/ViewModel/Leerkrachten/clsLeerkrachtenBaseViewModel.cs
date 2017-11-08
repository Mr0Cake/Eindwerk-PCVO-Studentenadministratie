using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Extensions;

namespace StudentenAdministratieApp.ViewModel.Leerkrachten
{
    public class clsLeerkrachtenBaseViewModel : ViewModelBase
    {
        private ObservableCollection<clsGebruiker> _Leerkrachten;

        public ObservableCollection<clsGebruiker> Leerkrachten
        {
            get
            {
                return _Leerkrachten = _Leerkrachten ?? Gebruikers.Where(p => Gebruikers_TypeGebruikers.ToList().FindIndex(o => o.IDGebruiker == p.IDGebruiker && GebruikerTypes.ToList().Find(z => z.TypeNaam.Equals("Docent")).IDType ==o.IDType)>-1).ToObservableCollection();
            }
            set { _Leerkrachten = value; }
        }


        public clsGebruiker _SelectedLeerkracht;

        public clsGebruiker SelectedLeerkracht
        {
            get { return _SelectedLeerkracht; }
            set
            {
                _SelectedLeerkracht = value;
                Notify();
                UpdateLists(value);
            }
        }

        


        public virtual void UpdateLists(clsGebruiker g)
        {

        }
    }
}
