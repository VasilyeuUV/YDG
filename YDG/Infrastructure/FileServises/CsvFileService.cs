using System;
using System.Collections.Generic;
using System.IO;
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
            using var writer = new StreamWriter(path, false, EncodesResolver.Resolve<T>());
            using var csvWriter = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(records);
        }
    }
}
