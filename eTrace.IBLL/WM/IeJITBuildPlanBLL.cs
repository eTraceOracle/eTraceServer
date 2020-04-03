using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IeJITBuildPlanBLL

    {
        ReporteJITBuildPlanSearchModel GeteJITBuildPlanSearchData(ReporteJITBuildPlanModelQuery query);    //For Download use

        ReporteJITBuildPlanSearchModel GeteJITBuildPlanSearchByPage(ReporteJITBuildPlanModelQuery query);  //For Search use

        long eJITBuildPlanSearchDataGetRowCount(ReporteJITBuildPlanModelQuery query);



        ReporteJITBuildPlanDetailModel GeteJITBuildPlanDetailData(ReporteJITBuildPlanModelQuery query);         //For Download use

        ReporteJITBuildPlanDetailModel GeteJITBuildPlanDetailByPage(ReporteJITBuildPlanModelQuery query);       //For Detail use

        long eJITBuildPlanDetailDataGetRowCount(ReporteJITBuildPlanModelQuery query);

    }
}
