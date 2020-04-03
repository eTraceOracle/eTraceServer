using eTrace.Report.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eTrace.Model.V2.Report;
using eTrace.Common;
using System.Data.SqlClient;

namespace eTrace.SqlServerDAL.V2.Report
{
    public class ReportFeedBackDAL : DALBase,   IReportFeedbackDAL
    {
        public ReportFeedBackDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public ReportFeedBackDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        public bool InsertItem(ReportFeedbackModel reportFeedback)
        {
            string sql = @"insert into [T_ReportFeedback] (SentOn ,IP,HostName ,[Like] ,Convenient ,Performance ,Comment ,BackToOldVersion ) 
                                                        values('{0}', '{1}', '{2}',{3},{4},{5},N'{6}','{7}' )";
            sql = string.Format(sql, reportFeedback.SentOn,
                                   reportFeedback.IP,
                                   reportFeedback.HostName,
                                   reportFeedback.Like==null ?"null": "'" + reportFeedback.Like.ToString() + "'",
                                   reportFeedback.Convenient == null ? "null" : "'"+reportFeedback.Convenient.ToString()+ "'",
                                   reportFeedback.Performance == null ? "null" : "'" + reportFeedback.Performance.ToString() + "'",
                                   reportFeedback.Comment,
                                   reportFeedback.BackToOldVersion);
            int result=  dbHelper.ExecuteSql(sql);
            return result > 0;
        }
    }
}
