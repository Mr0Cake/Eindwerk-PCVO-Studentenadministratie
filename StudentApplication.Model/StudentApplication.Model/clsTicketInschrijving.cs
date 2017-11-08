using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsTicketInschrijving : clsTicketBase
    {

        public clsTicketInschrijving() : base()
        {

        }




        public override void Copy(clsTicketBase tocopy)
        {
            if (tocopy is clsTicketInschrijving)
            {
                clsTicketInschrijving copy = tocopy as clsTicketInschrijving;
                IDTicketInschrijving = copy.IDTicketInschrijving;
                IDInschrijving = copy.IDInschrijving;
                IDModule = copy.IDModule;
                base.Copy(copy);
            }

        }

        private int iDTicketInschrijving;

        public int IDTicketInschrijving
        {
            get { return iDTicketInschrijving; }
            set
            {
                if (iDTicketInschrijving != value)
                {
                    iDTicketInschrijving = value;
                    _ID = value;
                    RaisePropertyChanged("IDTicketInschrijving");
                }
            }
        }


        private int iDInschrijving;

        public int IDInschrijving
        {
            get { return iDInschrijving; }
            set
            {
                if (iDInschrijving != value)
                {
                    iDInschrijving = value;
                    RaisePropertyChanged("IDInschrijving");
                }
            }
        }
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

        //IDGebruiker IDleraar --> ticketbase
    }
}
