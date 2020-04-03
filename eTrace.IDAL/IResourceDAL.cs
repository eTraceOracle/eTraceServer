﻿using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IResourceDAL
    {
        ProductStationModel GetProductStation(ProductStationQuery query);
    }
}
