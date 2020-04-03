using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface ISMJobDataRecordsModuleBLL
    {
        ReportSMJobDataRecordsJobModel GetSMJobDataRecordsJobDatas(ReportSMJobDataRecordsQuery query);

        ReportSMJobDataRecordsEquipmentModel GetSMJobDataRecordsEquipmentDatas(ReportSMJobDataRecordsQuery query);

        ReportSMJobDataRecordsSPGModel GetSMJobDataRecordsSPGDatas(ReportSMJobDataRecordsQuery query);

        long SMJobDataRecordsJobGetRowCount(ReportSMJobDataRecordsQuery query);
        long SMJobDataRecordsEquipmentGetRowCount(ReportSMJobDataRecordsQuery query);
        long SMJobDataRecordsSPGGetRowCount(ReportSMJobDataRecordsQuery query);
    }
}
