using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsGebruikers_ADGroeps : clsBaseModel
    {

        public clsGebruikers_ADGroeps():base()
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
        private int iDADGroep;

        public int IDADGroep
        {
            get { return iDADGroep; }
            set
            {
                if (iDADGroep != value)
                {
                    iDADGroep = value;
                    RaisePropertyChanged("IDADGroep");
                }
            }
        }
    }
}
