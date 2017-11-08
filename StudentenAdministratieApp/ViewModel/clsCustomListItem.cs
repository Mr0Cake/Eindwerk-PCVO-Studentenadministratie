using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenAdministratieApp.ViewModel
{
    public class clsCustomListItem<T>:PropChanged
    {
        public clsCustomListItem(T input)
        {
            ContainingObject = input;
        }
        private T _ContainingObject;
        /// <summary>
        /// Include an object with the listItem
        /// </summary>
        public T ContainingObject
        {
            get { return _ContainingObject; }
            set { _ContainingObject = value; }
        }

        private bool _IsChecked;
        /// <summary>
        /// Checked, executes the handler if checked is changed
        /// </summary>
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
                if (_CheckedHandler != null && ContainingObject != null)
                    CheckedHandler(ContainingObject, value);
                Notify();
            }
        }

        private Action<T, bool> _CheckedHandler;
        /// <summary>
        /// Optional handler to do stuff when Checkbox checked changes
        /// </summary>
        public Action<T, bool> CheckedHandler
        {
            get { return _CheckedHandler; }
            set { _CheckedHandler = value; }
        }

        private bool _IsLocked;
        /// <summary>
        /// object is locked
        /// </summary>
        public bool IsLocked
        {
            get { return _IsLocked; }
            set
            {
                _IsLocked = value;
                if (_LockedHandler != null && _ContainingObject != null)
                    LockedHandler(ContainingObject, value);
                Notify();
            }
        }

        private int _LockedBy = -1;
        /// <summary>
        /// id that locked the object
        /// </summary>
        public int LockedBy
        {
            get { return _LockedBy; }
            set { _LockedBy = value; }
        }

        private string _LockedMessage = "{0}";

        public string LockedMessage
        {
            get { return string.Format(_LockedMessage, _LockedBy>-1 ? ""+LockedBy:""); }
            set {
                if(value != null && value.Length>0 && value.IndexOf("{0}")>-1)
                    _LockedMessage = value;

            }
        }

        private bool _IsDisabled;

        public bool IsDisabled
        {
            get { return _IsDisabled; }
            set { _IsDisabled = value; }
        }



        private string _Name;
        /// <summary>
        /// ListItem Name
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }


        private Action<T, bool> _LockedHandler;
        /// <summary>
        /// Optional handler to do stuff when Checkbox Locked changes
        /// </summary>
        public Action<T, bool> LockedHandler
        {
            get { return _LockedHandler; }
            set { _LockedHandler = value; }
        }

      
    }
}
