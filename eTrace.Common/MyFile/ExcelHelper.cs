using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Common
{
    public class ExcelHelper
    {
        private static ExcelHelper _instance = new ExcelHelper();
        public static ExcelHelper Instance
        {
            get { return _instance; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable">dataTable</param>
        /// <param name="sheeetName">Sheet Name</param>
        /// <returns></returns>
        public MemoryStream Save(DataTable dataTable, List<ClumnsHeaderMapper> clumnsHeaderMappers, string sheeetName = "")
        {
            try
            {

                //文件流对象
                MemoryStream stream = new MemoryStream();
                if (dataTable == null)
                {
                    return stream;
                }
                //打开Excel对象
                XSSFWorkbook workbook = new XSSFWorkbook();

                //Excel的Sheet对象
                sheeetName = sheeetName != "" ? sheeetName : "sheet1";
                var sheet = workbook.CreateSheet(sheeetName);

                //set date format
                var cellStyle = workbook.CreateCellStyle();
                var cellFont = workbook.CreateFont();
                cellFont.FontHeightInPoints = 9;
                cellStyle.SetFont(cellFont);
                var format = workbook.CreateDataFormat();

                //set date  format
                var dateStyle = workbook.CreateCellStyle();
                var dateformat = workbook.CreateDataFormat();
                dateStyle.DataFormat = dateformat.GetFormat("yyyy-MM-dd HH:mm:ss");
                //header style
                var headerStyle = workbook.CreateCellStyle();
                var headerFont = workbook.CreateFont();
                headerFont.Boldweight = (short)FontBoldWeight.Bold;
                headerFont.FontHeightInPoints = 9;
                headerStyle.SetFont(headerFont);


                //使用NPOI操作Excel表
                var row = sheet.CreateRow(0);
                int count = 0;
                for (int i = 0; i < dataTable.Columns.Count; i++) //生成sheet第一行列名
                {
                        
                    var cell = row.CreateCell(count++);
                    cell.CellStyle = headerStyle;
                    var mapper = clumnsHeaderMappers.First(x => x.OriginalName == dataTable.Columns[i].Caption);
                    if (mapper != null)
                    {
                        cell.SetCellValue(mapper.NewName);
                    }
                    else
                    {
                        cell.SetCellValue(dataTable.Columns[i].Caption);
                    }
                }
                //将数据导入到excel表中
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    var rows = sheet.CreateRow(i + 1);
                    count = 0;
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                       
                        var cell = rows.CreateCell(count++);
                        Type type = dataTable.Rows[i][j].GetType();

                        if (dataTable.Rows[i][j] == null)
                        {
                            continue;
                        }
                        if (type == typeof(int) || type == typeof(Int16)
                            || type == typeof(Int32) || type == typeof(Int64))
                           {
                                cell.SetCellValue(Convert.ToInt32(dataTable.Rows[i][j]));
                            }
                           else
                          {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                    cell.SetCellValue((Double)dataTable.Rows[i][j]);
                            }
                            else if (type == typeof(decimal) || type == typeof(Decimal))
                            {
                                cell.SetCellValue(Convert.ToDouble(dataTable.Rows[i][j]));
                            }
                            else
                            {
                                if (type == typeof(DateTime) && dataTable.Rows[i][j] != null)
                                { 
                                    cell.SetCellValue(((DateTime)dataTable.Rows[i][j]));
                                    cell.CellStyle = dateStyle;
                                    //cellStyleDate.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");
                                    //cell.CellStyle = cellStyleDate;
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dataTable.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                    }
                                }
                            }
                         }



                    }
                }
                //保存excel文档
                AutoColumnWidth(sheet);
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //stream.Flush();
                //stream.Position = 0;
                workbook = null;
                //workbook.Dispose();

                return new MemoryStream(stream.ToArray());
            }
            catch (Exception ex)
            {
                return new MemoryStream();
            }
        }


        /// <summary>
        /// 创建多个Sheet的数据流
        /// </summary>
        /// <param name="modelList">每个sheet 需要创建一个一个SaveMultiSheetModel对象</param>
        /// <returns></returns>
        public MemoryStream SaveMultiSheet(List<SaveMultiSheetModel> modelList)
        {

            //文件流对象
            MemoryStream stream = new MemoryStream();
            //打开Excel对象
            XSSFWorkbook workbook = new XSSFWorkbook();

            try
            {
                //set date format
                var cellStyle = workbook.CreateCellStyle();
                var cellFont = workbook.CreateFont();
                cellFont.FontHeightInPoints = 9;
                cellStyle.SetFont(cellFont);
                var format = workbook.CreateDataFormat();
                //set date  format
                var dateStyle = workbook.CreateCellStyle();
                var dateformat = workbook.CreateDataFormat();
                dateStyle.DataFormat = dateformat.GetFormat("yyyy-MM-dd HH:mm:ss");
                //header style
                var headerStyle = workbook.CreateCellStyle();
                var headerFont = workbook.CreateFont();
                headerFont.Boldweight = (short)FontBoldWeight.Bold;
                headerFont.FontHeightInPoints = 9;
                headerStyle.SetFont(headerFont);

                foreach (var model in modelList)
                {
                    var dataList = model.DataList;
                    var clumnsHeaderMappers = model.ClumnsHeaderMapperList;
                    var sheetName = model.SheetName;

                    if (dataList == null)
                    {
                        continue;
                    }
                    if (dataList.Count == 0)
                    {
                        continue;
                    }
                    //Excel的Sheet对象
                    sheetName = sheetName != "" ? sheetName : "sheet1";
                    var sheet = workbook.CreateSheet(sheetName);
               

                    //使用NPOI操作Excel表
                    var row = sheet.CreateRow(0);
                    int count = 0;
                    //生成sheet第一行列名
                    Type modelType = dataList.First().GetType();
                    PropertyInfo[] propertys = modelType.GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
                    if (clumnsHeaderMappers == null)
                    {
                        clumnsHeaderMappers = new List<ClumnsHeaderMapper>();
                        for (int i = 0; i < propertys.Length; i++)
                        {
                            clumnsHeaderMappers.Add(new ClumnsHeaderMapper()
                            {
                                HeaderOrder = i,
                                NewName = propertys[i].Name,
                                OriginalName = propertys[i].Name
                            });
                        }
                    }
                    var headerMappersInOrder = clumnsHeaderMappers.OrderBy(x => x.HeaderOrder).ToList();
                    foreach (var item in headerMappersInOrder)
                    {
                        var property = propertys.FirstOrDefault(x => x.Name == item.OriginalName);
                        if (property != null)
                        {
                            var cell = row.CreateCell(count++);
                            cell.SetCellValue(item.NewName);
                            cell.CellStyle = headerStyle;
                        }
                    }
                    //将数据导入到excel表中
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        var rows = sheet.CreateRow(i + 1);
                        count = 0;
                        foreach (var item in headerMappersInOrder)
                        {
                            var property = propertys.FirstOrDefault(x => x.Name == item.OriginalName);
                            if (property != null)
                            {
                                var cell = rows.CreateCell(count++);
                                Type type = GetCoreType(property.PropertyType);
                                var value = property.GetValue(dataList[i], null);

                                if (value == null)
                                {
                                    continue;
                                }
                                if (type == typeof(int) || type == typeof(Int16)
                                        || type == typeof(Int32) || type == typeof(Int64))
                                {
                                   cell.SetCellValue(Convert.ToInt32(value));
                                }
                                else
                                {
                                        if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                                        {
                                            cell.SetCellValue((Double)value);
                                        }
                                        else if (type == typeof(decimal) || type == typeof(Decimal))
                                        {
                                            cell.SetCellValue(Convert.ToDouble(value));
                                        }
                                    else
                                        {
                                            if (type == typeof(DateTime))
                                            {
                                            cell.SetCellValue(((DateTime)value));
                                            //cellStyleDate.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");
                                            //cell.CellStyle = cellStyleDate;
                                            cell.CellStyle = dateStyle;
                                        }
                                            else
                                            {
                                                if (type == typeof(bool) || type == typeof(Boolean))
                                                {
                                                    cell.SetCellValue((bool)value);
                                                }
                                                else
                                                {
                                                    cell.SetCellValue(value.ToString());
                                                }
                                            }
                                        }
                                    }

                             

                            }
                        }
                    }
                    AutoColumnWidth(sheet);
                    //保存excel文档
                    sheet.ForceFormulaRecalculation = true;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            workbook.Write(stream);
            workbook = null;
            var returnStream = new MemoryStream(stream.ToArray());
            returnStream.Seek(0, System.IO.SeekOrigin.Begin);
            return returnStream;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataList">dataList</param>
        /// <param name="sheeetName">Sheet Name</param>
        /// <returns></returns>
        public MemoryStream Save<T>(List<T> dataList, List<ClumnsHeaderMapper> clumnsHeaderMappers, string sheeetName = "")
        {
            long counterErr = 0;
            try
            {
                //文件流对象
                MemoryStream stream = new MemoryStream();
                if (dataList == null)
                {
                    return stream;
                }
                if (dataList.Count == 0)
                {
                    return stream;
                }
                //打开Excel对象
                XSSFWorkbook workbook = new XSSFWorkbook();
                //Excel的Sheet对象
                sheeetName = sheeetName != "" ? sheeetName : "sheet1";
                var sheet = workbook.CreateSheet("sheet1");
                //set   format
                var cellStyle = workbook.CreateCellStyle();
                cellStyle.DataFormat = 0;
                var cellFont = workbook.CreateFont();
                cellFont.FontHeightInPoints = 9;
                cellStyle.SetFont(cellFont);
                var Cellformat = workbook.CreateDataFormat();

                //set date  format
                var dateStyle = workbook.CreateCellStyle();  
                var dateformat = workbook.CreateDataFormat();
                dateStyle.DataFormat = dateformat.GetFormat("yyyy-MM-dd HH:mm:ss");
                //header style
                var headerStyle = workbook.CreateCellStyle();
                var headerFont = workbook.CreateFont();
                headerFont.Boldweight = (short)FontBoldWeight.Bold;
                headerFont.FontHeightInPoints = 9;
                headerStyle.SetFont(headerFont);
                //创建字典存放列宽最大值
                //Dictionary<string, int> dicColMaxWidth = new Dictionary<string, int>();
                //使用NPOI操作Excel表
                var row = sheet.CreateRow(0);
                int count = 0;
                //生成sheet第一行列名
                Type modelType = dataList.First().GetType();
                PropertyInfo[] propertys = modelType.GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
                if (clumnsHeaderMappers == null)
                {
                    
                    clumnsHeaderMappers = new List<ClumnsHeaderMapper>();
                    for (int i = 0; i < propertys.Length; i++)
                    {

                        clumnsHeaderMappers.Add(new ClumnsHeaderMapper()
                        {
                            HeaderOrder = i,
                            NewName = propertys[i].Name,
                            OriginalName = propertys[i].Name
                        });
                    }
                }
                var headerMappersInOrder = clumnsHeaderMappers.OrderBy(x => x.HeaderOrder).ToList();
                foreach (var item in headerMappersInOrder)
                {
                    var property = propertys.FirstOrDefault(x => x.Name == item.OriginalName);
                    if (property != null)
                    {
                        var cell = row.CreateCell(count++);
                        cell.SetCellValue(item.NewName);
                        cell.CellStyle = headerStyle;
                        //int contextLength = Encoding.UTF8.GetBytes(item.NewName).Length;
                        //dicColMaxWidth.Add(item.OriginalName, contextLength);
                    }
                }
                //将数据导入到excel表中
                for (int i = 0; i < dataList.Count; i++)
                {
                    var rows = sheet.CreateRow(i + 1);
                    count = 0;
                    foreach (var item in headerMappersInOrder)
                    {
                        var property = propertys.FirstOrDefault(x => x.Name == item.OriginalName);
                        if (property != null)
                        {
                            counterErr = i;
                            var cell = rows.CreateCell(count++);
                            //cellStyle.DataFormat = 0;
                            cell.CellStyle = cellStyle;
                            Console.WriteLine(count);
                            Type type = GetCoreType(property.PropertyType);
                            var value = property.GetValue(dataList[i], null);
                            //int contextLength = Encoding.UTF8.GetBytes(value.ToString()).Length;
                            //if (contextLength > dicColMaxWidth[item.OriginalName])
                            //{
                            //    dicColMaxWidth.Add(item.OriginalName, contextLength);
                            //}
                            if (value == null)
                            {
                                continue;
                            }
                            if (type == typeof(int) || type == typeof(Int16)
                                || type == typeof(Int32) || type == typeof(Int64))
                            {
                                cell.SetCellValue(Convert.ToInt32(value));
                            }
                            else
                            {
                                if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                                {
                                    cell.SetCellValue((Double)value);
                                }
                                else if (type == typeof(decimal) || type == typeof(Decimal))
                                {
                                    cell.SetCellValue(Convert.ToDouble(value));
                                }
                                else
                                {
                                    if (type == typeof(DateTime) && value != null)
                                    {  
                                        cell.CellStyle = dateStyle;
                                        cell.SetCellValue(((DateTime)value));
                                        //cell.SetCellValue(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
                                    }
                                    else
                                    {
                                        if (type == typeof(bool) || type == typeof(Boolean))
                                        {
                                            cell.SetCellValue((bool)value);
                                        }
                                        else
                                        {
                                            cell.SetCellValue(value.ToString());
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                AutoColumnWidth(sheet);
                //保存excel文档
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                workbook = null;
                var returnStream = new MemoryStream(stream.ToArray());
                returnStream.Seek(0, System.IO.SeekOrigin.Begin);
                return returnStream;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 自适应宽度
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="cols">列数</param>
        public void AutoColumnWidth(ISheet sheet)
        {
            if (sheet.LastRowNum > 0)
            {

                //IRow row = null;
                //ICell cell = null;
                for (int col = 0; col < sheet.GetRow(0).LastCellNum; col++)
                {
                    sheet.AutoSizeColumn(col);//自适应宽度，但是其实还是比实际文本要宽
                    ////int columnWidth = sheet.GetColumnWidth(col) / 256;//获取当前列宽度
                    // row = sheet.GetRow(0);
                    // cell = row.GetCell(col);
                    //int contextLength = Encoding.UTF8.GetBytes(cell.ToString()).Length;//获取当前单元格的内容宽度
                    //int columnWidth = contextLength;
                    //for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                    //{
                    //     row = sheet.GetRow(rowIndex);
                    //     cell = row.GetCell(col);
                    //     contextLength = Encoding.UTF8.GetBytes(cell.ToString()).Length;//获取当前单元格的内容宽度
                    //    columnWidth = columnWidth < contextLength ? contextLength : columnWidth;
                    //    //columnWidth = contextLength;
                    //}
                    //sheet.SetColumnWidth(col, (columnWidth+2) * 275);//

                }
            }
        }
        /// <summary>
        /// Create File Name for Donwload file
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string GetFileName(string reportType)
        {
            DateTime now = DateTime.Now;
            return reportType + now.ToString("yyyyMMddhhmm") + ".xlsx";
        }
        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        private Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        private bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }


        /// <summary>
        /// Map field name in table to Clumns Name show to uses
        /// </summary>
        public class ClumnsHeaderMapper
        {

            public string OriginalName { get; set; }
            public string NewName { get; set; }
            public int HeaderOrder { get; set; }

        }
        /// <summary>
        /// 调用SaveMultiSheet时请求Model
        /// </summary>
        public class SaveMultiSheetModel
        {
            public string SheetName { get; set; }
            public List<ClumnsHeaderMapper> ClumnsHeaderMapperList { get; set; }

            public List<object> DataList { get; set; }
        }

    }
}
