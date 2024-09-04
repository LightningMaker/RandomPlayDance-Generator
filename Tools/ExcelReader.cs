using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPlayDance_Generator_3.Tools
{
    internal class ExcelReader
    {
        public static DataTable ReadExcel()
        {
            string path = Form1.ExcelPath;
            if(path == null)
            {
                path = "歌单.xlsx";
            }

            DataTable dtTable = new DataTable();
            ISheet sheet;
            string defaultValue = "<未知歌曲>";

            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    stream.Position = 0;
                    XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                    sheet = xssWorkbook.GetSheetAt(0);
                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;
                    for (int j = 0; j < cellCount; j++)
                    {
                        ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        {
                            dtTable.Columns.Add(cell.ToString());
                        }
                    }
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                        DataRow dataRow = dtTable.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            ICell cell = row.GetCell(j);
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                dataRow[j] = cell.ToString();
                            }
                        }

                        // 检查第一项是否为空
                        if (string.IsNullOrWhiteSpace(dataRow[0].ToString()))
                        {
                            dataRow[0] = defaultValue;
                        }

                        dtTable.Rows.Add(dataRow);
                    }
                }

                int totalCount = dtTable.Rows.Count;
                Form1.Instance.UpdateLog($"歌单读取完成，共计{totalCount}曲", Form1.LogLevel.Message);
            }
            catch (Exception ex)
            {
                Form1.Instance.UpdateLog($"歌单读取失败：{ex.Message}", Form1.LogLevel.Error);
                Form1.Instance.UpdateLog(ex.StackTrace, Form1.LogLevel.Error);
            }
            return dtTable;
        }
    }
}
