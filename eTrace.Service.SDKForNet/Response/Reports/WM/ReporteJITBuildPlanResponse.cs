using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReporteJITBuildPlanDetailResponse : ServerResponseBase<List<ReporteJITBuildPlanDetailResponse.Item>>
    {
        public class Item : ReporteJITBuildPlanDetailModel.Item

        {

        }

    }

    public class ReporteJITBuildPlanSearchResponse : ServerResponseBase<List<ReporteJITBuildPlanSearchResponse.Item>>
    {

        public class Item : ReporteJITBuildPlanSearchModel.Item

        {

        }

    }


}
