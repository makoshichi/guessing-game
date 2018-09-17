using GuessingGameReproduction.Core;
using System.Windows.Forms;

namespace GuessingGameReproduction.GUI
{
    public interface IDialogService
    {
        DialogResult FirstQuestion { get; }
        bool IsAnswerYes(Node node);
        DialogResult Guess(Node currentGuess);
        string ShowPromptDialog(string message);
        void ShowGameOverMessage();
    }
}
