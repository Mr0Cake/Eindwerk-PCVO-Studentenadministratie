using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsTaal : clsBaseModel
    {


        public clsTaal() : base()
        {

        }


        private int iDTaal;

        public int IDTaal
        {
            get { return iDTaal; }
            set
            {
                if (iDTaal != value)
                {
                    iDTaal = value;
                    _ID = value;
                    RaisePropertyChanged("IDTaal");
                }
            }
        }
        private string naam;

        public string TaalNaam
        {
            get { return naam; }
            set
            {
                if (naam != value)
                {
                    naam = value;
                    RaisePropertyChanged("TaalNaam");
                }
            }
        }

        public override string ToString()
        {
            return TaalNaam;
        }
    }
}
