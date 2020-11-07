using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using YDG.Interfaces;
using YDG.Models;

namespace YDG.Infrastructure.FileServises
{
    internal class CsvFileService : IFileService
    {
        IEnumerable<YDPostModel> IFileService.Open(string filename)
        {
            throw new NotImplementedException();
        }

        void IFileService.Save<T>(string path, IEnumerable<T> records)
        {
            if (records == null || records.Count() < 1 ) { return; }

            //using (FileStream fs = new FileStream(path, FileMode.Create))
            //{
            //    jsonFormatter.WriteObject(fs, records);
            //}

            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                using (var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture))
                {
                    csvWriter.Configuration.Delimiter = ";";
                    csvWriter.Configuration.HasHeaderRecord = true;
                    csvWriter.WriteHeader(typeof(CSVModel));

                    csvWriter.WriteHeader<CSVModel>();
                    csvWriter.NextRecord();

                    csvWriter.WriteRecords(records as IEnumerable);

                    //foreach (var item in records)
                    //{
                    //    csvWriter.WriteField(item);
                    //    csvWriter.NextRecord();
                    //}
                    writer.Flush();
                }
            }
        }
    }
}
