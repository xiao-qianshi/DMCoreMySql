namespace Dmt.DM.Code.Excel
{
    public class ExcelHelper
    {
        private readonly string _title = string.Empty;
        private readonly string _sheetName = string.Empty;
        private readonly string _filePath = string.Empty;


        public ExcelHelper(string tilte, string filePath, string sheetName = "sheet1")
        {
            _title = tilte;
            _sheetName = sheetName;
            _filePath = filePath;
        }
    }
}
