using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Extensions;
using System.Windows;
using System.Collections;
using StudentenAdministratieApp.Extensions;
using System.Timers;

namespace StudentenAdministratieApp.ViewModel.Ticketing
{
    public class clsTicketingViewModel : ViewModelBase
    {
        
        public clsTicketingViewModel()
        {
            

        }
        

        private ObservableCollection<clsTicketAanwezigheid> _TicketAanwezigheden;
        public ObservableCollection<clsTicketAanwezigheid> TicketAanwezigheden
        {
            get
            {
                if (_TicketAanwezigheden == null)
                {
                    _TicketAanwezigheden = BLL.GetData<clsTicketAanwezigheid>();
                    clsListUpdater.RegisterForUpdates<clsTicketAanwezigheid>(this, HandleTicketAanwezigheidMessage);

                    _TicketAanwezigheden.CollectionChanged += _TicketAanwezigheden_CollectionChanged;
                }


                return _TicketAanwezigheden;
            }

            set
            {
                _TicketAanwezigheden = value;
                _AanwezigheidTickets = _TicketAanwezigheden.Select(p => new clsUserTicket { TicketObject = p }).ToObservableCollection();
                _TicketAanwezigheden.CollectionChanged += _TicketAanwezigheden_CollectionChanged;

                Notify();
            }
        }

        /// <summary>
        /// Sync the 2 observable collections
        /// https://social.msdn.microsoft.com/Forums/en-US/2e278e3c-27ab-42b5-8a7b-6828ddbb9caf/how-to-sync-two-observable-collection-?forum=wpf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _TicketAanwezigheden_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (var x in e.NewItems)
                {
                    _AanwezigheidTickets.Add(new clsUserTicket { TicketObject = x as clsTicketAanwezigheid });
                }
            }

            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (var x in e.OldItems)
                {
                    _AanwezigheidTickets.ToList().RemoveAll(ticket => ticket.TicketObject.getID == (x as clsTicketAanwezigheid).IDTicketAanwezigheid);
                }
            }
        }

        /// <summary>
        /// Action that gets executed when clsTicketAanwezigheid receives a new update from the network
        /// </summary>
        /// <param name="action"></param>
        /// <param name="ID"></param>
        public void HandleTicketAanwezigheidMessage(clsListUpdater.ExecuteAction action, int ID)
        {
            if (action == clsListUpdater.ExecuteAction.CREATE)
            {
                TicketAanwezigheden.AddLock(BLL.GetData<clsTicketAanwezigheid>(ID));
            }
            if (action == clsListUpdater.ExecuteAction.DELETE)
            {
                if (SelectedTicket.TicketObject is clsTicketAanwezigheid)
                {
                    if (SelectedTicket.TicketObject.getID == ID)
                    {
                        MessageBox.Show("Het geopende ticket is verwijderd.");
                        SelectedTicket = null;
                        lock (((ICollection)TicketAanwezigheden).SyncRoot)
                            TicketAanwezigheden.ToList().RemoveAll(p => p.IDTicketAanwezigheid == ID);
                    }
                }
            }
            if (action == clsListUpdater.ExecuteAction.UPDATE)
            {
                clsTicketAanwezigheid toupdate = TicketAanwezigheden.Where(p => p.IDTicketAanwezigheid == ID).FirstOrDefault();
                if (toupdate != null)
                {
                    toupdate.IDGebruikerMedewerker = BLL.GetData<clsTicketAanwezigheid>(ID).IDGebruikerMedewerker;
                }
                else
                {
                    TicketAanwezigheden.AddLock(BLL.GetData<clsTicketAanwezigheid>(ID));
                }
            }
            Console.WriteLine("test");
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

                    _TicketInschrijvingen.CollectionChanged += _TicketInschrijvingen_CollectionChanged;
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
                    _InschrijvingTickets.ToList().RemoveAll(ticket => ticket.TicketObject.getID == (x as clsTicketInschrijving).IDTicketInschrijving);
                }
            }
        }

        ///// <summary>
        ///// Action that gets executed when clsTicketInschrijving receives a new update from the network
        ///// </summary>
        ///// <param name="action"></param>
        ///// <param name="ID"></param>
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


        private ObservableCollection<clsUserTicket> _AanwezigheidTickets;

        /// <summary>
        /// BOs for clsTicketAanwezigheid
        /// </summary>
        public ObservableCollection<clsUserTicket> AanwezigheidTickets
        {
            get
            {

                return _AanwezigheidTickets = _AanwezigheidTickets ?? TicketAanwezigheden.Select(p => new clsUserTicket { TicketObject = p }).ToObservableCollection();
            }
            set { _AanwezigheidTickets = value; }
        }


        private ObservableCollection<clsUserTicket> _InschrijvingTickets;
        /// <summary>
        /// BOs for clsTicketInschrijving
        /// </summary>
        public ObservableCollection<clsUserTicket> InschrijvingTickets
        {
            get { return _InschrijvingTickets = _InschrijvingTickets ?? TicketInschrijvingen.Select(p => new clsUserTicket { TicketObject = p }).ToObservableCollection(); ; }
            set { _InschrijvingTickets = value; }
        }




        private clsUserTicket _SelectedTicket;

        public clsUserTicket SelectedTicket
        {
            get { return _SelectedTicket; }
            set
            {
                _SelectedTicket = value;
                if(_SelectedTicket != null)
                {

                }

                Notify("SelectedTicket");
            }
        }

        private clsTicketingBaseViewModel _SelectedVM;
        public clsTicketingBaseViewModel SelectedVM
        {
            get
            {
                if (SelectedTicket != null && SelectedTicket.TicketObject is clsTicketInschrijving && SelectedTicket.IsLocked == true)
                {
                   
                }

                if (SelectedTicket != null && SelectedTicket.TicketObject is clsTicketAanwezigheid && SelectedTicket.IsLocked == true)
                {
                    _SelectedVM = _SelectedVM ?? new clsTicketAanwezigheidViewModel(SelectedTicket.TicketObject as clsTicketAanwezigheid);
                    return _SelectedVM;
                }

                return new clsLockedViewModel();
            }
        }


    }
}
