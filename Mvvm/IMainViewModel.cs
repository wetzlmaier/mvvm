using System.Windows.Media;

namespace Mvvm
{
    public interface IMainViewModel : IViewModel
    {
        ImageSource? Icon { get; }

        void CloseWindow();

        void Shutdown(int exitCode);
    }
}
