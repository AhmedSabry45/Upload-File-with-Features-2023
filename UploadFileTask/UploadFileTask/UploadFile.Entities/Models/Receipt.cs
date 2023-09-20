using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFile.Entities.Models
{
    public class Receipt
    {
        public string BusinessUnit { get; set; }
        public string ReceiptMethodID { get; set; }
        public string RemittanceBank { get; set; }
        public string RemittanceBankAccount { get; set; }
        public string ReceiptNumber { get; set; }
        public string ReceiptAmount { get; set; }
        public string ReceiptDate { get; set; }
        public string AccountingDate { get; set; }
        public string ConversionDate { get; set; }
        public string Currency { get; set; }
    }
}
