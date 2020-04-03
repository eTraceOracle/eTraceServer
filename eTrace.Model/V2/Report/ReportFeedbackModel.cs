using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    /// <summary>
    /// 
    /// </summary>
     public  class ReportFeedbackModel
    {

      public DateTime   SentOn { get; set; }
      public string  IP { get; set; }
      public string HostName { get; set; }
        /// <summary>
        /// Do you like this new version?
        /// </summary>
        public bool? Like { get; set; }
        /// <summary>
        /// Is it convenient to use?
        /// </summary>
        public bool? Convenient { get; set; }
        /// <summary>
        /// Is the performance better than previous version?
        /// </summary>
        public bool? Performance { get; set; }
      public string Comment { get; set; }
        /// <summary>
        /// if user back to old version
        /// </summary>
      public bool? BackToOldVersion { get; set; }
    }
}
