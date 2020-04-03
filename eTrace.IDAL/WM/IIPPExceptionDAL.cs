using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IIPPExceptionDAL
    {

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReportIPPExceptionModel.Item> GetIPPExceptionByPage(ReportIPPExceptionModelQuery query);


        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReportIPPExceptionModel.Item> GetIPPExceptionData(ReportIPPExceptionModelQuery query);


        /// <summary>
        /// 获取符合查询条件的总数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long IPPExceptionDataGetRowCount(ReportIPPExceptionModelQuery query);
    }
}
