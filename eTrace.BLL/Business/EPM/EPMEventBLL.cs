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
    public class EPMEventBLL : eTraceBLLBase<EPMEventBLL, IEPMEventModuleBLL>, IEPMEventModuleBLL
    {
        private IEPMEventDAL tdIEPMEventDAL = null;
        public EPMEventBLL()
        {
            tdIEPMEventDAL = DBManager.Instance.GetEPMEventDAL(EmDBType.eTraceConnection);
        }

        public ReportEPMEventModel GetSMEPMEventDatas(ReportEPMEventModelQuery query)
        {
            return tdIEPMEventDAL.GetSMEPMEventDatas(query);
        }

        public ReportEPMEventItemModel GetSMEPMEventItemDatas(ReportEPMEventModelQuery query)
        {
            return tdIEPMEventDAL.GetSMEPMEventItemDatas(query);
        }

        public List<string> GetCategory()
        {
            return tdIEPMEventDAL.GetCategory();
        }

        public long EPMEventDataGetRowCount(ReportEPMEventModelQuery query)
        {
            return tdIEPMEventDAL.EPMEventDataGetRowCount (query);
        }

    }
}
