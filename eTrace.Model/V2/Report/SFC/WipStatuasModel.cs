using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
     public class WipStatusDetailModel : ModelBase<List<WipStatusDetailItem>>
    {

    }
    public class WipStatusSummaryModel : ModelBase<List<WipStatusSummaryItem>>
    {
        /// <summary>
        /// Summary Count
        /// </summary>
        public long SummaryCount { get; set; }

    }
    /// <summary>
    /// Data model of each item
    /// one field of report corresponds to one proerty
    /// </summary>
    public class WipStatusDetailItem
    {
        public string InvOrg { get; set; }
        public string IntSN { get; set; }
        public string Model { get; set; }
        public string PCBA { get; set; }
        public string DJ { get; set; }
        public string JobID { get; set; }
        public string TVA { get; set; }
        public string ProdLine { get; set; }
        public string CurrentProcess { get; set; }
        public string Result { get; set; }
        public string MBPCBA { get; set; }
        public string MBIntSN { get; set; }
        public string MotherBoardSN { get; set; }
        public string PanelID { get; set; }
        public string LoadedTo { get; set; }
        public DateTime? ChangedOn { get; set; }
        public string ChangedBy { get; set; }
    }
    public class WipStatusSummaryItem
    {
        public string Model { get; set; }
        public string PCBA { get; set; }
        public string DJ { get; set; }
        public string JobID { get; set; }
        public string TVA { get; set; }
        public string ProdLine { get; set; }
        public string CurrentProcess { get; set; }
        public string Result { get; set; }
        public string MBPCBA { get; set; } 
        public int Count { get; set; }
    }

    public class GetWipStatusQuery : ModelQueryBase<GetWipStatusQuery.Item>
    {
        public class Item
        {

            public String WIPID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ORG { get; set; }
            /// <summary>
            /// Station
            /// </summary>
            public string Station { get; set; }
            /// <summary>
            ///
            /// </summary>
            public string Model { get; set; }
            /// <summary>
            /// 模块
            /// </summary>
            public string PCBA { get; set; }
            /// <summary>
            /// 产品型号
            /// </summary>
            public string DiscreteJob { get; set; }
            /// <summary>
            /// 板型号
            /// </summary>
            public string JobID { get; set; }
            public string MotherBoardSN { get; set; }
            public string IntSN { get; set; }
            public string Floor { get; set; }
            public string PanelID { get; set; }
            public string TVA { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public  bool IsGetDataByMotherBoradSN { get; set; }
        }
    }
}
