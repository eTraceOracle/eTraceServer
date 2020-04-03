using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportProductDataModel : ModelBase<List<ReportProductDataModel.Item>>
    {
        public class Item
        { 
            public string ProductSerialNo { get; set; }
            public string PalletID { get; set; }
            public string BoxID { get; set; }
            public string SaleOrder { get; set; }
            public string DeliveryNote { get; set; }
            public string DiscreteJob { get; set; }
            public string ProductionLine { get; set; }
            public int DJQty { get; set; }
            public string Model { get; set; }
            public string ModelRev { get; set; }
            public string CustomerPN { get; set; }
            public string CustomerRev { get; set; }
            public string SerialNo2 { get; set; }
            public string SerialNo3 { get; set; }
            public string SerialNo4 { get; set; }
            public string TVA { get; set; }
            public string FlatFile { get; set; }
            public string SentBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string PreSN { get; set; }
        }
    }

    public class ReportProductDataQuery : ModelQueryBase
    {
        public string Model { get; set; }
        public string SaleOrder { get; set; }
        public string DiscreteJob { get; set; }
        public string DeliveryNote { get; set; }
        public string FlatFile { get; set; }
        public string SendBy { get; set; }
        public string PalletID { get; set; }
        public string BoxID { get; set; }
        public string ProductSN { get; set; }
        public string TVANo { get; set; }
        public string Floor { get; set; }
        public string SerialNo2 { get; set; }
        public string SerialNo3 { get; set; }
        public string SerialNo4 { get; set; }
        public bool IsTracePreViousSN { get; set; }
        public DateTime? CreatedOnFrom { get; set; }
        public DateTime? CreatedOnTo { get; set; }
    }
}
