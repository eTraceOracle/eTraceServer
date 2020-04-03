using eTrace.Common;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.SqlServerDAL.V2.Report
{ 
    public class WIPStatusSummaryDAL : DALBase, eTrace.Report.IDAL.IWIPStatusSummaryDAL
    {
        private const string C_BASESQL = @"SELECT ISNULL(T_ProductRouting.SeqNo, 99999) AS SeqNo, 
                            dest.InvOrg, 
                            dest.Model, 
                            dest.PCBA,
                            dest.TVA, 
                            CASE WHEN COALESCE (dest.ProdLine, '''') = '''' THEN '''' ELSE dest.ProdLine END AS ProdLine, 
                                                     dest.CurrentProcess, 
                            dest.Result, 
                            ISNULL(sour.PCBA, '''') AS MBPCBA, 
                            COUNT(dest.Result) AS Count, 
                                dest.DJ, dest.JobID
                    FROM   T_WIPHeader AS dest WITH (nolock) LEFT OUTER JOIN
                         T_WIPHeader AS sour WITH (NOLOCK) ON dest.MotherBoardSN = sour.WIPID LEFT OUTER JOIN
                         T_ProductRouting WITH (NOLOCK) ON dest.Model = T_ProductRouting.Model AND dest.PCBA = T_ProductRouting.PCBA AND 
                         dest.CurrentProcess = T_ProductRouting.Process  WHERE 1=1   {0}  GROUP BY  T_ProductRouting.SeqNo, 
                                            dest.InvOrg, 
                                            dest.Model, 
                                            dest.PCBA, 
                                            dest.DJ, 
                                            dest.JobID, 
                                            dest.TVA,
                                            CASE WHEN COALESCE (dest.ProdLine, '''') = '''' THEN '''' ELSE dest.ProdLine END, 
                                            dest.CurrentProcess, 
                                            dest.Result, 
                                            sour.PCBA";
        private const string C_ORDERBY = @" dest.Model, dest.PCBA, dest.DJ, dest.JobID, ProdLine, SeqNo, dest.Result DESC";

        public WIPStatusSummaryDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public WIPStatusSummaryDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        /// <summary>
        /// 获取查询结果行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long GetTotalCount(GetWipStatusQuery.Item queryItem)
        {
            string sql =   GetSQLCount(string.Format(C_BASESQL, GetSqlCondition(queryItem)));

            long rowCount = dbHelper.GetCount(sql);
            return rowCount;
        }

        /// <summary>
        /// 按照条件查找锁数据，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<WipStatusSummaryItem> GetData(GetWipStatusQuery query)
        {
            string sql = string.Format(C_BASESQL, GetSqlCondition(query.Data));
            string orderBy = string.Empty;


            string sqlData = GetSQLData(sql, C_ORDERBY);
            return dbHelper.GetList<WipStatusSummaryItem>(sqlData, GetItemFromDataReader);


        }
        /// <summary>
        /// 按照条件查找锁数据，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<WipStatusSummaryItem> GetDataByPage(GetWipStatusQuery query)
        {
             
            string sql = string.Format(C_BASESQL, GetSqlCondition(query.Data));
            if (query.Pager!=null)
            {
                query.Pager.Order = C_ORDERBY;
            }
             string sqlDataByPage=GetSQLDataByPage(sql, query.Pager);
            return    dbHelper.GetList<WipStatusSummaryItem>(sqlDataByPage, GetItemFromDataReader);
                   
        }
        /// <summary>
        /// 从dataReader读取数据到 Item
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private WipStatusSummaryItem GetItemFromDataReader(System.Data.SqlClient.SqlDataReader dr)
        {
            WipStatusSummaryItem data = new WipStatusSummaryItem
            {
                Count= DBConvert.DB2Int(dr["Count"]),
                CurrentProcess= DBConvert.DB2String(dr["CurrentProcess"]),
                DJ= DBConvert.DB2String(dr["DJ"]),
                JobID = DBConvert.DB2String(dr["JobID"]),
                MBPCBA = DBConvert.DB2String(dr["MBPCBA"]),
                Model = DBConvert.DB2String(dr["Model"]),
                PCBA = DBConvert.DB2String(dr["PCBA"]),
                ProdLine = DBConvert.DB2String(dr["ProdLine"]),
                Result = DBConvert.DB2String(dr["Result"]),
                TVA = DBConvert.DB2String(dr["TVA"])
                
            };
            return data;
        }
        private string GetSqlCondition(GetWipStatusQuery.Item queryItem)
        {
            string sql = string.Empty;
            if (queryItem != null)
            {
                if (!string.IsNullOrEmpty(queryItem.Station) )
                {
                    sql += string.Format("AND dest.CurrentProcess = '{0}' ", queryItem.Station);
                }
                if (!string.IsNullOrEmpty(queryItem.ORG))
                {
                    sql += string.Format("AND dest.InvOrg = '{0}' ", queryItem.ORG);
                }
                if (!string.IsNullOrEmpty(queryItem.DiscreteJob))
                {
                    if (queryItem.DiscreteJob.Contains("*"))
                    {
                        sql += string.Format("AND dest.DJ like '{0}' ", queryItem.DiscreteJob.Replace("*", "%"));
                    }
                    else
                    {
                        sql += string.Format("AND dest.DJ IN ('{0}') ", queryItem.DiscreteJob.Replace(",", "','"));
                    }

                }
                if (!string.IsNullOrEmpty(queryItem.DiscreteJob))
                {
                    if (queryItem.DiscreteJob.Contains("*"))
                    {
                        sql += string.Format("AND dest.DJ like '{0}' ", queryItem.DiscreteJob.Replace("*", "%"));
                    }
                    else
                    {
                        sql += string.Format("AND dest.DJ IN ('{0}') ", queryItem.DiscreteJob.Replace(",", "','"));
                    }

                }
                if (!string.IsNullOrEmpty(queryItem.IntSN))
                {
                    if (queryItem.IntSN.Contains("*"))
                    {
                        sql += string.Format("AND dest.IntSN like '{0}' ", queryItem.IntSN.Replace("*", "%"));
                    }
                    else
                    {
                        sql += string.Format("AND dest.IntSN IN ('{0}') ", queryItem.IntSN.Replace(",", "','"));
                    }

                }
                if (!string.IsNullOrEmpty(queryItem.MotherBoardSN))
                {
                    if (queryItem.IntSN.Contains("*"))
                    {
                        sql += string.Format("AND sour.IntSN LIKE ('{0}') OR dest.IntSN LIKE ('{0}')  ", queryItem.MotherBoardSN.Replace("*", "%"));
                    }
                    else
                    {
                        sql += string.Format("AND sour.IntSN IN ('{0}')  OR dest.IntSN IN ('{0}')  ", queryItem.MotherBoardSN.Replace(",", "','"));
                    }

                }
                if (!string.IsNullOrEmpty(queryItem.PanelID))
                {
                    if (queryItem.PanelID.Contains("*"))
                    {
                        sql += string.Format("AND dest.PanelID like '{0}' ", queryItem.PanelID.Replace("*", "%"));
                    }
                    else
                    {
                        sql += string.Format("AND dest.PanelID IN ('{0}') ", queryItem.PanelID.Replace(",", "','"));
                    }

                }
                if (!string.IsNullOrEmpty(queryItem.TVA))
                {
                    if (queryItem.TVA.Contains("*"))
                    {
                        sql += string.Format("AND dest.TVA like '{0}' ", queryItem.TVA.Replace("*", "%"));
                    }
                    else
                    {
                        sql += string.Format("AND dest.TVA IN ('{0}') ", queryItem.TVA.Replace(",", "','"));
                    }

                }
                if (!string.IsNullOrEmpty(queryItem.Model))
                {
                    if (queryItem.Model.Contains("*"))
                    {
                        sql += string.Format("AND dest.Model like '{0}' ", queryItem.Model.Replace("*", "%"));
                    }
                    else
                    {
                        sql += string.Format("AND dest.Model IN ('{0}') ", queryItem.Model.Replace(",", "','"));
                    }

                }
                if (!string.IsNullOrEmpty(queryItem.PCBA))
                {
                    if (queryItem.PCBA.Contains("*"))
                    {
                        sql += string.Format("AND dest.PCBA like '{0}' ", queryItem.PCBA.Replace("*", "%"));
                    }
                    else
                    {
                        sql += string.Format("AND dest.PCBA IN ('{0}') ", queryItem.PCBA.Replace(",", "','"));
                    }

                }
                
          
            }
            return sql;
        }


        /// <summary>
        /// get total count for summary
        /// </summary>
        /// <param name="queryItem"></param>
        /// <returns></returns>
        public long GetSummaryCount(GetWipStatusQuery.Item queryItem)
        {
            string sqlQuery = string.Format(C_BASESQL, GetSqlCondition(queryItem));
            string sqlSumTemp = @"select sum(count) from ({0}) as t"; 
            string sql= string.Format(sqlSumTemp, sqlQuery);
             return  dbHelper.GetCount(sql);
        }
    }
}
