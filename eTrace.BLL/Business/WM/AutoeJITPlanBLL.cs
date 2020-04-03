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
    public class AutoeJITPlanBLL : eTraceBLLBase<AutoeJITPlanBLL, IAutoeJITPlanBLL>, IAutoeJITPlanBLL
    {
        private IAutoeJITPlanDAL tdIAutoeJITPlanDAL = null;
        public AutoeJITPlanBLL()
        {
            tdIAutoeJITPlanDAL = DBManager.Instance.GetAutoeJITPlanDAL(EmDBType.eTraceConnection);
        }

        public ReportAutoeJITPlanModel GetAutoeJITPlanData(ReportAutoeJITPlanModelQuery query)
        {
            return tdIAutoeJITPlanDAL.GetAutoeJITPlanData(query);
        }

        public long AutoeJITPlanDataGetRowCount(ReportAutoeJITPlanModelQuery query)
        {
            return tdIAutoeJITPlanDAL.AutoeJITPlanDataGetRowCount(query);
        }



    }
}
