using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
 

    /// <summary>
    /// GetSMTAOIData接口返回数据实体
    /// </summary>
    public class GetProductionErrorLogMisMatchResponse : ServerResponseBase<List<GetProductErrorLogResponseItem>>
    {
    }

    /// <summary>
    /// GetSMTAOIData接口返回数据实体数据项
    /// </summary>
    public class GetProductErrorLogResponseItem: eTrace.Model.V2.Report.GetProductErrorLogModel.MissMatchItem
    {
        //public string Module { get; set; }
        //public DateTime? DateTime { get; set; }
        //public int UserName { get; set; }
        //public string Model { get; set; }
        //public string IntSN { get; set; }
        //public string Eeprom { get; set; }
        //public string Label { get; set; }
        //public string Line { get; set; } 

    }
}
