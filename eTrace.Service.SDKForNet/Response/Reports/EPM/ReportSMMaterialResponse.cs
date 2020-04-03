using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportSMMaterialResponse : ServerResponseBase<List<ReportSMMaterialResponse.Item>>
    {
        public class Item
        {
            public string Material { get; set; }
            public string Description { get; set; }
            public string UOM { get; set; }
            public string Spec { get; set; }
            public string Image { get; set; }
            public string PictureUrl { get; set; }
            public string StdCost { get; set; }
            public string Currency { get; set; }
            public string MatType { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string EquipCategory { get; set; }
            public string EquipSubCategory { get; set; }
            public string EquipModel { get; set; }
            public string DefaultStock { get; set; }
            public string SafetyStock { get; set; }
            public string Status { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }
        }
    }
}
