using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    
    /// <summary>
    /// Based somewhat on:
    /// https://codeblog.jonskeet.uk/2008/08/09/making-reflection-fly-and-exploring-delegates/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class clsDataTableAdapter<T> where T : class, new()
    {
        private static Func<DataRow, T> FillProps;
        private static Action<DataRow, T> FillExisting;
        private static Action<T, T> FillBackup;
        private static Func<T, List<Tuple<string, object>>> ReadProps;
        private static Dictionary<string, Tuple<Type, Action<T, object>>> propCache = new Dictionary<string, Tuple<Type, Action<T, object>>>();
        private static Dictionary<Func<T,object>, string> propGetCache = new Dictionary<Func<T,object>, string>();
        /// <summary>
        /// Store the rowcount of the retrieved datarow, if this changes the cached method is out of sync
        /// </summary>
        private static int RowCount = 0;

        public Action<T, object> PropertyCall(MethodInfo method)
        {
            MethodInfo genericHelper = typeof(clsDataTableAdapter<T>).GetMethod("ConvertInvoke",
                BindingFlags.Static | BindingFlags.NonPublic);

            MethodInfo constructedHelper = genericHelper.MakeGenericMethod
                (typeof(T), method.GetParameters()[0].ParameterType);

            // Now call it. The null argument is because it’s a static method.
            object ret = constructedHelper.Invoke(null, new object[] { method });

            // Cast the result to the right kind of delegate and return it
            return (Action<T, object>)ret;
        }


        private static Action<TTarget, object> ConvertInvoke<TTarget, TParam>(MethodInfo method)
            where TTarget : class
        {
            // Convert the slow MethodInfo into a fast, strongly typed, open delegate
            Action<TTarget, TParam> func = (Action<TTarget, TParam>)Delegate.CreateDelegate
                (typeof(Action<TTarget, TParam>), method);

            // Now create a more weakly typed delegate which will call the strongly typed one
            Action<TTarget, object> ret = (TTarget target, object param) => func(target, (TParam)param);
            return ret;
        }

        public Func<T, object> CreateGetter(MethodInfo method)
        {
            // First fetch the generic form
            MethodInfo genericHelper = typeof(clsDataTableAdapter<T>).GetMethod("CreateGetterGeneric");

            MethodInfo constructedHelper = genericHelper.MakeGenericMethod
             (typeof(T),method.ReturnType);//, method.ReturnType);

            // Now call it. The null argument is because it’s a static method.
            object ret = constructedHelper.Invoke(null, new object[] { method });

            // Cast the result to the right kind of delegate and return it
            return (Func<T, object>)ret;
        }

        public static Func<TTarget,object> CreateGetterGeneric<TTarget, TReturn>(MethodInfo method)
        {
            // Convert the slow MethodInfo into a fast, strongly typed, open delegate
            Func<TTarget,TReturn> func = (Func<TTarget,TReturn>)Delegate.CreateDelegate
                (typeof(Func<TTarget,TReturn>), method);

            // Now create a more weakly typed delegate which will call the strongly typed one
            Func<TTarget, object> ret = (TTarget target) => func(target);
            return ret;
        }

        //try again
        /// <summary>
        /// Gets Properties in the object source
        /// Converts the properties to a list of string|object
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public List<Tuple<string,object>> GetParameters(T source)
        {
            if(ReadProps == null)
            {
                ReadProps = new Func<T, List<Tuple<string, object>>>((input) => {
                    List<Tuple<string, object>> outputList = new List<Tuple<string, object>>();
                    if (propGetCache.Count < 1)
                    {
                        //get all properties with primitive types, datetime, array of bytes and string
                        foreach (PropertyInfo info in typeof(T).GetProperties())
                        {
                            if (info.PropertyType.IsPrimitive || info.PropertyType == typeof(DateTime) || info.PropertyType == typeof(byte[]) || info.PropertyType == typeof(string) || info.PropertyType == typeof(object) || info.PropertyType == typeof(bool?))
                            {
                                string name = info.Name;
                                Console.WriteLine(name);
                                if (name.Equals("Error"))
                                    continue;
                                if (name.Equals("Item"))
                                    continue;
                                if (name.Equals("IsAanwezig"))
                                    Console.WriteLine("Datum");
                                if (info.PropertyType == typeof(DateTime))
                                    Console.WriteLine("Datum");
                                if (info.PropertyType == typeof(byte[]))
                                    Console.WriteLine("byte");
                                if (info.PropertyType == typeof(bool?))
                                    Console.WriteLine("NULLABLE bool");
                                propGetCache.Add(CreateGetter(info.GetGetMethod()), name);
                            }
                        }
                    }
                    //fout was dat op de plaats waar input nu staat source stond. en die veranderde naar de objectverwijzing van source van de eerste keer dat het object 
                    //geset was.
                    propGetCache.ToList().ForEach(x => outputList.Add(new Tuple<string, object>(x.Value, x.Key(input))));

                    return outputList;
                });
            }
            return ReadProps(source);
        }

        /// <summary>
        /// experiment
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public dynamic FillDynamicObject(DataRow dr)
        {
            dynamic d = new ExpandoObject();
            foreach (DataColumn column in dr.Table.Columns)
            {
                try
                {
                    string name = column.ColumnName;
                    //http://stackoverflow.com/a/5611096 expando object as idictionary
                    var p = d as IDictionary<String, object>;
                    p[name] = dr[name];
                }
                catch (Exception e) { Console.WriteLine("could not fill dynamic object\r\n" + e.Message); };
            }
            return d;
        }

        /// <summary>
        /// return dictionary from datatable
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public Dictionary<string,object> FillDictionary(DataRow dr)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                try
                {
                    string name = column.ColumnName;
                    d.Add(name, dr[name]);
                }
                catch (Exception e) { Console.WriteLine("could not fill dictionary\r\n" + e.Message); };
            }
            return d;
        }


        public T FillObject(DataRow dr)
        {
            if(FillProps == null || dr.Table.Columns.Count != RowCount)
            {
                if(RowCount != 0 && dr.Table.Columns.Count != RowCount)
                {
                    Console.WriteLine("Aantal kolommen komen niet overeen, " + RowCount + "<>" + dr.Table.Columns.Count);
                }
                FillProps = new Func<DataRow, T>((datarow) =>
                {
                    T toFill = new T();
                    RowCount = datarow.Table.Columns.Count;
                    foreach (DataColumn column in dr.Table.Columns)
                    {
                        try
                        {
                            string name = column.ColumnName;
                            if (!propCache.ContainsKey(name))
                            {
                                //PropertyInfo prop = typeof(T).GetProperty(name, BindingFlags.Public | BindingFlags.IgnoreCase);
                                PropertyInfo prop = typeof(T).GetProperties().Where(x => x.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
                                Type propertyType = prop.PropertyType;
                                propCache.Add(name, new Tuple<Type, Action<T, object>>(propertyType, PropertyCall(prop.SetMethod)));
                            }
                            if (name.Equals("IsAanwezig") && !datarow[name].Equals(null) && !DBNull.Value.Equals(datarow[name]))
                                Console.WriteLine("isaanwezig");
                            if (name.Contains("Email") && !datarow[name].Equals(null) && !DBNull.Value.Equals(datarow[name]))
                                Console.WriteLine("isaanwezig");
                            if (!datarow[name].Equals(null) && !DBNull.Value.Equals(datarow[name])) { 
                                Tuple<Type, Action<T, object>> value = propCache[name];
                                //error kan zijn door nullable type, check again: http://stackoverflow.com/questions/3531318/convert-changetype-fails-on-nullable-types
                                value.Item2(toFill, ConvertToType(datarow[name], Nullable.GetUnderlyingType(value.Item1) ?? value.Item1));
                            }
                        }
                        catch(Exception e) { Console.WriteLine("could not fill property\r\n"+e.Message); };
                    }
                    return toFill;
                });
                return FillProps(dr);
            }
            else
            {
                return FillProps(dr);
            }
        }
        public void FillObject(DataRow dr, T toFill)
        {
            if (FillExisting == null || RowCount != dr.Table.Columns.Count)
            {
                FillExisting = new Action<DataRow, T>((datarow, existing) =>
                {
                    RowCount = datarow.Table.Columns.Count;
                    foreach (DataColumn column in dr.Table.Columns)
                    {
                        try
                        {
                            string name = column.ColumnName;
                            if (!propCache.ContainsKey(name))
                            {
                                //case insensitive
                                PropertyInfo prop = typeof(T).GetProperties().Where(x => x.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
                                Type propertyType = prop.PropertyType;
                                propCache.Add(name, new Tuple<Type, Action<T, object>>(propertyType, PropertyCall(prop.GetSetMethod())));
                            }
                            if (name.Equals("IsAanwezig") && !datarow[name].Equals(null) && !DBNull.Value.Equals(datarow[name]))
                                Console.WriteLine("isaanwezig");
                            Tuple<Type, Action<T, object>> value = propCache[name];
                            value.Item2(existing, ConvertToType(datarow[name], Nullable.GetUnderlyingType(value.Item1) ?? value.Item1));
                        }
                        catch(Exception e)
                        {
                            string a = e.Message;
                            Console.WriteLine("could not fill property");
                        };
                    }
                    
                });
                
            }
            FillExisting(dr, toFill);
            
        }


        /// <summary>
        /// Fill backup of object
        /// </summary>
        /// <param name="original"></param>
        /// <param name="toFill"></param>
        public void FillObject(T original, T toFill)
        {
            if (FillBackup == null)
            {
                FillBackup = new Action<T, T>((kopij, backup) =>
                {
                    Type type = kopij.GetType();
                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo info in properties)
                    {
                        try
                        {
                            string name = info.Name;
                            if (!propCache.ContainsKey(name))
                            {
                                PropertyInfo prop = typeof(T).GetProperty(name);
                                Type propertyType = prop.PropertyType;
                                propCache.Add(name, new Tuple<Type, Action<T, object>>(propertyType, PropertyCall(prop.GetSetMethod())));
                            }
                            Tuple<Type, Action<T, object>> value = propCache[name];
                            value.Item2(backup, info.GetValue(kopij,null));
                        }
                        catch (Exception e)
                        {
                            string a = e.Message;
                            Console.WriteLine("could not fill property");
                        };
                    }

                });

            }
            FillBackup(original, toFill);

        }

        /// <summary>
        /// Convert Database types to c# types
        /// 
        /// </summary>
        /// <param name="source">object from database</param>
        /// <param name="target">Requested type</param>
        /// <returns></returns>
        public object ConvertToType(object source, Type target)
        {
            object output = null;
            if (source != null && !source.Equals(DBNull.Value))
            {
                
                    output = target != typeof(object) ? Convert.ChangeType(source, target) : source;
                
                    
                    

            }
                
            return output;
        }

    }
}
