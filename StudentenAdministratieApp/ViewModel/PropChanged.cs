using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentenAdministratieApp.ViewModel
{
    public class PropChanged : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string myPropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(myPropertyName));
            }
        }

        /// <summary>
        /// Call PropertyChanged with multiple properties
        /// </summary>
        /// <param name="parameters"></param>
        public void Notify(params string[] parameters)
        {

            if (PropertyChanged != null)
            {
                parameters.ToList().ForEach(p => NotifyPropertyChanged(p));
            }
        }

        /// <summary>
        /// Call PropertyChanged for the calling Property:
        /// { _Naam = value; Notify(); }
        /// </summary>
        /// <param name="name"></param>
        public void Notify([CallerMemberName] string name = null)
        {
            NotifyPropertyChanged(name);
        }

    }
}
