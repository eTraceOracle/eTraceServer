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
    public class RawMatPakStatusBLL : eTraceBLLBase<RawMatPakStatusBLL, IRawMatPakStatusBLL>, IRawMatPakStatusBLL
    {
        private IRawMatPakStatusDAL tdIRawMatPakStatusDAL = null;
        public RawMatPakStatusBLL()
        {
            tdIRawMatPakStatusDAL = DBManager.Instance.GetRawMatPakStatusDAL(EmDBType.eTraceConnection);
        }

        public ReportRawMatPakStatusModel GetRawMatPakStatusData(ReportRawMatPakStatusModelQuery query)
        {
            return tdIRawMatPakStatusDAL.GetRawMatPakStatusData(query);
        }

        public long RawMatPakStatusDataGetRowCount(ReportRawMatPakStatusModelQuery query)
        {
            return tdIRawMatPakStatusDAL.RawMatPakStatusDataGetRowCount(query);
        }



    }
}
