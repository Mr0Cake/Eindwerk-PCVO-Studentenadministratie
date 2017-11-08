using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Extensions;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudentenAdministratieApp.ViewModel.Leerkrachten
{
    public class clsAanwezigheidsLijstenViewModel : clsLeerkrachtenBaseViewModel
    {
        private ObservableCollection<clsCheckedClass<clsAanwezigheid>> _SelectedStudenten;

        public ObservableCollection<clsCheckedClass<clsAanwezigheid>> SelectedStudenten
        {
            get { return _SelectedStudenten; }
            set { _SelectedStudenten = value; }
        }

        private ObservableCollection<clsAanwezigheid> _SelectedAanwezigheden;

        public ObservableCollection<clsAanwezigheid> SelectedAanwezigheden
        {
            get { return _SelectedAanwezigheden; }
            set { _SelectedAanwezigheden = value; }
        }

        private ObservableCollection<clsKlas> _SelectedKlassen;

        public ObservableCollection<clsKlas> SelectedKlassen
        {
            get { return _SelectedKlassen; }
            set { _SelectedKlassen = value; Notify(); }
        }

        private int _FilterKlassen;

        public int FilterKlassen
        {
            get { return _FilterKlassen; }
            set { _FilterKlassen = value; Notify("FilterKlassen", "LeerkrachtKlasRooster"); }
        }

        private string _Filter;

        public string Filter
        {
            get { return _Filter; }
            set { _Filter = value; Notify("Filter", "LeerkrachtKlasRooster"); }
        }



        public override void UpdateLists(clsGebruiker g)
        {
            _LeerkrachtKlasRooster = null;
            Notify("LeerkrachtKlasRooster");
            IEnumerable<clsModule> modu = Modules.Where(x => Modules_GebruikersLeraars.ToList().FindIndex(p => p.IDModule == x.IDModule && g.IDGebruiker == p.IDGebruiker) > -1);

            SelectedKlassen = Klassen.Where(klas => modu.ToList().FindIndex(z => z.IDModule == klas.IDModule) > -1).ToObservableCollection();
            //Modules.Where(o => o.IDModule == Modules_GebruikersLeraars.ToList().Find(z => z.IDGebruiker == g.IDGebruiker).NNR(y=>y.IDModule)).Select(k => Klassen.Where(w => w.IDModule == k.IDModule))
        }

        private ObservableCollection<Tuple<clsKlasRooster, string>> _LeerkrachtKlasRooster;

        public ObservableCollection<Tuple<clsKlasRooster, string>> LeerkrachtKlasRooster
        {
            get
            {
                if (_LeerkrachtKlasRooster != null)
                    return _LeerkrachtKlasRooster.Where(o => MatchesFilter(o.Item1.IDKlas, FilterKlassen) && MatchesFilter(o.Item2, Filter)).ToObservableCollection();
                _LeerkrachtKlasRooster = new ObservableCollection<Tuple<clsKlasRooster, string>>();
                KlasRoosters.ToList().ForEach(p => 
                {
                    if (SelectedLeerkracht != null &&p.IDGebruikerLeraar == SelectedLeerkracht.IDGebruiker)
                    {
                       try
                        {
                            _LeerkrachtKlasRooster.Add(new Tuple<clsKlasRooster, string>(p, ModuleTypes.ToList().Find(r => r.IDType == Modules.ToList().Find(l => l.IDModule == Klassen.ToList().Find(u => u.IDKlas == p.IDKlas).IDModule).IDModuleType).InterneNaam +" | "+ p.StartDatum));
                            
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }
                        
                }
                );
                return _LeerkrachtKlasRooster.Where(o => MatchesFilter(o.Item1.IDKlas, FilterKlassen) && MatchesFilter(o.Item2, Filter)).ToObservableCollection();
            }
            set { _LeerkrachtKlasRooster = value; SelectedKlasRooster = value[0]; Notify("LeerkrachtKlasRooster","SelectedKlasRooster"); }
        }

        private Tuple<clsKlasRooster, string> _SelectedKlasRooster;

        public Tuple<clsKlasRooster, string> SelectedKlasRooster
        {
            get
            {
                return _SelectedKlasRooster;
            }
            set
            {
                _SelectedKlasRooster = value; Notify();
                if (SelectedStudenten == null)
                    SelectedStudenten = new ObservableCollection<clsCheckedClass<clsAanwezigheid>>();
                SelectedStudenten.Clear();
                if (value != null)
                {

                    List<clsAanwezigheid> aw = Aanwezigheden.Where(i => i.IDLesrooster == value.Item1.IDKlasRooster).ToList();
                    List<clsInschrijving> ins = Inschrijvingen.Where(i => i.IDKlas == value.Item1.IDKlas).ToList();
                    //elke student
                    foreach (clsGebruiker g in Cursisten)
                    {
                        //zit de gebruiker in de aanwezigheid van de klas
                        int index = -1;
                        if ((index = aw.FindIndex(p => p.IDGebruiker == g.IDGebruiker)) > -1)
                        {
                            SelectedStudenten.Add(
                            new clsCheckedClass<clsAanwezigheid>(aw[index], UpdateHandler, aw[index].IsAanwezig, g.Voornaam+" " + g.Naam, g.Photo));
                        }else if((index = ins.FindIndex(p => p.IDGebruiker == g.IDGebruiker))>-1)
                        {
                            SelectedStudenten.Add(
                                new clsCheckedClass<clsAanwezigheid>(new clsAanwezigheid() { IDGebruiker = g.IDGebruiker, IDLesrooster = value.Item1.IDKlasRooster, IsAanwezig = null }, UpdateHandler, null, g.Naam + " " + g.Voornaam, g.Photo));
                        }

                    }

                    
                    Notify("SelectedStudenten");

                }
                //SelectedStudenten = Gebruikers.Where(p => Aanwezigheden.Where(i => i.IDLesrooster == value.IDKlasRooster).ToList().FindIndex(o => o.IDGebruiker == p.IDGebruiker) > -1).ToObservableCollection();
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


        private ICommand _CancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                return _CancelCommand = _CancelCommand ?? new CommandHandler(() =>
                { Cancel(); ExecuteOnSave.Clear(); }, !Opgeslagen);

            }
        }
        /// <summary>
        /// restore the backup or clear the fields
        /// </summary>
        public void Cancel()
        {
            SelectedStudenten.ToList().ForEach(x =>
            {
                BLL.Fill((x.ItemContainer.Backup ?? new clsAanwezigheid()) as clsAanwezigheid, x.ItemContainer as clsAanwezigheid);
                x.IsChecked = x.InitialValue;
            });
        }

        public void Save()
        {
            ExecuteOnSave.Values.ToList().ForEach(x => x());
            Opgeslagen = true;
            DelayedAction(() => { Opgeslagen = false; }, 5000);
        }

        public void UpdateHandler(bool? val, clsAanwezigheid aw)
        {
            string key = "update:" + aw.IDAanwezigheid + ":" + aw.IDGebruiker + ":" + aw.IDLesrooster;
            if (ExecuteOnSave.ContainsKey(key))
                ExecuteOnSave.Remove(key);
            ExecuteOnSave.Add(key, () =>
            {
                aw.IsAanwezig = val;
                if (aw.IDAanwezigheid > 0)
                {
                    BLL.UpdateData(aw);
                }
                else
                {
                    BLL.InsertData(aw);
                    Aanwezigheden.Add(aw);
                }

                BLL.Fill(aw, aw.Backup);
            }

            

            );
        }

    }
}
