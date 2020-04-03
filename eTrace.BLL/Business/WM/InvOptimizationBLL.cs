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
    public class InvOptimizationBLL : eTraceBLLBase<InvOptimizationBLL, IInvOptimizationBLL>, IInvOptimizationBLL
    {
        private IInvOptimizationDAL tdIInvOptimizationDAL = null;
        private object _locker = new object();
        public InvOptimizationBLL()
        {
            tdIInvOptimizationDAL = DBManager.Instance.GetInvOptimizationDAL(EmDBType.eTraceConnection);
        }

        public ReportInvOptimizationModel GetInvOptimizationData(ReportInvOptimizationModelQuery query)
        {

            //lock (_locker)
            //{
                return tdIInvOptimizationDAL.GetInvOptimizationData(query);
            //}
        }

        public long InvOptimizationDataGetRowCount(ReportInvOptimizationModelQuery query)
        {
            return tdIInvOptimizationDAL.InvOptimizationDataGetRowCount(query);
        }
    }
}
