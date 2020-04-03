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
    public class SMFixtureTransBLL : eTraceBLLBase<SMFixtureTransBLL, ISMFixtureTransModuleBLL>, ISMFixtureTransModuleBLL
    {
        private ISMFixtureTransDAL tdISMFixtureTransDAL = null;
        public SMFixtureTransBLL()
        {
            tdISMFixtureTransDAL = DBManager.Instance.GetSMFixtureTransDAL(EmDBType.eTraceConnection);
        }

        public ReportSMFixtureTransModel GetSMFixtureTransDatas(ReportSMFixtureTransModelQuery query)
        {
            return tdISMFixtureTransDAL.GetSMFixtureTransDatas(query);
        }

        public long SMFixtureTransDataGetRowCount(ReportSMFixtureTransModelQuery query)
        {
            return tdISMFixtureTransDAL.SMFixtureTransDataGetRowCount (query);
        }

        public List<string> GetSMFixtureTransCate()
        {
            return tdISMFixtureTransDAL.GetSMFixtureTransCate();
        }

        public List<string> GetSMFixtureTransSubCate()
        {
            return tdISMFixtureTransDAL.GetSMFixtureTransSubCate();
        }

        public List<string> GetSMFixtureTransStatus()
        {
            return tdISMFixtureTransDAL.GetSMFixtureTransStatus();
        }

        public List<string> GetSMFixtureTransTransactionType()
        {
            return tdISMFixtureTransDAL.GetSMFixtureTransTransactionType();
        }

    }
}
