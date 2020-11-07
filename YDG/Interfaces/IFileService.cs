using System.Collections.Generic;
using YDG.Models;

namespace YDG.Interfaces
{
    internal interface IFileService
    {
        /// <summary>
        /// Open File
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        internal IEnumerable<YDPostModel> Open(string filename);

        /// <summary>
        /// Save File
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="list"></param>
        internal void Save<T>(string path, IEnumerable<T> records) where T : ICsvModel;
    }
}
