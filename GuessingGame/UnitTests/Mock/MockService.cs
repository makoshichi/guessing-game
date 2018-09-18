using GuessingGameReproduction.Core;
using GuessingGameReproduction.GUI;

namespace UnitTests.Mock
{
    class MockService : IDialogService
    {
        public bool DialogAnswerYes { get; set; }
        public bool DialogGuessYes { get; set; }
        public bool IsStart { get; set; }

        public bool CanProceed => IsStart;

        public bool IsAnswerYes(Node node)
        {
            return DialogAnswerYes;
        }

        public bool IsGuessYes(Node currentGuess)
        {
            return DialogGuessYes;
        }

        public void ShowGameOverMessage() { }

        public string ShowPromptDialog(string message)
        {
            return string.Empty;
        }
    }
}
