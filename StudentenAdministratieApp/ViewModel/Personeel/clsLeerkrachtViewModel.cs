using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenAdministratieApp.ViewModel.Personeel
{
    public class clsLeerkrachtViewModel:ViewModelBase
    {
        private clsGebruiker _Leerkracht; 

        public clsGebruiker Leerkracht
        {
            get { return _Leerkracht; }
            set { _Leerkracht = value; }
        }

        private ObservableCollection<clsKlas> _KlasLeerkracht;

        public ObservableCollection<clsKlas> KlasLeerkracht
        {
            get { return _KlasLeerkracht = _KlasLeerkracht ?? BLL.GetData<clsKlas>() ; }
            set { _KlasLeerkracht = value; }
        }

        private ObservableCollection<clsModule> _ModulesLeerkracht;

        //public ObservableCollection<clsModule> ModulesLeerkracht
        //{
        //    get { return _ModulesLeerkracht = _ModulesLeerkracht ?? ; }
        //    set { _ModulesLeerkracht = value; }
        //}





    }
}
