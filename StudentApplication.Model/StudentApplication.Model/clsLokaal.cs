using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsLokaal : clsBaseModel
    {

        public clsLokaal() : base()
        {

        }

        private int iDLokaal;

        public int IDLokaal
        {
            get { return iDLokaal; }
            set
            {
                if (iDLokaal != value)
                {
                    iDLokaal = value;
                    _ID = value;
                    RaisePropertyChanged("IDLokaal");
                }
            }
        }
        private string naam;

        public string Naam
        {
            get { return naam; }
            set
            {
                if (naam != value)
                {
                    naam = value;
                    RaisePropertyChanged("Naam");
                }
            }
        }
        private int iDVestiging;

        public int IDVestiging
        {
            get { return iDVestiging; }
            set
            {
                if (iDVestiging != value)
                {
                    iDVestiging = value;
                    RaisePropertyChanged("IDVestiging");
                }
            }
        }
    }
}
