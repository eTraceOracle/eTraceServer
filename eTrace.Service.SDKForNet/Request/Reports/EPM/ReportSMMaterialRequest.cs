using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportSMMaterialRequest : ServerRequestBase<ReportSMMaterialRequestItem>
        {

        }

        public class DownLoadSMMaterialRequest : ReportDownloadRequestBase<ReportSMMaterialRequestItem>
        {

        }


        public class ReportSMMaterialRequestItem
        {
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string EquipCategory { get; set; }
        public string EquipSubCategory { get; set; }
        public string Material { get; set; }
        public string EquipModel { get; set; }
        public string DefaultStore { get; set; }
    }
        
}

