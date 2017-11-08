using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsAanwezigheid : clsBaseModel
    {
        public clsAanwezigheid():base()
        {
           
        }

        private int iDAanwezigheid;

        public int IDAanwezigheid
        {
            get { return iDAanwezigheid; }
            set
            {
                if (iDAanwezigheid != value)
                {
                    iDAanwezigheid = value;
                    _ID = value;
                    RaisePropertyChanged("IDAanwezigheid");
                }
            }
        }

        private bool? isAanwezig;

        public bool? IsAanwezig
        {
            get { return isAanwezig; }
            set { isAanwezig = value; RaisePropertyChanged("IsAanwezig"); }
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
        private int iDKlasRooster;

        public int IDLesrooster
        {
            get { return iDKlasRooster; }
            set
            {
                if (iDKlasRooster != value)
                {
                    iDKlasRooster = value;
                    RaisePropertyChanged("IDLesrooster");
                }
            }
        }

        public override string ToString()
        {
            string output = "";
            output += IDGebruiker;
            if(IsAanwezig == null)
            {
                output += "null";
            }else
            {
                output += IsAanwezig.ToString();
            }
            return output;
        }
    }
}
