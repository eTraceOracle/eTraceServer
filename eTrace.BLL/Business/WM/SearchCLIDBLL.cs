using eTrace.Common;
using eTrace.Core;
using eTrace.Report.IBLL;
using eTrace.Report.IDAL;
using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.BLL.Business
{
    public class SearchCLIDBLL : eTraceBLLBase<SearchCLIDBLL, ISearchCLIDBLL>, ISearchCLIDBLL
    {
        private ISearchCLIDDAL tdISearchCLIDDAL = null;
        public SearchCLIDBLL()
        {
            tdISearchCLIDDAL = DBManager.Instance.GetSearchCLIDDAL(EmDBType.eTraceConnection);
        }

        public ReportSearchCLIDModel GetSearchCLIDData(ReportSearchCLIDModelQuery query)
        {
            return tdISearchCLIDDAL.GetSearchCLIDData(query);
        }

        public long SearchCLIDDataGetRowCount(ReportSearchCLIDModelQuery query)
        {
            return tdISearchCLIDDAL.SearchCLIDDataGetRowCount(query);
        }


    }
}
