using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using YDG.Data;
using YDG.Infrastructure.Converters;
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

            //HtmlParser parser = new HtmlParser();
            //this.Html = parser.GetText(this._html);
            
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
            get
            {
                if (_posts == null)
                {
                    //HtmlParser parser = new HtmlParser();
                    _posts = GetPosts(this._html);
                }
                return _posts;
            }

            set => Set(ref _posts, value);
        }


        private ObservableCollection<YDPostModel> GetPosts(string html)
        {
            ObservableCollection<YDPostModel> posts = new ObservableCollection<YDPostModel>();

            string jobstr = HtmlParser.GetHtmlTagCode(HtmlBlock.NewsBlock, HtmlBlock.NewsBlock);
            if (string.IsNullOrWhiteSpace(jobstr)) { return posts; }

            string separator = "~";
            jobstr = jobstr.Replace(HtmlBlock.ArticleBlock, separator + HtmlBlock.ArticleBlock);

            List<string> articles = jobstr.Split(separator).ToList();

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
            //post.Dtg = GetDtg(dtgTag);
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

            stats.CommentsCount = StringConverter.ToInt32(HtmlParser.UnHtml(HtmlParser.GetHtmlTagCode(tag, HtmlBlock.CommentsCount)));
            stats.LikesCount = StringConverter.ToInt32(HtmlParser.UnHtml(HtmlParser.GetHtmlTagCode(tag, HtmlBlock.LikesCount)));            
            stats.ViewsCount = StringConverter.ToInt32(HtmlParser.UnHtml(HtmlParser.GetHtmlTagCode(tag, HtmlBlock.ViewsCount)));
            stats.RepostsCount = 0;

            return stats;
        }

        #endregion

    }
}


/*   
 
            int indexOfBlockPosition = html.IndexOf(HtmlBlock.NewsBlock);
            string jobstr = indexOfBlockPosition > 0 ? html.Substring(indexOfBlockPosition) : string.Empty;           

            //post.Text = HtmlParser.UnHtml(GetHtmlTagCode(article, HtmlBlock.Text));

            string separator = "~";
            jobstr = jobstr.Replace(HtmlBlock.ArticleBlock, separator + HtmlBlock.ArticleBlock);

            List<string> articles = jobstr.Split(separator).ToList();

            string txt = string.Empty;

            foreach (var article in articles)
            {
                YDPostModel post = new YDPostModel();

                post.Text = HtmlParser.UnHtml(GetHtmlTagCode(article, HtmlBlock.Text));

                string dtgTag = GetHtmlTagCode(article, HtmlBlock.Dtg);
                //post.Dtg = GetDtg(dtgTag);
                post.PostUrl = GetUrl(dtgTag);

                post.Author = GetAuthorGroup(GetHtmlTagCode(article, HtmlBlock.Author));
                if (post.Author?.Url != null)
                {
                    post.Author.FollowersCount = GetHtmlData(post.Author.Url, HtmlBlock.UserInfo, HtmlBlock.UserInfoFollowersCount);
                }

                post.Group = GetAuthorGroup(GetHtmlTagCode(article, HtmlBlock.Group));
                if (post.Group?.Url != null)
                {
                    post.Group.FollowersCount = GetHtmlData(post.Group.Url, HtmlBlock.GroupInfo, HtmlBlock.GroupInfoFollowersCount);
                }

                post.Stats = GetStats(GetHtmlTagCode(article, HtmlBlock.StatsBlock));


                txt += post.Text + String.Format("\r\n\r\n");

                //var Text = HtmlParser.UnHtml(GetHtmlTagCode(article, HtmlBlock.ArticleBlock));

                //var art = HtmlParser.UnHtml(article);
            }
 
 */