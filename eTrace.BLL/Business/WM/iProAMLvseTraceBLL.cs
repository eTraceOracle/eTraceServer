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
    public class iProAMLvseTraceBLL : eTraceBLLBase<iProAMLvseTraceBLL, IiProAMLvseTraceBLL>, IiProAMLvseTraceBLL
    {
        private IiProAMLvseTraceDAL tdIiProAMLvseTraceDAL = null;
        public iProAMLvseTraceBLL()
        {
            tdIiProAMLvseTraceDAL = DBManager.Instance.GetiProAMLvseTraceDAL(EmDBType.eTraceConnection);
        }

        public ReportiProAMLvseTraceModel GetiProAMLvseTraceData(ReportiProAMLvseTraceModelQuery query)
        {
            return tdIiProAMLvseTraceDAL.GetiProAMLvseTraceData(query);
        }

        public long iProAMLvseTraceDataGetRowCount(ReportiProAMLvseTraceModelQuery query)
        {
            return tdIiProAMLvseTraceDAL.iProAMLvseTraceDataGetRowCount(query);
        }



    }
}
