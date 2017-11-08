using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Extensions;

namespace StudentenAdministratieApp.ViewModel.Lessenroosters
{
    public class clsLessenRoosterBaseViewModel : ViewModelBase
    {
        public clsLessenRoosterBaseViewModel()
        {
            Inschrijvingen.CollectionChanged += Inschrijvingen_CollectionChanged;
        }

        private void Inschrijvingen_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null || e.OldItems != null)
            {
                UserUpdated();
            }
        }

        /// <summary>
        /// get current week
        /// </summary>
        private static DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
        private static Calendar cal = dfi.Calendar;


        private clsGebruiker _SelectedGebruiker;
        public clsGebruiker SelectedGebruiker
        {
            get
            {
                return _SelectedGebruiker;
            }
            set
            {
                _SelectedGebruiker = value;
                UserUpdated();
                Notify();

            }

        }

        /// <summary>
        /// override this method if the selecteduser changes in a child class, this will trigger the updates for your lists
        /// </summary>
        public virtual void UserUpdated()
        {
            try
            {
                //SelectedKlasRoosters = KlasRoosters.Where(x => x.IDKlas == Inschrijvingen.ToList().Find(p => p.IDGebruiker == SelectedGebruiker.IDGebruiker).IDKlas).ToObservableCollection();
                SelectedKlasRoosters = new ObservableCollection<clsKlasRooster>();
                List<clsInschrijving> inschrijvingen = Inschrijvingen.Where(p => p.IDGebruiker == SelectedGebruiker.IDGebruiker).ToList();
                foreach (var v in KlasRoosters)
                {
                    if (inschrijvingen.FindIndex(o => o.IDKlas == v.IDKlas) > -1)
                    {
                        _SelectedKlasRoosters.Add(v);
                    }
                }
                _Lesmomenten.Clear();
                _SelectedKlasRoosters.ToList().ForEach(x =>
                {
                    clsCustomLesmomentItem it = new clsCustomLesmomentItem();
                    it.KlasRooster = x;
                    clsKlas klas = Klassen.ToList().Find(p => p.IDKlas == x.IDKlas);
                    clsModule mod = Modules.ToList().Find(o => o.IDModule == klas.IDModule);
                    List<clsGebruiker> leerkrachten = Gebruikers.Where(p => Modules_GebruikersLeraars.ToList().FindIndex(o => o.IDGebruiker == p.IDGebruiker && o.IDModule == mod.IDModule) > -1).ToList();
                    clsLokaal lok = Lokalen.ToList().Find(p => x.IDLokaal == p.IDLokaal);
                    it.ModuleType = ModuleTypes.ToList().Find(p => p.IDType == mod.IDModuleType);
                    it.Klas = klas;
                    it.Module = mod;
                    it.Leerkrachten = leerkrachten;
                    it.Lokaal = lok;
                    //als zondag = 0 doe + 6 
                    it.DayOfWeek = ((int)x.StartDatum.DayOfWeek);
                    _Lesmomenten.Add(it);
                });
                Notify("Lesmomenten", "SelectedKlasRoosters", "SelectedKlasRooster", "SelectedWeek");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        private ObservableCollection<clsKlasRooster> _SelectedKlasRoosters;

        /// <summary>
        /// Select klasroosters (lesmomenten) voor de geselecteerde week
        /// </summary>
        public ObservableCollection<clsKlasRooster> SelectedKlasRoosters
        {
            get { return _SelectedKlasRoosters.ToList().Where(p => cal.GetWeekOfYear(p.StartDatum, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == SelectedWeek).ToObservableCollection(); }
            set { _SelectedKlasRoosters = value; }
        }


        private clsKlasRooster _SelectedKlasRooster;

        public clsKlasRooster SelectedKlasRooster
        {
            get { return _SelectedKlasRooster; }
            set { _SelectedKlasRooster = value; Notify("SelectedKlasRooster", "IsSelected"); }
        }


        public bool IsSelected { get { return SelectedKlasRooster != null; } }


        private ObservableCollection<clsCustomLesmomentItem> _Lesmomenten = new ObservableCollection<clsCustomLesmomentItem>();

        public ObservableCollection<clsCustomLesmomentItem> Lesmomenten
        {
            get
            {
                return _Lesmomenten.Where(p => cal.GetWeekOfYear(p.KlasRooster.StartDatum, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == SelectedWeek).ToObservableCollection();
               
            }
            set { _Lesmomenten = value; Notify(); }
        }



        private int _SelectedWeek = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

        public int SelectedWeek
        {
            get { return _SelectedWeek; }
            set { _SelectedWeek = value; Notify("SelectedWeek", "Lesmomenten"); }
        }

        private List<int> _Weken = new List<int>();

        public List<int> Weken
        {
            get
            {
                if (_Weken.Count == 0)
                    for (int i = 0; i < cal.GetWeekOfYear(new DateTime(DateTime.Now.Year, 12, 31), dfi.CalendarWeekRule, dfi.FirstDayOfWeek); i++)
                        _Weken.Add(i);
                return _Weken;
            }
            set { _Weken = value; }
        }


    }
}
