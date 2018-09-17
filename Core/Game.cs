using GuessingGameReproduction.GUI;

namespace GuessingGameReproduction.Core
{
    public class Game
    {
        private IDialogService dialogService;
        private Node rootNode;
        private Node currentGuess = null;
        public DecisionTree DecisionTree { get; private set; }
        public bool IsGameOver { get; private set; }

        public Game(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            SetFirstQuestion();
        }

        private void SetFirstQuestion()
        {
            rootNode = new Node("lives in water", string.Empty);
            var answerYes = new Node
            {
                Answer = "shark"
            };
            rootNode.AnswerYes = answerYes;

            var answerNo = new Node
            {
                Answer = "monkey"
            };
            rootNode.AnswerNo = answerNo;

            DecisionTree = new DecisionTree(rootNode);
        }

        public void Start()
        {
            IsGameOver = false;

            if (!dialogService.CanProceed)
                return;

            currentGuess = null;
            Ask(rootNode);
        }

        public void Ask(Node node)
        {
            if (dialogService.IsAnswerYes(node))
            {
                if (currentGuess == null)
                    currentGuess = node = node.AnswerYes;
                else
                    currentGuess = node;

                if (currentGuess.AnswerYes == null)
                    Guess(node, true);
                else
                    Ask(currentGuess.AnswerYes);
            }
            else
            {
                if (currentGuess == null)
                    currentGuess = node = node.AnswerNo;

                if (node.AnswerNo == null)
                    Guess(node, false);
                else
                    Ask(node.AnswerNo);
            }
        }

        public void Guess(Node node, bool isAnswerYes)
        {
            if (dialogService.IsGuessYes(currentGuess))
            {
                dialogService.ShowGameOverMessage();
                IsGameOver = true;
            }
            else
                AddNewQuestion(node, isAnswerYes);

            Start();
        }

        public void AddNewQuestion(Node node, bool isAnswerYes)
        {
            var newAnswer = dialogService.ShowPromptDialog("What was the animal that you thought about?");

            var newQuestion =
                dialogService.ShowPromptDialog($"A {newAnswer} _______ but a {currentGuess.Answer} does not (Fill it with an animal trait, like 'lives in water').");

            var newNode = new Node(newQuestion, newAnswer);

            if (isAnswerYes)
                node.AnswerYes = newNode;
            else
                node.AnswerNo = newNode;

            DecisionTree.Tree = node;
        }

        // Tests:
        // AskAnswerYes
        // AskAnswerNo
        // GuessAnswerYes
        // GuesAnswerNo
        // AddNewQuestionAnswerYes
        // AddNewQuestionAnswerNo
        // AskMultipleQuestions
        // GuessMultipleAnswers
        // AddMultipleNewQuestions?
    }
}
