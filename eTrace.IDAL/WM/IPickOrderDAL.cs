using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IPickOrderDAL
    {

        ReportPickOrderModel GetPickOrderData(ReportPickOrderModelQuery query);
                                  

        long PickOrderDataGetRowCount(ReportPickOrderModelQuery query);


        //Initialization SupplyType List
        List<string> GetSupplyType();
    }
}
