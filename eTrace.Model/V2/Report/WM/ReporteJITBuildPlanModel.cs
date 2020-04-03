using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
 
    public class ReporteJITBuildPlanSearchModel : ModelBase<List<ReporteJITBuildPlanSearchModel.Item>>
    {
        public class Item
        { 
            public string Productionfloor { get; set; }
            public DateTime CreateOn { get; set; }
            public string CreateBy { get; set; }
            public string DJNO { get; set; }
            public string Model { get; set; }
            public string Subinventory { get; set; }
            public decimal RequireQty { get; set; }
            public string OrgCode { get; set; }
            public decimal WipQty { get; set; }
            public string DeliveryDate { get; set; }
            public string ShipInstruction { get; set; }
        }
    }

    public class ReporteJITBuildPlanDetailModel : ModelBase<List<ReporteJITBuildPlanDetailModel.Item>>
    {
        public class Item
        {
            public string Productionfloor { get; set; }
            public DateTime CreateOn { get; set; }
            public string CreateBy { get; set; }
            public string SubInventory { get; set; }
            public string OrgCode { get; set; }
            public string ItemNo { get; set; }
            public decimal ItemRequiredQty { get; set; }
            public decimal OrderQTy { get; set; }
            public string DeliveryDate { get; set; }
            public string OrderNumber { get; set; }
            public decimal RecQty { get; set; }
            public string RecBy { get; set; }
            public string RecON { get; set; }
            public string eJITID { get; set; }
            public string Status { get; set; }
            public string ShipInstruction { get; set; }
        }
    }


    public class ReporteJITBuildPlanModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string Productionfloor { get; set; }
        public DateTime UploadFrom { get; set; }
        public DateTime UploadTo { get; set; }
        public string ItemNO { get; set; }
        public string UploadBy { get; set; }
        public Boolean DisplayOpen { get; set; }

    }
}
