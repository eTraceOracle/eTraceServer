using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportAutoeJITPlanModel : ModelBase<List<ReportAutoeJITPlanModel.Item>>
    {
        public class Item
        {
            public string OrgCode { get; set; }
            public string ITEM_NO { get; set; }
            public string SUBINV { get; set; }
            public string LOCATOR { get; set; }
            public decimal EJIT_REQ_QTY { get; set; }
            public decimal ORDER_QTY { get; set; }
            public string EJIT_TRIGGER_TYPE { get; set; }
            public string DESCRIPTION { get; set; }
            public string NEED_BY_DATE { get; set; }
            public string Creation_date { get; set; }
            public string Plan_Arrival_date { get; set; }
            public string GAP { get; set; }
            public string PRIMARY_UNIT_OF_MEASURE { get; set; }
            public string ITEM_TYPE { get; set; }
            public string FREQUENCY { get; set; }
            public string LEAD_TIME { get; set; }
            public string MPQ { get; set; }
            public string VENDOR_NAME { get; set; }
        }
    }

    

    public class ReportAutoeJITPlanModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string Locator { get; set; }
        public DateTime NeedFrom { get; set; }
        public DateTime NeedTo { get; set; }
        public DateTime PlanFrom { get; set; }
        public DateTime PlanTo { get; set; }
        public DateTime CreationFrom { get; set; }
        public DateTime CreationTo { get; set; }
    }
}
