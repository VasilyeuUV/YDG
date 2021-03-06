﻿using YDG.ViewModels.Base;
using YDG.ViewModels.DataModels;

namespace YDG.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {

        #region ЗАГОЛОВОК ОКНА

        private string _title = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;       // титул окна как название проета

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
            //{   
            //// Полная реализация
            //if (Equals(_title, value)) { return; }
            //_title = value;
            //OnPropertyChanged();
            //}
        }

        #endregion


        #region MAINMENU
        private MainMenuViewModel _mainMenuViewModel = null;

        public MainMenuViewModel MainMenuViewModel
        {
            get
            {
                if (_mainMenuViewModel == null)
                {
                    _mainMenuViewModel = new MainMenuViewModel();
                }
                return _mainMenuViewModel;
            }
        }
        #endregion


        #region STATUSBAR
        private StatusBarViewModel _statusBarViewModel = null;

        public StatusBarViewModel StatusBarViewModel
        {
            get
            {
                if (_statusBarViewModel == null)
                {
                    _statusBarViewModel = new StatusBarViewModel();
                }
                return _statusBarViewModel;
            }
        }
               
        #endregion


        #region YDGData
        private YDGViewModel _yGDViewModel = null;
        public YDGViewModel YGDViewModel => _yGDViewModel ??= new YDGViewModel();
        #endregion

    }
}
