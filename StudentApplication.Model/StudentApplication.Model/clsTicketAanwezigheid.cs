using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsTicketAanwezigheid : clsTicketBase
    {

        public clsTicketAanwezigheid() : base()
        {

        }


        public override void Copy(clsTicketBase tocopy)
        {
            if (tocopy is clsTicketAanwezigheid)
            {
                clsTicketAanwezigheid copy = tocopy as clsTicketAanwezigheid;
                IDTicketAanwezigheid = copy.IDTicketAanwezigheid;
                IDKlasRooster = copy.IDKlasRooster;
                base.Copy(copy);
            }
        }


        private int iDTicketAanwezigheid;

        public int IDTicketAanwezigheid
        {
            get { return iDTicketAanwezigheid; }
            set
            {
                if (iDTicketAanwezigheid != value)
                {
                    iDTicketAanwezigheid = value;
                    _ID = value;
                    RaisePropertyChanged("IDTicketAanwezigheid");
                }
            }
        }
        private int iDKlasRooster;

        public int IDKlasRooster
        {
            get { return iDKlasRooster; }
            set
            {
                if (iDKlasRooster != value)
                {
                    iDKlasRooster = value;
                    RaisePropertyChanged("IDAanwezigheid");
                }
            }
        }

        //idstudent --> ticketbase
        //idgebruiker leraar --> TicketBase
    }
}
