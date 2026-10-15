namespace Mvvm
{
    public interface IMessageService
    {
        void ShowMessage(string message, string caption);

        void ShowMessage(string message);

        void ShowWarning(string message, string caption);

        void ShowWarning(string message);

        void ShowError(string message, string caption);

        void ShowError(string message);

        MessageResult ShowYesNoCancelQuestion(string message, string caption);

        MessageResult ShowYesNoQuestion(string message, string caption);

        MessageResult ShowOkCancelQuestion(string message, string caption);
    }
}
