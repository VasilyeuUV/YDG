using System;
using System.Collections.Generic;
using System.Text;

namespace YDG.Data
{
    internal static class HtmlBlock
    {
        public static string NewsBlock { get; } = "<div class=\"feed__main-content";
        public static string ArticleBlock { get; } = "<article class=\"event-layout-default";
        
        
        public static string Dtg { get; } = "<a class=\"event-head__time";
        public static string Text { get; } = "<div class=\"event-content-news\"";


        public static string StatsBlock { get; } = "<div class=\"event-footer__stats";
        public static string LikesCount { get; } = "<span class=\"event-footer-button__text";
        public static string CommentsCount { get; } = "<span class=\"event-footer-comments-info__count";
        public static string ViewsCount { get; } = "<span class=\"event-footer__views";


        public static string Author { get; } = "<a class=\"event-head__user-link";
        public static string UserInfo { get; } = "<div class=\"user-info \">";
        public static string UserInfoFollowersCount { get; } = "<span class=\"tabs__item-content\"";

        public static string Group { get; } = "<a class=\"event-head__location-link";
        public static string GroupInfo { get; } = "<div class=\"district-feed-head\"";
        public static string GroupInfoFollowersCount { get; } = "<span class=\"text district-feed-head__members-count\"";

    }
}
