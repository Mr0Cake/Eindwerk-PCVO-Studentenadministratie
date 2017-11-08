using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Model
{
    public class clsMailTemplate : clsBaseModel
    {

        public clsMailTemplate() : base()
        {

        }

        private int iDTemplate;

        public int IDTemplate
        {
            get { return iDTemplate; }
            set
            {
                if (iDTemplate != value)
                {
                    iDTemplate = value;
                    _ID = value;
                    RaisePropertyChanged("IDTemplate");
                }
            }
        }
        private int iDTaal;

        public int IDTaal
        {
            get { return iDTaal; }
            set
            {
                if (iDTaal != value)
                {
                    iDTaal = value;
                    RaisePropertyChanged("IDTaal");
                }
            }
        }
        private string onderwerp;

        public string Onderwerp
        {
            get { return onderwerp; }
            set
            {
                if (onderwerp != value)
                {
                    onderwerp = value;
                    RaisePropertyChanged("Onderwerp");
                }
            }
        }
        private string titel;

        public string Titerl
        {
            get { return titel; }
            set
            {
                if (titel != value)
                {
                    titel = value;
                    RaisePropertyChanged("Titerl");
                }
            }
        }
        private string bericht;

        public string Bericht
        {
            get { return bericht; }
            set
            {
                if (bericht != value)
                {
                    bericht = value;
                    RaisePropertyChanged("Bericht");
                }
            }
        }
        private string slot;

        public string Slot
        {
            get { return slot; }
            set
            {
                if (slot != value)
                {
                    slot = value;
                    RaisePropertyChanged("Slot");
                }
            }
        }
        private byte[] slotFoto;

        public byte[] SlotFoto
        {
            get { return slotFoto; }
            set
            {
                if (slotFoto != value)
                {
                    slotFoto = value;
                    RaisePropertyChanged("SlotFoto");
                }
            }
        }
    }
}
