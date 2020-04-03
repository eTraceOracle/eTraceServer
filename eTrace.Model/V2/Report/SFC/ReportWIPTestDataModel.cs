using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{

    public class ReportWIPTDHeaderModel : ModelBase<List<ReportWIPTDHeaderModel.Item>>
    {
        public class Item
        {
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
            public int TestCycleTime { get; set; }
            public string MBPCBA { get; set; }
            public string MBIntSN { get; set; }
            public string CurIntSN { get; set; }
        }
    }
    public class ReportWIPTestDataModel : ModelBase<List<ReportWIPTestDataModel.Item>>
    {
        public class Item
        {
            public string IntSerialNo { get; set; }
            public string ProcessName { get; set; }
            public int SeqNo { get; set; }
            public string TestStep { get; set; }
            public string Model { get; set; }
            public string PCBA { get; set; }
            public DateTime ProdDate { get; set; }
            public string TesterNo { get; set; }
            public string TestName { get; set; }
            public string ProgramName { get; set; }
            public string ProgramRevision { get; set; }
            public string InputCondition { get; set; }
            public string OutputLoading { get; set; }
            public string OutputName { get; set; }
            public string TestID { get; set; }
            public string OperatorName { get; set; }
            public string IPSReference { get; set; }
            public double LowerLimit { get; set; }
            public double Result { get; set; }
            public double UpperLimit { get; set; }
            public string Unit { get; set; }
            public string Status { get; set; }
        }
    }

    public class ReportWIPPropertiesModel : ModelBase<List<ReportWIPPropertiesModel.Item>>
    {
        public class Item
        {
            public string IntSerialNo { get; set; }
            public int SeqNo { get; set; }
            public string ProcessName { get; set; }
            public string Model { get; set; }
            public string PCBA { get; set; }
            public string PropertyType { get; set; }
            public string PropertyName { get; set; }
            public string InputType { get; set; }
            public string PropertyValue { get; set; }
            public string MotherBoard { get; set; }
            public string MotherBoardSN { get; set; }
            public string ChangedBy { get; set; }
            public DateTime ChangedOn { get; set; }
        }
    }
    public class ReportWIPTestSummaryDataModel : ModelBase<List<ReportWIPTestSummaryDataModel.Item>>
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
    public class ReportWIPTestDataQuery: ReportWIPTestDataQueryBase
    {

    } 
    public class ReportWIPTestDataQueryBase : ModelQueryBase
    {
        public DateTime? ProductTimeStart { get; set; }
        public DateTime? ProductTimeEnd { get; set; }
        public string Model { get; set; }
        public string IntSN { get; set; }
        public string Station { get; set; }
        public bool IsCustomerReport { get; set; }
    }
}
