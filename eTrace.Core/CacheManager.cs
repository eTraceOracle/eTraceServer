using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Core
{
    public class CacheManager
    {
        public static CacheManager instance = new CacheManager();
        public static CacheManager Instance
        {
            get { return instance; }
        }

        public CacheManager()
        {

        }

        public void Inited()
        {
            dicCache.Clear();
        }

        private static Dictionary<string, object> dicCache = new Dictionary<string, object>();

        public void SetData(string key, object value)
        {
            dicCache[key] = value;
        }

        public T GetData<T>(string key)
        {
            if (dicCache.ContainsKey(key))
            {
                return (T)dicCache[key];
            }
            return default(T);
        }
    }
}

