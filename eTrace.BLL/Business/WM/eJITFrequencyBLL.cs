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
    public class eJITFrequencyBLL : eTraceBLLBase<eJITFrequencyBLL, IeJITFrequencyBLL>, IeJITFrequencyBLL
    {
        private IeJITFrequencyDAL tdIeJITFrequencyDAL = null;
        public eJITFrequencyBLL()
        {
            tdIeJITFrequencyDAL = DBManager.Instance.GeteJITFrequencyDAL(EmDBType.eTraceConnection);
        }

        public ReporteJITFrequencyModel GeteJITFrequencyData(ReporteJITFrequencyModelQuery query)
        {
            return tdIeJITFrequencyDAL.GeteJITFrequencyData(query);
        }

        public long eJITFrequencyDataGetRowCount(ReporteJITFrequencyModelQuery query)
        {
            return tdIeJITFrequencyDAL.eJITFrequencyDataGetRowCount(query);
        }



    }
}
