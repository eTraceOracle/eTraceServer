using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReporteJITBuildPlanRequest : ServerRequestBase<ReporteJITBuildPlanRequestItem>
     {

     }

   public class DownLoadeJITBuildPlanDetailRequest : ReportDownloadRequestBase<ReporteJITBuildPlanRequestItem>
     {

     }

    public class DownLoadeJITBuildPlanSearchRequest : ReportDownloadRequestBase<ReporteJITBuildPlanRequestItem>
    {

    }



    public class ReporteJITBuildPlanRequestItem
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

