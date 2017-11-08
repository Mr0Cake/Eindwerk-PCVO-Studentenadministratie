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
    public class clsTicketWijzigingViewModel : clsTicketingBaseViewModel
    {

        private ObservableCollection<clsGebruikerWebUpdate> _TicketWijzigingen;

        public ObservableCollection<clsGebruikerWebUpdate> TicketWijzigingen
        {
            get { return _TicketWijzigingen = _TicketWijzigingen ?? BLL.GetData<clsGebruikerWebUpdate>(); }
            set { _TicketWijzigingen = value; }
        }

        private void doUpdate()
        {
            Updating = true;
            Notify("Updating");
            ObservableCollection<clsGebruikerWebUpdate> updatedTickets = customBLL.TicketWijzigingSelectByDate<clsGebruikerWebUpdate>(updatedDT);
            updatedTickets.ToList().ForEach(p =>
            {
                if (p.IsGedelete)
                {
                    TicketWijzigingen.ToList().RemoveAll(x => x.ID == p.ID);
                }
                else
                {
                    TicketWijzigingen.Where(x => x.ID == p.ID).ToList().ForEach(z =>
                    {
                        BLL.Fill(p, z);

                    });
                }
            }

            );
            System.Threading.Thread.Sleep(1000);
            Updating = false;
            Notify("Updating");
        }

        public bool Updating { get; set; }

        private void _TicketWijzigingen_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (var x in e.NewItems)
                {
                    WijzigingsTickets.Add(new clsUserTicket { TicketObject = x as clsGebruikerWebUpdate });
                }
            }

            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (var x in e.OldItems)
                {
                    clsUserTicket ut = WijzigingsTickets.ToList().Find(ticket => (ticket.TicketObject as clsGebruikerWebUpdate).ID == (x as clsGebruikerWebUpdate).ID);
                    WijzigingsTickets.Remove(ut);
                }
            }
        }

        public override void Save()
        {
            clsGebruikerWebUpdate ticket = SelectedTicket.TicketObject as clsGebruikerWebUpdate;
            if (!string.IsNullOrEmpty(ticket.Straat))
            {
                SelectedGebruiker.Straat = ticket.Straat;
            }
            if (!string.IsNullOrEmpty(ticket.Huisnummer))
            {
                SelectedGebruiker.HuisNummer = ticket.Huisnummer;
            }
            if (ticket.IDPostcode > 0)
            {
                SelectedGebruiker.IDPostCode = ticket.IDPostcode;
            }
            if (!string.IsNullOrEmpty(ticket.EmailPersoonlijk))
            {
                SelectedGebruiker.EmailPersoonlijk = ticket.EmailPersoonlijk;
            }
            if (!string.IsNullOrEmpty(ticket.GSMNummer))
            {
                SelectedGebruiker.GSMNummer = ticket.GSMNummer;
            }
            if (!string.IsNullOrEmpty(ticket.TelefoonNummer))
            {
                SelectedGebruiker.TelefoonNummer = ticket.TelefoonNummer;
            }
            if (ticket.Photo != null)
            {
                SelectedGebruiker.Photo = ticket.Photo;
            }
            if (ticket.IDTaal > 0)
            {
                SelectedGebruiker.IDTaal = ticket.IDTaal;
            }
            BLL.UpdateData(SelectedGebruiker);
            //BLL.DeleteData(ticket);

            //ListUpdater.DoUpdate<clsGebruikerWebUpdate>(clsListUpdater.ExecuteAction.DELETE, ticket.ID);
            Delete();
            ListUpdater.DoUpdate<clsGebruiker>(clsListUpdater.ExecuteAction.UPDATE, SelectedGebruiker.IDGebruiker);

            SelectedTicket = null;
            Notify("WijzigingsTickets");
        }

        public void HandleTicketWijzigingMessage(clsListUpdater.ExecuteAction action, int ID)
        {
            if (action == clsListUpdater.ExecuteAction.CREATE)
            {
                TicketWijzigingen.Add(BLL.GetData<clsGebruikerWebUpdate>(ID));
            }
            if (action == clsListUpdater.ExecuteAction.DELETE)
            {
                if (SelectedTicket.TicketObject is clsGebruikerWebUpdate)
                {
                    if (SelectedTicket.TicketObject.getID == ID)
                    {
                        MessageBox.Show("Het geopende ticket is verwijderd.");
                        SelectedTicket = null;
                        lock (((ICollection)TicketWijzigingen).SyncRoot)
                            TicketWijzigingen.ToList().RemoveAll(p => p.ID == ID);
                    }
                }
            }
            if (action == clsListUpdater.ExecuteAction.UPDATE)
            {
                clsGebruikerWebUpdate toupdate = TicketWijzigingen.Where(p => p.ID == ID).FirstOrDefault();
                if (toupdate != null)
                {
                    toupdate.IDGebruikerMedewerker = BLL.GetData<clsGebruikerWebUpdate>(ID).IDGebruikerMedewerker;
                    Notify("SelectedTicket");

                }
                else
                {
                    TicketWijzigingen.AddLock(BLL.GetData<clsGebruikerWebUpdate>(ID));
                    Notify("SelectedTicket");
                }
            }
        }


        private ObservableCollection<clsUserTicket> _WijzigingsTickets;
        /// <summary>
        /// BOs for clsTicketInschrijving
        /// </summary>
        public ObservableCollection<clsUserTicket> WijzigingsTickets
        {
            get
            {
                if (_WijzigingsTickets == null)
                {
                    _WijzigingsTickets = TicketWijzigingen.Select(p => new clsUserTicket { TicketObject = p }).ToObservableCollection();
                    clsListUpdater.SequentialUpdateList.Add(doUpdate);
                    clsListUpdater.RegisterForUpdates<clsGebruikerWebUpdate>(this, HandleTicketWijzigingMessage);
                    TicketWijzigingen.CollectionChanged += _TicketWijzigingen_CollectionChanged;
                }
                return _WijzigingsTickets;
            }
            set { _WijzigingsTickets = value; }
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
            if (SelectedTicket != null && SelectedTicket.TicketObject != null)
            {

                BLL.DeleteData(SelectedTicket.TicketObject as clsGebruikerWebUpdate);
                //send message to network
                ListUpdater.DoUpdate<clsInschrijving>(clsListUpdater.ExecuteAction.DELETE, SelectedTicket.TicketObject.getID);
                //delete from local storage
                TicketWijzigingen.Remove(SelectedTicket.TicketObject as clsGebruikerWebUpdate);
                //WijzigingsTickets.ToList().RemoveAll(x => x.TicketObject.Equals(SelectedTicket));
                base.Delete();
                Opgeslagen = true;

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
                if (value != null)
                {
                    if (value.IsLocked == true)
                    {
                        base.SelectedTicket = value;

                        SelectedGebruiker = Gebruikers.Where(x => x.IDGebruiker == value.TicketObject.IDGebruikerStudent).FirstOrDefault();


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
                        base.SelectedTicket = null;

                    }
                }
                else
                {
                    base.SelectedTicket = null;
                }
            }
        }

    }
}
