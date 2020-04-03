using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model
{
    public abstract class ModelBase
    {
        public ModelPager Pager { get; set; }

        public bool IsOverMaxRow { get; set; }
    }
    public  class ModelBase<T> : ModelBase where T : new()
    {
        public ModelBase()
        {
            if (typeof(T).Name.ToUpper().StartsWith("LIST"))
            {
                   Data = new T();
            }
        }

        public T Data { get; set; }

    }

    public class ModelPager
    {
        public long TotalCount { get; set; }
    }
}