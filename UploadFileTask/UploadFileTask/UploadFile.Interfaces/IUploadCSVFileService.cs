using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadFile.Entities.Models;

namespace UploadFile.Interfaces
{
    public interface IUploadCSVFileService
    {
        List<Receipt> UploadFile(IFormFile file);
        string GetDataTabletFromCSVFile(IFormFile file);

        //ExportFile(IFormFile file);
    }
}
