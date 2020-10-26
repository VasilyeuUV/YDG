using System;

namespace YDG.Data
{
    internal static class YDGData
    {
        internal static event EventHandler HtmlChanged = delegate { };


        private static string _html = string.Empty;

        public static string Html
        {
            get => _html;
            set
            {
                _html = value;
                HtmlChanged(typeof(YDGData), EventArgs.Empty);
            }
        }
    }
}
