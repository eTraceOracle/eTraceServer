using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportProductTestDataModel : ModelBase<List<ReportProductTestDataModel.Item>>
    {
        public class Item
        {
            public string ExtSerialNo { get; set; }
            public string ProcessName { get; set; }
            public int SeqNo { get; set; }
            public string TestStep { get; set; }
            public string TestId { get; set; }
            public string TestName { get; set; }
            public string InputCondition { get; set; }
            public DateTime DateTime { get; set; }
            public double LowLimit { get; set; }
            public string TestNameAndInputCondition
            {
                get
                {
                    return TestName + ' ' + InputCondition;
                }
            }
            public double Result { get; set; }
            public double HighLimit { get; set; }
            public string Unit { get; set; }
            public string SystemNo { get; set; }
            public string Status { get; set; }
        }
    }
    public class ReportProductTestSummaryDataModel : ModelBase<List<ReportProductTestSummaryDataModel.Item>>
    {
        public class Item
        {
            public string ExtSerialNo { get; set; }
            public string CartonID { get; set; }
            public string IntSerialNo { get; set; }
            public string ProcessName { get; set; }
            public int SeqNo { get; set; }
            public string PO { get; set; }
            public string Model { get; set; }
            public string PCBA { get; set; }
            public DateTime ProdDate { get; set; }
            public DateTime WIPIn { get; set; }
            public string Result { get; set; }
            public string OperatorName { get; set; }
            public string TesterNo { get; set; }
            public string ProgramName { get; set; }
            public string ProgramRevision { get; set; }
            public string IPSNo { get; set; }
            public string IPSRevision { get; set; }
            public string Remark { get; set; }
        }
    }
    public class ReportProductTestDataQuery: ReportProductTestDataQueryBase
    {

    } 
    public class ReportProductTestDataQueryBase : ModelQueryBase
    {
        public DateTime? ProductTimeStart { get; set; }
        public DateTime? ProductTimeEnd { get; set; }
        public string Model { get; set; }
        public string TestId { get; set; }
        public string ProductSN { get; set; }
        public string Station { get; set; }
        public string TestStep { get; set; }
        public string CartonId { get; set; }
        public bool IsCustomerReport { get; set; }
        public string DJ { get; set; }
    }
}
