using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApplication.Model.Helpers;
using MvvmValidation;

namespace StudentApplication.Model
{
    public class clsGebruiker : clsBaseModel
    {
        public clsGebruiker():base()
        {
            ConfigureValidationRules();
        }

        protected override void ConfigureValidationRules()
        {
            //Validator.AddRule(() => Naam, () => RuleResult.Assert(!string.IsNullOrEmpty(Naam), "Naam mag niet leeg zijn."));
            //Validator.AddRule(() => Voornaam, () => RuleResult.Assert(!string.IsNullOrEmpty(Voornaam), "Voornaam mag niet leeg zijn."));
            //Validator.AddRule(() => GeboorteDatum, () => RuleResult.Assert(GeboorteDatum != null, "Vul de datum in."));
            //Validator.AddRule(() => GeboorteDatum, () => RuleResult.Assert(GeboorteDatum > DateTime.MinValue, "Vul de datum in."));
            //Validator.AddRule(() => Nationaliteit, () => RuleResult.Assert(!string.IsNullOrEmpty(Nationaliteit), "Nationaliteit mag niet leeg zijn."));
            //Validator.AddRule(() => Geslacht, () => RuleResult.Assert(!string.IsNullOrEmpty(Geslacht), "Geslacht mag niet leeg zijn."));
            //Validator.AddRule(() => Straat, () => RuleResult.Assert(!string.IsNullOrEmpty(Straat), "Straat mag niet leeg zijn."));
            //Validator.AddRule(() => HuisNummer, () => RuleResult.Assert(!string.IsNullOrEmpty(HuisNummer), "Huisnummer mag niet leeg zijn."));
            ////Validator.AddRule(() => IDPostCode, () => RuleResult.Assert(IDPostCode > 0, "Postcode moet ingevuld zijn."));
            //Validator.AddRule(() => RijksregisterNummer, () => RuleResult.Assert(!string.IsNullOrEmpty(RijksregisterNummer), "Rijksregisternummer moet ingevuld zijn."));
            //Validator.AddRule(() => GSMNummer, () => RuleResult.Assert(!string.IsNullOrEmpty(GSMNummer) && isNumber(GSMNummer), "GSM nummer is niet geldig"));
            //Validator.AddRule(() => EmailPersoonlijk, () => RuleResult.Assert(!string.IsNullOrEmpty(EmailPersoonlijk), "Persoonlijke email moet ingevuld zijn."));
            //Validator.AddRule(() => EmailPersoonlijk, () => RuleResult.Assert(EmailPersoonlijk.IndexOf("@") > 2 && EmailPersoonlijk.IndexOf(".") < EmailPersoonlijk.Length - 2, "Persoonlijke email moet een geldige mail zijn."));
        }

        private bool isNumber(string input)
        {
            int nummer = -1;
            if(int.TryParse(input, out nummer)){
                return true;
            }
            return false;
        }

        public clsGebruiker(string naam):this()
        {
            Gebruikersnaam = naam;
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
                    _ID = value;
                    RaisePropertyChanged("IDGebruiker");
                }
            }
        }
        private string gebruikersNaam;

        public string Gebruikersnaam
        {
            get { return gebruikersNaam; }
            set
            {
                if (gebruikersNaam != value)
                {
                    gebruikersNaam = value;
                    RaisePropertyChanged("Gebruikersnaam");
                }
            }
        }
        private string familieNaam;

        public string Naam
        {
            get { return familieNaam; }
            set
            {
                if (familieNaam != value)
                {
                    familieNaam = value;
                    RaisePropertyChanged("Naam");
                }
            }
        }
        private string voorNaam;

        public string Voornaam
        {
            get { return voorNaam; }
            set
            {
                if (voorNaam != value)
                {
                    voorNaam = value;
                    RaisePropertyChanged("VoorNaam");
                }
            }
        }
        private DateTime geboorteDatum;

        public DateTime GeboorteDatum
        {
            get { return geboorteDatum; }
            set
            {
                if (geboorteDatum != value)
                {
                    geboorteDatum = value;
                    RaisePropertyChanged("GeboorteDatum");
                }
            }
        }
        private string nationaliteit;

        public string Nationaliteit
        {
            get { return nationaliteit; }
            set
            {
                if (nationaliteit != value)
                {
                    nationaliteit = value;
                    RaisePropertyChanged("Nationaliteit");
                }
            }
        }
        private string geslacht;

        public string Geslacht
        {
            get { return geslacht; }
            set
            {
                if (geslacht != value)
                {
                    geslacht = value;
                    RaisePropertyChanged("Geslacht");
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

        private clsPostcode _Postcode;

        public clsPostcode Postcode
        {
            get { return _Postcode = _Postcode ?? clsBLLHelper.CustomBLL.GetData<clsPostcode>(IDPostCode); }
            set { _Postcode = value; }
        }


        private string rijksregisterNummer;

        public string RijksregisterNummer
        {
            get { return rijksregisterNummer; }
            set
            {
                if (rijksregisterNummer != value)
                {
                    rijksregisterNummer = value;
                    RaisePropertyChanged("RijksregisterNummer");
                }
            }
        }
        private string eMail;

        public string Email
        {
            get { return eMail; }
            set
            {
                if (eMail != value)
                {
                    eMail = value;
                    RaisePropertyChanged("Email");
                }
            }
        }
        private string eMailPersoonlijk;

        public string EmailPersoonlijk
        {
            get { return eMailPersoonlijk; }
            set
            {
                if (eMailPersoonlijk != value)
                {
                    eMailPersoonlijk = value;
                    RaisePropertyChanged("EmailPersoonlijk");
                }
            }
        }
        private string gsmNummer;

        public string GSMNummer
        {
            get { return gsmNummer; }
            set
            {
                if (gsmNummer != value)
                {
                    gsmNummer = value;
                    RaisePropertyChanged("GSMNummer");
                }
            }
        }
        private string telefoonNummer;

        public string TelefoonNummer
        {
            get { return telefoonNummer; }
            set
            {
                if (telefoonNummer != value)
                {
                    telefoonNummer = value;
                    RaisePropertyChanged("TelefoonNummer");
                }
            }
        }
        private byte[] foto;

        public byte[] Photo
        {
            get { return foto; }
            set
            {
                if (foto != value)
                {
                    foto = value;
                    RaisePropertyChanged("Photo");
                }
            }
        }
        private int iDTaal;

        public int IDTaal
        {
            get { return iDTaal; }
            set
            {
                if (iDTaal != value)
                {
                    iDTaal = value;
                    RaisePropertyChanged("IDTaal");
                }
            }
        }

        private clsTaal _Taal;

        public clsTaal Taal
        {
            get { return _Taal = _Taal ?? clsBLLHelper.CustomBLL.GetData<clsTaal>(IDTaal); }
            set { _Taal = value; }
        }


        private int iDNiveau;

        public int IDNiveau
        {
            get { return iDNiveau; }
            set
            {
                if (iDNiveau != value)
                {
                    iDNiveau = value;
                    RaisePropertyChanged("IDNiveau");
                }
            }
        }

        private clsNiveau _Niveau;

        public clsNiveau Niveau
        {
            get { return _Niveau = _Niveau ?? clsBLLHelper.CustomBLL.GetData<clsNiveau>(iDNiveau); }
            set { _Niveau = value; }
        }


        private bool toelatingsProefGeslaagd;

        public bool ToelatingsProefGeslaagd
        {
            get { return toelatingsProefGeslaagd; }
            set
            {
                if (toelatingsProefGeslaagd != value)
                {
                    toelatingsProefGeslaagd = value;
                    RaisePropertyChanged("ToelatingsProefGeslaagd");
                }
            }
        }

        

        public override string ToString()
        {
            return voorNaam + " " + Naam + " - "+IDGebruiker;
        }
    }
}
