namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportMobileEquipmentRequest : ServerRequestBase<ReportMobileEquipmentRequestItem>
    {

    }

    public class ReportMobileEquipmentRequestItem
    {
        public string EquipmentID { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string PictureURL { get; set; }
        public string MailGroup { get; set; }
    }

}