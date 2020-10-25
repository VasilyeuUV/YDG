using YDG.ViewModels.Base;

namespace YDG.ViewModels.DataModels
{
    internal class YDGViewModel : ViewModelBase
    {

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
