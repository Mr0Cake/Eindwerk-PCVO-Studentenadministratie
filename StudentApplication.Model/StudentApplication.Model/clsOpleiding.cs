using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsOpleiding : clsBaseModel
    {

        public clsOpleiding() : base()
        {

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
                    _ID = value;
                    RaisePropertyChanged("IDOpleiding");
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
        private string shortCut;

        public string ShortCut
        {
            get { return shortCut; }
            set
            {
                if (shortCut != value)
                {
                    shortCut = value;
                    RaisePropertyChanged("ShortCut");
                }
            }
        }
        private int iDStudieGebied;

        public int IDStudieGebied
        {
            get { return iDStudieGebied; }
            set
            {
                if (iDStudieGebied != value)
                {
                    iDStudieGebied = value;
                    RaisePropertyChanged("IDStudieGebied");
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
