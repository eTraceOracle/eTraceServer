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
    public class PickOrderBLL : eTraceBLLBase<PickOrderBLL, IPickOrderBLL>, IPickOrderBLL
    {
        private IPickOrderDAL tdIPickOrderDAL = null;
        public PickOrderBLL()
        {
            tdIPickOrderDAL = DBManager.Instance.GetPickOrderDAL(EmDBType.eTraceConnection);
        }

        public ReportPickOrderModel GetPickOrderData(ReportPickOrderModelQuery query)
        {
            return tdIPickOrderDAL.GetPickOrderData(query);
        }

        public long PickOrderDataGetRowCount(ReportPickOrderModelQuery query)
        {
            return tdIPickOrderDAL.PickOrderDataGetRowCount(query);
        }

        
        public List<string> GetSupplyType()
        {
            return tdIPickOrderDAL.GetSupplyType();
        }
    }
}
