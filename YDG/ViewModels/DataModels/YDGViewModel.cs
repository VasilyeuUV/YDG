using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Web;
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

            HtmlParser parser = new HtmlParser();
            this.Html = parser.GetText(this._html);
            
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
                    HtmlParser parser = new HtmlParser();
                    _posts = parser.GetPosts(this._html);
                }
                return _posts;
            }

            set => Set(ref _posts, value);
        }


        #endregion

    }
}


/*   
 
         Console.WriteLine("Enter a string having '&', '<', '>' or '\"' in it: ");
        string myString = Console.ReadLine();

        // Encode the string.
        string myEncodedString = HttpUtility.HtmlEncode(myString);

        Console.WriteLine($"HTML Encoded string is: {myEncodedString}");
        StringWriter myWriter = new StringWriter();

        // Decode the encoded string.
        HttpUtility.HtmlDecode(myEncodedString, myWriter);

        string myDecodedString = myWriter.ToString();
        Console.Write($"Decoded string of the above encoded string is: {myDecodedString}");
 
 */