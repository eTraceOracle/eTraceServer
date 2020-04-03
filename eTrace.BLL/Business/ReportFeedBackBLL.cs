using eTrace.Report.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eTrace.Model.V2.Report;
using eTrace.Core;
using eTrace.Report.IDAL;
using eTrace.Common;

namespace eTrace.Report.BLL.Business
{
    public class ReportFeedBackBLL : eTraceBLLBase<ReportFeedBackBLL, IReportFeedBackBLL>, IReportFeedBackBLL
    {
        private IReportFeedbackDAL _reportFeedbackDAL = null;
        public ReportFeedBackBLL()
        {
            _reportFeedbackDAL = DBManager.Instance.GetReportFeedbackDAL(EmDBType.eTraceConnection);
        }
        public bool RecordUserFeedback(ReportFeedbackModel reportFeedback)
        {
            return  _reportFeedbackDAL.InsertItem(reportFeedback);
        }
    }
}
