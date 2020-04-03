using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKCode
{
    public enum emServerCodeType
    {
        Success=0,

        #region 1~100 

        ServerNotFound=1,
        ApiNotFound=2,
        ServerTimeout=3,


        #endregion
    }
}
