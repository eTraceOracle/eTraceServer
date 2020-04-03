using eTrace.Core;
using eTrace.Model;
using eTrace.Service.SDKForNet;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using eTrace.Common;
using eTrace.Service.SDKForNet.Request.Reports;
using eTrace.Model.V2.Report;
using System.Net.Http.Headers;
using eTrace.Report.BLL;
using System.Data;

namespace eTraceWebApi
{
    /// <summary>
    /// 
    /// ServerBaseController
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServerBaseController<T> : ApiController where T : ServerBaseController<T>, new()
    {
        /// <summary>
        /// logger
        /// </summary>
        protected static ILog logger = LogManager.GetLogger(typeof(T));

        /// <summary>
        /// GetServerResponseDataInited
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        protected T1 GetServerResponseDataInited<T1>()
            where T1 : ServerResponseBase, new()
        {
            var response = new T1();
            response.ReponseServerTime = DateTime.Now;
            response.Start();
            return response;
        }
        /// <summary>
        /// GetBusinessResponseDataInited
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        protected T1 GetBusinessResponseDataInited<T1>()
            where T1 : ServerResponseBase, new()
        {
            var response = new T1();
            response.RequestTime = DateTime.Now;
            response.Start();
            return response;
        }
        /// <summary>
        /// use for Check row count actions
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        protected T1 GetCheckRowCountResponse<T1>(long   rowCount)
        where T1 : eTrace.Service.SDKForNet.Response.Reports.ReportCheckRowsCountResponse, new()
        {
            var response = new T1();
            response.RequestTime = DateTime.Now;
            response.Start();
            response.BussinesCode = EmBussinesCodeType.Success;
            response.LessThanCheckDownloadRowCount = (rowCount <= CheckDownloadRowCount);
            response.LessThanMaxDownloadRowCount = (rowCount <= MaxDownloadRowCount);
            response.RowCount = rowCount;
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1">数据对象类型</typeparam>
        /// <param name="response">响应数据对象</param>
        /// <param name="format"></param>
        /// <returns></returns>
        protected HttpResponseMessage GetResponse<T1>(T1 response, EmFormatType format) where T1 : ServerResponseBase, new()
        {
            response.ReponseTime = DateTime.Now;
            string data = SerializationHelper.Serialize(response, format);

            HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.OK);
            msg.Content = new StringContent(data, Encoding.GetEncoding("GB2312"));
            return msg;
        }
        /// <summary>
        /// Conver  Query result to Response result for reports Query
        /// </summary>
        /// <typeparam name="ResponseType">server response type ,for report,its main data is a list ResponseItemType</typeparam>
        /// <typeparam name="ResponseItemType">item data type of response</typeparam>
        /// <typeparam name="QueryResultItemType">data query result item type</typeparam>
        /// <param name="queryResultlist"></param>
        /// <returns></returns>
        protected ResponseType GetReportQueryResponse<ResponseType, ResponseItemType, QueryResultItemType>(ModelBase<List<QueryResultItemType>> queryResultlist)
            where ResponseType : ServerResponseBase<List<ResponseItemType>>, new()
            where ResponseItemType : class, new()
            where QueryResultItemType : class
        {
            ResponseType response = GetBusinessResponseDataInited<ResponseType>();
            if (queryResultlist != null)
            {
                if (queryResultlist.IsOverMaxRow)
                {
                    response = GetResponseError(ref response, EmBussinesCodeType.IsOverMaxRow);
                }
                else
                {
                    if (queryResultlist.Data != null)
                    {
                        foreach (var item in queryResultlist.Data)
                        {
                            ResponseItemType data = new ResponseItemType();
                            data.CopyModelValueFrom(item);
                            response.Data.Add(data);
                        }
                    }

                    if (queryResultlist.Pager != null)
                    {
                        response.Pager = new ResponsePager()
                        {
                            TotalCount = queryResultlist.Pager.TotalCount,
                        };
                    }
                }
            }
            else
            {
                response = GetResponseError(ref response, EmBussinesCodeType.ReportFixtureDatasError);
            }
            return response;
        }
        /// <summary>
        /// get download excel response
        /// </summary>
        /// <returns></returns>
        protected HttpResponseMessage GetDownloadExcelResponse<U>(List<U> datalist, List<TableHeader> tableHeaderList, string fileName = "report")
        {
            HttpResponseMessage fullResponse;
            //todo 抽象下载逻辑
            List<ExcelHelper.ClumnsHeaderMapper> mappers = null;
            if (tableHeaderList != null)
            {
                mappers = tableHeaderList
                    .Select(x => new ExcelHelper.ClumnsHeaderMapper()
                    {
                        HeaderOrder = x.HeaderOrder,
                        OriginalName = x.ColumnName,
                        NewName = x.HeaderLabel
                    }).ToList();
            }
            var excelHelper = new ExcelHelper();
            MemoryStream stream = excelHelper.Save(datalist, mappers);
            if (stream != null)
            {
                fullResponse = Request.CreateResponse(HttpStatusCode.OK);
                fullResponse.Content = new StreamContent(stream);

                //define file Name
                string fileFullName = excelHelper.GetFileName(fileName);
                string contentDisposition = string.Format("attachment;filename={0}", fileFullName);
                fullResponse.Content.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse(contentDisposition);
                fullResponse.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                return fullResponse;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// get download excel response
        /// </summary>
        /// <returns></returns>
        protected HttpResponseMessage GetDownloadExcelResponse(DataTable datatable, List<TableHeader> tableHeaderList, string fileName = "report")
        {
            HttpResponseMessage fullResponse;
            //todo 抽象下载逻辑
            List<ExcelHelper.ClumnsHeaderMapper> mappers = null;
            if (tableHeaderList != null)
            {
                mappers = tableHeaderList
                    .Select(x => new ExcelHelper.ClumnsHeaderMapper()
                    {
                        HeaderOrder = x.HeaderOrder,
                        OriginalName = x.ColumnName,
                        NewName = x.HeaderLabel
                    }).ToList();
            }
            var excelHelper = new ExcelHelper();
            MemoryStream stream = excelHelper.Save(datatable, mappers);
            if (stream != null)
            {
                fullResponse = Request.CreateResponse(HttpStatusCode.OK);
                fullResponse.Content = new StreamContent(stream);

                //define file Name
                string fileFullName = excelHelper.GetFileName(fileName);
                string contentDisposition = string.Format("attachment;filename={0}", fileFullName);
                fullResponse.Content.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse(contentDisposition);
                fullResponse.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                return fullResponse;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// GetModelQuery
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="queryClient"></param>
        /// <returns></returns>
        protected T2 GetModelQuery<T1, T2>(T1 queryClient)
            where T1 : ServerRequestBase, new()
            where T2 : ModelQueryBase, new()
        {
            T2 model = new T2();
            if (queryClient != null)
            {
                if (queryClient.Pager != null)
                {
                    model.Pager = new QueryPager();
                    model.Pager.CopyModelValueFrom(queryClient.Pager);
                }
                model.CopyModelValueFrom(queryClient.DataObj);
            }

            return model;
        }
        protected T2 GetModelQueryData<T1, T2,T3>(T1 queryClient)
       where T1 : ServerRequestBase, new()
       where T2 : ModelQueryBase<T3>, new()
        where T3 :new()
        {
            T2 model = new T2();
            if (queryClient != null)
            {
                if (queryClient.Pager != null)
                {
                    model.Pager = new QueryPager();
                    model.Pager.CopyModelValueFrom(queryClient.Pager);
                }
                model.Data = new T3();
                model.Data.CopyModelValue(queryClient.DataObj);
            }

            return model;
        }


        /// <summary>
        /// GetModelQuery
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="queryClient"></param>
        /// <returns></returns>
        protected T3 GetModelQuery<T1, T2, T3>(T1 queryClient)
            where T1 : ServerRequestBase<T2>, new()
            where T2 : class
            where T3 : ModelQueryBase, new()
        {
            T3 model = new T3();
            if (queryClient != null)
            {
                if (queryClient.Pager != null)
                {
                    model.Pager = new QueryPager();
                    model.Pager.CopyModelValueFrom(queryClient.Pager);
                }
                model.CopyModelValueFrom<T3, T2>(queryClient.Data);
            }
            return model;
        }

        /// <summary>
        /// GetResponseError
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="responseData"></param>
        /// <param name="bCode"></param>
        /// <returns></returns>
        protected T1 GetResponseError<T1>(ref T1 responseData, EmBussinesCodeType bCode)
            where T1 : ServerResponseBase
        {
            responseData.BussinesCode = bCode;
            return responseData;
        }
        /// <summary>
        /// GetResponseError
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="responseData"></param>
        /// <param name="ex"></param>
        /// <param name="bCode"></param>
        /// <returns></returns>
        protected T1 GetResponseError<T1>(ref T1 responseData, Exception ex, EmBussinesCodeType bCode)
            where T1 : ServerResponseBase
        {
            responseData.BussinesCode = bCode;
            responseData.GodMsg = ex.Message;
            return responseData;
        }


        /// <summary>
        ///  Create  GetResponseError
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="ex"></param>
        /// <param name="bCode"></param>
        /// <returns></returns>
        protected T1 GetResponseError<T1>(Exception ex, EmBussinesCodeType bCode)
            where T1 : ServerResponseBase, new()
        {
            T1 response = GetBusinessResponseDataInited<T1>();
            response.BussinesCode = bCode;
            response.GodMsg = ex.Message;
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1">数据对象类型</typeparam>
        /// <param name="format"></param>
        /// <returns></returns>
        protected T1 GetRequest<T1>(EmFormatType format) where T1 : class
        {
            Stream stream = Stream.Null;
            // 获取POST上来的内容
            HttpContent content = this.Request.Content;
            Task readTask = content.ReadAsStreamAsync().ContinueWith((task) => { stream = task.Result; });
            readTask.Wait();

            //bool isJson = this.Request.Content.Headers.ContentType.MediaType.ToLower().Contains("json");

            string data = string.Empty;
            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("GB2312")))
            {
                data = reader.ReadToEnd();
            }

            //if (this.Log.IsDebugEnabled)
            //{
            //    this.Log.DebugFormat("请求报文数据：{0}", data);
            //}

            T1 result = SerializationHelper.Deserialize<T1>(data, format);

            return result;
        }
        /// <summary>
        /// Max row count for download excel report ,base on the setting in web.config
        /// </summary>
        protected long MaxDownloadRowCount
        {
            get
            {
                return LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            }
        }
        /// <summary>
        /// if download row count bigger than this value ,need user confirm to continue.
        /// </summary>
        protected long CheckDownloadRowCount
        {
            get
            {
                return LocalConfiguration.Instance.ReportCheckDownloadRowCount();
            }
        }

        /// <summary>
        /// base function for downloading excel report 
        /// </summary>
        /// <typeparam name="QueryModelType"></typeparam>
        /// <typeparam name="ItemType"></typeparam>
        /// <param name="queryModel">queryt model,send paramar to query data</param>
        /// <param name="tableHeaders">table header to show </param>
        /// <param name="dwonloadfileName">excel file name </param>
        /// <param name="GetRowCountFunc">the function to get records count</param>
        /// <param name="GetDataFunc">the fucntion to get records</param>
        /// <returns></returns>
        protected HttpResponseMessage DownloadReportResponse<QueryModelType, ItemType>(
            QueryModelType queryModel,
            List<TableHeader> tableHeaders,
            Func<QueryModelType, ModelBase<List<ItemType>>> GetDataFunc,
            string dwonloadfileName
            ) where QueryModelType : ModelQueryBase, new()
        {
            HttpResponseMessage fullResponse;
            queryModel.Pager = null;
            var dbDatas = GetDataFunc(queryModel);
            if (dbDatas.Data.Count > 0)
            {
                fullResponse = GetDownloadExcelResponse(dbDatas.Data, tableHeaders, dwonloadfileName);
                if (fullResponse == null)
                {
                    fullResponse = Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                fullResponse= Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return fullResponse;
        }
        protected HttpResponseMessage DownloadReportResponse<QueryModelType, ItemType>(
            QueryModelType queryModel,
            List<TableHeader> tableHeaders,
            Func<QueryModelType, long> GetRowCountFunc,
            Func<QueryModelType, ModelBase<List<ItemType>>> GetDataFunc,
            string dwonloadfileName
            ) where QueryModelType : ModelQueryBase, new()
        {
            HttpResponseMessage fullResponse;
            long rowCount = GetRowCountFunc(queryModel);
            if (rowCount > 0 && rowCount < MaxDownloadRowCount)
            {
                queryModel.Pager = null;
                var dbDatas = GetDataFunc(queryModel);
                fullResponse = GetDownloadExcelResponse(dbDatas.Data, tableHeaders, dwonloadfileName);
                if (fullResponse == null)
                {
                    fullResponse = Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            else if (rowCount == 0)
            {
                //not available data
                fullResponse = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                //over max row limit
                fullResponse = Request.CreateResponse(HttpStatusCode.RequestEntityTooLarge);
            }
            return fullResponse;
        }


        /// <summary>
        /// get model data for function SaveMultiSheet
        /// </summary>
        /// <param name="tableHeaders"></param>
        /// <param name="dataList"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static ExcelHelper.SaveMultiSheetModel GetSaveMultiSheetModel(List<TableHeader> tableHeaders,List<object> dataList, string sheetName)
        {
            ExcelHelper.SaveMultiSheetModel SMsaveModel = new ExcelHelper.SaveMultiSheetModel()
            {
                ClumnsHeaderMapperList = tableHeaders
                   .Select(x => new ExcelHelper.ClumnsHeaderMapper()
                   {
                       HeaderOrder = x.HeaderOrder,
                       OriginalName = x.ColumnName,
                       NewName = x.HeaderLabel
                   }).ToList(),
                DataList = dataList,
                SheetName = sheetName
            };
            return SMsaveModel;
        }
        /// <summary>
        /// GetTest
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public string GetTest()
        //{
        //    return "Base Test";
        //}
    }
}
