using System;

namespace YDG.Models
{
    internal class YDPostModel
    {

        public DateTime Dtg { get; internal set; }

        public string Text { get; internal set; }

        public Uri PostUrl { get; internal set; }

        public YDAuthorGroupModel Author { get; internal set; }

        public YDAuthorGroupModel Group { get; internal set; }

        public YDPostStatsModel Stats { get; set; }

    }
}
