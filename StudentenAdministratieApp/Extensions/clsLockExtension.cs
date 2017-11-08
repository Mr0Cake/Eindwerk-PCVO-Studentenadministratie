using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenAdministratieApp.Extensions
{
    public static class clsLockExtension
    {
        public static void AddLock<T>(this IList<T> list, T toAdd)
        {
            lock (((ICollection)list).SyncRoot)
            {
                list.Add(toAdd);
            }
        }


        public static void RemoveLock<T>(this IList<T> list, T toAdd)
        {
            lock (((ICollection)list).SyncRoot)
            {
                list.Remove(toAdd);
            }
        }

        public static void ClearLock<T>(this IList<T> list)
        {
            lock (((ICollection)list).SyncRoot)
            {
                list.Clear();
            }
        }
    }
}
