using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFile.Entities.Models
{
    public class FileModel
    {
        public IFormFile File { get; set; }
    }
}
