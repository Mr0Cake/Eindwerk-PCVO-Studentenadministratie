using StudentenAdministratieApp.ViewModel.Ticketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApplication.Model;
using BLL;

namespace StudentenAdministratieApp.ViewModel
{
    public class clsUserTicket : PropChanged
    {

        private static clsCustomBLL BLL = new clsCustomBLL();



        private clsLockedViewModel locked = new clsLockedViewModel();






        private bool? _IsLocked;

        /// <summary>
        /// null: not locked
        /// true: locked by user
        /// false: locked by someone else
        /// </summary>
        public bool? IsLocked
        {
            get
            {
                if (TicketObject.IDGebruikerMedewerker == 0)
                {
                    _IsLocked = null;
                    return _IsLocked;
                }
                if (TicketObject.IDGebruikerMedewerker > 0 && MainWindowViewModel.User.IDGebruiker == TicketObject.IDGebruikerMedewerker)
                {
                    _IsLocked = true;
                    //getVM();
                }
                else
                {
                    _IsLocked = false;
                }

                return _IsLocked;
            }
            set
            {
                if (value == true || value == false)
                {
                    if (RequestLock())
                    {
                        _IsLocked = true;
                    }

                }
                if (value == null)
                {
                    ReleaseLock();
                    _IsLocked = null;

                }



                Notify("IsLocked", "ItemViewModel");

            }
        }


        public bool? ObjectType
        {
            get
            {
                if (_IsLocked != true)
                {
                    //ItemViewModel = locked;
                    return null;
                }
                if (TicketObject is clsTicketInschrijving)
                {
                    //_CurrentViewModel = _CurrentViewModel ?? new clsTicketInschrijvingViewModel(TicketObject as clsTicketInschrijving);
                    //ItemViewModel = _CurrentViewModel;
                    return true;
                }
                if (TicketObject is clsTicketAanwezigheid)
                {
                    //CurrentViewModel = _CurrentViewModel ?? new clsTicketAanwezigheidViewModel(TicketObject as clsTicketAanwezigheid);
                    //ItemViewModel = _CurrentViewModel;
                    return false;
                }
                //ItemViewModel = locked;
                return null;
            }
        }

        private clsTicketBase _TicketObject;

        public clsTicketBase TicketObject
        {
            get { return _TicketObject; }
            set
            {
                if (value == null)
                {
                    clsListUpdater.UnRegister<clsTicketInschrijving>(this);
                    clsListUpdater.UnRegister<clsTicketAanwezigheid>(this);
                }
                else
                {

                }
                _TicketObject = value; RegisterForUpdate(); Notify("TicketObject", "IsLocked");
            }
        }


        public void RegisterForUpdate()
        {
            Console.WriteLine("Registered For Updates");
            if (TicketObject is clsTicketInschrijving)
            {
                clsListUpdater.RegisterForUpdates<clsTicketInschrijving>(this, update);
            }
            if(TicketObject is clsTicketAanwezigheid)
            {
                clsListUpdater.RegisterForUpdates<clsTicketAanwezigheid>(this, update);
            }
            if(TicketObject is clsGebruikerWebUpdate)
            {
                clsListUpdater.RegisterForUpdates<clsGebruikerWebUpdate>(this, update);
            }

        }

        public void update(clsListUpdater.ExecuteAction ac, int id)
        {
            Console.WriteLine("UPDATE OBJECT DATA --------------------------");
            Console.WriteLine("ReceivedAction: " + ac.ToString());
            if (TicketObject is clsTicketInschrijving)
            {
                Console.WriteLine("Ticketinschrijving");
                if ((TicketObject as clsTicketInschrijving).IDTicketInschrijving == id)
                {

                    //if (ac == clsListUpdater.ExecuteAction.DELETE)
                    //{
                    //    Console.WriteLine("DELETE");
                    //    TicketObject = null;
                    //}
                    if (ac == clsListUpdater.ExecuteAction.UPDATE)
                    {
                        Console.WriteLine("UPDATE");
                        clsTicketInschrijving newticket = BLL.GetData<clsTicketInschrijving>(id);
                        TicketObject.IDGebruikerMedewerker = newticket.IDGebruikerMedewerker;

                    }
                    Notify("TicketObject", "IsLocked");
                }
            }
            if(TicketObject is clsTicketAanwezigheid)
            {
                if ((TicketObject as clsTicketAanwezigheid).IDTicketAanwezigheid == id)
                {
                    Console.WriteLine("Ticketaanwezigheid");
                    //if (ac == clsListUpdater.ExecuteAction.DELETE)
                    //{
                    //    Console.WriteLine("DELETE");
                    //    TicketObject = null;
                    //}
                    if (ac == clsListUpdater.ExecuteAction.UPDATE)
                    {
                        Console.WriteLine("UPDATE");
                        clsTicketAanwezigheid newticket = BLL.GetData<clsTicketAanwezigheid>(id);
                        TicketObject.IDGebruikerMedewerker = newticket.IDGebruikerMedewerker;
                    }
                    Notify("TicketObject", "IsLocked");
                }
            }
            if(TicketObject is clsGebruikerWebUpdate)
            {
                if ((TicketObject as clsGebruikerWebUpdate).ID == id)
                {
                    Console.WriteLine("TicketWebUpdate");
                    //if (ac == clsListUpdater.ExecuteAction.DELETE)
                    //{
                    //    Console.WriteLine("DELETE");
                    //    TicketObject = null;
                    //}
                    if (ac == clsListUpdater.ExecuteAction.UPDATE)
                    {
                        Console.WriteLine("UPDATE");
                        clsGebruikerWebUpdate newticket = BLL.GetData<clsGebruikerWebUpdate>(id);
                        TicketObject.IDGebruikerMedewerker = newticket.IDGebruikerMedewerker;
                    }
                    Notify("TicketObject", "IsLocked");
                }
            }
            Console.WriteLine("UPDATE HANDLED --------------------------------------");
        }


        public void ReleaseLock()
        {
            TicketObject.IDGebruikerMedewerker = 0;
            if (TicketObject is clsTicketInschrijving)
            {
                BLL.UpdateData<clsTicketInschrijving>(TicketObject as clsTicketInschrijving);
                ViewModelBase.ListUpdater.DoUpdate<clsTicketInschrijving>(clsListUpdater.ExecuteAction.UPDATE, TicketObject.getID);
                Notify("TicketObject", "IsLocked");
            }
            if (TicketObject is clsTicketAanwezigheid)
            {
                BLL.UpdateData<clsTicketAanwezigheid>(TicketObject as clsTicketAanwezigheid);
                ViewModelBase.ListUpdater.DoUpdate<clsTicketAanwezigheid>(clsListUpdater.ExecuteAction.UPDATE, TicketObject.getID);

                Notify("TicketObject", "IsLocked");
            }
            if (TicketObject is clsGebruikerWebUpdate)
            {
                BLL.UpdateData<clsGebruikerWebUpdate>(TicketObject as clsGebruikerWebUpdate);
                ViewModelBase.ListUpdater.DoUpdate<clsGebruikerWebUpdate>(clsListUpdater.ExecuteAction.UPDATE, TicketObject.getID);

                Notify("TicketObject", "IsLocked");
            }
        }




        public bool RequestLock()
        {
            int id = 0;

            if (TicketObject is clsTicketInschrijving)
                id = BLL.RequestTicketInschrijvingLock(TicketObject.getID, MainWindowViewModel.User.IDGebruiker);
            if (TicketObject is clsTicketAanwezigheid)
                id = BLL.RequestTicketAanwezigheidLock(TicketObject.getID, MainWindowViewModel.User.IDGebruiker);
            if (TicketObject is clsGebruikerWebUpdate)
                id = BLL.RequestTicketWijzigingLock(TicketObject.getID, MainWindowViewModel.User.IDGebruiker);



            if (id == MainWindowViewModel.User.IDGebruiker)
            {
                if (TicketObject is clsTicketInschrijving)
                {
                    BLL.SelectUpdate<clsTicketInschrijving>(TicketObject as clsTicketInschrijving);
                    ViewModelBase.ListUpdater.DoUpdate<clsTicketInschrijving>(clsListUpdater.ExecuteAction.UPDATE, TicketObject.getID);
                    Notify("TicketObject", "IsLocked");
                }
                if (TicketObject is clsTicketAanwezigheid)
                {
                    BLL.SelectUpdate<clsTicketAanwezigheid>(TicketObject as clsTicketAanwezigheid);
                    ViewModelBase.ListUpdater.DoUpdate<clsTicketAanwezigheid>(clsListUpdater.ExecuteAction.UPDATE, TicketObject.getID);
                    Notify("TicketObject", "IsLocked");
                }
                if (TicketObject is clsGebruikerWebUpdate)
                {
                    BLL.SelectUpdate<clsGebruikerWebUpdate>(TicketObject as clsGebruikerWebUpdate);
                    ViewModelBase.ListUpdater.DoUpdate<clsGebruikerWebUpdate>(clsListUpdater.ExecuteAction.UPDATE, TicketObject.getID);
                    Notify("TicketObject", "IsLocked");
                }

            }


            return id == MainWindowViewModel.User.IDGebruiker && id != 0;

        }

    }
}
