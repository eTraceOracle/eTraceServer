using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{

    /// <summary>
    /// base class for report download
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReportDownloadRequestBase<T> : ServerRequestBase<T>
    {
        /// <summary>
        /// headers only in this list can show in download excel
        /// </summary>
        public   List<TableHeader> TableHeaders = new List<TableHeader>();
    }


    public class TableHeader
    {

        public int HeaderOrder { get; set; }
        /// <summary>
        /// field name of table
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// label of header  show in excel
        /// </summary>
        public string HeaderLabel { get; set; }
    }
}
