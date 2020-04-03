using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class FixtureCategoryModel : ModelBase<List<FixtureCategoryModel.Item>>
    {
        public class Item 
        {
            public string category { get; set; }
            public List<string> subCategory { get; set; }
        }

        
    }
    public class FixtureCategoryDBModel : ModelBase<List<FixtureCategoryDBModel.Item>>
    {

        public class Item
        {
            public string category { get; set; }
            public string subCategory { get; set; }
        }
    }
}
