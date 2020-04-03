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
    public class IPPExceptionBLL : eTraceBLLBase<IPPExceptionBLL, IIPPExceptionBLL>, IIPPExceptionBLL
    {
        private IIPPExceptionDAL tdIIPPExceptionDAL = null;
        public IPPExceptionBLL()
        {
            tdIIPPExceptionDAL = DBManager.Instance.GetIPPExceptionDAL(EmDBType.eTraceConnection);
        }

         public ReportIPPExceptionModel GetIPPExceptionData(ReportIPPExceptionModelQuery query)
        {
            ReportIPPExceptionModel resultModel = new ReportIPPExceptionModel()
            {
                Data = tdIIPPExceptionDAL.GetIPPExceptionData(query),
            };
            return resultModel;

        }

        public ReportIPPExceptionModel GetIPPExceptionByPage(ReportIPPExceptionModelQuery query)
        {
            ReportIPPExceptionModel resultModel = new ReportIPPExceptionModel()
            {
                Data = tdIIPPExceptionDAL.GetIPPExceptionByPage(query),

                Pager = new ModelPager()
                {
                    TotalCount = tdIIPPExceptionDAL.IPPExceptionDataGetRowCount(query)
                }
            };
            resultModel.IsOverMaxRow = resultModel.Data.Count > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return resultModel;

        }

        public long IPPExceptionDataGetRowCount(ReportIPPExceptionModelQuery query)
        {
            return tdIIPPExceptionDAL.IPPExceptionDataGetRowCount(query);
        }

    }
}
