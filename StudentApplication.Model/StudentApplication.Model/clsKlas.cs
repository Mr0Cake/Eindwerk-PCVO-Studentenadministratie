using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsKlas : clsBaseModel
    {

        public clsKlas() : base()
        {
           
        }



        private int iDKlas;

        public int IDKlas
        {
            get { return iDKlas; }
            set
            {
                if (iDKlas != value)
                {
                    iDKlas = value;
                    _ID = value;
                    RaisePropertyChanged("IDKlas");
                }
            }
        }
        private ObservableCollection<clsInschrijving> ins;
        public ObservableCollection<clsInschrijving> Inschrijving
        {
            get
            {
                return ins ?? (ins = bllCustom.GetDataListSpecific<clsInschrijving>(IDKlas));
            }
            set { ins = value; }
        }

        private clsModuleType moduleType;
        public clsModuleType ModuleType
        {
            get
            {
                return moduleType ?? (moduleType = bllCommon.GetData<clsModuleType>(IDModule));
            }
            set { moduleType = value; }
        }
        //private int iDGebruikerStudent;

        //public int IDGebruikerStudent
        //{
        //    get { return iDGebruikerStudent; }
        //    set
        //    {
        //        if (iDGebruikerStudent != value)
        //        {
        //            iDGebruikerStudent = value;
        //            RaisePropertyChanged("IDGebruikerStudent");
        //        }
        //    }
        //}
        private int iDModule;

        public int IDModule
        {
            get { return iDModule; }
            set
            {
                if (iDModule != value)
                {
                    iDModule = value;
                    RaisePropertyChanged("IDModule");
                }
            }
        }
        private string naam;

        public string Naam
        {
            get { return naam; }
            set
            {
                if (naam != value)
                {
                    naam = value;
                    RaisePropertyChanged("Naam");
                }
            }
        }
        //was er hier ook geen idLeerkracht?
    }
}
