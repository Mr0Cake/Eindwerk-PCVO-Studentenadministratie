using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsModules_GebruikersLeraars : clsBaseModel
    {

        public clsModules_GebruikersLeraars() : base()
        {

        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set
            {
                if (iD != value)
                {
                    iD = value;
                    _ID = value;
                    RaisePropertyChanged("ID");
                }
            }
        }
        private int iDGebruiker;

        public int IDGebruiker
        {
            get { return iDGebruiker; }
            set
            {
                if (iDGebruiker != value)
                {
                    iDGebruiker = value;
                    RaisePropertyChanged("IDGebruiker");
                }
            }
        }
        private int iDModule;

        public int IDModule
        {
            get { return iDModule; }
            set
            {
                if (iDModule != value)
                {
                    iDModule = value;
                    RaisePropertyChanged("IDModule");
                }
            }
        }
    }
}
