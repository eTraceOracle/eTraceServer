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
    public class ComponentsUsedBLL : eTraceBLLBase<ComponentsUsedBLL, IComponentsUsedBLL>, IComponentsUsedBLL
    {
        private IComponentsUsedDAL tdIComponentsUsedDAL = null;
        public ComponentsUsedBLL()
        {
            tdIComponentsUsedDAL = DBManager.Instance.GetComponentsUsedDAL(EmDBType.eTraceConnection);
        }

        public ReportComponentsUsedModel GetComponentsUsedData(ReportComponentsUsedModelQuery query)
        {
            ReportComponentsUsedModel resultModel = new ReportComponentsUsedModel()
            {
                Data = tdIComponentsUsedDAL.GetComponentsUsedData(query),
            };
            return resultModel;

        }

        public ReportComponentsUsedModel GetComponentsUsedByPage(ReportComponentsUsedModelQuery query)
        {
            ReportComponentsUsedModel resultModel = new ReportComponentsUsedModel()
            {
                Data = tdIComponentsUsedDAL.GetComponentsUsedByPage(query),

                Pager = new ModelPager()
                {
                    TotalCount = tdIComponentsUsedDAL.ComponentsUsedDataGetRowCount(query)
                }
            };
            resultModel.IsOverMaxRow = resultModel.Data.Count > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return resultModel;

        }

        public long ComponentsUsedDataGetRowCount(ReportComponentsUsedModelQuery query)
        {
            return tdIComponentsUsedDAL.ComponentsUsedDataGetRowCount(query);
        }

        public ReportComponentsUsedTLAModel GetComponentsUsedTLAData(ReportComponentsUsedModelQuery query)
        {
            ReportComponentsUsedTLAModel resultModel = new ReportComponentsUsedTLAModel()
            {
                Data = tdIComponentsUsedDAL.GetComponentsUsedTLAData(query),
            };
            return resultModel;

        }

    }
}
