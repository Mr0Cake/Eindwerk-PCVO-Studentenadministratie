using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApplication.Model;
using System.Collections.ObjectModel;
using BLL.Extensions;
using System.Windows.Input;

namespace StudentenAdministratieApp.ViewModel.Cursisten
{
    public class clsAfwezigheidViewModel : clsCursistenViewModel
    {
        protected clsGebruiker _SelectedCursist;

        public virtual clsGebruiker SelectedCursist
        {
            get { return _SelectedCursist; }
            set
            {
                _SelectedCursist = value;
                Notify();
                _SelectedKlas = null;
                _SelectedKlassen = null;
                Notify("SelectedKlassen", "SelectedKlas");

            }
        }

        private ICommand _SaveCommand;

        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand = _SaveCommand ?? new CommandHandler(() =>
                { Save(); ExecuteOnSave.Clear(); }, !Opgeslagen);

            }
        }

        public void Save()
        {
            ExecuteOnSave.Values.ToList().ForEach(p => p());
            Opgeslagen = true;
            DelayedAction(() => Opgeslagen = false, 5000);
        }

        private ObservableCollection<clsKlas> _SelectedKlassen;

        public ObservableCollection<clsKlas> SelectedKlassen
        {
            get
            {
                if (SelectedCursist != null)
                {
                    if (_SelectedKlassen == null)
                    {
                        _SelectedKlassen = new ObservableCollection<clsKlas>();
                        Inschrijvingen.ToList().Where(x => x.IDGebruiker == SelectedCursist.IDGebruiker).ToList().ForEach(p => _SelectedKlassen.Add(Klassen.ToList().Find(o => o.IDKlas == p.IDKlas)));
                        List<clsModule> selectedmodules = Modules.Where(p => _SelectedKlassen.ToList().FindIndex(o => o.IDModule == p.IDModule) > -1).ToList();
                        _SelectedKlassen.ToList().ForEach(x => x.ModuleType = ModuleTypes.Where(o => selectedmodules.FindIndex(p => p.IDModuleType == o.IDType && x.IDModule == p.IDModule) > -1).FirstOrDefault());
                        Inschrijvingen.CollectionChanged += Inschrijvingen_CollectionChanged;
                        Aanwezigheden.CollectionChanged += Aanwezigheden_CollectionChanged;
                    }
                }
                return _SelectedKlassen;
            }
            set
            {
                _SelectedKlassen = value; Notify();
                if (value != null && value.Count > 0)
                {
                    SelectedKlas = value[0];
                    Notify("SelectedKlas");
                }
            }
        }

        private void Aanwezigheden_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if ((e.NewItems != null && e.NewItems.Count > 0) || (e.OldItems != null && e.OldItems.Count > 0))
            {
                SelectedCursist = SelectedCursist;
            }
            //if (e.NewItems != null && e.NewItems.Count > 0)
            //{
            //    foreach (var x in e.NewItems)
            //    {
            //        _SelectedKlassen.Add(Klassen.ToList().Find(o => o.IDKlas == (x as clsInschrijving).IDKlas));
            //    }
            //}

            //if (e.OldItems != null && e.OldItems.Count > 0)
            //{
            //    foreach (var x in e.OldItems)
            //    {
            //        _SelectedKlassen.Remove(Klassen.ToList().Find(o => o.IDKlas == (x as clsInschrijving).IDKlas));
            //    }
            //}
            //Notify("SelectedCursist");
        }

        private void Inschrijvingen_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            

            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (var x in e.NewItems)
                {
                    _SelectedKlassen.Add(Klassen.ToList().Find(o => o.IDKlas == (x as clsInschrijving).IDKlas));
                }
            }

            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (var x in e.OldItems)
                {
                    _SelectedKlassen.Remove(Klassen.ToList().Find(o => o.IDKlas == (x as clsInschrijving).IDKlas));
                }
            }
            Notify("SelectedCursist");
        }

        private clsKlas _SelectedKlas;

        public clsKlas SelectedKlas
        {
            get { return _SelectedKlas; }
            set
            {
                _SelectedKlas = value;
                Notify();
                //return different default value http://stackoverflow.com/a/24009496
                if (value != null)
                {
                    IEnumerable<clsKlasRooster> r = KlasRoosters.Where(o => o.IDKlas == value.IDKlas);
                    _CursistKlasRooster = new ObservableCollection<clsKlasRoosterItem>();
                    foreach (clsKlasRooster k in r)
                    {
                        int idGebruiker = SelectedCursist.IDGebruiker;
                        int idkl = k.IDKlasRooster;
                        clsAanwezigheid aw = Aanwezigheden.Where(x => x.IDLesrooster == idkl && x.IDGebruiker == idGebruiker).FirstOrDefault();
                        bool? isChecked = false;
                        if (aw != null)
                        {
                            isChecked = aw.IsAanwezig;
                        }
                        else
                        {
                            aw = new clsAanwezigheid();
                            aw.IDLesrooster = k.IDKlasRooster;
                            aw.IDGebruiker = SelectedCursist.IDGebruiker;
                            aw.IsAanwezig = isChecked;

                        }
                        //clsCheckedClass<clsKlasRooster> ck = new clsCheckedClass<clsKlasRooster>(k, CheckedHandler, isChecked, k.StartDatum.ToShortDateString());
                        clsKlasRoosterItem ck = new clsKlasRoosterItem(k, aw, CheckedHandler, isChecked, k.StartDatum.ToShortDateString());

                        _CursistKlasRooster.Add(ck);
                    }

                }
                Notify("CursistKlasRooster");
            }
        }


        private ObservableCollection<clsKlasRoosterItem> _CursistKlasRooster;

        public ObservableCollection<clsKlasRoosterItem> CursistKlasRooster
        {
            get
            {
                if (SelectedKlas == null)
                    return null;

                return _CursistKlasRooster;

            }
            set { _CursistKlasRooster = value; }
        }





        public void CheckedHandler(bool? isChecked, clsKlasRooster selectedKlasrooster, clsAanwezigheid aw)
        {
            aw.IsAanwezig = isChecked;
            string key = "update:" + selectedKlasrooster.IDKlas + ":" + SelectedCursist.IDGebruiker + ":" + selectedKlasrooster.StartDatum.ToShortDateString();
            if (ExecuteOnSave.ContainsKey(key))
                ExecuteOnSave.Remove(key);
            ExecuteOnSave.Add(key, () =>
                {

                    if (aw.IDAanwezigheid < 1)
                    {
                        BLL.InsertData(aw);
                        Aanwezigheden.Add(aw);
                    }
                    else
                    {
                        BLL.UpdateData(aw);
                    }

                }


            );
        }
    }
}
