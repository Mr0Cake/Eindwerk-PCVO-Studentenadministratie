using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApplication.Model.Helpers;


namespace StudentApplication.Model
{
    public class clsStudieGebied : clsBaseModel
    {

        public clsStudieGebied() : base()
        {

        }


        private int iDStudieGebied;

        public int IDStudieGebied
        {
            get { return iDStudieGebied; }
            set
            {
                if (iDStudieGebied != value)
                {
                    iDStudieGebied = value;
                    _ID = value;
                    RaisePropertyChanged("IDStudieGebied");
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

        private ObservableCollection<clsOpleiding> opleiding;
        public ObservableCollection<clsOpleiding> Opleiding
        {
            get
            {

                return opleiding = opleiding ?? clsBLLHelper.CustomBLL.GetDataListSpecific<clsOpleiding>(IDStudieGebied);
            }
            set { opleiding = value; }
        }
    }
}
