using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Extensions;
using System.Collections.ObjectModel;
using DAL;
using System.Diagnostics;

namespace BLL
{


    public class clsCommonBLL
    {
        /// <summary>
        /// Generic dictionary to store the Type and the method to fill the object.
        /// </summary>
        protected static GenericDicionary FillMethods = new GenericDicionary();


        public bool HasConnection
        {
            get { return DAL.clsCommonMethods.HasConnection; }
        }


        /// <summary>
        /// call Fill<objectName>(DataRow dr) and it will fill the object based on the datatable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        internal T Fill<T>(DataRow dr) where T : class, new()
        {
            clsDataTableAdapter<T> adapterFor = null;
            if (!FillMethods.GetValue(typeof(T), out adapterFor))
            {
                adapterFor = new clsDataTableAdapter<T>();
                FillMethods.Add(typeof(T), adapterFor);
            }
            return adapterFor.FillObject(dr);
        }

        /// <summary>
        /// Fills existing object toFill using the datarow as datasource
        /// used for inserts and updates.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="toFill"></param>
        public void Fill<T>(DataRow dr, T toFill) where T : class, new()
        {
            clsDataTableAdapter<T> adapterFor = null;
            if (!FillMethods.GetValue(typeof(T), out adapterFor))
            {
                adapterFor = new clsDataTableAdapter<T>();
                FillMethods.Add(typeof(T), adapterFor);
            }
            adapterFor.FillObject(dr, toFill);
        }


        /// <summary>
        /// Fills existing objects toFill, backup using the datarow as datasource
        /// used for inserts and updates.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="toFill"></param>
        public void Fill<T>(DataRow dr, T toFill, T backup) where T : class, new()
        {
            clsDataTableAdapter<T> adapterFor = null;
            if (!FillMethods.GetValue(typeof(T), out adapterFor))
            {
                adapterFor = new clsDataTableAdapter<T>();
                FillMethods.Add(typeof(T), adapterFor);
            }
            adapterFor.FillObject(dr, toFill);
            adapterFor.FillObject(dr, backup);
        }

        /// <summary>
        /// Fill a backup copy using the same cache as the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="copy"></param>
        public void Fill<T>(T original, T copy) where T : class, new()
        {
            clsDataTableAdapter<T> adapterFor = null;
            if (!FillMethods.GetValue(typeof(T), out adapterFor))
            {
                adapterFor = new clsDataTableAdapter<T>();
                FillMethods.Add(typeof(T), adapterFor);
            }
            adapterFor.FillObject(original, copy);
        }

        /// <summary>
        /// Converts datatable to a dictionary
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetDataDictionary(DataTable dt)
        {
            List<Dictionary<string, object>> objects = new List<Dictionary<string, object>>();
            clsDataTableAdapter<dynamic> adapterFor = new clsDataTableAdapter<dynamic>();
            foreach (DataRow v in dt.Rows)
                objects.Add(adapterFor.FillDictionary(v));

            return objects;
        }

        /// <summary>
        /// Converts the DataTable into a list of objects. (T)
        /// </summary>
        /// <param name="dt">DataTable returned from database</param>
        /// <returns>a new ObservableCollection filled with clsVestiging</returns>
        internal ObservableCollection<T> GetDataList<T>(DataTable dt) where T : class, new()
        {
            ObservableCollection<T> _ListOfT = new ObservableCollection<T>();
            foreach (DataRow row in dt.Rows)
                _ListOfT.Add(Fill<T>(row));
            return _ListOfT;
        }


        /// <summary>
        /// Converts the DataTable into a list of objects. (T)
        /// This includes a backup object.
        /// </summary>
        /// <param name="dt">DataTable returned from database</param>
        /// <returns>a new ObservableCollection filled with clsVestiging</returns>
        internal ObservableCollection<T> GetDataList<T>(DataTable dt, Action<T, T> backup) where T : class, new()
        {
            ObservableCollection<T> _ListOfT = new ObservableCollection<T>();
            foreach (DataRow row in dt.Rows)
            {
                T orig = new T();
                T back = new T();
                Fill<T>(row, orig, back);
                backup(orig, back);
                _ListOfT.Add(orig);
            }
                
            return _ListOfT;
        }
        /// <summary>
        /// Read the parameters of the object T
        /// This will cache all the properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        internal List<Tuple<string, object>> GetParameters<T>(T source) where T : class, new()
        {
            clsDataTableAdapter<T> adapterFor = null;
            if (!FillMethods.GetValue<clsDataTableAdapter<T>>(typeof(T), out adapterFor))
            {
                adapterFor = new clsDataTableAdapter<T>();
                FillMethods.Add(typeof(T), adapterFor);
            }
            return adapterFor.GetParameters(source);
        }
        /// <summary>
        /// Update object T and return the values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toUpdate"></param>
        public void UpdateData<T>(T toUpdate) where T : class, new()
        {
            try
            {
                Fill(DAL.clsCommonMethods.GetDataTable<T>(enSqlCommandType.Update, GetParameters(toUpdate)).Rows[0], toUpdate);
            }catch(IndexOutOfRangeException ex)
            {
                //update niet gelukt
            }

        }

        /// <summary>
        /// Selects the object in the database and updates the properties accordingly.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toUpdate"></param>
        public void SelectUpdate<T>(T toUpdate) where T : class, new()
        {
            Fill(DAL.clsCommonMethods.GetDataTable<T>(enSqlCommandType.Select, GetParameters(toUpdate)).Rows[0], toUpdate);
        }


        /// <summary>
        /// Delete object T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toDelete"></param>
        public void DeleteData<T>(T toDelete) where T : class, new()
        {
            DAL.clsCommonMethods.GetDataTable<T>(enSqlCommandType.Delete, GetParameters(toDelete));
        }
        /// <summary>
        /// Insert object T, fills in id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toInsert"></param>
        public void InsertData<T>(T toInsert) where T : class, new()
        {
            try
            {
                Fill(DAL.clsCommonMethods.GetDataTable<T>(enSqlCommandType.Insert, GetParameters(toInsert)).Rows[0], toInsert);
            }
            catch  { }

        }
        /// <summary>
        /// get object T with ID 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetData<T>(int id) where T : class, new()
        {
            T list = GetDataList<T>(DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.Select, "", id)).FirstOrDefault();
            return list;
        }
       

        /// <summary>
        /// Gets a List of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ObservableCollection<T> GetData<T>() where T : class, new()
        {
            ObservableCollection<T> list = GetDataList<T>(DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.SelectAll));
            return list;
        }

        /// <summary>
        /// Gets a list of T with backups
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fillBackup"></param>
        /// <returns></returns>
        public ObservableCollection<T> GetDataWithBackup<T>(Action<T,T> fillBackup) where T : class, new()
        {
            ObservableCollection<T> list = GetDataList<T>(DAL.clsCommonMethods.GetDataTableCustom<T>(enSqlCommandType.SelectAll), fillBackup);
            return list;
        }


        bool logged = false;
        Stopwatch stopwatch;
        private void LogTime(string method)
        {
            if (!logged)
            {
                stopwatch = Stopwatch.StartNew();
                logged = true;
            }
            else
            {
                stopwatch.Stop();
                Console.WriteLine("Elapsed Time: " + method + ":" + stopwatch.ElapsedMilliseconds.ToString());
                stopwatch.Reset();

            }
        }


    }

    /// <summary>
    /// how to create a generic dictionary.
    /// http://stackoverflow.com/a/654851
    /// </summary>
    public class GenericDicionary
    {
        private Dictionary<Type, object> _GenericDictionary = new Dictionary<Type, object>();
        public void Add<T>(Type key, T value) where T : class
        {
            _GenericDictionary.Add(key, value);
        }

        public bool GetValue<T>(Type key, out T output) where T : class
        {
            object value = null;
            if (_GenericDictionary.TryGetValue(key, out value))
            {
                if (value is T)
                {
                    output = value as T;
                    return true;
                }
            }
            output = null;
            return false;
        }
    }
}
