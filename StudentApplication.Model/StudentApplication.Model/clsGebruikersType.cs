using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsGebruikersType : clsBaseModel
    {


        public clsGebruikersType() : base()
        {
  
        }


        private string typeNaam;

        public string TypeNaam
        {
            get { return typeNaam; }
            set
            {
                if (typeNaam != value)
                {
                    typeNaam = value;
                    RaisePropertyChanged("TypeNaam");
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
                    _ID = value;
                    RaisePropertyChanged("IDType");
                }
            }
        }
    }
}
