using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace FarmMonitor.Common
{
    public class ExcelExtensions
    {

        #region 从Excel中加载数据（泛型）+IEnumerable<T> LoadFromExcel<T>(string FileName, int index) where T : new()
        /// <summary>
        /// 从Excel中加载数据（泛型）
        /// </summary>
        /// <typeparam name="T">每行数据的类型</typeparam>
        /// <param name="FileName">Excel文件名</param>
        /// <returns>泛型列表</returns>
        public static List<T> LoadFromExcel<T>(string FileName, int index) where T : new()
        {
            FileInfo existingFile = new FileInfo(FileName);
            List<T> resultList = new List<T>();
            Dictionary<string, int> dictHeader = new Dictionary<string, int>();

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[index];

                int colStart = worksheet.Dimension.Start.Column;  //工作区开始列
                int colEnd = worksheet.Dimension.End.Column;       //工作区结束列
                int rowStart = worksheet.Dimension.Start.Row;       //工作区开始行号
                int rowEnd = worksheet.Dimension.End.Row;       //工作区结束行号

                //将每列标题添加到字典中
                for (int i = colStart; i <= colEnd; i++)
                {
                    dictHeader[worksheet.Cells[rowStart, i].Value.ToString()] = i;
                }
                //dictHeader.Add("Id",0);

                List<PropertyInfo> propertyInfoList = new List<PropertyInfo>(typeof(T).GetProperties());

                for (int row = rowStart + 1; row <= rowEnd; row++)
                {
                    T result = new T();

                    //为对象T的各属性赋值
                    int num = 0;
                    foreach (PropertyInfo p in propertyInfoList)
                    {
                        num++;
                        if (num >= 2)
                        {
                            try
                            {
                                ExcelRange cell = worksheet.Cells[row, dictHeader[p.Name]]; //与属性名对应的单元格
                                string val = cell.Value + "";
                                if (string.IsNullOrEmpty(val))
                                    continue;
                                string propName = p.PropertyType.Name.ToLower();
                                if (propName.Contains("nullable"))
                                {
                                    propName = p.PropertyType.FullName.ToLower();
                                    propName = propName.Substring(propName.IndexOf("[[") + 2);
                                    propName = propName.Substring(propName.IndexOf(".") + 1);
                                    propName = propName.Substring(0, propName.IndexOf(","));
                                }
                                switch (propName)
                                {
                                    case "string":
                                        p.SetValue(result, cell.GetValue<String>());
                                        break;
                                    case "int16":
                                        p.SetValue(result, Convert.ToInt16(cell.GetValue<String>()));
                                        break;
                                    case "int32":
                                        p.SetValue(result, Convert.ToInt32(cell.GetValue<String>()));
                                        break;
                                    case "int64":
                                        p.SetValue(result, Convert.ToInt64(cell.GetValue<String>()));
                                        break;
                                    case "decimal":
                                        p.SetValue(result, Convert.ToDecimal(cell.GetValue<String>()));
                                        break;
                                    case "double":
                                        p.SetValue(result, Convert.ToDouble(cell.GetValue<String>()));
                                        break;
                                    case "datetime":
                                        p.SetValue(result, Convert.ToDateTime(cell.GetValue<String>()));
                                        break;
                                    case "boolean":
                                        p.SetValue(result, Convert.ToBoolean(cell.GetValue<String>()));
                                        break;
                                    case "byte":
                                        p.SetValue(result, cell.GetValue<Byte>());
                                        break;
                                    case "char":
                                        p.SetValue(result, cell.GetValue<Char>());
                                        break;
                                    case "single":
                                        p.SetValue(result, Convert.ToSingle(cell.GetValue<Single>()));
                                        break;
                                    default:
                                        break;
                                }

                            }
                            catch (KeyNotFoundException ex)
                            { }
                        }
                    }
                    resultList.Add(result);
                }
            }
            return resultList;
        }

        public static List<T> LoadFromExcelFun<T>(string FileName, int index) where T : new()
        {
            FileInfo existingFile = new FileInfo(FileName);
            List<T> resultList = new List<T>();
            Dictionary<string, int> dictHeader = new Dictionary<string, int>();

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[index];

                int colStart = worksheet.Dimension.Start.Column;  //工作区开始列
                int colEnd = worksheet.Dimension.End.Column;       //工作区结束列
                int rowStart = worksheet.Dimension.Start.Row;       //工作区开始行号
                int rowEnd = worksheet.Dimension.End.Row;       //工作区结束行号

                //将每列标题添加到字典中
                for (int i = colStart; i <= colEnd; i++)
                {
                    // string str = worksheet.Cells[rowStart, i].Value.ToString();
                    dictHeader[worksheet.Cells[rowStart, i].Value.ToString().Trim().Replace(" ", "_")] = i;

                }
                //dictHeader.Add("Id",0);

                List<PropertyInfo> propertyInfoList = new List<PropertyInfo>(typeof(T).GetProperties());

                for (int row = rowStart + 1; row <= rowEnd; row++)
                {
                    T result = new T();

                    //为对象T的各属性赋值
                    int num = 0;
                    foreach (PropertyInfo p in propertyInfoList)
                    {
                        //num++;
                        //if (num >= 2)
                        //{
                        #region
                        try
                        {
                            ExcelRange cell = worksheet.Cells[row, dictHeader[p.Name]]; //与属性名对应的单元格
                            string val = cell.Value + "";
                            if (string.IsNullOrEmpty(val))
                                continue;
                            string propName = p.PropertyType.Name.ToLower();
                            if (propName.Contains("nullable"))
                            {
                                propName = p.PropertyType.FullName.ToLower();
                                propName = propName.Substring(propName.IndexOf("[[") + 2);
                                propName = propName.Substring(propName.IndexOf(".") + 1);
                                propName = propName.Substring(0, propName.IndexOf(","));
                            }
                            switch (propName)
                            {
                                case "string":
                                    p.SetValue(result, cell.GetValue<String>());
                                    break;
                                case "int16":
                                    p.SetValue(result, Convert.ToInt16(cell.GetValue<String>()));
                                    break;
                                case "int32":
                                    p.SetValue(result, Convert.ToInt32(cell.GetValue<String>()));
                                    break;
                                case "int64":
                                    p.SetValue(result, Convert.ToInt64(cell.GetValue<String>()));
                                    break;
                                case "decimal":
                                    p.SetValue(result, Convert.ToDecimal(cell.GetValue<String>()));
                                    break;
                                case "double":
                                    p.SetValue(result, Convert.ToDouble(cell.GetValue<String>()));
                                    break;
                                case "datetime":
                                    p.SetValue(result, Convert.ToDateTime(cell.GetValue<String>()));
                                    break;
                                case "boolean":
                                    p.SetValue(result, Convert.ToBoolean(cell.GetValue<String>()));
                                    break;
                                case "byte":
                                    p.SetValue(result, cell.GetValue<Byte>());
                                    break;
                                case "char":
                                    p.SetValue(result, cell.GetValue<Char>());
                                    break;
                                case "single":
                                    p.SetValue(result, Convert.ToSingle(cell.GetValue<Single>()));
                                    break;
                                default:
                                    break;
                            }

                        }
                        catch (KeyNotFoundException ex)
                        { }
                        #endregion
                        //}
                    }
                    resultList.Add(result);
                }
            }
            return resultList;
        }
        public static List<T> LoadFromExcelFun2<T>(string FileName, int index) where T : new()
        {
            FileInfo existingFile = new FileInfo(FileName);
            List<T> resultList = new List<T>();
            Dictionary<string, int> dictHeader = new Dictionary<string, int>();

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[index];

                int colStart = worksheet.Dimension.Start.Column;  //工作区开始列
                int colEnd = worksheet.Dimension.End.Column;       //工作区结束列
                int rowStart = worksheet.Dimension.Start.Row;       //工作区开始行号
                int rowEnd = worksheet.Dimension.End.Row;       //工作区结束行号

                //将每列标题添加到字典中
                for (int i = colStart; i <= colEnd; i++)
                {
                    // string str = worksheet.Cells[rowStart, i].Value.ToString();
                    dictHeader[worksheet.Cells[rowStart, i].Value.ToString().Trim().Replace(" ", "_").Replace("(", "").Replace(")", "").Replace(".", "").Replace("-", "").Replace(":", "")] = i;


                }
                List<PropertyInfo> propertyInfoList = new List<PropertyInfo>(typeof(T).GetProperties());

                for (int row = rowStart + 1; row <= rowEnd; row++)
                {
                    T result = new T();

                    //为对象T的各属性赋值
                    int num = 0;
                    foreach (PropertyInfo p in propertyInfoList)
                    {
                        num++;
                        if (num >= 2)
                        {
                            #region
                            try
                            {
                                ExcelRange cell = worksheet.Cells[row, dictHeader[p.Name]]; //与属性名对应的单元格
                                string val = cell.Value + "";
                                if (string.IsNullOrEmpty(val))
                                    continue;
                                string propName = p.PropertyType.Name.ToLower();
                                if (propName.Contains("nullable"))
                                {
                                    propName = p.PropertyType.FullName.ToLower();
                                    propName = propName.Substring(propName.IndexOf("[[") + 2);
                                    propName = propName.Substring(propName.IndexOf(".") + 1);
                                    propName = propName.Substring(0, propName.IndexOf(","));
                                }
                                switch (propName)
                                {
                                    case "string":
                                        p.SetValue(result, cell.GetValue<String>());
                                        break;
                                    case "int16":
                                        p.SetValue(result, Convert.ToInt16(cell.GetValue<String>()));
                                        break;
                                    case "int32":
                                        p.SetValue(result, Convert.ToInt32(cell.GetValue<String>()));
                                        break;
                                    case "int64":
                                        p.SetValue(result, Convert.ToInt64(cell.GetValue<String>()));
                                        break;
                                    case "decimal":
                                        p.SetValue(result, Convert.ToDecimal(cell.GetValue<String>()));
                                        break;
                                    case "double":
                                        p.SetValue(result, Convert.ToDouble(cell.GetValue<String>()));
                                        break;
                                    case "datetime":
                                        p.SetValue(result, Convert.ToDateTime(cell.GetValue<String>()));
                                        break;
                                    case "boolean":
                                        p.SetValue(result, Convert.ToBoolean(cell.GetValue<String>()));
                                        break;
                                    case "byte":
                                        p.SetValue(result, cell.GetValue<Byte>());
                                        break;
                                    case "char":
                                        p.SetValue(result, cell.GetValue<Char>());
                                        break;
                                    case "single":
                                        p.SetValue(result, Convert.ToSingle(cell.GetValue<Single>()));
                                        break;
                                    default:
                                        break;
                                }

                            }
                            catch (KeyNotFoundException ex)
                            { }
                            #endregion
                        }
                    }
                    resultList.Add(result);
                }
            }
            return resultList;
        }
        #endregion

        /// <summary>
        /// 将dataSet导出到excel
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        public static void ImportExcel(DataSet ds, string fileName, string[] sheetName)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                int count = 0;
                foreach (DataTable dt in ds.Tables)
                {
                    CreateSheet(package, dt, sheetName[count]);
                    count++;
                }
                //package.Workbook.Worksheets[0].Column(1).Hidden = false;
                string fName = HttpUtility.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmsss") + fileName + ".xlsx");
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fName);
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.BinaryWrite(package.GetAsByteArray());
                //ep.SaveAs(Response.OutputStream);    第二种方式
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 将dataSet导出到excel
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        public static void ImportExcel2(DataSet ds, string fileName, List<string> titleList, string[] sheetName)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                int count = 0;
                foreach (DataTable dt in ds.Tables)
                {
                    CreateSheet2(package, dt, titleList[count], sheetName[count]);
                    count++;
                }
                //package.Workbook.Worksheets[0].Column(1).Hidden = false;
                string fName = HttpUtility.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmsss") + fileName + ".xlsx");
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fName);
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.BinaryWrite(package.GetAsByteArray());
                //ep.SaveAs(Response.OutputStream);    第二种方式
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, string connectionString, IDataParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 从dataTable创建sheet
        /// </summary>
        /// <param name="package"></param>
        /// <param name="dt"></param>
        /// <param name="name"></param>
        private static void CreateSheet2(ExcelPackage package, DataTable dt, string title, string name)
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(name);
            int i = 0;

            List<string> colNameList = title.Split(',').ToList();

            foreach (DataColumn col in dt.Columns)
            {
                i++;
                worksheet.InsertColumn(i, i);
                worksheet.Cells[1, i].Value = colNameList[i - 1].Trim(); //col.ColumnName;
                //把ReadOnly的列设置为隐藏列
                if (col.ReadOnly)
                {
                    worksheet.Column(i).Hidden = true;
                }
                //设置宽度
                if (col.ExtendedProperties.ContainsKey("width"))
                {
                    int width = 20;
                    if (int.TryParse(col.ExtendedProperties["width"] + "", out width))
                    {
                        worksheet.Column(i).Width = width;
                    }
                }
            }
            // dt.Rows.Remove(dt.Rows[0]);
            int rowIndex = 1;
            foreach (DataRow row in dt.Rows)
            {
                rowIndex++;

                for (int i2 = 0; i2 < i; i2++)
                {
                    worksheet.Cells[rowIndex, i2 + 1].Value = row[i2].ToString();
                }

            }
        }

        private static void CreateSheet(ExcelPackage package, DataTable dt, string name)
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(name);
            int i = 0;


            foreach (DataColumn col in dt.Columns)
            {
                i++;
                worksheet.InsertColumn(i, i);
                worksheet.Cells[1, i].Value = col.ColumnName; //col.ColumnName;
                //把ReadOnly的列设置为隐藏列
                if (col.ReadOnly)
                {
                    worksheet.Column(i).Hidden = true;
                }
                //设置宽度
                if (col.ExtendedProperties.ContainsKey("width"))
                {
                    int width = 20;
                    if (int.TryParse(col.ExtendedProperties["width"] + "", out width))
                    {
                        worksheet.Column(i).Width = width;
                    }
                }
            }
            // dt.Rows.Remove(dt.Rows[0]);
            int rowIndex = 1;
            foreach (DataRow row in dt.Rows)
            {
                rowIndex++;

                for (int i2 = 0; i2 < i; i2++)
                {
                    worksheet.Cells[rowIndex, i2 + 1].Value = row[i2].ToString();
                }

            }
        }


        public static void ImportDataTable(string fileName, DataTable dt)
        {
            int errorRowIndex = 0;
            int errorColIndex = 0;
            string errorSheetName = string.Empty;
            try
            {
                using (var excel = new ExcelPackage(new FileInfo(fileName)))
                {
                    var sheet = excel.Workbook.Worksheets[1];

                    if (sheet != null)
                    {
                        errorSheetName = sheet.Name;

                        var rows = sheet.Dimension.End.Row;
                        for (var i = 2; i <= rows; i++)
                        {
                            errorRowIndex = i;
                            DataRow dr = dt.NewRow();
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                errorColIndex = j + 1;
                                string value = "";

                                var cell = sheet.Cells[i, j + 1];
                                if (cell == null)
                                {
                                    continue;
                                }
                                value = cell.Value + "";

                                if (string.IsNullOrEmpty(value))
                                {
                                    continue;
                                }

                                switch (dt.Columns[j].DataType.Name.ToLower())
                                {
                                    case "":
                                        value = Convert.ToString(sheet.Cells[i, j + 1].GetValue<DateTime>()).Trim();
                                        break;

                                    case "string":
                                        value = Convert.ToString(sheet.Cells[i, j + 1].GetValue<String>()).Trim();
                                        break;
                                    case "int16":
                                        value = Convert.ToString(sheet.Cells[i, j + 1].GetValue<Int16>()).Trim();
                                        break;
                                    case "int32":
                                        value = Convert.ToString(sheet.Cells[i, j + 1].GetValue<Int32>()).Trim();
                                        break;
                                    case "int64":
                                        value = Convert.ToString(sheet.Cells[i, j + 1].GetValue<Int64>()).Trim();
                                        break;
                                    case "decimal":
                                        value = Convert.ToString(sheet.Cells[i, j + 1].GetValue<Decimal>()).Trim();
                                        break;
                                    case "double":
                                        value = Convert.ToString(sheet.Cells[i, j + 1].GetValue<Double>()).Trim();
                                        break;
                                    case "datetime":
                                        value = Convert.ToString(sheet.Cells[i, j + 1].GetValue<DateTime>()).Trim();
                                        break;
                                    case "boolean":
                                        value = Convert.ToString(sheet.Cells[i, j + 1].GetValue<Boolean>()).Trim();
                                        break;
                                    default:
                                        break;
                                }
                                if (!string.IsNullOrEmpty(value) && value.ToUpper() != "#N/A" && value.ToUpper() != "-")
                                {
                                    dr[j] = value;
                                }
                            }
                            if (!string.IsNullOrEmpty(dr[0].ToString())
                                || !string.IsNullOrEmpty(dr[1].ToString())
                                || !string.IsNullOrEmpty(dr[1].ToString()))
                            {
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (errorRowIndex > 0)
                {
                    throw new Exception("Excel错误行号:\t" + errorRowIndex + ",第" + errorColIndex + "列\n所在Sheet名称为:\t" + errorSheetName + "\n\n错误描述:\t" + ex.Message);
                }
                else
                {
                    throw ex;
                }
            }
        }

        public static void DataTableToSQLServer(DataTable dt, string connectionString)
        {
            using (SqlConnection destinationConnection = new SqlConnection(connectionString))
            {
                destinationConnection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                {
                    bulkCopy.BulkCopyTimeout = 600;
                    bulkCopy.BatchSize = 5000;
                    //bulkCopy.NotifyAfter = 1;
                    //bulkCopy.SqlRowsCopied += bulkCopy_SqlRowsCopied;
                    try
                    {
                        bulkCopy.DestinationTableName = dt.TableName;//要插入的表的表名
                        //映射字段名 DataTable列名 ,数据库 对应的列名
                        foreach (DataColumn col in dt.Columns)
                        {
                            //bulkCopy.ColumnMappings.Add(d.Key, d.Value);
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }

                        bulkCopy.WriteToServer(dt);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

        }
    }
}


