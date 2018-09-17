using GuessingGameReproduction.Core;
using GuessingGameReproduction.GUI;
using UnitTests.Mock;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class GuessingGameTests
    {
        Game game;

        [SetUp]
        public void Init()
        {
            IDialogService dialogMock = new DialogMock();
            game = new Game(dialogMock);
        }

        [Test]
        public void AskAnswerYes()
        {
            game.Ask(game.DecisionTree.Tree);
            // What?
        }

        [Test]
        public void AskAnswerNo()
        {
            game.Ask(game.DecisionTree.Tree);
            // Now?
        }

        [Test]
        public void GuessAnswerYes()
        {
            int count = game.DecisionTree.NodesCount;
            game.Guess(game.DecisionTree.Tree, true);
            //Assert.That(count, Is count == game.DecisionTree.NodesCount);
        }
    }
}
