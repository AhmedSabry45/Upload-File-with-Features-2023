using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UploadFile.Entities.Models;
using UploadFile.Interfaces;

namespace UploadFile.Services
{
    public class UploadCSVFileService : IUploadCSVFileService
    {
        public List<Receipt> UploadFile(IFormFile file)
        {



            if (file == null || file.Length == 0)
                return new List<Receipt>();



            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;



                var receipts = new List<Receipt>();



                using (var parser = new TextFieldParser(memoryStream, Encoding.UTF8))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        var fields = parser.ReadFields();



                        if (parser.LineNumber != 2)
                        {
                            // Assuming CSV columns: Date, Amount, ...
                            var receipt = new Receipt
                            {
                                BusinessUnit = fields[0],
                                ReceiptMethodID = fields[1],
                                RemittanceBank = fields[2],
                                RemittanceBankAccount = fields[3],
                                ReceiptNumber = fields[4],
                                ReceiptAmount = fields[5],
                                ReceiptDate = fields[6],
                                AccountingDate = fields[7],
                                ConversionDate = fields[8],
                                Currency = fields[9],
                            };
                            receipts.Add(receipt);
                        }
                    }
                }
                return receipts;
            }



        }
        public string GetDataTabletFromCSVFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;

                DataTable csvData = new DataTable();

                try

                {

                    using (TextFieldParser csvReader = new TextFieldParser(memoryStream))

                    {

                        csvReader.SetDelimiters(new string[] { "," });
                        csvReader.HasFieldsEnclosedInQuotes = true;
                        string[] colFields = csvReader.ReadFields();

                        foreach (string column in colFields)
                        {
                            DataColumn datecolumn = new DataColumn(column);
                            datecolumn.AllowDBNull = true;
                            csvData.Columns.Add(datecolumn);
                        }

                        while (!csvReader.EndOfData)

                        {
                            string[] fieldData = csvReader.ReadFields();
                            for (int i = 0; i < fieldData.Length; i++)

                            {

                                if (fieldData[i] == "")

                                {

                                    fieldData[i] = null;

                                }

                            }

                            csvData.Rows.Add(fieldData);

                        }

                    }

                }

                catch (Exception ex)

                {

                    throw;

                }
                var data = JsonConvert.SerializeObject(csvData);
                return data;

            }
        }
        
    }
}
