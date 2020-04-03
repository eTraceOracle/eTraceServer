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
    public class ResourceBLL : eTraceBLLBase<ResourceBLL, IResourceModuleBLL>, IResourceModuleBLL
    {
        private IResourceDAL resourceModuleDal = null;
        public ResourceBLL()
        {
            resourceModuleDal = DBManager.Instance.GetResourceModuleDAL(EmDBType.eTraceConnection);
        }


        public ProductStationModel GetProductStation(ProductStationQuery query)
        {
            return resourceModuleDal.GetProductStation(query);
        }

       
    }
}
