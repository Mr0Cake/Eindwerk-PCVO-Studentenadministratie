using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extensions
{
    public static class clsDataTableExtensions {
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> ToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>(table.Rows.Count);

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties().Where(p => p.CanWrite))
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Add array of T to observablecollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obs"></param>
        /// <param name="array"></param>
        public static void AddRange<T>(this ObservableCollection<T> obs, T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                obs.Add(array[i]);
        }

        /// <summary>
        /// Convert List<T> to ObservableCollection<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            ObservableCollection<T> obs = new ObservableCollection<T>();
            foreach (var v in list)
                obs.Add(v);
            return obs;
        }

        /// <summary>
        /// Convert DataTable to Observable collection using reflection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this DataTable table) where T : class, new()
        {
            try
            {
                ObservableCollection<T> list = new ObservableCollection<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties().Where(p => p.CanWrite))
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Similar to Convert.toInt32(dr[parameter]) but with DBNull check
        /// -->
        /// int ID = dr.ConvertTo<int>(parameter) if you know it cannot be null (when there is an error it still returns default)
        /// int Count = dr.ConvertTo<int>(parameter, 0) returns 0 on error or on DBNull
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="parameter"></param>
        /// <param name="ifNull"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this DataRow dr, string parameter, T ifNull = default(T))
        {
            try
            {
                object output = dr[parameter];
                if (!output.Equals(DBNull.Value))
                {
                    return (T)Convert.ChangeType(output, typeof(T));
                }
            }
            catch { Console.WriteLine("Could not convert " + typeof(T).ToString()); }

            return ifNull;
        }

        //public static object ConvertTo(this object objStart, Type Target)
        //{
        //    try
        //    {
        //        if (!objStart.Equals(DBNull.Value))
        //        {
        //            return Convert.ChangeType(objStart, Target);
        //        }
        //    }
        //    catch { Console.WriteLine("Could not convert " + typeof(T).ToString()); }

        //    return ifNull;
        //}

        //public static void FillCommon(this StudentApplication.Model.clsBaseModel toFill, DataRow dr)
        //{
        //    toFill.CreatedBy = dr.ConvertTo("CreatedBy", "");
        //    toFill.CreatedOn = dr.ConvertTo("CreatedOn", DateTime.MinValue);
        //    toFill.ModifiedOn = dr.ConvertTo("ModifiedOn", DateTime.MinValue);
        //    toFill.ControlField = dr.ConvertTo<object>("ControlField");
        //    toFill.IsDeleted = dr.ConvertTo<bool>("IsDeleted");
        //}

    }
}
