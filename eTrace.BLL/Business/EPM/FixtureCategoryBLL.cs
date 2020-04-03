using eTrace.Common;
using eTrace.Core;
using eTrace.Report.IBLL;
using eTrace.Report.IDAL;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.BLL.Business
{
    public class FixtureCategoryBLL : eTraceBLLBase<FixtureCategoryBLL, IFixtureCategoryModuleBLL>, IFixtureCategoryModuleBLL
    {

         private IFixtureCategoryDAL fixtureCatModuleDal = null;

         public FixtureCategoryBLL()
        {
            fixtureCatModuleDal = DBManager.Instance.GetEPMResourceModuleDAL(EmDBType.eTraceConnection);
        } 


        /// <summary>
        /// 获取类别二级联动
        /// </summary>
        /// <returns></returns>
        public FixtureCategoryModel GetCategorys()
        {
            FixtureCategoryModel result = new FixtureCategoryModel();
            FixtureCategoryDBModel dbModel=fixtureCatModuleDal.GetCategorys();
            List<FixtureCategoryDBModel.Item> list = dbModel.Data;
            HashSet<string> hs = new HashSet<string>();
            foreach(var item in list)
            {
                hs.Add(item.category);
            }
            foreach (var item in hs)
            {
                FixtureCategoryModel.Item fcmi=new FixtureCategoryModel.Item();
                fcmi.category = item;
                List<string> subCategorys = new List<string>();
                foreach (var model in list)
                {
                     if(item==model.category)
                     {
                        subCategorys.Add(model.subCategory);
                     }
     
                }
                fcmi.subCategory = subCategorys;
                result.Data.Add(fcmi);
            }
            return result;
        }
    }
}
