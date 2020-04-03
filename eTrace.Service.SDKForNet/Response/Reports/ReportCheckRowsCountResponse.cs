using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
    public class ReportCheckRowsCountResponse : ServerResponseBase
    {
        /// <summary>
        /// If bigger than CheckDownloadRow and less than MaxDownloadRow,Clent should alert the warning message.
        /// </summary>
        public bool LessThanCheckDownloadRowCount { get; set; }
        /// <summary>
        /// If Bigger Than Max DownloadRow,Check avoid downloading
        /// </summary>
        public bool LessThanMaxDownloadRowCount { get; set; }
        public long RowCount { get; set; }
    }
}
