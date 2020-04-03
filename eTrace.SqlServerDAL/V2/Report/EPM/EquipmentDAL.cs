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
    public class EquipmentDAL : DALBase, IEquipmentDAL
    {
        #region corts

        public EquipmentDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public EquipmentDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_tdHeader_select = @"select * from T_TDHeader with(nolock) where 1=1 ";

        private const string sql_Equipment_select = @"SELECT EquipmentID,Category, SubCategory, Description, Model, Spec, 
                                                            CONVERT(varchar(10),MfrDate,120) as MfrDate, CurrProdLine, FixedAssessID, Manufacturer, 
                                                            ManufacturerSN, CONVERT(varchar(10),AcqDate,120) as AcqDate, Department, SeqOnLine, Owner, 
                                                            Status,ChangedOn,ChangedBy,Remarks FROM T_SMEquipment a  WITH (nolock) where 1=1 ";

        private const string sql_equipment_status_select = "select distinct status from  T_SMEquipment with (nolock) order by Status";

        private const string sql_all_department_select = "select Dept from T_SMDept with (nolock)";


        #endregion

        #region methods


        #region methods EquipmentData- 设备数据

        public ReportEquipmentModel GetEquipmentData(ReportEquipmentQuery query)
        {
            ReportEquipmentModel result = new ReportEquipmentModel();
            result.Data = new List<ReportEquipmentModel.Item>();
            string sql = sql_Equipment_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportEquipmentModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        public long EquipmentDataGetRowCount(ReportEquipmentQuery query)
        {
            string sql = sql_Equipment_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }
        public void GetEquipmentAction(ReportEquipmentQuery query, Action<ReportEquipmentModel.Item> action, Action actionEnd)
        {
            ReportEquipmentModel result = new ReportEquipmentModel();
            result.Data = new List<ReportEquipmentModel.Item>();
            string sql = sql_Equipment_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                if (action != null)
                {
                    action(GetReportEquipmentModel(dr));
                }
            });
            if (actionEnd != null)
            {
                actionEnd();
            }
        }

        private string GetReportEquipmentSqlCondition(ReportEquipmentQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.EquipmentID))
                {
                    sql += string.Format(" and a.EquipmentID='{0}'", query.EquipmentID);
                }
                if (!string.IsNullOrEmpty(query.FixedAssessID))
                {
                    sql += string.Format(" and a.FixedAssessID='{0}'", query.FixedAssessID);
                }
                if (!string.IsNullOrEmpty(query.Category))
                {
                    sql += string.Format(" and a.Category='{0}'", query.Category);
                }
                if (!string.IsNullOrEmpty(query.SubCategory))
                {
                    sql += string.Format(" and a.SubCategory='{0}'", query.SubCategory);
                }
                if (!string.IsNullOrEmpty(query.Status))
                {
                    sql += string.Format(" and a.Status='{0}'", query.Status);
                }
                if (!string.IsNullOrEmpty(query.Model))
                {
                    sql += string.Format(" and a.Model='{0}'", query.Model);
                }
                if (!string.IsNullOrEmpty(query.CurrProdLine))
                {
                    sql += string.Format(" and a.CurrProdLine='{0}'", query.CurrProdLine);
                }
                if (!string.IsNullOrEmpty(query.Department))
                {
                    sql += string.Format(" and a.Department='{0}'", query.Department);
                }
            }
            return sql;
        }

        private ReportEquipmentModel.Item GetReportEquipmentModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportEquipmentModel.Item data = new ReportEquipmentModel.Item
            {
                EquipmentID = DBConvert.DB2String(dr["EquipmentID"]),
                Category = DBConvert.DB2String(dr["Category"]),
                SubCategory = DBConvert.DB2String(dr["SubCategory"]),                
                Description = DBConvert.DB2String(dr["Description"]),
                Model = DBConvert.DB2String(dr["Model"]),
                Spec = DBConvert.DB2String(dr["Spec"]),
                MfrDate = DBConvert.DB2Datetime(dr["MfrDate"]),
                CurrProdLine = DBConvert.DB2String(dr["CurrProdLine"]),
                FixedAssessID = DBConvert.DB2String(dr["FixedAssessID"]),
                Manufacturer = DBConvert.DB2String(dr["Manufacturer"]),
                ManufacturerSN = DBConvert.DB2String(dr["ManufacturerSN"]),
                AcqDate = DBConvert.DB2Datetime(dr["AcqDate"]),
                Department = DBConvert.DB2String(dr["Department"]),
                SeqOnLine = DBConvert.DB2String(dr["SeqOnLine"]),
                Owner = DBConvert.DB2String(dr["Owner"]),
                Status = DBConvert.DB2String(dr["Status"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"])
            };
            return data;
        }


        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <returns></returns>
        public List<string> GetEquipmentStatus()
        {
            string sql = sql_equipment_status_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["status"]));
            });

            return result;
        }

        /// <summary>
        /// Get All Department
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllDepartment()
        {
            string sql = sql_all_department_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["Dept"]));
            });

            return result;
        }



        #endregion

        #endregion
    }
}
