using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model
{
    public enum EmBussinesCodeType
    {
        Success = 0,

        #region 1~100

        ServerNotFound = 1,
        ApiNotFound = 2,
        ServerTimeout = 3,

        IsOverMaxRow = 20,
        #endregion

        #region 100000~200000  Report
        #region 100000~110000 Common Report
        #region 100000~110100 Common Report WipData
        ReportIntsnEmpty = 100000,
        #endregion

        #region 100100~110200 Common Report WipData
        ReportInternalError = 100100,
        #endregion


        #region 100201~110300 Common Report WipData
        ReportProductGetProcessError = 100201,
        ReportCheckDownloadRowCountSuccess = 100300,
        ReportCheckDownloadRowCountEqualZero=100301,
        ReportCheckDownloadRowCountTooLarge = 100302,

        #endregion
        #endregion
        #region 110001~120000 V1 Report
        #region 110000~110100 V1 Report WipData
        #endregion
        #endregion
        #region 120001~130000 V2 Report
        /// <summary>
        /// over max row count limit when download report
        /// </summary>
        ReportOverMaxRowsCount = 120001,
        #region 120000~120100 V2 Report WipData
        #endregion

        #region 130000~130100 V2 Report Quality
        /// <summary>
        /// 
        /// </summary>
        DJNotFound = 1300000,
        NoDataFound = 1300001,
        #endregion

        #endregion
        #endregion

        ReportFixtureDatasError = 100200,
        ReportUnKnowError = 200000,
    }
}
