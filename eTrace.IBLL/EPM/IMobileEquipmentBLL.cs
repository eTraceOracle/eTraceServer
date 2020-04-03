using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IMobileEquipmentBLL
    {
        ReportMobileEquipmentModel GetMobileEquipmentData(ReportMobileEquipmentQuery query);

        long EquipmentDataGetRowCount(ReportMobileEquipmentQuery query);
        bool InsertMobileItem(ReportMobileEquipmentQuery reportEquipmentMobile);
    }
}
