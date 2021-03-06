﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportSMLocResponse : ServerResponseBase<List<ReportSMLocResponse.Item>>
    {
        public class Item
        {
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string LocID { get; set; }
            public string FixtureID { get; set; }
            public string ItemNo { get; set; }
            public string Description { get; set; }
            public string MaxStorage { get; set; }
            public string Store { get; set; }
            public string Rack { get; set; }
            public string Bin { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }
        }
    }
}
