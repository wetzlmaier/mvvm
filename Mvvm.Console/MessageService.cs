using System.ComponentModel.Composition;

namespace Mvvm.Console
{
    [Export(typeof(IMessageService))]
    public class MessageService : IMessageService
    {
        public void ShowMessage(string message, string caption)
        {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine($"{caption} | {message}");
        }

        public void ShowMessage(string message)
        {
            ShowMessage(message, "Message");
        }

        public void ShowWarning(string message, string caption)
        {
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine($"{caption} | {message}");
        }

        public void ShowWarning(string message)
        {
            ShowWarning(message, "Warning");
        }

        public void ShowError(string message, string caption)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.Error.WriteLine($"{caption} | {message}");
        }

        public void ShowError(string message)
        {
            ShowError(message, "Error");
        }

        public MessageResult ShowYesNoCancelQuestion(string message, string caption)
        {
            throw new NotImplementedException();
        }

        public MessageResult ShowYesNoQuestion(string message, string caption)
        {
            throw new NotImplementedException();
        }

        public MessageResult ShowOkCancelQuestion(string message, string caption)
        {
            throw new NotImplementedException();
        }
    }
}
