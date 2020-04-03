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
    public class OnHandCompBLL : eTraceBLLBase<OnHandCompBLL, IOnHandCompBLL>, IOnHandCompBLL
    {
        private IOnHandCompDAL tdIOnHandCompDAL = null;
        public OnHandCompBLL()
        {
            tdIOnHandCompDAL = DBManager.Instance.GetOnHandCompDAL(EmDBType.eTraceConnection);
        }

        public ReportOnHandCompModel GetOnHandCompData(ReportOnHandCompModelQuery query)
        {
            return tdIOnHandCompDAL.GetOnHandCompData(query);
        }

        public long OnHandCompDataGetRowCount(ReportOnHandCompModelQuery query)
        {
            return tdIOnHandCompDAL.OnHandCompDataGetRowCount(query);
        }



    }
}
