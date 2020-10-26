using YDG.Data;
using YDG.ViewModels.Base;

namespace YDG.ViewModels.DataModels
{
    internal class YDGViewModel : ViewModelBase
    {

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
