using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model
{
    public abstract class ModelQueryBase
    {
        public QueryPager Pager { get; set; }
    }

    /// <summary>
    /// 可变data类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
     public class ModelQueryBase<T> : ModelQueryBase where T : new()
    {
        public ModelQueryBase()
        {
            if (typeof(T).Name.ToUpper().StartsWith("LIST"))
            {
                Data = new T();
            }
        }
        /// <summary>
        /// item Data of Query
        /// </summary>
        public T Data { get; set; }
    }

    public class QueryPager
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string Order { get; set; } 
    }



    //public class QueryOrder
    //{
    //    public QueryOrder()
    //    {
    //        OrderType = EmOrderType.Asc;
    //    }
    //    public string ColumnName { get; set; }
    //    public EmOrderType OrderType { get; set; }
    //}
}
