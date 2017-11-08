using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsGebruikers_TypeGebruikers : clsBaseModel
    {


        public clsGebruikers_TypeGebruikers() : base()
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
        private int iDType;

        public int IDType
        {
            get { return iDType; }
            set
            {
                if (iDType != value)
                {
                    iDType = value;
                    RaisePropertyChanged("IDType");
                }
            }
        }
    }
}
