using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.IBLL
{
    public interface IWipModuleBLL
    {
        /// <summary>
        /// 获取WipFlow数据，包含部分Header数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<WipFlowDB> GetWipFlowDBs(WipFlowQuery query);
    }
}
