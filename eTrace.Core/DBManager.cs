using eTrace.Common;
using eTrace.Report.IDAL;
using eTrace.Report.SqlServerDAL.V2.Report;
using eTrace.SqlServerDAL.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Core
{
    public class DBManager
    {
        private static DBManager instance = new DBManager();
        public static DBManager Instance
        {
            get { return instance; }
        }

        #region Report
        #region V2

        public IReportFeedbackDAL GetReportFeedbackDAL(DBHelper dbHelper)
        {
            return new ReportFeedBackDAL(dbHelper);
        }
        public IReportFeedbackDAL GetReportFeedbackDAL(EmDBType dbType)
        {
            return new ReportFeedBackDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public IProductDAL GetTDHeaderModuleDAL(DBHelper dbHelper)
        {
            return new ProductDAL(dbHelper);
        }
        public IProductDAL GetTDHeaderModuleDAL(EmDBType dbType)
        {
            return new ProductDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public IProductArvhiveDAL GetProductArchiveModuleDAL(DBHelper dbHelper)
        {
            return new ProductArchiveDAL(dbHelper);
        }
        public IProductArvhiveDAL GetProductArchiveModuleDAL(EmDBType dbType)
        {
            return new ProductArchiveDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public IWIPLockDAL GetWIPLockDAL(DBHelper dbHelper)
        {
            return new eTrace.SqlServerDAL.V2.Report.WIPLockDAL(dbHelper);
        }
        public IWIPLockDAL GetWIPLockDAL(EmDBType dbType)
        {
            return new eTrace.SqlServerDAL.V2.Report.WIPLockDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public IWIPStatusDetailDAL GetWIPStatusDetailDAL(EmDBType dbType)
        {
            return new eTrace.SqlServerDAL.V2.Report.WIPStatusDetailDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public IWIPStatusSummaryDAL GetWIPStatusSummaryDAL(EmDBType dbType)
        {
            return new eTrace.SqlServerDAL.V2.Report.WIPStatusSummaryDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public IProductErrorLogDAL GetPorductErrorLogDAL(EmDBType dbType)
        {
            return new eTrace.SqlServerDAL.V2.Report.PorductErrorLogDAL(DBManagerConfig.Instance.GetDbHelper(dbType));

        }
        public IListOfRepairDataDAL GetListOfRepairDataDAL(EmDBType dbType)
        {
            return new eTrace.SqlServerDAL.V2.Report.ListOfRepairDataDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public Report.IDAL.DailyRepairList.IMoreThanOneDAL GetMoreThanOneDAL(EmDBType dbType)
        {
            return new eTrace.SqlServerDAL.V2.Report.MoreThanOneDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public Report.IDAL.DailyRepairList.ITopTenComonentDAL GetTopTenComonentDAL(EmDBType dbType)
        {
            return new eTrace.SqlServerDAL.V2.Report.TopTenComonentDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public Report.IDAL.DailyRepairList.IWipInWipOutDAL GetWipInWipOutDAL(EmDBType dbType)
        {
            return new eTrace.SqlServerDAL.V2.Report.WipInWipOutDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public Report.IDAL.DailyRepairList.IWipOutDAL GetWipOutDAL(EmDBType dbType)
        {
            return new eTrace.SqlServerDAL.V2.Report.WipOutDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }


        public IResourceDAL GetResourceModuleDAL(DBHelper dbHelper)
        {
            return new ResourceDAL(dbHelper);
        }
        public IResourceDAL GetResourceModuleDAL(EmDBType dbType)
        {
            return new ResourceDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }

        public IWIPProductDAL GetWIPProductModuleDAL(DBHelper dbHelper)
        {
            return new WIPProductDAL(dbHelper);
        }
        public IWIPProductDAL GetWIPProductModuleDAL(EmDBType dbType)
        {
            return new WIPProductDAL(dbType);
        }
        #endregion
        #endregion

        #region EPM
        #region Fixture
        public IFixtureDAL GetFixtureModuleDAL(EmDBType dbType)
        {
            return new FixtureDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }

        public IFixtureCategoryDAL GetEPMResourceModuleDAL(EmDBType dbType)
        {
            return new FixtureCategoryDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region Equipment
        public IEquipmentDAL GetEquipmentModuleDAL(DBHelper dbHelper)
        {
            return new EquipmentDAL(dbHelper);
        }
        public IEquipmentDAL GetEquipmentModuleDAL(EmDBType dbType)
        {
            return new EquipmentDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region MobileEquipment
        public IMobileEquipmentDAL GetMobileEquipmentModuleDAL(DBHelper dbHelper)
        {
            return new MobileEquipmentDAL(dbHelper);
        }
        public IMobileEquipmentDAL GetMobileEquipmentModuleDAL(EmDBType dbType)
        {
            return new MobileEquipmentDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region EquipmentFixturePMHeader
        public IEquipmentFixturePMHeaderDAL GetEquipmentFixturePMHeaderModuleDAL(DBHelper dbHelper)
        {
            return new EquipmentFixturePMHeaderDAL(dbHelper);
        }
        public IEquipmentFixturePMHeaderDAL GetEquipmentFixturePMHeaderModuleDAL(EmDBType dbType)
        {
            return new EquipmentFixturePMHeaderDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region EquipmentRepairHeader
        public IEquipmentRepairHeaderDAL GetEquipmentRepairHeaderModuleDAL(DBHelper dbHelper)
        {
            return new EquipmentRepairHeaderDAL(dbHelper);
        }
        public IEquipmentRepairHeaderDAL GetEquipmentRepairHeaderModuleDAL(EmDBType dbType)
        {
            return new EquipmentRepairHeaderDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region EquipmentPMPlan
        public IEquipmentPMPlanDAL GetEquipmentPMPlanModuleDAL(DBHelper dbHelper)
        {
            return new EquipmentPMPlanDAL(dbHelper);
        }
        public IEquipmentPMPlanDAL GetEquipmentPMPlanModuleDAL(EmDBType dbType)
        {
            return new EquipmentPMPlanDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region EquipmentPMSpec
        public IEquipmentPMSpecDAL GetEquipmentPMSpecModuleDAL(DBHelper dbHelper)
        {
            return new EquipmentPMSpecDAL(dbHelper);
        }
        public IEquipmentPMSpecDAL GetEquipmentPMSpecModuleDAL(EmDBType dbType)
        {
            return new EquipmentPMSpecDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMLoc
        public ISMLocDAL GetSMLocModuleDAL(DBHelper dbHelper)
        {
            return new SMLocDAL(dbHelper);
        }
        public ISMLocDAL GetSMLocModuleDAL(EmDBType dbType)
        {
            return new SMLocDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMMatTrans
        public ISMMatTransDAL GetSMMatTransModuleDAL(DBHelper dbHelper)
        {
            return new SMMatTransDAL(dbHelper);
        }
        public ISMMatTransDAL GetSMMatTransModuleDAL(EmDBType dbType)
        {
            return new SMMatTransDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMMaterial
        public ISMMaterialDAL GetSMMaterialModuleDAL(DBHelper dbHelper)
        {
            return new SMMaterialDAL(dbHelper);
        }
        public ISMMaterialDAL GetSMMaterialModuleDAL(EmDBType dbType)
        {
            return new SMMaterialDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMMatInv
        public ISMMatInvDAL GetSMMatInvModuleDAL(DBHelper dbHelper)
        {
            return new SMMatInvDAL(dbHelper);
        }
        public ISMMatInvDAL GetSMMatInvModuleDAL(EmDBType dbType)
        {
            return new SMMatInvDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMInspSpec
        public ISMInspSpecDAL GetSMInspSpecModuleDAL(DBHelper dbHelper)
        {
            return new SMInspSpecDAL(dbHelper);
        }
        public ISMInspSpecDAL GetSMInspSpecModuleDAL(EmDBType dbType)
        {
            return new SMInspSpecDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region GetSMFixtureInspHeaderModuleDAL
        public ISMFixtureInspHeaderDAL GetSMFixtureInspHeaderModuleDAL(DBHelper dbHelper)
        {
            return new SMFixtureInspHeaderDAL(dbHelper);
        }
        public ISMFixtureInspHeaderDAL GetSMFixtureInspHeaderModuleDAL(EmDBType dbType)
        {
            return new SMFixtureInspHeaderDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMPI
        public ISMPIDAL GetSMPIModuleDAL(DBHelper dbHelper)
        {
            return new SMPIDAL(dbHelper);
        }
        public ISMPIDAL GetSMPIModuleDAL(EmDBType dbType)
        {
            return new SMPIDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMJobDataRecords
        public ISMJobDataRecordsDAL GetSMJobDataRecordsDAL(DBHelper dbHelper)
        {
            return new SMJobDataRecordsDAL(dbHelper);
        }
        public ISMJobDataRecordsDAL GetSMJobDataRecordsDAL(EmDBType dbType)
        {
            return new SMJobDataRecordsDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMFixtureTrans
        public ISMFixtureTransDAL GetSMFixtureTransDAL(DBHelper dbHelper)
        {
            return new SMFixtureTransDAL(dbHelper);
        }
        public ISMFixtureTransDAL GetSMFixtureTransDAL(EmDBType dbType)
        {
            return new SMFixtureTransDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMSolderPasteGlue
        public ISMSolderPasteGlueDAL GetSMSolderPasteGlueDAL(DBHelper dbHelper)
        {
            return new SMSolderPasteGlueDAL(dbHelper);
        }
        public ISMSolderPasteGlueDAL GetSMSolderPasteGlueDAL(EmDBType dbType)
        {
            return new SMSolderPasteGlueDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMMSD
        public ISMMSDDAL GetSMMSDDAL(DBHelper dbHelper)
        {
            return new SMMSDDAL(dbHelper);
        }
        public ISMMSDDAL GetSMMSDDAL(EmDBType dbType)
        {
            return new SMMSDDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMFixtureVerification
        public ISMFixtureVerificationDAL GetSMFixtureVerificationDAL(DBHelper dbHelper)
        {
            return new SMFixtureVerificationDAL(dbHelper);
        }
        public ISMFixtureVerificationDAL GetSMFixtureVerificationDAL(EmDBType dbType)
        {
            return new SMFixtureVerificationDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMFeeder
        public ISMFeederDAL GetSMFeederDAL(DBHelper dbHelper)
        {
            return new SMFeederDAL(dbHelper);
        }
        public ISMFeederDAL GetSMFeederDAL(EmDBType dbType)
        {
            return new SMFeederDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region SMS
        public ISMSDAL GetSMSDAL(DBHelper dbHelper)
        {
            return new SMSDAL(dbHelper);
        }
        public ISMSDAL GetSMSDAL(EmDBType dbType)
        {
            return new SMSDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region EPMEvent
        public IEPMEventDAL GetEPMEventDAL(DBHelper dbHelper)
        {
            return new EPMEventDAL(dbHelper);
        }
        public IEPMEventDAL GetEPMEventDAL(EmDBType dbType)
        {
            return new EPMEventDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #endregion

        #region Quality
        public IWIPUnitByDJDAL GetWIPUnitByDJModuleDAL(DBHelper dbHelper)
        {
            return new WIPUnitByDJDAL(dbHelper);
        }
        public IWIPUnitByDJDAL GetWIPUnitByDJModuleDAL(EmDBType dbType)
        {
            return new WIPUnitByDJDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public IWIPUnitBySNDAL GetWIPUnitBySNModuleDAL(DBHelper dbHelper)
        {
            return new WIPUnitBySNDAL(dbHelper);
        }
        public IWIPUnitBySNDAL GetWIPUnitBySNModuleDAL(EmDBType dbType)
        {
            return new WIPUnitBySNDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public ISMTQCDataDAL GetSMTQCDataModuleDAL(DBHelper dbHelper)
        {
            return new SMTQCDataDAL(dbHelper);
        }
        public ISMTQCDataDAL GetSMTQCDataModuleDAL(EmDBType dbType)
        {
            return new SMTQCDataDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        public IUploadCiscoDataDAL GetUploadCiscoDataModuleDAL(DBHelper dbHelper)
        {
            return new UploadCiscoDataDAL(dbHelper);
        }
        public IUploadCiscoDataDAL GetUploadCiscoDataModuleDAL(EmDBType dbType)
        {
            return new UploadCiscoDataDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }


        #endregion

        #region WM

        #region iProAMLvseTrace
        public IiProAMLvseTraceDAL GetiProAMLvseTraceDAL(DBHelper dbHelper)
        {
            return new iProAMLvseTraceDAL(dbHelper);
        }
        public IiProAMLvseTraceDAL GetiProAMLvseTraceDAL(EmDBType dbType)
        {
            return new iProAMLvseTraceDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion
        #region InvOptimization
        public IInvOptimizationDAL GetInvOptimizationDAL(DBHelper dbHelper)
        {
            return new InvOptimizationDAL(dbHelper);
        }
        public IInvOptimizationDAL GetInvOptimizationDAL(EmDBType dbType)
        {
            return new InvOptimizationDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region OnHandComp
        public IOnHandCompDAL GetOnHandCompDAL(DBHelper dbHelper)
        {
            return new OnHandCompDAL(dbHelper);
        }
        public IOnHandCompDAL GetOnHandCompDAL(EmDBType dbType)
        {
            return new OnHandCompDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region LabelInfo
        public ILabelInfoDAL GetLabelInfoDAL(DBHelper dbHelper)
        {
            return new LabelInfoDAL(dbHelper);
        }
        public ILabelInfoDAL GetLabelInfoDAL(EmDBType dbType)
        {
            return new LabelInfoDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion


        #region SearchCLID
        public ISearchCLIDDAL GetSearchCLIDDAL(DBHelper dbHelper)
        {
            return new SearchCLIDDAL(dbHelper);
        }
        public ISearchCLIDDAL GetSearchCLIDDAL(EmDBType dbType)
        {
            return new SearchCLIDDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion


        #region ComponentsUsed
        public IComponentsUsedDAL GetComponentsUsedDAL(DBHelper dbHelper)
        {
            return new ComponentsUsedDAL(dbHelper);
        }

        public IComponentsUsedDAL GetComponentsUsedDAL(EmDBType dbType)
        {
            return new ComponentsUsedDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion

        #region eJITBuildPlan
        public IeJITBuildPlanDAL GeteJITBuildPlanDAL(DBHelper dbHelper)
        {
            return new eJITBuildPlanDAL(dbHelper);
        }
        public IeJITBuildPlanDAL GeteJITBuildPlanDAL(EmDBType dbType)
        {
            return new eJITBuildPlanDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion



        #region AutoeJITPlan
        public IAutoeJITPlanDAL GetAutoeJITPlanDAL(DBHelper dbHelper)
        {
            return new AutoeJITPlanDAL(dbHelper);
        }
        public IAutoeJITPlanDAL GetAutoeJITPlanDAL(EmDBType dbType)
        {
            return new AutoeJITPlanDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion


        #region eJITFrequency
        public IeJITFrequencyDAL GeteJITFrequencyDAL(DBHelper dbHelper)
        {
            return new eJITFrequencyDAL(dbHelper);
        }
        public IeJITFrequencyDAL GeteJITFrequencyDAL(EmDBType dbType)
        {
            return new eJITFrequencyDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion


        #region IPPException
        public IIPPExceptionDAL GetIPPExceptionDAL(DBHelper dbHelper)
        {
            return new IPPExceptionDAL(dbHelper);
        }
        public IIPPExceptionDAL GetIPPExceptionDAL(EmDBType dbType)
        {
            return new IPPExceptionDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion


        #region PickOrder
        public IPickOrderDAL GetPickOrderDAL(DBHelper dbHelper)
        {
            return new PickOrderDAL(dbHelper);
        }
        public IPickOrderDAL GetPickOrderDAL(EmDBType dbType)
        {
            return new PickOrderDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion


        #region RawMatPacking
        public IRawMatPackingDAL GetRawMatPackingDAL(DBHelper dbHelper)
        {
            return new RawMatPackingDAL(dbHelper);
        }
        public IRawMatPackingDAL GetRawMatPackingDAL(EmDBType dbType)
        {
            return new RawMatPackingDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion


        #region RawMatPakStatus
        public IRawMatPakStatusDAL GetRawMatPakStatusDAL(DBHelper dbHelper)
        {
            return new RawMatPakStatusDAL(dbHelper);
        }
        public IRawMatPakStatusDAL GetRawMatPakStatusDAL(EmDBType dbType)
        {
            return new RawMatPakStatusDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion


        #region FGAging
        public IFGAgingDAL GetFGAgingDAL(DBHelper dbHelper)
        {
            return new FGAgingDAL(dbHelper);
        }
        public IFGAgingDAL GetFGAgingDAL(EmDBType dbType)
        {
            return new FGAgingDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion


        #region SOShipment
        public ISOShipmentDAL GetSOShipmentDAL(DBHelper dbHelper)
        {
            return new SOShipmentDAL(dbHelper);
        }
        public ISOShipmentDAL GetSOShipmentDAL(EmDBType dbType)
        {
            return new SOShipmentDAL(DBManagerConfig.Instance.GetDbHelper(dbType));
        }
        #endregion


        #endregion

    }
}
