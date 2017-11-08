using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenAdministratieApp.ViewModel
{
    public class clsCustomLesmomentItem : PropChanged
    {
        public string Header { get { return ModuleType.InterneNaam; } }

        public string Leerkracht
        {
            get
            {
                string leerkracht = "";
                Leerkrachten.ForEach(p => leerkracht += p.Voornaam + " " + p.Naam + " " +p.IDGebruiker+ ", ");
                if(leerkracht.LastIndexOf(", ") > -1)
                leerkracht = leerkracht.Substring(0,leerkracht.LastIndexOf(", "));
                return leerkracht;
            }
        }

        public int Hours
        {
            //+1 omdat de index van de kalenderView niet op 0 begint
            get { return (KlasRooster.EindDatum - KlasRooster.StartDatum).Hours; }
        }

        public int StartHour
        {
            
            get { return KlasRooster.StartDatum.Hour; }
        }

        private int _DayOfWeek;
        public int DayOfWeek
        {
            get { return _DayOfWeek; }
            set { _DayOfWeek = value; }
        }

        private clsKlas _Klas;

        public clsKlas Klas
        {
            get { return _Klas; }
            set { _Klas = value; Notify(); }
        }


        private clsKlasRooster _KlasRooster;

        public clsKlasRooster KlasRooster
        {
            get { return _KlasRooster; }
            set { _KlasRooster = value; Notify(); }
        }

        private clsModule _Module;

        public clsModule Module
        {
            get { return _Module; }
            set { _Module = value; Notify(); }
        }

        private clsModuleType _ModuleType;

        public clsModuleType ModuleType
        {
            get { return _ModuleType; }
            set { _ModuleType = value; Notify("ModuleType", "Header");  }
        }

        private List<clsGebruiker> _Leerkrachten;

        public List<clsGebruiker> Leerkrachten
        {
            get { return _Leerkrachten; }
            set { _Leerkrachten = value; Notify("Leerkrachten", "Leerkracht"); }
        }


        private clsLokaal _Lokaal;

        public clsLokaal Lokaal
        {
            get { return _Lokaal; }
            set { _Lokaal = value; Notify(); }
        }


    }
}
