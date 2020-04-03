using eTrace.Model;
using eTrace.Model.V2.Report;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportSMMatInvRequest : ServerRequestBase<ReportSMMatInvRequestItem>
        {

        }

        public class DownLoadSMMatInvRequest : ReportDownloadRequestBase<ReportSMMatInvRequestItem>
        {

        }


        public class ReportSMMatInvRequestItem
        {
           
            public string Material { get; set; }
            public string LocID { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string EquipCategory { get; set; }
            public string EquipSubCategory { get; set; }
            public string EquipModel { get; set; }
            public string Store { get; set; }
        }   
        
}

