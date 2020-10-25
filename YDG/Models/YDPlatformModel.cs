using System;

namespace YDG.Models
{
    internal class YDPlatformModel
    {

        public string Platform { get; } = "Яндекс.Район";

        public string GroupName { get; internal set; }

        public Uri GroupUrl { get; internal set; }

        public int GroupFollowersCount { get; set; }

    }
}
