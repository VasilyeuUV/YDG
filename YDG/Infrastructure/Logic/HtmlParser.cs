using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using YDG.Data;
using YDG.Models;

namespace YDG.Infrastructure.Logic
{
    internal class HtmlParser
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
            html = _tags_.Replace(html, " ");
            html = _notOkCharacter_.Replace(html, " ");
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











        internal ObservableCollection<YDPostModel> GetPosts(string html)
        {
            ObservableCollection<YDPostModel> posts = new ObservableCollection<YDPostModel>();

            string jobstr = GetHtmlTagCode(HtmlBlock.NewsBlock, HtmlBlock.NewsBlock);
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


        private string GetHtmlTagCode(string html, string tag)
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

            int countStartTag = CountWords(html, startTag);
            int countEndTag = CountWords(html, endTag);

            int tagCount = 1;
            int startTagIndex = startTag.Length;
            int endTagIndex = 0;
            do
            {
                startTagIndex = html.IndexOf(startTag, startTagIndex);
                if (startTagIndex < 0) { startTagIndex = html.Length; }

                endTagIndex = html.IndexOf(endTag, endTagIndex);
                if (endTagIndex < 0) { endTagIndex = html.Length; }

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


        /// <summary>
        /// Количество вхождений строки S0 в строку S
        /// </summary>
        /// <param name="s"></param>
        /// <param name="s0"></param>
        /// <returns></returns>
        private int CountWords(string s, string s0)
        {
            int count = (s.Length - s.Replace(s0, "").Length) / s0.Length;
            return count;
        }



        private YDPostModel GetPost(string article)
        {

            YDPostModel post = new YDPostModel();

            post.Text = HtmlParser.UnHtml(GetHtmlTagCode(article, HtmlBlock.Text));
            


            //post.Author = GetAuthor(article);
            //if (post.Author == null) { return null; }

            //post.Platform = GetPlatform(article);
            //if (post.Platform == null) { return null; }

            //post.Reactions = GetStats(article);

            post.Dtg = new DateTime();
            






            /* 
             
                    // Decode the encoded string.
                    StringWriter myWriter = new StringWriter();                    
                    HttpUtility.HtmlDecode(this.Html, myWriter);             
             */

            throw new NotImplementedException();
        }






        internal string GetText(string html)
        {
            



            int indexOfBlockPosition = html.IndexOf(HtmlBlock.NewsBlock);
            string jobstr = indexOfBlockPosition > 0 ? html.Substring(indexOfBlockPosition) : string.Empty;

            

            string separator = "~";
            jobstr = jobstr.Replace(HtmlBlock.ArticleBlock, separator + HtmlBlock.ArticleBlock);

            List<string> articles = jobstr.Split(separator).ToList();

            foreach (var article in articles)
            {
                var Text = HtmlParser.UnHtml(GetHtmlTagCode(article, HtmlBlock.ArticleBlock));

                string content = HtmlParser.UnHtml(GetHtmlTagCode(article, HtmlBlock.Text));

                //var art = HtmlParser.UnHtml(article);
            }

            return jobstr;
        }
    }
}
