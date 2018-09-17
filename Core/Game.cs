using GuessingGameReproduction.GUI;
using System.Windows.Forms;

namespace GuessingGameReproduction.Core
{
    public class Game
    {
        private IDialogService dialogService;
        private Node firstNode;
        private Node currentGuess = null;
        public DecisionTree DecisionTree { get; private set; }
        public bool IsGameOver { get; private set; }

        public Game(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            IsGameOver = false;
            SetFirstQuestion();
            //Start();
        }

        private void SetFirstQuestion()
        {
            firstNode = new Node("lives in water", string.Empty);
            var answerYes = new Node();
            answerYes.Answer = "shark";
            firstNode.AnswerYes = answerYes;
            var answerNo = new Node();
            answerNo.Answer = "monkey";
            firstNode.AnswerNo = answerNo;
            DecisionTree = new DecisionTree(firstNode);
        }

        public void Start()
        {
            var proceed = dialogService.FirstQuestion;

            if (!proceed.Equals(DialogResult.OK))
                return;

            currentGuess = null;
            Ask(firstNode);
        }

        public void Ask(Node node)
        {
            bool isAnswerYes = dialogService.IsAnswerYes(node);

            if (isAnswerYes)
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
            var guess = dialogService.Guess(currentGuess);

            if (guess.Equals(DialogResult.Yes))
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
    }
}
