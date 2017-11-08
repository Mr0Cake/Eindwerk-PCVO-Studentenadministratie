using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmValidation;
using StudentApplication.Model;

namespace StudentenAdministratieApp.ViewModel.Ticketing
{
    public class clsTicketingBaseViewModel:ViewModelBase
    {
        public static BLL.clsCustomBLL customBLL = new BLL.clsCustomBLL();
        public static DateTime updatedDT = DateTime.Now;
        public clsTicketingBaseViewModel()
        {
            ConfigureValidationRules();
        }

        protected void ConfigureValidationRules()
        {
            Validator.AddRule(() => MailText, () => RuleResult.Assert(!string.IsNullOrEmpty(MailText), "Mail mag niet leeg zijn."));
            Validator.AddRule(() => MailTo, () => RuleResult.Assert(!string.IsNullOrEmpty(MailText), "Adres mag niet leeg zijn."));
            Validator.AddRule(() => MailTo, () => RuleResult.Assert(MailTo.IndexOf("@")  > 3, "Vul een correct email adres in."));
            
        }




        private string _MailText;

        public string MailText
        {
            get { return _MailText; }
            set { _MailText = value; Notify(); }
        }

        private string _MailTo;

        public string MailTo
        {
            get { return _MailTo; }
            set { _MailTo = value; Notify(); }
        }


        private clsGebruiker _SelectedGebruiker;

        public clsGebruiker SelectedGebruiker
        {
            get { return _SelectedGebruiker; }
            set { _SelectedGebruiker = value; Notify(); }
        }



        private ICommand _SaveCommand;

        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand = _SaveCommand ?? new CommandHandler(() =>
                { Save(); SendMail(); }, true);

            }
        }

        private ICommand _DeleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return _DeleteCommand = _DeleteCommand ?? new CommandHandler(() =>
                { Delete();  }, true);

            }
        }

        public virtual void Delete()
        {
            SelectedTicket = null;
        }



        protected clsUserTicket _SelectedTicket;

        public virtual clsUserTicket SelectedTicket
        {
            get { return _SelectedTicket; }
            set
            {
                _SelectedTicket = value;
                Notify("SelectedTicket");
            }
        }

        public virtual void Save()
        {

        }


        public void SendMail()
        {
            Validator.Validate(() => MailText);
        }
    }
}
