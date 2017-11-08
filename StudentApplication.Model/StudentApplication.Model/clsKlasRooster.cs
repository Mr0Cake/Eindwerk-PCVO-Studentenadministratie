using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsKlasRooster : clsBaseModel
    {

        public clsKlasRooster() : base()
        {
 
        }


        private int iDSchedule;

        public int IDKlasRooster
        {
            get { return iDSchedule; }
            set
            {
                if (iDSchedule != value)
                {
                    iDSchedule = value;
                    _ID = value;
                    RaisePropertyChanged("IDKlasRooster");
                }
            }
        }
        private int iDGebruikerLeraar;

        public int IDGebruikerLeraar
        {
            get { return iDGebruikerLeraar; }
            set
            {
                if (iDGebruikerLeraar != value)
                {
                    iDGebruikerLeraar = value;
                    RaisePropertyChanged("IDGebruikerLeraar");
                }
            }
        }

        private int iDKlas;

        public int IDKlas
        {
            get { return iDKlas; }
            set
            {
                if (iDKlas != value)
                {
                    iDKlas = value;
                    RaisePropertyChanged("IDKlas");
                }
            }
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
                    RaisePropertyChanged("IDLokaal");
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

        #region KlasroosterControl
        private DateTime start;
        public DateTime Start
        {
            get { return startDatum; }
            set
            {
                if (start != value)
                {
                    start = value;
                    RaisePropertyChanged("StartDatum");
                }
            }
        }
        private DateTime end;
        public DateTime End
        {
            get { return eindDatum; }
            set
            {
                if (end != value)
                {
                    end = value;
                    RaisePropertyChanged("EindDatum");
                }
            }
        }
        private string id;
        private bool _allDay;
        public bool AllDay
        {
            get { return false; }
            set { _allDay = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;
        public string Name
        {
            get { return Klas.ModuleType.OfficieleNaam + " Lokaal: " + Lokaal.Naam + " Leraar: " + Leraar.Naam; }
            //get { return "Klasnaam komt hier"; }
            set { name = value; }
        }
        #endregion
        private clsKlas klas;
        public clsKlas Klas
        {
            get
            {
                //return klas ?? (klas = BLL.clsCustomBLL.GetAllWordsByListID(ListId));
                return klas ?? (klas = bllCommon.GetData<clsKlas>(IDKlas));
                //return _Words;
            }
            set { klas = value; }
        }
        private clsGebruiker leraar;
        public clsGebruiker Leraar
        {
            get
            {
                return leraar ?? (leraar = bllCommon.GetData<clsGebruiker>(IDGebruikerLeraar));
                //return _Words;
            }
            set { leraar = value; }
        }
        private clsLokaal lokaal;
        public clsLokaal Lokaal
        {
            get
            {
                return lokaal ?? (lokaal = bllCommon.GetData<clsLokaal>(IDLokaal));
                //return _Words;
            }
            set { lokaal = value; }
        }

    }
}
