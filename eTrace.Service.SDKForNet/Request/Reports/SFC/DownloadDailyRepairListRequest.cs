using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class DownloadDailyRepairListRequest : ServerRequestBase<DailyRepairListRequestItem>
    {
        public List<TableHeader> WipInWipOutHeaders = new List<TableHeader>();
        public List<TableHeader> WipOutHeaders = new List<TableHeader>();
        public List<TableHeader> TopTenComponentHeaders = new List<TableHeader>();
        public List<TableHeader> MoreThanOneHeaders = new List<TableHeader>();
    }
    public class DailyRepairListRequestItem : eTrace.Model.V2.Report.DailyRepairList.RequestItem
    {

    }
}
