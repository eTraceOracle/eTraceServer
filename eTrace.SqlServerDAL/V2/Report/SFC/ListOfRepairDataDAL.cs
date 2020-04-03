using eTrace.Common;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.SqlServerDAL.V2.Report
{
    public class ListOfRepairDataDAL : DALBase, eTrace.Report.IDAL.IListOfRepairDataDAL
    {
        #region sqlBase
        private const string   sqlwip0 = " SELECT distinct A.Model, 'N/A' as Description, B.BusinessUnit, A.ProdOrder, T_POQty.OrgCode, A.ExtSerialNo, (case when ISNULL(A.ExtSerialNo,'') = '' then T_WIPHeader.IntSN else A.IntSerialNo end) as IntSerialNo, A.Floor, A.RepairerID,"
            + " convert(varchar(12),A.RepairDate,101) as RepairDate, convert(varchar(12),A.RepairDate,108) as RepairTime,"
            + " convert(varchar(12),A.FailTime,101) as FailDate, convert(varchar(12),A.FailTime,108) as FailTime,"
            + " A.ChangedBy,convert(varchar(12),A.ChangedOn,101) as ChangedOn, convert(varchar(12),A.ChangedOn,108) as ChangedTime,"
            + " datediff(minute,A.RepairDate,A.ChangedOn) as StayMinutes, A.TestStation, A.TestRound, A.SymptomCode, C.Description as SymptomCodeDesc, "
            + " A.Cause_Component as [DefectCode], D.Description as DefectCodeDesc, A.DefectCode_Component as [Cause],  "
            + " A.PriDefect, A.AssemblyPN, A.CompRefD, A.CompPN, A.CompSupplier,"
            + " A.OrgCompDateCode, A.CompLotNo, A.Comment, A.Status, A.ReturnToTestStep, A.RepairRound, A.CompDesc, A.Category, A.FailItem,"
            + " '' as Tester,'' as UpperLimit,'' as LowerLimit, '' as Result, '' as TestStep,A.BurnTime,A.AgingTime, A.RepSupplier, A.RepCompDateCode, A.RepLotNo, A.RepCLID, A.Marking, A.RepComment,"
            + " A.CauseType as SMTMaterial,"
            + " A.FAOperator,convert(varchar(12),A.FATime,101) + ' ' + convert(varchar(12),A.FATime,108) as FATime,A.ReworkOperator,convert(varchar(12),A.ReworkTime,101) + ' ' +convert(varchar(12),A.ReworkTime,108) as ReworkTime,A.AssAndQCOperator,convert(varchar(12),A.AssAndQCTime,101) + ' ' +convert(varchar(12),A.AssAndQCTime,108) as AssAndQCTime,A.OwnerShip "
            + " FROM "
            + " (SELECT T_RepHeader.WIPID, "
            + " T_RepHeader.Model, T_RepHeader.ProdOrder, T_RepHeader.ExtSerialNo, T_RepHeader.IntSerialNo, T_RepHeader.Floor, T_RepItem.RepairerID, "
            + " T_RepItem.FailTime, T_RepItem.RepairDate,T_RepItem.ChangedBy, T_RepItem.ChangedOn, T_RepItem.TestStation, T_RepItem.TestRound, T_RepItem.SymptomCode,  "
            + " T_RepReplacement.DefectCode as DefectCode_Component, T_RepReplacement.Cause as Cause_Component,  "
            + " T_RepItem.DefectCode as DefectCode_Unit, T_RepItem.Cause as Cause_Unit,  "
            + " isnull(ISNULL(T_RepReplacement.PriDefect,T_RepItem.PriDefect),1) AS PriDefect,  "
            + " ISNULL(T_RepReplacement.AssemblyPN,T_RepItem.AssemblyPN) AS AssemblyPN, ISNULL(T_RepReplacement.CompRefD,T_RepItem.CompRefD) AS CompRefD, "
            + " ISNULL(T_RepReplacement.CompPN,T_RepItem.CompPN) AS CompPN,ISNULL(T_RepReplacement.CompSupplier,T_RepItem.CompSupplier) AS CompSupplier, "
            + " ISNULL(T_RepReplacement.CompDateCode, T_RepItem.CompDateCode) as OrgCompDateCode, T_RepItem.Comment, T_RepItem.Status,  "
            + " T_RepItem.ReturnToTestStep, T_RepItem.Item AS RepairRound, T_RepReplacement.CompDesc, T_RepItem.Category, T_RepItem.FailItem, "
            + " T_RepItem.BurnTime, '' AS AgingTime,T_RepReplacement.RepSupplier as RepSupplier, T_RepReplacement.RepDateCode as RepCompDateCode, T_RepReplacement.RepLotNo, T_RepReplacement.CLID as RepCLID, T_RepReplacement.Marking, T_RepReplacement.RepComment, "
            + " T_RepReplacement.CompLotNo,T_RepReplacement.CauseType, "
            + " T_RepItem.FAOperator,T_RepItem.FATime,T_RepItem.ReworkOperator,T_RepItem.ReworkTime,T_RepItem.AssAndQCOperator,T_RepItem.AssAndQCTime,T_RepItem.OwnerShip "
            + " FROM T_RepHeader with (nolock) "
            + " INNER JOIN T_RepItem with (nolock) ON T_RepHeader.RepID = T_RepItem.RepID  "
            + " LEFT OUTER JOIN T_RepReplacement with (nolock) ON T_RepItem.RepID = T_RepReplacement.RepId AND T_RepItem.Item = T_RepReplacement.Item) AS A "
            + " LEFT OUTER JOIN T_WIPHeader with (nolock) ON A.WIPID = T_WIPHeader.WIPID "
            + " LEFT OUTER JOIN V_ProductMaster AS B with (nolock) ON A.Model = B.Model "
            + " LEFT OUTER JOIN T_POQty with (nolock) on A.ProdOrder = T_POQty.PO LEFT OUTER JOIN T_RepCodes as C with (nolock) on A.SymptomCode = C.Code and C.Category = 'SYMPTOM' LEFT OUTER JOIN T_RepCodes as D with (nolock) on A.Cause_Component = D.Code and D.Category = 'DEFECT' "
            + " WHERE 1=1  {0} ";

        private const string sqlwip1 = " SELECT distinct A.Model, 'N/A' as Description, B.BusinessUnit, A.ProdOrder, T_POQty.OrgCode, A.ExtSerialNo, (case when ISNULL(A.ExtSerialNo,'') = '' then T_WIPHeader.IntSN else A.IntSerialNo end) as IntSerialNo, A.Floor, A.RepairerID, "
                    + " convert(varchar(12),A.RepairDate,101) as RepairDate, convert(varchar(12),A.RepairDate,108) as RepairTime, "
                    + " convert(varchar(12),A.FailTime,101) as FailDate, convert(varchar(12),A.FailTime,108) as FailTime, "
                    + " A.ChangedBy,convert(varchar(12),A.ChangedOn,101) as ChangedOn, convert(varchar(12),A.ChangedOn,108) as ChangedTime, "
                    + " datediff(minute,A.RepairDate,A.ChangedOn) as StayMinutes, A.TestStation, A.TestRound, A.SymptomCode, C.Description as SymptomCodeDesc, "
                    + " A.Cause_Component as [DefectCode], D.Description as DefectCodeDesc, A.DefectCode_Component as [Cause],   "
                    + " A.PriDefect, A.AssemblyPN, A.CompRefD, A.CompPN, A.CompSupplier, "
                    + " A.OrgCompDateCode, A.CompLotNo, A.Comment, A.Status, A.ReturnToTestStep, A.RepairRound, A.CompDesc, A.Category, A.FailItem, "
                    + " '' as Tester, '' as UpperLimit,'' as LowerLimit, '' as Result, '' as TestStep,A.BurnTime,A.AgingTime, A.RepSupplier, A.RepCompDateCode, A.RepLotNo, A.RepCLID, A.Marking, A.RepComment, "
                    + " A.CauseType as SMTMaterial, "
                    + " A.FAOperator,convert(varchar(12),A.FATime,101) + ' ' + convert(varchar(12),A.FATime,108) as FATime,A.ReworkOperator,convert(varchar(12),A.ReworkTime,101) + ' ' +convert(varchar(12),A.ReworkTime,108) as ReworkTime,A.AssAndQCOperator,convert(varchar(12),A.AssAndQCTime,101) + ' ' +convert(varchar(12),A.AssAndQCTime,108) as AssAndQCTime,A.OwnerShip "
                    + " FROM "
                    + " (SELECT T_RepHeader.WIPID, "
                    + " T_RepHeader.Model, T_RepHeader.ProdOrder, T_RepHeader.ExtSerialNo, T_RepHeader.IntSerialNo, T_RepHeader.Floor, T_RepItem.RepairerID, "
                    + " T_RepItem.FailTime, T_RepItem.RepairDate,T_RepItem.ChangedBy, T_RepItem.ChangedOn, T_RepItem.TestStation, T_RepItem.TestRound, T_RepItem.SymptomCode,  "
                    + " T_RepReplacement.DefectCode as DefectCode_Component, T_RepReplacement.Cause as Cause_Component,  "
                    + " T_RepItem.DefectCode as DefectCode_Unit, T_RepItem.Cause as Cause_Unit,  "
                    + " isnull(ISNULL(T_RepReplacement.PriDefect,T_RepItem.PriDefect),1) AS PriDefect,  "
                    + " ISNULL(T_RepReplacement.AssemblyPN,T_RepItem.AssemblyPN) AS AssemblyPN, ISNULL(T_RepReplacement.CompRefD,T_RepItem.CompRefD) AS CompRefD, "
                    + " ISNULL(T_RepReplacement.CompPN,T_RepItem.CompPN) AS CompPN,ISNULL(T_RepReplacement.CompSupplier,T_RepItem.CompSupplier) AS CompSupplier, "
                    + " ISNULL(T_RepReplacement.CompDateCode, T_RepItem.CompDateCode) as OrgCompDateCode, T_RepItem.Comment, T_RepItem.Status,  "
                    + " T_RepItem.ReturnToTestStep, T_RepItem.Item AS RepairRound, T_RepReplacement.CompDesc, T_RepItem.Category, T_RepItem.FailItem, "
                    + " T_RepItem.BurnTime, ROUND(CAST(DATEDIFF(HOUR, T_RepItem.RepairDate, GETDATE()) AS FLOAT) / 24, 1) AS AgingTime, "
                    + " T_RepReplacement.RepSupplier AS RepSupplier, T_RepReplacement.RepDateCode as RepCompDateCode, T_RepReplacement.RepLotNo, T_RepReplacement.CLID as RepCLID, T_RepReplacement.Marking, T_RepReplacement.RepComment, "
                    + " T_RepReplacement.CompLotNo,T_RepReplacement.CauseType, "
                    + " T_RepItem.FAOperator,T_RepItem.FATime,T_RepItem.ReworkOperator,T_RepItem.ReworkTime,T_RepItem.AssAndQCOperator,T_RepItem.AssAndQCTime,T_RepItem.OwnerShip "
                    + " FROM T_RepHeader with (nolock) "
                    + " INNER JOIN T_RepItem with (nolock) ON T_RepHeader.RepID = T_RepItem.RepID  "
                    + " LEFT OUTER JOIN T_RepReplacement with (nolock) ON T_RepItem.RepID = T_RepReplacement.RepId AND T_RepItem.Item = T_RepReplacement.Item "
                    + " WHERE (T_RepHeader.ExtSerialNo='' OR T_RepHeader.ExtSerialNo IS NULL) "
                    + " ) AS A "
                    + " LEFT OUTER JOIN T_WIPHeader with (nolock) ON A.WIPID = T_WIPHeader.WIPID "
                    + " LEFT OUTER JOIN V_ProductMaster AS B with (nolock) ON A.Model = B.Model "
                    + " LEFT OUTER JOIN T_POQty on A.ProdOrder = T_POQty.PO LEFT OUTER JOIN T_RepCodes as C with (nolock) on A.SymptomCode = C.Code and C.Category = 'SYMPTOM' LEFT OUTER JOIN T_RepCodes as D with (nolock) on A.Cause_Component = D.Code and D.Category = 'DEFECT' "
                    + " WHERE 1=1   {0}  ";


        private const string C_ORDERBY = " A.Model, A.ProdOrder, OrgCode, RepairDate";
        #endregion
     
        public ListOfRepairDataDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public ListOfRepairDataDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        /// <summary>
        /// 获取查询结果行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long GetTotalCount(GetListOfRepairDataQuery.Item query)
        {
            string sql = GetSQLCount( GetSqlCondition(query));
            long rowCount = dbHelper.GetCount(sql);
            return rowCount;
        }

        /// <summary>
        /// 按照条件查找锁数据，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<GetListOfRepairDataModel.Item> GetListOfRepirData(GetListOfRepairDataQuery query)
        {
            string sql = GetSqlCondition(query.Data);
            string orderBy = string.Empty;

            string sqlData = GetSQLData(sql, C_ORDERBY);
            return dbHelper.GetList<GetListOfRepairDataModel.Item>(sqlData, GetItemFromDataReader);
        }

        /// <summary>
        /// 按照条件查找锁数据，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<GetListOfRepairDataModel.Item> GetListOfRepairDataByPage(GetListOfRepairDataQuery query)
        {
            //' order by   Status , LockedOn desc'
            string sql =  GetSqlCondition(query.Data);
            if (query.Pager!=null)
            {
                query.Pager.Order = C_ORDERBY;
                string sqlDataByPage = GetSQLDataByPage(sql, query.Pager);
                return dbHelper.GetList<GetListOfRepairDataModel.Item>(sqlDataByPage, GetItemFromDataReader);
            }else
            {
                return new List<GetListOfRepairDataModel.Item>();
            }
          
        }
        private List<string> GetWIPIDList(string IntSNListString)
        {
            List<string> wipidList = new List<string>();
            string sql = string.Format("SELECT WIPID FROM T_WIPHeader WITH (nolock) WHERE IntSN in ('{0}') ", IntSNListString);
            dbHelper.ExecuteReader(sql, x =>
            {
                wipidList.Add(DBConvert.DB2String(x["WIPID"]));
            });
            return wipidList;
        }
        /// <summary>
        /// 从dataReader读取数据到 Item
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private GetListOfRepairDataModel.Item GetItemFromDataReader(System.Data.SqlClient.SqlDataReader dr)
        {
            try
            {
                GetListOfRepairDataModel.Item data = new GetListOfRepairDataModel.Item
                {
                    Model = DBConvert.DB2String(dr["Model"]),
                    PriDefect = DBConvert.DB2Bool(dr["PriDefect"]),
                    Description = DBConvert.DB2String(dr["Description"]),
                    BusinessUnit = DBConvert.DB2String(dr["BusinessUnit"]),
                    ProdOrder = DBConvert.DB2String(dr["ProdOrder"]),
                    OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                    ExtSerialNo = DBConvert.DB2String(dr["ExtSerialNo"]),
                    IntSerialNo = DBConvert.DB2String(dr["IntSerialNo"]),
                    Floor = DBConvert.DB2String(dr["Floor"]),
                    RepairerID = DBConvert.DB2String(dr["RepairerID"]),
                    RepairDate = DBConvert.DB2String(dr["RepairDate"]),
                    RepairTime = DBConvert.DB2String(dr["RepairTime"]),
                    FailDate = DBConvert.DB2String(dr["FailDate"]),
                    FailTime = DBConvert.DB2String(dr["FailTime"]),
                    ChangedOn = DBConvert.DB2String(dr["ChangedOn"]),
                    ChangedTime = DBConvert.DB2String(dr["ChangedTime"]),
                    ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                    StayMinutes = DBConvert.DB2String(dr["StayMinutes"]),
                    TestStation = DBConvert.DB2String(dr["TestStation"]),
                    TestRound = DBConvert.DB2String(dr["TestRound"]),
                    SymptomCode = DBConvert.DB2String(dr["SymptomCode"]),
                    SymptomCodeDesc = DBConvert.DB2String(dr["SymptomCodeDesc"]),
                    DefectCode = DBConvert.DB2String(dr["DefectCode"]),
                    Cause = DBConvert.DB2String(dr["Cause"]),
                    AssemblyPN = DBConvert.DB2String(dr["AssemblyPN"]),
                    CompRefD = DBConvert.DB2String(dr["CompRefD"]),
                    CompPN = DBConvert.DB2String(dr["CompPN"]),
                    CompSupplier = DBConvert.DB2String(dr["CompSupplier"]),
                    OrgCompDateCode = DBConvert.DB2String(dr["OrgCompDateCode"]),
                    OrgCompLotNo = DBConvert.DB2String(dr["CompLotNo"]),
                    Comment = DBConvert.DB2String(dr["Comment"]),
                    Status = DBConvert.DB2String(dr["Status"]),
                    ReturnToTestStep = DBConvert.DB2String(dr["ReturnToTestStep"]),
                    RepairRound = DBConvert.DB2String(dr["RepairRound"]),
                    CompDesc = DBConvert.DB2String(dr["CompDesc"]),
                    Category = DBConvert.DB2String(dr["Category"]),
                    FailItem = DBConvert.DB2String(dr["FailItem"]),
                    Tester = DBConvert.DB2String(dr["Tester"]),
                    UpperLimit = DBConvert.DB2String(dr["UpperLimit"]),
                    LowerLimit = DBConvert.DB2String(dr["LowerLimit"]),
                    Result = DBConvert.DB2String(dr["Result"]),
                    TestStep = DBConvert.DB2String(dr["TestStep"]),
                    BurnTime = DBConvert.DB2String(dr["BurnTime"]),
                    AgingTime = DBConvert.DB2String(dr["AgingTime"]),
                    RepSupplier = DBConvert.DB2String(dr["RepSupplier"]),
                    RepCompDateCode = DBConvert.DB2String(dr["RepCompDateCode"]),
                    RepLotNo = DBConvert.DB2String(dr["RepLotNo"]),
                    RepCLID = DBConvert.DB2String(dr["RepCLID"]),
                    Marking = DBConvert.DB2String(dr["Marking"]),
                    RepComment = DBConvert.DB2String(dr["RepComment"]),
                    SMTMaterial = DBConvert.DB2String(dr["SMTMaterial"]),
                    FAOperator = DBConvert.DB2String(dr["FAOperator"]),
                    FATime = DBConvert.DB2String(dr["FATime"]),
                    DefectCodeDesc = DBConvert.DB2String(dr["DefectCodeDesc"]),
                    //PossibleDateCode = DBConvert.DB2String(dr["PossibleDateCode"]),
                    //PossibleLotNO = DBConvert.DB2String(dr["PossibleLotNO"]),
                    ReworkOperator = DBConvert.DB2String(dr["ReworkOperator"]),
                    ReworkTime = DBConvert.DB2String(dr["ReworkTime"]),
                    AssAndQCOperator = DBConvert.DB2String(dr["AssAndQCOperator"]),
                    AssAndQCTime = DBConvert.DB2String(dr["AssAndQCTime"]),
                    OwnerShip = DBConvert.DB2String(dr["OwnerShip"])

                };
                return data;
            }
            catch (Exception ex)
            {

                return null;
            }
        
        }
          
        private string GetSqlCondition(GetListOfRepairDataQuery.Item queryItem)
        {
            string sql = string.Empty;
            if (queryItem != null)
            {
                if (queryItem.FailTimeFrom.HasValue)
                {
                    sql += string.Format(" and A.FailTime>='{0}'", queryItem.FailTimeFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (queryItem.FailTimeTo.HasValue)
                {
                    sql += string.Format(" and A.FailTime<'{0}'", queryItem.FailTimeTo.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (queryItem.RepairDateFrom.HasValue)
                {
                    sql += string.Format(" and A.RepairDate>='{0}'", queryItem.RepairDateFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (queryItem.RepairDateTo.HasValue)
                {
                    sql += string.Format(" and RepairDate<'{0}'", queryItem.RepairDateTo.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (queryItem.FATimeFrom.HasValue)
                {
                    sql += string.Format(" and A.FATime>='{0}'", queryItem.FATimeFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (queryItem.FATimeTo.HasValue)
                {
                    sql += string.Format(" and A.FATime<'{0}'", queryItem.FATimeTo.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (queryItem.ChangeOnFrom.HasValue)
                {
                    sql += string.Format(" and A.ChangedOn>='{0}'", queryItem.ChangeOnFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (queryItem.ChangeOnTo.HasValue)
                {
                    sql += string.Format(" and A.ChangedOn<'{0}'", queryItem.ChangeOnTo.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                if (!string.IsNullOrEmpty(queryItem.Model))
                {
                    if (queryItem.Model.Contains("*"))
                    {
                        sql += "and a.Model  like '" + queryItem.Model.Replace("*", "%") + "' ";
                    }
                    else
                    {
                        sql += string.Format(" and  A.Model in ('{0}') ", queryItem.Model.Trim().Replace(",","','"));
                    }
                }
                if (!string.IsNullOrEmpty(queryItem.Floor))
                {
                    if (queryItem.Floor.Contains("*"))
                    {
                        sql += "and a.Floor  like '" + queryItem.Floor.Replace("*", "%") + "' ";
                    }
                    else
                    {
                        sql += string.Format(" and A.Floor in ('{0}')", queryItem.Floor.Trim().Replace(",", "','"));
                    }
                }
                if (!string.IsNullOrEmpty(queryItem.ExtSN))
                {
                    if (queryItem.ExtSN.Contains("*"))
                    {
                        sql += "and a.ExtSerialNo   like '" + queryItem.ExtSN.Replace("*", "%") + "' ";
                    }
                    else
                    {
                        sql += string.Format(" and A.ExtSerialNo in ('{0}')", queryItem.ExtSN.Trim().Replace(",", "','"));
                    }
                }
                if (!string.IsNullOrEmpty(queryItem.IntSN))
                {
                    //此处照搬旧逻辑
                    var wipIdList = GetWIPIDList(queryItem.IntSN);
                    if (wipIdList.Count==0)
                    {
                        sql += string.Format(" and A.IntSerialNo in ('{0}') ", queryItem.IntSN.Trim().Replace(",", "','"));
                    }else
                    {
                        //foreach (var wipid in wipIdList)
                        //{
                        //    sql += string.Format("and A.WIPID = '{0}' ",   queryItem.IntSN);
                        //}
                        sql += string.Format(" and A.WIPID  in ('{0}') ",string.Join(",", wipIdList));
                    } 
                }
                if (!string.IsNullOrEmpty(queryItem.ChangedBy))
                {
                    sql += string.Format(" and A.ChangedBy  in ('{0}')", queryItem.ChangedBy.Trim().Replace(",", "','"));
                }
                if (!string.IsNullOrEmpty(queryItem.Station))
                {
                    sql += string.Format(" and A.TestStation in ('{0}')", queryItem.Station.Trim().Replace(",", "','"));
                }
                if (!string.IsNullOrEmpty(queryItem.DefectCode))
                {
                    sql += string.Format(" and A.DefectCode in ('{0}')", queryItem.DefectCode.Trim().Replace(",", "','"));
                }
                if (!string.IsNullOrEmpty(queryItem.Cause))
                {
                    sql += string.Format(" and A.Cause in ('{0}')", queryItem.Cause.Trim().Replace(",", "','"));
                }
                if (!string.IsNullOrEmpty(queryItem.Customer))
                {
                    sql += string.Format(" and B.Customer in ('{0}')", queryItem.Customer.Trim().Replace(",", "','"));
                }
                if (!string.IsNullOrEmpty(queryItem.BusinessUnit))
                {
                    sql += string.Format(" and B.BusinessUnit in ('{0}')", queryItem.BusinessUnit.Trim().Replace(",", "','"));
                }
                if (!string.IsNullOrEmpty(queryItem.ComponentPN))
                {
                    sql += string.Format(" and A.CompPN in ('{0}')", queryItem.ComponentPN.Trim().Replace(",", "','"));
                }
                if (!string.IsNullOrEmpty(queryItem.PrimaryDefect))
                {
                    sql += string.Format(" and A.PriDefect in ('{0}')", queryItem.PrimaryDefect.Trim().Replace(",", "','"));
                }
                if (!string.IsNullOrEmpty(queryItem.Tester))
                {
                    sql += string.Format("(A.FailItem like '%||%||%'{0}'%||%||%') " , queryItem.Tester);
                }
            }
            if (queryItem.WIPUnit)
            {
                sql = string.Format(sqlwip1, sql);
            }
            else
            {
                sql = string.Format(sqlwip0, sql);
            }
            return sql;
        }

        public PossibleMaterialInfo GetPossibleMaterialInfo(string Assembly, string ItemNO, string TLA_DJ)
        {
            //SqlConnection thisConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("eTraceConnection").ConnectionString);
            PossibleMaterialInfo rt = new PossibleMaterialInfo();
            string sql1 = "";
            string sql2 = "";

            if (!string.IsNullOrEmpty(ItemNO.Trim()))
                sql2 = " vc.materialno in ('" + ItemNO.Trim().Replace(",", "','") + "') ";

            if (!string.IsNullOrEmpty(Assembly.Trim()))
                sql1 = " c.materialno in ('" + Assembly.Trim().Replace(",", "','") + "') ";


            if (sql1 == string.Empty)
                return null;


            string sqlx = " "
            + " select distinct tmp.* "
                + " from ( "
                + " ( "
                + " select t1.po as TLA_DJ,vc.datecode, vc.lotno "
                + " from "
                + " t_po_clid as vp with(nolock) "
                + " inner join t_clmaster as vc with(nolock) on vc.clid=vp.clid "
                + " inner join  "
                + " (		 "
                + " select distinct c.clid, c.purOrdNo, p.po, c.materialno, p.clidqty as suba_clidqty, p.issueDate as suba_issueDate  "
                + " from t_po_clid as p with(nolock)  "
                + " inner join t_clmaster as c with(nolock) on p.clid=c.clid "
                + " where " + sql1 + " "
                + " union  "
                + " select distinct c.clid, c.purOrdNo, o.po, c.materialno, p.clidqty as suba_clidqty, p.issueDate as suba_issueDate  "
                + " from t_po_clid as p with(nolock) 	 "
                + " inner join t_pdto_po as o with(nolock) on p.pdto = o.pdto		 "
                + " inner join t_clmaster as c with(nolock) on p.clid=c.clid	 "
                + " where " + sql1 + " "
                + " and p.pdto is not null	 "
                + " ) as t1					 "
                + " on t1.purordno = vp.po		 "
                + " where " + sql2 + " "
                + " and t1.purordno <> ''	 "
                + " ) "
                + " union all "
                + " ( "
                + " select t1.po as TLA_DJ,vc.datecode, vc.lotno "
                + " from t_pdto_po as pd with(nolock) "
                + " inner join t_po_clid as vp with(nolock) on pd.pdto=vp.pdto	 "
                + " inner join t_clmaster as vc with(nolock) on vc.clid=vp.clid	 "
                + " inner join  "
                + " (		 "
                + " select distinct c.clid, c.purOrdNo, p.po, c.materialno, p.clidqty as suba_clidqty, p.issueDate as suba_issueDate "
                + " from t_po_clid as p with(nolock) 	 "
                + " inner join t_clmaster as c with(nolock) on p.clid=c.clid	 "
                + " where " + sql1 + " "
                + " union "
                + " select distinct c.clid, c.purOrdNo, o.po, c.materialno, p.clidqty as suba_clidqty, p.issueDate as suba_issueDate "
                + " from t_po_clid as p with(nolock) 	 "
                + " inner join t_pdto_po as o with(nolock) on p.pdto = o.pdto	 "
                + " inner join t_clmaster as c with(nolock) on p.clid=c.clid "
                + " where " + sql1 + " "
                + " and p.pdto is not null		 "
                + " ) as t1	 "
                + " on t1.purordno = pd.po		 "
                + " where " + sql2 + " "
                + " and t1.purordno <> ''		 "
                + " and pd.pdto is not null	 "
                + " ) "
                       + " ) as tmp "
                       + " where(tmp.TLA_DJ = '" + TLA_DJ + "') ";


            try
            {

                rt.DateCodeList = new List<string>();
                rt.LotNOList = new List<string>();
                dbHelper.ExecuteReader(sqlx, x =>
                {
                    rt.DateCodeList.Add(DBConvert.DB2String(x["datecode"]));
                    rt.LotNOList.Add(DBConvert.DB2String(x["lotno"]));
                });
               
            }
            catch (Exception ex)
            {
            }

            finally
            {
                
            }
            return rt;
        }
    }
}
