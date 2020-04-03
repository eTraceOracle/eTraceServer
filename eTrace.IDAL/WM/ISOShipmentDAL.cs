using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ISOShipmentDAL
    {

        /// <summary>
        /// 获取Material Label Information
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportSOShipmentDetailModel GetSOShipmentDetailData(ReportSOShipmentModelQuery query);
                                   
        ReportSOShipmentSummaryModel GetSOShipmentSummaryData(ReportSOShipmentModelQuery query);


        //List<string> GetSOShipmentID();


        long SOShipmentDataGetRowCount(ReportSOShipmentModelQuery query);
    }
}
