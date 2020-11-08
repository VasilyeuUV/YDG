using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using YDG.Infrastructure.Commands;
using YDG.Infrastructure.Converters;
using YDG.Infrastructure.Dialogs;
using YDG.Infrastructure.FileServises;
using YDG.Interfaces;
using YDG.Models;
using YDG.ViewModels.Base;
using YDG.Views.Windows;


namespace YDG.ViewModels
{
    /// <summary>
    /// ViewModel Главного меню приложения
    /// </summary>
    internal class MainMenuViewModel : ViewModelBase
    {

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
                    var posts = obj as ICollection<YDPostModel>;
                    SaveFile(posts);
                });

        private void SaveFile(ICollection<YDPostModel> records)
        {            
            IFileService fileService = new CsvFileService();
            IDialogService dialogService = new DefaultDialogService();

            try
            {
                if (records == null || records.Count < 1) 
                { 
                    throw new Exception("Нет данных для сохранения"); 
                }

                if (dialogService.SaveFileDialog() == true)
                {
                    fileService.Save(dialogService.FilePath, PostToCsvConverter.Convert(records));
                    dialogService.ShowMessage("Файл сохранен");
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }

        #endregion

        #endregion
    }
}
