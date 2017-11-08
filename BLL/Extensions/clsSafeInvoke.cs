using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extensions
{
    /// <summary>
    /// Extension methods to copy ?. null-propagating operator
    /// </summary>
    public static class clsSafeInvoke
    {
        //http://stackoverflow.com/questions/872323/method-call-if-not-null-in-c-sharp

        public static void NN<T>(this T obj, Action<T> action)
        {
            if (obj != null)
            {
                action(obj);
            }
        }

        public static TR NNR<T, TR>(this T obj, Func<T, TR> func) where T : class
        {
            if(obj != null)
            {
                return func(obj);
            }else
            {
                return default(TR);
            }
        }
    }
}
