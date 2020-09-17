using Dmt.DM.Code.Excel.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;

namespace Dmt.DM.Code.Excel
{
    public partial class NPOIExcel
    {
        int _startRow = 0;
        int _startColumn = 0;
        public bool ToExcel<T>(List<T> list, string sheetName, string filePath, int startRow = 3, int startColumn = 1) where T : class
        {
            this._sheetName = sheetName;
            this._filePath = filePath;
            this._startRow = startRow;
            this._startColumn = startColumn;
            IWorkbook workBook = null;
            using (FileStream fs = new FileStream(this._filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workBook = new HSSFWorkbook(fs);
                fs.Close();
            }

            ISheet sheet = workBook.GetSheet(this._sheetName);
            //sheet = sheet ?? workBook.CreateSheet(this._sheetName);
            var type = typeof(T);
            PropertyInfo[] infos = type.GetProperties();
            foreach (var item in list)
            {
                IRow row = sheet.GetRow(this._startRow);
                row = row ?? sheet.CreateRow(this._startRow);
                foreach (var child in infos)
                {
                    ICell cell = row.GetCell(this._startColumn);
                    cell = cell ?? row.CreateCell(this._startColumn);
                    cell.SetCellValue(item.GetType().GetProperty(child.Name).GetValue(item, null) + "");
                    this._startColumn += 1;
                }
                this._startRow += 1;
                this._startColumn = startColumn;
            }

            //写入数据流
            //workBook.Write(fs);
            //fs.Flush();
            //fs.Close();
            using (FileStream fs = new FileStream(this._filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workBook.Write(fs);
                fs.Close();
            }

            return true;
        }

        public bool ToExcel(DataTable table, string sheetName, string filePath, int startRow = 3, int startColumn = 1)
        {
            this._sheetName = sheetName;
            this._filePath = filePath;
            this._startRow = startRow;
            this._startColumn = startColumn;
            IWorkbook workBook = null;
            using (FileStream fs = new FileStream(this._filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workBook = new HSSFWorkbook(fs);
                fs.Close();
            }
            ISheet sheet = workBook.GetSheet(this._sheetName);
            foreach (DataRow item in table.Rows)
            {
                IRow row = sheet.GetRow(this._startRow);
                row = row ?? sheet.CreateRow(this._startRow);
                foreach (var child in item.ItemArray)
                {
                    if (child != null)
                    {
                        ICell cell = row.GetCell(this._startColumn);
                        cell = cell ?? row.CreateCell(this._startColumn);
                        cell.SetCellValue(child + "");
                    }
                    this._startColumn += 1;
                }
                this._startRow += 1;
                this._startColumn = startColumn;
            }

            //写入数据流
            //workBook.Write(fs);
            //fs.Flush();
            //fs.Close();
            using (FileStream fs = new FileStream(this._filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workBook.Write(fs);
                fs.Close();
            }

            return true;
        }

        public List<ImportScheduleModel> ToListForSchedule(string sheetName, string filePath, List<string> dialysisTypes, int startRow = 4, int startColumn = 0)
        {
            List<ImportScheduleModel> list = new List<ImportScheduleModel>();
            this._sheetName = sheetName;
            this._filePath = filePath;
            this._startRow = startRow;
            this._startColumn = startColumn;
            IWorkbook workBook = null;
            using (FileStream fs = new FileStream(this._filePath, FileMode.Open, FileAccess.Read))
            {
                workBook = new HSSFWorkbook(fs);

                ISheet sheet = workBook.GetSheet(this._sheetName);
                sheet = workBook.GetSheetAt(0);
                var rows = sheet.GetRowEnumerator();

                while (rows.MoveNext())
                {
                    var row = rows.Current as HSSFRow;
                    if (row.RowNum < startRow) continue; //从startRow开始读取数据

                    ICell cell = row.GetCell(0);
                    if (cell == null || cell.CellType.Equals(CellType.Blank)) continue;
                    var groupName = cell.CellType == CellType.Numeric ? cell.NumericCellValue.ToInt().ToString() : cell.StringCellValue;
                    if (string.IsNullOrEmpty(groupName)) continue;
                    cell = row.GetCell(1);
                    if (cell == null || cell.CellType.Equals(CellType.Blank)) continue;
                    var bedNo = cell.CellType == CellType.Numeric ? cell.NumericCellValue.ToInt().ToString() : cell.StringCellValue;
                    if (string.IsNullOrEmpty(bedNo)) continue;

                    for (var i = 2; i < row.LastCellNum; i++)
                    {
                        cell = row.GetCell(i);
                        if (cell == null)
                        {
                            continue;
                        }
                        else
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Blank:
                                    break;
                                case CellType.Boolean:
                                    break;
                                case CellType.Numeric:
                                    break;
                                case CellType.String:
                                    var value = cell.StringCellValue;
                                    if (string.IsNullOrEmpty(value)) continue;
                                    value = value.ToUpper();
                                    var item = new ImportScheduleModel
                                    {
                                        F_GroupName = groupName,
                                        F_DialysisBedNo = bedNo,
                                        DayOfWeek = (i - 2) / 3 + 1,
                                        F_VisitNo = (i - 2) % 3 + 1
                                    };
                                    var find = dialysisTypes.Find(t => value.Contains(t));
                                    if (find == null) //未填写透析模式，使用默认值
                                    {
                                        item.F_Name = value.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("（", "").Replace("）", "").Replace("-", "");
                                    }
                                    else
                                    {
                                        item.F_DialysisType = find;
                                        item.F_Name = value.Replace(find, "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace("（", "").Replace("）", "").Replace("-", "");
                                    }
                                    list.Add(item);
                                    break;
                                case CellType.Error:
                                    break;
                                case CellType.Formula:
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                fs.Close();
            }
            return list;
        }
    }
}
