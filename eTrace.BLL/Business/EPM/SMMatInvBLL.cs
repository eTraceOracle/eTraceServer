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
    public class SMMatInvBLL : eTraceBLLBase<SMMatInvBLL, ISMMatInvModuleBLL>, ISMMatInvModuleBLL
    {
        private ISMMatInvDAL tdISMMatInvDAL = null;
        public SMMatInvBLL()
        {
            tdISMMatInvDAL = DBManager.Instance.GetSMMatInvModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportSMMatInvModel GetSMMatInvDatas(ReportSMMatInvModelQuery query)
        {
            return tdISMMatInvDAL.GetSMMatInvDatas(query);
        }

        public long SMMatInvDataGetRowCount(ReportSMMatInvModelQuery query)
        {
            return tdISMMatInvDAL.SMMatInvDataGetRowCount (query);
        }

    }
}
