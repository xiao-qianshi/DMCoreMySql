using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Dmt.DM.Code.Excel
{
    public partial class NPOIExcel
    {
        private string _title;
        private string _sheetName;
        private string _filePath;

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool ToExcel(DataTable table)
        {
            FileStream fs = new FileStream(this._filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            IWorkbook workBook = new HSSFWorkbook();
            this._sheetName = this._sheetName.IsEmpty() ? "sheet1" : this._sheetName;
            ISheet sheet = workBook.CreateSheet(this._sheetName);

            //处理表格标题
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue(this._title);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, table.Columns.Count - 1));
            row.Height = 500;

            ICellStyle cellStyle = workBook.CreateCellStyle();
            IFont font = workBook.CreateFont();
            font.FontName = "微软雅黑";
            font.FontHeightInPoints = 17;
            cellStyle.SetFont(font);
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.Alignment = HorizontalAlignment.Center;
            row.Cells[0].CellStyle = cellStyle;

            //处理表格列头
            row = sheet.CreateRow(1);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                row.Height = 350;
                sheet.AutoSizeColumn(i);
            }

            //处理数据内容
            for (int i = 0; i < table.Rows.Count; i++)
            {
                row = sheet.CreateRow(2 + i);
                row.Height = 250;
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(table.Rows[i][j].ToString());
                    sheet.SetColumnWidth(j, 256 * 15);
                }
            }

            //写入数据流
            workBook.Write(fs);
            fs.Flush();
            fs.Close();

            return true;
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table"></param>
        /// <param name="title"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public bool ToExcel(DataTable table, string title, string sheetName, string filePath)
        {
            this._title = title;
            this._sheetName = sheetName;
            this._filePath = filePath;
            return ToExcel(table);
        }
        /// <summary>
        /// 导出排班表
        /// </summary>
        /// <param name="table">数据明细</param>
        /// <param name="title">标题</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="dateList">日期列表</param>
        /// <param name="summery">汇总信息</param>
        /// <param name="sheetName">子表名称</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public bool ToExcelForSchedule(DataTable table, string title, string subTitle, List<string> dateList, string summery, string sheetName, string filePath)
        {
            this._title = title;
            this._sheetName = sheetName;
            this._filePath = filePath;
            return ToExcelForSchedule(table, subTitle, dateList, summery);
        }
        /// <summary>
        /// 导出排班表具体实现
        /// </summary>
        /// <param name="table"></param>
        /// <param name="subTitle"></param>
        /// <param name="dateList"></param>
        /// <param name="summery"></param>
        /// <returns></returns>
        private bool ToExcelForSchedule(DataTable table, string subTitle, List<string> dateList, string summery)
        {
            FileStream fs = new FileStream(this._filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            IWorkbook workBook = new HSSFWorkbook();
            this._sheetName = this._sheetName.IsEmpty() ? "sheet1" : this._sheetName;
            ISheet sheet = workBook.CreateSheet(this._sheetName);

            //处理表格标题
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue(this._title);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, table.Columns.Count));
            row.Height = 600;
            ICellStyle cellStyle = workBook.CreateCellStyle();
            IFont font = workBook.CreateFont();
            font.FontName = "黑体";
            font.FontHeightInPoints = 18;
            cellStyle.SetFont(font);
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.Alignment = HorizontalAlignment.Center;
            row.Cells[0].CellStyle = cellStyle;

            //处理副标题
            row = sheet.CreateRow(1);
            row.CreateCell(0).SetCellValue(subTitle);
            sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, table.Columns.Count));
            row.Height = 400;
            cellStyle = workBook.CreateCellStyle();
            font = workBook.CreateFont();
            font.FontName = "楷体";
            font.FontHeightInPoints = 12;
            cellStyle.SetFont(font);
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.Alignment = HorizontalAlignment.Center;
            row.Cells[0].CellStyle = cellStyle;

            //处理表头（复合表头）
            //一级表头
            row = sheet.CreateRow(2);
            row.Height = 300;
            cellStyle = workBook.CreateCellStyle();
            font = workBook.CreateFont();
            font.FontName = "楷体";
            font.FontHeightInPoints = 11;
            cellStyle.SetFont(font);
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.Alignment = HorizontalAlignment.Center;
            var cell = row.CreateCell(0);//.SetCellValue("床位");
            cell.CellStyle = cellStyle;
            cell.SetCellValue("床位");
            sheet.AddMergedRegion(new CellRangeAddress(2, 2, 0, 1));

            for (int i = 0; i < dateList.Count; i++)
            {
                cell = row.CreateCell(i * 3 + 2);//.SetCellValue(dateList[i]);
                cell.SetCellValue(dateList[i]);
                cell.CellStyle = cellStyle;
                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 2 + i * 3, 1 + (i + 1) * 3));
            }
            //二级表头
            var cols = new List<string>
            {
                "分组",
                "床号",
                "早班",
                "中班",
                "晚班",
                "早班",
                "中班",
                "晚班",
                "早班",
                "中班",
                "晚班",
                "早班",
                "中班",
                "晚班",
                "早班",
                "中班",
                "晚班",
                "早班",
                "中班",
                "晚班",
                "早班",
                "中班",
                "晚班"
            };
            row = sheet.CreateRow(3);
            row.Height = 300;
            for (int i = 0; i < cols.Count; i++)
            {
                cell = row.CreateCell(i);
                cell.SetCellValue(cols[i]);
                cell.CellStyle = cellStyle;
            }


            ////处理表格列头
            //row = sheet.CreateRow(1);
            //for (int i = 0; i < table.Columns.Count; i++)
            //{
            //    row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
            //    row.Height = 350;
            //    sheet.AutoSizeColumn(i);
            //}

            //处理数据内容
            for (int i = 0; i < table.Rows.Count; i++)
            {
                row = sheet.CreateRow(4 + i);
                row.Height = 250;
                for (int j = 0; j < table.Columns.Count - 1; j++)
                {
                    cell = row.CreateCell(j);//.SetCellValue(table.Rows[i][j].ToString());
                    cell.SetCellValue(table.Rows[i][j].ToString());
                    cell.CellStyle = cellStyle;
                    sheet.SetColumnWidth(j, 256 * 15);
                }
            }

            //写入数据流
            workBook.Write(fs);
            fs.Flush();
            fs.Close();

            return true;
        }
    }
}
