using StudentApplication.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsModuleType : clsBaseModel
    {


        public clsModuleType() : base()
        {

        }

        private int iDModuleType;

        public int IDType
        {
            get { return iDModuleType; }
            set
            {
                if (iDModuleType != value)
                {
                    iDModuleType = value;
                    _ID = value;
                    RaisePropertyChanged("IDType");
                }
            }
        }
        private string code;

        public string Code
        {
            get { return code; }
            set
            {
                if (code != value)
                {
                    code = value;
                    RaisePropertyChanged("Code");
                }
            }
        }
        private string officieleNaam;

        public string OfficieleNaam
        {
            get { return officieleNaam; }
            set
            {
                if (officieleNaam != value)
                {
                    officieleNaam = value;
                    RaisePropertyChanged("OfficieleNaam");
                }
            }
        }
        private string interneNaam;

        public string InterneNaam
        {
            get { return interneNaam; }
            set
            {
                if (interneNaam != value)
                {
                    interneNaam = value;
                    RaisePropertyChanged("InterneNaam");
                }
            }
        }
        private ObservableCollection<clsModule> module;
        public ObservableCollection<clsModule> Module
        {
            get
            {

                return module = module ?? clsBLLHelper.CustomBLL.GetDataListSpecific<clsModule>(IDType);
            }
            set { module = value; }
        }
    }
}
