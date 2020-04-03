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
using System.IO;

namespace eTrace.Report.SqlServerDAL.V2.Report
{
    public class WIPProductDAL : DALBase, IWIPProductDAL
    {
        #region corts

        public WIPProductDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public WIPProductDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_wipflow_select = @"SELECT wh.WIPID, wh.IntSN, wh.Model, wh.PCBA, wh.DJ, wh.InvOrg, wh.ProdLine, wh.CurrentProcess, wh.[Result], wh.AllPassed, wh.MotherBoardSN, wh.JobID, wh.PanelID, wh.ChangedOn, wh.ChangedBy,
wf.SeqNo, wf.Process, wf.Status, wf.TestRound, wf.FailedTest, wf.LastResult, wf.MaxTestRound, wf.MaxFailure
FROM T_WIPHeader wh with(nolock)
left JOIN T_WIPFlow wf with(nolock)
on wh.WIPID=wf.WIPID
WHERE wh.IntSN='{0}'";

        private const string sql_wipheader_select = @" select
            A.WIPID,
            A.IntSN,
            A.Model,
            A.PCBA,
            A.DJ,
            A.InvOrg,
            A.CurrentProcess,
            A.Result,
            B.IntSN as MotherBoardSN,
            A.JobID,
            A.ChangedOn,
            A.ChangedBy
            from T_WIPHeader  A   with(nolock)    
            left join T_WIPHeader B with(nolock) on A.MotherBoardSN =B.WIPID 
		    where A.IntSN ='{0}'";
        private const string sql_wipflowList_select = @"SELECT        
            wf.SeqNo, 
            wf.Process, 
            wf.Status, 
            wf.TestRound,
            wf.FailedTest,
            wf.LastResult,
            wf.MaxTestRound,
            wf.MaxFailure
            FROM T_WIPFlow wf with(nolock)
            left JOIN T_WIPHeader wh with(nolock)
            on wh.WIPID=wf.WIPID
            WHERE wh.IntSN='{0}'";
        private const string sql_wippropertyList_select = @"SELECT 
            wp.SeqNo, 
            wp.PropertyType, 
            wp.PropertyName,
            wp.InputType,   
            wp.PropertyValue,
            wp.ChangedOn,
            wp.ChangedBy
            FROM T_WIPProperties wp with(nolock)
            left JOIN T_WIPHeader wh with(nolock)
            on wh.WIPID=wp.WIPID
            WHERE wh.IntSN='{0}'";
        private const string sql_wiptdheaderList_select = @"SELECT 
            wt.ProcessName, 
            wt.SeqNo, 
            wt.ProdDate,
            wt.Result,   
            wt.OperatorName
            FROM T_WIPTDHeader wt with(nolock)
            left JOIN T_WIPHeader wh with(nolock)
            on wh.WIPID=wt.WIPID
            WHERE wh.IntSN='{0}'";

        private const string sql_wip_testdata_select = @"SELECT     T_WIPTDHeader.IntSerialNo, T_WIPTDHeader.Model, T_WIPTDHeader.PCBA, T_WIPTDHeader.ProcessName, T_WIPTDHeader.SeqNo, 
                      T_WIPTDHeader.ProdDate, T_WIPTDHeader.OperatorName, T_WIPTDHeader.TesterNo, T_WIPTDHeader.ProgramName, 
                      T_WIPTDHeader.ProgramRevision, T_WIPTDItem.TestStep, T_WIPTDItem.TestName, T_WIPTDItem.Result, T_WIPTDItem.OutputName, 
                      T_WIPTDItem.InputCondition, T_WIPTDItem.OutputLoading, T_WIPTDItem.LowerLimit, T_WIPTDItem.UpperLimit, T_WIPTDItem.Unit, 
                      T_WIPTDItem.Status, T_WIPTDItem.IPSReference, T_WIPTDItem.TestID
FROM         T_WIPTDHeader WITH (NOLOCK) INNER JOIN
                      T_WIPHeader WITH (NOLOCK) ON T_WIPTDHeader.WIPID = T_WIPHeader.WIPID INNER JOIN
                      T_WIPTDItem WITH (NOLOCK) ON T_WIPTDHeader.TDID = T_WIPTDItem.TDID where 1=1";

        private const string sql_wip_testdata_header = @"SELECT dest.IntSerialNo, dest.ProcessName, dest.SeqNo, dest.PO, dest.Model, dest.PCBA, dest.ProdDate, dest.WIPIn, dest.Result, 
                      dest.OperatorName, dest.TesterNo, dest.ProgramName, dest.ProgramRevision, dest.IPSNo, dest.IPSRevision, dest.Remark, (case when ISNULL(sour2.IntSN,'') <> '' THEN sour2.PCBA else '' end) AS MBPCBA, 
                      sour2.IntSN AS MBIntSN, dest.WIPID, sour.IntSN AS CurIntSN,DATEDIFF(SS, dest.WIPIn, dest.ProdDate) as TestCycleTime
FROM         T_WIPHeader AS sour WITH (nolock) INNER JOIN
                      T_WIPTDHeader AS dest WITH (nolock) ON sour.WIPID = dest.WIPID LEFT OUTER JOIN
                      T_WIPHeader AS sour2 WITH (nolock) ON sour.MotherBoardSN = sour2.WIPID 
                      WHERE     sour.WIPID IN (SELECT distinct WIPID FROM T_WIPTDHeader WITH (nolock) WHERE (T_WIPTDHeader.IntSerialNo IN ('{0}'))) 
                                OR sour.WIPID IN (SELECT distinct WIPID FROM T_WIPHeader WITH (nolock) WHERE (T_WIPHeader.IntSN IN ('{1}'))) group by dest.IntSerialNo,dest.ProcessName, dest.SeqNo, dest.PO, dest.Model, dest.PCBA, dest.ProdDate, dest.WIPIn, dest.Result,
                                    dest.OperatorName, dest.TesterNo, dest.ProgramName, dest.ProgramRevision, dest.IPSNo, dest.IPSRevision, dest.Remark, sour2.PCBA,sour2.IntSN , dest.WIPID, sour.IntSN ";
        //(T_WIPTDHeader.IntSerialNo IN ('{0}')))
        private const string sql_wip_testdata_header1 = @") OR sour.WIPID IN (SELECT distinct WIPID FROM T_WIPHeader WITH (nolock) WHERE 1=1";
        //(T_WIPHeader.IntSN IN ('{1}'))) group by dest.IntSerialNo,dest.ProcessName, dest.SeqNo, dest.PO, dest.Model, dest.PCBA, dest.ProdDate, dest.WIPIn, dest.Result, 
        //              dest.OperatorName, dest.TesterNo, dest.ProgramName, dest.ProgramRevision, dest.IPSNo, dest.IPSRevision, dest.Remark, sour2.PCBA, 
        //              sour2.IntSN , dest.WIPID, sour.IntSN";


        private const string sql_wip_testdata_properties = @"SELECT        dest.IntSN as IntSerialNo, dest.Model, T_WIPProperties.SeqNo, dest.PCBA, T_WIPProperties.PropertyType, T_WIPProperties.PropertyName, T_WIPProperties.InputType, 
                         T_WIPProperties.PropertyValue, T_WIPProperties.ChangedOn, T_WIPProperties.ChangedBy, sour.PCBA AS MotherBoard, sour.IntSN AS MotherBoardSN, 
                         T_WIPProperties.WIPID, T_WIPFlow.Process as ProcessName
FROM            T_WIPHeader AS dest WITH (NOLOCK) INNER JOIN
                         T_WIPProperties WITH (NOLOCK) ON dest.WIPID = T_WIPProperties.WIPID LEFT OUTER JOIN
                         T_WIPFlow WITH (NOLOCK) ON T_WIPProperties.SeqNo = T_WIPFlow.SeqNo AND T_WIPProperties.WIPID = T_WIPFlow.WIPID LEFT OUTER JOIN
                         T_WIPHeader AS sour WITH (NOLOCK) ON dest.MotherBoardSN = sour.WIPID
WHERE        (dest.IntSN IN ('{0}'))";
        #endregion

        #region methods

        public ReportWIPProductModel GetWIPUnitFlow(WIPUnitFlowQuery query)
        {
            ReportWIPProductModel result = new ReportWIPProductModel();
            string sql = string.Format(sql_wipflow_select, query.IntSN);
 
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }

            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Data.Add(GetWIPUnitFlowModel(dr));
            });
            return result;
        }

        private ReportWIPProductModel.Item GetWIPUnitFlowModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportWIPProductModel.Item data = new ReportWIPProductModel.Item
            {
                SeqNo = DBConvert.DB2Int(dr["SeqNo"]),
                IntSN = DBConvert.DB2String(dr["IntSN"]),
                PCBA = DBConvert.DB2String(dr["PCBA"]),
                DJ = DBConvert.DB2String(dr["DJ"]),
                FailedTest = DBConvert.DB2Int(dr["FailedTest"]),
                LastResult = DBConvert.DB2String(dr["LastResult"]),
                MaxFailure = DBConvert.DB2Int(dr["MaxFailure"]),
                MaxTestRound = DBConvert.DB2Int(dr["MaxTestRound"]),
                Process = DBConvert.DB2String(dr["Process"]),
                ProdLine = DBConvert.DB2String(dr["ProdLine"]),
                Status = DBConvert.DB2String(dr["Status"]),
                TestRound = DBConvert.DB2Int(dr["TestRound"]),
            };
            return data;
        }
        /// <summary>
        /// convert datareader to WIPHeaderModel
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private WIPHeaderModel GetWipHeader(System.Data.SqlClient.SqlDataReader dr)
        {
            WIPHeaderModel rt = new WIPHeaderModel()
            {
                WIPID = DBConvert.DB2String(dr["WIPID"]),
                ChangedBy =DBConvert.DB2String(dr["ChangedBy"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                CurrentProcess = DBConvert.DB2String(dr["CurrentProcess"]),
                DJ  = DBConvert.DB2String(dr["DJ"]),
                IntSN = DBConvert.DB2String(dr["IntSN"]),
                InvOrg = DBConvert.DB2String(dr["InvOrg"]),
                JobID = DBConvert.DB2String(dr["JobID"]),
                Model = DBConvert.DB2String(dr["Model"]),
                MotherBoardSN = DBConvert.DB2String(dr["MotherBoardSN"]),
                PCBA = DBConvert.DB2String(dr["PCBA"]),
                Result = DBConvert.DB2String(dr["Result"] ),
            };
            return rt;
        }
        private WIPFlowModel GetWIPFlow(System.Data.SqlClient.SqlDataReader dr)
        {
            WIPFlowModel rt = new WIPFlowModel()
            {
                FailedTest = DBConvert.DB2Int (dr["FailedTest"]),
                LastResult = DBConvert.DB2String(dr["LastResult"]),
                MaxFailure  = DBConvert.DB2Int (dr["MaxFailure"]),
                MaxTestRound  = DBConvert.DB2Int (dr["MaxTestRound"]),
                Process  = DBConvert.DB2String(dr["Process"]),
                SeqNo  = DBConvert.DB2Int(dr["SeqNo"]),
                Status  = DBConvert.DB2String(dr["Status"]),
                TestRound  = DBConvert.DB2Int(dr["TestRound"])
            };
            return rt;
        }
        private WIPPropertyModel GetWIPProperty(System.Data.SqlClient.SqlDataReader dr)
        {
            WIPPropertyModel rt = new WIPPropertyModel()
            {
                SeqNo = DBConvert.DB2Int(dr["SeqNo"]),
                ChangedBy  = DBConvert.DB2String(dr["ChangedBy"]),
                ChangedOn  = DBConvert.DB2Datetime(dr["ChangedOn"]),
                InputType  = DBConvert.DB2String(dr["InputType"]),
                PropertyName  = DBConvert.DB2String(dr["PropertyName"]),
                PropertyType  = DBConvert.DB2String(dr["PropertyType"]),
                PropertyValue  = DBConvert.DB2String(dr["PropertyValue"])
            };
            return rt;
        }

        private WIPTestDataModel GetWIPTestData(System.Data.SqlClient.SqlDataReader dr)
        {
            WIPTestDataModel rt = new WIPTestDataModel()
            {
                SeqNo = DBConvert.DB2Int(dr["SeqNo"]),
                OperatorName = DBConvert.DB2String(dr["OperatorName"]),
                ProcessName = DBConvert.DB2String(dr["ProcessName"]),
                ProdDate = DBConvert.DB2Datetime (dr["ProdDate"]),
                Result = DBConvert.DB2String(dr["Result"])
            };
            return rt;
        }
        public WIPHeaderModel GetWIPheaderByIntSN(string intSN)
        {
            WIPHeaderModel wipHeader = new WIPHeaderModel();
            string sql = string.Format(sql_wipheader_select, intSN);
            dbHelper.ExecuteReader(sql, (dr) =>
            {

                wipHeader = GetWipHeader(dr);
                //wipHeader = dbHelper.GetOject<WIPHeaderModel>(dr);
                //Console.WriteLine("GetWipHeader:"+(t2 - t1).ToString());
            });
     
            return wipHeader;
        }

        public List<WIPFlowModel> GetWIPFlowListByIntSN(string intSN)
        {
            string sql = string.Format(sql_wipflowList_select, intSN);
            
            return  dbHelper.GetList<WIPFlowModel>(sql,GetWIPFlow);
        }

        public List<WIPPropertyModel> GetWIPPropertyListByIntSN(string intSN)
        {
            string sql = string.Format(sql_wippropertyList_select, intSN);
            return  dbHelper.GetList<WIPPropertyModel>(sql, GetWIPProperty); 
        }

        public List<WIPTestDataModel> GetWIPTestDataListByIntSN(string intSN)
        {
            string sql = string.Format(sql_wiptdheaderList_select, intSN);
            return dbHelper.GetList<WIPTestDataModel>(sql,GetWIPTestData );
        }
        #endregion


        public ReportWIPTDHeaderModel GetWIPTDHeader(ReportWIPTestDataQuery query)
        {
            ReportWIPTDHeaderModel result = new ReportWIPTDHeaderModel();
            result.Data = new List<ReportWIPTDHeaderModel.Item>();
            string sql = sql_wip_testdata_header;
            #region Conditions
            sql = GetReportWIPTDHeaderSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportWIPTDheaderModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportWIPPropertiesModel GetWIPProperties(ReportWIPTestDataQuery query)
        {
            ReportWIPPropertiesModel result = new ReportWIPPropertiesModel();
            result.Data = new List<ReportWIPPropertiesModel.Item>();
            string sql = string.Empty;
            #region Conditions
            sql = GetWIPPropertiesSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportWIPPropertiesModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        private ReportWIPPropertiesModel.Item GetReportWIPPropertiesModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportWIPPropertiesModel.Item data = new ReportWIPPropertiesModel.Item
            {
                IntSerialNo = DBConvert.DB2String(dr["IntSerialNo"]),
                ProcessName = DBConvert.DB2String(dr["ProcessName"]),
                SeqNo = DBConvert.DB2Int(dr["SeqNo"]),
                Model = DBConvert.DB2String(dr["Model"]),
                PCBA = DBConvert.DB2String(dr["PCBA"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                PropertyType = DBConvert.DB2String(dr["PropertyType"]),
                PropertyName = DBConvert.DB2String(dr["PropertyName"]),
                InputType = DBConvert.DB2String(dr["InputType"]),
                PropertyValue = DBConvert.DB2String(dr["PropertyValue"]),
                MotherBoard = DBConvert.DB2String(dr["MotherBoard"]),
                MotherBoardSN = DBConvert.DB2String(dr["MotherBoardSN"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
            };
            return data;
        }


        private string GetReportWIPTDHeaderSqlCondition(ReportWIPTestDataQuery query)
        {
            string sql = string.Empty;
            //string sql1 = sql_wip_testdata_header1;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.IntSN))
                {
                    //sql += " and " + "T_WIPTDHeader.IntSerialNo IN ('" + query.IntSN.Replace(",", "','") + "') ";
                    //sql1 += " and " + "T_WIPHeader.IntSN IN ('" + query.IntSN.Replace(",", "','") + "') ";
                    sql = string.Format(sql_wip_testdata_header, query.IntSN.Replace(",", "','"), query.IntSN.Replace(",", "','"));
                }

            //    if (!string.IsNullOrEmpty(query.Model))
            //    {
            //        if (query.Model.Contains("*"))
            //        {
            //            sql += "and T_WIPTDHeader.Model like '" + query.Model.Replace("*", "%") + "' ";
            //            sql1 += "and T_WIPHeader.Model like '" + query.Model.Replace("*", "%") + "' ";
            //        }
            //        else
            //        {
            //            sql += "and T_WIPTDHeader.Model in ('" + query.Model.Replace(",", "','") + "') ";
            //            sql1 += " and " + "T_WIPHeader.Model IN ('" + query.Model.Replace(",", "','") + "') ";
            //        }
            //    }
            //    if (!string.IsNullOrEmpty(query.Station))
            //    {
            //        if (query.Station.Contains("*"))
            //        {
            //            sql += "and T_WIPTDHeader.ProcessName like '" + query.Station.Replace("*", "%") + "' ";
            //            sql1 += "and T_WIPHeader.CurrentProcess like '" + query.Station.Replace("*", "%") + "' ";
            //        }
            //        else
            //        {
            //            sql += "and T_WIPTDHeader.ProcessName in ('" + query.Station.Replace(",", "','") + "') ";
            //            sql1 += "and T_WIPHeader.CurrentProcess in ('" + query.Station.Replace(",", "','") + "') ";
            //        }
            //    }
            //    if (query.ProductTimeStart.HasValue)
            //    {
            //        sql += string.Format(" and T_WIPTDHeader.ProdDate>='{0}'", query.ProductTimeStart.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            //    }
            //    if (query.ProductTimeEnd.HasValue)
            //    {
            //        sql += string.Format(" and T_WIPTDHeader.ProdDate<'{0}'", query.ProductTimeEnd.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            //    }
            }
            //sql1 += ")";
            //sql += sql1;
            return sql;
        }

        private string GetWIPPropertiesSqlCondition(ReportWIPTestDataQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.IntSN))
                {
                    sql = string.Format(sql_wip_testdata_properties, query.IntSN.Replace(",", "','"));
                }

               
            }
            return sql;
        }

        private ReportWIPTDHeaderModel.Item GetReportWIPTDheaderModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportWIPTDHeaderModel.Item data = new ReportWIPTDHeaderModel.Item
            {
                IntSerialNo = DBConvert.DB2String(dr["IntSerialNo"]),
                ProcessName = DBConvert.DB2String(dr["ProcessName"]),
                SeqNo = DBConvert.DB2Int(dr["SeqNo"]),
                PO = DBConvert.DB2String(dr["PO"]),
                Model = DBConvert.DB2String(dr["Model"]),
                PCBA = DBConvert.DB2String(dr["PCBA"]),
                ProdDate = DBConvert.DB2Datetime(dr["ProdDate"]),
                WIPIn = DBConvert.DB2Datetime(dr["WIPIn"]),
                Result = DBConvert.DB2String(dr["Result"]),
                OperatorName = DBConvert.DB2String(dr["OperatorName"]),
                TesterNo = DBConvert.DB2String(dr["TesterNo"]),
                ProgramName = DBConvert.DB2String(dr["ProgramName"]),
                ProgramRevision = DBConvert.DB2String(dr["ProgramRevision"]),
                IPSNo = DBConvert.DB2String(dr["IPSNo"]),
                IPSRevision = DBConvert.DB2String(dr["IPSRevision"]),
                Remark = DBConvert.DB2String(dr["Remark"]),
                TestCycleTime = DBConvert.DB2Int(dr["TestCycleTime"]),
                MBPCBA = DBConvert.DB2String(dr["MBPCBA"]),
                MBIntSN= DBConvert.DB2String(dr["MBIntSN"]),
                CurIntSN= DBConvert.DB2String(dr["CurIntSN"])
            };
            return data;
        }

        public ReportWIPTestDataModel GetWIPTestData(ReportWIPTestDataQuery query)
        {
            ReportWIPTestDataModel result = new ReportWIPTestDataModel();
            result.Data = new List<ReportWIPTestDataModel.Item>();
            string sql = sql_wip_testdata_select;
            #region Conditions
            sql += GetReportWIPTestDataSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportWIPTestDataModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        private ReportWIPTestDataModel.Item GetReportWIPTestDataModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportWIPTestDataModel.Item data = new ReportWIPTestDataModel.Item
            {
                IntSerialNo = DBConvert.DB2String(dr["IntSerialNo"]),
                ProcessName = DBConvert.DB2String(dr["ProcessName"]),
                SeqNo = DBConvert.DB2Int(dr["SeqNo"]),
                TestStep = DBConvert.DB2String(dr["TestStep"]),
                Model = DBConvert.DB2String(dr["Model"]),
                PCBA = DBConvert.DB2String(dr["PCBA"]),
                ProdDate = DBConvert.DB2Datetime(dr["ProdDate"]),
                TesterNo = DBConvert.DB2String(dr["TesterNo"]),
                TestName = DBConvert.DB2String(dr["TestName"]),
                ProgramName = DBConvert.DB2String(dr["ProgramName"]),
                ProgramRevision = DBConvert.DB2String(dr["ProgramRevision"]),
                InputCondition= DBConvert.DB2String(dr["InputCondition"]),
                OutputLoading = DBConvert.DB2String(dr["OutputLoading"]),
                OutputName = DBConvert.DB2String(dr["OutputName"]),
                TestID = DBConvert.DB2String(dr["TestID"]),
                OperatorName = DBConvert.DB2String(dr["OperatorName"]),
                IPSReference = DBConvert.DB2String(dr["IPSReference"]),
                LowerLimit = DBConvert.DB2Double(dr["LowerLimit"]),
                Result = DBConvert.DB2Double(dr["Result"]),
                UpperLimit = DBConvert.DB2Double(dr["UpperLimit"]),
                Unit = DBConvert.DB2String(dr["Unit"]),
                
                Status = DBConvert.DB2String(dr["Status"])
               
            };

            return data;
        }


        private string GetReportWIPTestDataSqlCondition(ReportWIPTestDataQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.IntSN))
                {    
                    sql += " and " + "IntSN in ('" + query.IntSN.Replace(",", "','") + "') ";
                }

                if (!string.IsNullOrEmpty(query.Model))
                {
                    if (query.Model.Contains("*"))
                    {
                        sql += "and T_WIPTDHeader.Model like '" + query.Model.Replace("*", "%") + "' ";
                    }
                    else
                    {
                        sql += "and T_WIPTDHeader.Model in ('" + query.Model.Replace(",", "','") + "') ";
                    }
                }
                if (!string.IsNullOrEmpty(query.Station))
                {
                    if (query.Model.Contains("*"))
                    {
                        sql += "and T_WIPTDHeader.ProcessName like '" + query.Station.Replace("*", "%") + "' ";
                    }
                    else
                    {
                        sql += "and T_WIPTDHeader.ProcessName  in ('" + query.Station.Replace(",", "','") + "') ";
                    }
                }
                if (query.ProductTimeStart.HasValue)
                {
                    sql += string.Format(" and T_WIPTDHeader.ProdDate>='{0}'", query.ProductTimeStart.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (query.ProductTimeEnd.HasValue)
                {
                    sql += string.Format(" and T_WIPTDHeader.ProdDate<'{0}'", query.ProductTimeEnd.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            return sql;
        }

        public long ReportWIPTDHeaderGetRowCount(ReportWIPTestDataQuery query)
        {
            string sql = sql_wip_testdata_header;
            #region Conditions
            sql = GetReportWIPTDHeaderSqlCondition(query);
            #endregion
            string countSql = GetSQLCount(sql);
            long rowCount = dbHelper.GetCount(countSql);
            return rowCount;
        }

        public long ReportWIPTestDataGetRowCount(ReportWIPTestDataQuery query)
        {
            string sql = sql_wip_testdata_select;
            #region Conditions
            sql += GetReportWIPTestDataSqlCondition(query);
            #endregion
            string countSql = GetSQLCount(sql);
            long rowCount = dbHelper.GetCount(countSql);
            return rowCount;
        }
    }
}
