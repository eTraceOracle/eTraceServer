using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IEquipmentModuleBLL
    {
        ReportEquipmentModel GetEquipmentData(ReportEquipmentQuery query);

        //Initialization Equipment Status List
        List<string> GetEquipmentStatus();

        List<string> GetAllDepartment();

        long EquipmentDataGetRowCount(ReportEquipmentQuery query);
    }
}
