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
    public class RawMatPackingBLL : eTraceBLLBase<RawMatPackingBLL, IRawMatPackingBLL>, IRawMatPackingBLL
    {
        private IRawMatPackingDAL tdIRawMatPackingDAL = null;
        public RawMatPackingBLL()
        {
            tdIRawMatPackingDAL = DBManager.Instance.GetRawMatPackingDAL(EmDBType.eTraceConnection);
        }

        public ReportRawMatPackingModel GetRawMatPackingData(ReportRawMatPackingModelQuery query)
        {
            return tdIRawMatPackingDAL.GetRawMatPackingData(query);
        }

        public long RawMatPackingDataGetRowCount(ReportRawMatPackingModelQuery query)
        {
            return tdIRawMatPackingDAL.RawMatPackingDataGetRowCount(query);
        }



    }
}
