using GuessingGameReproduction.Core;
using GuessingGameReproduction.GUI;
using UnitTests.Mock;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class GuessingGameTests
    {
        IDialogService mockService;
        Game game;

        [SetUp]
        public void Init()
        {
            mockService = new MockService();
            game = new Game(mockService);
        }

        [Test]
        public void AskAnswerYes()
        {
            ((MockService)mockService).DialogAnswerYes = true;
            game.Ask(game.DecisionTree.Tree);
            Assert.That(game.CurrentGuess.AnswerYes != null);
        }

        [Test]
        public void AskAnswerNo()
        {
            ((MockService)mockService).DialogAnswerYes = false;
            game.Ask(game.DecisionTree.Tree);
            Assert.That(game.CurrentGuess.AnswerNo != null);
        }

        [Test]
        public void GuessYes() 
        {
            game.Ask(game.DecisionTree.Tree);
            ((MockService)mockService).DialogGuessYes = true;
            game.Guess(game.DecisionTree.Tree, true);
            Assert.That(game.IsGameOver);
        }

        [Test]
        public void GuessNo()
        {
            game.Ask(game.DecisionTree.Tree);
            int previousCount = game.DecisionTree.NodesCount;
            ((MockService)mockService).DialogGuessYes = false;
            game.Guess(game.DecisionTree.Tree, true);
            Assert.Less(previousCount, game.DecisionTree.NodesCount);
        }

        [Test]
        public void AskAnswerYesGuessYes()
        {
            ((MockService)mockService).DialogAnswerYes = true;
            game.Ask(game.DecisionTree.Tree);
            game.CurrentGuess.AnswerYes = null;
            ((MockService)mockService).DialogGuessYes = true;
            game.Guess(game.CurrentGuess, true);
            Assert.That(game.IsGameOver);
        }

        [Test]
        public void AskAnswerYesGuessNo()
        {
            int previousCount = game.DecisionTree.NodesCount;
            ((MockService)mockService).DialogAnswerYes = true;
            game.Ask(game.DecisionTree.Tree);
            game.CurrentGuess.AnswerYes = null;
            ((MockService)mockService).DialogGuessYes = true;
            game.Guess(game.CurrentGuess, true);
            Assert.Less(previousCount, game.DecisionTree.NodesCount);
        }

        [Test]
        public void AskAnswerNoGuessYes()
        {
            int previousCount = game.DecisionTree.NodesCount;
            ((MockService)mockService).DialogAnswerYes = false;
            game.Ask(game.DecisionTree.Tree);
            game.CurrentGuess.AnswerYes = null; 
            ((MockService)mockService).DialogGuessYes = true;
            game.Guess(game.CurrentGuess, true);
            Assert.Less(previousCount, game.DecisionTree.NodesCount);
        }

        [Test]
        public void AskAnswerNoGuessNo()
        {
            int previousCount = game.DecisionTree.NodesCount;
            ((MockService)mockService).DialogAnswerYes = false;
            game.Ask(game.DecisionTree.Tree);
            game.CurrentGuess.AnswerYes = null; 
            ((MockService)mockService).DialogGuessYes = true;
            game.Guess(game.CurrentGuess, true);
            Assert.That(game.IsGameOver);
        }

        [Test]
        public void AddQuestion()
        {
            game.Ask(game.DecisionTree.Tree);
            int previousCount = game.DecisionTree.NodesCount;
            game.AddNewQuestion(game.DecisionTree.Tree, true);
            Assert.Less(previousCount, game.DecisionTree.NodesCount);
        }
    }
}
