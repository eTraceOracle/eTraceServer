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
    public class SMMatTransBLL : eTraceBLLBase<SMMatTransBLL, ISMMatTransModuleBLL>, ISMMatTransModuleBLL
    {
        private ISMMatTransDAL tdISMMatTransDAL = null;
        public SMMatTransBLL()
        {
            tdISMMatTransDAL = DBManager.Instance.GetSMMatTransModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportSMMatTransModel GetSMMatTransDatas(ReportSMMatTransModelQuery query)
        {
            return tdISMMatTransDAL.GetSMMatTransDatas(query);
        }

        public long SMMatTransDataGetRowCount(ReportSMMatTransModelQuery query)
        {
            return tdISMMatTransDAL.SMMatTransDataGetRowCount (query);
        }

        public List<string> GetSMMatTransFromLocID()
        {
            return tdISMMatTransDAL.GetSMMatTransFromLocID();
        }

        public List<string> GetSMMatTransToLocID()
        {
            return tdISMMatTransDAL.GetSMMatTransToLocID();
        }

        public List<string> GetMovementType()
        {
            return tdISMMatTransDAL.GetMovementType();
        }
    }
}
