using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmValidation;

namespace StudentenAdministratieApp.ViewModel
{
    public class clsCheckedClass<T> : PropChanged
    {
        public clsCheckedClass(T item, Action<bool?, T> checkedHandler, bool? isChecked, string header)
        {
            ItemContainer = item;
            CheckedHandler = checkedHandler;
            Validation = new ValidationHelper();
            _IsChecked = isChecked;
            Header = header;
            Notify("IsChecked");
            InitialValue = isChecked;
        }

        public clsCheckedClass(T item, Action<bool?, T> checkedHandler, bool? isChecked, string header, byte[] image):this(item, checkedHandler, isChecked, header)
        {
            CustomImage = image;
        }

        private byte[] _CustomImage;

        public byte[] CustomImage
        {
            get { return _CustomImage; }
            set { _CustomImage = value; Notify(); }
        }


        public readonly bool? InitialValue;

        private bool? _IsChecked;

        public bool? IsChecked
        {
            get { return _IsChecked; }
            set { _IsChecked = value; CheckedHandler(value, ItemContainer); Notify("IsChecked","IsCheckedTranslation"); }
        }


        private string[] _TranslatedValues = { "Aanwezig", "Gewettigd Afwezig", "Ongewettigd Afwezig" };

        public string[] TranslatedValues
        {
            get { return _TranslatedValues; }
            set { _TranslatedValues = value; }
        }



        public string IsCheckedTranslation
        {
            get
            {
                return IsChecked == true ? TranslatedValues[0] : IsChecked == false ? TranslatedValues[1] : TranslatedValues[2];
            }
        }

        private string _Header;

        public string Header
        {
            get { return _Header; }
            set { _Header = value; Notify(); }
        }


        public ValidationHelper Validation { get; set; }

        private T _ItemContainer;

        public T ItemContainer
        {
            get { return _ItemContainer; }
            set { _ItemContainer = value; }
        }

        


        private Action<bool?, T> _CheckedHandler;

        public Action<bool?, T> CheckedHandler
        {
            get { return _CheckedHandler; }
            set { _CheckedHandler = value; }
        }



    }
}
