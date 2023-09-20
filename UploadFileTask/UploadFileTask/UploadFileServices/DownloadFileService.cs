using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadFile.Entities.Models;
using UploadFile.Interfaces;

namespace UploadFile.Services
{
    public class DownloadFileService:IDownloadFileService
    {
        private readonly IExportFileService _exportFileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUploadCSVFileService _uploadCSVFileService;
        public DownloadFileService(IExportFileService exportFileService, IHttpContextAccessor contextAccessor, IUploadCSVFileService uploadCSVFileService)
        {
            _exportFileService = exportFileService;
            _httpContextAccessor = contextAccessor;
            _uploadCSVFileService = uploadCSVFileService;
        }

        public string GetExportFilePath(FileModel file, string UserName)
        {
            //var dt = UploadCSVFileService(JsonData.ToString());
           var jsonData = _uploadCSVFileService.GetDataTabletFromCSVFile(file.File);
            var dt = ConvertJsonToDataTable(jsonData);

            ExportTemplateBase exportTemplateBase = new ExportTemplateBase
            {
                Name = "ExcelSheet",
                TemplateName = "ExcelSheet",
                UserName = UserName
            };
            var filePath = _exportFileService.ExportData(exportTemplateBase, dt);
            return filePath;
        }

        public DataTable ConvertJsonToDataTable(string jsonData )

        {
            if (string.IsNullOrEmpty(jsonData))
            {
                return null;
            }
            var dataTable = JsonConvert.DeserializeObject<DataTable>(jsonData);

            return dataTable;

        }
    }
}
