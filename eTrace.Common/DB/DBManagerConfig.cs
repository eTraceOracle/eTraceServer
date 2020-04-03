using eTrace.Common;
using eTrace.Report.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Common
{
    public class DBManagerConfig
    {
        public EventHandler<SQLEventArgs> DumpSqlEvent;
        private static DBManagerConfig instance = new DBManagerConfig();
        public static DBManagerConfig Instance
        {
            get { return instance; }
        }

        private static object lockObj = new object();
        private Dictionary<string, DBHelper> dicDBHelper = new Dictionary<string, DBHelper>();

        private DBManagerConfig()
        {
            InitDB();
        }

        private void InitDB()
        {
            dicDBHelper[EmDBType.eTraceConnection.ToString()] = new DBHelper(LocalConfiguration.Instance.GetDbConnection(EmDBType.eTraceConnection.ToString()));
            dicDBHelper[EmDBType.eTraceConnectionArchive.ToString()] = new DBHelper(LocalConfiguration.Instance.GetDbConnection(EmDBType.eTraceConnectionArchive.ToString()));
            dicDBHelper[EmDBType.eTraceAdditionConnectionCH1.ToString()] = new DBHelper(LocalConfiguration.Instance.GetDbConnection(EmDBType.eTraceAdditionConnectionCH1.ToString()));
            dicDBHelper[EmDBType.eTraceV1ConnectionCH1.ToString()] = new DBHelper(LocalConfiguration.Instance.GetDbConnection(EmDBType.eTraceV1ConnectionCH1.ToString()));
            dicDBHelper[EmDBType.eTraceAdditionConnectionCN1.ToString()] = new DBHelper(LocalConfiguration.Instance.GetDbConnection(EmDBType.eTraceAdditionConnectionCN1.ToString()));
            dicDBHelper[EmDBType.eTraceV1ConnectionCN1.ToString()] = new DBHelper(LocalConfiguration.Instance.GetDbConnection(EmDBType.eTraceV1ConnectionCN1.ToString()));
            //dicDBHelper[EmDBType.eTraceConnectionString.ToString()] = new DBHelper(LocalConfiguration.Instance.GetDbConnection(EmDBType.eTraceConnectionString.ToString()));

            foreach (var item in dicDBHelper)
            {
                item.Value.DumpSqlEvent += OnDumpSqlEvent;
            }
        }

        private void OnDumpSqlEvent(object sender, SQLEventArgs e)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(sender, e);
            }
        }

        public DBHelper GetDbHelper(string connection)
        {
            return GetInstance(connection);
        }

        public DBHelper GetDbHelper(EmDBType emDBType)
        {
            return GetInstance(emDBType.ToString());
        }

        private DBHelper GetInstance(string connection)
        {
            lock (lockObj)
            {
                if (!dicDBHelper.ContainsKey(connection))
                {
                    dicDBHelper[connection] = new DBHelper(connection);
                }
                return dicDBHelper[connection];
            }
        } 
    }
}
