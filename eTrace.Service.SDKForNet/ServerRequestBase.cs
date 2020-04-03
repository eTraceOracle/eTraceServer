using eTrace.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet
{


    /// <summary>
    /// 由于已被多处应用
    /// 增加基类移除pager
    ///无需分页请求基类直接从这里继承
    /// </summary>
    public abstract class RequestBase
    {
        /// <summary>
        /// 客户端令牌--IT管控
        /// </summary>
        [JsonProperty(Order = 1)]
        public string ClientToken { get; set; }

        [JsonProperty(Order = 2)]
        public string Version { get; set; }

        [JsonProperty(Order = 3)]
        public DateTime ClientTime { get; set; }

        [JsonProperty(Order = 4)]
        public EmETraceType eTraceType { get; set; }

        /// <summary>
        /// EN/CN
        /// </summary>
        [JsonProperty(Order = 5)]
        public EmLanguageType LanguageType { get; set; }


        [JsonIgnore]
        public object DataObj { get; set; }
    }
    public abstract class ServerRequestBase:RequestBase
    {
        public RequestPager Pager { get; set; }
    }


    public abstract class ReportRequsetBase: ServerRequestBase
    {

    }
    public abstract class ServerRequestBase<T> : ServerRequestBase
    {
        [JsonProperty(Order = 7)]
        [JsonIgnore]
        private T _data;
        public T Data
        {
            get { return _data; }
            set
            {
                _data = value;
                DataObj = value;
            }
        }
    }
    public  class RequestBase<T> : RequestBase
    {
        [JsonProperty(Order = 7)]
        [JsonIgnore]
        private T _data;
        public T Data
        {
            get { return _data; }
            set
            {
                _data = value;
                DataObj = value;
            }
        }
    }

    public class ServerRequest : ServerRequestBase
    {

    }

    public class ServerRequest<T> : ServerRequestBase<T>
    {

    }

    public class RequestPager
    {
        [JsonProperty(Order = 10)]
        public int CurrentPage { get; set; }
        [JsonProperty(Order = 11)]
        public int PageSize { get; set; }
        [JsonProperty(Order = 12)]
        public string Order { get; set; }
    }

    //public class RequestOrder
    //{
    //    public RequestOrder()
    //    {
    //        OrderType = EmOrderType.Asc;
    //    }

    //    public string ColumnName { get; set; }
    //    public EmOrderType OrderType { get; set; }
    //}



    public static class Extends
    {
        public static DateTime? GetDateTime(this string value)
        {
            return GetDateTime(value, string.Empty);
        }
        public static DateTime? GetDateTime(this string value, string formarter)
        {
            DateTime? result = null;
            DateTime valueTmp = DateTime.Now;
            if (!string.IsNullOrEmpty(value))
            {
                if (DateTime.TryParse(value, out valueTmp))
                {
                    result = valueTmp;
                }
                if (!string.IsNullOrEmpty(formarter))
                {
                    result = DateTime.Parse(valueTmp.ToString(formarter));
                }
            }

            return result;
        }
    }
}
