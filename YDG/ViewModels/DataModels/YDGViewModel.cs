using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using YDG.Data;
using YDG.Infrastructure.Logic;
using YDG.Models;
using YDG.ViewModels.Base;

namespace YDG.ViewModels.DataModels
{
    internal class YDGViewModel : ViewModelBase
    {

        /// <summary>
        /// CTOR
        /// </summary>
        public YDGViewModel()
        {
            YDGData.HtmlChanged += YDGData_HtmlChanged;
        }

        private void YDGData_HtmlChanged(object sender, System.EventArgs e)
        {
            this._html = YDGData.Html;
            this.Posts = GetPosts(Html);           
        }


        #region HTML_CODE
        private string _html = string.Empty;

        public string Html
        {
            get => _html;
            set => Set(ref _html, value);
        }
        #endregion


        #region PostModel

        private YDPostModel _postModel = null;

        #endregion


        #region Posts

        private ObservableCollection<YDPostModel> _posts = null;

        public ObservableCollection<YDPostModel> Posts
        {
            get => _posts;
            set => Set(ref _posts, value);
        }


        private ObservableCollection<YDPostModel> GetPosts(string html)
        {
            if (string.IsNullOrWhiteSpace(html)) { return null; }

            string jobstr = HtmlParser.GetHtmlTagCode(html, HtmlBlock.NewsBlock);
            if (string.IsNullOrWhiteSpace(jobstr)) { return null; }

            string separator = "~";
            jobstr = jobstr.Replace(HtmlBlock.ArticleBlock, separator + HtmlBlock.ArticleBlock);

            List<string> articles = jobstr.Split(separator).ToList();

            ObservableCollection<YDPostModel> posts = new ObservableCollection<YDPostModel>();
            foreach (var article in articles)
            {
                YDPostModel post = GetPost(article);

                if (post == null) { continue; }
                posts.Add(post);
            }

            return posts;
        }

        private YDPostModel GetPost(string article)
        {
            if (string.IsNullOrWhiteSpace(article)) { return null; }

            YDPostModel post = new YDPostModel();

            // Post
            post.Text = HtmlParser.UnHtml(HtmlParser.GetHtmlTagCode(article, HtmlBlock.Text));
            string dtgTag = HtmlParser.GetHtmlTagCode(article, HtmlBlock.Dtg);
            post.Dtg = GetDtg(HtmlParser.UnHtml(dtgTag));
            post.PostUrl = HtmlParser.GetUrl(dtgTag);

            // Author
            post.Author = GetAuthorGroup(HtmlParser.GetHtmlTagCode(article, HtmlBlock.Author));
            if (post.Author?.Url != null)
            {
                post.Author.FollowersCount = HtmlParser.GetHtmlData(post.Author.Url, HtmlBlock.UserInfo, HtmlBlock.UserInfoFollowersCount);
            }

            // Group
            post.Group = GetAuthorGroup(HtmlParser.GetHtmlTagCode(article, HtmlBlock.Group));
            if (post.Group?.Url != null)
            {
                post.Group.FollowersCount = HtmlParser.GetHtmlData(post.Group.Url, HtmlBlock.GroupInfo, HtmlBlock.GroupInfoFollowersCount);
            }

            // Statistics
            post.Stats = GetStats(HtmlParser.GetHtmlTagCode(article, HtmlBlock.StatsBlock));

            return post;
        }

        private System.DateTime GetDtg(string dtgTag)
        {
            int value = GetInt(dtgTag);

            DateTime dtg = DateTime.Now;

            if (dtgTag.ToLower().IndexOf("м") >= 0)
            {
                return dtg.AddMinutes(-value);
            }
            if (dtgTag.ToLower().IndexOf("ч") >= 0)
            {
                return dtg.AddHours(-value);
            }
            if (dtgTag.ToLower().IndexOf("д") >= 0)
            {
                return dtg.AddDays(-value);
            }
            if (dtgTag.ToLower().IndexOf("вчера") >= 0)
            {
                return dtg.AddDays(-1);
            }
            return dtg;
        }

        private static int GetInt(string dtgTag)
        {
            int value;
            int.TryParse(string.Join("", dtgTag.Where(c => char.IsDigit(c))), out value);

            if (dtgTag.ToLower().IndexOf("т") >= 0)
            {
                value *= 1000;
            }

            return value;
        }

        private YDAuthorGroupModel GetAuthorGroup(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag)) { return null; }

            YDAuthorGroupModel model = new YDAuthorGroupModel();

            model.Name = HtmlParser.UnHtml(tag);
            if (string.IsNullOrWhiteSpace(model.Name)) { return null; }

            model.Url = HtmlParser.GetUrl(tag);
            return model;
        }


        private YDPostStatsModel GetStats(string tag)
        {
            YDPostStatsModel stats = new YDPostStatsModel();
            if (string.IsNullOrWhiteSpace(tag)) { return stats; }

            stats.CommentsCount = GetInt(HtmlParser.UnHtml(HtmlParser.GetHtmlTagCode(tag, HtmlBlock.CommentsCount)));
            stats.LikesCount = GetInt(HtmlParser.UnHtml(HtmlParser.GetHtmlTagCode(tag, HtmlBlock.LikesCount)));            
            stats.ViewsCount = GetInt(HtmlParser.UnHtml(HtmlParser.GetHtmlTagCode(tag, HtmlBlock.ViewsCount)));
            stats.RepostsCount = 0;

            return stats;
        }

        #endregion

    }
}
