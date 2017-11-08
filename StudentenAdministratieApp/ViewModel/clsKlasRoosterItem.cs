﻿using MvvmValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApplication.Model;

namespace StudentenAdministratieApp.ViewModel
{
    public class clsKlasRoosterItem:PropChanged
    {
        public clsKlasRoosterItem(clsKlasRooster item, clsAanwezigheid aw, Action<bool?, clsKlasRooster, clsAanwezigheid> checkedHandler, bool? isChecked, string header)
        {
            ItemContainer = item;
            Aanwezigheid = aw;
            CheckedHandler = checkedHandler;
            Validation = new ValidationHelper();
            _IsChecked = isChecked;
            Header = header;
            Notify("IsChecked");
            InitialValue = isChecked;
        }

        public clsKlasRoosterItem(clsKlasRooster item, clsAanwezigheid aw, Action<bool?, clsKlasRooster, clsAanwezigheid> checkedHandler, bool? isChecked, string header, byte[] image):this(item, aw, checkedHandler, isChecked, header)
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
            set { _IsChecked = value; CheckedHandler(value, ItemContainer, Aanwezigheid); Notify("IsChecked", "IsCheckedTranslation"); }
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

        private clsKlasRooster _ItemContainer;

        public clsKlasRooster ItemContainer
        {
            get { return _ItemContainer; }
            set { _ItemContainer = value; }
        }


        private clsAanwezigheid _Aanwezigheid;

        public clsAanwezigheid Aanwezigheid
        {
            get { return _Aanwezigheid; }
            set { _Aanwezigheid = value; }
        }


        private Action<bool?, clsKlasRooster, clsAanwezigheid> _CheckedHandler;

        public Action<bool?, clsKlasRooster, clsAanwezigheid> CheckedHandler
        {
            get { return _CheckedHandler; }
            set { _CheckedHandler = value; }
        }



    }
}
