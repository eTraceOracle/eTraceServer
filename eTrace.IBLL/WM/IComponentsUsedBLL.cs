using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IComponentsUsedBLL

    {
        ReportComponentsUsedModel GetComponentsUsedData(ReportComponentsUsedModelQuery query);   //For Download use

        ReportComponentsUsedModel GetComponentsUsedByPage(ReportComponentsUsedModelQuery query);  //For Search use

        ReportComponentsUsedTLAModel GetComponentsUsedTLAData(ReportComponentsUsedModelQuery query);   //For Download use

        long ComponentsUsedDataGetRowCount(ReportComponentsUsedModelQuery query);


    }
}
