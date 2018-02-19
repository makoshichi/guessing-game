using GuessingGameReproduction.GUI;
using System;
using System.Windows.Forms;

namespace GuessingGameReproduction.Core
{
    class Game
    {
        private Node firstNode;
        private Node currentGuess = null;

        public Game()
        {
            SetFirstQuestion();
            Start();
        }

        private void SetFirstQuestion()
        {
            firstNode = new Node("lives in water", String.Empty);
            var answerYes = new Node();
            answerYes.Answer = "shark";
            firstNode.AnswerYes = answerYes;
            var answerNo = new Node();
            answerNo.Answer = "monkey";
            firstNode.AnswerNo = answerNo;
        }

        private void Start()
        {
            var proceed = MessageBox.Show("Think about an animal...", "Guessing Game", MessageBoxButtons.OKCancel);

            if (!proceed.Equals(DialogResult.OK))
                return;

            currentGuess = null;
            Ask(firstNode);
        }

        private void Ask(Node node)
        {
            bool isAnswerYes = MessageBox.Show(String.Format("Does the animal that you thought about {0}?", node.Question), 
                "Guessing Game", MessageBoxButtons.YesNo).Equals(DialogResult.Yes);

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

        private void Guess(Node node, bool isAnswerYes)
        {
            var guess = MessageBox.Show(String.Format("Is the animal you thought about a {0}?", currentGuess.Answer), 
                "Guessing Game", MessageBoxButtons.YesNo);

            if (guess.Equals(DialogResult.Yes))
                MessageBox.Show("I win!", "Guessing Game");
            else
                AddNewQuestion(node, isAnswerYes);

            Start();
        }

        private void AddNewQuestion(Node node, bool isAnswerYes)
        {
            var newAnswer = InputBox.Show("What was the animal that you thought about?");

            var newQuestion = 
                InputBox.Show(String.Format("A {0} _______ but a {1} does not (Fill it with an animal trait, like 'lives in water').", newAnswer, currentGuess.Answer));

            var newNode = new Node(newQuestion, newAnswer);

            if (isAnswerYes)
                node.AnswerYes = newNode;
            else
                node.AnswerNo = newNode;
        }
    }
}
