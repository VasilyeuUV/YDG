using System;
using YDG.Services.Interfaces;

namespace YDG.Models
{
    internal class YDAuthorGroupModel : INameable, ILinkable, IFollowersCounable
    {
        public string Name { get; set; }

        public Uri Url { get; set; }

        public int FollowersCount { get; set; }
    }
}
