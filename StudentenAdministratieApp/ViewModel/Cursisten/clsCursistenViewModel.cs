using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BLL.Extensions;
using System.Text;
using System.Threading.Tasks;

namespace StudentenAdministratieApp.ViewModel.Cursisten
{
    public class clsCursistenViewModel:ViewModelBase
    {
        

        //private static ObservableCollection<clsInschrijving> _Inschrijvingen;

        //public static ObservableCollection<clsInschrijving> Inschrijvingen
        //{
        //    get { return _Inschrijvingen = _Inschrijvingen ?? BLL.GetData<clsInschrijving>(); }
        //    set { _Inschrijvingen = value; }
        //}

        private clsPostcode _SelectedPostcode;

        public clsPostcode SelectedPostcode
        {
            get { return _SelectedPostcode; }
            set { _SelectedPostcode = value; Notify(); }
        }

        private clsTaal _SelectedTaal;

        public clsTaal SelectedTaal
        {
            get { return _SelectedTaal; }
            set { _SelectedTaal = value; Notify(); }
        }


        private clsNiveau _SelectedNiveau;

        public clsNiveau SelectedNiveau
        {
            get { return _SelectedNiveau; }
            set { _SelectedNiveau = value; Notify(); }
        }
    }
}
