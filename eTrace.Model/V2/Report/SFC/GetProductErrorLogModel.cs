using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
     public class GetProductErrorLogModel : ModelBase<List<GetProductErrorLogModel.MissMatchItem>>
    {
        public class Item
        {
            public DateTime DateTime { get; set; }
            public string ErrorMsg { get; set; }
            public string Module { get; set; }
            public string UserName { get; set; }
            public string Remarks { get; set; } 
        }

        public class MissMatchItem
        {
            public string Module { get; set; }
            public DateTime? DateTime { get; set; }
            public string UserName { get; set; }
            public string Model { get; set; }
            public string IntSN { get; set; }
            public string Eeprom { get; set; }
            public string Label { get; set; }
            public string Line { get; set; }
        }
    }
    
    public class GetProductErrorLogQuery : ModelQueryBase<GetProductErrorLogQuery.Item>
    {
        public class Item
        {
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime? ErrorDateFrom { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime? ErrorDateTo { get; set; }
            /// <summary>
            /// 工单
            /// </summary>
            public string DiscreteJob { get; set; }
            /// <summary>
            /// 模块
            /// </summary>
            public string Module { get; set; }
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
