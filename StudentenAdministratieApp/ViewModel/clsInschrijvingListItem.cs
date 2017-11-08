using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApplication.Model;
using System.Collections.ObjectModel;


namespace StudentenAdministratieApp.ViewModel
{
    public class clsInschrijvingListItem : PropChanged
    {
        public clsInschrijvingListItem(clsModule module, List<clsKlas> klassen, clsGebruiker cursist, string naam, Action<bool, clsInschrijving, clsKlas> checkedHandler)
        {
            CheckedHandler = checkedHandler;
            Module = module;
            Klassen = klassen;
            Naam = naam;
            if (Klassen == null || Klassen.Count == 0)
            {
                Klassen.Add(new clsKlas { Naam = "A", IDModule = Module.IDModule });
                _SelectedKlas = Klassen[0];
            }
            if (Inschrijving == null)
            {
                Inschrijving = new clsInschrijving { IDModule = module.IDModule, IDGebruiker = cursist.IDGebruiker, IsValidated = true };
            }


            Notify("SelectedKlas");

        }

        public clsInschrijvingListItem(clsModule module, List<clsKlas> klassen, clsGebruiker cursist, string naam, Action<bool, clsInschrijving, clsKlas> checkedHandler, Action<clsInschrijving> updateHandler) : this(module, klassen, cursist, naam, checkedHandler)
        {
            UpdateHandler = updateHandler;
        }

        public clsInschrijvingListItem(clsInschrijving inschrijving, clsModule module, List<clsKlas> klassen, clsKlas klas, clsGebruiker cursist, string naam, Action<bool, clsInschrijving, clsKlas> checkedHandler) : this(module, klassen, cursist, naam, checkedHandler)
        {

            if (inschrijving != null)
            {
                Check();
                Inschrijving = inschrijving;
                if (klas != null)
                {
                    //bug: klas zit niet in klassen
                    _SelectedKlas = Klassen.Where(k => k.IDKlas == inschrijving.IDKlas).FirstOrDefault();
                    Notify("SelectedKlas");
                }

            }
            else
            {
                Inschrijving = new clsInschrijving();
                if (klas != null)
                {
                    _SelectedKlas = Klassen[Klassen.IndexOf(klas)];
                    Notify("SelectedKlas");
                }
            }
        }

        public clsInschrijvingListItem(clsInschrijving inschrijving, clsModule module, List<clsKlas> klassen, clsKlas klas, clsGebruiker cursist, string naam, Action<bool, clsInschrijving, clsKlas> checkedHandler, Action<clsInschrijving> updateInschrijving) : this(inschrijving, module, klassen, klas, cursist, naam, checkedHandler)
        {
            UpdateHandler = updateInschrijving;
            if (klas != null)
            {
                _SelectedKlas = Klassen.Where(k => k.IDKlas == inschrijving.IDKlas).FirstOrDefault();
                Notify("SelectedKlas");
            }
        }

        private string _Naam;

        public string Naam
        {
            get { return _Naam; }
            set { _Naam = value; Notify(); }
        }



        private clsGebruiker _Gebruiker;

        public clsGebruiker Gebruiker
        {
            get { return _Gebruiker; }
            set { _Gebruiker = value; }
        }


        private bool _IsChecked = false;

        public bool Checked
        {
            get { return _IsChecked; }
            set
            {
                if (value != _IsChecked)
                {
                    _IsChecked = value;
                    Notify("Checked", "NotChecked");
                    int selected = Klassen.FindIndex(p => p.Equals(SelectedKlas));
                    CheckedHandler(value, Inschrijving, Klassen[selected]);
                }
            }
        }

        public bool NotChecked
        {
            get { return !Checked; }
        }

        public void Check()
        {
            _IsChecked = !_IsChecked;
            Notify("IsChecked");
        }

        private Action<bool, clsInschrijving, clsKlas> _CheckedHandler;

        public Action<bool, clsInschrijving, clsKlas> CheckedHandler
        {
            get { return _CheckedHandler; }
            set { _CheckedHandler = value; }
        }

        private Action<clsInschrijving> _updateHandler;

        public Action<clsInschrijving> UpdateHandler
        {
            get { return _updateHandler; }
            set { _updateHandler = value; }
        }


        private clsInschrijving _Inschrijving;

        public clsInschrijving Inschrijving
        {
            get { return _Inschrijving; }
            set { _Inschrijving = value; }
        }

        private List<clsKlas> _Klassen;

        public List<clsKlas> Klassen
        {
            get { return _Klassen; }
            set { _Klassen = value; }
        }


        private clsKlas _SelectedKlas;

        public clsKlas SelectedKlas
        {
            get { return _SelectedKlas = _SelectedKlas ?? Klassen.FirstOrDefault() ?? new clsKlas { Naam = "A", IDModule = Module.IDModule }; }
            set
            {
                _SelectedKlas = value;
                Notify();
                if (value != null)
                {
                    Inschrijving.IDKlas = value.IDKlas;
                    int selected = Klassen.FindIndex(p => p.Equals(SelectedKlas));

                    UpdateHandler(Inschrijving);
                }
            }
        }

        private clsStudieGebied _SelectedStudiegebied;

        public clsStudieGebied SelectedStudiegebied
        {
            get
            {
                if (_SelectedStudiegebied != null)
                    return _SelectedStudiegebied;

                if (Opleiding != null && Opleiding.IDOpleiding != 0)
                    _SelectedStudiegebied = ViewModelBase.StudieGebieden.Where(x => x.IDStudieGebied == Opleiding.IDStudieGebied).FirstOrDefault();

                return _SelectedStudiegebied;

            }
            set { _SelectedStudiegebied = value; Notify(); }
        }




        private clsModule _Module;

        public clsModule Module
        {
            get { return _Module; }
            set { _Module = value; Notify(); }
        }

        private clsOpleiding _Opleiding;

        public clsOpleiding Opleiding
        {
            get
            {
                if (_Opleiding != null)
                    return _Opleiding;

                if (Module != null && Module.IDModule != 0)
                    _Opleiding = ViewModelBase.Opleidingen.Where(x => x.IDOpleiding == Module.IDOpleiding).FirstOrDefault();

                return _Opleiding;
            }
            set { _Opleiding = value; Notify(); }
        }







    }
}
