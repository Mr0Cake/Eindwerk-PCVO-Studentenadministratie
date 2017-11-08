using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsNiveau : clsBaseModel
    {


        public clsNiveau() : base()
        {

        }


        private int _IDNiveau;

        public int IDNiveau
        {
            get { return _IDNiveau; }
            set { _IDNiveau = value; RaisePropertyChanged("IDNiveau"); }
        }

        private string _Naam;

        public string Naam
        {
            get { return _Naam; }
            set { _Naam = value; RaisePropertyChanged("Naam"); }
        }

        public override string ToString()
        {
            return Naam;
        }

    }
}
