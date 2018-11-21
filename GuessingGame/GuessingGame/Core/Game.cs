using GuessingGameReproduction.GUI;

namespace GuessingGameReproduction.Core
{
    public class Game
    {
        private IDialogService dialogService;
        private Node rootNode;
        public Node CurrentGuess { get; set; }
        public DecisionTree DecisionTree { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool IsCurrentAnswerYes { get; private set; }

        public Game(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            SetFirstQuestion();
            CurrentGuess = null;
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

        private Node ChooseFromRootNode(bool isAnswerYes)
        {
            return isAnswerYes ? rootNode.AnswerYes : rootNode.AnswerNo;
        }

        public void Start()
        {
            IsGameOver = false;

            if (!dialogService.CanProceed)
                return;

            CurrentGuess = null;
            Ask(rootNode);
        }

        public void Ask(Node node)
        {
            bool isAnswerYes = dialogService.IsAnswerYes(node);

            if (CurrentGuess == null) 
                CurrentGuess = node = ChooseFromRootNode(isAnswerYes);
            else if (isAnswerYes)
                CurrentGuess = node;

            var nextQuestion = isAnswerYes ? CurrentGuess.AnswerYes : node.AnswerNo;

            if (nextQuestion == null)
            {
                Guess(node, isAnswerYes);
                Start();
            }
            else
                Ask(nextQuestion);
        }

        public void Guess(Node node, bool isAnswerYes)
        {
            if (dialogService.IsGuessYes(CurrentGuess))
            {
                dialogService.ShowGameOverMessage();
                IsGameOver = true;
            }
            else
                AddNewQuestion(node, isAnswerYes);

            //Start();
        }

        public void AddNewQuestion(Node node, bool isAnswerYes)
        {
            var newAnswer = dialogService.ShowPromptDialog("What was the animal that you thought about?");

            var newQuestion =
                dialogService.ShowPromptDialog($"A {newAnswer} _______ but a {CurrentGuess.Answer} does not (Fill it with an animal trait, like 'lives in water').");

            var newNode = new Node(newQuestion, newAnswer);

            if (isAnswerYes)
                node.AnswerYes = newNode;
            else
                node.AnswerNo = newNode;

            DecisionTree.Tree = node;
        }
    }
}
