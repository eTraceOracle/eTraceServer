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
    public class SMSolderPasteGlueBLL : eTraceBLLBase<SMSolderPasteGlueBLL, ISMSolderPasteGlueModuleBLL>, ISMSolderPasteGlueModuleBLL
    {
        private ISMSolderPasteGlueDAL tdISMSolderPasteGlueDAL = null;
        public SMSolderPasteGlueBLL()
        {
            tdISMSolderPasteGlueDAL = DBManager.Instance.GetSMSolderPasteGlueDAL(EmDBType.eTraceConnection);
        }

        public ReportSMSolderPasteGlueModel GetSMSolderPasteGlueDatas(ReportSMSolderPasteGlueModelQuery query)
        {
            return tdISMSolderPasteGlueDAL.GetSMSolderPasteGlueDatas(query);
        }

        public long SMSolderPasteGlueDataGetRowCount(ReportSMSolderPasteGlueModelQuery query)
        {
            return tdISMSolderPasteGlueDAL.SMSolderPasteGlueDataGetRowCount (query);
        }

        public List<string> GetSMSolderPasteGlueLastTransaction()
        {
            return tdISMSolderPasteGlueDAL.GetSMSolderPasteGlueLastTransaction();
        }

    }
}
