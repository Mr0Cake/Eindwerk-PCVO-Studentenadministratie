using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{

    public class clsTicketBase : clsBaseModel
    {

        public clsTicketBase() : base()
        {

        }


        /// <summary>
        /// Copies the tocopy object into the current object.
        /// This is to not lose memory references
        /// </summary>
        /// <param name="tocopy"></param>
        public virtual void Copy(clsTicketBase tocopy)
        {
            IDGebruikerMedewerker = tocopy.IDGebruikerMedewerker;
            IDGebruikerStudent = tocopy.IDGebruikerStudent;
        }

        private int iDGebruikerLeraar;

        public int IDGebruikerMedewerker
        {
            get { return iDGebruikerLeraar; }
            set
            {
                if (iDGebruikerLeraar != value)
                {
                    iDGebruikerLeraar = value;
                    RaisePropertyChanged("IDGebruikerMedewerker");
                }
            }
        }

        private int iDGebruikerStudent;

        public int IDGebruikerStudent
        {
            get { return iDGebruikerStudent; }
            set
            {
                if (iDGebruikerStudent != value)
                {
                    iDGebruikerStudent = value;
                    RaisePropertyChanged("IDGebruikerStudent");
                }
            }
        }
    }
}
