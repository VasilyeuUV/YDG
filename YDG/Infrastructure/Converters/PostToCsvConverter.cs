using System;
using System.Collections.Generic;
using System.Text;
using YDG.Interfaces;
using YDG.Models;

namespace YDG.Infrastructure.Converters
{
    internal class PostToCsvConverter
    {

        internal static IEnumerable<ICsvModel> Convert(ICollection<YDPostModel> records)
        {
            List<CSVModel> csvList = new List<CSVModel>();
            foreach (var post in records)
            {
                var csv = GetCsv(post);
                if (csv != null ) { csvList.Add(csv); }                
            }
            return csvList;
        }

        private static CSVModel GetCsv(YDPostModel post)
        {
            try
            {
                CSVModel csv = new CSVModel()
                {
                    PostDate = post.Dtg.ToString(" dd.MM.yyyy"),
                    PostTime = post.Dtg.ToString(" HH:mm"),
                    PostText = ToUtf8(post.Text),
                    PostUrl = post.PostUrl?.AbsoluteUri,

                    AuthorName = ToUtf8(post.Author?.Name),
                    AuthorUrl = post.Author?.Url?.AbsoluteUri,
                    AuthorSubscribersCount = post.Author?.FollowersCount.ToString(),

                    GroupName = ToUtf8(post.Group?.Name),
                    GroupUrl = post.Group.Url?.AbsoluteUri,
                    GroupSubscribersCount = post.Group?.FollowersCount.ToString(),

                    CommentsCount = post.Stats?.CommentsCount.ToString(),
                    LikeCount = post.Stats?.LikesCount.ToString(),
                    RepostsCount = post.Stats?.RepostsCount.ToString(),
                    ViewsCount = post.Stats?.ViewsCount.ToString(),
                    PotencialViewsCount = post.Group?.FollowersCount.ToString(),

                    PostRegion = ToUtf8(post.Group?.Name),
                };



                return csv;
            }
            catch (Exception)
            {
                return null;
            }

        }

        private static string ToUtf8(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
