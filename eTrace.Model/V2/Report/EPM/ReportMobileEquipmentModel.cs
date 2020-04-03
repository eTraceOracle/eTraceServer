using System.Collections.Generic;

namespace eTrace.Model.V2.Report
{
    public class ReportMobileEquipmentModel : ModelBase<List<ReportMobileEquipmentModel.Item>>
    {
        public class Item
        {
            public string EquipmentID { get; set; }
            public string Department { get; set; }
            public string Description { get; set; }              
            public string Status { get; set; }
            public string Location { get; set; }
            public string PMLastDate { get; set; }
            public string PMNextDate { get; set; }
            public string PMMan { get; set; }
            public string PictureURL { get; set; }
            public string MailGroup { get; set; }
        }
    }

    public class ReportMobileEquipmentQuery : ModelQueryBase
    {
        public string EquipmentID { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string PictureURL { get; set; }
        public string MailGroup { get; set; }

    }
}
