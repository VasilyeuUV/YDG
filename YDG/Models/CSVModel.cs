using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace YDG.Models
{
    internal class CSVModel
    {
        public string PostDate { get; set; }
        public string PostTime { get; set; }
        public string PostText { get; set; }
        public string PostType => "Пост";
        public string PostUrl { get; set; }
        public string PostTonality { get; set; } = "Нейтральный";
        public string AuthorName { get; set; }
        public string AuthorUrl { get; set; }
        public string AuthorSubscribersCount { get; set; }
        public string Platform => "Яндекс.Район";
        public string GroupName { get; set; }
        public string GroupUrl { get; set; }
        public string GroupSubscribersCount { get; set; }
        public string LikeCount { get; set; }
        public string CommentsCount { get; set; }
        public string RepostsCount { get; set; }
        public string ViewsCount { get; set; }
        public string PostRegion { get; set; }
        public string PostTopic { get; set; }
        public string PostSubtopic { get; set; }



        ///// <summary>
        ///// Загрузка данных из CSV файла
        ///// </summary>
        ///// <param name="fileName">Имя файла</param>
        ///// <param name="first_row_is_column_name">Указывает что первая строка файла содержит имена столбцов</param>
        ///// <returns></returns>
        //internal DataTable ImportFromCSV(string fileName, bool first_row_is_column_name)
        //{
        //    DataTable ret = new DataTable();
        //    TextReader tr = new StreamReader(fileName, Encoding.GetEncoding(1251));
        //    String line = "";
        //    line = tr.ReadLine();
        //    string[] columns = line.Split(separator);
        //    for (int i = 0; i < columns.Length; i++)
        //        if (first_row_is_column_name)
        //            ret.Columns.Add(columns[i]);
        //        else
        //            ret.Columns.Add("column_" + i.ToString());
        //    if (first_row_is_column_name)
        //        line = tr.ReadLine();
        //    while (line != null)
        //    {
        //        string[] blocks = line.Split(separator);
        //        DataRow dr = ret.NewRow();
        //        for (int i = 0; i < (blocks.Length & ret.Columns.Count); i++)
        //        {
        //            dr["column_" + i.ToString()] = blocks[i];
        //        }
        //        ret.Rows.Add(dr);
        //        line = tr.ReadLine();
        //    }
        //    return ret;
        //}


        ///// <summary>
        ///// Выгрузка в CSV файл
        ///// </summary>
        ///// <param name="fileName">Имя файла</param>
        ///// <param name="table">Таблица для выгрузки</param>
        ///// <returns></returns>
        //internal bool ExportToCSV(string fileName, ICollection<YDPostModel> models)
        //{
        //    if (models == null) { throw new NullReferenceException("Нет данных для сохранения"); }

        //    CsvExport 


        //    using (StreamWriter sw = new StreamWriter(file))
        //    {
        //        sw.Write(models);
        //    }

        //    using (StreamWriter writer = new StreamWriter(file))
        //    {
        //        using (var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture))
        //        {
        //            csvWriter.Configuration.Delimiter = ";";
        //            csvWriter.Configuration.HasHeaderRecord = true;
        //            csvWriter.WriteHeader(typeof(Person));

        //            csvWriter.WriteHeader<Person>();
        //            csvWriter.NextRecord();

        //            foreach (var item in someTexts)
        //            {
        //                csvWriter.WriteField(item);
        //                csvWriter.NextRecord();
        //            }

        //            writer.Flush();
        //        }
        //    }



        //    return true;
        //}

    }
}
