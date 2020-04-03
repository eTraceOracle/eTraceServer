using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Common
{
    public static class ModelCopyExtends
    {
        public static void CopyModelValueFrom<T1, T2>(this T1 targetModel, T2 sourceModel)
            where T1 : class
            where T2 : class
        {
            Type t1 = targetModel.GetType();   
            Type t2 = sourceModel.GetType();
            PropertyInfo[] property2 = t2.GetProperties();
            foreach (var mi in property2)
            {
                BindingFlags flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;
                var des = t1.GetProperty(mi.Name, flag);
                if (des != null)
                {
                    try
                    {
                        des.SetValue(targetModel, mi.GetValue(sourceModel, null), null);
                    }
                    catch
                    { }
                }
            } 
        }
        public static void CopyModelValue(this object model1, object model2) 
        {
            Type t1 = model1.GetType();
            Type t2 = model2.GetType();
            PropertyInfo[] property2 = t2.GetProperties();
            foreach (var mi in property2)
            {
                BindingFlags flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;
                var des = t1.GetProperty(mi.Name, flag);
                if (des != null)
                {
                    try
                    {
                        des.SetValue(model1, mi.GetValue(model2, null), null);
                    }
                    catch
                    { }
                }
            }
        }
    }
}
