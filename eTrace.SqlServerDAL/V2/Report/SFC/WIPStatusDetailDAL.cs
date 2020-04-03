using eTrace.Common;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.SqlServerDAL.V2.Report
{ 
    public class WIPStatusDetailDAL : DALBase, eTrace.Report.IDAL.IWIPStatusDetailDAL
    {
        private const string C_BASESQL = @"SELECT   dest.WIPID, 
                                                    dest.IntSN, 
                                                    dest.Model, 
                                                    dest.PCBA, 
                                                    dest.DJ, 
                                                    dest.InvOrg, 
                                                    dest.ProdLine, 
                                                    dest.CurrentProcess, 
                                                    dest.Result, 
                                                    dest.AllPassed, 
							                        dest.MotherBoardSN, 
                                                    dest.JobID, 
                                                    dest.PanelID, 
                                                    dest.TopBottom, 
                                                    dest.ChangedOn, 
                                                    dest.ChangedBy,
                                                    dest.TVA, 
                                                    (case when ISNULL(sour.IntSN,'''') <> '''' THEN sour.PCBA else '''' end) AS MBPCBA, 
                                                    sour.IntSN AS MBIntSN, 
                                                    Isnull(heatsink.JobID, '''') as LoadedTo
							                FROM         T_WIPHeader AS dest WITH (nolock) LEFT OUTER JOIN
							                        T_WIPHeader AS sour WITH(NOLOCK) ON dest.MotherBoardSN = sour.WIPID
							                        LEFT OUTER JOIN T_SFSABatchInput AS heatsink WITH (nolock) ON 
                                            dest.WIPID = heatsink.ChildSAWIPID AND heatsink.Status = 1  where 1=1 ";
        private const string C_ORDERBY = @" ChangedOn ASC";
        public WIPStatusDetailDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public WIPStatusDetailDAL(EmDBType dbType)
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
            string sql = GetSQLCount( C_BASESQL + GetSqlCondition(queryItem));

            long rowCount = dbHelper.GetCount(sql);
            return rowCount;
        }

        /// <summary>
        /// 按照条件查找锁数据，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<WipStatusDetailItem> GetData(GetWipStatusQuery query)
        {
            string sql = C_BASESQL + GetSqlCondition(query.Data);
            string orderBy = string.Empty;


            string sqlData = GetSQLData(sql, C_ORDERBY);
            return dbHelper.GetList<WipStatusDetailItem>(sqlData, GetItemFromDataReader);


        }
        /// <summary>
        /// 按照条件查找锁数据，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<WipStatusDetailItem> GetDataByPage(GetWipStatusQuery query)
        {
             
            string sql = C_BASESQL + GetSqlCondition(query.Data);
            if (query.Pager!=null)
            {
                query.Pager.Order = C_ORDERBY;
            }
             string sqlDataByPage=GetSQLDataByPage(sql, query.Pager);
            return    dbHelper.GetList<WipStatusDetailItem>(sqlDataByPage, GetItemFromDataReader);
                   
        }
        /// <summary>
        /// 从dataReader读取数据到 Item
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private WipStatusDetailItem GetItemFromDataReader(System.Data.SqlClient.SqlDataReader dr)
        {
            WipStatusDetailItem data = new WipStatusDetailItem
            {
                ChangedBy= DBConvert.DB2String(dr["ChangedBy"]),
                ChangedOn= DBConvert.DB2Datetime(dr["ChangedOn"]),
                CurrentProcess= DBConvert.DB2String(dr["CurrentProcess"]),
                DJ= DBConvert.DB2String(dr["DJ"]),
                IntSN= DBConvert.DB2String(dr["IntSN"]),
                InvOrg= DBConvert.DB2String(dr["InvOrg"]),
                LoadedTo = DBConvert.DB2String(dr["LoadedTo"]),
                JobID = DBConvert.DB2String(dr["JobID"]),
                MBIntSN = DBConvert.DB2String(dr["MBIntSN"]),
                MBPCBA = DBConvert.DB2String(dr["MBPCBA"]),
                Model = DBConvert.DB2String(dr["Model"]),
                MotherBoardSN = DBConvert.DB2String(dr["MotherBoardSN"]),
                PanelID = DBConvert.DB2String(dr["PanelID"]),
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

                //if (!queryItem.IsGetDataByMotherBoradSN)
                //{
                    if (!string.IsNullOrEmpty(queryItem.Station))
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
                            sql += string.Format("AND dest.DJ like '{0}' ", queryItem.DiscreteJob.Trim().Replace("*", "%"));
                        }
                        else
                        {
                            sql += string.Format("AND dest.DJ IN ('{0}') ", queryItem.DiscreteJob.Trim().Replace(",", "','"));
                        }

                    }
                    if (!string.IsNullOrEmpty(queryItem.DiscreteJob))
                    {
                        if (queryItem.DiscreteJob.Contains("*"))
                        {
                            sql += string.Format("AND dest.DJ like '{0}' ", queryItem.DiscreteJob.Trim().Replace("*", "%"));
                        }
                        else
                        {
                            sql += string.Format("AND dest.DJ IN ('{0}') ", queryItem.DiscreteJob.Trim().Replace(",", "','"));
                        }

                    }
                    if (!string.IsNullOrEmpty(queryItem.IntSN))
                    {
                        if (queryItem.IntSN.Contains("*"))
                        {
                            sql += string.Format("AND dest.IntSN like '{0}' ", queryItem.IntSN.Trim().Replace("*", "%"));
                        }
                        else
                        {
                            sql += string.Format("AND dest.IntSN IN ('{0}') ", queryItem.IntSN.Trim().Replace(",", "','"));
                        }

                    }
                    if (!string.IsNullOrEmpty(queryItem.MotherBoardSN))
                    {
                        if (queryItem.IntSN.Contains("*"))
                        {
                            sql += string.Format("AND sour.IntSN LIKE ('{0}') OR dest.IntSN LIKE ('{0}')  ", queryItem.MotherBoardSN.Trim().Replace("*", "%"));
                        }
                        else
                        {
                            sql += string.Format("AND sour.IntSN IN ('{0}')  OR dest.IntSN IN ('{0}')  ", queryItem.MotherBoardSN.Trim().Replace(",", "','"));
                        }

                    }
                    if (!string.IsNullOrEmpty(queryItem.PanelID))
                    {
                        if (queryItem.PanelID.Contains("*"))
                        {
                            sql += string.Format("AND dest.PanelID like '{0}' ", queryItem.PanelID.Trim().Replace("*", "%"));
                        }
                        else
                        {
                            sql += string.Format("AND dest.PanelID IN ('{0}') ", queryItem.PanelID.Trim().Replace(",", "','"));
                        }

                    }
                    if (!string.IsNullOrEmpty(queryItem.TVA))
                    {
                        if (queryItem.TVA.Contains("*"))
                        {
                            sql += string.Format("AND dest.TVA like '{0}' ", queryItem.TVA.Trim().Replace("*", "%"));
                        }
                        else
                        {
                            sql += string.Format("AND dest.TVA IN ('{0}') ", queryItem.TVA.Trim().Replace(",", "','"));
                        }

                    }
                if (!string.IsNullOrEmpty(queryItem.Floor))
                {
                    if (queryItem.TVA.Contains("*"))
                    {
                        sql += string.Format("AND dest.ProdLine like '{0}' ", queryItem.Floor.Trim().Replace("*", "%"));
                    }
                    else
                    {
                        sql += string.Format("AND dest.ProdLine IN ('{0}') ", queryItem.Floor.Trim().Replace(",", "','"));
                    }

                }
                if (!string.IsNullOrEmpty(queryItem.Model))
                    {
                        if (queryItem.Model.Contains("*"))
                        {
                            sql += string.Format("AND dest.Model like '{0}' ", queryItem.Model.Trim().Replace("*", "%"));
                        }
                        else
                        {
                            sql += string.Format("AND dest.Model IN ('{0}') ", queryItem.Model.Trim().Replace(",", "','"));
                        }

                    }
                    if (!string.IsNullOrEmpty(queryItem.PCBA))
                    {
                        if (queryItem.PCBA.Contains("*"))
                        {
                            sql += string.Format("AND dest.PCBA like '{0}' ", queryItem.PCBA.Trim().Replace("*", "%"));
                        }
                        else
                        {
                            sql += string.Format("AND dest.PCBA IN ('{0}') ", queryItem.PCBA.Trim().Replace(",", "','"));
                        }
                    }
                //}
                //else
                //{
                //    if (!string.IsNullOrEmpty(queryItem.MotherBoardSN))
                //    {
                //        if (queryItem.MotherBoardSN.Contains("*"))
                //        {
                //            sql += string.Format("AND sour.IntSN LIKE ('{0}') OR dest.IntSN LIKE ('{0}') ", queryItem.PCBA.Trim().Replace("*", "%"));
                //        }
                //        else
                //        {
                //            sql += string.Format(" AND sour.IntSN IN ('{0}') OR dest.IntSN IN ('{0}') ", queryItem.PCBA.Trim().Replace(",", "','"));
                //        }

                //    }
                //}
     
                
          
            }
            return sql;
        }



    }
}
