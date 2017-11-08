using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsModule : clsBaseModel
    {

        public clsModule() : base()
        {

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
                    _ID = value;
                    RaisePropertyChanged("IDModule");
                }
            }
        }

        private int iDModuleType;
        public int IDModuleType
        {
            get { return iDModuleType; }
            set
            {
                if (iDModuleType != value)
                {
                    iDModuleType = value;
                    RaisePropertyChanged("IDModuleType");
                }
            }
        }

        private int iDResultaatToelating;
        public int IDResultaatToekenning
        {
            get { return iDResultaatToelating; }
            set
            {
                if (iDResultaatToelating != value)
                {
                    iDResultaatToelating = value;
                    RaisePropertyChanged("IDResultaatToekenning");
                }
            }
        }

        private int iDGebruikerCoordinator;
        public int IDGebruikerCoordinator
        {
            get { return iDGebruikerCoordinator; }
            set
            {
                if (iDGebruikerCoordinator != value)
                {
                    iDGebruikerCoordinator = value;
                    RaisePropertyChanged("IDGebruikerCoordinator");
                }
            }
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
                    RaisePropertyChanged("IDAcademieJaar");
                }
            }
        }

        private int iDOpleiding;
        public int IDOpleiding
        {
            get { return iDOpleiding; }
            set
            {
                if (iDOpleiding != value)
                {
                    iDOpleiding = value;
                    RaisePropertyChanged("IDOpleiding");
                }
            }
        }

        private int semester;
        public int Semester
        {
            get { return semester; }
            set
            {
                if (semester != value)
                {
                    semester = value;
                    RaisePropertyChanged("Semester");
                }
            }
        }

        private int scoreToPass;
        public int PuntenOmTeSlagen
        {
            get { return scoreToPass; }
            set
            {
                if (scoreToPass != value)
                {
                    scoreToPass = value;
                    RaisePropertyChanged("PuntenOmTeSlagen");
                }
            }
        }

        private int maximumInschrijvingen;
        public int MaximumInschrijvingen
        {
            get { return maximumInschrijvingen; }
            set
            {
                if (maximumInschrijvingen != value)
                {
                    maximumInschrijvingen = value;
                    RaisePropertyChanged("MaximumInschrijvingen");
                }
            }
        }

        private int totaalAantalUren;
        public int TotaalAantalUren
        {
            get { return totaalAantalUren; }
            set
            {
                if (totaalAantalUren != value)
                {
                    totaalAantalUren = value;
                    RaisePropertyChanged("TotaalAantalUren");
                }
            }
        }

        /// <summary>
        /// Is inschrijven nog steeds mogelijk?
        /// </summary>
        /// <returns>true wanneer op dit moment ingeschreven kan worden, false als de tijd verstreken is</returns>
        public bool KanInschrijven()
        {
            try
            {
                return DateTime.Now < StartDatum.AddDays((EindDatum - StartDatum).Days / 3);
            }
            catch (ArgumentNullException)
            {
                return false;
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
