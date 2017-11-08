using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsAcademieJaar : clsBaseModel
    {
        public clsAcademieJaar():base()
        {
   
        }

        private int iDAcademieJaar;

        public int IDAcademieJaar
        {
            get { return iDAcademieJaar; }
            set
            {
                if (iDAcademieJaar != value)
                {
                    iDAcademieJaar = value;
                    _ID = value;
                    RaisePropertyChanged("IDAcademieJaar");
                }
            }
        }
        private string academieJaar;

        public string AcademieJaar
        {
            get { return academieJaar; }
            set
            {
                if (academieJaar != value)
                {
                    academieJaar = value;
                    RaisePropertyChanged("AcademieJaar");
                }
            }
        }

        private DateTime startDatum;
        public DateTime StartDatum
        {
            get { return startDatum; }
            set
            {
                if (startDatum != value)
                {
                    startDatum = value;
                    RaisePropertyChanged("StartDatum");
                }
            }
        }
        private DateTime eindDatum;
        public DateTime EindDatum
        {
            get { return eindDatum; }
            set
            {
                if (eindDatum != value)
                {
                    eindDatum = value;
                    RaisePropertyChanged("EindDatum");
                }
            }
        }
    }
}
