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
    public class eJITBuildPlanBLL : eTraceBLLBase<eJITBuildPlanBLL, IeJITBuildPlanBLL>, IeJITBuildPlanBLL
    {
        private IeJITBuildPlanDAL tdIeJITBuildPlanDAL = null;
        public eJITBuildPlanBLL()
        {
            tdIeJITBuildPlanDAL = DBManager.Instance.GeteJITBuildPlanDAL(EmDBType.eTraceConnection);
        }


        public ReporteJITBuildPlanSearchModel GeteJITBuildPlanSearchData(ReporteJITBuildPlanModelQuery query)
        {
            ReporteJITBuildPlanSearchModel resultModel = new ReporteJITBuildPlanSearchModel()
            {
                Data = tdIeJITBuildPlanDAL.GeteJITBuildPlanSearchData(query),
            };
            return resultModel;

        }

        public ReporteJITBuildPlanSearchModel GeteJITBuildPlanSearchByPage(ReporteJITBuildPlanModelQuery query)
        {
            ReporteJITBuildPlanSearchModel resultModel = new ReporteJITBuildPlanSearchModel()
            {
                Data = tdIeJITBuildPlanDAL.GeteJITBuildPlanSearchByPage(query),

                Pager = new ModelPager()
                {
                    TotalCount = tdIeJITBuildPlanDAL.eJITBuildPlanSearchDataGetRowCount(query)
                }
            };
            resultModel.IsOverMaxRow = resultModel.Data.Count > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return resultModel;

        }


        public long eJITBuildPlanSearchDataGetRowCount(ReporteJITBuildPlanModelQuery query)
        {
            return tdIeJITBuildPlanDAL.eJITBuildPlanSearchDataGetRowCount(query);
        }



        public ReporteJITBuildPlanDetailModel GeteJITBuildPlanDetailData(ReporteJITBuildPlanModelQuery query)
        {
            ReporteJITBuildPlanDetailModel resultModel = new ReporteJITBuildPlanDetailModel()
            {
                Data = tdIeJITBuildPlanDAL.GeteJITBuildPlanDetailData(query),
            };
            return resultModel;

        }

        public ReporteJITBuildPlanDetailModel GeteJITBuildPlanDetailByPage(ReporteJITBuildPlanModelQuery query)
        {
            ReporteJITBuildPlanDetailModel resultModel = new ReporteJITBuildPlanDetailModel()
            {
                Data = tdIeJITBuildPlanDAL.GeteJITBuildPlanDetailByPage(query),

                Pager = new ModelPager()
                {
                    TotalCount = tdIeJITBuildPlanDAL.eJITBuildPlanDetailDataGetRowCount(query)
                }
            };
            resultModel.IsOverMaxRow = resultModel.Data.Count > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return resultModel;

        }
        
        public long eJITBuildPlanDetailDataGetRowCount(ReporteJITBuildPlanModelQuery query)
        {
            return tdIeJITBuildPlanDAL.eJITBuildPlanDetailDataGetRowCount(query);
        }


    }
}
