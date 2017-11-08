using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsResultaat : clsBaseModel
    {

        public clsResultaat() : base()
        {

        }


        private int iDResultaat;

        public int IDResultaat
        {
            get { return iDResultaat; }
            set
            {
                if (iDResultaat != value)
                {
                    iDResultaat = value;
                    _ID = value;
                    RaisePropertyChanged("IDResultaat");
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
        private int iDBeoordelingsType;

        public int IDBeoordelingsType
        {
            get { return iDBeoordelingsType; }
            set
            {
                if (iDBeoordelingsType != value)
                {
                    iDBeoordelingsType = value;
                    RaisePropertyChanged("IDBeoordelingsType");
                }
            }
        }
        private int iDGebruikerStudent;

        public int IDGebruikerStudent
        {
            get { return iDGebruikerStudent; }
            set
            {
                if (iDGebruikerStudent != value)
                {
                    iDGebruikerStudent = value;
                    RaisePropertyChanged("IDGebruikerStudent");
                }
            }
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
                    RaisePropertyChanged("IDInschrijving");
                }
            }
        }
        private int behaaldePunten;

        public int BehaaldePunten
        {
            get { return behaaldePunten; }
            set
            {
                if (behaaldePunten != value)
                {
                    behaaldePunten = value;
                    RaisePropertyChanged("BehaaldePunten");
                }
            }
        }
        private int maximumPunten;

        public int MaximumPunten
        {
            get { return maximumPunten; }
            set
            {
                if (maximumPunten != value)
                {
                    maximumPunten = value;
                    RaisePropertyChanged("MaximumPunten");
                }
            }
        }
        private string advies;

        public string Advies
        {
            get { return advies; }
            set
            {
                if (advies != value)
                {
                    advies = value;
                    RaisePropertyChanged("Advies");
                }
            }
        }
        private bool gedelibereerd;

        public bool Gedelibereerd
        {
            get { return gedelibereerd; }
            set
            {
                if (gedelibereerd != value)
                {
                    gedelibereerd = value;
                    RaisePropertyChanged("Gedelibereerd");
                }
            }
        }
    }
}
