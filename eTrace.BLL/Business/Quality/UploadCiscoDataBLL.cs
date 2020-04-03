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
    public class UploadCiscoDataBLL : eTraceBLLBase<UploadCiscoDataBLL, IUploadCiscoDataModuleBLL>, IUploadCiscoDataModuleBLL
    {
        //private IUploadCiscoDataDAL tdUploadCiscoDataV2ModuleDal = null;
        private IUploadCiscoDataDAL tdUploadCiscoDataRptModuleDal = null;
        public UploadCiscoDataBLL()
        {
            tdUploadCiscoDataRptModuleDal = DBManager.Instance.GetUploadCiscoDataModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportUploadCiscoDataModel GetUploadCiscoDataData(ReportUploadCiscoDataQuery query)
        {

            return tdUploadCiscoDataRptModuleDal.GetUploadCiscoDataData(query);
        }

        public long UploadCiscoDataDataGetRowCount(ReportUploadCiscoDataQuery query)
        {
            return tdUploadCiscoDataRptModuleDal.UploadCiscoDataDataGetRowCount(query);
        }

    }
}
