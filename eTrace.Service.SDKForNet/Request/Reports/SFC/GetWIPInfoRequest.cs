using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports.SFC
{
   public  class GetWIPInfoRequest : ServerRequestBase<GetWIPInfoRequestItem>
    {
    
    }
    public class GetWIPInfoRequestItem
    {
        /// <summary>
        /// 内部序列号
        /// </summary>
        public string IntSN { get; set; } 
    }
}