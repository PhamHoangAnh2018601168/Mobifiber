using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static MobiFiber.Code.ExcelTemplateHelper;

namespace MobiFiber.Code
{
    public static class Utility
    {
        public static Sheet FindSheetByName(this SpreadsheetDocument document, string sheetName)
        {
            Sheet sheet =
                (from item in document.WorkbookPart.Workbook.Sheets.Elements<Sheet>()
                 where item.Name == sheetName
                 select item).Single();
            return sheet;
        }

        public static string Int2Column(this int value)
        {
            List<char> result = new List<char>();

            if (value > 0)
            {
                value--; //1 based

                if (value < 26)
                {
                    result.Add((char)('A' + value));
                }
                else
                {
                    value -= 26;
                    do
                    {
                        result.Add((char)('A' + value % 26));
                        value = value / 26;
                    } while (value > 0);

                    if (result.Count == 1)
                    {
                        result.Add('A');
                    }
                }
            }

            return new string(result.Reverse<char>().ToArray());
        }
        public static int Column2Int(this string value)
        {
            int result = 0;

            if (value != null && value.Length > 0)
            {
                foreach (char item in value)
                {
                    result = 26 * result + (item - 'A');
                }

                result++; //1 based

                if (value.Length > 1)
                {
                    result += 26;
                }
            }

            return result;
        }

        public static IEnumerable<IGrouping<Row, Cell>> FindCellsByRange(this SpreadsheetDocument document, CellRangePosition range)
        {
            Sheet sheet = document.FindSheetByName(range.SheetName);

            WorksheetPart workSheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id);

            SheetData sheetData = workSheetPart.Worksheet.GetFirstChild<SheetData>();

            var result = from row in sheetData.Elements<Row>()
                         where range.Start.Row <= row.RowIndex && row.RowIndex <= range.End.Row
                         let cells = row.Elements<Cell>()
                         from cell in cells
                         where range.Contains(new CellPosition(cell.CellReference))
                         group cell by row into cellsInRange
                         select cellsInRange;

            return result;
        }

        public static object GetDataFromObj(this Object obj, string variable)
        {
            var properties = obj.GetType().GetProperties().ToList();
            var find = properties.Find(e => e.Name == variable);
            if (find != null)
            {
                return find.GetValue(obj);
            }
            return null;
        }
        public static void SetCellAutoValue(this Cell cell, object value)
        {
            if (value is Decimal)
            {
                cell.DataType.Value = CellValues.Number;
                cell.CellValue.Text = ((Decimal)value).ToString(CultureInfo.InvariantCulture);
            }
            else if (value is Double)
            {
                cell.DataType.Value = CellValues.Number;
                cell.CellValue.Text = ((Double)value).ToString(CultureInfo.InvariantCulture);
            }
            else if (value is float)
            {
                cell.DataType.Value = CellValues.Number;
                cell.CellValue.Text = ((float)value).ToString(CultureInfo.InvariantCulture);
            }
            else if (value is Int16)
            {
                cell.DataType.Value = CellValues.Number;
                cell.CellValue.Text = ((Int16)value).ToString(CultureInfo.InvariantCulture);
            }
            else if (value is Int32)
            {
                cell.DataType.Value = CellValues.Number;
                cell.CellValue.Text = ((Int32)value).ToString(CultureInfo.InvariantCulture);
            }
            else if (value is Int64)
            {
                cell.DataType.Value = CellValues.Number;
                cell.CellValue.Text = ((Int64)value).ToString(CultureInfo.InvariantCulture);
            }
            else if (value is DateTime)
            {
                cell.DataType.Value = CellValues.Number;
                cell.CellValue.Text = ((DateTime)value).ToOADate().ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                cell.DataType.Value = CellValues.String;
                cell.CellValue.Text = value.ToString();
            }
        }


        public static Worksheet FindWorksheetBySheet(this SpreadsheetDocument document, Sheet sheet)
        {
            WorksheetPart workSheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id);

            Worksheet worksheet = workSheetPart.Worksheet;
            return worksheet;
        }
    }
}
