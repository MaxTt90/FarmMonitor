using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace FarmMonitor.Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// 下载excel
        /// </summary>
        /// <param name="dt">datatable数据源</param>
        /// <param name="title">excel名称</param>
        /// <param name="context">操作上下文</param>
        public static void ExportExcel(DataTable dt, string title, HttpContext context)
        {
            HSSFWorkbook book = new HSSFWorkbook();

            ISheet sheet = book.CreateSheet("Sheet1");

            IRow headerrow = sheet.CreateRow(0);
            ICellStyle style = book.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            style.VerticalAlignment = VerticalAlignment.CENTER;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = headerrow.CreateCell(i);
                cell.CellStyle = style;
                cell.SetCellValue(dt.Columns[i].ColumnName);

            }

            int rowIndex = 1;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                var row = sheet.CreateRow(rowIndex + j);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var cell = row.CreateCell(i);
                    cell.SetCellValue(dt.Rows[j][i].ToString());
                }
            }

            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", HttpUtility.UrlEncode(title + "_" + DateTime.Now.ToString("yyyy-MM-dd"), System.Text.Encoding.UTF8)));
            context.Response.BinaryWrite(ms.ToArray());
            context.Response.End();

            ms.Close();
            ms.Dispose();


        }


        /// <summary>
        /// 下载excel(多个datatable存放在多个工作表)
        /// </summary>
        /// <param name="dtList">DataTable集合</param>
        /// <param name="title">excel名称</param>
        /// <param name="context">操作上下文</param>
        /// hyp2015-12-1
        public static void ExportManyExcel(List<DataTable> dtList, string title,List<string> name, HttpContext context)
        {
            HSSFWorkbook book = new HSSFWorkbook();
            if (name.Count != dtList.Count)
            {
                name = new List<string>();
                for (int i = 0; i < dtList.Count(); i++)
                {
                    var name1 = "Sheet" + (i + 1);
                    name.Add(name1);
                }
            }
            for (int i = 0; i < dtList.Count(); i++)
            {
                ISheet sheet = book.CreateSheet(name[i]);
                IRow headerrow = sheet.CreateRow(0);
                ICellStyle style = book.CreateCellStyle();
                style.Alignment = HorizontalAlignment.CENTER;
                style.VerticalAlignment = VerticalAlignment.CENTER;

                for (int j = 0; j < dtList[i].Columns.Count; j++)
                {
                    ICell cell = headerrow.CreateCell(j);
                    cell.CellStyle = style;
                    cell.SetCellValue(dtList[i].Columns[j].ColumnName);

                }

                int rowIndex = 1;
                for (int k = 0; k < dtList[i].Rows.Count; k++)
                {
                    var row = sheet.CreateRow(rowIndex + k);
                    for (int z = 0; z < dtList[i].Columns.Count; z++)
                    {
                        var cell = row.CreateCell(z);
                        cell.SetCellValue(dtList[i].Rows[k][z].ToString());
                    }
                }



            }




            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", HttpUtility.UrlEncode(title + "_" + DateTime.Now.ToString("yyyy-MM-dd"), System.Text.Encoding.UTF8)));
            context.Response.BinaryWrite(ms.ToArray());
            context.Response.End();

            ms.Close();
            ms.Dispose();


        }







        #region NOPI 操作
        /// <summary>
        /// 使用NPOI方法获得EXCEL文件的第一个sheet的数据
        /// </summary>
        /// <param name="filePath">EXCEL的物理文件路径</param>
        /// <returns>DataTable</returns>
        /// Author FredJiang
        /// Create Date 2014年5月29日
        public DataTable GetExcelFirstTableByNOPI(string filePath)
        {

            IWorkbook wb = CreateIWorkbook(filePath);//获得工作薄
            ISheet ws = wb.GetSheetAt(0);//获得第一个sheet对象
            return SheetToDataTable(ws);
        }

        /// <summary>
        /// 使用NPOI方法获得EXCEL文件的第一个sheet的数据
        /// </summary>
        /// <param name="filePath">EXCEL的物理文件路径</param>
        /// <returns>DataTable</returns>
        /// Author FredJiang
        /// Create Date 2014年5月29日
        public DataTable GetExcelFirstTableByNOPIS(MemoryStream ms)
        {
            DataTable dt = new DataTable();
            IWorkbook wb = CreateIWorkbookS(ms);//获得工作薄
            ISheet ws = wb.GetSheetAt(0);//获得第一个sheet对象
            return SheetToDataTable(ws);
        }

        /// <summary>
        /// 根据文件路径和工作表名获取sheet中的数据
        /// </summary>
        /// <param name="filePath">EXCEL的物理文件路径</param>
        /// <param name="sheetName">EXCEL的sheet名称</param>
        /// <returns>DataTable</returns>
        /// Author zhongliang
        /// Create Date 2014年8月8日
        public DataTable GetExcelTableBySheetName(string filePath, string sheetName)
        {
            DataTable dt = new DataTable();
            IWorkbook wb = CreateIWorkbook(filePath);//获得工作薄
            ISheet ws = wb.GetSheet(sheetName);//获得指定sheet对象
            return SheetToDataTable(ws);
        }

        /// <summary>
        /// 使用NPOI方法获得EXCEL文件的第一个sheet的数据——赠品物流信息专用
        /// </summary>
        /// <param name="filePath">EXCEL的物理文件路径</param>
        /// <returns>DataTable</returns>
        /// Author zhongliang
        /// Create Date 2014年7月9日
        public DataTable GetExcelFirstTableByNOPISForGiftLogics(MemoryStream ms)
        {
            DataTable dt = new DataTable();
            IWorkbook wb = CreateIWorkbookS(ms);//获得工作薄
            ISheet ws = wb.GetSheetAt(0);//获得第一个sheet对象
            return SheetToDataTableForGiftLogics(ws);
        }


        public string GetDataTableToExcelByNOPI(DataTable dt, string filePath = "")
        {
            IWorkbook wb = new XSSFWorkbook();
            ISheet ws = wb.CreateSheet(dt.TableName == "" ? "Sheet1" : dt.TableName);
            IRow row = null;
            ICell cell = null;
            int rowIndex = 0;

            #region 根据DataTable的列生成第一行数据
            row = ws.CreateRow(rowIndex);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }
            #endregion

            #region 生成数据行信息
            rowIndex++;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                row = ws.CreateRow(rowIndex + j);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    cell = row.CreateCell(i);
                    cell.SetCellValue(dt.Rows[j][i].ToString());
                }
            }
            #endregion

            if (filePath == "")
            {
                filePath = "~/Files/DownloadTemp/" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            }

            FileStream fs = File.OpenWrite(HttpContext.Current.Server.MapPath(filePath));
            wb.Write(fs);
            return filePath;

        }

        /// <summary>
        /// 根据路径将DataTable转换成Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileName">指定要保持的文件路径</param>
        /// <returns>true或false</returns>
        public bool GetDataTableToExcelByNOPIS(DataTable dt, string filePath)
        {
            bool IsSaved = false;  //保存成功
            try
            {
                IWorkbook wb = new XSSFWorkbook();
                ISheet ws = wb.CreateSheet(dt.TableName == "" ? "Sheet1" : dt.TableName);
                IRow row = null;
                ICell cell = null;
                int rowIndex = 0;

                #region 根据DataTable的列生成第一行数据
                row = ws.CreateRow(rowIndex);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    cell = row.CreateCell(i);
                    cell.SetCellValue(dt.Columns[i].ColumnName);
                }
                #endregion

                #region 生成数据行信息
                rowIndex++;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    row = ws.CreateRow(rowIndex + j);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        cell = row.CreateCell(i);
                        cell.SetCellValue(dt.Rows[j][i].ToString());
                    }
                }
                #endregion

                FileStream fs = File.OpenWrite(AppDomain.CurrentDomain.BaseDirectory + VirtualPathUtility.ToAbsolute(filePath));
                wb.Write(fs);
                IsSaved = true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                IsSaved = false;
            }
            return IsSaved;
        }

        #region basic function
        /// <summary>
        /// 根据文件物理路径获得workbook对象
        /// </summary>
        /// <param name="filePath">excel文件物理路径</param>
        /// <returns>IWorkbook对象</returns>
        /// Author FredJiang
        /// Create Date 2014年5月29日
        private IWorkbook CreateIWorkbook(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                //return new HSSFWorkbook(fs);
                return WorkbookFactory.Create(fs);
            }
        }

        /// <summary>
        /// 根据内存流获得workbook对象
        /// </summary>
        /// <param name="ms">Excel文件的内存流</param>
        /// <returns>IWorkbook</returns>
        /// Author FredJiang
        /// Create Date 2014年5月29日
        public IWorkbook CreateIWorkbookS(MemoryStream ms)
        {
            //IWorkbook wb = new HSSFWorkbook(ms);
            IWorkbook wb = WorkbookFactory.Create(ms);
            return wb;
        }

        /// <summary>
        /// Sheet转DataTable
        /// </summary>
        /// <param name="ws">ISheet</param>
        /// <returns>DataTable</returns>
        /// Author FredJiang
        /// Create Date 2014年5月29日
        private DataTable SheetToDataTable(ISheet ws)
        {
            DataTable dt = new DataTable();
            IRow row = null;             //公用行对象
            ICell cell = null;           //公用单元格对象
            int maxRow = 0, maxCell = 0; //最大行数,最大列数以第行为标准
            //如果sheet行数小于1,则不进行数据处理
            if (ws.LastRowNum > 0)
            {
                maxRow = ws.LastRowNum;
                row = ws.GetRow(0);
                maxCell = row.LastCellNum;  //第一行的最大列数量
                //生成datatable的列
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    dt.Columns.Add(new DataColumn(row.GetCell(i).StringCellValue, typeof(string)));
                }
                //添加数据到datatable
                for (int i = 1; i <= ws.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    //循环列添加行的数据
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row = ws.GetRow(i);
                        if (row != null)
                        {
                            cell = ws.GetRow(i).GetCell(j);
                            if (cell != null)
                            {
                                //#region 获取Excel表中列内容格式，若是日期类型则需要进行转换（edited by zhongliang）
                                //short format = cell.CellStyle.DataFormat;
                                //if (format == 14 || format == 22 || format == 31 || format == 57 || format == 58)//(yyyy-MM-dd HH:mm:ss)-22，(yyyy-MM)-17,(yyyy-MM-dd)-14，yyyy-MM月-57，月日(MM-dd) - 58
                                //{
                                //    DateTime date = cell.DateCellValue;
                                //    dr[j] = date.ToString("yyyy-MM-dd HH:mm:ss");
                                //}
                                //#endregion
                                //else
                                //{
                                    dr[j] = cell.ToString();
                                //}
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    dt.Rows.Add(dr);
                }
            }

            return dt;

        }


        /// <summary>
        /// Sheet转DataTable——赠品物流信息
        /// </summary>
        /// <param name="ws">ISheet</param>
        /// <returns>DataTable</returns>
        /// Author zhongliang
        /// Create Date 2014年7月9日
        private DataTable SheetToDataTableForGiftLogics(ISheet ws)
        {
            DataTable dt = new DataTable();
            IRow row = null;             //公用行对象
            ICell cell = null;           //公用单元格对象
            int maxRow = 0, maxCell = 0; //最大行数,最大列数以第行为标准
            //如果sheet行数小于1,则不进行数据处理
            if (ws.LastRowNum > 3)
            {
                maxRow = ws.LastRowNum;
                row = ws.GetRow(2);
                maxCell = row.LastCellNum;  //第一行的最大列数量
                //生成datatable的列
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    dt.Columns.Add(new DataColumn(row.GetCell(i).StringCellValue, typeof(string)));
                }
                //添加数据到datatable
                int tempInt = 0;
                for (int i = 3; i <= ws.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    //循环列添加行的数据
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row = ws.GetRow(i);
                        if (row != null)
                        {
                            cell = ws.GetRow(i).GetCell(j);
                            if (cell != null)
                            {
                                #region 获取Excel表中列内容格式，若是日期类型则需要进行转换（edited by zhongliang）
                                short format = cell.CellStyle.DataFormat;
                                if (format == 14 || format == 22 || format == 31 || format == 57 || format == 58)//(yyyy-MM-dd HH:mm:ss)-22，(yyyy-MM)-17,(yyyy-MM-dd)-14，yyyy-MM月-57，月日(MM-dd) - 58
                                {
                                    DateTime date = cell.DateCellValue;
                                    dr[j] = date.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                #endregion
                                else
                                {
                                    dr[j] = cell.ToString();
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(dr["备注"].ToString()) && int.TryParse(dr["备注"].ToString(), out tempInt))
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;

        }

        #endregion
        #endregion


    }
}
