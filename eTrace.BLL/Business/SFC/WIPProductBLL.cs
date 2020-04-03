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
    public class WIPProductBLL : eTraceBLLBase<WIPProductBLL, IWIPProductModuleBLL>, IWIPProductModuleBLL
    {
        private IWIPProductDAL _tdProductV2ModuleDal = null;
        private eTrace.Report.IDAL.IWIPLockDAL _WIPLockV2ModuleDal = null;
        private eTrace.Report.IDAL.IWIPStatusDetailDAL _WIPStatusDetailDAL = null;
        private eTrace.Report.IDAL.IWIPStatusSummaryDAL _WIPStatusSummaryDAL = null;
        public WIPProductBLL()
        {
            _tdProductV2ModuleDal = DBManager.Instance.GetWIPProductModuleDAL(EmDBType.eTraceConnection);
            _WIPLockV2ModuleDal = DBManager.Instance.GetWIPLockDAL(EmDBType.eTraceConnection);
            _WIPStatusDetailDAL = DBManager.Instance.GetWIPStatusDetailDAL(EmDBType.eTraceConnection);
            _WIPStatusSummaryDAL = DBManager.Instance.GetWIPStatusSummaryDAL(EmDBType.eTraceConnection);
        }

        public WipInfo GetWipInfo(string intSN)
        {
            WipInfo wipInfo = new WipInfo();
            wipInfo.WIPHeader   = _tdProductV2ModuleDal.GetWIPheaderByIntSN(intSN);
            wipInfo.WIPPropertyList   = _tdProductV2ModuleDal.GetWIPPropertyListByIntSN(intSN);
            wipInfo.WIPFlowList    = _tdProductV2ModuleDal.GetWIPFlowListByIntSN(intSN);
            wipInfo.WIPTestDataList   = _tdProductV2ModuleDal.GetWIPTestDataListByIntSN (intSN);
            return wipInfo;
        }

        public ReportWIPProductModel GetWIPUnitFlow(WIPUnitFlowQuery query)
        {

            return _tdProductV2ModuleDal.GetWIPUnitFlow(query);
        }

        public GetWipLockDataResultModel GetWipLockData(GetWipLockDataModelQuery query)
        {
            GetWipLockDataResultModel resultModel = new GetWipLockDataResultModel() {
                Data= _WIPLockV2ModuleDal.GetWipLoackData(query),
                Pager=new ModelPager()
                {
                    TotalCount= _WIPLockV2ModuleDal.GetTotalCount(query.Data)
                }                
            };
            resultModel.IsOverMaxRow = resultModel.Data.Count > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount(); 
            return resultModel;
            
        }
        public GetWipLockDataResultModel GetWipLockDataByPage(GetWipLockDataModelQuery query)
        {
            GetWipLockDataResultModel resultModel = new GetWipLockDataResultModel()
            {
                Data = _WIPLockV2ModuleDal.GetWipLoackDataByPage(query),
                Pager = new ModelPager()
                {
                    TotalCount = _WIPLockV2ModuleDal.GetTotalCount(query.Data)
                }
            };
            resultModel.IsOverMaxRow = resultModel.Data.Count > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return resultModel;

        }
        public long GetWipLockDataTotalCount(GetWipLockDataModelQuery query)
        {
            return _WIPLockV2ModuleDal.GetTotalCount(query.Data);
        }

        public WipStatusDetailModel GetWipStatusDetailByPage(GetWipStatusQuery query)
        {
            WipStatusDetailModel resultModel = new WipStatusDetailModel()
            {
                Data = _WIPStatusDetailDAL.GetDataByPage(query),
                Pager = new ModelPager()
                {
                    TotalCount = _WIPStatusDetailDAL.GetTotalCount(query.Data)
                }
            };
            resultModel.IsOverMaxRow = resultModel.Data.Count > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return resultModel;
        }

        public GetProductErrorLogModel GetProductErrorLog(GetProductErrorLogQuery query)
        {
            GetProductErrorLogModel resultModel = new GetProductErrorLogModel()
            {
                //Data = _WIPLockV2ModuleDal.GetWipLoackDataByPage(query),
                //Pager = new ModelPager()
                //{
                //    TotalCount = _WIPLockV2ModuleDal.GetTotalCount(query.Data)
                //}
            };
            resultModel.IsOverMaxRow = resultModel.Data.Count > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return resultModel;
        }

        public ReportWIPTestDataModel GetWIPTestData(ReportWIPTestDataQuery query)
        {
            return _tdProductV2ModuleDal.GetWIPTestData(query);
        }

        public ReportWIPTDHeaderModel GetWIPTDHeader(ReportWIPTestDataQuery query)
        {
            return _tdProductV2ModuleDal.GetWIPTDHeader(query);
        }

        public List<WipStatusDetailItem> GetWipStatusDetail(GetWipStatusQuery query)
        {
            return _WIPStatusDetailDAL.GetData(query);
        }

        public long GetWipStatusDetailTotalCount(GetWipStatusQuery query)
        {
            return _WIPStatusDetailDAL.GetTotalCount(query.Data);
        }

        public WipStatusSummaryModel GetWipStatusSummaryByPage(GetWipStatusQuery query)
        {
            WipStatusSummaryModel resultModel = new WipStatusSummaryModel()
            {
                Data = _WIPStatusSummaryDAL.GetDataByPage(query),
                Pager = new ModelPager()
                {
                    TotalCount = _WIPStatusSummaryDAL.GetTotalCount(query.Data)
                },
                SummaryCount = _WIPStatusSummaryDAL.GetSummaryCount(query.Data)
                
            };
            resultModel.IsOverMaxRow = resultModel.Data.Count > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return resultModel; ;
        }
        //public List<GetWipStatusDetailItem> GetWipStatusDetail(GetWipStatusQuery query)
        //{
        //    return _WIPStatusDetailDAL.GetData(query);
        //}
        public List<WipStatusSummaryItem> GetWipStatusSummary(GetWipStatusQuery query)
        {
            return _WIPStatusSummaryDAL.GetData(query);
        }

        public long GetWipStatusSummaryTotalCount(GetWipStatusQuery query)
        {
            return _WIPStatusSummaryDAL.GetTotalCount(query.Data);
        }
        public long ReportWIPTDHeaderGetRowCount(ReportWIPTestDataQuery query)
        {
            return _tdProductV2ModuleDal.ReportWIPTDHeaderGetRowCount(query);
        }

        public long ReportWIPTestDataGetRowCount(ReportWIPTestDataQuery query)
        {
            return _tdProductV2ModuleDal.ReportWIPTestDataGetRowCount(query);
        }

        public ReportWIPPropertiesModel GetWIPProperties(ReportWIPTestDataQuery query)
        {
            return _tdProductV2ModuleDal.GetWIPProperties(query);
        }
    }
}
