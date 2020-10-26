using System.Windows;
using System.Windows.Input;
using YDG.Data;
using YDG.Infrastructure.Commands;
using YDG.ViewModels.Base;
using YDG.Views.Windows;

namespace YDG.ViewModels
{
    /// <summary>
    /// ViewModel Главного меню приложения
    /// </summary>
    internal class MainMenuViewModel : ViewModelBase
    {

        //public string Html { get; private set; }


        #region КОМАНДЫ

        #region CloseApplicationCommand

        private ICommand _closeApplicationCommand = null;
        public ICommand CloseApplicationCommand =>
            _closeApplicationCommand ??= new LambdaCommand(obj => { Application.Current.Shutdown(); });

        #endregion

        #region OpenTextEditorCommand

        private ICommand _openTextEditorCommand = null;
        public ICommand OpenTextEditorCommand =>
            _openTextEditorCommand ??= new LambdaCommand(
                obj => 
                {
                    TextEditorWindow te = new TextEditorWindow();
                    te.ShowDialog();
                    //this.Html = YDGData.Html;
                });

        #endregion 


        #endregion





        // РЕАЛИЗАЦИЯ ОДНОЙ КОМАНДЫ (подробный вариант)

        //public MainMenuViewModel()
        //{
        //    //
        //    #region Команды
        //    CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
        //    #endregion
        //}


        //#region КОМАНДЫ

        //#region CloseApplicationCommand
        //public ICommand CloseApplicationCommand { get; }

        ///// <summary>
        ///// Условия доступности команды (закрытие приложения)
        ///// </summary>
        ///// <param name="p"></param>
        ///// <returns></returns>
        //private bool CanCloseApplicationCommandExecute(object p) => true;

        ///// <summary>
        ///// Работает, когда команда выполняется
        ///// </summary>
        ///// <param name="p"></param>
        //private void OnCloseApplicationCommandExecuted(object p)
        //{
        //    Application.Current.Shutdown();
        //} 
        //#endregion

        //#endregion

    }
}
