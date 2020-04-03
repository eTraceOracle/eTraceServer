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
    public class FGAgingBLL : eTraceBLLBase<FGAgingBLL, IFGAgingBLL>, IFGAgingBLL
    {
        private IFGAgingDAL tdIFGAgingDAL = null;
        public FGAgingBLL()
        {
            tdIFGAgingDAL = DBManager.Instance.GetFGAgingDAL(EmDBType.eTraceConnection);
        }

        public ReportFGAgingDetailModel GetFGAgingDetailData(ReportFGAgingModelQuery query)
        {
            return tdIFGAgingDAL.GetFGAgingDetailData(query);
        }

        public ReportFGAgingSummaryModel GetFGAgingSummaryData(ReportFGAgingModelQuery query)
        {
            return tdIFGAgingDAL.GetFGAgingSummaryData(query);
        }


        public long FGAgingDataGetRowCount(ReportFGAgingModelQuery query)
        {
            return tdIFGAgingDAL.FGAgingDataGetRowCount(query);
        }



    }
}
