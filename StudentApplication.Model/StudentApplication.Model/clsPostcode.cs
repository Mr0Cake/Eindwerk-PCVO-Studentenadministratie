using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsPostcode : clsBaseModel
    {

        public clsPostcode() : base()
        {

        }


        public override string ToString()
        {
            return Postcode + " " + Plaatsnaam + ", " + Provincie;
        }

        private int iDPostcode;

        public int IDPostCode
        {
            get { return iDPostcode; }
            set
            {
                if (iDPostcode != value)
                {
                    iDPostcode = value;
                    _ID = value;
                    RaisePropertyChanged("IDPostCode");
                }
            }
        }
        private string postcode;

        public string Postcode
        {
            get { return postcode; }
            set
            {
                if (postcode != value)
                {
                    postcode = value;
                    RaisePropertyChanged("Postcode");
                }
            }
        }
        private string plaatsNaam;

        public string Plaatsnaam
        {
            get { return plaatsNaam; }
            set
            {
                if (plaatsNaam != value)
                {
                    plaatsNaam = value;
                    RaisePropertyChanged("Plaatsnaam");
                }
            }
        }
        private string provincie;

        public string Provincie
        {
            get { return provincie; }
            set
            {
                if (provincie != value)
                {
                    provincie = value;
                    RaisePropertyChanged("Provincie");
                }
            }
        }

    }
}
