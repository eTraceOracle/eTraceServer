using eTrace.Common;
using eTrace.Report.IDAL;
using eTrace.Model.V2.Report;

using System.Collections.Generic;


namespace eTrace.Report.SqlServerDAL.V2.Report
{
    public class MobileEquipmentDAL : DALBase, IMobileEquipmentDAL
    {
        #region corts

        public MobileEquipmentDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public MobileEquipmentDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_MobileEquipmentData_select = @"exec sp_PMGetMobileEquipment ";

        #endregion

        #region methods

        #region get MobileEquipment EquipmentData

        public ReportMobileEquipmentModel GetMobileEquipmentData(ReportMobileEquipmentQuery query)
        {
            ReportMobileEquipmentModel result = new ReportMobileEquipmentModel();
            result.Data = new List<ReportMobileEquipmentModel.Item>();
            string sql = sql_MobileEquipmentData_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion

            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Data.Add(GetReportEquipmentModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        public long EquipmentDataGetRowCount(ReportMobileEquipmentQuery query)
        {
            long rowCount = 1;
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportMobileEquipmentQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.EquipmentID))
                {
                    sql += string.Format("'{0}'", query.EquipmentID);
                }
            }
            return sql;
        }

        private ReportMobileEquipmentModel.Item GetReportEquipmentModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportMobileEquipmentModel.Item data = new ReportMobileEquipmentModel.Item
            {
                EquipmentID = DBConvert.DB2String(dr["EquipmentID"]),
                Department = DBConvert.DB2String(dr["Department"]),
                Description = DBConvert.DB2String(dr["Description"]),
                Status = DBConvert.DB2String(dr["Status"]),
                Location= DBConvert.DB2String(dr["Location"]),
                PMLastDate = DBConvert.DB2String(dr["PMLastDate"]),
                PMNextDate = DBConvert.DB2String(dr["PMNextDate"]),
                PMMan = DBConvert.DB2String(dr["PMMan"]),
                PictureURL= DBConvert.DB2String(dr["PictureURL"]),               
                MailGroup = DBConvert.DB2String(dr["MailGroup"]),
            };
            return data;
        }

        #endregion

        public bool InsertMobileItem(ReportMobileEquipmentQuery ReportMobileEquipment)
        {
            string sql = @" exec sp_PMInsertMobileEquipment '{0}', '{1}', N'{2}','{3}' ";
            sql = string.Format(sql, ReportMobileEquipment.EquipmentID,
                                   ReportMobileEquipment.Location,
                                   ReportMobileEquipment.Description,
                                   ReportMobileEquipment.PictureURL);
            int result = dbHelper.ExecuteSql(sql);
            return result > 0;
        }

        #endregion
    }
}
