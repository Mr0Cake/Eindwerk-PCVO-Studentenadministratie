using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BLL.Extensions;
using System.Windows;

namespace StudentenAdministratieApp.ViewModel.Cursisten
{
    public class clsInschrijvingenBaseViewModel : clsCursistenViewModel
    {



        protected clsGebruiker _SelectedCursist;

        public virtual clsGebruiker SelectedCursist
        {
            get { return _SelectedCursist; }
            set
            {
                _SelectedCursist = value;
                Notify();
                if (_SelectedCursist != null)
                {
                    GebruikerInschrijvingen = null;
                    Notify("GebruikerInschrijvingen", "SelectedGeslacht");
                }

            }
        }
        private int _FilterAcademie;

        public int FilterAcademie
        {
            get { return _FilterAcademie; }
            set { _FilterAcademie = value; Notify("FilterAcademie", "GebruikerInschrijvingen"); }
        }

        private int _FilterOpleiding;

        public int FilterOpleiding
        {
            get { return _FilterOpleiding; }
            set { _FilterOpleiding = value; Notify("FilterOpleiding", "GebruikerInschrijvingen"); }
        }


        private int _FilterStudiegebied;

        public int FilterStudiegebied
        {
            get { return _FilterStudiegebied; }
            set { _FilterStudiegebied = value; Notify("FilterStudiegebied", "GebruikerInschrijvingen"); }
        }


        private string _Filter;

        public string Filter
        {
            get { return _Filter; }
            set { _Filter = value; Notify("Filter", "GebruikerInschrijvingen"); }
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
        private ICommand _DeleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return _DeleteCommand = _DeleteCommand ?? new CommandHandler(() =>
                { Delete(); ExecuteOnSave.Clear(); }, !Opgeslagen);

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

        public virtual void Cancel()
        {

        }

        private ICommand _ClearCommand;

        public ICommand ClearCommand
        {
            get
            {
                return _ClearCommand = _ClearCommand ?? new CommandHandler(() =>
                { SelectedCursist = new clsGebruiker(); GebruikerInschrijvingen = null; ExecuteOnSave.Clear(); }, true);

            }
        }

        private ICommand _LeesEIDCommand;

        public ICommand LeesEIDCommand
        {
            get
            {
                return _LeesEIDCommand = _LeesEIDCommand ?? new CommandHandler(() =>
                { leesEID(SelectedCursist); Notify("SelectedGeslacht"); }, SelectedCursist != null);

            }
        }

        private ICommand _OpenImage;

        public ICommand OpenImage
        {
            get
            {
                return _OpenImage = _OpenImage ?? new CommandHandler(() =>
                { OpenImageFile((arr) => SelectedCursist.Photo = arr); }, !Opgeslagen);

            }
        }

        private string _SelectedGeslacht;

        public string SelectedGeslacht
        {
            get { return SelectedCursist == null ? Geslacht[0] : SelectedCursist.Geslacht == "M" ? Geslacht[0] : Geslacht[1]; }
            set
            {
                if (SelectedCursist != null)
                {
                    SelectedCursist.Geslacht = value.Equals("Man") ? "M" : "F";
                }
                Notify();
            }
        }


        public List<string> Geslacht
        {
            get { return new List<string>() { "Man", "Vrouw" }; }
        }

        public virtual void Save()
        {
            ExecuteOnSave.Values.ToList().ForEach(p => p());
            BLL.Fill(SelectedCursist, SelectedCursist.Backup);
        }
        public virtual void Delete()
        {
            if (MessageBox.Show("Ben je zeker dat je deze gebruiker wilt deleten?", "Bevestig Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                BLL.DeleteData(SelectedCursist);
                Gebruikers.Remove(SelectedCursist);
                SelectedCursist = null;
                //Cursisten.Remove(SelectedCursist);
            }
        }

        public virtual void vulInschrijvingen(clsGebruiker sgebruiker)
        {
            _GebruikerInschrijvingen = new ObservableCollection<clsInschrijvingListItem>();

            foreach (var o in Modules.Where(x => x.KanInschrijven() && VoorafgaandeModules.ToList().FindIndex(p => p.IDModuleType == x.IDModuleType) < 0)
                .ToList())
            {
                List<clsKlas> klassen = Klassen.Where(z => z.IDModule == o.IDModule).ToList();
                clsModuleType types = ModuleTypes.Where(t => t.IDType == o.IDModuleType).FirstOrDefault();
                if (types != null)
                {
                    clsInschrijvingListItem li = new clsInschrijvingListItem(o, klassen, sgebruiker, types.InterneNaam, DoeInschrijving, UpdateInschrijving);
                    trycatch(() => li.SelectedStudiegebied = StudieGebieden.ToList().Find(p => p.IDStudieGebied == Opleidingen.ToList().Find(x => x.IDOpleiding == li.Module.IDOpleiding).IDStudieGebied));
                    trycatch(() => li.Opleiding = Opleidingen.ToList().Find(p => p.IDOpleiding == li.Module.IDOpleiding));
                    _GebruikerInschrijvingen.Add(li);
                }
            }
        }

        protected ObservableCollection<clsInschrijvingListItem> _GebruikerInschrijvingen;

        public virtual ObservableCollection<clsInschrijvingListItem> GebruikerInschrijvingen
        {
            get
            {
                if (_SelectedCursist != null)
                {
                    if (_GebruikerInschrijvingen == null)
                    {
                        vulInschrijvingen(_SelectedCursist);
                    }
                    return _GebruikerInschrijvingen
                        .Where(o => MatchesFilter(o.Module.IDAcademieJaar, FilterAcademie))
                        .Where(o => MatchesFilter(o.Naam, Filter))
                        .Where(o => MatchesFilter(o.SelectedStudiegebied.IDStudieGebied, FilterStudiegebied))
                        .Where(o => MatchesFilter(o.Opleiding.IDOpleiding, FilterOpleiding)).ToObservableCollection();
                }
                return _GebruikerInschrijvingen;
                
            }
            set { _GebruikerInschrijvingen = value; Notify(); }
        }

        public virtual void DoeInschrijving(bool input, clsInschrijving inschr, clsKlas klas)
        {
            if (inschr != null && klas != null)
            {
                inschr.IDKlas = klas.IDKlas;
            }


            if (input)
            {
                ExecuteOnSave.Add("Inschrijving:" + inschr.IDModule + ":" + klas.IDKlas, () =>
                {
                    if (SelectedCursist.IDGebruiker > 0)
                    {
                        if (inschr.IDKlas < 1)
                            BLL.InsertData(klas);

                        inschr.IDGebruiker = SelectedCursist.IDGebruiker;

                        inschr.InschrijvingsDatum = DateTime.Now;
                        BLL.InsertData(inschr);
                    }
                });
            }
            else
            {
                string keypart = "Inschrijving:" + inschr.IDModule;

                if (!string.IsNullOrEmpty(keypart))
                    ExecuteOnSave.ToList().RemoveAll(k => k.Key.ToLower().Contains(keypart.ToLower()));
            }
        }

        public void SchrijfIn(clsInschrijving inschr, clsKlas klas)
        {
            if (inschr.IDKlas <= 0)
                BLL.InsertData(klas);
            clsInschrijving insch = new clsInschrijving
            {
                IDModule = klas.IDModule,
                IDKlas = klas.IDKlas,
                IsValidated = true,
                IDGebruiker = SelectedCursist.IDGebruiker,
                InschrijvingsDatum = DateTime.Now
            };
            BLL.InsertData(insch);
            Inschrijvingen.Add(insch);
        }

        public void SchrijfUit(clsInschrijving inschr)
        {
            BLL.DeleteData(inschr);
            if (Inschrijvingen.Contains(inschr))
            {
                Inschrijvingen.Remove(inschr);
            }
        }

        /// <summary>
        /// Find inschrijving that is selected for update and update IDKlas
        /// </summary>
        /// <param name="insch"></param>
        public void UpdateInschrijving(clsInschrijving insch)
        {
            string key = ExecuteOnSave.Keys.ToList().Find(x => x.StartsWith("Inschrijving:" + insch.IDModule));
            if (!string.IsNullOrEmpty(key))
            {
                if (key.LastIndexOf(':') != -1 && !key.Substring(key.LastIndexOf(':')).Contains(insch.IDKlas + "") && insch.IDKlas > 0)
                {
                    ExecuteOnSave.Remove(key);
                    ExecuteOnSave.Add("Inschrijving:" + insch.IDModule + ":" + insch.IDKlas, () =>
                    {
                        if (SelectedCursist.IDGebruiker > 0 && insch.IDKlas > 0)
                        {
                            insch.IDGebruiker = SelectedCursist.IDGebruiker;
                            insch.InschrijvingsDatum = DateTime.Now;
                            BLL.InsertData(insch);
                            Inschrijvingen.Add(insch);
                            clsInschrijvingListItem li = GebruikerInschrijvingen.Where(x => x.Inschrijving.IDGebruiker == insch.IDGebruiker && x.Inschrijving.IDModule == insch.IDModule && x.Inschrijving.IDKlas == insch.IDKlas).FirstOrDefault();
                            if (li != null)
                            {
                                li.Inschrijving = insch;
                            }
                        }
                    });
                }
            }
            else
            {
                ExecuteOnSave.Add("Inschrijving:" + insch.IDModule + ":" + insch.IDKlas, () =>
                {
                    if (SelectedCursist.IDGebruiker > 0 && insch.IDKlas > 0)
                    {
                        insch.IDGebruiker = SelectedCursist.IDGebruiker;
                        insch.InschrijvingsDatum = DateTime.Now;
                        BLL.InsertData(insch);
                        Inschrijvingen.Add(insch);
                        clsInschrijvingListItem li = GebruikerInschrijvingen.Where(x => x.Inschrijving.IDGebruiker == insch.IDGebruiker && x.Inschrijving.IDModule == insch.IDModule && x.Inschrijving.IDKlas == insch.IDKlas).FirstOrDefault();
                        if (li != null)
                        {
                            li.Inschrijving = insch;
                        }
                    }
                });
            }
            //BLL.UpdateData(insch);
        }
    }
}
