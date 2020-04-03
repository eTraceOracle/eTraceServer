using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportSMJobDataRecordsJobResponse : ServerResponseBase<List<ReportSMJobDataRecordsJobResponse.Job>>
    {
        public class Job: ReportSMJobDataRecordsJobModel.Job
        {

        }
    }

    public class ReportSMJobDataRecordsEquipmentResponse : ServerResponseBase<List<ReportSMJobDataRecordsEquipmentResponse.Equipment>>
    {
        public class Equipment : ReportSMJobDataRecordsEquipmentModel.Equipment
        {

        }
    }

    public class ReportSMJobDataRecordsSPGResponse : ServerResponseBase<List<ReportSMJobDataRecordsSPGResponse.SPG>>
    {
        public class SPG : ReportSMJobDataRecordsSPGModel.SPG
        {

        }
    }

}
