using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using UploadFile.Entities.Models;
using UploadFile.Interfaces;

namespace UploadFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadCSVFileController : ControllerBase
    {
        private readonly IUploadCSVFileService _uploadCSVFileService;
        private readonly IDownloadFileService _downloadFileService;

        public UploadCSVFileController(IUploadCSVFileService uploadCSVFileService, IDownloadFileService downloadFileService)
        {
            _uploadCSVFileService = uploadCSVFileService;
            _downloadFileService = downloadFileService;
        }

        [HttpPost]
        [Route("[action]")]
        public string  UploadFile([FromForm] FileModel file)
        {
            return _uploadCSVFileService.GetDataTabletFromCSVFile(file.File);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult GetExportFilePath( string UserName,[FromForm] FileModel file)
        {
            
            var Url=_downloadFileService.GetExportFilePath(file, UserName);
          
             return Ok(new { Url = Url });
        }
        
        
        [HttpGet]
        [Route("[action]")]

        public string GetString()
        {
            return "Hello";
        }
    }
}
