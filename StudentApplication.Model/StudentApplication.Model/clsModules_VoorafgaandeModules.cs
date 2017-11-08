using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsModules_VoorafgaandeModules : clsBaseModel
    {

        public clsModules_VoorafgaandeModules() : base()
        {

        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set
            {
                if (iD != value)
                {
                    iD = value;
                    _ID = value;
                    RaisePropertyChanged("ID");
                }
            }
        }
        private int iDModuleType;

        public int IDModuleType
        {
            get { return iDModuleType; }
            set
            {
                if (iDModuleType != value)
                {
                    iDModuleType = value;
                    RaisePropertyChanged("IDModuleType");
                }
            }
        }
        private int iDVoorafgaandModule;

        public int IDVoorafgaandModule
        {
            get { return iDVoorafgaandModule; }
            set
            {
                if (iDVoorafgaandModule != value)
                {
                    iDVoorafgaandModule = value;
                    RaisePropertyChanged("IDVoorafgaandModule");
                }
            }
        }
    }
}
