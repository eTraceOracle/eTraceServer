using eTrace.Common;
using eTrace.Core;
using eTrace.Report.IBLL;
using eTrace.Report.IDAL;
using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using eTrace.Model.V2.Report.DailyRepairList;

namespace eTrace.Report.BLL
{
    public class RepairBLL : eTraceBLLBase<RepairBLL, IRepairBLL>, IRepairBLL
    {
        private IListOfRepairDataDAL _listOfRepairDataDAL = null;
        private eTrace.Report.IDAL.DailyRepairList.IMoreThanOneDAL _moreThanOneDAL = null;
        private eTrace.Report.IDAL.DailyRepairList.ITopTenComonentDAL _topTenComonentDAL = null;
        private eTrace.Report.IDAL.DailyRepairList.IWipInWipOutDAL _wipInWipOutDAL = null;
        private eTrace.Report.IDAL.DailyRepairList.IWipOutDAL _wipOutDAL = null;

        public RepairBLL()
        {
            _listOfRepairDataDAL= DBManager.Instance.GetListOfRepairDataDAL(EmDBType.eTraceConnection);
            _moreThanOneDAL = DBManager.Instance.GetMoreThanOneDAL(EmDBType.eTraceConnection);
            _topTenComonentDAL = DBManager.Instance.GetTopTenComonentDAL(EmDBType.eTraceConnection);
            _wipInWipOutDAL = DBManager.Instance.GetWipInWipOutDAL(EmDBType.eTraceConnection);
            _wipOutDAL = DBManager.Instance.GetWipOutDAL(EmDBType.eTraceConnection);
        }
        #region ListOfRepairData
        public List<GetListOfRepairDataModel.Item> GetListOfRepairData(GetListOfRepairDataQuery query)
        {
            return convertItemList(_listOfRepairDataDAL.GetListOfRepirData(query), query.Data.MaterialInfo);
        }

        public GetListOfRepairDataModel GetListOfRepairDataByPage(GetListOfRepairDataQuery query)
        {
            var dataList = _listOfRepairDataDAL.GetListOfRepairDataByPage(query);
            
            GetListOfRepairDataModel rt = new GetListOfRepairDataModel()
            {
                Data = convertItemList( dataList,query.Data.MaterialInfo),
                IsOverMaxRow = false,
                Pager = new ModelPager()
                {
                    TotalCount = _listOfRepairDataDAL.GetTotalCount(query.Data)
                }
            };

            return rt;
        }
        public long GetListOfRepairDataTotalCount(GetListOfRepairDataQuery query)
        {
            return _listOfRepairDataDAL.GetTotalCount(query.Data);
        }
        #endregion
        public List<GetMoreThanOneModel.Item> GetMoreThanOneData(GetMoreThanOneQuery query)
        {
            return _moreThanOneDAL.GetData(query);
        }

        public GetMoreThanOneModel GetMoreThanOneDataByPage(GetMoreThanOneQuery query)
        {
            GetMoreThanOneModel rt = new GetMoreThanOneModel();
            rt = new GetMoreThanOneModel()
            {
                Data = _moreThanOneDAL.GetDataByPage(query),
                IsOverMaxRow=false,
                Pager=new ModelPager()
                {
                    TotalCount=_moreThanOneDAL.GetTotalCount(query.Data)
                }
            };
            return rt;
        }

        public long GetMoreThanOneDataTotalCount(GetMoreThanOneQuery query)
        {
            return _moreThanOneDAL.GetTotalCount(query.Data);
        }

        public List<GetTopTenComonentModel.Item> GetTopTenComonentData(GetTopTenComonentQuery query)
        {
            return _topTenComonentDAL.GetData(query);
        }

        public GetTopTenComonentModel GetTopTenComonentDataByPage(GetTopTenComonentQuery query)
        {
          
           var  rt = new GetTopTenComonentModel()
            {
                Data = _topTenComonentDAL.GetDataByPage(query),
                IsOverMaxRow = false,
                Pager = new ModelPager()
                {
                    TotalCount = _topTenComonentDAL.GetTotalCount(query.Data)
                }
            };
            return rt;

        }

        public long GetTopTenComonentDataTotalCount(GetTopTenComonentQuery query)
        {
            return _topTenComonentDAL.GetTotalCount(query.Data);
        }

        public List<GetWipInWipOutModel.Item> GetWipInWipOutData(GetWipInWipOutQuery query)
        {
            return _wipInWipOutDAL.GetData(query);
        }

        public GetWipInWipOutModel GetWipInWipOutDataByPage(GetWipInWipOutQuery query)
        {
            var rt = new GetWipInWipOutModel()
            {
                Data = _wipInWipOutDAL.GetDataByPage(query),
                IsOverMaxRow = false,
                Pager = new ModelPager()
                {
                    TotalCount = _wipInWipOutDAL.GetTotalCount(query.Data)
                }
            };
            return rt;

        }

        public long GetWipInWipOutDataTotalCount(GetWipInWipOutQuery query)
        {
            return _wipInWipOutDAL.GetTotalCount(query.Data);
        }

        public List<GetWipOutModel.Item> GetWipOutData(GetWipOutQuery query)
        {
            return _wipOutDAL.GetData(query);
        }

        public GetWipOutModel GetWipOutDataByPage(GetWipOutQuery query)
        {
            var rt = new GetWipOutModel()
            {
                Data = _wipOutDAL.GetDataByPage(query),
                IsOverMaxRow = false,
                Pager = new ModelPager()
                {
                    TotalCount = _wipOutDAL.GetTotalCount(query.Data)
                }
            };
            return rt;
        }

        public long GetWipOutDataTotalCount(GetWipOutQuery query)
        {
            return _wipOutDAL.GetTotalCount(query.Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private List<GetListOfRepairDataModel.Item> convertItemList (List<GetListOfRepairDataModel.Item> itemList,bool isCheckMaterialInfo=false)
        {
            List<GetListOfRepairDataModel.Item> returnList = new List<GetListOfRepairDataModel.Item>();
            foreach (var item in itemList)
            {
                string[] FailItemArray= item.FailItem.Split(new string[] { "||" }, StringSplitOptions.None);
                if (FailItemArray.Length>=7)
                {
                    item.TestStep = FailItemArray[6].ToString().Replace("T:", "");
                }
                if (FailItemArray.Length >= 6)
                {
                    item.Result = FailItemArray[5].ToString().Replace("R:", "");
                }
                if (FailItemArray.Length >= 5)
                {
                    item.UpperLimit = FailItemArray[4].ToString().Replace("U:", "");
                }
                if (FailItemArray.Length >= 4)
                {
                    item.LowerLimit = FailItemArray[3].ToString().Replace("L:", "");
                }
                if (FailItemArray.Length >= 3)
                {
                    item.Tester = FailItemArray[2].ToString();
                }
                if (isCheckMaterialInfo)
                {
                    if ( (!string.IsNullOrEmpty(item.CompPN)) &&
                            (string.IsNullOrEmpty(item.OrgCompDateCode)) &&
                            (string.IsNullOrEmpty(item.OrgCompLotNo)) 
                        )
                    {
                         var materialInfo= _listOfRepairDataDAL.GetPossibleMaterialInfo(item.AssemblyPN, item.CompPN, item.ProdOrder);
                        if (materialInfo!=null)
                        {
                            item.PossibleDateCode = string.Join(",", materialInfo.DateCodeList);
                            item.PossibleLotNO = string.Join(",", materialInfo.LotNOList);
                        }
                    }
                }
                returnList.Add(item);
            }
            return returnList;
        }
     

    }
}
