using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenAdministratieApp.ViewModel
{
    public class clsCustomCheckedListItem<T,K>:PropChanged
    {

        public clsCustomCheckedListItem(T info, K value, bool? isChecked, Action<bool?,K> checkedHandler)
        {
            InfoItem = info;
            ValueItem = value;
            _Checked = isChecked;
            Notify("Checked");
            CheckedHandler = checkedHandler;
        }

        public clsCustomCheckedListItem(T info, K value, bool? isChecked, Action<bool?, K> checkedHandler, string header):this(info,value,isChecked,checkedHandler)
        {
            Header = header;
        }

        

        private T _InfoItem;

        public T InfoItem
        {
            get { return _InfoItem; }
            set { _InfoItem = value; }
        }

        private K _ValueItem;

        public K ValueItem
        {
            get { return _ValueItem; }
            set { _ValueItem = value; }
        }


        private bool? _Checked;

        public bool? Checked
        {
            get { return _Checked; }
            set { _Checked = value; Notify(); CheckedHandler(value, ValueItem); }
        }

        private Action<bool?,K> _CheckedHandler;

        public Action<bool?,K> CheckedHandler
        {
            get { return _CheckedHandler; }
            set { _CheckedHandler = value; }
        }

        private string _Header;

        public string Header
        {
            get { return _Header; }
            set { _Header = value; Notify(); }
        }


    }
}
