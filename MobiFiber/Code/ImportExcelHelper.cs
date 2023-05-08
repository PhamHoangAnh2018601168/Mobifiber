using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MobiFiber.Models;
using MobiFiber.PartialViewModel;

namespace MobiFiber.Code
{
    public class ImportExcelHelper
    {

        //public static void ReadExcelFileStream(Stream stream, List<dynamic> lstResult, List<dynamic> lstError, Dictionary<string, string> dic_mapping)
        //{
        //    if (stream == null)
        //    {
        //        return;
        //    }

        //    using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(stream, false))
        //    {
        //        WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
        //        WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
        //        SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
        //        SharedStringTable sharedStringTable = spreadsheetDocument.WorkbookPart.SharedStringTablePart.SharedStringTable;
        //        Dictionary<string, List<string>> _dic = new Dictionary<string, List<string>>();
        //        DeviceViewModel newItem = new DeviceViewModel();
        //        Dictionary<string, string> _dic_mapping_excel = new Dictionary<string, string>();
        //        int iRow = 0;
        //        int StartPoint = 3;
        //        foreach (Row r in sheetData.Elements<Row>())
        //        {
        //            newItem = new DeviceViewModel();
        //            iRow++;
        //            if (iRow < StartPoint)
        //            {
        //                continue;
        //            }
        //            // Mapping data
        //            newItem = MappingData(sharedStringTable, r, dic_mapping);

        //            // Validate item
        //            List<string> lstErrorMessage = new List<string>();
        //            bool obj_Invalid = ValidateData(newItem, iRow, lstErrorMessage);
        //            if (obj_Invalid)
        //            {
        //                newItem.Error = lstErrorMessage;
        //                lstError.Add(newItem);
        //            }
        //            else
        //            {
        //                lstResult.Add(newItem);
        //            }
        //        }
        //    }
        //}

        //private static DeviceViewModel MappingData(SharedStringTable sharedStringTable, Row r, Dictionary<string, string> dic_mapping)
        //{
        //    DeviceViewModel newItem = new DeviceViewModel();

        //    foreach (Cell c in r.Elements<Cell>())
        //    {
        //        string cellValue = sharedStringTable.ElementAt(Int32.Parse(c.InnerText)).InnerText;
        //        string col_name = GetColumnName(c.CellReference.ToString());
        //        string name = c.CellReference.ToString();
        //        string property_Name = dic_mapping.Keys.Contains(col_name) ? dic_mapping[col_name] : ""; // Get data column and property

        //        switch (property_Name)
        //        {
        //            case "DeviceName":
        //                newItem.DeviceName = cellValue;
        //                break;
        //            case "DeviceCode":
        //                newItem.DeviceCode = cellValue;
        //                break;
        //            case "Serial":
        //                newItem.Serial = cellValue;
        //                break;
        //            //case "DevicePrice":
        //            //    newItem.DevicePrice = cellValue;
        //            //    break;
        //            //case "AllocationTime":
        //            //    newItem.AllocationTime = cellValue;
        //            //    break;
        //            //case "DateReinputWarehouse":
        //            //    newItem.DateReinputWarehouse = cellValue;
        //            //    break;
        //        }
        //    }

        //    return newItem;
        //}
        //private static string GetColumnName(StringValue cellName)
        //{
        //    var regex = new Regex("[a-zA-Z]+");
        //    var match = regex.Match(cellName);
        //    return match.Value;
        //}
        //private static bool ValidateData(DeviceViewModel obj, int iRow, List<string> lstErrorMessage)
        //{

        //    bool Invalid = false;
        //    if (string.IsNullOrEmpty(obj.DeviceName))
        //    {
        //        lstErrorMessage.Add(buildMessage(iRow, "Tên thiết bị Trống"));
        //        Invalid = true;
        //        obj.ErrorDeviceName = true;
        //    }

        //    if (string.IsNullOrEmpty(obj.DeviceCode))
        //    {
        //        lstErrorMessage.Add(buildMessage(iRow, "Mã thiết bị trống"));
        //        Invalid = true;
        //        obj.ErrorDeviceCode = true;
        //    }

        //    if (string.IsNullOrEmpty(obj.Serial))
        //    {
        //        lstErrorMessage.Add(buildMessage(iRow, "Serial trống"));
        //        Invalid = true;
        //        obj.ErrorSerial = true;
        //    }

        //    if (obj.DevicePrice <= 0)
        //    {
        //        lstErrorMessage.Add(buildMessage(iRow, "Giá thiết bị không hợp lệ"));
        //        Invalid = true;
        //        obj.ErrorDevicePrice = true;
        //    }

        //    if (obj.AllocationTime <= 0)
        //    {
        //        lstErrorMessage.Add(buildMessage(iRow, "Thời gian phân bổ thiết bị không hợp lệ"));
        //        Invalid = true;
        //        obj.ErrorAllocationTime = true;
        //    }

        //    return Invalid; // Invalid
        //}
        //private static string buildMessage(int row, string message)
        //{
        //    string strError = "Row: {0}, {1}";
        //    return string.Format(strError, row, message);
        //}
    }
}
