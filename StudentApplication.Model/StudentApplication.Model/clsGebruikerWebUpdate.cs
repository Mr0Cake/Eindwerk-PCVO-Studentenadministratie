using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsGebruikerWebUpdate : clsTicketBase
    {

        public clsGebruikerWebUpdate() : base()
        {
            
        }

        public override void Copy(clsTicketBase tocopy)
        {
            if (tocopy is clsGebruikerWebUpdate)
            {
                clsGebruikerWebUpdate copy = tocopy as clsGebruikerWebUpdate;
                ID = copy.ID;
                IDGebruiker = copy.IDGebruiker;
                Straat = copy.straat;
                Huisnummer = copy.Huisnummer;
                IDPostcode = copy.IDPostcode;
                EmailPersoonlijk = copy.EmailPersoonlijk;
                GSMNummer = copy.GSMNummer;
                TelefoonNummer = copy.TelefoonNummer;
                Photo = copy.Photo;
                IDTaal = copy.IDTaal;
                base.Copy(copy);
            }
        }

        private int id;

        public int ID
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    _ID = value;
                    RaisePropertyChanged("ID");
                }
            }
        }


        public int IDGebruiker
        {
            get { return base.IDGebruikerStudent; }
            set
            {
                if (IDGebruikerStudent != value)
                {
                    IDGebruikerStudent = value;
                    RaisePropertyChanged("IDGebruiker");
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

        public string Huisnummer
        {
            get { return huisNummer; }
            set
            {
                if (huisNummer != value)
                {
                    huisNummer = value;
                    RaisePropertyChanged("Huisnummer");
                }
            }
        }
        private int iDPostCode;

        public int IDPostcode
        {
            get { return iDPostCode; }
            set
            {
                if (iDPostCode != value)
                {
                    iDPostCode = value;
                    RaisePropertyChanged("IDPostcode");
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
            get
            {
                return gsmNummer;
            }
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
            get
            {
                return telefoonNummer;
            }
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
            get { return foto; /*?? new byte[1];*/ }
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
    }
}
