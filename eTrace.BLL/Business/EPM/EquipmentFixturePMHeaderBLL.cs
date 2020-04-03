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
    public class EquipmentFixturePMHeaderBLL : eTraceBLLBase<EquipmentFixturePMHeaderBLL, IEquipmentFixturePMHeaderModuleBLL>, IEquipmentFixturePMHeaderModuleBLL
    {
        private IEquipmentFixturePMHeaderDAL tdIEquipmentFixturePMHeaderDAL = null;
        public EquipmentFixturePMHeaderBLL()
        {
            tdIEquipmentFixturePMHeaderDAL = DBManager.Instance.GetEquipmentFixturePMHeaderModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportEquipmentFixturePMHeaderModel GetSMEquipmentFixturePMHeaderDatas(ReportEquipmentFixturePMHeaderQuery query)
        {
            return tdIEquipmentFixturePMHeaderDAL.GetSMEquipmentFixturePMHeaderDatas(query);
        }

        public ReportEquipmentFixturePMDetailModel GetSMEquipmentFixturePMDetailDatas(ReportEquipmentFixturePMHeaderQuery query)
        {
            return tdIEquipmentFixturePMHeaderDAL.GetSMEquipmentFixturePMDetailDatas(query);
        }
        public ReportEquipmentFixturePMItemModel GetSMEquipmentFixturePMItemDatas(ReportEquipmentFixturePMHeaderQuery query)
        {
            return tdIEquipmentFixturePMHeaderDAL.GetSMEquipmentFixturePMItemDatas(query);
        }
        public ReportEquipmentFixturePMMatModel GetSMEquipmentFixturePMMatDatas(ReportEquipmentFixturePMHeaderQuery query)
        {
            return tdIEquipmentFixturePMHeaderDAL.GetSMEquipmentFixturePMMatDatas(query);
        }

        public long EquipmentFixturePMHeaderDataGetRowCount(ReportEquipmentFixturePMHeaderQuery query)
        {
            return tdIEquipmentFixturePMHeaderDAL.EquipmentFixturePMHeaderDataGetRowCount(query);
        }

        public long EquipmentFixturePMDetailDataGetRowCount(ReportEquipmentFixturePMHeaderQuery query)
        {
            return tdIEquipmentFixturePMHeaderDAL.EquipmentFixturePMDetailDataGetRowCount(query);
        }

        public List<string> GetPMFrequency()
        {
            return tdIEquipmentFixturePMHeaderDAL.GetPMFrequency();
        }

    }
}
