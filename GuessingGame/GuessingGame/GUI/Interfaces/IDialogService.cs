using GuessingGameReproduction.Core;

namespace GuessingGameReproduction.GUI
{
    public interface IDialogService
    {
        bool CanProceed { get; }
        bool IsAnswerYes(Node node);
        bool IsGuessYes(Node currentGuess);
        string ShowPromptDialog(string message);
        void ShowGameOverMessage();
    }
}
