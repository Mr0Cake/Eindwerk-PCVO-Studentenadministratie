using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsVestiging : clsBaseModel
    {


        public clsVestiging() : base()
        {

        }


        private int iDVestiging;

        public int IDVestiging
        {
            get { return iDVestiging; }
            set
            {
                if (iDVestiging != value)
                {
                    iDVestiging = value;
                    _ID = value;
                    RaisePropertyChanged("IDVestiging");
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
        private string straat;

        public string Straat
        {
            get { return straat; }
            set
            {
                if (straat != value)
                {
                    straat = value;
                    RaisePropertyChanged("Straat");
                }
            }
        }
        private string huisNummer;

        public string HuisNummer
        {
            get { return huisNummer; }
            set
            {
                if (huisNummer != value)
                {
                    huisNummer = value;
                    RaisePropertyChanged("HuisNummer");
                }
            }
        }
        private int iDPostCode;

        public int IDPostCode
        {
            get { return iDPostCode; }
            set
            {
                if (iDPostCode != value)
                {
                    iDPostCode = value;
                    RaisePropertyChanged("IDPostCode");
                }
            }
        }
        private string telefoon;

        public string Telefoon
        {
            get { return telefoon; }
            set
            {
                if (telefoon != value)
                {
                    telefoon = value;
                    RaisePropertyChanged("Telefoon");
                }
            }
        }

    }
}
