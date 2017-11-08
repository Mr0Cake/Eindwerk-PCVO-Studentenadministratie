using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenAdministratieApp.ViewModel
{
    public class clsCustomTreeviewItem<T,K>:IEnumerable<clsCustomListItem<K>>
    {
        private clsCustomListItem<T> _Item;

        public clsCustomListItem<T> Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private List<clsCustomListItem<K>> _Items = new List<clsCustomListItem<K>>();

        public List<clsCustomListItem<K>> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }


        public IEnumerator<clsCustomListItem<K>> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
