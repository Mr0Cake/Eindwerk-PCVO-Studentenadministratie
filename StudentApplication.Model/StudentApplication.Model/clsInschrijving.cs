using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsInschrijving : clsBaseModel
    {

        public clsInschrijving() : base()
        {
     
        }


        private int iDInschrijving;

        public int IDInschrijving
        {
            get { return iDInschrijving; }
            set
            {
                if (iDInschrijving != value)
                {
                    iDInschrijving = value;
                    _ID = value;
                    RaisePropertyChanged("IDInschrijving");
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
        private int iDModule;

        public int IDModule
        {
            get { return iDModule; }
            set
            {
                if (iDModule != value)
                {
                    iDModule = value;
                    RaisePropertyChanged("IDModule");
                }
            }
        }

        private int iDKlas;

        public int IDKlas
        {
            get { return iDKlas; }
            set { iDKlas = value; RaisePropertyChanged("IDKlas"); }
        }



        private bool _IsValidated;

        public bool IsValidated
        {
            get { return _IsValidated; }
            set { _IsValidated = value; }
        }



        private DateTime inschrijfDatum;

        public DateTime InschrijvingsDatum
        {
            get { return inschrijfDatum; }
            set
            {
                if (inschrijfDatum != value)
                {
                    inschrijfDatum = value;
                    RaisePropertyChanged("InschrijvingsDatum");
                }
            }
        }

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
        private ObservableCollection<clsKlasRooster> klasRooster;
        public ObservableCollection<clsKlasRooster> KlasRooster
        {
            get
            {
                //return klas ?? (klas = BLL.clsCustomBLL.GetAllWordsByListID(ListId));
                return klasRooster ?? (klasRooster = bllCustom.GetDataListSpecific<clsKlasRooster>(IDKlas));
                //return _Words;
            }
            set { klasRooster = value; }
        }
        private clsGebruiker gebruiker;
        public clsGebruiker Gebruiker
        {
            get
            {
                return gebruiker ?? (gebruiker = bllCommon.GetData<clsGebruiker>(IDGebruiker));
                //return _Words;
            }
            set { gebruiker = value; }
        }
        private clsModule module;
        public clsModule Module
        {
            get
            {
                return module ?? (module = bllCommon.GetData<clsModule>(IDModule));
                //return _Words;
            }
            set { module = value; }
        }


    }
}
