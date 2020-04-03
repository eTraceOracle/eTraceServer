using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    
    public class GetListOfRepairDataModel : ModelBase<List<GetListOfRepairDataModel.Item>>
    {

        public class Item
        {
            public string Model { get; set; }
            public bool PriDefect { get; set; }
            public string Description { get; set; }
            public string BusinessUnit { get; set; }
            public string ProdOrder { get; set; }
            public string OrgCode { get; set; }
            public string ExtSerialNo { get; set; }
            public string IntSerialNo { get; set; }
            public string Floor { get; set; }
            public string RepairerID { get; set; }
            public string RepairDate { get; set; }
            public string RepairTime { get; set; }
            public string FailDate { get; set; }
            public string FailTime { get; set; }
            public string ChangedBy { get; set; }
            public string ChangedOn { get; set; }
            public string ChangedTime { get; set; }
            public string StayMinutes { get; set; }
            public string TestStation { get; set; }
            public string TestRound { get; set; }
            public string SymptomCode { get; set; }
            public string SymptomCodeDesc { get; set; }
            public string DefectCode { get; set; }
            public string DefectCodeDesc { get; set; }
            public string Cause { get; set; }
            public string AssemblyPN { get; set; }
            public string CompRefD { get; set; }
            public string CompPN { get; set; }
            public string CompSupplier { get; set; }
            public string OrgCompDateCode { get; set; }
            public string OrgCompLotNo { get; set; }
            public string Comment { get; set; }
            public string Status { get; set; }
            public string ReturnToTestStep { get; set; }
            public string RepairRound { get; set; }
            public string CompDesc { get; set; }
            public string Category { get; set; }
            public string FailItem { get; set; }
            public string Tester { get; set; }
            public string UpperLimit { get; set; }
            public string LowerLimit { get; set; }
            public string Result { get; set; }
            public string TestStep { get; set; }
            public string BurnTime { get; set; }
            public string AgingTime { get; set; }
            public string RepSupplier { get; set; }
            public string RepCompDateCode { get; set; }
            public string RepLotNo { get; set; }
            public string RepCLID { get; set; }
            public string Marking { get; set; }
            public string RepComment { get; set; }
            public string SMTMaterial { get; set; }
            public string FAOperator { get; set; }
            public string FATime { get; set; }
            public string ReworkOperator { get; set; }
            public string ReworkTime { get; set; }
            public string AssAndQCOperator { get; set; }
            public string AssAndQCTime { get; set; }
            public string OwnerShip { get; set; }
            public string PossibleDateCode { get; set; }
            public string PossibleLotNO { get; set; }
        }
    } 
    
    public class GetListOfRepairDataQuery : ModelQueryBase<GetListOfRepairDataQuery.Item>
    {

        public class Item
        {
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime? FailTimeFrom { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime? FailTimeTo { get; set; }
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime? RepairDateFrom { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime? RepairDateTo { get; set; }
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime? FATimeFrom { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime? FATimeTo { get; set; }
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime? ChangeOnFrom { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime? ChangeOnTo { get; set; }
            /// <summary>
            /// 产品型号
            /// </summary>
            public string Model { get; set; }
            /// <summary>
            /// 楼层
            /// </summary>
            public string Floor { get; set; }
            /// <summary>
            /// 外部序列号
            /// </summary>
            public string ExtSN { get; set; }
            /// <summary>
            /// 内部序列号
            /// </summary>
            public string IntSN { get; set; }
            public string ChangedBy { get; set; }
            public string Station { get; set; }
            public string DefectCode { get; set; }
            public string Cause { get; set; }
            public string Customer { get; set; }
            public string BusinessUnit { get; set; }
            public string ComponentPN { get; set; }
            public string PrimaryDefect { get; set; }
            public string Tester { get; set; }
            public bool WIPUnit { get; set; }
            public bool MaterialInfo { get; set; }
        }


    }


    /// <summary>
    /// 
    /// </summary>
    public class PossibleMaterialInfo
    {
        public string Assembly;
        public string ItemNO;
        public string TLA_DJ;
        public List<string> DateCodeList { get; set; }
        public List<string> LotNOList { get; set; }
    }
}
