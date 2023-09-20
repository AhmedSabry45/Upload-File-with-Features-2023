using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFile.Entities.Models
{
    public class ExportTemplateBase
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public string TemplateName { get; set; }
        public string ReportName { get; set; }
        public string SheetName { get; set; }
        public int FrozenRow { get; set; }
        public int FrozenColumn { get; set; }
        public string CustomerTemplateName { get; set; }
        
        public Dictionary<string, string> SubstitutionDictionary()
        {
            var parameter = new Dictionary<string, string>
            {
                {"TemplateName",TemplateName },
                {"ReportName",ReportName },
                {"UserName",UserName },
                {"CustomerName",CustomerName },
                {"Name",Name },
                {"FrozenRow", FrozenRow.ToString()},
                {"FrozenColumn", FrozenColumn.ToString()},
                {"SheetName",SheetName },
                {"CustomerTemplateName",CustomerTemplateName },
              
            };
            return parameter;
        }
    }
}
