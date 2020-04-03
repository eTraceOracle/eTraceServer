using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKCode
{
    public enum emBussinesCodeType
    {
        Success=0,

        #region 100000~200000  Report
        #region 100000~110000 Common Report
        #region 100000~110100 Common Report WipData
        ReportIntsnEmpty = 100000
        #endregion
        #endregion
        #region 110001~120000 V1 Report
        #region 110000~110100 V1 Report WipData
        #endregion
        #endregion
        #region 120001~130000 V2 Report
        #region 120000~120100 V2 Report WipData
        #endregion
        #endregion
        #endregion
    }
}
