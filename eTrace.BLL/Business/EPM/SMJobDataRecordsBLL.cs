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
    public class SMJobDataRecordsBLL : eTraceBLLBase<SMJobDataRecordsBLL, ISMJobDataRecordsModuleBLL>, ISMJobDataRecordsModuleBLL
    {
        private ISMJobDataRecordsDAL tdISMJobDataRecordsDAL = null;
        public SMJobDataRecordsBLL()
        {
            tdISMJobDataRecordsDAL = DBManager.Instance.GetSMJobDataRecordsDAL(EmDBType.eTraceConnection);
        }

        public ReportSMJobDataRecordsJobModel GetSMJobDataRecordsJobDatas(ReportSMJobDataRecordsQuery query)
        {
            return tdISMJobDataRecordsDAL.GetSMJobDataRecordsJobDatas(query);
        }

        public ReportSMJobDataRecordsEquipmentModel GetSMJobDataRecordsEquipmentDatas(ReportSMJobDataRecordsQuery query)
        {
            return tdISMJobDataRecordsDAL.GetSMJobDataRecordsEquipmentDatas(query);
        }

        public ReportSMJobDataRecordsSPGModel GetSMJobDataRecordsSPGDatas(ReportSMJobDataRecordsQuery query)
        {
            return tdISMJobDataRecordsDAL.GetSMJobDataRecordsSPGDatas(query);
        }

        public long SMJobDataRecordsJobGetRowCount(ReportSMJobDataRecordsQuery query)
        {
            return tdISMJobDataRecordsDAL.SMJobDataRecordsJobGetRowCount(query);
        }

        public long SMJobDataRecordsEquipmentGetRowCount(ReportSMJobDataRecordsQuery query)
        {
            return tdISMJobDataRecordsDAL.SMJobDataRecordsEquipmentGetRowCount(query);
        }
        public long SMJobDataRecordsSPGGetRowCount(ReportSMJobDataRecordsQuery query)
        {
            return tdISMJobDataRecordsDAL.SMJobDataRecordsSPGGetRowCount(query);
        }

    }
}
