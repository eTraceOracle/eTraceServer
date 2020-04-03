using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    
    public class GetWipLockDataResultModel : ModelBase<List<GetWipLockDataResultModel.Item>>
    {
        public class Item
        {
            public string Model { get; set; }
            public string PCBA { get; set; }
            public string ProdLine { get; set; }
            public string Process { get; set; }
            public string FailedTestStep { get; set; }
            public string LockType { get; set; }
            public string Details { get; set; }
            public string Status { get; set; }
            public DateTime LockedOn { get; set; }
            public DateTime UnlockedOn { get; set; }
            public string UnlockedBy { get; set; }
            public string CauseCode { get; set; }
            public string FA_TE { get; set; }
            public string FA_PE { get; set; }
            public string FA_PQE { get; set; }
            public string FA_Others { get; set; }
            public string PBR { get; set; }
            public string Remarks { get; set; }
            public string RepairerID { get; set; }
            public string RepairDate { get; set; }
            public DateTime? RepairTime { get; set; }
            public string DefectCode { get; set; }
            public string Cause { get; set; }
            public string CompRefD { get; set; }
            public string CompPN { get; set; }
        }
    } 
    
    public class GetWipLockDataModelQuery : ModelQueryBase<GetWipLockDataModelQuery.Item>
    {

        public class Item
        {
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime? StartTime { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime? EndTime { get; set; }
            /// <summary>
            /// 产线
            /// </summary>
            public string ProdLine { get; set; }
            /// <summary>
            /// 工序
            /// </summary>
            public string Process { get; set; }
            /// <summary>
            /// 产品型号
            /// </summary>
            public string Model { get; set; }
            /// <summary>
            /// 板型号
            /// </summary>
            public string PCBA { get; set; }
        }
    }
}
