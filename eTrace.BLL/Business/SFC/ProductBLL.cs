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

namespace eTrace.Report.BLL
{
    public class ProductBLL : eTraceBLLBase<ProductBLL, IProductModuleBLL>, IProductModuleBLL
    {
        private IProductDAL tdProductV2ModuleDal = null; 

        public ProductBLL()
        {
            tdProductV2ModuleDal = DBManager.Instance.GetTDHeaderModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportTDHeaderModel GetTDHeaders(ReportTDHeaderQuery query)
        {
            return tdProductV2ModuleDal.GetTDHeaders(query);
        }

        public ReportProductTestDataModel GetProductTestData(ReportProductTestDataQuery query)
        {
            return tdProductV2ModuleDal.GetProductTestData(query);
        }

        public long ProductTestDataGetRowCount(ReportProductTestDataQuery query)
        {
            return tdProductV2ModuleDal.ProductTestDataGetRowCount(query);
        }
        public ReportProductDataModel GetProductDatas(ReportProductDataQuery query)
        {
            return tdProductV2ModuleDal.GetProductDatas(query);
        }

        public ReportProductTestSummaryDataModel GetProductTestDataSummary(ReportProductTestDataQuery query)
        {

            return tdProductV2ModuleDal.GetProductTestDataSummary(query);
        }

        public long ProductTestDataSummaryGetRowCount(ReportProductTestDataQuery query)
        {
            return tdProductV2ModuleDal.ProductTestDataSummaryGetRowCount(query);
        }

        public long ProductDataGetRowCount(ReportProductDataQuery query)
        {
            return tdProductV2ModuleDal.ProductDataGetRowCount(query);
        }

   
    }
}
