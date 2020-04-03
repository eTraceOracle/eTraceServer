using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportSMPIHeaderResponse : ServerResponseBase<List<ReportSMPIHeaderResponse.Header>>
    {
        public class Header: ReportSMPIHeaderModel.Header
        {

        }
    }

    public class ReportSMPIEquipmentsResponse : ServerResponseBase<List<ReportSMPIEquipmentsResponse.Equipments>>
    {

        public class Equipments: ReportSMPIEquipmentsModel.Equipments
        {

        }
    }

    public class ReportSMPIMatsResponse : ServerResponseBase<List<ReportSMPIMatsResponse.Mats>>
    {
        public class Mats: ReportSMPIMatsModel.Mats
        {


        }
    }

    public class ReportSMPIHistoryResponse : ServerResponseBase<List<ReportSMPIHistoryResponse.History>>
    {
        public class History: ReportSMPIHistoryModel.History
        {

        }
    }

  }

