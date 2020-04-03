using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportEquipmentPMSpecRequest : ServerRequestBase<ReportEquipmentPMSpecRequestItem>
        {

        }

        public class DownLoadEquipmentPMSpecRequest : ReportDownloadRequestBase<ReportEquipmentPMSpecRequestItem>
        {

        }


        public class ReportEquipmentPMSpecRequestItem
        {
            public string PMSpecID { get; set; }
            public string Frequency { get; set; }   
        }
        
}

