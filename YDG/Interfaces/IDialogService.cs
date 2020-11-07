namespace YDG.Interfaces
{
    public interface IDialogService
    {
        /// <summary>
        /// показ сообщения
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);

        /// <summary>
        /// путь к выбранному файлу
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// открытие файла
        /// </summary>
        /// <returns></returns>
        bool OpenFileDialog();

        /// <summary>
        /// сохранение файла
        /// </summary>
        /// <returns></returns>
        bool SaveFileDialog(); 
    }
}
