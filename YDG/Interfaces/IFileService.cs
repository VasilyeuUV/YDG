using System.Collections.Generic;
using YDG.Models;

namespace YDG.Interfaces
{
    public interface IFileService
    {
        internal List<YDPostModel> Open(string filename);
        internal void Save(string filename, List<YDPostModel> phonesList);
    }
}
