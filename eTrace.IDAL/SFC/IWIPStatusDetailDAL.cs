using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IWIPStatusDetailDAL
    {
        /// <summary>
        /// Query Data by Page
        /// </summary>
        /// <param name="query">Query Object,Constructed by condition and  pagiantion parameters</param>
        /// <returns></returns>
        List<WipStatusDetailItem> GetDataByPage(GetWipStatusQuery query);
        /// <summary>
        /// Query Data
        /// </summary>
        /// <param name="query">Query Object, pagiantion parameters can be nullable</param>
        /// <returns></returns>
        List<WipStatusDetailItem> GetData(GetWipStatusQuery query);
        /// <summary>
        /// Get the total number of data that meets the query conditions
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long GetTotalCount(GetWipStatusQuery.Item queryItem);
        

    }
}
