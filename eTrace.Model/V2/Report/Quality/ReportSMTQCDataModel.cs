using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportSMTQCDataModel : ModelBase<List<ReportSMTQCDataModel.Item>>
    {
        public class Item
        {
            public string Model { get; set; }
            public string ProdDate { get; set; }
            public string Shift { get; set; }
            public string Floor { get; set; }
            public string LineType { get; set; }
            public string Line { get; set; }
            public decimal SeqNo { get; set; }
            public string Revision { get; set; }
            public string PO { get; set; }
            public string TlaPN { get; set; }
            public string TlaDesc { get; set; }
            public string TestStation { get; set; }
            public string CauseCode { get; set; }
            public string DefectCode { get; set; }
            public string DefectCodeDesc { get; set; }
            public string RootCause { get; set; }
            public string Comment { get; set; }
            public decimal DefectQty { get; set; }
            public decimal CompQty { get; set; }
            public decimal OFD { get; set; }
            public decimal OppQty { get; set; }
            public decimal InspectQty { get; set; }
            public decimal BuildQty { get; set; }
            public string ProdSNs { get; set; }
            public string CompPosition { get; set; }
            public string Component { get; set; }
            public string CompDesc { get; set; }
            public string CompSupplier { get; set; }
            public string DateCode { get; set; }
            public string LotNo { get; set; }
            public string Marking { get; set; }
            public string TVA { get; set; }
            public string BoardStatus { get; set; }
            public string TinStoveStatus { get; set; }
            public string QCEmpID { get; set; }
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string WScarrierNo { get; set; }
            public string PinNo { get; set; }
            public string PCBA { get; set; }
        }
    }

    public class ReportSMTQCDataQuery : ModelQueryBase
    {
        public string Model { get; set; }
        public string Shift { get; set; }
        public string LineType { get; set; }
        public string Floor { get; set; }
        public DateTime? PDF { get; set; }
        public DateTime? PDT { get; set; }
    }
}
