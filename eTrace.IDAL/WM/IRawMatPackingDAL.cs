﻿using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IRawMatPackingDAL
    {

        /// <summary>
        /// 获取CLID Information
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportRawMatPackingModel GetRawMatPackingData(ReportRawMatPackingModelQuery query);
                                   

        long RawMatPackingDataGetRowCount(ReportRawMatPackingModelQuery query);
    }
}
