using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageProvider;
using System.Collections.ObjectModel;
using BLL.Extensions;
using StudentenAdministratieApp.Extensions;
using System.Windows;
using System.Collections;
using System.Windows.Input;

namespace StudentenAdministratieApp.ViewModel.Ticketing
{
    public class clsTicketInschrijvingViewModel : clsTicketingBaseViewModel
    {

        public clsTicketInschrijvingViewModel()
        {

        }

        private ObservableCollection<clsTicketInschrijving> _TicketInschrijvingen;

        public ObservableCollection<clsTicketInschrijving> TicketInschrijvingen
        {
            get
            {
                if (_TicketInschrijvingen == null)
                {
                    _TicketInschrijvingen = _TicketInschrijvingen ?? BLL.GetData<clsTicketInschrijving>();
                    clsListUpdater.RegisterForUpdates<clsTicketInschrijving>(this, HandleTicketInschrijvingMessage);
                    clsListUpdater.SequentialUpdateList.Add(doUpdate);

                }
                return _TicketInschrijvingen;
            }
            set
            {
                _TicketInschrijvingen = value;
                _InschrijvingTickets = TicketInschrijvingen.Select(p => new clsUserTicket { TicketObject = p }).ToObservableCollection();
                _TicketInschrijvingen.CollectionChanged += _TicketInschrijvingen_CollectionChanged;
                Notify();
            }
        }

        private void doUpdate()
        {
            ObservableCollection<clsTicketInschrijving> updatedTickets = customBLL.TicketInschrijvingSelectByDate<clsTicketInschrijving>(updatedDT);
            updatedTickets.ToList().ForEach(p =>
            {
                if (p.IsGedelete)
                {
                    TicketInschrijvingen.ToList().RemoveAll(x => x.IDTicketInschrijving == p.IDTicketInschrijving);
                }
                else
                {
                    TicketInschrijvingen.Where(x => x.IDTicketInschrijving == p.IDTicketInschrijving).ToList().ForEach(z =>
                    {
                        BLL.Fill(p, z);

                    });
                }
            }

            );
        }

        private void _TicketInschrijvingen_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (var x in e.NewItems)
                {
                    _InschrijvingTickets.Add(new clsUserTicket { TicketObject = x as clsTicketInschrijving });
                }
            }

            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (var x in e.OldItems)
                {
                    //_InschrijvingTickets.ToList().RemoveAll(ticket => (ticket.TicketObject as clsTicketInschrijving).IDTicketInschrijving == (x as clsUserTicket) as clsTicketInschrijving).IDTicketInschrijving);
                    clsTicketInschrijving deleteditem = x as clsTicketInschrijving;
                    if (deleteditem != null)
                    {
                        int index = _InschrijvingTickets.ToList().FindIndex(a => deleteditem.getID == a.TicketObject.getID);
                        if (index > -1)
                        {
                            _InschrijvingTickets.RemoveAt(index);
                        }
                        
                        
                    }
                }
            }
        }

        /// <summary>
        /// Action that gets executed when clsTicketInschrijving receives a new update from the network
        /// </summary>
        /// <param name="action"></param>
        /// <param name="ID"></param>
        public void HandleTicketInschrijvingMessage(clsListUpdater.ExecuteAction action, int ID)
        {
            if (action == clsListUpdater.ExecuteAction.CREATE)
            {
                TicketInschrijvingen.Add(BLL.GetData<clsTicketInschrijving>(ID));
            }
            if (action == clsListUpdater.ExecuteAction.DELETE)
            {
                if (SelectedTicket.TicketObject is clsTicketInschrijving)
                {
                    if (SelectedTicket.TicketObject.getID == ID)
                    {
                        MessageBox.Show("Het geopende ticket is verwijderd.");
                        SelectedTicket = null;
                        lock (((ICollection)TicketInschrijvingen).SyncRoot)
                            TicketInschrijvingen.ToList().RemoveAll(p => p.IDTicketInschrijving == ID);
                    }
                }
            }
            if (action == clsListUpdater.ExecuteAction.UPDATE)
            {
                clsTicketInschrijving toupdate = TicketInschrijvingen.Where(p => p.IDTicketInschrijving == ID).FirstOrDefault();
                if (toupdate != null)
                {
                    toupdate.IDGebruikerMedewerker = BLL.GetData<clsTicketInschrijving>(ID).IDGebruikerMedewerker;
                }
                else
                {
                    TicketInschrijvingen.AddLock(BLL.GetData<clsTicketInschrijving>(ID));
                }
            }
        }


        private ObservableCollection<clsUserTicket> _InschrijvingTickets;
        /// <summary>
        /// BOs for clsTicketInschrijving
        /// </summary>
        public ObservableCollection<clsUserTicket> InschrijvingTickets
        {
            get
            {
                if (_InschrijvingTickets == null)
                {
                    _InschrijvingTickets = TicketInschrijvingen.Select(p => new clsUserTicket { TicketObject = p }).ToObservableCollection();

                    _TicketInschrijvingen.CollectionChanged += _TicketInschrijvingen_CollectionChanged;
                }

                return _InschrijvingTickets;
            }
            set { _InschrijvingTickets = value; }
        }





        private clsInschrijving _SelectedInschrijving;

        public clsInschrijving SelectedInschrijving
        {
            get { return _SelectedInschrijving; }
            set
            {
                _SelectedInschrijving = value;
                Notify();
                if (value != null)
                {
                    SelectedModuleType = ModuleTypes.First(p => p.IDType == Modules.Where(o => o.IDModule == value.IDModule).Select(x => x.IDModuleType).FirstOrDefault());
                    BenodigdeModules = VoorafgaandeModules.Where(x => x.IDModuleType == SelectedModuleType.IDType).Select(p => ModuleTypes.Where(o => o.IDType == p.IDModuleType).FirstOrDefault()).ToObservableCollection();
                }

            }
        }

        public override clsUserTicket SelectedTicket
        {
            get
            {
                return base.SelectedTicket;
            }

            set
            {
                if(value!= null)
                if (value.IsLocked == true)
                {
                    base.SelectedTicket = value;

                    if (value != null)
                    {
                        SelectedInschrijving = Inschrijvingen.ToList().Find(x => x.IDInschrijving == (value.TicketObject as clsTicketInschrijving).IDInschrijving);
                        //Sanity Check
                        if (SelectedInschrijving == null || Gebruikers.Where(x => x.IDGebruiker == SelectedInschrijving.IDGebruiker).ToList().Count == 0 || Inschrijvingen.Where(x => x.IDInschrijving == SelectedInschrijving.IDInschrijving).ToList().Count == 0 || Modules.Where(x => x.IDModule == SelectedInschrijving.IDModule).ToList().Count == 0)
                        {
                            if (MessageBox.Show("Dit ticket bevat foutieve data, wilt u dit verwijderen?", "Fout", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                            {
                                Delete();
                            }
                        }
                        if (SelectedInschrijving != null)
                        {
                            SelectedGebruiker = Gebruikers.Where(x => x.IDGebruiker == SelectedInschrijving.IDGebruiker).FirstOrDefault();
                            if (Bevestig)
                            {
                                MailText = template1();
                            }
                            else
                            {
                                MailText = template2();
                            }
                        }
                    }
                }
                else
                {
                    if (value.IsLocked == false)
                    {
                        MessageBox.Show("Dit ticket is reeds in behandeling door " + Gebruikers.Where(x => x.IDGebruiker == value.TicketObject.IDGebruikerMedewerker).FirstOrDefault().Naam, "Locked");
                    }
                    else
                    {
                        MessageBox.Show("Zorg ervoor dat het geselecteerde alleen maar wijzigbaar is door u, druk op het slot icoontje", "Lock");
                    }

                }
                Notify();

            }
        }

        private clsModuleType _SelectedModuleType;

        public clsModuleType SelectedModuleType
        {
            get { return _SelectedModuleType; }
            set { _SelectedModuleType = value; Notify(); Notify("MailTemplate"); }
        }

        private ObservableCollection<clsModuleType> _BehaaldeModules;
        public ObservableCollection<clsModuleType> BehaaldeModules
        {
            get
            {
                return _BehaaldeModules;
            }
            set
            {
                _BehaaldeModules = value; Notify();
            }
        }


        private ObservableCollection<clsModuleType> _BenodigdeModules;

        public ObservableCollection<clsModuleType> BenodigdeModules
        {
            get { return _BenodigdeModules; }
            set { _BenodigdeModules = value; }
        }





        public string template2()
        {
            string gebruikersnaam = Gebruikers.Where(x => x.IDGebruiker == SelectedTicket.TicketObject.IDGebruikerStudent).Select(x => x.Voornaam).FirstOrDefault();
            string actionText = "Uw ";
            if (SelectedInschrijving != null)
            {
                if (SelectedInschrijving.IsValidated == true)
                {
                    actionText += "uitschrijving  ";
                }
                else
                {
                    actionText += "inschrijving ";
                }
            }

            return "Beste " + gebruikersnaam + "\r\n\r\n" + actionText + "voor de module " + SelectedModuleType.InterneNaam + " is niet goedgekeurd. \r\n\r\nMet Vriendelijke groeten\r\nHet Secretariaat";
        }

        public string template1()
        {
            string gebruikersnaam = Gebruikers.Where(x => x.IDGebruiker == SelectedTicket.TicketObject.IDGebruikerStudent).Select(x => x.Voornaam).FirstOrDefault();
            string actionText = "U bent ";
            if (SelectedInschrijving != null)
            {
                if (SelectedInschrijving.IsValidated == true)
                {
                    actionText += "uitgeschreven ";
                }
                else
                {
                    actionText += "ingeschreven ";
                }
            }

            return "Beste " + gebruikersnaam + "\r\n\r\n" + actionText + "voor de module " + SelectedModuleType.InterneNaam + ". \r\n\r\nMet Vriendelijke groeten\r\nHet Secretariaat";
        }


        private bool _Bevestig;

        public bool Bevestig
        {
            get { return _Bevestig; }
            set
            {
                _Bevestig = value;
                if (SelectedTicket != null && SelectedTicket.TicketObject != null)
                {
                    if (value)
                    {
                        MailText = template1();
                    }
                    else
                    {
                        MailText = template2();
                    }
                }

                Notify();
            }
        }


        public override void Save()
        {

            if (SelectedInschrijving != null)
            {
                if (Bevestig)
                {
                    if (SelectedInschrijving.IsValidated == true)
                    {
                        //IsValidated == true --> Delete inschrijving
                        BLL.DeleteData<clsInschrijving>(SelectedInschrijving);
                        ListUpdater.DoUpdate<clsInschrijving>(clsListUpdater.ExecuteAction.DELETE, SelectedInschrijving.IDInschrijving);
                        SelectedInschrijving = null;

                    }
                    else
                    {
                        SelectedInschrijving.IsValidated = true;
                        SelectedInschrijving.IDKlas = Klassen.Where(x => x.IDModule == SelectedInschrijving.IDModule).Select(p => p.IDKlas).FirstOrDefault();
                        BLL.UpdateData<clsInschrijving>(SelectedInschrijving);
                        ListUpdater.DoUpdate<clsInschrijving>(clsListUpdater.ExecuteAction.UPDATE, SelectedInschrijving.IDInschrijving);

                    }
                }
                BLL.DeleteData(SelectedTicket.TicketObject as clsTicketInschrijving);
                ListUpdater.DoUpdate<clsTicketInschrijving>(clsListUpdater.ExecuteAction.DELETE, SelectedTicket.TicketObject.getID);
                TicketInschrijvingen.Remove(SelectedTicket.TicketObject as clsTicketInschrijving);

                SendMail();
                Bevestig = false;
                SelectedTicket = null;
            }

        }


        //private ICommand _DeleteCommand;

        //public ICommand DeleteCommand
        //{
        //    get
        //    {
        //        return _DeleteCommand = _DeleteCommand ?? new CommandHandler(() =>
        //        { Delete(); }, true);

        //    }
        //}


        public override void Delete()
        {
            if (SelectedInschrijving != null)
            {
                if (SelectedInschrijving.IsValidated == true)
                {
                    //delete the ticket but don't delete the inschrijving


                }
                else
                {
                    //delete inschrijving and ticket
                    //delete from database
                    BLL.DeleteData(SelectedInschrijving);
                    //send message to network
                    ListUpdater.DoUpdate<clsInschrijving>(clsListUpdater.ExecuteAction.DELETE, SelectedInschrijving.IDInschrijving);
                    //delete from local storage
                    Inschrijvingen.Remove(SelectedInschrijving);

                }
            }
            //delete from database
            BLL.DeleteData(SelectedTicket.TicketObject as clsTicketInschrijving);
            //send message to network
            ListUpdater.DoUpdate<clsTicketInschrijving>(clsListUpdater.ExecuteAction.DELETE, (SelectedTicket.TicketObject as clsTicketInschrijving).IDInschrijving);
            //delete from local storage
            TicketInschrijvingen.Remove(SelectedTicket.TicketObject as clsTicketInschrijving);

            SelectedTicket = null;


            Opgeslagen = true;
        }

    }
}
