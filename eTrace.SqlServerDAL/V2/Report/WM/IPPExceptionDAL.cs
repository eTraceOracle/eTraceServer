using eTrace.Common;
using eTrace.Report.IDAL;
using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.SqlServerDAL.V2.Report
{
    public class IPPExceptionDAL : DALBase, IIPPExceptionDAL
    {                                                    
        #region corts

        public IPPExceptionDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public IPPExceptionDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_IPPException_select = @"exec ora_ippexceptionrpt_V2 ";

        #endregion

        #region methods

        #region methods GetIPPExceptionDatas

        private string GetReportSqlCondition(ReportIPPExceptionModelQuery query, string GetRowCount = "N")
        {
            string sql = string.Empty;
            string condition1 = string.Empty;

            if (query != null)
            {

                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                   condition1 = " '" + query.OrgCode + "' ";
                }


                if (!string.IsNullOrEmpty(query.Floor))
                {
                    condition1 += ",'" + query.Floor + "'";
                }
                else
                {
                    condition1 += "," + "''";
                }


                if (!string.IsNullOrEmpty(query.DJ))
                {
                    condition1 += ",'" + query.DJ + "'";
                }
                else
                {
                    condition1 += "," + "''";
                }

                if (!string.IsNullOrEmpty(query.Model))
                {
                    condition1 += ",'" + query.Model + "'";
                }
                else
                {
                    condition1 += "," + "''";
                }


                if (query.ExceptionOnly)    //  True                
                {
                    condition1 += ",1";
                }
                else
                {
                    condition1 += ",0";
                }


                int CurrPage = 0;
                int PageSize = 0;
                if (query.Pager != null)
                {
                    CurrPage = (query.Pager.CurrentPage - 1) * query.Pager.PageSize;
                    PageSize = query.Pager.PageSize;
                }

                condition1 += ", " + CurrPage + ", " + PageSize + ",'" + GetRowCount + "'";


                //sql = string.Format("exec ora_ippexceptionrpt_V2  '{0}','{1}','{2}','{3}' ", condition1, CurrPage, PageSize, GetRowCount);

                sql = sql_IPPException_select + condition1;
            }

            return sql;
        }

        private ReportIPPExceptionModel.Item GetReportIPPExceptionModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportIPPExceptionModel.Item data = new ReportIPPExceptionModel.Item

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                ProdFloor = DBConvert.DB2String(dr["ProdFloor"]),
                DJ = DBConvert.DB2String(dr["DJ"]),
                Model = DBConvert.DB2String(dr["Model"]),
                Start_Qty = DBConvert.DB2Decimal(dr["Start_Qty"]),
                Sum_IPPQty = DBConvert.DB2Decimal(dr["Sum_IPPQty"]),
                Difference_Qty = DBConvert.DB2Decimal(dr["Difference_Qty"]),
            };
            return data;
        }



        /// <summary>
        /// 按照条件查找 Data，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReportIPPExceptionModel.Item> GetIPPExceptionData(ReportIPPExceptionModelQuery query)
        {
            string sql = GetReportSqlCondition(query);
            return dbHelper.GetList<ReportIPPExceptionModel.Item>(sql, GetReportIPPExceptionModel);
        }


        /// <summary>
        /// 按照条件查找 Data，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReportIPPExceptionModel.Item> GetIPPExceptionByPage(ReportIPPExceptionModelQuery query)
        {
            string sql = GetReportSqlCondition(query);
            return dbHelper.GetList<ReportIPPExceptionModel.Item>(sql, GetReportIPPExceptionModel);
        }


        /// <summary>
        /// 获取查询结果行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long IPPExceptionDataGetRowCount(ReportIPPExceptionModelQuery query)
        {
            string GetRowCount = "Y";
            string sql = GetReportSqlCondition(query, GetRowCount);
            //long rowCount = dbHelper.GetPageCount(GetSQLCount(sql));

            long rowCount = dbHelper.GetCount(sql);

            return rowCount;
        }

        #endregion

        #endregion
    }
}
