using System;
using System.Collections.Generic;
using System.Text;

namespace YDG.Models
{
    internal class YDAuthorModel
    {
        public string NickName { get; internal set; }

        public Uri Url { get; internal set; }

        public int FollowersCount { get; internal set; }
    }
}
