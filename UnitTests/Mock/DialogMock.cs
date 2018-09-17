using GuessingGameReproduction.Core;
using GuessingGameReproduction.GUI;

namespace UnitTests.Mock
{
    class DialogMock : IDialogService
    {
        public bool AnswerYes { get; set; }
        public bool GuessYes { get; set; }
        public bool IsStart { get; set; }

        public bool CanProceed => IsStart;

        public bool IsAnswerYes(Node node)
        {
            return AnswerYes;
        }

        public bool IsGuessYes(Node currentGuess)
        {
            return GuessYes;
        }

        public void ShowGameOverMessage() { }

        public string ShowPromptDialog(string message)
        {
            return string.Empty;
        }
    }
}
