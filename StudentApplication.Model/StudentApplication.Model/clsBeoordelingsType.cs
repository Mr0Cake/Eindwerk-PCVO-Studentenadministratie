using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsBeoordelingsType : clsBaseModel
    {
        
        public clsBeoordelingsType():base()
{
            
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
                    _ID = value;
                    RaisePropertyChanged("IDType");
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
    }
}
