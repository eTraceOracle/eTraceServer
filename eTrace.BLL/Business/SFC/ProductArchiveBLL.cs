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
    public class ProductArchiveBLL : eTraceBLLBase<ProductArchiveBLL, IProductArchiveModuleBLL>, IProductArchiveModuleBLL
    {
        private IProductArvhiveDAL   tdProductV2ModuleDal = null; 
        
        public ProductArchiveBLL()
        {
            tdProductV2ModuleDal = DBManager.Instance.GetProductArchiveModuleDAL(EmDBType.eTraceConnectionArchive);
        }

        public ReportTDHeaderModel GetTDHeaders(ReportTDHeaderQuery query)
        {
            return tdProductV2ModuleDal.GetTDHeaders(query);
        }

        public ReportProductTestDataArchiveModel GetProductTestData(ReportProductTestDataArchiveQuery query)
        {
            return tdProductV2ModuleDal.GetProductTestData(query);
        }

        public long ProductTestDataGetRowCount(ReportProductTestDataArchiveQuery query)
        {
            return tdProductV2ModuleDal.ProductTestDataGetRowCount(query);
        }
        public ReportProductArchiveDataModel GetProductDatasArchive(ReportProductArchiveDataQuery query)
        {
            return tdProductV2ModuleDal.GetProductDatasArchive(query);
        }

        public ReportProductTestSummaryDataArchiveModel GetProductTestDataSummary(ReportProductTestDataArchiveQuery query)
        {

            return tdProductV2ModuleDal.GetProductTestDataSummary(query);
        }

        public long ProductTestDataSummaryGetRowCount(ReportProductTestDataArchiveQuery query)
        {
            return tdProductV2ModuleDal.ProductTestDataSummaryGetRowCount(query);
        }

        public long ProductDataGetRowCount(ReportProductArchiveDataQuery query)
        {
            return tdProductV2ModuleDal.ProductDataGetRowCount(query);
        }

      
    }
}
