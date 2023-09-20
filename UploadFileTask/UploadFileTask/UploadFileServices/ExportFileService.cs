using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Runtime.InteropServices;
using UploadFile.Interfaces;
using Microsoft.Extensions.Hosting;
using UploadFile.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace UploadFile.Services
{
    public class ExportFileService:IExportFileService
    {


        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public ExportFileService(IWebHostEnvironment hostingEnvironment, IConfiguration configuration , IHttpContextAccessor contextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }


        public string ExportData(ExportTemplateBase exportTemplateBase, DataTable data)
        {
            var localPath = GetLocalPath(exportTemplateBase.TemplateName, ".xlsx");
          Export(localPath, data, _hostingEnvironment, exportTemplateBase.SubstitutionDictionary());
            return GetDownloadUrl(Path.GetFileName(localPath));
        }

        private string GetLocalPath(string fileTitle, string extension)
        {
            string WEBurl = Path.Combine(_hostingEnvironment.WebRootPath, @"ExportFiles\", $"{fileTitle}_{DateTime.Now:yyyyMMddHHmmssfff}{extension}");

            return WEBurl;
        }

        public string GetDownloadUrl(string FileName)
        {
            string serverPath = _configuration["ExportFilesURL"];
            var request = _contextAccessor.HttpContext.Request;
            string URL = string.Format("{0}://{1}{2}/{3}", request.Scheme, request.Host, serverPath, FileName);
            return URL;
        }
        public void Export(string fullPath, DataTable data, IWebHostEnvironment env, Dictionary<string, string> substitutionValue = null)
        {
            //FIX template Path
            
            try
            {
                var temp = new FileInfo(Path.Combine(env.WebRootPath, @"Templates\", "Default.xlsx"));
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(new FileInfo(fullPath), temp))
                {
                    var sheet = package.Workbook.Worksheets["Sheet1"];
                    
                              sheet.Cells[4, 1].LoadFromDataTable(data, true);
                               SetTemplateValuesReportStyle(ref sheet, substitutionValue);
                               SetFrozenPane(ref sheet, substitutionValue);
                    package.Save();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                //throw;
            }
        }

        private void SetTemplateValuesReportStyle(ref ExcelWorksheet worksheet, Dictionary<string, string> substitutionValue)
        {
            //var CustomerName = worksheet.Cells[1, 3];
            //CustomerName.Value = substitutionValue["CustomerName"] ?? "Z2 Data";
            var TimeCell = worksheet.Cells[2, 10];
            TimeCell.Value = String.Format("{0:MMMM dd, yyyy}", DateTime.Now);
            var ByCell = worksheet.Cells[3, 10];
            ByCell.Value = (substitutionValue["UserName"] ?? "Administrator");
            //var ReportNameCell = worksheet.Cells["A4"];
            //ByCell.Value = (substitutionValue["ReportName"] ?? "Report");
            var IsValidSheetName = substitutionValue.TryGetValue("SheetName", out string sheetName);
            worksheet.Name = IsValidSheetName && !string.IsNullOrEmpty(sheetName) ? sheetName : worksheet.Name;
            var sheetLabel = worksheet.Cells[2, 1];
            sheetLabel.Value = worksheet.Name;
            var sheetDescription = worksheet.Cells[3, 1];
            sheetDescription.Value = "";// "A list of " + worksheet.Name;
        }

       
private void SetFrozenPane(ref ExcelWorksheet worksheet, Dictionary<string, string> substitutionValue)

        {

            if (substitutionValue.TryGetValue("FrozenRow", out string RowValue) && substitutionValue.TryGetValue("FrozenColumn", out string ColValue))

            {

                if (int.Parse(RowValue) != 0 && int.Parse(ColValue) != 0)

                {

                    worksheet.View.FreezePanes(int.Parse(RowValue) + 1, int.Parse(ColValue) + 1);

                }

            }

        }
    }
}


