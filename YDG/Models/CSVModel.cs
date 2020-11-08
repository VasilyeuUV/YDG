using YDG.Interfaces;

namespace YDG.Models
{
    internal class CSVModel : ICsvModel
    {
        public string PostDate { get; set; }
        public string PostTime { get; set; }
        public string PostText { get; set; }
        public string PostType => "Пост";
        public string PostUrl { get; set; }
        public string PostTonality { get; set; } = "Нейтральная";
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
        public string PotencialViewsCount { get; set; }
        public string PostRegion { get; set; }
        public string PostTopic { get; set; }
        public string PostSubtopic { get; set; }
    }
}
