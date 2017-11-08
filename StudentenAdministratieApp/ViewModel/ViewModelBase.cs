using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using System.ComponentModel;
using System.Collections.ObjectModel;
using StudentApplication.Model;
using System.Windows.Input;
using MvvmValidation;
using Microsoft.Win32;
using BLL.Extensions;
using EID.Wrapper;
using System.Windows;

namespace StudentenAdministratieApp.ViewModel
{
    public class ViewModelBase : PropChanged
    {
        public ViewModelBase()
        {
            Validator = new ValidationHelper();
            if (!BLL.HasConnection)
            {

                System.Windows.MessageBox.Show("Kon geen verbinding met de database maken.");
                System.Environment.Exit(0);

            }
        }

        /// <summary>
        /// Validationhelper
        /// https://github.com/pglazkov/mvvmvalidation
        /// </summary>
        protected ValidationHelper Validator { get; private set; }



        #region utilities

        private static clsCommonBLL _BLL = new clsCommonBLL();
        /// <summary>
        /// BLL instance
        /// </summary>
        public static clsCommonBLL BLL
        {
            get { return _BLL; }
        }

        /// <summary>
        /// executes the action later
        /// </summary>
        /// <param name="withDelay"></param>
        /// <param name="delayms"></param>
        public void DelayedAction(Action withDelay, int delayms)
        {
            Task.Delay(delayms).ContinueWith(_ => withDelay());
        }

        public void trycatch(Action a)
        {
            try { a(); } catch { }
        }


        public static void leesEID(clsGebruiker toFill)
        {
            try
            {
                Wrapper wrapper = new Wrapper();
                CardData data = new CardData();

                data = (CardData)wrapper.GetCardData();

                if (data.FirstCard == null)
                {
                    //No card found
                    MessageBox.Show("Identiteitskaart niet gedetecteerd. Controleer het toestel en probeer opnieuw.");
                }
                else
                {
                    toFill.Naam = data.FirstCard.Surname;
                    toFill.Voornaam = data.FirstCard.FirstNames;
                    toFill.RijksregisterNummer = data.FirstCard.NationalNumber;
                    toFill.Nationaliteit = data.FirstCard.Nationality;
                    toFill.GeboorteDatum = data.FirstCard.BirthDate;

                    //Kan zijn dat Male & Female niet de juiste string waarde zijn
                    //Heb nu geen eid reader bij de hand dus kan dit niet testen.
                    //in de database staat dit ook niet goed heb ik gemerkt.
                    if (data.FirstCard.Gender == "M")
                    {
                        toFill.Geslacht = "M";

                    }
                    else if (data.FirstCard.Gender == "F" || data.FirstCard.Gender == "V")
                    {
                        toFill.Geslacht = "F";
                    }


                    toFill.IDPostCode = PostCodes.ToList().Find(p => p.Postcode == data.FirstCard.ZipCode).IDPostCode;

                    toFill.Photo = data.FirstCard.PhotoData; //jpg-image



                    string straat = data.FirstCard.StreetAndNumber;
                    string bus = "";
                    string nummer = "";
                    int index = -1;
                    if ((index = straat.LastIndexOf('/')) > -1)
                    {
                        //adres bevat busnummer
                        bus = straat.Substring(index);
                        straat = straat.Substring(0, index);
                    }
                    if ((index = straat.LastIndexOf(' ')) > -1)
                    {
                        nummer = straat.Substring(index);
                        straat = straat.Substring(0, index);
                    }


                    toFill.HuisNummer = nummer + bus;
                    toFill.Straat = straat;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Er heeft zich een fout voorgedaan bij het lezen van de identiteitskaart.");
            }
        }

        public class CommandHandler : ICommand
        {
            private Action _action;
            private bool _canExecute;
            public CommandHandler(Action action, bool canExecute)
            {
                _action = action;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                _action();
            }
        }




        #region Properties

        private Dictionary<string, Action> _ExecuteOnSave;
        /// <summary>
        /// Een lijst van Actions die pas uitgevoerd mogen worden als de gebruiker op Save drukt.
        /// Gebruik:
        /// Vul de lijst met een identifier(string) die de actie moet voorstellen, Bijvoorbeeld ik wil de gebruiker 2 inschrijven in de Module 4
        /// ExecuteOnSave.Add(IDGebruiker+":"+IDModule, () => bll.inschrijven(IDGebruiker, IDModule));
        /// Wanneer je dan bijvoorbeeld een gebruiker wilt uitschrijven voor een module kijk je of de key "2:4" reeds bestaat, dan weet je dat de gebruiker waarschijnlijk een inschrijving heeft willen doen en deze wilt verwijderen,
        /// dus delete het item uit de dictionary en dan is alles terug zoals ervoor.
        /// 
        /// Moet nog getest worden of ID:ID voldoende is, het kan zijn dat hier "inschrijving" of iets dergelijk voor gezet kan worden.
        /// </summary>
        public Dictionary<string, Action> ExecuteOnSave
        {
            get { return _ExecuteOnSave = _ExecuteOnSave ?? new Dictionary<string, Action>(); }
            set { _ExecuteOnSave = value; }
        }
       
        private bool _Opgeslagen = false;

        public bool Opgeslagen
        {
            get { return _Opgeslagen; }
            set { _Opgeslagen = value; Notify(); }

        }


        private string _ValidationErrors;

        public string ValidationErrors
        {
            get { return _ValidationErrors; }
            set { _ValidationErrors = value; Notify(); }
        }


        private static clsListUpdater _ListUpdater = new clsListUpdater();

        /// <summary>
        /// instance to update lists when a networkmessage is received.
        /// </summary>
        public static clsListUpdater ListUpdater
        {
            get { return _ListUpdater; }
            set { _ListUpdater = value; }
        }



        #endregion

        #region Methods

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/cc221415(v=vs.95).aspx
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public bool OpenImageFile(Action<byte[]> stream)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (.jpg)|*.jpg";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                stream(System.IO.File.ReadAllBytes(openFileDialog.FileName));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if filter matches start of string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected bool MatchesFilter(string input, string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return true;
            }

            if (filter.Length <= input.Length && !string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(filter))
            {
                //string part = input.Substring(0, filter.Length).ToLower();
                //return filter.ToLower().Equals(part);
                return input.Contains(filter);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the specified input id is found in the list
        /// </summary>
        /// <param name="input"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected bool MatchesFilter(int input, int filter)
        {
            if (filter == 0)
                return true;

            if (input != 0 && filter != 0)
            {
                return filter == input;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #endregion


        #region DataLists

        #region WillNotChangeOften
        private static ObservableCollection<clsPostcode> _PostCodes;

        public static ObservableCollection<clsPostcode> PostCodes
        {
            get { return _PostCodes = _PostCodes ?? BLL.GetData<clsPostcode>(); }
            set { _PostCodes = value; }
        }


        public static ObservableCollection<clsModule> _Modules;
        public static ObservableCollection<clsModule> Modules
        {
            get { return _Modules = _Modules ?? BLL.GetData<clsModule>(); }
            set { _Modules = value; }
        }


        private static ObservableCollection<clsStudieGebied> _StudieGebieden;

        public static ObservableCollection<clsStudieGebied> StudieGebieden
        {
            get { return _StudieGebieden = _StudieGebieden ?? BLL.GetData<clsStudieGebied>(); }
            set { _StudieGebieden = value; }
        }

        private static ObservableCollection<clsOpleiding> _Opleidingen;

        public static ObservableCollection<clsOpleiding> Opleidingen
        {
            get { return _Opleidingen = _Opleidingen ?? BLL.GetData<clsOpleiding>(); }
            set { _Opleidingen = value; }
        }

        private static ObservableCollection<clsAcademieJaar> _Academiejaren;

        public static ObservableCollection<clsAcademieJaar> Academiejaren
        {
            get { return _Academiejaren = _Academiejaren ?? BLL.GetData<clsAcademieJaar>(); }
            set { _Academiejaren = value; }
        }


        private static ObservableCollection<clsLokaal> _Lokalen;

        public static ObservableCollection<clsLokaal> Lokalen
        {
            get { return _Lokalen = _Lokalen ?? BLL.GetData<clsLokaal>(); }
            set { _Lokalen = value; }
        }

        private static ObservableCollection<clsModuleType> _ModuleTypes;

        public static ObservableCollection<clsModuleType> ModuleTypes
        {
            get { return _ModuleTypes = _ModuleTypes ?? BLL.GetData<clsModuleType>(); }
            set { _ModuleTypes = value; }
        }

        private static ObservableCollection<clsTaal> _Talen;

        public static ObservableCollection<clsTaal> Talen
        {
            get { return _Talen = _Talen ?? BLL.GetData<clsTaal>(); }
            set { _Talen = value; }
        }

        private static ObservableCollection<clsNiveau> _Niveaus;

        public static ObservableCollection<clsNiveau> Niveaus
        {
            get { return _Niveaus = _Niveaus ?? BLL.GetData<clsNiveau>(); }
            set { _Niveaus = value; }
        }

        private static ObservableCollection<clsGebruikersType> _GebruikerTypes;

        public static ObservableCollection<clsGebruikersType> GebruikerTypes
        {
            get { return _GebruikerTypes = _GebruikerTypes ?? BLL.GetData<clsGebruikersType>(); }
            set { _GebruikerTypes = value; }
        }


        private static ObservableCollection<clsModules_GebruikersLeraars> _Modules_GebruikersLeraars;

        public static ObservableCollection<clsModules_GebruikersLeraars> Modules_GebruikersLeraars
        {
            get { return _Modules_GebruikersLeraars = _Modules_GebruikersLeraars ?? BLL.GetData<clsModules_GebruikersLeraars>(); }
            set { _Modules_GebruikersLeraars = value; }
        }


        #endregion


        #region WillChangeOften
        private static ObservableCollection<clsGebruiker> _Gebruikers;
        public static ObservableCollection<clsGebruiker> Gebruikers
        {
            get { return _Gebruikers = _Gebruikers ?? BLL.GetDataWithBackup<clsGebruiker>((obj1, obj2) => obj1.Backup = obj2); }
            set { _Gebruikers = value; }
        }

        private static ObservableCollection<clsGebruiker> _Cursisten;

        public static ObservableCollection<clsGebruiker> Cursisten
        {
            get
            {
                if (_Cursisten == null)
                {


                    _Cursisten = Gebruikers.Where(p => Gebruikers_TypeGebruikers.ToList().FindIndex(o => o.IDGebruiker == p.IDGebruiker && GebruikerTypes.ToList().Find(z => z.TypeNaam.Equals("Student")).IDType == o.IDType) > -1).ToObservableCollection();
                    Gebruikers.CollectionChanged += Gebruikers_CollectionChanged;



                }
                return _Cursisten;

            }
            set { _Cursisten = value; }
        }

        /// <summary>
        /// Synchroniseer cursisten met gebruikers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Gebruikers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (var x in e.NewItems)
                {
                    try
                    {
                        //is dit een cursist?
                        if (Gebruikers_TypeGebruikers.Where(p => p.IDGebruiker == (x as clsGebruiker).IDGebruiker).Select(o => GebruikerTypes.Where(a => a.IDType == o.IDType).FirstOrDefault().TypeNaam).FirstOrDefault() == "Student")
                            _Cursisten.Add(x as clsGebruiker);
                    }
                    catch (NullReferenceException)
                    {
                        //niet gevonden
                    }
                }
            }

            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (var x in e.OldItems)
                {
                    try
                    {
                        //is dit een cursist?
                        if (Gebruikers_TypeGebruikers.Where(p => p.IDGebruiker == (x as clsGebruiker).IDGebruiker).Select(o => GebruikerTypes.Where(a => a.IDType == o.IDType).FirstOrDefault().TypeNaam).FirstOrDefault() == "Student")
                            _Cursisten.Remove(x as clsGebruiker);
                    }
                    catch (NullReferenceException)
                    {
                        //niet gevonden
                    }
                }
            }
        }


        private static ObservableCollection<clsKlas> _Klassen;

        public static ObservableCollection<clsKlas> Klassen
        {
            get
            {
                if (_Klassen == null)
                {
                    _Klassen = BLL.GetData<clsKlas>();
                    _Klassen.ToList().ForEach(x => x.ModuleType = Modules.Where(i => i.IDModule == x.IDModule).Select(p => ModuleTypes.ToList().Find(z => z.IDType == p.IDModuleType)).FirstOrDefault());
                }
                return _Klassen;
            }
            set { _Klassen = value; }
        }

        private static ObservableCollection<clsModules_VoorafgaandeModules> _VoorafgaandeModules;

        public static ObservableCollection<clsModules_VoorafgaandeModules> VoorafgaandeModules
        {
            get { return _VoorafgaandeModules = _VoorafgaandeModules ?? BLL.GetData<clsModules_VoorafgaandeModules>(); }
            set { _VoorafgaandeModules = value; }
        }

        private static ObservableCollection<clsGebruikers_TypeGebruikers> _Gebruikers_TypeGebruikers;

        public static ObservableCollection<clsGebruikers_TypeGebruikers> Gebruikers_TypeGebruikers
        {
            get { return _Gebruikers_TypeGebruikers = _Gebruikers_TypeGebruikers ?? BLL.GetData<clsGebruikers_TypeGebruikers>(); }
            set { _Gebruikers_TypeGebruikers = value; }
        }


        private static ObservableCollection<clsInschrijving> _Inschrijvingen;

        public static ObservableCollection<clsInschrijving> Inschrijvingen
        {
            get { return _Inschrijvingen = _Inschrijvingen ?? BLL.GetData<clsInschrijving>(); }
            set { _Inschrijvingen = value; }
        }








        private static ObservableCollection<clsAanwezigheid> _Aanwezigheden;

        public static ObservableCollection<clsAanwezigheid> Aanwezigheden
        {
            get { return _Aanwezigheden = _Aanwezigheden ?? BLL.GetDataWithBackup<clsAanwezigheid>((obj1, obj2) => obj1.Backup = obj2); }
            set { _Aanwezigheden = value; }
        }

        private static ObservableCollection<clsKlasRooster> _KlasRoosters;

        public static ObservableCollection<clsKlasRooster> KlasRoosters
        {
            get { return _KlasRoosters = _KlasRoosters ?? BLL.GetData<clsKlasRooster>(); }
            set { _KlasRoosters = value; }
        }
        #endregion


        #endregion


    }
}
