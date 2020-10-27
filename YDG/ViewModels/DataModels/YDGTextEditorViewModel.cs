using System;
using System.IO;
using System.Web;
using System.Windows;
using System.Windows.Input;
using YDG.Data;
using YDG.Infrastructure.Commands;
using YDG.ViewModels.Base;

namespace YDG.ViewModels.DataModels
{
    internal class YDGTextEditorViewModel : ViewModelBase
    {

        #region HTML_CODE
        private string _html = string.Empty;

        public string Html
        {
            get => _html;
            set => Set(ref _html, value);
        }
        #endregion

        #region КОМАНДЫ

        #region TextEditorCloseWindowCommand

        private ICommand _textEditoCloseWindowCommand = null;
        public ICommand TextEditorCloseWindowCommand =>
            _textEditoCloseWindowCommand ??= new LambdaCommand(
                obj =>
                {
                    this.Html = string.Empty;
                    YDGData.Html = string.Empty;

                    Window w = obj as Window;
                    w?.Close();
                });

        #endregion

        #region TextEditorExecuteCommand

        private ICommand _textEditorExecuteCommand = null;
        public ICommand TextEditorExecuteCommand =>
            _textEditorExecuteCommand ??= new LambdaCommand(
                obj =>
                {
                    YDGData.Html = this.Html;
                    this.Html = string.Empty;

                    Window w = obj as Window;
                    w?.Close();
                },
                obj =>
                {
                    return this.Html.Length < 20 ? false : true;
                }
                );

        #endregion 

        #endregion
    }
}
