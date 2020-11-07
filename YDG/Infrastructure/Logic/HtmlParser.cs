using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace YDG.Infrastructure.Logic
{
    internal static class HtmlParser
    {
        private static readonly Regex _tags_ = new Regex(@"<[^>]+?>", RegexOptions.Multiline | RegexOptions.Compiled);

        //add characters that are should not be removed to this regex
        private static readonly Regex _notOkCharacter_ = new Regex(@"[^\w;&#@.:/\\?=|%!() -]", RegexOptions.Compiled);

        //private static readonly Regex _paragraphTags = new Regex(@"<(?!br|p|\/p)[^>]*>", RegexOptions.Compiled);

        private static readonly Regex _newLineTagRegex = new Regex(@"<.?\s? br>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly string _startTagPattern = @"<\w*";




        public static String UnHtml(String html)
        {
            html = HttpUtility.UrlDecode(html);
            html = HttpUtility.HtmlDecode(html);

            html = RemoveTag(html, "<!--", "-->");
            html = RemoveTag(html, "<script", "</script>");
            html = RemoveTag(html, "<style", "</style>");

            //replace matches of these regexes with space
            html = _newLineTagRegex.Replace(html, string.Format("\r\n"));
            html = html.Replace(@"</p>", @"</p>" + string.Format("\r\n"), StringComparison.OrdinalIgnoreCase);
            html = _tags_.Replace(html, " ");
            //html = _notOkCharacter_.Replace(html, " ");
            html = SingleSpacedTrim(html);

            return html;
        }


        private static String RemoveTag(String html, String startTag, String endTag)
        {
            Boolean bAgain;
            do
            {
                bAgain = false;
                Int32 startTagPos = html.IndexOf(startTag, 0, StringComparison.CurrentCultureIgnoreCase);
                if (startTagPos < 0)
                    continue;
                Int32 endTagPos = html.IndexOf(endTag, startTagPos + 1, StringComparison.CurrentCultureIgnoreCase);
                if (endTagPos <= startTagPos)
                    continue;
                html = html.Remove(startTagPos, endTagPos - startTagPos + endTag.Length);
                bAgain = true;
            } while (bAgain);
            return html;
        }

        private static String SingleSpacedTrim(String inString)
        {
            StringBuilder sb = new StringBuilder();
            Boolean inBlanks = false;
            foreach (Char c in inString)
            {
                switch (c)
                {
                    //case '\r':
                    //case '\n':
                    case '\t':
                    case ' ':
                        if (!inBlanks)
                        {
                            inBlanks = true;
                            sb.Append(' ');
                        }
                        continue;
                    default:
                        inBlanks = false;
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString().Trim();
        }











        //internal ObservableCollection<YDPostModel> GetPosts(string html)
        //{
        //    ObservableCollection<YDPostModel> posts = new ObservableCollection<YDPostModel>();

        //    string jobstr = GetHtmlTagCode(HtmlBlock.NewsBlock, HtmlBlock.NewsBlock);
        //    if (string.IsNullOrWhiteSpace(jobstr)) { return posts; }

        //    string separator = "~";
        //    jobstr = jobstr.Replace(HtmlBlock.ArticleBlock, separator + HtmlBlock.ArticleBlock);

        //    List<string> articles = jobstr.Split(separator).ToList();
            
        //    foreach (var article in articles)
        //    {
        //        YDPostModel post = GetPost(article);

        //        if (post == null) { continue; }
        //        posts.Add(post);
        //    }

        //    return posts;
        //}


        internal static string GetHtmlTagCode(string html, string tag)
        {
            int indexOfBlockPosition = html.IndexOf(tag);
            if (indexOfBlockPosition < 0) { return string.Empty; }

            html = html.Substring(indexOfBlockPosition);

            int a = tag.IndexOf(' '); if (a < 0) { a = tag.Length; }
            int b = tag.IndexOf('>'); if (b < 0) { b = tag.Length; }
            int startTagLength =  a < b ? a : b;
            string startTag = tag.Substring(0, startTagLength + 1);

            string endTag = (startTag[0] + @"/" + startTag.Substring(1)).Trim();
            if (endTag[endTag.Length - 1] != '>') { endTag += '>'; }

            //int countStartTag = CountWords(html, startTag);
            //int countEndTag = CountWords(html, endTag);

            int tagCount = 1;
            int startTagIndex = startTag.Length;
            int endTagIndex = 0;
            do
            {

                endTagIndex = html.IndexOf(endTag, endTagIndex);
                if (endTagIndex < 0)
                {
                    endTagIndex = html.Length;
                    endTagIndex = html.IndexOf('>', startTagIndex);         // ?
                }

                startTagIndex = html.IndexOf(startTag, startTagIndex);
                if (startTagIndex < 0) { startTagIndex = html.Length; }

                if (startTagIndex < endTagIndex)
                {
                    tagCount++;
                    startTagIndex += startTag.Length;
                }
                else
                {
                    tagCount--;
                    endTagIndex += endTag.Length;
                }
            } while (tagCount > 0);

            string result = html.Substring(0, endTagIndex);


            return result;
        }


        ///// <summary>
        ///// Количество вхождений строки S0 в строку S
        ///// </summary>
        ///// <param name="s"></param>
        ///// <param name="s0"></param>
        ///// <returns></returns>
        //private int CountWords(string s, string s0)
        //{
        //    int count = (s.Length - s.Replace(s0, "").Length) / s0.Length;
        //    return count;
        //}



        //private YDPostModel GetPost(string article)
        //{

        //    //YDPostModel post = new YDPostModel();

        //    //post.Text = HtmlParser.UnHtml(GetHtmlTagCode(article, HtmlBlock.Text));



        //    //post.Author = GetAuthor(GetHtmlTagCode(article, HtmlBlock.Author));
        //    //if (post.Author == null) { return null; }

        //    ////post.Platform = GetPlatform(article);
        //    ////if (post.Platform == null) { return null; }

        //    ////post.Reactions = GetStats(article);

        //    //post.Dtg = new DateTime();
            






        //    /* 
             
        //            // Decode the encoded string.
        //            StringWriter myWriter = new StringWriter();                    
        //            HttpUtility.HtmlDecode(this.Html, myWriter);             
        //     */

        //    throw new NotImplementedException();
        //}






        internal static int GetHtmlData(Uri url, string block, string tag)
        {
            if (url == null 
                || string.IsNullOrWhiteSpace(block) 
                || string.IsNullOrWhiteSpace(tag))
            {
                return 0;
            }

            string html = GetHtmlCodeText(url.AbsoluteUri);





            /*
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            Cookie cookie = new Cookie
            {
                Name = "beget",
                Value = "begetok"
            };
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Uri(url.AbsoluteUri), cookie);

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex) 
            { 
                response = (HttpWebResponse)ex.Response;
                if (response.StatusCode != HttpStatusCode.Redirect) { throw (ex); }
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }
                html = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
            }
            */



            if (string.IsNullOrWhiteSpace(html)) { return 0; }





            //using (WebClient client = new WebClient())
            //{
            //    string strUrl = url.AbsoluteUri;

            //    Stream data = client.OpenRead(url.AbsoluteUri);
            //    StreamReader reader = new StreamReader(data);
            //    html = reader.ReadToEnd();
            //    data.Close();
            //    reader.Close();
            //}
            
            string htmlBlock = GetHtmlTagCode(html, block);
            string numberOf = HtmlParser.UnHtml(GetHtmlTagCode(htmlBlock, tag));

            try
            {
                return Int32.Parse(numberOf);
            }
            catch (Exception)
            {
                return 0;
            }
        }


        private static string GetHtmlCodeText(string url)
        {
            string html = string.Empty;
            //url = @"https://www.javaer101.com/article/5043442.html";


            // отключить перенаправление
            //HttpClientHandler httpClientHandler = new HttpClientHandler();
            //httpClientHandler.AllowAutoRedirect = false;

            HttpClient httpClient = new HttpClient(/*httpClientHandler*/);
            using (HttpResponseMessage response = httpClient.GetAsync(url).Result)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {

                }

                using (HttpContent content = response.Content)
                {
                    html = content.ReadAsStringAsync().Result;
                }
            }

            return html;
        }


        private static async Task<string> GetHtmlPageTextAsync(string url)
        {
            string html = string.Empty;
            await Task.Run(async () => {

                // ... используем HttpClient.
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(url))
                using (HttpContent content = response.Content)
                {
                    // ... записать ответ
                    html = await content.ReadAsStringAsync();
                }
            });
            return html;
        }















        //internal string GetText(string html)
        //{            
        //    int indexOfBlockPosition = html.IndexOf(HtmlBlock.NewsBlock);
        //    string jobstr = indexOfBlockPosition > 0 ? html.Substring(indexOfBlockPosition) : string.Empty;           

        //    //post.Text = HtmlParser.UnHtml(GetHtmlTagCode(article, HtmlBlock.Text));

        //    string separator = "~";
        //    jobstr = jobstr.Replace(HtmlBlock.ArticleBlock, separator + HtmlBlock.ArticleBlock);

        //    List<string> articles = jobstr.Split(separator).ToList();

        //    string txt = string.Empty;

        //    foreach (var article in articles)
        //    {
        //        YDPostModel post = new YDPostModel();

        //        post.Text = HtmlParser.UnHtml(GetHtmlTagCode(article, HtmlBlock.Text));

        //        string dtgTag = GetHtmlTagCode(article, HtmlBlock.Dtg);
        //        //post.Dtg = GetDtg(dtgTag);
        //        post.PostUrl = GetUrl(dtgTag);

        //        post.Author = GetAuthorGroup(GetHtmlTagCode(article, HtmlBlock.Author));
        //        if (post.Author?.Url != null)
        //        {
        //            post.Author.FollowersCount = GetHtmlData(post.Author.Url, HtmlBlock.UserInfo, HtmlBlock.UserInfoFollowersCount);
        //        }

        //        post.Group = GetAuthorGroup(GetHtmlTagCode(article, HtmlBlock.Group));
        //        if (post.Group?.Url != null)
        //        {
        //            post.Group.FollowersCount = GetHtmlData(post.Group.Url, HtmlBlock.GroupInfo, HtmlBlock.GroupInfoFollowersCount);
        //        }

        //        post.Stats = GetStats(GetHtmlTagCode(article, HtmlBlock.StatsBlock));


        //        txt += post.Text + String.Format("\r\n\r\n");

        //        //var Text = HtmlParser.UnHtml(GetHtmlTagCode(article, HtmlBlock.ArticleBlock));

        //        //var art = HtmlParser.UnHtml(article);
        //    }
        //    return string.IsNullOrWhiteSpace(txt) ? "Нет данных" : txt;
        //}

        //private YDPostStatsModel GetStats(string tag)
        //{
        //    if (string.IsNullOrWhiteSpace(tag)) { return null; }

        //    YDPostStatsModel stats = new YDPostStatsModel();

        //    stats.CommentsCount = StringConverter.ToInt32(HtmlParser.UnHtml(GetHtmlTagCode(tag, HtmlBlock.CommentsCount)));
        //    stats.LikesCount = StringConverter.ToInt32(HtmlParser.UnHtml(GetHtmlTagCode(tag, HtmlBlock.LikesCount)));
        //    stats.RepostsCount = 0;
        //    stats.ViewsCount = StringConverter.ToInt32(HtmlParser.UnHtml(GetHtmlTagCode(tag, HtmlBlock.ViewsCount)));

        //    return stats;
        //}


        //private YDAuthorGroupModel GetAuthorGroup(string tag)
        //{
        //    if (string.IsNullOrWhiteSpace(tag)) { return null; }

        //    YDAuthorGroupModel model = new YDAuthorGroupModel();

        //    model.Name = HtmlParser.UnHtml(tag);
        //    if (string.IsNullOrWhiteSpace(model.Name)) { return null; }

        //    model.Url = GetUrl(tag);
        //    return model;
        //}


        internal static Uri GetUrl(string author)
        {
            int hrefPosition = author.IndexOf("href");
            if (hrefPosition < 0) { return null; }

            StringBuilder url = new StringBuilder();

            int httpPosition = author.IndexOf("http");
            if (httpPosition < 0)
            {
                url.Append("https://local.yandex.ru");
                httpPosition = author.IndexOf('"', hrefPosition);
            }
            int endUrlPosition = author.IndexOf('"', httpPosition + 1);
            if (endUrlPosition < 0) { endUrlPosition = author.Length - 1; }

            string urlTmp = author.Substring(httpPosition + 1, endUrlPosition - httpPosition - 1);

            url.Append(urlTmp);
            return new Uri(url.ToString());
        }
    }
}
