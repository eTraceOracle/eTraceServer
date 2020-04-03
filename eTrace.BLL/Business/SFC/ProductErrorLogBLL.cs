using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eTrace.Model.V2.Report;
using eTrace.Report.IDAL;
using eTrace.Core;
using eTrace.Common;
using eTrace.Report.IBLL;

namespace eTrace.Report.BLL
{
    public class ProductErrorLogBLL : eTraceBLLBase<ProductErrorLogBLL, IProductErrorLogBLL> , IProductErrorLogBLL
    {
        private IProductErrorLogDAL _ProductErrorLogDAL = null;

        public ProductErrorLogBLL()
        {
            _ProductErrorLogDAL = DBManager.Instance.GetPorductErrorLogDAL(EmDBType.eTraceConnection);

        }
   

        public long GetProductErrorLogTotalCount(GetProductErrorLogQuery query)
        {
            return _ProductErrorLogDAL.GetTotalCount(query.Data);
        }
        public GetProductErrorLogModel GetProductErrorLogByPage(GetProductErrorLogQuery query)
        {
            GetProductErrorLogModel resultModel = new GetProductErrorLogModel()
            {
                Data = _ProductErrorLogDAL.GetProductionErrorLogByPage(query).ConvertAll(x => GetMissMatchItem(x)),
                Pager = new Model.ModelPager()
                {
                    TotalCount = _ProductErrorLogDAL.GetTotalCount(query.Data)
                }
            };
            resultModel.IsOverMaxRow = resultModel.Data.Count > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return resultModel;
        }

        public List<GetProductErrorLogModel.MissMatchItem> GetProductErrorLog(GetProductErrorLogQuery query)
        {
            var list= _ProductErrorLogDAL.GetProductionErrorLog(query);
            return list.ConvertAll<GetProductErrorLogModel.MissMatchItem>(x => GetMissMatchItem(x));
        }

        private GetProductErrorLogModel.MissMatchItem GetMissMatchItem(GetProductErrorLogModel.Item item)
        {
            var messageDictionary = GetDictionaryFromMessage(item.ErrorMsg);

            return new GetProductErrorLogModel.MissMatchItem()
            {
                DateTime = item.DateTime,
                Eeprom = messageDictionary.ContainsKey("Eeprom") ? messageDictionary["Eeprom"] : "",
                IntSN = messageDictionary.ContainsKey("IntSN") ? messageDictionary["IntSN"] : "",
                Label = messageDictionary.ContainsKey("Label") ? messageDictionary["Label"] : "",
                Line = messageDictionary.ContainsKey("Line") ? messageDictionary["Line"] : "",
                Model = messageDictionary.ContainsKey("Model") ? messageDictionary["Model"] : "",
                Module = item.Module,
                UserName = item.UserName
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private Dictionary<string,string> GetDictionaryFromMessage(string message)
        {
            Dictionary<string, string> rt = new Dictionary<string, string>();
              string[] messageArray = message.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < messageArray.Length; i++)
            {
                string item = messageArray[i];
                string[] itemSplitArray = item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (itemSplitArray.Length>0 && !string.IsNullOrEmpty(itemSplitArray[0]))
                {
                    if (itemSplitArray.Length==1)
                    {
                        rt.Add(itemSplitArray[0], "");
                    }else
                    {
                        rt.Add(itemSplitArray[0], itemSplitArray[1]);
                    }
                }
            }
            return rt;
        }
    }
}
