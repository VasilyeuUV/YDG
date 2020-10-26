using System;

namespace YDG.Models
{
    internal class YDPostModel
    {

        public DateTime Dtg { get; internal set; }

        public string Text { get; internal set; }

        public Uri PostUrl { get; internal set; }

        public YDAuthorModel Author { get; internal set; }

        public YDPlatformModel Platform { get; internal set; }

        public YDPostStatsModel Reactions { get; set; }

    }
}
