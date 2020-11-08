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

        private static readonly Regex _notOkCharacter_ = new Regex(@"[^\w;&#@.:/\\?=|%!() -]", RegexOptions.Compiled);

        private static readonly Regex _newLineTagRegex = new Regex(@"<.?\s? br>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly string _startTagPattern = @"<\w*";




        public static String UnHtml(String html)
        {
            html = HttpUtility.UrlDecode(html);
            html = HttpUtility.HtmlDecode(html);

            html = RemoveTag(html, "<!--", "-->");
            html = RemoveTag(html, "<script", "</script>");
            html = RemoveTag(html, "<style", "</style>");

            html = _newLineTagRegex.Replace(html, string.Format("\r\n"));
            html = html.Replace(@"</p>", @"</p>" + string.Format("\r\n"), StringComparison.OrdinalIgnoreCase);
            html = _tags_.Replace(html, " ");
            html = SingleSpacedTrim(html);

            return html;
        }

        /// <summary>
        /// Remove tags
        /// </summary>
        /// <param name="html"></param>
        /// <param name="startTag"></param>
        /// <param name="endTag"></param>
        /// <returns></returns>
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

        /// <summary>
        /// single spaced trim
        /// </summary>
        /// <param name="inString"></param>
        /// <returns></returns>
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


        internal static int GetHtmlData(Uri url, string block, string tag)
        {
            if (url == null 
                || string.IsNullOrWhiteSpace(block) 
                || string.IsNullOrWhiteSpace(tag))
            {
                return 0;
            }

            string html = GetHtmlCodeText(url.AbsoluteUri);

            if (string.IsNullOrWhiteSpace(html)) { return 0; }

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
