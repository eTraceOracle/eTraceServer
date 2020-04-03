using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ISMJobDataRecordsDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportSMJobDataRecordsJobModel GetSMJobDataRecordsJobDatas(ReportSMJobDataRecordsQuery query);
        ReportSMJobDataRecordsEquipmentModel GetSMJobDataRecordsEquipmentDatas(ReportSMJobDataRecordsQuery query);
        ReportSMJobDataRecordsSPGModel GetSMJobDataRecordsSPGDatas(ReportSMJobDataRecordsQuery query);
                                   
        long SMJobDataRecordsJobGetRowCount(ReportSMJobDataRecordsQuery query);
        long SMJobDataRecordsEquipmentGetRowCount(ReportSMJobDataRecordsQuery query);
        long SMJobDataRecordsSPGGetRowCount(ReportSMJobDataRecordsQuery query);
    }
}
