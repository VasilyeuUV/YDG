using System.Windows;
using YDG.Infrastructure.Commands.Base;

namespace YDG.Infrastructure.Commands
{

    /// <summary>
    /// Класс конкретной команды
    /// </summary>
    internal class CloseApplicationCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
