using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Common.Log
{
    /// <summary>
    /// 
    /// </summary>
    public  class CustomADOAppender:log4net.Appender.AdoNetAppender
    {


        //string _connectiongStr = "111";

        
        //public new string ConnectionString
        //{
        //    set
        //    {
        //        _connectiongStr = value;
        //        base.ConnectionString = _connectiongStr;
        //    }
        //    get
        //    {
        //        return base.ConnectionString;
        //    }
        //}
        public CustomADOAppender()
        {
            //this.ConnectionString = "111";
            this.ConnectionString = eTrace.Common.DBManagerConfig.Instance.GetDbHelper(EmDBType.eTraceConnection).ConnectionString;
        }
    }
}
