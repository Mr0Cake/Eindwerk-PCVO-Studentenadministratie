using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsADGroep : clsBaseModel
    {

        public clsADGroep():base()
        {
           
        }

        private int iDADGroup;

        public int IDADGroep
        {
            get { return iDADGroup; }
            set
            {
                if (iDADGroup != value)
                {
                    iDADGroup = value;
                    _ID = value;
                    RaisePropertyChanged("IDADGroep");
                }
            }
        }
        private string naam;

        public string GroepNaam
        {
            get { return naam; }
            set
            {
                if (naam != value)
                {
                    naam = value;
                    RaisePropertyChanged("GroepNaam");
                }
            }
        }
    }
}
