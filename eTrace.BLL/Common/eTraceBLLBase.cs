using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.BLL
{
    public abstract class eTraceBLLBase<T1, T2> where T1 : T2, new()
    {
        protected static ILog logger = LogManager.GetLogger(typeof(T1));

        private static T2 instance = new T1(); 
        public static T2 Instance
        {
            get { return instance; }
        }



        public eTraceBLLBase()
        {

        }
    }
}
