using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL;


namespace SQLConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string value = "";
            do
            {
                value = "";
                Console.Write("Enter query: ");
                string Query = Console.ReadLine();
                BLL.clsCommonBLL BLL = new clsCommonBLL();
                List<Dictionary<string, object>> output = BLL.GetDataDictionary(clsCommonMethods.GetDataTableParams(Query));
                StringBuilder sb = new StringBuilder();
                if (output.FirstOrDefault() != null)
                {
                    foreach (var item in output.First())
                    {
                        if(item.Value is int || item.Value is DateTime || item.Value is bool)
                        {
                            sb.Append(item.Key.PadLeft(10));
                        }
                        else
                        {
                            sb.Append(item.Key.PadLeft(30));
                        }
                        
                    }
                    sb.AppendLine();
                    int count = 0;
                    foreach (var v in output)
                    {
                        if (count++ < 5)
                        {
                            sb.Append(format(v));
                        }else
                        {
                            sb.AppendLine();
                            Console.WriteLine(sb.ToString());
                            sb.Clear();
                            System.Threading.Thread.Sleep(2000);
                            count = 0;
                        }
                    }

                    sb.AppendLine();
                }
                Console.WriteLine(sb.ToString());
                Console.Write("New Query? (Y/N): ");
                value = Console.ReadLine().ToUpper();
            } while (value == "Y");
        }

        private static string format(Dictionary<string, object> input)
        {
            StringBuilder sb = new StringBuilder();
            
            if(input != null)
            {
                if(input.Count > 0)
                {
                    foreach(var v in input)
                    {
                        try
                        {
                            if(v.Value is int || v.Value is DateTime || v.Value is bool)
                            {
                                sb.Append(v.Value.ToString().PadLeft(10));
                            }
                            else { sb.Append(v.Value.ToString().PadLeft(30)); }
                            
                        }
                        catch { }
                    }
                }
            }

            return sb.AppendLine().ToString();
        }
    }
}
