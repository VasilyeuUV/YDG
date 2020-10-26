using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using YDG.Data;
using YDG.Models;

namespace YDG.Infrastructure.Logic
{
    internal class HtmlParser
    {
        internal ObservableCollection<YDPostModel> GetPosts(string html)
        {
            ObservableCollection<YDPostModel> collection = new ObservableCollection<YDPostModel>();

            

            return collection;
        }

        internal string GetText(string html)
        {
            int indexOfBlockPosition = html.IndexOf(HtmlBlock.NewsBlock); 
            string jobstr = indexOfBlockPosition > 0 ? html.Substring(indexOfBlockPosition) : string.Empty;

            List<string> words = jobstr.Split(HtmlBlock.ArticleBlock).ToList();


            return jobstr;
        }
    }
}
