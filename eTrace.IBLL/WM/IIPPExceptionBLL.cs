using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IIPPExceptionBLL

    {

        ReportIPPExceptionModel GetIPPExceptionData(ReportIPPExceptionModelQuery query);         //For Download use

        ReportIPPExceptionModel GetIPPExceptionByPage(ReportIPPExceptionModelQuery query);       //For Detail use

        long IPPExceptionDataGetRowCount(ReportIPPExceptionModelQuery query);
    }
}
