using System.ComponentModel.Composition;


namespace Mvvm.Winforms
{
    [Export(typeof(IMessageService))]
    public class MessageService : IMessageService
    {
        public void ShowMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public void ShowMessage(string message)
        {
            ShowMessage(message, "Message");
        }

        public void ShowWarning(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void ShowWarning(string message)
        {
            ShowWarning(message, "Warning");
        }

        public void ShowError(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowError(string message)
        {
            ShowError(message, "Error");
        }

        public MessageResult ShowYesNoCancelQuestion(string message, string caption)
        {
            return (MessageResult)MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
        }

        public MessageResult ShowYesNoQuestion(string message, string caption)
        {
            return (MessageResult)MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public MessageResult ShowOkCancelQuestion(string message, string caption)
        {
            return (MessageResult)MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
    }
}
