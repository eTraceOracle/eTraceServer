using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ILabelInfoDAL
    {

        /// <summary>
        /// 获取Material Label Information
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportLabelInfoDetailModel GetLabelInfoDetailData(ReportLabelInfoModelQuery query);
                                   
        ReportLabelInfoSummaryModel GetLabelInfoSummaryData(ReportLabelInfoModelQuery query);


        ReportLabelInfoePurgeDTModel GetLabelInfoePurgeDTData(ReportLabelInfoModelQuery query);

        ReportLabelInfoePurgeSMModel GetLabelInfoePurgeSMData(ReportLabelInfoModelQuery query);


        //List<string> GetLabelInfoID();


        long LabelInfoDataGetRowCount(ReportLabelInfoModelQuery query);
    }
}
