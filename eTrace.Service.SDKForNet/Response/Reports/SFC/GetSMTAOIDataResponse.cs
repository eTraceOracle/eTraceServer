using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports.SFC
{
    /// <summary>
    /// GetSMTAOIData接口返回数据实体
    /// </summary>
    public class GetSMTAOIDataResponse : ServerResponseBase<List<GetSMTAOIDataResponseItem>>
    {
    }

    /// <summary>
    /// GetSMTAOIData接口返回数据实体数据项
    /// </summary>
    public class GetSMTAOIDataResponseItem
    {
        public string ProgramName { get; set; }
        public string PanelID { get; set; }
        public int BoardIndex     { get; set; }
        public string AOIBarcode { get; set; }
        public string IntSN { get; set; }
        public string TopBtm { get; set; }
        public int TestCount { get; set; }
        public string EquipmentID { get; set; }
        public DateTime ProdDate { get; set; }
        public string OperatorName { get; set; }
        public string Result { get; set; }
        public int FailComp { get; set; }
        public int Checked { get; set; }
        public string Remark { get; set; }

    }
}
