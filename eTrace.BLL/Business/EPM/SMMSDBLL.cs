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
    public class SMMSDBLL : eTraceBLLBase<SMMSDBLL, ISMMSDModuleBLL>, ISMMSDModuleBLL
    {
        private ISMMSDDAL tdISMMSDDAL = null;
        public SMMSDBLL()
        {
            tdISMMSDDAL = DBManager.Instance.GetSMMSDDAL(EmDBType.eTraceConnection);
        }

        public ReportSMMSDModel GetSMMSDDatas(ReportSMMSDModelQuery query)
        {
            return tdISMMSDDAL.GetSMMSDDatas(query);
        }

        public ReportSMMSDPMItemModel GetSMMSDPMItemDatas(ReportSMMSDModelQuery query)
        {
            return tdISMMSDDAL.GetSMMSDPMItemDatas(query);
        }

        public List<string> GetSMMSDLastTransaction()
        {
            return tdISMMSDDAL.GetSMMSDLastTransaction();
        }

        public long SMMSDDataGetRowCount(ReportSMMSDModelQuery query)
        {
            return tdISMMSDDAL.SMMSDDataGetRowCount (query);
        }

    }
}
