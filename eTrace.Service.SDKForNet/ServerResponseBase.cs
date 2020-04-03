using eTrace.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet
{
    public abstract class ServerResponseBase
    {
        [JsonProperty(Order = 1)]
        public EmLanguageType LanguageType { get; set; }
        [JsonProperty(Order = 2)]
        public string ServerVersion { get; set; }
        public ServerResponseBase()
        {
            BussinesCode = EmBussinesCodeType.Success;
        }

        [JsonProperty(Order = 3)]
        public EmBussinesCodeType BussinesCode { get; set; }

        [JsonProperty(Order = 4)]
        public string ErrorMsg
        {
            get
            {
                string cMsg = BussinesCodeManager.Instance.GetMessage(LanguageType, BussinesCode);
                if (string.IsNullOrEmpty(GodMsg))
                {
                    return cMsg;
                }
                else
                {
                    string godMsg = string.Format("Exection error:{0}", GodMsg);
                    if (string.IsNullOrEmpty(cMsg))
                    {
                        return godMsg;
                    }
                    else
                    {
                        return string.Format("{0}-{1}", cMsg, godMsg);
                    }
                }
            }
        }

        [JsonIgnore]
        public string GodMsg { get; set; }
        [JsonProperty(Order = 5)]
        public DateTime RequestTime { get; set; }
        [JsonProperty(Order = 6)]
        public DateTime ReponseTime { get; set; }

        [JsonProperty(Order = 7)]
        public double ResponseTotalSecond { get { return (ReponseTime - RequestTime).TotalSeconds; } }

        [JsonProperty(Order = 8)]
        public DateTime RequestServerTime { get; set; }
        [JsonProperty(Order = 9)]
        public DateTime ReponseServerTime { get; set; }

        [JsonProperty(Order = 10)]
        public double ResponseServerTotalSecond { get { return (ReponseServerTime - RequestServerTime).TotalSeconds; } }


        [JsonProperty(Order = 11)]
        public ResponsePager Pager { get; set; }

        private Stopwatch stopWatch;

        public void Start()
        {
            if (stopWatch == null)
                stopWatch = new Stopwatch();

            stopWatch.Start();
        }
        public void Stop()
        {
            if (stopWatch != null)
                stopWatch.Stop();
        }

    }
    public   class ServerResponseBase<T> : ServerResponseBase where T : new()
    {
        public ServerResponseBase()
        {
            Data = new T();
        }
        [JsonProperty(Order = 12)]
        public T Data { get; set; }
    }

    public class ServerResponse : ServerResponseBase
    {

    }

    public class ServerResponse<T> : ServerResponseBase<T> where T : new()
    {

    }

    public class ResponsePager
    {
        public long TotalCount { get; set; }
    }
}
