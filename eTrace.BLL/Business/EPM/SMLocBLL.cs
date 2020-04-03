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
    public class SMLocBLL : eTraceBLLBase<SMLocBLL, ISMLocModuleBLL>, ISMLocModuleBLL
    {
        private ISMLocDAL tdISMLocDAL = null;
        public SMLocBLL()
        {
            tdISMLocDAL = DBManager.Instance.GetSMLocModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportSMLocModel GetSMLocDatas(ReportSMLocModelQuery query)
        {
            return tdISMLocDAL.GetSMLocDatas(query);
        }

        public long SMLocDataGetRowCount(ReportSMLocModelQuery query)
        {
            return tdISMLocDAL.SMLocDataGetRowCount (query);
        }

        //public List<string> GetSMLocCate()
        //{
        //    return tdISMLocDAL.GetSMLocCate();
        //}

        public ReportSMLocModel GetSMLocCate()
        {
            return tdISMLocDAL.GetSMLocCate();
        }
        public List<string> GetSMLocCategory()
        {
            return tdISMLocDAL.GetSMLocCategory();
        }
        public List<string> GetSMLocSubCategory()
        {
            return tdISMLocDAL.GetSMLocSubCategory();
        }
        public List<string> GetSMLocStore()
        {
            return tdISMLocDAL.GetSMLocStore();
        }


    }
}
