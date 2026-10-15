using System.ComponentModel.Composition;
using System.Windows;

namespace Mvvm.Wpf
{
    [Export(typeof(IMessageService))]
    public class MessageService : IMessageService
    {
        public void ShowMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.None);
        }

        public void ShowMessage(string message)
        {
            ShowMessage(message, "Message");
        }

        public void ShowWarning(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowWarning(string message)
        {
            ShowWarning(message, "Warning");
        }

        public void ShowError(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowError(string message)
        {
            ShowError(message, "Error");
        }

        public MessageResult ShowYesNoCancelQuestion(string message, string caption)
        {
            return (MessageResult)MessageBox.Show(message, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
        }

        public MessageResult ShowYesNoQuestion(string message, string caption)
        {
            return (MessageResult)MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        }

        public MessageResult ShowOkCancelQuestion(string message, string caption)
        {
            return (MessageResult)MessageBox.Show(message, caption, MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
        }
    }
}
