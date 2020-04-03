using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportWIPProductModel : ModelBase<List<ReportWIPProductModel.Item>>
    {
        public class Item
        {
            public int SeqNo { get; set; }
            public string IntSN { get; set; }
            public string PCBA { get; set; }
            public string DJ { get; set; }
            public string ProdLine { get; set; }
            public string Process { get; set; }
            public string Status { get; set; }
            public string LastResult { get; set; }
            public int TestRound { get; set; }
            public int FailedTest { get; set; }
            public int MaxTestRound { get; set; }
            public int MaxFailure { get; set; }
        }
    }

    public class WIPUnitFlowQuery : ModelQueryBase
    {
        public string IntSN { get; set; }
    }
    #region GetWipInfo

    /// <summary>
    /// Contain All Wip data in this Model
    /// </summary>
    public class WipInfo
    {
        public  WIPHeaderModel WIPHeader { get; set; }
        public List<WIPFlowModel> WIPFlowList { get; set; }
        public List<WIPPropertyModel> WIPPropertyList { get; set; }
        public List<WIPTestDataModel> WIPTestDataList { get; set; }
    }

    public class WIPHeaderModel
    {
        public string WIPID { get; set; }
        public string IntSN { get; set; }
        public string Model { get; set; }
        public string PCBA { get; set; }
        public string DJ { get; set; }
        public string InvOrg { get; set; }
        public string CurrentProcess { get; set; }
        public string Result { get; set; }
        public string MotherBoardSN { get; set; } 
        public string JobID { get; set; }
        public DateTime ChangedOn { get; set; }
        public string ChangedBy { get; set; }
    }
    public class WIPFlowModel
    { 
        public int SeqNo { get; set; }
        public string Process { get; set; }
        public string Status { get; set; }
        public int TestRound { get; set; }
        public int FailedTest { get; set; }
        public string LastResult { get; set; }
        public int  MaxTestRound { get; set; }
        public int MaxFailure { get; set; }
    }

    public class WIPPropertyModel
    {
        public int SeqNo { get; set; }
        public string PropertyType { get; set; }
        public string PropertyName { get; set; }
        public string InputType { get; set; }
        public string PropertyValue { get; set; }
        public DateTime ChangedOn { get; set; }
        public string ChangedBy { get; set; }
    }
    public class WIPTestDataModel
    {
        public string ProcessName { get; set; }
        public int SeqNo { get; set; }
        public DateTime ProdDate { get; set; }
        public string Result { get; set; }
        public string OperatorName { get; set; }

    }
    #endregion
}

