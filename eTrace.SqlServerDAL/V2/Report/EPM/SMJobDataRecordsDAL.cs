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
    public class SMJobDataRecordsDAL : DALBase, ISMJobDataRecordsDAL
    {                                                    
        #region corts

        public SMJobDataRecordsDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMJobDataRecordsDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMJobDataRecords_Job = @" select distinct JobID,JobQty,CONVERT(varchar(10),ScheduleDate,120) as ScheduleDate, DJ,DJQty,Assembly,
                                                            AssemblyRev,Side,Model,ModelRev,PCB,PCBRev,ProdLine,BaseLine,Status,LeadFree,dateadd(hour,8,StartTime) as StartTime,
                                                            dateadd(hour,8,CompleteTime) as CompletedTime,dateadd(hour,8,ChangedOn) as ChangedOn,
                                                            ChangedBy,Remarks from t_smjob as j with(nolock)
                                                            where 1=1 ";

        private const string sql_SMJobDataRecords_Equipment = @" select distinct j.dj as [DJ],  j.jobid as [JobID], j.JobQty, CONVERT(varchar(10),j.ScheduleDate,120) as ScheduleDate, 
                                                            j.assembly as [Assembly], j.AssemblyRev, j.ProdLine, e.EquipmentID as [EquipmentID], 
                                                            e.SeqNO as [SeqNo], i.Item, i.ItemType as [ItemType], i.Category as [Category], i.Value as [Value], i.Unit as [Unit] 
                                                            from t_smjob as j with(nolock)   
                                                            inner join T_SMJobEquip as e with(nolock) on e.JobID = j.JobID 
                                                            inner join T_SMJobEquipItem as i with(nolock) on i.JobID = e.JobID and i.EquipmentID = e.EquipmentID 
                                                            where 1=1 ";

        private const string sql_SMJobDataRecords_SPG = @" select distinct j.dj as [DJ],  j.jobid as [JobID], j.JobQty, CONVERT(varchar(10),j.ScheduleDate,120) as ScheduleDate, 
                                                            j.assembly as [Assembly], j.AssemblyRev, j.ProdLine,dateadd(hour,8, j.StartTime) as StartTime,
                                                            dateadd(hour,8, j.CompleteTime) as CompleteTime, m.Material, m.value as [MaterialID], 
                                                            dateadd(hour,8, m.ChangedOn) as ChangedOn, m.ChangedBy, f.Category, f.SubCategory
                                                            from t_smjob as j with(nolock)
                                                            inner join T_SMJobMat as m with(nolock) on m.JobID = j.JobID
                                                            left outer join T_SMFixture as f with(nolock) on(m.Value = f.FixtureID or m.Value = f.BatchID)
                                                            where 1=1 and m.matType IN ( 'GENERAL','Solder Paste','Glue') ";
        #endregion


      #region methods SMJobDataRecords

        public ReportSMJobDataRecordsJobModel GetSMJobDataRecordsJobDatas(ReportSMJobDataRecordsQuery query)
        {
            ReportSMJobDataRecordsJobModel result = new ReportSMJobDataRecordsJobModel();
            result.Data = new List<ReportSMJobDataRecordsJobModel.Job>();
            string sql = sql_SMJobDataRecords_Job;
            #region Conditions
            sql += GetReportSMJobDataRecordsSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMJobDataRecordsJobModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }


        public ReportSMJobDataRecordsEquipmentModel GetSMJobDataRecordsEquipmentDatas(ReportSMJobDataRecordsQuery query)
        {
            ReportSMJobDataRecordsEquipmentModel result = new ReportSMJobDataRecordsEquipmentModel();
            result.Data = new List<ReportSMJobDataRecordsEquipmentModel.Equipment>();
            string sql = sql_SMJobDataRecords_Equipment;
            #region Conditions
            sql += GetReportSMJobDataRecordsSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMJobDataRecordsEquipmentModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportSMJobDataRecordsSPGModel GetSMJobDataRecordsSPGDatas(ReportSMJobDataRecordsQuery query)
        {
            ReportSMJobDataRecordsSPGModel result = new ReportSMJobDataRecordsSPGModel();
            result.Data = new List<ReportSMJobDataRecordsSPGModel.SPG>();
            string sql = sql_SMJobDataRecords_SPG;
            #region Conditions
            sql += GetReportSMJobDataRecordsSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMJobDataRecordsSPGModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }


        #region "Get Row Count"
            public long SMJobDataRecordsJobGetRowCount(ReportSMJobDataRecordsQuery query)
            {
                string sql = sql_SMJobDataRecords_Job;
                #region Conditions
                sql += GetReportSMJobDataRecordsSqlCondition(query);
                #endregion
                string  countSql = GetSQLCount( sql);
                long rowCount=  dbHelper.GetCount(countSql);
                //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
                return rowCount;
            }

            public long SMJobDataRecordsEquipmentGetRowCount(ReportSMJobDataRecordsQuery query)
            {
                string sql = sql_SMJobDataRecords_Equipment;
                #region Conditions
                sql += GetReportSMJobDataRecordsSqlCondition(query);
                #endregion
                string countSql = GetSQLCount(sql);
                long rowCount = dbHelper.GetCount(countSql);
                //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
                return rowCount;
            }

            public long SMJobDataRecordsSPGGetRowCount(ReportSMJobDataRecordsQuery query)
            {
                string sql = sql_SMJobDataRecords_SPG;
                #region Conditions
                sql += GetReportSMJobDataRecordsSqlCondition(query);
                #endregion
                string countSql = GetSQLCount(sql);
                long rowCount = dbHelper.GetCount(countSql);
                //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
                return rowCount;
            }
        #endregion

        private string GetReportSMJobDataRecordsSqlCondition(ReportSMJobDataRecordsQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Assembly))
                {
                    sql += string.Format(" and j.Assembly='{0}'", query.Assembly);
                }
                if (!string.IsNullOrEmpty(query.Job))
                {
                    sql += string.Format(" and j.JobID='{0}'", query.Job);
                }
                if (!string.IsNullOrEmpty(query.ProdLine))
                {
                    sql += string.Format(" and j.ProdLine='{0}'", query.ProdLine);
                }
                if (!string.IsNullOrEmpty(query.BaseLine))
                {
                    sql += string.Format(" and j.BaseLine='{0}'", query.BaseLine);
                }

                if (!query.DateFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), j.ScheduleDate, 23) >='{0}'", query.DateFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.DateTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), j.ScheduleDate, 23) <='{0}'", query.DateTo.ToString("yyyy-MM-dd"));
                }


                if (!string.IsNullOrEmpty(query.JobStatus))
                {
                    sql += string.Format(" and j.Status='{0}'", query.JobStatus);
                }
                if (!string.IsNullOrEmpty(query.DJ))
                {
                    sql += string.Format(" and j.dj='{0}'", query.DJ);
                }
                if (!string.IsNullOrEmpty(query.Material))
                {
                    sql += string.Format(" and m.material='{0}'", query.Material);
                }
                if (!string.IsNullOrEmpty(query.MaterialID))
                {
                    sql += string.Format(" and m.value='{0}'", query.MaterialID);
                }
            }
            return sql;
        }

        private ReportSMJobDataRecordsJobModel.Job GetReportSMJobDataRecordsJobModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMJobDataRecordsJobModel.Job data = new ReportSMJobDataRecordsJobModel.Job
            {
                JobID = DBConvert.DB2String(dr["JobID"]),
                JobQty = DBConvert.DB2String(dr["JobQty"]),
                ScheduleDate = DBConvert.DB2Datetime(dr["ScheduleDate"]),
                DJ = DBConvert.DB2String(dr["DJ"]),
                DJQty = DBConvert.DB2String(dr["DJQty"]),
                Assembly = DBConvert.DB2String(dr["Assembly"]),
                AssemblyRev = DBConvert.DB2String(dr["AssemblyRev"]),
                Side = DBConvert.DB2String(dr["Side"]),
                Model = DBConvert.DB2String(dr["Model"]),
                ModelRev = DBConvert.DB2String(dr["ModelRev"]),
                PCB = DBConvert.DB2String(dr["PCB"]),
                PCBRev = DBConvert.DB2String(dr["PCBRev"]),
                ProdLine = DBConvert.DB2String(dr["ProdLine"]),
                BaseLine = DBConvert.DB2String(dr["BaseLine"]),
                Status = DBConvert.DB2String(dr["Status"]),
                LeadFree = DBConvert.DB2String(dr["LeadFree"]),
                StartTime = DBConvert.DB2Datetime(dr["StartTime"]),
                CompletedTime = DBConvert.DB2Datetime(dr["CompletedTime"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),

            };
            return data;
        }

        private ReportSMJobDataRecordsEquipmentModel.Equipment GetReportSMJobDataRecordsEquipmentModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMJobDataRecordsEquipmentModel.Equipment data = new ReportSMJobDataRecordsEquipmentModel.Equipment
            {
                DJ = DBConvert.DB2String(dr["DJ"]),
                JobID = DBConvert.DB2String(dr["JobID"]),
                JobQty = DBConvert.DB2String(dr["JobQty"]),
                ScheduleDate = DBConvert.DB2Datetime(dr["ScheduleDate"]),
                Assembly = DBConvert.DB2String(dr["Assembly"]),
                AssemblyRev = DBConvert.DB2String(dr["AssemblyRev"]),
                ProdLine = DBConvert.DB2String(dr["ProdLine"]),
                EquipmentID = DBConvert.DB2String(dr["EquipmentID"]),
                SeqNo = DBConvert.DB2String(dr["SeqNo"]),
                Item = DBConvert.DB2String(dr["Item"]),
                ItemType = DBConvert.DB2String(dr["ItemType"]),
                Category = DBConvert.DB2String(dr["Category"]),
                Value = DBConvert.DB2String(dr["Value"]),
                Unit = DBConvert.DB2String(dr["Unit"]),
            };
            return data;
        }

        private ReportSMJobDataRecordsSPGModel.SPG GetReportSMJobDataRecordsSPGModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMJobDataRecordsSPGModel.SPG data = new ReportSMJobDataRecordsSPGModel.SPG
            {
                DJ = DBConvert.DB2String(dr["DJ"]),
                JobID = DBConvert.DB2String(dr["JobID"]),
                JobQty = DBConvert.DB2String(dr["JobQty"]),
                ScheduleDate = DBConvert.DB2Datetime(dr["ScheduleDate"]),
                Assembly = DBConvert.DB2String(dr["Assembly"]),
                AssemblyRev = DBConvert.DB2String(dr["AssemblyRev"]),
                ProdLine = DBConvert.DB2String(dr["ProdLine"]),
                StartTime = DBConvert.DB2Datetime(dr["StartTime"]),
                CompleteTime = DBConvert.DB2Datetime(dr["CompleteTime"]),
                Material = DBConvert.DB2String(dr["Material"]),
                MaterialID = DBConvert.DB2String(dr["MaterialID"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Category = DBConvert.DB2String(dr["Category"]),
                SubCategory = DBConvert.DB2String(dr["SubCategory"]),
            };
            return data;
        }


        #endregion

    }
}
