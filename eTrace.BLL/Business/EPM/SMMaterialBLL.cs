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
    public class SMMaterialBLL : eTraceBLLBase<SMMaterialBLL, ISMMaterialModuleBLL>, ISMMaterialModuleBLL
    {
        private ISMMaterialDAL tdISMMaterialDAL = null;
        public SMMaterialBLL()
        {
            tdISMMaterialDAL = DBManager.Instance.GetSMMaterialModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportSMMaterialModel GetSMMaterialDatas(ReportSMMaterialModelQuery query)
        {
            return tdISMMaterialDAL.GetSMMaterialDatas(query);
        }

        public long SMMaterialDataGetRowCount(ReportSMMaterialModelQuery query)
        {
            return tdISMMaterialDAL.SMMaterialDataGetRowCount (query);
        }

        public List<string> GetSMMaterialCategory()
        {
            return tdISMMaterialDAL.GetSMMaterialCategory();
        }

        public List<string> GetSMMaterialSubCategory()
        {
            return tdISMMaterialDAL.GetSMMaterialSubCategory();
        }

        public List<string> GetSMMaterialEquipCategory()
        {
            return tdISMMaterialDAL.GetSMMaterialEquipCategory();
        }

        public List<string> GetSMMaterialEquipSubCategory()
        {
            return tdISMMaterialDAL.GetSMMaterialEquipSubCategory();
        }

        public List<string> GetSMMaterialEquipModel()
        {
            return tdISMMaterialDAL.GetSMMaterialEquipModel();
        }

        public List<string> GetSMMaterialDefaultStore()
        {
            return tdISMMaterialDAL.GetSMMaterialDefaultStore();
        }

    }
}
