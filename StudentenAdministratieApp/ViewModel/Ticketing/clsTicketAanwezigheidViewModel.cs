using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenAdministratieApp.ViewModel.Ticketing
{
    public class clsTicketAanwezigheidViewModel : clsTicketingBaseViewModel
    {

        public clsTicketAanwezigheidViewModel()
        {

        }

        public clsTicketAanwezigheidViewModel(clsTicketAanwezigheid ticket)
        {
            Ticket = ticket;
        }

        private clsTicketAanwezigheid _Ticket;

        public clsTicketAanwezigheid Ticket
        {
            get { return _Ticket; }
            set
            {
                _Ticket = value;
                SelectedAanwezigheid = Aanwezigheden.Where(p => p.IDLesrooster == value.IDKlasRooster && p.IDGebruiker == value.IDGebruikerStudent).FirstOrDefault() ?? new clsAanwezigheid() { IDGebruiker = value.IDGebruikerStudent, IDLesrooster = value.IDKlasRooster, IsAanwezig = null };
                Notify();
            }
        }

        private clsAanwezigheid _SelectedAanwezigheid;

        public clsAanwezigheid SelectedAanwezigheid
        {
            get { return _SelectedAanwezigheid; }
            set
            {
                if (value == null)
                {
                    _SelectedAanwezigheid = new clsAanwezigheid() { IDLesrooster = Ticket.IDKlasRooster, IDGebruiker = Ticket.IDGebruikerStudent };
                }
                else
                {
                    _SelectedAanwezigheid = value;
                }
                Notify();

                SelectedKlas = KlasRoosters.Where(i => i.IDKlasRooster == Ticket.IDKlasRooster).Select(o => Klassen.Where(p => p.IDKlas == o.IDKlas).FirstOrDefault()).FirstOrDefault();
                SelectedModuleType = ModuleTypes.First(x => x.IDType == Modules.Where(o => o.IDModule == SelectedKlas.IDModule).Select(p => p.IDModuleType).FirstOrDefault());
                
            }
        }

        private clsKlas _SelectedKlas;

        public clsKlas SelectedKlas
        {
            get { return _SelectedKlas; }
            set { _SelectedKlas = value; Notify(); }
        }


        private clsModuleType _SelectedModuleType;

        public clsModuleType SelectedModuleType
        {
            get { return _SelectedModuleType; }
            set { _SelectedModuleType = value; Notify(); }
        }

        

        public void KeurAf()
        {
            string gebruikersnaam = Gebruikers.Where(x => x.IDGebruiker == Ticket.IDGebruikerStudent).Select(x => x.Voornaam).FirstOrDefault();
            MailText = "Beste " + gebruikersnaam + "<br /><br />" + "Uw aanvraag voor afwezigheid op " + KlasRoosters.ToList().Find(p => p.IDKlasRooster == SelectedAanwezigheid.IDLesrooster).StartDatum.ToShortDateString() +
                "Voor de les " + SelectedModuleType.InterneNaam + " is afgekeurd.<br /><br />Met Vriendelijke Groeten<br />Het Secretariaat";
            BLL.DeleteData(Ticket);
            clearField();
        }

        public void clearField()
        {
            Ticket = null;
            SelectedModuleType = null;
            SelectedKlas = null;
            SelectedAanwezigheid = null;
        }


        private List<string> _MailTemplate;

        public List<string> MailTemplate
        {
            get
            {
                if (_MailTemplate == null)
                {
                    _MailTemplate = new List<string> { template1(), template2() };
                }
                return _MailTemplate;
            }
            set { _MailTemplate = value; }
        }


        private string _SelectedTemplate;

        public string SelectedTemplate
        {
            get { return _SelectedTemplate; }
            set { _SelectedTemplate = value; Notify(); }
        }



        public string template2()
        {
            if (Ticket == null)
                return "";
            string gebruikersnaam = Gebruikers.Where(x => x.IDGebruiker == Ticket.IDGebruikerStudent).Select(x => x.Voornaam).FirstOrDefault();
            
            return "Beste " + gebruikersnaam + "\r\n\r\n" + "Uw afwezigheid voor de module <b>" + SelectedModuleType.InterneNaam + "</b> is niet goedgekeurd. \r\n\r\nMet Vriendelijke groeten\r\nHet Secretariaat";
        }

        public string template1()
        {
            if (Ticket == null)
                return "";
            string gebruikersnaam = Gebruikers.Where(x => x.IDGebruiker == Ticket.IDGebruikerStudent).Select(x => x.Voornaam).FirstOrDefault();

            return "Beste " + gebruikersnaam + "\r\n\r\n" + "Uw afwezigheid voor de module <b>" + SelectedModuleType.InterneNaam + "</b> is goedgekeurd. \r\n\r\nMet Vriendelijke groeten\r\nHet Secretariaat";
        }


        private bool _Bevestig;

        public bool Bevestig
        {
            get { return _Bevestig; }
            set { _Bevestig = value; Notify(); }
        }
        public override void Save()
        {
            if (Bevestig)
            {
                BLL.UpdateData(SelectedAanwezigheid);
                ListUpdater.DoUpdate<clsAanwezigheid>(clsListUpdater.ExecuteAction.UPDATE, SelectedAanwezigheid.IDAanwezigheid);
            }
            else
            {
                BLL.DeleteData(SelectedAanwezigheid);
                ListUpdater.DoUpdate<clsAanwezigheid>(clsListUpdater.ExecuteAction.DELETE, SelectedAanwezigheid.IDAanwezigheid);

            }

            BLL.DeleteData(Ticket as clsTicketAanwezigheid);
            ListUpdater.DoUpdate<clsTicketAanwezigheid>(clsListUpdater.ExecuteAction.DELETE, Ticket.IDTicketAanwezigheid);



            string gebruikersnaam = Gebruikers.Where(x => x.IDGebruiker == Ticket.IDGebruikerStudent).Select(x => x.Voornaam).FirstOrDefault();
            string actionText = "U bent ";
            if (SelectedAanwezigheid != null)
            {
                if (SelectedAanwezigheid.IsAanwezig == false)
                {
                    //false -> gewettigd afwezig
                    BLL.UpdateData(SelectedAanwezigheid);
                    SelectedAanwezigheid = null;
                    actionText += "<b>Gewettigd Afwezig</b> ";
                }
                else
                {
                    if (SelectedAanwezigheid.IsAanwezig == null)
                    {
                        BLL.UpdateData(SelectedAanwezigheid);
                        actionText += "<b>Niet Gewettigd Afwezig </b> ";
                    }
                }
            }



            Opgeslagen = true;


            if (MailText.Length == 0)
                MailText = "Beste " + gebruikersnaam + "\r\n\r\n" + actionText + "voor de module <b>" + SelectedModuleType.InterneNaam + "</b>\r\n\r\nMet Vriendelijke groeten\r\nHet Secretariaat";

        }


    }
}
