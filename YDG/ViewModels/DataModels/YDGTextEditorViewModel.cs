using System.Windows.Input;
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

        #region TextEditorExitCommandCommand

        private ICommand _textEditorExitCommandCommand = null;
        public ICommand TextEditorExitCommandCommand =>
            _textEditorExitCommandCommand ??= new LambdaCommand(obj => 
            { 
            
            });

        #endregion

        #region JobStartCommand

        private ICommand _jobStartCommand = null;
        public ICommand JobStartCommand =>
            _jobStartCommand ??= new LambdaCommand(
                obj =>
                {

                });

        #endregion 


        #endregion
    }
}
