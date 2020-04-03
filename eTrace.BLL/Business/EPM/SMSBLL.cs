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
    public class SMSBLL : eTraceBLLBase<SMSBLL, ISMSBLL>, ISMSBLL
    {
        private ISMSDAL tdISMSDAL = null;
        public SMSBLL()
        {
            tdISMSDAL = DBManager.Instance.GetSMSDAL(EmDBType.eTraceConnection);
        }


        public ReportSMSSummaryModel GetSMSSummaryData(ReportSMSModelQuery query)
        {
            ReportSMSSummaryModel resultModel = new ReportSMSSummaryModel()
            {
                Data = tdISMSDAL.GetSMSSummaryData(query),
                Pager = new ModelPager()
                {
                    TotalCount = tdISMSDAL.SMSSummaryDataGetRowCount(query)
                }

            };
            return resultModel;

        }

        public ReportSMSSummaryModel GetSMSSummaryByPage(ReportSMSModelQuery query)
        {
            ReportSMSSummaryModel resultModel = new ReportSMSSummaryModel()
            {
                Data = tdISMSDAL.GetSMSSummaryByPage(query),
                Pager = new ModelPager()
                {
                    TotalCount = tdISMSDAL.SMSSummaryDataGetRowCount(query)
                }
            };
            resultModel.IsOverMaxRow = resultModel.Pager.TotalCount > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return resultModel;

        }


        public long SMSSummaryDataGetRowCount(ReportSMSModelQuery query)
        {
            return tdISMSDAL.SMSSummaryDataGetRowCount(query);
        }



        public ReportSMSDetailModel GetSMSDetailData(ReportSMSModelQuery query)
        {
            ReportSMSDetailModel resultModel = new ReportSMSDetailModel()
            {
                Data = tdISMSDAL.GetSMSDetailData(query),
                Pager = new ModelPager()
                {
                    TotalCount = tdISMSDAL.SMSDetailDataGetRowCount(query)
                }
            };
            return resultModel;

        }

        public ReportSMSDetailModel GetSMSDetailByPage(ReportSMSModelQuery query)
        {
            ReportSMSDetailModel resultModel = new ReportSMSDetailModel()
            {
                Data = tdISMSDAL.GetSMSDetailByPage(query),

                Pager = new ModelPager()
                {
                    TotalCount = tdISMSDAL.SMSDetailDataGetRowCount(query)
                }
            };
            resultModel.IsOverMaxRow = resultModel.Pager.TotalCount > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return resultModel;

        }
        
        public long SMSDetailDataGetRowCount(ReportSMSModelQuery query)
        {
            return tdISMSDAL.SMSDetailDataGetRowCount(query);
        }


    }
}
