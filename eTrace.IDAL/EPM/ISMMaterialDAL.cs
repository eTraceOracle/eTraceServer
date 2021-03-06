﻿using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ISMMaterialDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportSMMaterialModel GetSMMaterialDatas(ReportSMMaterialModelQuery query);
                                                                               
        List<string> GetSMMaterialCategory();
        List<string> GetSMMaterialSubCategory();
        List<string> GetSMMaterialEquipCategory();
        List<string> GetSMMaterialEquipSubCategory();
        List<string> GetSMMaterialEquipModel();
        List<string> GetSMMaterialDefaultStore();

        long SMMaterialDataGetRowCount(ReportSMMaterialModelQuery query);
    }
}
