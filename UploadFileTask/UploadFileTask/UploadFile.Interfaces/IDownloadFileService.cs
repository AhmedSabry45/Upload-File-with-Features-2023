using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadFile.Entities.Models;

namespace UploadFile.Interfaces
{
    public interface IDownloadFileService
    {
        string GetExportFilePath(FileModel file, string UserName);
    }
}
