﻿using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportSMMatInvResponse : ServerResponseBase<List<ReportSMMatInvResponse.Item>>
    {
        public class Item: ReportSMMatInvModel.Item
        {
            //Item ReportSMMatInvModel = new Item();
        }
    }
}
