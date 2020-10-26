using System.IO;
using System.Text;
using System.Web;
using YDG.Data;
using YDG.ViewModels.Base;

namespace YDG.ViewModels.DataModels
{
    internal class YDGViewModel : ViewModelBase
    {

        private string temp = "&<, >";


        public YDGViewModel()
        {
            YDGData.HtmlChanged += YDGData_HtmlChanged;
        }

        private void YDGData_HtmlChanged(object sender, System.EventArgs e)
        {
            this.Html = YDGData.Html;
        }


        #region HTML_CODE
        private string _html = string.Empty;

        public string Html
        {
            get => _html;
            set => Set(ref _html, value);
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