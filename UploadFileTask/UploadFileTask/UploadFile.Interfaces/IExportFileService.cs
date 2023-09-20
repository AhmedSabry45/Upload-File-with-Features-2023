using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.InteropServices;
using UploadFile.Entities.Models;

namespace UploadFile.Interfaces
{
    public interface IExportFileService
    {
         string ExportData(ExportTemplateBase exportTemplateBase, DataTable data);

    }
}
