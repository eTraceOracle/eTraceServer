using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportSMFixtureInspHeaderModel : ModelBase<List<ReportSMFixtureInspHeaderModel.Item>>
    {
        public class Item
        {

            public string InspID{ get; set; }
            public string InspType{ get; set; }
            public string FixtureID{ get; set; }
            public string ItemNo{ get; set; }
            public string InspSpecID{ get; set; }
            public DateTime InspectedOn { get; set; }
            public string InspectedBy{ get; set; }
            public string InspResult{ get; set; }
            public string BatchID{ get; set; }
            public string Store{ get; set; }
            public DateTime ChangedOn{ get; set; }
            public string ChangedBy{ get; set; }
            public string Remarks{ get; set; }
        }

            //public class Header
            //{
            //    public string InspID { get; set; }
            //    public string InspItem { get; set; }
            //    public string InspCondition { get; set; }
            //    public string LowerLimit { get; set; }
            //    public string UpperLimit { get; set; }
            //    public string InspValue { get; set; }
            //    public string Unit { get; set; }
            //    public string InspResult { get; set; }
            //    public string Remarks { get; set; }
            //}

    }

    public class ReportSMFixtureInspItemModel : ModelBase<List<ReportSMFixtureInspItemModel.Item>>
    {
        public class Item
        {
            public string InspID { get; set; }
            public string InspItem { get; set; }
            public string InspCondition { get; set; }
            public string LowerLimit { get; set; }
            public string UpperLimit { get; set; }
            public string InspValue { get; set; }
            public string Unit { get; set; }
            public string InspResult { get; set; }
            public string Remarks { get; set; }
        }

    }

    public class ReportSMFixtureInspHeaderModelQuery : ModelQueryBase
    {
        public string InspID { get; set; }
        public string InspType { get; set; }
        public string InspSpecID { get; set; }
        public string FixtureID { get; set; }
        public string ItemNO { get; set; }
        public string BatchID { get; set; }
        public DateTime InspectedFrom { get; set; }
        public DateTime InspectedTo { get; set; }
        public string Store { get; set; }
        public string Owner { get; set; }
    }
}
