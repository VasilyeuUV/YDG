using Microsoft.Win32;
using System;
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

        #region FileSaveAsCommand

        private ICommand _fileSaveAsCommand = null;
        public ICommand FileSaveAsCommand =>
            _fileSaveAsCommand ??= new LambdaCommand(
                obj =>
                {
                    SaveFile();
                });

        private void SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xls files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
            }
            else
                return;
            //сохраняем Workbook
            wb.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            saveFileDialog.Dispose();
        }
                if (res == DialogResult.Cancel)
                {
                    StatusLabel.Text = "Сохранение результатов экспорта отменено";
                }
}




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
