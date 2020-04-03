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
    public class SOShipmentBLL : eTraceBLLBase<SOShipmentBLL, ISOShipmentBLL>, ISOShipmentBLL
    {
        private ISOShipmentDAL tdISOShipmentDAL = null;
        public SOShipmentBLL()
        {
            tdISOShipmentDAL = DBManager.Instance.GetSOShipmentDAL(EmDBType.eTraceConnection);
        }

        public ReportSOShipmentDetailModel GetSOShipmentDetailData(ReportSOShipmentModelQuery query)
        {
            return tdISOShipmentDAL.GetSOShipmentDetailData(query);
        }

        public ReportSOShipmentSummaryModel GetSOShipmentSummaryData(ReportSOShipmentModelQuery query)
        {
            return tdISOShipmentDAL.GetSOShipmentSummaryData(query);
        }

        public long SOShipmentDataGetRowCount(ReportSOShipmentModelQuery query)
        {
            return tdISOShipmentDAL.SOShipmentDataGetRowCount(query);
        }



    }
}
