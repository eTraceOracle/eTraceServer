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
    public class SMFeederBLL : eTraceBLLBase<SMFeederBLL, ISMFeederModuleBLL>, ISMFeederModuleBLL
    {
        private ISMFeederDAL tdISMFeederDAL = null;
        public SMFeederBLL()
        {
            tdISMFeederDAL = DBManager.Instance.GetSMFeederDAL(EmDBType.eTraceConnection);
        }

        public ReportSMFeederHeaderModel GetSMFeederHeaderDatas(ReportSMFeederHeaderModelQuery query)
        {
            return tdISMFeederDAL.GetSMFeederHeaderDatas(query);
        }

        public ReportSMFeederPMHeaderModel GetSMFeederPMHeaderDatas(ReportSMFeederHeaderModelQuery query)
        {
            return tdISMFeederDAL.GetSMFeederPMHeaderDatas(query);
        }
        public ReportSMFeederPMHeaderItemModel GetSMFeederPMHeaderItemDatas(ReportSMFeederHeaderModelQuery query)
        {
            return tdISMFeederDAL.GetSMFeederPMHeaderItemDatas(query);
        }
        public ReportSMFeederPMHeaderMatModel GetSMFeederPMHeaderMatDatas(ReportSMFeederHeaderModelQuery query)
        {
            return tdISMFeederDAL.GetSMFeederPMHeaderMatDatas(query);
        }
        public ReportSMFeederRepairHeaderModel GetSMFeederRepairHeaderDatas(ReportSMFeederHeaderModelQuery query)
        {
            return tdISMFeederDAL.GetSMFeederRepairHeaderDatas(query);
        }
        public ReportSMFeederRepairHeaderMatModel GetSMFeederRepairHeaderMatDatas(ReportSMFeederHeaderModelQuery query)
        {
            return tdISMFeederDAL.GetSMFeederRepairHeaderMatDatas(query);
        }

        public long SMFeederDataGetRowCount(ReportSMFeederHeaderModelQuery query)
        {
            return tdISMFeederDAL.SMFeederDataGetRowCount (query);
        }

    }
}
