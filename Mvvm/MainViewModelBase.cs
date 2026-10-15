using Common.Reflection;
using Common.Windows.Media.Imaging;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Mvvm
{
    public abstract class MainViewModelBase : ViewModelBase, IMainViewModel
    {
        protected MainViewModelBase() 
        {
            if (IsInDesignMode)
                return;

            var mainWindow = Application.Current?.MainWindow;

            if (mainWindow == null)
                return;

            mainWindow.Closing += MainWindow_Closing;

            var icon = AssemblyHelper.LoadApplicationIcon();

            if (icon != null)
            {
                var bitmapSource = BitmapSourceHelper.LoadBitmap(icon.ToBitmap());

                if (bitmapSource != null)
                    Icon = bitmapSource;
            }
        }

        public void Shutdown(int exitCode)
        {
            if (IsInDesignMode)
                return;

            Application.Current?.Shutdown(exitCode);
        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            OnClosing(e);
        }

        protected virtual void OnClosing(CancelEventArgs e)
        {
            bool canClose = OnClosing();

            if (!canClose)
            {
                e.Cancel = true;
                return;
            }

            OnClosed();
        }

        public ImageSource? Icon { get; private set; }

        public void CloseWindow()
        {
            if (IsInDesignMode)
                return;

            Application.Current?.MainWindow?.Close();
        }
    }
}
