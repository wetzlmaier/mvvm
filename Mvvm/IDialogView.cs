using System.ComponentModel;

namespace Mvvm
{
    public interface IDialogView<TDialogResult> : IView
    {
        TDialogResult DialogResult { get; set; }

        event CancelEventHandler Closing;

        event EventHandler Closed;

        void Show();

        void Close();
    }
}
