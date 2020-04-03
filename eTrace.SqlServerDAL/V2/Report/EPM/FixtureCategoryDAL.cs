using eTrace.Common;
using eTrace.Report.IDAL;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.SqlServerDAL.V2.Report
{
    public class FixtureCategoryDAL : DALBase, IFixtureCategoryDAL
    {
       #region corts

        public FixtureCategoryDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public FixtureCategoryDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields
        private const string sql_fixtureCat_select = @" select Category,SubCategory from T_SMFixtureCat with (nolock)";
        #endregion

        public FixtureCategoryDBModel GetCategorys()
        {
            FixtureCategoryDBModel result = new FixtureCategoryDBModel();
            string sql = sql_fixtureCat_select;
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Data.Add(GetCategoryModel(dr));
            });
            return result;
        }

        private FixtureCategoryDBModel.Item GetCategoryModel(System.Data.SqlClient.SqlDataReader dr)
        {
            FixtureCategoryDBModel.Item data = new FixtureCategoryDBModel.Item() 
            {
                category = DBConvert.DB2String(dr["Category"]),
                subCategory = DBConvert.DB2String(dr["SubCategory"]),
            };
            return data;
        }
    }
}
