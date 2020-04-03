using eTrace.Report.BLL;
using eTrace.Report.IBLL;
using eTrace.Model;
using eTrace.Model.V2.Report;
using eTrace.Service.SDKForNet;
using eTrace.Service.SDKForNet.Request;
using eTrace.Service.SDKForNet.Request.Reports;
using eTrace.Service.SDKForNet.Response;
using eTrace.Service.SDKForNet.Response.Reports;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eTrace.Common;
using System.IO;
using System.Net.Http.Headers;
using eTrace.Report.BLL.Business;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Configuration;
using eTrace.Service.SDKForNet.Request.Reports.Common;
using eTrace.Common.Mail;
using System.Drawing;
using System.Web;
using System.Drawing.Imaging;

namespace eTraceWebApi.Controllers
{
    /// <summary>
    /// ReportProductModuleController
    /// </summary>
    public class ReportMobileEquipmentController : ServerBaseController<ReportMobileEquipmentController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportMobileEquipmentController()
        {

        }

        /// <summary>
        /// Get Mobile Equipment Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportMobileEquipmentResponse GetMobileEquipmentDatas([FromBody] ReportMobileEquipmentRequest requestData)
        {
            ReportMobileEquipmentResponse response = GetBusinessResponseDataInited<ReportMobileEquipmentResponse>();
            try
            {
                ReportMobileEquipmentQuery queryModel = GetModelQuery<ReportMobileEquipmentRequest, ReportMobileEquipmentQuery>(requestData);

                ReportMobileEquipmentModel dbDatas = MobileEquipmentBLL.Instance.GetMobileEquipmentData(queryModel);
                var data = new ReportMobileEquipmentResponse.Item();
                data.CopyModelValueFrom<ReportMobileEquipmentResponse.Item, ReportMobileEquipmentModel.Item>(dbDatas.Data.FirstOrDefault());
                response.Data = data;

            }
            catch (Exception ex)
            {
                //pocceed in GlobleExceptionFilter
                throw ex;
            }
            return response;
        }


        [HttpPost]
        public HttpResponseMessage UploadFile()
        {
            string Month, Day, Hour, Minute, Second, Millisecond;
             var content = Request.Content;
            var uploadDir = ConfigurationManager.AppSettings["ImgFolder"];//"D:\\My Documents\\My Music";
            var newFileName = "";
            var filename = "";

            var sp = new MultipartMemoryStreamProvider();
            Task.Run(async () => await Request.Content.ReadAsMultipartAsync(sp)).Wait();
            foreach (var item in sp.Contents)
            {
                if (item.Headers.ContentDisposition.FileName != null)
                {
                    filename = item.Headers.ContentDisposition.FileName.Replace("\"", "");

                    if (DateTime.Now.Month.ToString().Length == 1)
                    {
                        Month = "0" + DateTime.Now.Month.ToString();
                    }
                    else
                    {
                        Month= DateTime.Now.Month.ToString();
                    }
                    if (DateTime.Now.Day.ToString().Length == 1)
                    {
                        Day = "0" + DateTime.Now.Day.ToString();
                    }
                    else
                    {
                        Day = DateTime.Now.Day.ToString();
                    }
                    if (DateTime.Now.Hour.ToString().Length == 1)
                    {
                        Hour = "0" + DateTime.Now.Hour.ToString();
                    }
                    else
                    {
                        Hour = DateTime.Now.Hour.ToString();
                    }
                    if (DateTime.Now.Minute.ToString().Length == 1)
                    {
                        Minute = "0" + DateTime.Now.Minute.ToString();
                    }
                    else
                    {
                        Minute = DateTime.Now.Minute.ToString();
                    }
                    if (DateTime.Now.Second.ToString().Length == 1)
                    {
                        Second = "0" + DateTime.Now.Second.ToString();
                    }
                    else
                    {
                        Second = DateTime.Now.Second.ToString();
                    }

                    string Strdate = DateTime.Now.Year.ToString() + Month + Day + Hour + Minute + Second + DateTime.Now.Millisecond.ToString();
                    filename = System.Web.HttpContext.Current.Request.Form[0]+"__"+ Strdate+"__"+ filename;
                    newFileName = uploadDir + "\\" + filename;
                    var ms = item.ReadAsStreamAsync().Result;
                    using (var br = new BinaryReader(ms))
                    {
                        var data = br.ReadBytes((int)ms.Length);
                        File.WriteAllBytes(newFileName, data);
                    }
                }
            }

            var result = new Dictionary<string, string>();
            result.Add("retfilename", filename);  
            var resp = Request.CreateResponse(HttpStatusCode.OK, result);
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            CompressImage("D:\\My Documents\\My Music\\123.jpg", "D:\\My Documents\\My Music\\Test\\123.jpg", 50, 1024, true);
            return resp;
        }

        /// <summary>
        /// 发送用户建议
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServerResponseBase<bool> SendRemindMail(ReportMobileEquipmentRequest request)
        {
            try
            {
                ServerResponseBase<bool> rt = new ServerResponseBase<bool>();

                rt.Data = MobileEquipmentBLL.Instance.InsertMobileItem(new ReportMobileEquipmentQuery
                {
                    EquipmentID = request.Data.EquipmentID,
                    Location = request.Data.Location,
                    Description = request.Data.Description,
                    PictureURL= request.Data.PictureURL
                });

                string Imagefilename = ConfigurationManager.AppSettings["ImgFolder"] + request.Data.PictureURL;

                string module = "eTraceReport";
                //string[] mailTo = new string[] { "Lison-SP.Li@artesyn.com" };
                string[] mailTo = new string[] { };
                mailTo = request.Data.MailGroup.Split(',');
                string mailFrom = "eTrace." + module + "@artesyn.com";
                string mailSubject = "eTrace Mobile Equipment";
                Dictionary<string, string> dicMessage = new Dictionary<string, string>();
                dicMessage.Add("Item", "Content");
                dicMessage.Add("Equipment ID", request.Data.EquipmentID);
                dicMessage.Add("Location", request.Data.Location);
                dicMessage.Add("Remark", request.Data.Description);

                string message = @"<body style='font-size:90%;font-family:Calibri'><p>" + "Hi User, " + "</p>" +
                             "<p>" + "This information comes from the mobile terminal," + "</p>" +
                              "</body>";
                message = message + MailHelper.ExportDictionaryToHtml(dicMessage);

                rt.Data = MailHelper.SendMail(mailFrom, mailTo.ToArray(), mailSubject, message, Imagefilename);
                return rt;
            }
            catch (Exception ex)
            {
                eTraceOracleERP.eTraceOracleERPSoapClient WS = new eTraceOracleERP.eTraceOracleERPSoapClient();

                WS.ErrorLog("eTraceReport-CommonContorller-SendRemindMail", "", ex.ToString(), "");

                ServerResponseBase<bool> rt = new ServerResponseBase<bool>();
                return rt;
            }

        }

        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片地址</param>
        /// <param name="dFile">压缩后保存图片地址</param>
        /// <param name="flag">压缩质量（数字越小压缩率越高）1-100</param>
        /// <param name="size">压缩后图片的最大大小</param>
        /// <param name="sfsc">是否是第一次调用</param>
        /// <returns></returns>
        public static bool CompressImage(string sFile, string dFile, int flag = 90, int size = 300, bool sfsc = true)
        {
            //如果是第一次调用，原始图像的大小小于要压缩的大小，则直接复制文件，并且返回true
            FileInfo firstFileInfo = new FileInfo(sFile);
            if (sfsc == true && firstFileInfo.Length < size * 1024)
            {
                firstFileInfo.CopyTo(dFile);
                return true;
            }
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int dHeight = iSource.Height / 2;
            int dWidth = iSource.Width / 2;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                    FileInfo fi = new FileInfo(dFile);
                    if (fi.Length > 1024 * size)
                    {
                        flag = flag - 10;
                        CompressImage(sFile, dFile, flag, size, false);
                    }
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
                //删除原图片
                File.Delete(sFile);
            }
        }

    }
}
