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
    public class LabelInfoBLL : eTraceBLLBase<LabelInfoBLL, ILabelInfoBLL>, ILabelInfoBLL
    {
        private ILabelInfoDAL tdILabelInfoDAL = null;
        public LabelInfoBLL()
        {
            tdILabelInfoDAL = DBManager.Instance.GetLabelInfoDAL(EmDBType.eTraceConnection);
        }

        public ReportLabelInfoDetailModel GetLabelInfoDetailData(ReportLabelInfoModelQuery query)
        {
            return tdILabelInfoDAL.GetLabelInfoDetailData(query);
        }

        public ReportLabelInfoSummaryModel GetLabelInfoSummaryData(ReportLabelInfoModelQuery query)
        {
            return tdILabelInfoDAL.GetLabelInfoSummaryData(query);
        }

        public ReportLabelInfoePurgeDTModel GetLabelInfoePurgeDTData(ReportLabelInfoModelQuery query)
        {
            return tdILabelInfoDAL.GetLabelInfoePurgeDTData(query);
        }

        public ReportLabelInfoePurgeSMModel GetLabelInfoePurgeSMData(ReportLabelInfoModelQuery query)
        {
            return tdILabelInfoDAL.GetLabelInfoePurgeSMData(query);
        }

        public long LabelInfoDataGetRowCount(ReportLabelInfoModelQuery query)
        {
            return tdILabelInfoDAL.LabelInfoDataGetRowCount(query);
        }



    }
}
